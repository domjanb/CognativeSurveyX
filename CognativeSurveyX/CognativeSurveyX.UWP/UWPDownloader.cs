using CognativeSurveyX.Modell;
using CognativeSurveyX.UWP;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(UWPDownloader))]
namespace CognativeSurveyX.UWP
{
    public class UWPDownloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public string zipFileMentett = "";
        string pathToNewFolder;
        public void DownloadFile(string url, string folder)
        {
            //string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);
            pathToNewFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, folder);
            //Constans.myZipPath = pathToNewFolder;
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
                zipFileMentett = Path.GetFileName(url);
            }
            catch (Exception ex)
            {
                //Constans.exception = ex;
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false, zipFileMentett));
            }
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false, zipFileMentett));
            }
            else
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true, zipFileMentett));
                
                //Constans.ExtractZipFile(pathToNewFolder + "/" + zipFileMentett, null, pathToNewFolder);
            }
        }
    }
    
}
