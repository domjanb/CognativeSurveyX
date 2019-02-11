using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CognativeSurveyX.iOS;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(IOSPath))]
namespace CognativeSurveyX.iOS
{
    public class IOSPath :IPath
    {
        public string MyPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
    }
}