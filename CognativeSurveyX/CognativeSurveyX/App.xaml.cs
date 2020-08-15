using CognativeSurveyX.Nyelv;
using Plugin.Multilingual;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Push;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CognativeSurveyX
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            AppResource.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            MainPage = new MainPage3();
        }
        

        protected override void OnStart()
        {
            // Handle when your app starts
            //AppCenter.Start("1611df4e-a362-4fb4-a1f4-42ce5508c0d2", typeof(Push));

        }
    
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
