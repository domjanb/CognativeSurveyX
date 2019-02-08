using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX
{
    public interface IDownloader
    {
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }

    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public string ZipFileMentett = "";
        public DownloadEventArgs(bool fileSaved, string zipFileMentett)
        {
            FileSaved = fileSaved;
            ZipFileMentett = zipFileMentett;

        }
    }
}
