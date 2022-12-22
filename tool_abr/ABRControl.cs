using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        Process process
        {
            get
            {
                Process proc = new Process();
                //proc.StartInfo.RedirectStandardInput = true;
                //proc.StartInfo.RedirectStandardOutput = true;
                //proc.StartInfo.RedirectStandardError = true;
                proc.EnableRaisingEvents = true;
                proc.StartInfo.WorkingDirectory = dir == null ? Directory.GetCurrentDirectory() : dir;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.FileName = "ffmpeg";
                return proc;
            }
        }

        Process proc;

        public void SetDirectory(string dir) => this.dir = dir;

        public void Call(M3U8Setting setting)
        {
            if (proc != null && !proc.HasExited) return;
            proc = process;
            proc.StartInfo.Arguments = setting.Arguments;
            string path = Path.Combine(proc.StartInfo.WorkingDirectory, "command.txt");
            if (File.Exists(path))
            {
                var writer = File.AppendText(path);
                writer.WriteLine(proc.StartInfo.Arguments);
                writer.Close();
            }
            else
            {
                File.WriteAllText(path, proc.StartInfo.Arguments);
            }
            Debug.WriteLine(proc.StartInfo.Arguments);
            proc.ErrorDataReceived += Proc_ErrorDataReceived;
            proc.OutputDataReceived += Proc_ErrorDataReceived;
            proc.Exited += Proc_Exited;
            proc.Start();
        }

        private void Proc_Exited(object sender, System.EventArgs e)
        {
            string path = Path.Combine(proc.StartInfo.WorkingDirectory, "log.txt");
            if (File.Exists(path))
            {
                var writer = File.AppendText(path);
                writer.WriteLine(e.ToString());
                writer.Close();
            }
            else
            {
                File.WriteAllText(path, e.ToString());
            }
        }

        private void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string path = Path.Combine(proc.StartInfo.WorkingDirectory, "log.txt");
            if (File.Exists(path))
            {
                var writer = File.AppendText(path);
                writer.WriteLine(e.Data.ToString());
                writer.Close();
            }
            else
            {
                File.WriteAllText(path, e.Data.ToString());
            }
        }
    }
}