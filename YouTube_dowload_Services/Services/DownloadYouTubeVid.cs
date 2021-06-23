using System;
using System.Linq;
using System.Threading.Tasks;
using YouTubeDownloadProject.Model;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloadProject.Services
{
    public class DownloadYouTubeVid
    {
        // Api - Torturial.
        // https://github.com/Tyrrrz/YoutubeExplode

        /// <summary>
        /// Download 720/30 fps as maximum or highest resolution by uploader.
        /// </summary>
        /// <param name="VidToDownload"></param>
        /// <returns></returns>
        public static async Task DownloadYouTubeVidFunction(VidInfoModell VidToDownload)
        {
            var youtube = new YoutubeClient();

            try
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, VidToDownload.VidTitle + $" YouTubeVideo.{streamInfo.Container}");
            }
            catch
            {
                Console.WriteLine("Error while downloading");
            }
            /*
             * Note that while it may be tempting to just always use muxed streams, given that they contain both audio and video,
             * it's important to note that they are limited in quality and don't go beyond 720p30.
             * If you want to download the video in maximum quality, you need to download the audio-only and video-only streams separately and then mux them together on your own using tools like FFmpeg.
             * You can also use YoutubeExplode.Converter which wraps FFmpeg and provides an extension point for YoutubeExplode to download videos directly.
             * https://github.com/Tyrrrz/YoutubeExplode.Converter
             */
        }

        /// <summary>
        /// Download highest posible video resoultion and fps update.
        /// </summary>
        /// <param name="VidToDownload"></param>
        public static async Task HighEndDownload(VidInfoModell VidToDownload, int Resolution, int FrameRate)
        {
            var youtube = new YoutubeClient();

            try
            {
                // Get stream manifest
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                // Select streams (GetWith Highest VideoQuality / highest bitrate audio)
                var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                var videoStreamInfo = streamManifest.GetVideoStreams().GetWithHighestVideoQuality();

                if (Resolution != 0)
                {
                    string ResolutionLetter = "";

                    if (Resolution <= 1440)
                        ResolutionLetter = "p";
                    else
                        ResolutionLetter = "k";

                    videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == Resolution.ToString() + ResolutionLetter + FrameRate.ToString());
                }

                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };


                // Download and process them into one file
                await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(VidToDownload.VidTitle + ".mp4").Build());
            }
            catch
            {
                Console.WriteLine("Error while downloading");
            }
        }

        /// <summary>
        /// Download video only. Highest possible quality
        /// </summary>
        /// <param name="VidToDownload"></param>
        /// <returns></returns>
        public static async Task DownloadYouTubeVidFunctionVideoOnly(VidInfoModell VidToDownload, int Resolution, int framerate)
        {
            var youtube = new YoutubeClient();

            try
            {
                Console.WriteLine("Start Downloading");

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest
                    .GetVideoOnlyStreams()
                    .Where(s => s.Container == Container.Mp4)
                    .GetWithHighestVideoQuality();

                if (Resolution != 0)
                {
                    string ResolutionLetter = "";

                    if (Resolution <= 1440)
                        ResolutionLetter = "p";
                    else
                        ResolutionLetter = "k";


                    streamInfo = streamManifest
                    .GetVideoOnlyStreams()
                    .Where(s => s.Container == Container.Mp4)
                    .First(s => s.VideoQuality.Label == Resolution.ToString() + ResolutionLetter + framerate.ToString());
                }
                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            }
            catch
            {
                Console.WriteLine("Error while downloading");
            }
        }

        /// <summary>
        /// Download Audio only.
        /// </summary>
        /// <param name="VidToDownload"></param>
        /// <returns></returns>
        public static async Task DownloadYouTubeVidFunctionAudioOnly(VidInfoModell VidToDownload, int bitrate)
        {
            var youtube = new YoutubeClient();

            try
            {
                Console.WriteLine("Start Downloading");
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            }
            catch
            {
                Console.WriteLine("Error while downloading");
            }
        }
    }
}
