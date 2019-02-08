using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LabelHtml.Forms.Plugin.Droid;
using CognativeSurveyX.Modell;
using System.IO;


namespace CognativeSurveyX.Droid
{
    [Activity(Label = "CognativeSurveyX", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

	        //Constants.myZipPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            HtmlLabelRenderer.Initialize();


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}