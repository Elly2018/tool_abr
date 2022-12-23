using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

namespace Funique
{
    public sealed class ABRControl
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
                //proc.StartInfo.RedirectStandardInput = true;
                //proc.StartInfo.RedirectStandardOutput = true;
                //proc.StartInfo.RedirectStandardError = true;
                //proc.EnableRaisingEvents = true;
                proc.StartInfo.WorkingDirectory = workdir;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.FileName = "ffmpeg";
                return proc;
            }
        }

        Process proc = null;
        Thread thread = null;
        Queue<string> jobs = new Queue<string>();

        public void SetDirectory(string dir) => this.dir = dir;

        public void Call(M3U8Setting setting)
        {
            Kill();
            string[] args = setting.Arguments;
            foreach (var i in args) jobs.Enqueue(i);
            WriteCommand(args);
            if(thread != null)
            {
                thread.Interrupt();
            }
            thread = new Thread(BackgroundRunning);
            thread.Start();
        }
        public void Kill()
        {
            jobs.Clear();
            if (thread != null)
                thread.Interrupt();
            if (proc != null)
                proc.Kill();
        }
        void WriteCommand(string[] args)
        {
            string path = Path.Combine(workdir, "command.txt");
            string message = DateTime.UtcNow.ToString();
            message += "\n";
            foreach (var i in args) message += i;
            message += "\n";
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
            string args = string.Empty;
            while (jobs.TryDequeue(out args))
            {
                Debug.WriteLine(args);
                proc = process;
                try
                {
                    proc.StartInfo.Arguments = args;
                    proc.ErrorDataReceived += Proc_ErrorDataReceived;
                    proc.OutputDataReceived += Proc_ErrorDataReceived;
                    proc.Exited += Proc_Exited;
                    proc.Start();
                    proc.WaitForExit();
                    proc.Dispose();
                }
                catch(ThreadInterruptedException ex)
                {
                    proc.Kill();
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void Proc_Exited(object sender, System.EventArgs e)
        {
            string path = Path.Combine(proc.StartInfo.WorkingDirectory, "log.txt");
            string message = e.ToString();
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
        private void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string path = Path.Combine(proc.StartInfo.WorkingDirectory, "log.txt");
            string message = e.Data.ToString();
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
    }
}