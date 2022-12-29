using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Funique
{
    public sealed class ABRControl : IDisposable
    {
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
        public JobExecute[] Analysis(M3U8Setting setting)
        {
            setting.WorkDir = dir;
            return setting.Processes;
        }
        public void RunSingle(JobExecute job)
        {
            Kill();
            StartBackgroundProcess(new JobExecute[1] { job });
        }
        void StartBackgroundProcess(JobExecute[] args)
        {
            foreach (var i in args) jobs.Enqueue(i);
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
                        args.DoneProcess.Invoke();
                    }
                }
                catch (ThreadInterruptedException ex)
                {
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