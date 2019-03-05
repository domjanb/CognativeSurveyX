using CognativeSurveyX.Data;
using CognativeSurveyX.Fregments;
using CognativeSurveyX.Modell;
using CognativeSurveyX.myDataBase;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReggiPage : ContentPage
	{
        event ConnectivityChangedEventHandler ConnectivityChanged;
        UsersDataAccess adatBazis = new UsersDataAccess();
        List<Entry> valaszok = new List<Entry>();
        Label lbl = new Label();
        Button reggomb;
        private RestApiModell visszaRestApi;
        List<Button> listOfButtons = new List<Button>();

        public ReggiPage()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                Debug.WriteLine($"Connectivity changed to {args.IsConnected}");
                lbl.IsVisible = false;
                reggiFormMutat();
            };

            adatBazis.DeleteCogAzonAll();

            //regform
            int netTipus = Constans.milyenANet();
            if (netTipus == 0)
            {
                
                lbl.Text = "Kérlek kapcsolj NET-et";
                myLayout.Children.Add(lbl);
            }
            else
            {
                reggiFormMutat();
            }
        
        }

        private void reggiFormMutat()
        {
            var regForm = new Grid();
            regForm.HorizontalOptions = LayoutOptions.Center;
            regForm.Padding = 20;

            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            regForm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            regForm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            regForm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            regForm.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //regForm.ColumnDefinitions.w
            var zeroC = new Label { Text = "", HorizontalTextAlignment = TextAlignment.End };
            var nameC = new Label { Text = Nyelv.AppResource.Name, HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center };
            var name = new Entry { Placeholder = Nyelv.AppResource.Name };
            name.TextChanged += OnEntryTextChanged;
            var name2C = new Label { Text = Nyelv.AppResource.Surename, HorizontalTextAlignment = TextAlignment.End };
            var name2 = new Entry { Placeholder = Nyelv.AppResource.Surename };
            name2.TextChanged += OnEntryTextChanged;
            var codeC = new Label { Text = Nyelv.AppResource.Code, HorizontalTextAlignment = TextAlignment.End };
            var code = new Entry { Placeholder = Nyelv.AppResource.Code, Keyboard = Keyboard.Numeric };
            code.TextChanged += OnEntryTextChanged;
            var passC = new Label { Text = Nyelv.AppResource.Password, HorizontalTextAlignment = TextAlignment.End };
            var pass = new Entry { Placeholder = Nyelv.AppResource.Password, IsPassword = true };
            pass.TextChanged += OnEntryTextChanged;
            var emilC = new Label { Text = Nyelv.AppResource.Email, HorizontalTextAlignment = TextAlignment.End };
            var emil = new Entry { Placeholder = Nyelv.AppResource.Email };
            emil.TextChanged += OnEntryTextChanged;
            var regButton = new Button { Text = Nyelv.AppResource.Registration };
            //regButton.IsVisible = false;
            //regButton.Clicked +=await regButtonClickAsync;
            regButton.Clicked += async (sender, e) =>
            {
                valaszok[0].Text = "33";
                valaszok[1].Text = "33";
                valaszok[2].Text = "33";
                valaszok[3].Text = "33";
                valaszok[4].Text = "33";
                User user = new User();
                user.user_name = valaszok[0].Text;
                user.user_surnamed = valaszok[1].Text;
                user.user_kod = valaszok[2].Text;
                user.user_password = valaszok[3].Text;
                user.user_emil = valaszok[4].Text;
                var rs = new Data.RestService();
                Debug.WriteLine(user);
                visszaRestApi = await rs.Reggi(user);
                Debug.WriteLine("visszastring:" + Convert.ToString(visszaRestApi));
                if (visszaRestApi.error)
                {
                    var idd2 = adatBazis.SaveCogAzon(new Cogazon
                    {
                        uemail = user.user_emil,
                        uname = user.user_name,
                        upass = user.user_password,
                        userid = Convert.ToInt16(user.user_kod),
                        usname = user.user_surnamed
                    });

                    
                    

                }
                var cc = visszaRestApi.message;

                //var visszatrue= vissza.Rootobject.error;
                Debug.WriteLine("vlasz " + Convert.ToString(visszaRestApi));
                if (visszaRestApi.message == "Regisztráció rendben")
                {
                    //this.Master = new MenuPage();
                    //this.Detail = new NavigationPage(new ProjectPage());
                    Navigation.PushModalAsync(new MainPage3());
                }
            };
            reggomb = regButton;

            valaszok.Add(name);
            valaszok.Add(name2);
            valaszok.Add(code);
            valaszok.Add(pass);
            valaszok.Add(emil);

            //regForm.Children.Add(zeroC, 0, 0);
            regForm.Children.Add(nameC, 1, 0);
            regForm.Children.Add(name, 2, 0);
            regForm.Children.Add(name2C, 1, 1);
            regForm.Children.Add(name2, 2, 1);
            regForm.Children.Add(codeC, 1, 2);
            regForm.Children.Add(code, 2, 2);
            regForm.Children.Add(passC, 1, 3);
            regForm.Children.Add(pass, 2, 3);
            regForm.Children.Add(emilC, 1, 4);
            regForm.Children.Add(emil, 2, 4);

            //regForm.Children.Add(regButton, 1, 6);
            //Grid.SetColumnSpan(regButton, 2);
            //Grid.SetColumnSpan(regButton, 1);




            myLayout.Children.Add(regForm);
            myLayout.Children.Add(regButton);
        }

        public class ConnectivityChangedEventArgs : EventArgs
        {
            public bool IsConnected { get; set; }
        }

        public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {

            //
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;

            var inputBox = (Entry)sender;
            Boolean nyert = true;
            for (var i = 0; i < valaszok.Count; i++)
            {
                if (Constans.Length(valaszok[i].Text) > 0)
                {
                    Trace.WriteLine(valaszok[i].Text);


                }
                if (Constans.Length(Constans.BTrim(valaszok[i].Text)) == 0)
                {

                    nyert = false;
                    //Debug.WriteLine("aa");
                    //Console.WriteLine("hellololllll");
                    //Debug.WriteLine(i);
                    //var ho = Length(BTrim(valaszok[i].Text));
                    //Debug.WriteLine(ho);
                }
            }

            if (nyert)
            {
                reggomb.IsVisible = true;
            }
            else
            {
                reggomb.IsVisible = false;
            }
        }
        private int kerdivAlidKeres(int projid)
        {
            int visszaTero = 1;
            UsersDataAccess adatBazis = new UsersDataAccess();
            var kerdivek = adatBazis.GetCogDataAsProjid(projid);
            foreach (var item in kerdivek)
            {
                if (item.alid > visszaTero)
                {
                    visszaTero = item.alid;
                }
            }
            return visszaTero + 1;
        }
    }
}