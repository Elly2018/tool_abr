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

    public enum HWAccelType
    {
        None,
        CUDA,
        CUVID,
        DRM,
        DXVA2,
        QSV,
        D3D11VA
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

        public static Dictionary<HWAccelType, string> HWAccelTypeDict = new Dictionary<Funique.HWAccelType, string>()
        {
            { HWAccelType.None, "none" },
            { HWAccelType.CUDA, "cuda" },
            { HWAccelType.CUVID, "cuvid" },
            { HWAccelType.DRM, "drm" },
            { HWAccelType.DXVA2, "dxva2" },
            { HWAccelType.QSV, "qsv" },
            { HWAccelType.D3D11VA, "d3d11va" },
        };

        public static Dictionary<HLSPlaylistType, string> HLSPlaylistTypeDict = new Dictionary<Funique.HLSPlaylistType, string>()
        {
            { HLSPlaylistType.None, "none" },
            { HLSPlaylistType.EVENT, "event" },
            { HLSPlaylistType.VOD, "vod" },
        };
    }
}
