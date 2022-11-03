using System.Collections.Generic;

namespace Funique
{
    public enum HLSType
    {
        MPEG,
        FMP4
    }

    public static class HLSTypeUtility
    {
        public static Dictionary<HLSType, string> HLSTypeDict = new Dictionary<Funique.HLSType, string>()
        {
            { HLSType.MPEG, "mpegts" },
            { HLSType.FMP4, "fmp4" },
        };
    }
}
