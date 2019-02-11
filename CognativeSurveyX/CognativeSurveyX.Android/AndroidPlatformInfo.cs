using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CognativeSurveyX.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidPlatformInfo))]
namespace CognativeSurveyX.Droid
{
    public class AndroidPlatformInfo : IPlatformInfo
    {

        public string GetModell()
        {
            return String.Format("{0} {1}", Build.Manufacturer,Build.Model);

        }

        public string GetVersion()
        {
            return Build.VERSION.Release.ToString();
        }
    }
}