using System.Collections.Generic;
using System.ComponentModel;

namespace Funique
{
    [System.Serializable]
    public sealed class M3U8Setting : INotifyPropertyChanged
    {
        public bool Sync;
        public string Input;
        public string MasterName;
        public HLSType Type;
        public int ListSize;
        public bool DeleteFlag;
        public bool AppendFlag;
        public int MuxingQueue;
        public string OutputM3U8FileName;
        public string OutputSegmentFileName;
        public ABRSetting[] Settings = new ABRSetting[0];

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Arguments
        {
            get
            {
                List<string> args = new List<string>();
                int SettingCount = Settings.Length;
                if (Sync) args.Add("-re");
                args.Add($"\"{Input}\"");
                for(int i = 0; i < SettingCount; i++)
                {
                    args.Add("-map");
                    args.Add("0:0");
                    args.Add("-map");
                    args.Add("0:1");
                }
                for (int i = 0; i < SettingCount; i++)
                {
                    ABRSetting target = Settings[i];
                    args.Add($"-s:v:{i}");
                    args.Add($"{target.Width}x{target.Height}");
                    args.Add($"-c:v:{i}");
                    args.Add(string.IsNullOrEmpty(target.VideoCodec) ? "copy" : target.VideoCodec);
                    args.Add($"-c:a:{i}");
                    args.Add(string.IsNullOrEmpty(target.AudioCodec) ? "copy" : target.AudioCodec);
                    args.Add($"-b:v:{i}");
                    args.Add($"{target.BitRate}k");
                }
                args.Add("-var_stream_map");
                List<string> buffer = new List<string>();
                for (int i = 0; i < SettingCount; i++)
                {
                    buffer.Add($"v:{i},a:{i}");
                }
                args.Add($"\"{string.Join(' ', buffer)}\"");
                args.Add("-master_pl_name");
                args.Add(MasterName);
                args.Add("-f");
                args.Add("hls");
                args.Add("-hls_segment_type");
                args.Add(HLSTypeUtility.HLSTypeDict[Type]);
                args.Add("-hls_list_size");
                args.Add($"{ListSize}");
                buffer.Clear();
                if (DeleteFlag) buffer.Add("delete_segments");
                if (AppendFlag) buffer.Add("append_list");
                if(buffer.Count > 0)
                {
                    args.Add("-hls_flags");
                    args.Add($"{string.Join('+', buffer)}");
                }
                args.Add("-max_muxing_queue_size");
                args.Add($"{MuxingQueue}");
                args.Add("-hls_segment_filename");
                args.Add($"\"{OutputSegmentFileName}\"");
                args.Add($"\"{OutputM3U8FileName}\"");
                return string.Join(' ', args);
            }
        }
    }
}