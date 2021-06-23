using System;

namespace YouTubeDownloadProject.Model
{
    public class VidInfoModell
    {
        public string VidLink { get; set; }
        public string id { get; set; }
        public string VidTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public YoutubeExplode.Common.Thumbnail thumb { get; set; }
    }
}
