using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using CognativeSurveyX.iOS;
using System.ComponentModel;
using System.IO;
using System.Net;
using CognativeSurveyX.Modell;

[assembly: Dependency(typeof(IosDownloader))]
namespace CognativeSurveyX.iOS
{
    public class IosDownloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public string zipFileMentett = "";
        string pathToNewFolder;

        public void DownloadFile(string url, string folder)
        {
            pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);
            Directory.CreateDirectory(pathToNewFolder);
            //Constans.myZipPath = pathToNewFolder;

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
            }
        }
    }
}