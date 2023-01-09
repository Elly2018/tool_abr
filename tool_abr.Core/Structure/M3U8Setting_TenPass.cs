using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Funique
{
    public sealed partial class M3U8Setting
    {
        void P10_Main(List<string> args, string file, string output)
        {
            args.Add("-report");
            args.Add("-loglevel");
            args.Add("fatal");
            args.Add("-print_format");
            args.Add("json");
            args.Add("-show_streams");
            args.Add("-i");
            args.Add($"\"{file}\"");
            args.Add("-o");
            args.Add($"\"{output}\"");
        }

        void P10_GenerateMasterM3U8()
        {
            string realstring = string.Empty;
            realstring += "#EXTM3U\n";
            realstring += "#EXT-X-VERSION:7\n";
            string audID = string.Empty;
            string subID = string.Empty;

            if (File.Exists(Path.Combine(WorkDir, OutputAudioM3U8FileName)))
            {
                audID = "aud1";
                realstring += $"#EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID=\"{audID}\",LANGUAGE=\"en\",NAME=\"English\",AUTOSELECT=YES,DEFAULT=YES,CHANNELS=\"2\",URI=\"{OutputAudioM3U8FileName}\"\n";
            }
            if (File.Exists(Path.Combine(WorkDir, OutputSubtitleM3U8FileName)))
            {
                subID = "sub1";
                realstring += $"#EXT-X-MEDIA:TYPE=SUBTITLES,GROUP-ID=\"{subID}\",LANGUAGE=\"en\",NAME=\"English\",AUTOSELECT=YES,DEFAULT=YES,FORCED=NO,URI=\"{OutputSubtitleM3U8FileName}\"\n";
            }

            realstring += "\n";

            for (int i = 0; i < Settings.Count; i++)
            {
                JObject o = JObject.Parse(File.ReadAllText(Path.Combine(WorkDir, "temp", $"Video{i}.json")));
                int bitrate = 0;
                int width = 0;
                int height = 0;
                if(o.ContainsKey("streams"))
                    foreach (JToken oi in o["streams"])
                    {
                        bitrate += oi["bit_rate"].Value<int>();
                        if(oi["width"] != null)
                        {
                            width = oi["width"].Value<int>();
                        }
                        if (oi["height"] != null)
                        {
                            height = oi["height"].Value<int>();
                        }
                    }
                Debug.WriteLine($"Stream {i}: {bitrate}");

                realstring += $"#EXT-X-STREAM-INF:BANDWIDTH={bitrate},RESOLUTION={width}x{height}";
                if (!string.IsNullOrEmpty(audID)) realstring += $",AUDIO=\"{audID}\"";
                if (!string.IsNullOrEmpty(subID)) realstring += $",SUBTITLES=\"{subID}\"";
                realstring += $"\n{OutputM3U8FileName.Replace("%v", i.ToString())}\n\n";
            }
            File.WriteAllText(Path.Combine(WorkDir, MasterName), realstring);
        }
    }
}
