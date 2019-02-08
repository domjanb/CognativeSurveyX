using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using CognativeSurveyX.Droid;
using System.IO;
using System.Net;
using System.ComponentModel;
using CognativeSurveyX.Modell;

[assembly: Dependency(typeof(AndroidDownloader))]
namespace CognativeSurveyX.Droid
{
    public class AndroidDownloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public string zipFileMentett = "";
        string pathToNewFolder;

        public void DownloadFile(string url, string folder)
        {
            pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            //var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            pathToNewFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), folder);
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
            //Constans.errorDuma = e.Error;
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