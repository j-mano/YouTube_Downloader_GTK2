using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeDownloadProject.Model;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloadProject.Services
{
    public class DownloadYouTubeVid
    {
        // https://github.com/Tyrrrz/YoutubeExplode

        public static async Task DownloadYouTubeVidFunction(VidInfoModell VidToDownload)
        {
            var youtube = new YoutubeClient();

            try
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            }
            catch
            {
                
            }
            /*
             * Note that while it may be tempting to just always use muxed streams, given that they contain both audio and video,
             * it's important to note that they are limited in quality and don't go beyond 720p30.
             * If you want to download the video in maximum quality, you need to download the audio-only and video-only streams separately and then mux them together on your own using tools like FFmpeg.
             * You can also use YoutubeExplode.Converter which wraps FFmpeg and provides an extension point for YoutubeExplode to download videos directly.
             * https://github.com/Tyrrrz/YoutubeExplode.Converter
             */
        }

        public static async Task DownloadYouTubeVidFunctionVideoOnly(VidInfoModell VidToDownload)
        {
            var youtube = new YoutubeClient();

            try
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest
                .GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GetWithHighestVideoQuality();

                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            }
            catch
            {

            }
        }

        public static async Task DownloadYouTubeVidFunctionAudioOnly(VidInfoModell VidToDownload)
        {
            var youtube = new YoutubeClient();

            try
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(VidToDownload.id);

                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                // Download the stream to a file. Local aplication folder.
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            }
            catch
            {

            }
        }
    }
}
