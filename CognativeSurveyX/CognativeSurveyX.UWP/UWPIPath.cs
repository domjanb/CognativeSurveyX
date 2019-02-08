using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognativeSurveyX.UWP;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(UWPIPath))]
namespace CognativeSurveyX.UWP
{
    public class UWPIPath :IPath
    {
        public string MyPath => Path.Combine(ApplicationData.Current.LocalFolder.Path);
    }
}
