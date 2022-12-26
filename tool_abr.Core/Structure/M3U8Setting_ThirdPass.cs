using System.Collections.Generic;
using System.IO;

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
            args.Add("-muxdelay");
            args.Add("0");
        }
        void P3_HLSConfig(List<string> args, string workdir)
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

            Directory.CreateDirectory(Path.Combine(workdir, Path.GetDirectoryName(OutputSubtitleM3U8FileName)));
            Directory.CreateDirectory(Path.Combine(workdir, Path.GetDirectoryName(OutputSubtitleSegmentFileName)));
        }
    }
}
