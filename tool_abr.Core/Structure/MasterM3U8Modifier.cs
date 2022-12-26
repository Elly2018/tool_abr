using System.Collections.Generic;

namespace Funique
{
    public struct MasterM3U8Modifier
    {
        struct VideoPlaylist
        {
            public string line;
            public string uri;

            public VideoPlaylist(string line, string uri)
            {
                this.line = line;
                this.uri = uri;
            }
        }

        List<VideoPlaylist> playlists;

        public MasterM3U8Modifier(string text)
        {
            playlists = new List<VideoPlaylist>();

            string[] data = text.Split('\n');
            bool skip = false;
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
            }
        }
    }
}
