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

            /*var x = reggi_e();
            
            Debug.WriteLine(x.ToString());
            Debug.WriteLine(x.Result);
            if (x.Result)
            {
                this.Master = new MenuPage();
                this.Detail = new NavigationPage(new ProjectPage());
            }
            else
            {
                this.Master = new MenuPage();
                //this.Detail = new NavigationPage(new MainPage2());
                this.Detail = new NavigationPage(new ReggiPage());
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
        /*public async Task<bool> reggi_e()
        {
            UsersDataAccessAsync adatBazisAsync = new UsersDataAccessAsync();
            var regisztrácioK = await adatBazisAsync.GetCogAzonAsync();
            foreach(var item in regisztrácioK)
            {

            }
            if (regisztrácioK.Count()==1)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }*/
    }
}