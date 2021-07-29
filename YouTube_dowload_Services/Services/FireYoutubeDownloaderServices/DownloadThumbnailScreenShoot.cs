using System;
using System.Net;
using YouTube_dowload_Services.Services.Autofaqinterfaces;
using YouTubeDownloadProject.Model;

namespace YouTube_dowload_Services.Services.FireYoutubeDownloaderServices
{
    public class DownloadThumbnailScreenShoot : IDownloadThumbnailScreenShootInterface
    {
        public void DownloadThumbnailScreenShoote(VidInfoModell SelectedVidInfo)
        {
            // Downloading image as webp format.
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(SelectedVidInfo.thumb.Url), @"ThumbNail.webp");
            }

            // webp to png formating.
            using (var image = Aspose.Imaging.Image.Load(@"ThumbNail.webp"))
            {
                var options = new Aspose.Imaging.ImageOptions.PngOptions();
                image.Save(@"ThumbNail.png", options);
            }
        }
    }
}
