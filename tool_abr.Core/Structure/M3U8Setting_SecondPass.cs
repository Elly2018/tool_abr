using System.Collections.Generic;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P2_Prefix(List<string> args)
        {
            Commom_Prefix(args);
        }
        void P2_FileInput(List<string> args)
        {
            args.Add("-i");
            args.Add($"\"{InputAudio}\"");
            if (!string.IsNullOrEmpty(AudioCodec))
            {
                args.Add("-c:a");
                args.Add(AudioCodec);
            }
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
            args.Add($"{OutputAudioM3U8FileName}");
            args.Add($"{OutputAudioSegmentFileName}");
        }
    }
}
