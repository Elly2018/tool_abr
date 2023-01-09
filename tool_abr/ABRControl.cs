using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Funique
{
    public sealed class ABRControl : IDisposable
    {
        internal const int CTRL_C_EVENT = 0;
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        public static ABRControl Instance
        {
            get
            {
                if (_Instance == null) _Instance = new ABRControl();
                return _Instance;
            }
        }
        static ABRControl _Instance;

        string dir = null;
        string workdir => dir == null ? Directory.GetCurrentDirectory() : dir;
        Process process
        {
            get
            {
                Process proc = new Process();
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.EnableRaisingEvents = true;
                proc.StartInfo.WorkingDirectory = workdir;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "ffmpeg";
                proc.OutputDataReceived += Proc_OutputDataReceived;
                proc.ErrorDataReceived += Proc_OutputDataReceived;
                return proc;
            }
        }

        Process proc = null;
        Thread thread = null;
        Queue<JobExecute> jobs = new Queue<JobExecute>();
        public Action<string> log = null;

        public void SetDirectory(string dir)
        {
            this.dir = dir;
        }

        public void Call(JobExecute[] setting)
        {
            Kill();
            StartBackgroundProcess(setting);
        }
        public void Kill()
        {
            jobs.Clear();
            try
            {
                if (thread != null)
                    thread.Interrupt();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            try
            {
                if (proc != null)
                    proc.Kill();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        public bool Close()
        {
            if(proc != null && !proc.HasExited)
            {
                proc.StandardInput.WriteLine("q");
            }
            return false;
        }
        public JobExecute[] Analysis(M3U8Setting setting)
        {
            setting.WorkDir = dir;
            return setting.Processes;
        }
        public void Print(JobExecute[] jobs)
        {
            WriteCommand(jobs);
        }
        public void RunSingle(JobExecute job)
        {
            Kill();
            StartBackgroundProcess(new JobExecute[1] { job });
        }
        void StartBackgroundProcess(JobExecute[] args)
        {
            foreach (var i in args) jobs.Enqueue(i);
            //jobs = new Queue<JobExecute>(jobs.Reverse());
            WriteCommand(args);
            thread = new Thread(BackgroundRunning);
            thread.Start();
        }
        void WriteCommand(JobExecute[] args)
        {
            string path = Path.Combine(workdir, "command.txt");
            string message = DateTime.UtcNow.ToString();
            message += "\n";
            foreach (var i in args)
            {
                foreach (var j in i.ArgumentList)
                {
                    message += (j + "\n");
                }
                message += "\n";
            }
            message += "\n\n";
            if (File.Exists(path))
            {
                var writer = File.AppendText(path);
                writer.WriteLine(message);
                writer.Close();
            }
            else
            {
                File.WriteAllText(path, message);
            }
        }
        void BackgroundRunning()
        {
            JobExecute args = null;
            while (jobs.TryDequeue(out args))
            {
                try
                {
                    var al = args.ArgumentList;
                    if (args.BeginProcess != null)
                    {
                        try
                        {
                            args.BeginProcess.Invoke();
                        }
                        catch (Exception ex)
                        {
                            log.Invoke($"{ex.Message}\n{ex.StackTrace}");
                        }
                    }
                    for (int i = 0; i < al.Length; i++)
                    {
                        string current = al[i];
                        proc = process;
                        switch (args.Type)
                        {
                            case JobType.CMD:
                                proc.StartInfo.FileName = "cmd";
                                current = "-c " + current;
                                break;
                            default:
                            case JobType.FFMPEG:
                                proc.StartInfo.FileName = "ffmpeg";
                                break;
                            case JobType.FFPROBE:
                                proc.StartInfo.FileName = "ffprobe";
                                break;
                        }
                        Debug.WriteLine($"{proc.StartInfo.FileName} {current}");
                        proc.StartInfo.Arguments = current;
                        proc.Start();
                        proc.BeginOutputReadLine();
                        proc.BeginErrorReadLine();
                        proc.WaitForExit();
                        proc.OutputDataReceived -= Proc_OutputDataReceived;
                        proc.ErrorDataReceived -= Proc_OutputDataReceived;
                        proc.Dispose();
                    }
                    if (args.DoneProcess != null)
                    {
                        try
                        {
                            args.DoneProcess.Invoke();
                        }
                        catch(Exception ex)
                        {
                            log.Invoke($"{ex.Message}\n{ex.StackTrace}");
                        }
                    }
                }
                catch (ThreadInterruptedException ex)
                {
                    if(!proc.HasExited)
                        proc.Kill();
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e != null && e.Data != null && log != null)
            {
                var strs = e.Data.ToString().Split(Environment.NewLine);
                foreach (var i in strs)
                    log.Invoke(i + "\n");
            }
        }

        public void Dispose()
        {
            Kill();
        }
    }
}