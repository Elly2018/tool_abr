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

        Process process
        {
            get
            {
                Process proc = new Process();
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.FileName = "ffmpeg";
                return proc;
            }
        }
        public List<M3U8Setting> loads = new List<M3U8Setting>();
        public string[] filechanged = new string[0];
        public string[] loads_string = new string[0];

        public void Call(M3U8Setting setting)
        {
            Process proc = process;
            proc.StartInfo.Arguments = setting.Arguments;
            proc.Start();
        }
    }
}