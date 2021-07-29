using System;
using System.Threading.Tasks;
using Gtk;
using YouTubeDownloadProject.Model;
using YouTubeDownloadProject.Services;
using YouTube_dowload_Services.Services.FireYoutubeDownloaderServices;
using Gdk;
using YouTube_dowload_Services.Services.Autofaqinterfaces;

public partial class MainWindow : Gtk.Window
{
    VidInfoModell SelectedVidInfo = new VidInfoModell();

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Pressed(object sender, EventArgs e)
    {
        System.Environment.Exit(1);
    }

    protected async void OnDownloadBTNPressed(object sender, EventArgs e)
    {
        await DownloadVideoAsync(0);
    }

    protected async Task OnDownloadBTNHighResPressedAsync(object sender, EventArgs e)
    {
        await DownloadVideoAsync(1);
    }

    protected async void OnRetriveVidInfoPressed(object sender, EventArgs e)
    {
        await getYoutubeInfoAsync();
    }

    // Retrive info from frontend.
    private string GetEntertYoutubeLink()
    {
        return YouTubbeLinkInput_Textbox.Text;
    }

    // Api calls.
    private async Task getYoutubeInfoAsync()
    {
        try
        {
            SelectedVidInfo = await RetriveYouTubeVidInfo.getYoutubeVidAsync(GetEntertYoutubeLink());
            PrintOutInfo();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Error, Retriving information about youtube vid.");
            YouTubbeLinkInput_Textbox.Text = "Error, Retriving information about youtube vid. Write in a new one" + " " + e;
        }
    }

    /// <summary>
    /// Downloading selected youtube vidoe. Expected res input selection select the resoltion of the video.
    /// Res = 0 means maximum resoluiton to 720p. Res = 1 means maximum resoluiton of video. Higher than thath is selecting specific resolution.
    /// </summary>
    /// <returns>calls the downloadfunction in backend.</returns>
    /// <param name="res">Res.</param>
    private async Task DownloadVideoAsync(int res)
    {
        try
        {
            DownloadProgressLBL.Text = "Downloading";

            if (res >= 2)
                await DownloadYouTubeVid.HighEndDownload(SelectedVidInfo, res, 30);
            else if(res == 1)
                await DownloadYouTubeVid.HighEndDownload(SelectedVidInfo, 0, 0);
            else
                await DownloadYouTubeVid.DownloadYouTubeVidFunction(SelectedVidInfo);

            DownloadProgressLBL.Text = "Downloaded to Aplication Folder";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Error, Downloading youtube video.");
            YouTubbeLinkInput_Textbox.Text = "Error, Downloading youtube vid. Try Again" + " " + e;
        }
    }

    // Printout to gui
    private void PrintOutInfo()
    {
        try
        {
            DownloadThumbnailScreenShoot ThumbnailDownloader = new DownloadThumbnailScreenShoot();

            ThumbnailDownloader.DownloadThumbnailScreenShoote(SelectedVidInfo);

            // Displaying thumbnail
            Video_Image.Pixbuf = new Pixbuf(@"ThumbNail.png");

            Description_TextBox.Buffer.Text = "Duration: " + SelectedVidInfo.Duration + "\n" + "Description: " + "\n" + SelectedVidInfo.Description;

            VideoTitle_LBL.Text = "Title of the clip: " + SelectedVidInfo.VidTitle;
        }
        catch (Exception e)
        {
            Description_TextBox.Buffer.Text = e.ToString();
        }
    }
}
