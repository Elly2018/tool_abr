using System.Collections.Generic;
using System.IO;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P2_JobBegin()
        {
            Directory.CreateDirectory(Path.Combine(WorkDir, Path.GetDirectoryName(OutputAudioM3U8FileName)));
            Directory.CreateDirectory(Path.Combine(WorkDir, Path.GetDirectoryName(OutputAudioSegmentFileName)));
        }
        void P2_Prefix(List<string> args)
        {
            Commom_Prefix(args);
        }
        void P2_FileInput(List<string> args)
        {
            if (!string.IsNullOrEmpty(AudioOffset))
            {
                args.Add("-itsoffset");
                args.Add($"{AudioOffset}");
            }
            args.Add("-i");
            args.Add($"\"{InputAudio}\"");
            if (!string.IsNullOrEmpty(AudioCodec))
            {
                args.Add("-c:a");
                args.Add(AudioCodec);
            }
            if(StartNumber != 0)
            {
                args.Add("-ss");
                args.Add($"{StartNumber / float.Parse(InputFramerate)}");
            }
            if (VFrame != 0)
            {
                args.Add("-t");
                args.Add($"{VFrame / float.Parse(InputFramerate)}");
            }
            args.Add("-muxdelay");
            args.Add("0");
        }
        void P2_HLSConfig(List<string> args)
        {
            args.Add("-f");
            args.Add("segment");
            args.Add("-segment_time");
            args.Add($"{HLSTime}");
            args.Add("-segment_list_size");
            args.Add("0");
            args.Add("-segment_list");
            args.Add($"\"{OutputAudioM3U8FileName}\"");
            args.Add($"\"{OutputAudioSegmentFileName}\"");
        }
    }
}
