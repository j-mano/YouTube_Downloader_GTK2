using System;
using System.Threading.Tasks;
using Gtk;
using YouTubeDownloadProject.Model;
using YouTubeDownloadProject.Services;
using Gdk;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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
        // Console app
        System.Environment.Exit(1);
    }

    protected async void OnDownloadBTNPressed(object sender, EventArgs e)
    {
        await DownloadVideoAsync();
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

    // Api calls
    private async Task getYoutubeInfoAsync()
    {
        try
        {
            //SelectedVidInfo = await RetriveYouTubeVidInfo.getYoutubeVidAsync(GetEntertYoutubeLink());
            PrintOutInfo();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Error, Retriving information about youtube vid.");
            YouTubbeLinkInput_Textbox.Text = "Error, Retriving information about youtube vid. Write in a new one";
        }
    }

    private async Task DownloadVideoAsync()
    {
        try
        {
            DownloadProgressLBL.Text = "Downloading";
            await DownloadYouTubeVid.DownloadYouTubeVidFunction(SelectedVidInfo);
            DownloadProgressLBL.Text = "Downloaded to Aplication Folder";
        }
        catch
        {
            Console.WriteLine("Error, Downloading youtube video.");
            YouTubbeLinkInput_Textbox.Text = "Error, Downloading youtube vid. Try Again";
        }
    }

    // Printout to gui
    private void PrintOutInfo()
    {
        SaveImage(SelectedVidInfo.thumb.Url, @"/h", ImageFormat.Png);

        // Loading image from url
        Video_Image.Pixbuf = new Pixbuf(@"h.jpeg");

        Description_TextBox.Buffer.Text = "Duration: " + SelectedVidInfo.Duration + "\n" + "Description: " + "\n" + SelectedVidInfo.Description;

        VideoTitle_LBL.Text = "Title of the clip: " + SelectedVidInfo.VidTitle;
    }

    public void SaveImage(string imageUrl, string filename, ImageFormat format)
    {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(imageUrl);
        Bitmap bitmap; bitmap = new Bitmap(stream);

        if (bitmap != null)
        {
            bitmap.Save(filename, format);
        }

        stream.Flush();
        stream.Close();
        client.Dispose();
    }
}
