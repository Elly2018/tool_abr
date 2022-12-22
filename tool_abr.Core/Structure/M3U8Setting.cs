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

        #region Fields
        bool _Sync;
        int _StreamingLoop;
        bool _Wall_Clock;
        bool _Cache;
        int _StartNumber;
        int _VFrame;
        string _Input;
        string _InputAudio;
        string _InputSubtitle;
        string _InputFramerate;
        string _OutputFramerate;
        bool _HaveAudio;
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
        bool _SeperateAudio;
        string _OutputM3U8FileName;
        string _OutputSegmentFileName;
        ObservableCollection<ABRSetting> _Settings = new ObservableCollection<ABRSetting>();
        #endregion

        #region Properties
        [JsonProperty] public bool Sync
        {
            set
            {
                _Sync = value;
                OnPropertyChanged("Sync");
            }
            get => _Sync;
        }
        [JsonProperty] public int StreamingLoop
        {
            set
            {
                _StreamingLoop = value;
                OnPropertyChanged("StreamingLoop");
            }
            get => _StreamingLoop;
        }
        [JsonProperty] public bool Wall_Clock
        {
            set
            {
                _Wall_Clock = value;
                OnPropertyChanged("Wall_Clock");
            }
            get => _Wall_Clock;
        }
        [JsonProperty] public bool Cache
        {
            set
            {
                _Cache = value;
                OnPropertyChanged("Cache");
            }
            get => _Cache;
        }
        [JsonProperty] public bool HaveAudio
        {
            set
            {
                _HaveAudio = value;
                OnPropertyChanged("HaveAudio");
            }
            get => _HaveAudio;
        }
        [JsonProperty] public int StartNumber
        {
            set
            {
                _StartNumber = value;
                OnPropertyChanged("StartNumber");
            }
            get => _StartNumber;
        }
        [JsonProperty] public int VFrame
        {
            set
            {
                _VFrame = value;
                OnPropertyChanged("VFrame");
            }
            get => _VFrame;
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
        [JsonProperty] public string InputAudio
        {
            set
            {
                _InputAudio = value;
                OnPropertyChanged("InputAudio");
            }
            get => _InputAudio;
        }
        [JsonProperty] public string InputSubtitle
        {
            set
            {
                _InputSubtitle = value;
                OnPropertyChanged("InputSubtitle");
            }
            get => _InputSubtitle;
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
        [JsonProperty] public HWAccelType HWAccelType
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
        [JsonProperty] public int HLSInitTime
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
        [JsonProperty] public string InputFramerate
        {
            set
            {
                _InputFramerate = value;
                OnPropertyChanged("InputFramerate");
            }
            get => _InputFramerate;
        }
        [JsonProperty] public string OutputFramerate
        {
            set
            {
                _OutputFramerate = value;
                OnPropertyChanged("OutputFramerate");
            }
            get => _OutputFramerate;
        }
        [JsonProperty] public int StartTime
        {
            set
            {
                _StartTime = value;
                OnPropertyChanged("StartTime");
            }
            get => _StartTime;
        }
        [JsonProperty] public int FKeyframe
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
        [JsonProperty] public bool SeperateAudio
        {
            set
            {
                _SeperateAudio = value;
                OnPropertyChanged("SeperateAudio");
            }
            get => _SeperateAudio;
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
        #endregion

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
                InputFramerate = load.InputFramerate;
                StartNumber = load.StartNumber;
                VFrame = load.VFrame;
                Input = load.Input;
                InputAudio = load.InputAudio;
                InputSubtitle = load.InputSubtitle;
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
                SeperateAudio = load.SeperateAudio;
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
                List<string> buffer = new List<string>();
                int SettingCount = Settings.Count;
                Prefix(args);
                FileInput(args);
                MapLayout(args, SettingCount);
                ProfileSetup(args, SettingCount);
                HLSMapLayout(args, buffer, SettingCount);
                HLSConfig(args, buffer);
                return string.Join(' ', args);
            }
        }

        void Prefix(List<string> args)
        {
            args.Add("-loglevel");
            args.Add("repeat+level+verbose");
            args.Add("-report");
            args.Add("-hide_banner");
            if (HWAccelType != HWAccelType.None)
            {
                args.Add("-hwaccel");
                args.Add(HWAccelTypeDict[HWAccelType]);
            }
            if (Sync) args.Add("-re");
            args.Add("-stream_loop");
            args.Add($"{StreamingLoop}");
            if (Wall_Clock)
            {
                args.Add("-use_wallclock_as_timestamps");
                args.Add("1");
            }
        }
        void FileInput(List<string> args)
        {
            if (!string.IsNullOrEmpty(InputFramerate))
            {
                args.Add("-framerate");
                args.Add($"{InputFramerate}");
            }
            if (Input.ToLower().Contains("png") || Input.ToLower().Contains("jpg"))
            {
                args.Add("-start_number");
                args.Add($"{StartNumber}");
            }
            args.Add("-i");
            args.Add($"\"{Input}\"");
            if (!string.IsNullOrEmpty(InputAudio))
            {
                args.Add("-i");
                args.Add($"\"{InputAudio}\"");
            }
            if (!string.IsNullOrEmpty(InputSubtitle))
            {
                args.Add("-i");
                args.Add($"\"{InputSubtitle}\"");
            }
            if (!string.IsNullOrEmpty(OutputFramerate))
            {
                args.Add("-framerate");
                args.Add($"{OutputFramerate}");
            }
            if (VFrame != 0)
            {
                args.Add("-vframes");
                args.Add($"{VFrame}");
            }
        }
        void MapLayout(List<string> args, int SettingCount)
        {
            if (FKeyframe > 0)
            {
                args.Add("-force_key_frames");
                args.Add($"\"expr: gte(t, n_forced * {FKeyframe})\"");
            }
            for (int i = 0; i < SettingCount; i++)
            {
                args.Add("-map");
                args.Add("0:0");
                if (!string.IsNullOrEmpty(InputAudio))
                {
                    args.Add("-map");
                    args.Add("1:0");
                }
                else if (HaveAudio)
                {
                    args.Add("-map");
                    args.Add("0:1");
                }
            }
            if (!string.IsNullOrEmpty(InputSubtitle))
            {
                args.Add("-map");
                args.Add("2:0");
            }
        }
        void ProfileSetup(List<string> args, int SettingCount)
        {
            for (int i = 0; i < SettingCount; i++)
            {
                ABRSetting target = Settings[i];
                if (target.Width != 0 && target.Height != 0)
                {
                    args.Add($"-s:v:{i}");
                    args.Add($"{target.Width}x{target.Height}");
                }
                args.Add($"-c:v:{i}");
                args.Add(string.IsNullOrEmpty(target.VideoCodec) ? "copy" : target.VideoCodec);
                if (target.CRF != 0)
                {
                    args.Add($"-crf:{i}");
                    args.Add($"{target.CRF}");
                }
                if (!string.IsNullOrEmpty(target.Preset))
                {
                    args.Add($"-preset:{i}");
                    args.Add($"{target.Preset}");
                }
                if (target.MaxRate != 0)
                {
                    if (target.VideoCodec.Contains("nvenc"))
                    {
                        args.Add($"-rc");
                        args.Add($"cbr");
                        args.Add($"-cbr");
                        args.Add($"1");
                    }
                    bool use_xx_params = target.VideoCodec == "libx265" || target.VideoCodec == "libx264";
                    if (target.VideoCodec == "libx265")
                    {
                        args.Add($"-x265-params");
                    }
                    else if (target.VideoCodec == "libx264")
                    {
                        args.Add($"-x264-params");
                    }
                    else
                    {
                        args.Add($"-maxrate:{i}k");
                        args.Add($"{target.MaxRate}");
                        args.Add($"-bufsize:{i}");
                        args.Add($"{target.MaxRate * 2}k");
                        args.Add($"-b:v:{i}");
                        args.Add($"{target.MaxRate}k");
                    }

                    if (use_xx_params)
                    {
                        string a = $"vbv-maxrate={target.MaxRate}:vbv-bufsize={target.MaxRate * 2}:bitrate={target.MaxRate}";
                        if (target.NOG != 0) a = $"no-open-gop:${target.NOG}:" + a;
                        if (target.Keyint != 0) a = $"keyint:${target.Keyint}:" + a;
                        args.Add($"\"{a}\"");
                    }

                    if (target.VideoCodec == "hevc_nvenc")
                    {

                    }
                }
                if (!string.IsNullOrEmpty(InputAudio) || HaveAudio)
                {
                    args.Add($"-c:a:{SettingCount}");
                    args.Add("copy");
                }
            }
            if (!string.IsNullOrEmpty(InputSubtitle))
            {
                args.Add($"-c:s:{SettingCount + 1}");
                args.Add("ttml");
            }
        }
        void HLSMapLayout(List<string> args, List<string> buffer, int SettingCount)
        {
            args.Add("-var_stream_map");
            for (int i = 0; i < SettingCount; i++)
            {
                if (!string.IsNullOrEmpty(InputAudio) || HaveAudio)
                {
                    buffer.Add($"v:{i},a:{i}");
                }
                else
                {
                    buffer.Add($"v:{i}");
                }
            }
            if (!string.IsNullOrEmpty(InputSubtitle))
            {
                //buffer.Add("s:0");
            }
            args.Add($"\"{string.Join(' ', buffer)}\"");
            buffer.Clear();
            args.Add("-master_pl_name");
            args.Add(MasterName);
        }
        void HLSConfig(List<string> args, List<string> buffer)
        {
            args.Add("-f");
            args.Add("hls");
            if (StartTime > 0)
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
            if (PlaylistType != HLSPlaylistType.None)
            {
                args.Add("-hls_playlist_type");
                args.Add(HLSTypeUtility.HLSPlaylistTypeDict[PlaylistType]);
            }
            if (ListSize != 0)
            {
                args.Add("-hls_list_size");
                args.Add($"{ListSize}");
            }
            if (DeleteFlag) buffer.Add("delete_segments");
            if (AppendFlag) buffer.Add("append_list");
            if (IndenFlag) buffer.Add("independent_segments");
            if (SplitFlag) buffer.Add("split_by_time");
            if (buffer.Count > 0)
            {
                args.Add("-hls_flags");
                args.Add($"{string.Join('+', buffer)}");
            }
            buffer.Clear();
            args.Add("-max_muxing_queue_size");
            args.Add($"{MuxingQueue}");
            args.Add("-hls_segment_filename");
            args.Add($"\"{OutputSegmentFileName}\"");
            args.Add($"\"{OutputM3U8FileName}\"");
        }
    }
}