using System;
using System.Collections.Generic;
using System.Text;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P3_Prefix(List<string> args)
        {
            Commom_Prefix(args);
        }
        void P3_FileInput(List<string> args)
        {
            args.Add("-i");
            args.Add($"\"{InputSubtitle}\"");
            if (!string.IsNullOrEmpty(SubtitleCodec))
            {
                args.Add("-c:s");
                args.Add(SubtitleCodec);
            }
        }
        void P3_HLSConfig(List<string> args)
        {
            args.Add("-f");
            args.Add("segment");
            args.Add("-segment_time");
            args.Add($"{HLSTime}");
            args.Add("-segment_list_size");
            args.Add("0");
            args.Add("-segment_list");
            args.Add($"{OutputSubtitleM3U8FileName}");
            args.Add($"{OutputSubtitleSegmentFileName}");
        }
    }
}
