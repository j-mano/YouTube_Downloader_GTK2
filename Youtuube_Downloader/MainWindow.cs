using System;
using System.Threading.Tasks;
using Gtk;
using YouTubeDownloadProject.Model;
using YouTubeDownloadProject.Services;

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

    protected void OnDownloadBTNPressed(object sender, EventArgs e)
    {

    }


    protected void OnRetriveVidInfoPressed(object sender, EventArgs e)
    {
        Task getYoutubeInfoTask = Task.Run(() => { getYoutubeInfo(); });
    }

    // Retrive info from frontend.
    private string GetEntertYoutubeLink()
    {
        return YouTubbeLinkInput_Textbox.Text;
    }

    // Api calls
    private async void getYoutubeInfo()
    {
        try
        {
            SelectedVidInfo = await RetriveYouTubeVidInfo.getYoutubeVidAsync(GetEntertYoutubeLink());
            printOutInfo();
        }
        catch
        {
            Console.WriteLine("Error, Retriving information about youtube vid.");
            YouTubbeLinkInput_Textbox.Text = "Error, Retriving information about youtube vid. Write in a new one";
        }
    }

    private async void DownloadVideo()
    {
        try
        {
            DownloadProgressLBL.Text = "Downloading";
            await DownloadYouTubeVid.DownloadYouTubeVidFunction(SelectedVidInfo);
            DownloadProgressLBL.Text = "Downloaded to Aplication Folder";
        }
        catch
        {
            Console.WriteLine("Error, Downloading youtube vid.");
            YouTubbeLinkInput_Textbox.Text = "Error, Downloading youtube vid. Try Again";
        }
    }

    // Printout to gui
    private void printOutInfo()
    {

        Description_TextBox.Buffer.Text = "Duration: " + SelectedVidInfo.Duration + "\n" + "Description: " + "\n" + SelectedVidInfo.Description;

        VideoTitle_LBL.Text = "Title of the clip: " + SelectedVidInfo.VidTitle;
    }
}
