using System.Collections.Generic;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P1_Prefix(List<string> args)
        {
            Commom_Prefix(args);
            args.Add("-stream_loop");
            args.Add($"{StreamingLoop}");
        }
        void P1_FileInput(List<string> args)
        {
            if (!string.IsNullOrEmpty(VSync))
            {
                args.Add("-vsync");
                args.Add(VSync);
            }
            if (!string.IsNullOrEmpty(ASync))
            {
                args.Add("-async");
                args.Add(ASync);
            }
            if (UseConCat)
            {
                for(int i = 0; i < Inputs.Count; i++)
                {
                    MultipleInput buffer = Inputs[i];
                    if (!string.IsNullOrEmpty(buffer.Framerate))
                    {
                        args.Add("-framerate");
                        args.Add(buffer.Framerate);
                    }
                    if (Input.ToLower().Contains("png") || Input.ToLower().Contains("jpg"))
                    {
                        args.Add("-start_number");
                        args.Add($"{buffer.StartNumber}");
                    }
                    args.Add("-i");
                    args.Add($"\"{buffer.Input}\"");
                }
            }
            else
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
                if (VFrame != 0)
                {
                    args.Add("-vframes");
                    args.Add($"{VFrame}");
                }
            }
            if (!SeperateAudio && !string.IsNullOrEmpty(InputAudio))
            {
                if (!string.IsNullOrEmpty(AudioOffset))
                {
                    args.Add("-itsoffset");
                    args.Add($"{AudioOffset}");
                }
                args.Add("-i");
                args.Add($"\"{InputAudio}\"");
            }
            args.Add("-muxdelay");
            args.Add("0");
        }
        void P1_MapLayout(List<string> args, int SettingCount)
        {
            if (FKeyframe > 0)
            {
                args.Add("-force_key_frames");
                args.Add($"\"expr: gte(t, n_forced * {FKeyframe})\"");
            }
            if (UseConCat || !string.IsNullOrEmpty(Lut))
            {
                List<string> vi = new List<string>();
                if (UseConCat)
                    vi.Add(ConCatMap());
                if (!string.IsNullOrEmpty(Lut))
                    vi.Add(GlobalLut());

                for (int i = 0; i < SettingCount; i++)
                {
                    args.Add("-filter_complex");
                    args.Add($"\"{string.Join(';', vi)}\"");
                }
            }
            for (int i = 0; i < SettingCount; i++)
            {
                args.Add("-map");
                if (UseConCat) args.Add("\"v\"");
                else args.Add("0:0");

                if (!SeperateAudio && !string.IsNullOrEmpty(InputAudio))
                {
                    args.Add("-map");
                    if (UseConCat) args.Add($"{Inputs.Count}:0");
                    else args.Add("1:0");
                }
                else if (HaveAudio)
                {
                    args.Add("-map");
                    args.Add("0:1");
                }

                ABRSetting target = Settings[i];
                SegmentSetup(target, args, i, SettingCount, true);
                if ((!SeperateAudio && !string.IsNullOrEmpty(InputAudio)) || HaveAudio)
                {
                    args.Add($"-c:a:{SettingCount}");
                    args.Add(string.IsNullOrEmpty(target.AudioCodec) ? "copy" : target.AudioCodec);
                }
            }
        }
        void P1_HLSMapLayout(List<string> args, List<string> buffer, int SettingCount)
        {
            args.Add("-var_stream_map");
            for (int i = 0; i < SettingCount; i++)
            {
                if ((!SeperateAudio && !string.IsNullOrEmpty(InputAudio)) || HaveAudio)
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
        void P1_HLSConfig(List<string> args, List<string> buffer, string workdir)
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
