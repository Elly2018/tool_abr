using System.Collections.Generic;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P4_Prefix(List<string> args)
        {
            Commom_Prefix(args);
            args.Add("-stream_loop");
            args.Add($"{StreamingLoop}");
        }
        void P4_FileInput(List<string> args)
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
            if (OutputAudio)
            {
                if ((Input.ToLower().Contains("png") || Input.ToLower().Contains("jpg")) && !string.IsNullOrEmpty(InputAudio))
                {
                    args.Add("-i");
                    args.Add($"\"{InputAudio}\"");
                }
            }
            if (!string.IsNullOrEmpty(OutputFramerate))
            {
                args.Add("-framerate");
                args.Add($"{OutputFramerate}");
            }
            args.Add("-muxdelay");
            args.Add("0");
        }
        void P4_MapLayout(List<string> args, int SettingCount)
        {
            if (FKeyframe > 0)
            {
                args.Add("-force_key_frames");
                args.Add($"\"expr: gte(t, n_forced * {FKeyframe})\"");
            }
        }
        void P4_ProfileSetup(List<string> args, int SettingCount)
        {
            for (int i = 0; i < SettingCount; i++)
            {
                ABRSetting target = Settings[i];
                if (VFrame != 0)
                {
                    args.Add("-vframes");
                    args.Add($"{VFrame}");
                }
                if (target.Width != 0 && target.Height != 0)
                {
                    args.Add($"-s:v");
                    args.Add($"{target.Width}x{target.Height}");
                }
                args.Add($"-c:v");
                args.Add(string.IsNullOrEmpty(target.VideoCodec) ? "copy" : target.VideoCodec);
                if (target.CRF != 0)
                {
                    args.Add($"-crf");
                    args.Add($"{target.CRF}");
                }
                if (!string.IsNullOrEmpty(target.Preset))
                {
                    args.Add($"-preset");
                    args.Add($"{target.Preset}");
                }
                if (!string.IsNullOrEmpty(target.PixFmt))
                {
                    args.Add($"-pix_fmt");
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
                        args.Add($"-maxrate");
                        args.Add($"{target.MaxRate}k");
                        args.Add($"-bufsize");
                        args.Add($"{target.MaxRate * 2}k");
                        args.Add($"-b:v");
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
                if (OutputAudio)
                {
                    if ((Input.ToLower().Contains("png") || Input.ToLower().Contains("jpg")) && !string.IsNullOrEmpty(InputAudio))
                    {
                        args.Add($"-c:a");
                        args.Add(string.IsNullOrEmpty(target.AudioCodec) ? "copy" : target.AudioCodec);
                    }
                }
                args.Add(target.FileName);
            }
        }
    }
}
