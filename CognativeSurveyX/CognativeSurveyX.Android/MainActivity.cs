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
using Plugin.Media;
using Plugin.Permissions;
using Plugin.CurrentActivity;


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
            CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //Constants.myZipPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            HtmlLabelRenderer.Initialize();
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
    
}