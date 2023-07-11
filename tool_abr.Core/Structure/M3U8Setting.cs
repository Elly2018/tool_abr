using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Funique
{
    [System.Serializable]
    public sealed partial class M3U8Setting : INotifyPropertyChanged
    {
        [JsonIgnore] public Dictionary<HLSType, string> HLSTypeDict => HLSTypeUtility.HLSTypeDict;
        [JsonIgnore] public Dictionary<HWAccelType, string> HWAccelTypeDict => HLSTypeUtility.HWAccelTypeDict;
        [JsonIgnore] public Dictionary<HLSPlaylistType, string> HLSPlaylistTypeDict => HLSTypeUtility.HLSPlaylistTypeDict;
        [JsonIgnore] public Dictionary<AudioSourceType, string> AudioSourceTypeDict => HLSTypeUtility.AudioSourceTypeDict;

        #region Fields
        bool _Sync;
        string _Lut;
        bool _Overwrite;
        bool _UseConCat;
        int _StreamingLoop;
        bool _Wall_Clock;
        bool _Cache;
        int _StartNumber;
        int _VFrame;
        string _ASync;
        string _VSync;
        string _Input;
        string _InputAudio;
        string _InputSubtitle;
        string _InputFramerate;
        string _AudioOffset;
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
        bool _OutputSingleFile;
        bool _OutputAudio;
        bool _ProcessingSingleFileFirst;
        bool _SeperateAudio;
        int _AudioSource;
        string _AudioCodec;
        string _SubtitleCodec;
        string _OutputM3U8FileName;
        string _OutputAudioM3U8FileName;
        string _OutputSubtitleM3U8FileName;
        string _OutputSegmentFileName;
        string _OutputAudioSegmentFileName;
        string _OutputSubtitleSegmentFileName;
        ObservableCollection<ABRSetting> _Settings = new ObservableCollection<ABRSetting>();
        ObservableCollection<JobExecute> _Jobs = new ObservableCollection<JobExecute>();
        ObservableCollection<MultipleInput> _Inputs = new ObservableCollection<MultipleInput>();
        #endregion

        #region Properties
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
        public string Lut
        {
            set
            {
                _Lut = value;
                OnPropertyChanged("Lut");
            }
            get => _Lut;
        }
        [JsonProperty]
        public bool Overwrite
        {
            set
            {
                _Overwrite = value;
                OnPropertyChanged("Overwrite");
            }
            get => _Overwrite;
        }
        [JsonProperty]
        public bool UseConCat
        {
            set
            {
                _UseConCat = value;
                OnPropertyChanged("UseConCat");
                OnPropertyChanged("MultipleInputEnable");
            }
            get => _UseConCat;
        }
        [JsonProperty]
        public int StreamingLoop
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
        [JsonProperty]
        public bool HaveAudio
        {
            set
            {
                _HaveAudio = value;
                OnPropertyChanged("HaveAudio");
                OnPropertyChanged("AudioInputEnable");
            }
            get => _HaveAudio;
        }
        [JsonProperty]
        public int StartNumber
        {
            set
            {
                _StartNumber = value;
                OnPropertyChanged("StartNumber");
            }
            get => _StartNumber;
        }
        [JsonProperty]
        public int VFrame
        {
            set
            {
                _VFrame = value;
                OnPropertyChanged("VFrame");
            }
            get => _VFrame;
        }
        [JsonProperty]
        public string ASync
        {
            set
            {
                _ASync = value;
                OnPropertyChanged("ASync");
            }
            get => _ASync;
        }
        [JsonProperty]
        public string VSync
        {
            set
            {
                _VSync = value;
                OnPropertyChanged("VSync");
            }
            get => _VSync;
        }
        [JsonProperty]
        public string Input
        {
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
            get => _Input;
        }
        [JsonProperty]
        public string InputAudio
        {
            set
            {
                _InputAudio = value;
                OnPropertyChanged("InputAudio");
            }
            get => _InputAudio;
        }
        [JsonProperty]
        public string InputSubtitle
        {
            set
            {
                _InputSubtitle = value;
                OnPropertyChanged("InputSubtitle");
            }
            get => _InputSubtitle;
        }
        [JsonProperty]
        public string MasterName
        {
            set
            {
                _MasterName = value;
                OnPropertyChanged("MasterName");
            }
            get => _MasterName;
        }
        [JsonProperty]
        public HLSType Type
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
        [JsonProperty]
        public int ListSize
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
        [JsonProperty]
        public int HLSTime
        {
            set
            {
                _HLSTime = value;
                OnPropertyChanged("HLSTime");
            }
            get => _HLSTime;
        }
        [JsonProperty]
        public string InputFramerate
        {
            set
            {
                _InputFramerate = value;
                OnPropertyChanged("InputFramerate");
            }
            get => _InputFramerate;
        }
        [JsonProperty]
        public string AudioOffset
        {
            set
            {
                _AudioOffset = value;
                OnPropertyChanged("AudioOffset");
            }
            get => _AudioOffset;
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
        [JsonProperty]
        public HLSPlaylistType PlaylistType
        {
            set
            {
                _PlaylistType = (int)value;
                OnPropertyChanged("PlaylistType");
            }
            get => (HLSPlaylistType)_PlaylistType;
        }
        [JsonProperty]
        public bool DeleteFlag
        {
            set
            {
                _DeleteFlag = value;
                OnPropertyChanged("DeleteFlag");
            }
            get => _DeleteFlag;
        }
        [JsonProperty]
        public bool AppendFlag
        {
            set
            {
                _AppendFlag = value;
                OnPropertyChanged("AppendFlag");
            }
            get => _AppendFlag;
        }
        [JsonProperty]
        public bool SeperateAudio
        {
            set
            {
                _SeperateAudio = value;
                OnPropertyChanged("SeperateAudio");
            }
            get => _SeperateAudio;
        }
        [JsonProperty]
        public AudioSourceType AudioSource
        {
            set
            {
                _AudioSource = (int)value;
                OnPropertyChanged("AudioSource");
            }
            get => (AudioSourceType)_AudioSource;
        }
        [JsonProperty]
        public string AudioCodec
        {
            set
            {
                _AudioCodec = value;
                OnPropertyChanged("AudioCodec");
            }
            get => _AudioCodec;
        }
        [JsonProperty]
        public string SubtitleCodec
        {
            set
            {
                _SubtitleCodec = value;
                OnPropertyChanged("SubtitleCodec");
            }
            get => _SubtitleCodec;
        }
        [JsonProperty]
        public bool IndenFlag
        {
            set
            {
                _IndenFlag = value;
                OnPropertyChanged("IndenFlag");
            }
            get => _IndenFlag;
        }
        [JsonProperty]
        public bool SplitFlag
        {
            set
            {
                _SplitFlag = value;
                OnPropertyChanged("SplitFlag");
            }
            get => _SplitFlag;
        }
        [JsonProperty]
        public int MuxingQueue
        {
            set
            {
                _MuxingQueue = value;
                OnPropertyChanged("MuxingQueue");
            }
            get => _MuxingQueue;
        }
        [JsonProperty]
        public bool OutputSingleFile
        {
            set
            {
                _OutputSingleFile = value;
                OnPropertyChanged("OutputSingleFile");
            }
            get => _OutputSingleFile;
        }
        [JsonProperty]
        public bool OutputAudio
        {
            set
            {
                _OutputAudio = value;
                OnPropertyChanged("OutputAudio");
            }
            get => _OutputAudio;
        }
        [JsonProperty]
        public bool ProcessingSingleFileFirst
        {
            set
            {
                _ProcessingSingleFileFirst = value;
                OnPropertyChanged("ProcessingSingleFileFirst");
            }
            get => _ProcessingSingleFileFirst;
        }
        [JsonProperty]
        public string OutputM3U8FileName
        {
            set
            {
                _OutputM3U8FileName = value;
                OnPropertyChanged("OutputM3U8FileName");
            }
            get => _OutputM3U8FileName;
        }
        [JsonProperty]
        public string OutputAudioM3U8FileName
        {
            set
            {
                _OutputAudioM3U8FileName = value;
                OnPropertyChanged("OutputAudioM3U8FileName");
            }
            get => _OutputAudioM3U8FileName;
        }
        [JsonProperty]
        public string OutputSubtitleM3U8FileName
        {
            set
            {
                _OutputSubtitleM3U8FileName = value;
                OnPropertyChanged("OutputSubtitleM3U8FileName");
            }
            get => _OutputSubtitleM3U8FileName;
        }
        [JsonProperty]
        public string OutputSegmentFileName
        {
            set
            {
                _OutputSegmentFileName = value;
                OnPropertyChanged("OutputSegmentFileName");
            }
            get => _OutputSegmentFileName;
        }
        [JsonProperty]
        public string OutputAudioSegmentFileName
        {
            set
            {
                _OutputAudioSegmentFileName = value;
                OnPropertyChanged("OutputAudioSegmentFileName");
            }
            get => _OutputAudioSegmentFileName;
        }
        [JsonProperty]
        public string OutputSubtitleSegmentFileName
        {
            set
            {
                _OutputSubtitleSegmentFileName = value;
                OnPropertyChanged("OutputSubtitleSegmentFileName");
            }
            get => _OutputSubtitleSegmentFileName;
        }
        [JsonProperty]
        public ObservableCollection<ABRSetting> Settings
        {
            set
            {
                _Settings = value;
                OnPropertyChanged("Settings");
            }
            get => _Settings;
        }
        [JsonProperty]
        public ObservableCollection<MultipleInput> Inputs
        {
            set
            {
                _Inputs = value;
                OnPropertyChanged("Inputs");
            }
            get => _Inputs;
        }
        #endregion

        #region Local Properties
        [JsonIgnore]
        public ObservableCollection<JobExecute> Jobs
        {
            set
            {
                _Jobs = value;
                OnPropertyChanged("Jobs");
            }
            get => _Jobs;
        }
        [JsonIgnore]
        public string WorkDir { set; get; }
        [JsonIgnore]
        public bool MultipleInputEnable => !UseConCat;
        [JsonIgnore]
        public bool AudioInputEnable => !HaveAudio;
        #endregion

        #region Tooltip
        public string MasterNameTooltip
        {
            get
            {
                return "Generate master M3U8 file name, This will generate in the work directory";
            }
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
                Lut = load.Lut;
                Overwrite = load.Overwrite;
                Cache = load.Cache;
                InputFramerate = load.InputFramerate;
                StartNumber = load.StartNumber;
                VFrame = load.VFrame;
                Input = load.Input;
                InputAudio = load.InputAudio;
                InputSubtitle = load.InputSubtitle;
                AudioOffset = load.AudioOffset;
                ASync = load.ASync;
                VSync = load.VSync;
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
                OutputSingleFile = load.OutputSingleFile;
                OutputAudio = load.OutputAudio;
                ProcessingSingleFileFirst = load.ProcessingSingleFileFirst;
                SeperateAudio = load.SeperateAudio;
                AudioSource = load.AudioSource;
                PlaylistType = load.PlaylistType;
                AudioCodec = load.AudioCodec;
                SubtitleCodec = load.SubtitleCodec;
                OutputM3U8FileName = load.OutputM3U8FileName;
                OutputAudioM3U8FileName = load.OutputAudioM3U8FileName;
                OutputSubtitleM3U8FileName = load.OutputSubtitleM3U8FileName;
                OutputSegmentFileName = load.OutputSegmentFileName;
                OutputAudioSegmentFileName = load.OutputAudioSegmentFileName;
                OutputSubtitleSegmentFileName = load.OutputSubtitleSegmentFileName;
                Settings = load.Settings;
                Inputs = load.Inputs;
                UseConCat = load.UseConCat;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        [JsonIgnore] public string[] Arguments
        {
            get
            {
                return Processes.Select(x => x.Argument).ToArray();
            }
        }

        [JsonIgnore] public JobExecute[] Processes
        {
            get
            {
                List<JobExecute> result = new List<JobExecute>();
                List<string> args = new List<string>();
                List<string> buffer = new List<string>();
                int SettingCount = Settings.Count;

                // First pass: Main HLS stream
                if (NeedPass(1))
                {
                    P1_Prefix(args);
                    P1_FileInput(args);
                    P1_MapLayout(args, SettingCount);
                    P1_HLSMapLayout(args, buffer, SettingCount);
                    P1_HLSConfig(args, buffer, WorkDir);
                    result.Add(new JobExecute("Main ABR HLS", "", string.Join(' ', args)));
                    args.Clear();
                }

                // Second pass: Audio seperation
                if (NeedPass(2))
                {
                    P2_Prefix(args);
                    P2_FileInput(args);
                    P2_HLSConfig(args);
                    JobExecute b = new JobExecute("Main Audio HLS", "", string.Join(' ', args));
                    b.BeginProcess = P2_JobBegin;
                    result.Add(b);
                    args.Clear();
                }

                // Third pass: Subtitle seperation
                if (NeedPass(3))
                {
                    P3_Prefix(args);
                    P3_FileInput(args);
                    P3_HLSConfig(args);
                    JobExecute b = new JobExecute("Main Subtitle HLS", "", string.Join(' ', args));
                    b.BeginProcess = P3_JobBegin;
                    result.Add(b);
                    args.Clear();
                }

                // Four pass: Single file output
                if (NeedPass(4))
                {
                    P4_Prefix(args);
                    P4_FileInput(args);
                    P4_MapLayout(args, SettingCount);
                    if (ProcessingSingleFileFirst) result.Insert(0, new JobExecute("Single File", "", string.Join(' ', args)));
                    else result.Add(new JobExecute("Single File", "", string.Join(' ', args)));
                    args.Clear();
                }

                // Ten pass: Analysis master m3u8 and create it
                if (NeedPass(10))
                {
                    JobExecute job_buffer = new JobExecute()
                    {
                        Name = "Master Generate"
                    };
                    for(int i = 0; i < Settings.Count; i++)
                    {
                        string b = Path.Combine(WorkDir, OutputM3U8FileName.Replace("/", "\\"));
                        b = b.Replace("%v", i.ToString());
                        Directory.CreateDirectory(Path.Combine(WorkDir, "temp"));
                        P10_Main(args, b, Path.Combine(WorkDir, "temp", $"Video{i}.json"));
                        job_buffer.Arguments.Add(string.Join(' ', args));
                        args.Clear();
                    }
                    job_buffer.DoneProcess = P10_GenerateMasterM3U8;
                    job_buffer.Type = JobType.FFPROBE;
                    result.Add(job_buffer);
                    args.Clear();
                }
                return result.ToArray();
            }
        }

        bool NeedPass(int pass)
        {
            if (pass == 1) return true;
            else if (pass == 2)
            {
                if (SeperateAudio)
                {
                    if (AudioSource == AudioSourceType.None ||
                        (AudioSource == AudioSourceType.FromFile && string.IsNullOrEmpty(InputAudio)))
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            else if (!string.IsNullOrEmpty(InputSubtitle) && pass == 3) return true;
            else if (OutputSingleFile && pass == 4) return true;
            else if (pass == 10) return true;
            return false;
        }
        void Commom_Prefix(List<string> args)
        {
            args.Add(Overwrite ? "-y" : "-n");
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
            if (Wall_Clock)
            {
                args.Add("-use_wallclock_as_timestamps");
                args.Add("1");
            }
        }
        void SegmentSetup(ABRSetting target, List<string> args, int i, int SettingCount, bool suffix)
        {
            if (target.Width != 0 && target.Height != 0)
            {
                if(suffix) args.Add($"-s:v:{i}");
                else args.Add($"-s:v");

                args.Add($"{target.Width}x{target.Height}");
            }

            if (suffix) args.Add($"-c:v:{i}");
            else args.Add($"-c:v");

            args.Add(string.IsNullOrEmpty(target.VideoCodec) ? "copy" : target.VideoCodec);
            if (target.CRF != 0)
            {
                if (suffix) args.Add($"-crf:{i}");
                else args.Add($"-crf");
                args.Add($"{target.CRF}");
            }
            if (!string.IsNullOrEmpty(target.Preset))
            {
                if (suffix) args.Add($"-preset:{i}");
                else args.Add($"-preset");
                args.Add($"{target.Preset}");
            }
            if (!string.IsNullOrEmpty(target.PixFmt))
            {
                if (suffix) args.Add($"-pix_fmt:{i}");
                else args.Add($"-pix_fmt");
                args.Add($"{target.PixFmt}");
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
                    if (suffix) args.Add($"-maxrate:{i}");
                    else args.Add($"-maxrate");
                    args.Add($"{target.MaxRate}k");
                    if (suffix) args.Add($"-bufsize:{i}");
                    else args.Add($"-bufsize");
                    args.Add($"{target.MaxRate * 2}k");
                    if (suffix) args.Add($"-b:v:{i}");
                    else args.Add($"-b:v");
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
            if (!string.IsNullOrEmpty(target.FPS))
            {
                if (suffix) args.Add($"-r:{i}");
                else args.Add($"-r");
                args.Add(target.FPS);
            }
        }
        string ConCatMap()
        {
            string vi = string.Empty;
            for (int i = 0; i < Inputs.Count; i++)
            {
                vi += $"[{i}:v]";
            }
            vi += $"concat=n={Inputs.Count}:v=1:a=0[v]";
            return vi;
        }
        string GlobalLut()
        {
            return $"[v]lut3d={Lut}";
        }
    }
}