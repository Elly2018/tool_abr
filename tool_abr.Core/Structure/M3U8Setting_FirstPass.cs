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
            if (!SeperateAudio && !string.IsNullOrEmpty(InputAudio))
            {
                args.Add("-i");
                args.Add($"\"{InputAudio}\"");
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
        void P1_MapLayout(List<string> args, int SettingCount)
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
                if (!SeperateAudio && !string.IsNullOrEmpty(InputAudio))
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
        }
        void P1_ProfileSetup(List<string> args, int SettingCount)
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
        void P1_HLSMapLayout(List<string> args, List<string> buffer, int SettingCount)
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
        void P1_HLSConfig(List<string> args, List<string> buffer)
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
