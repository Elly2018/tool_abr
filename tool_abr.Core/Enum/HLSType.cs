using System.Collections.Generic;

namespace Funique
{
    public enum TranscodeType
    {
        HLS,
        DASH
    }

    public enum HLSType
    {
        MPEG,
        FMP4
    }

    public enum HLSPlaylistType
    {
        None,
        EVENT,
        VOD
    }

    public static class HLSTypeUtility
    {
        public static Dictionary<TranscodeType, string> TranscodeTypeDict = new Dictionary<Funique.TranscodeType, string>()
        {
            { TranscodeType.HLS, "hls" },
            { TranscodeType.DASH, "dash" },
        };

        public static Dictionary<HLSType, string> HLSTypeDict = new Dictionary<Funique.HLSType, string>()
        {
            { HLSType.MPEG, "mpegts" },
            { HLSType.FMP4, "fmp4" },
        };

        public static Dictionary<HLSPlaylistType, string> HLSPlaylistTypeDict = new Dictionary<Funique.HLSPlaylistType, string>()
        {
            { HLSPlaylistType.None, "none" },
            { HLSPlaylistType.EVENT, "event" },
            { HLSPlaylistType.VOD, "vod" },
        };
    }
}
