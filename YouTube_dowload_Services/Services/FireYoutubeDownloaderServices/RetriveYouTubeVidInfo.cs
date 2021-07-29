using System;
using System.Threading.Tasks;
using YouTubeDownloadProject.Model;
using YoutubeExplode;

namespace YouTubeDownloadProject.Services
{
    public class RetriveYouTubeVidInfo
    {
        public static async Task<VidInfoModell> getYoutubeVidAsync(String youtubelink)
        {
            VidInfoModell returninfo = new VidInfoModell();
            returninfo.VidLink = youtubelink;

            try
            {
                // Loadup default values
                var youtube = new YoutubeClient();
                var video   = await youtube.Videos.GetAsync(returninfo.VidLink);

                // Creating model of info
                returninfo.Author       = video.Author.Title;
                returninfo.Description  = video.Description;
                returninfo.Duration     = video.Duration.Value;
                returninfo.VidTitle     = video.Title;
                returninfo.id           = video.Id;
                returninfo.thumb        = video.Thumbnails[0];
            }
            catch
            {
                returninfo.VidTitle = "No Title Found.";
            }

            return returninfo;
        }
    }
}
