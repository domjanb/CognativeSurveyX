using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CognativeSurveyX.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IOSPlatformInfo))]
namespace CognativeSurveyX.iOS
{
    public class IOSPlatformInfo : IPlatformInfo
    {
        UIDevice device = new UIDevice();
        public string GetModell()
        {
            return device.Model.ToString();
        }

        public string GetVersion()
        {
            return String.Format("{0} {1}", device.SystemName,device.SystemVersion);
        }
    }
}