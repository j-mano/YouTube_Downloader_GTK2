using System;
using YouTube_dowload_Services.Services.Autofaqinterfaces;
using YouTube_dowload_Services.Services.FireYoutubeDownloaderServices;
using YouTubeDownloadProject.Model;

namespace Youtuube_Downloader.Autofaq
{
    public class Aolication : IAolication
    {
        IDownloadThumbnailScreenShootInterface _DownloadThumbnailScreenShootInterface;

        public Aolication(IDownloadThumbnailScreenShootInterface d)
        {
            _DownloadThumbnailScreenShootInterface = d;
        }

        public void Run()
        {

        }
    }
}
