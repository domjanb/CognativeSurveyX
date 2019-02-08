using CognativeSurveyX.Controls;
using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FSzurRadio2 : ContentPage
	{
        List<Checkbox> listCheckbox = new List<Checkbox>();
        //List<Constans.myCheckbox> listCheckbox = new List<Constans.myCheckbox>();
        //public static Dictionary<string, Checkbox> myCheckbox = new Dictionary<string, Checkbox>();
        public static List<Tuple<string, Checkbox>> myCheckbox = new List<Tuple<string, Checkbox>>();
        public FSzurRadio2 ()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            string csoport = "";
            foreach (var item in Constans.aktQuestion.choices)
            {
                var kotojelPos=item.IndexOf("-");
                if (csoportositoE(item))
                {
                    Kinyilo nyilo = new Kinyilo();
                    nyilo.Text = item;
                    nyilo.CheckedChange += nyilo_CheckedChange;
                    myStack.Children.Add(nyilo);
                    csoport = item;
                }
                else
                {
                    Checkbox button = new Checkbox();
                    button.Text = item;
                    button.HorizontalOptions = LayoutOptions.Start;
                    //button.FontSize = "Large";
                    button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    button.BackgroundColor = Color.Transparent;


                    listCheckbox.Add(button);
                    //myCheckbox.Add(csoport, button);
                    myCheckbox.Add(Tuple.Create(csoport, button));
                    
                    //button.Opacity = 1;
                    button.CheckedChange += Button_CheckedChange;
                    button.IsVisible = false;
                    myStack.Children.Add(button);
                }

            }
            myLayout.Children.Add(myScroll);
        }

        private void nyilo_CheckedChange(object sender, bool e)
        {
            Kinyilo kinyilo = (Kinyilo)sender;
            foreach(var item in myCheckbox)
            {
                if (item.Item1 == kinyilo.Text)
                {
                    if (kinyilo.IsChecked)
                    {
                        item.Item2.IsVisible = true;
                    }
                    else
                    {
                        item.Item2.IsVisible = false;
                    }
                }
                
            }
            //throw new NotImplementedException();
            
        }

        private void Button_CheckedChange(object sender, bool e)
        {
            foreach(var item in listCheckbox)
            {
                if (item.IsChecked)
                {
                    
                    Debug.WriteLine(((Checkbox)item).Text);
                }
            }
            //throw new NotImplementedException();
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
        private bool csoportositoE(string duma)
        {
            bool vissza = true;
            int kotojelPos = duma.IndexOf("-");
            if (kotojelPos > -1)
            {
                string eleje = duma.Substring(0, kotojelPos);
                if (Convert.ToInt64(eleje) != null)
                {
                    vissza = false;
                }
            }


                return vissza;
        }
        public struct CheckboxTomb
        {
            public CheckboxTomb(string neve, Checkbox checkbox)
            {
                Neve = neve;
                Checkbox = checkbox;

            }

            public string Neve { get; private set; }
            public Checkbox Checkbox { get; private set; }

        }

    }
}