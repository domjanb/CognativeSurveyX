using CognativeSurveyX.Nyelv;
using Plugin.Multilingual;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CognativeSurveyX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppResource.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            MainPage = new MainPage2();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
