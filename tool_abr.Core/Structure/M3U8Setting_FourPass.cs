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
                for (int i = 0; i < Inputs.Count; i++)
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
            }
            if (OutputAudio)
            {
                if ((Input.ToLower().Contains("png") || Input.ToLower().Contains("jpg")) && !string.IsNullOrEmpty(InputAudio))
                {
                    if (!string.IsNullOrEmpty(AudioOffset))
                    {
                        args.Add("-itsoffset");
                        args.Add($"{AudioOffset}");
                    }
                    args.Add("-i");
                    args.Add($"\"{InputAudio}\"");
                }
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
            if (UseConCat || !string.IsNullOrEmpty(Lut))
            {
                args.Add("-filter_complex");
                List<string> vi = new List<string>();
                if (UseConCat)
                    vi.Add(ConCatMap());
                if (!string.IsNullOrEmpty(Lut))
                    vi.Add(GlobalLut());
                args.Add($"\"{string.Join(';', vi)}\"");
            }

            for (int i = 0; i < SettingCount; i++)
            {
                args.Add("-map");
                if (UseConCat) args.Add("\"v\"");
                else args.Add("0:0");

                if (OutputAudio)
                {
                    if (!string.IsNullOrEmpty(InputAudio))
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
                }

                ABRSetting target = Settings[i];
                SegmentSetup(target, args, i, SettingCount, false);
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
