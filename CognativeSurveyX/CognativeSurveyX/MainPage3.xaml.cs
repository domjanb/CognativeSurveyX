using CognativeSurveyX.myDataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage3 : MasterDetailPage
    {
        public MainPage3()
        {
            InitializeComponent();

            UsersDataAccessAsync adatBazisAsync = new UsersDataAccessAsync();
            

            /*var regisztrácioK = adatBazisAsync.GetCogAzonAsync();
            //regisztrácioK.Wait();
            //var loaded = regisztrácioK.Result;

            Debug.WriteLine(regisztrácioK.ToString());
            foreach (var item in regisztrácioK)
            {

            }*/


            UsersDataAccess adatBazis = new UsersDataAccess();
            int regisztrácioDarab = adatBazis.GetCogAzon().Count();
            if (regisztrácioDarab == 1)
            {
                this.Master = new MenuPage();
                this.Detail = new NavigationPage(new ProjectPage());
            }
            else
            {
                this.Master = new MenuPage();
                //this.Detail = new NavigationPage(new MainPage2());
                this.Detail = new NavigationPage(new ReggiPage());
            }
                
            
        }
    }
}