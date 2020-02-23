using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            UsersDataAccess adatBazis = new UsersDataAccess();
            Button torolValasz = new Button();
            torolValasz.Text = "DeleAnswers";
            torolValasz.Clicked += async (sender, e) =>
            {
               adatBazis.DeleteCogDataAll();
            };
            myLayout.Children.Add(torolValasz);

            Button torolReggi = new Button();
            torolReggi.Text = "DeleReggi";
            torolReggi.Clicked += async (sender, e) =>
            {
                adatBazis.DeleteCogAzonAll();
            };
            myLayout.Children.Add(torolReggi);

            Button torolKerdiv = new Button();
            torolKerdiv.Text = "DeleQuest";
            torolKerdiv.Clicked += async (sender, e) =>
            {
                adatBazis.DeleteCogDataKerdivAll();
            };
            myLayout.Children.Add(torolKerdiv);


            Button torolKozponti = new Button();
            torolKozponti.Text = "DeleCentral";
            torolKozponti.Clicked += async (sender, e) =>
            {
                adatBazis.DeleteKozpontiAll();
            };
            myLayout.Children.Add(torolKozponti);
        }
    }
}