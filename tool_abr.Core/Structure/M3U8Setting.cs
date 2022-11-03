using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Funique
{
    [System.Serializable]
    public sealed class M3U8Setting : INotifyPropertyChanged
    {
        [JsonIgnore] public Dictionary<HLSType, string> HLSTypeDict => HLSTypeUtility.HLSTypeDict;

        bool _Sync;
        bool _StreamingLoop;
        bool _Wall_Clock;
        string _Input;
        string _MasterName;
        int _Type;
        int _ListSize;
        bool _DeleteFlag;
        bool _AppendFlag;
        int _MuxingQueue;
        string _OutputM3U8FileName;
        string _OutputSegmentFileName;
        ObservableCollection<ABRSetting> _Settings = new ObservableCollection<ABRSetting>();

        [JsonProperty] public bool Sync
        {
            set
            {
                _Sync = value;
                OnPropertyChanged("Sync");
            }
            get => _Sync;
        }
        [JsonProperty]
        public bool StreamingLoop
        {
            set
            {
                _StreamingLoop = value;
                OnPropertyChanged("StreamingLoop");
            }
            get => _StreamingLoop;
        }
        [JsonProperty]
        public bool Wall_Clock
        {
            set
            {
                _Wall_Clock = value;
                OnPropertyChanged("Wall_Clock");
            }
            get => _Wall_Clock;
        }
        [JsonProperty] public string Input
        {
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
            get => _Input;
        }
        [JsonProperty] public string MasterName
        {
            set
            {
                _MasterName = value;
                OnPropertyChanged("MasterName");
            }
            get => _MasterName;
        }
        [JsonProperty] public HLSType Type
        {
            set
            {
                _Type = (int)value;
                OnPropertyChanged("Type");
            }
            get => (HLSType)_Type;
        }
        [JsonProperty] public int ListSize
        {
            set
            {
                _ListSize = value;
                OnPropertyChanged("ListSize");
            }
            get => _ListSize;
        }
        [JsonProperty] public bool DeleteFlag
        {
            set
            {
                _DeleteFlag = value;
                OnPropertyChanged("DeleteFlag");
            }
            get => _DeleteFlag;
        }
        [JsonProperty] public bool AppendFlag
        {
            set
            {
                _AppendFlag = value;
                OnPropertyChanged("AppendFlag");
            }
            get => _AppendFlag;
        }
        [JsonProperty] public int MuxingQueue
        {
            set
            {
                _MuxingQueue = value;
                OnPropertyChanged("MuxingQueue");
            }
            get => _MuxingQueue;
        }
        [JsonProperty] public string OutputM3U8FileName
        {
            set
            {
                _OutputM3U8FileName = value;
                OnPropertyChanged("OutputM3U8FileName");
            }
            get => _OutputM3U8FileName;
        }
        [JsonProperty] public string OutputSegmentFileName
        {
            set
            {
                _OutputSegmentFileName = value;
                OnPropertyChanged("OutputSegmentFileName");
            }
            get => _OutputSegmentFileName;
        }
        [JsonProperty] public ObservableCollection<ABRSetting> Settings
        {
            set
            {
                _Settings = value;
                OnPropertyChanged("Settings");
            }
            get => _Settings;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void Save(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void Load(string path)
        {
            if (!File.Exists(path)) return;
            try
            {
                M3U8Setting load = JsonConvert.DeserializeObject<M3U8Setting>(File.ReadAllText(path));
                Sync = load.Sync;
                Input = load.Input;
                MasterName = load.MasterName;
                Type = load.Type;
                ListSize = load.ListSize;
                DeleteFlag = load.DeleteFlag;
                AppendFlag = load.AppendFlag;
                MuxingQueue = load.MuxingQueue;
                OutputM3U8FileName = load.OutputM3U8FileName;
                OutputSegmentFileName = load.OutputSegmentFileName;
                Settings = load.Settings;
            } 
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        [JsonIgnore] public string Arguments
        {
            get
            {
                List<string> args = new List<string>();
                int SettingCount = Settings.Count;
                if (Sync) args.Add("-re");
                if (StreamingLoop)
                {
                    args.Add("-stream_loop");
                    args.Add("-1");
                }
                if (Wall_Clock)
                {
                    args.Add("-use_wallclock_as_timestamps");
                    args.Add("-1");
                }
                args.Add("-i");
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
                    if(target.Width != 0 && target.Height != 0)
                    {
                        args.Add($"-s:v:{i}");
                        args.Add($"{target.Width}x{target.Height}");
                    }
                    args.Add($"-c:v:{i}");
                    args.Add(string.IsNullOrEmpty(target.VideoCodec) ? "copy" : target.VideoCodec);
                    args.Add($"-c:a:{i}");
                    args.Add(string.IsNullOrEmpty(target.AudioCodec) ? "copy" : target.AudioCodec);
                    if(target.BitRate != 0)
                    {
                        args.Add($"-b:v:{i}");
                        args.Add($"{target.BitRate}k");
                    }
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