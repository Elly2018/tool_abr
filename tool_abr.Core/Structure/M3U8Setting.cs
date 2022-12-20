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
        [JsonIgnore] public Dictionary<HWAccelType, string> HWAccelTypeDict => HLSTypeUtility.HWAccelTypeDict;
        [JsonIgnore] public Dictionary<HLSPlaylistType, string> HLSPlaylistTypeDict => HLSTypeUtility.HLSPlaylistTypeDict;

        bool _Sync;
        bool _StreamingLoop;
        bool _Wall_Clock;
        bool _Cache;
        string _Input;
        string _MasterName;
        int _Type;
        int _HWAccelType;
        int _ListSize;
        int _HLSInitTime;
        int _HLSTime;
        int _StartTime;
        int _FKeyframe;
        int _PlaylistType;
        bool _DeleteFlag;
        bool _AppendFlag;
        bool _IndenFlag;
        bool _SplitFlag;
        int _MuxingQueue;
        string _OutputM3U8FileName;
        string _OutputSegmentFileName;
        ObservableCollection<ABRSetting> _Settings = new ObservableCollection<ABRSetting>();

        [JsonProperty] 
        public bool Sync
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
        [JsonProperty]
        public bool Cache
        {
            set
            {
                _Cache = value;
                OnPropertyChanged("Cache");
            }
            get => _Cache;
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
        [JsonProperty]
        public HWAccelType HWAccelType
        {
            set
            {
                _HWAccelType = (int)value;
                OnPropertyChanged("HWAccelType");
            }
            get => (HWAccelType)_HWAccelType;
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
        [JsonProperty]
        public int HLSInitTime
        {
            set
            {
                _HLSInitTime = value;
                OnPropertyChanged("HLSInitTime");
            }
            get => _HLSInitTime;
        }
        [JsonProperty] public int HLSTime
        {
            set
            {
                _HLSTime = value;
                OnPropertyChanged("HLSTime");
            }
            get => _HLSTime;
        }
        [JsonProperty]
        public int StartTime
        {
            set
            {
                _StartTime = value;
                OnPropertyChanged("StartTime");
            }
            get => _StartTime;
        }
        [JsonProperty]
        public int FKeyframe
        {
            set
            {
                _FKeyframe = value;
                OnPropertyChanged("FKeyframe");
            }
            get => _FKeyframe;
        }
        [JsonProperty] public HLSPlaylistType PlaylistType
        {
            set
            {
                _PlaylistType = (int)value;
                OnPropertyChanged("PlaylistType");
            }
            get => (HLSPlaylistType)_PlaylistType;
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
        [JsonProperty] public bool IndenFlag
        {
            set
            {
                _IndenFlag = value;
                OnPropertyChanged("IndenFlag");
            }
            get => _IndenFlag;
        }
        [JsonProperty] public bool SplitFlag
        {
            set
            {
                _SplitFlag = value;
                OnPropertyChanged("SplitFlag");
            }
            get => _SplitFlag;
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
                Cache = load.Cache;
                Input = load.Input;
                MasterName = load.MasterName;
                Type = load.Type;
                HWAccelType = load.HWAccelType;
                ListSize = load.ListSize;
                HLSInitTime = load.HLSInitTime;
                HLSTime = load.HLSTime;
                StartTime = load.StartTime;
                FKeyframe = load.FKeyframe;
                DeleteFlag = load.DeleteFlag;
                AppendFlag = load.AppendFlag;
                IndenFlag = load.IndenFlag;
                SplitFlag = load.SplitFlag;
                MuxingQueue = load.MuxingQueue;
                PlaylistType = load.PlaylistType;
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
                args.Add("-hide_banner");
                if(HWAccelType != HWAccelType.None)
                {
                    args.Add("-hwaccel");
                    args.Add(HWAccelTypeDict[HWAccelType]);
                }
                if (Sync) args.Add("-re");
                if (StreamingLoop)
                {
                    args.Add("-stream_loop");
                    args.Add("-1");
                }
                if (Wall_Clock)
                {
                    args.Add("-use_wallclock_as_timestamps");
                    args.Add("1");
                }
                args.Add("-i");
                args.Add($"\"{Input}\"");
                if(FKeyframe > 0)
                {
                    args.Add("-force_key_frames");
                    args.Add($"\"expr: gte(t, n_forced * {FKeyframe})\"");
                }
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
                    if(target.MaxRate != 0)
                    {
                        if (target.VideoCodec.Contains("nvenc"))
                        {
                            args.Add($"-rc");
                            args.Add($"cbr");
                            args.Add($"-cbr");
                            args.Add($"1");
                        }
                        args.Add($"-crf:{i}");
                        args.Add($"{target.CRF}");
                        if (target.VideoCodec == "libx265")
                        {
                            args.Add($"-x265-params");
                            args.Add($"\"vbv-maxrate={target.MaxRate}:vbv-bufsize={target.MaxRate * 2}:bitrate={target.MaxRate}\"");
                        }
                        else if (target.VideoCodec == "libx264")
                        {
                            args.Add($"-x264-params");
                            args.Add($"\"vbv-maxrate={target.MaxRate}:vbv-bufsize={target.MaxRate * 2}:bitrate={target.MaxRate}\"");
                        }
                        else
                        {
                            args.Add($"-maxrate:{i}");
                            args.Add($"{target.MaxRate}k");
                            args.Add($"-bufsize:{i}");
                            args.Add($"{target.MaxRate * 2}k");
                            args.Add($"-b:v:{i}");
                            args.Add($"{target.MaxRate}k");
                        }

                        if(target.VideoCodec == "hevc_nvenc")
                        {

                        }
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
                if(StartTime > 0)
                {
                    args.Add("-hls_start_number_source");
                    args.Add(StartTime.ToString());
                }
                args.Add("-hls_allow_cache");
                args.Add((Cache ? 1 : 0).ToString());
                args.Add("-hls_init_time");
                args.Add(HLSInitTime.ToString());
                args.Add("-hls_time");
                args.Add(HLSTime.ToString());
                args.Add("-hls_segment_type");
                args.Add(HLSTypeUtility.HLSTypeDict[Type]);
                if(PlaylistType != HLSPlaylistType.None)
                {
                    args.Add("-hls_playlist_type");
                    args.Add(HLSTypeUtility.HLSPlaylistTypeDict[PlaylistType]);
                }
                if(ListSize != 0)
                {
                    args.Add("-hls_list_size");
                    args.Add($"{ListSize}");
                }
                buffer.Clear();
                if (DeleteFlag) buffer.Add("delete_segments");
                if (AppendFlag) buffer.Add("append_list");
                if (IndenFlag) buffer.Add("independent_segments");
                if (SplitFlag) buffer.Add("split_by_time");
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