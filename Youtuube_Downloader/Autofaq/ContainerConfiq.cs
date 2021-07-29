using System;
using System.ComponentModel;
using Autofac;
using YouTube_dowload_Services.Services.Autofaqinterfaces;
using YouTube_dowload_Services.Services.FireYoutubeDownloaderServices;
using IContainer = Autofac.IContainer;

namespace Youtuube_Downloader.Autofaq
{
    public static class ContainerConfiq
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //builderType.Register<>().As<>();
            builder.RegisterType<Aolication>().As<IAolication>();
            builder.RegisterType<DownloadThumbnailScreenShoot>().As<IDownloadThumbnailScreenShootInterface>();

            return builder.Build();
        }
    }
}
