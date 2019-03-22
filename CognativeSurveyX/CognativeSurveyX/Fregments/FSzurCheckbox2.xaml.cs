using CognativeSurveyX.Controls;
using CognativeSurveyX.Modell;
using LabelHtml.Forms.Plugin.Abstractions;
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
	public partial class FSzurCheckbox2 : ContentPage
	{
        List<Checkbox> listCheckbox = new List<Checkbox>();
        public static List<Tuple<string, string,Checkbox>> myCheckbox = new List<Tuple<string, string,Checkbox>>();
        public FSzurCheckbox2 ()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label sorszam = new Label();
            sorszam.Margin = new Thickness(1, 1, 1, 1);
            sorszam.Text = Constans.sorszamErtek();
            sorszam.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            myStack.Children.Add(sorszam);

            HtmlLabel kerdes = new HtmlLabel();
            kerdes.Text = Constans.ParamErtekeBeilleszt(Constans.aktQuestion.question_title);
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            int idx = 0;
            string csoport = "";
            foreach (var item in Constans.aktQuestion.choices)
            {
                idx++;
                var kotojelPos = item.IndexOf("-");
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

                    string kod = Constans.aktQuestion.choicesKod[idx - 1];
                    listCheckbox.Add(button);
                    //myCheckbox.Add(csoport, button);
                    myCheckbox.Add(Tuple.Create(csoport, kod,button));

                    //button.Opacity = 1;
                    button.CheckedChange += Button_CheckedChange;
                    button.IsVisible = false;
                    if (!Constans.aktQuestion.choicesVisible[idx - 1])
                    {
                        button.IsVisible = false;
                    }
                    myStack.Children.Add(button);
                }

            }
            myLayout.Children.Add(myScroll);
        }

        private void nyilo_CheckedChange(object sender, bool e)
        {
            Constans.valaszok = "";
            Kinyilo kinyilo = (Kinyilo)sender;
            foreach (var item in myCheckbox)
            {
                if (item.Item1 == kinyilo.Text)
                {
                    if (kinyilo.IsChecked)
                    {
                        item.Item3.IsVisible = true;
                        //Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(item.Item1) + "=" + Convert.ToString(item.Item1) + ";" ;
                    }
                    else
                    {
                        item.Item3.IsVisible = false;
                    }
                }

            }
            //throw new NotImplementedException();

        }

        private void Button_CheckedChange(object sender, bool e)
        {
            Constans.valaszok = "";
            /*foreach (var item in listCheckbox)
            {
                if (item.IsChecked)
                {
                    Debug.WriteLine(((Checkbox)item).Text);
                }
            }*/
            foreach (var item in myCheckbox)
            {

                if (item.Item3.IsChecked)
                {
                    string otherDuma = "";
                    if (item.Item3.KellEOther)
                    {
                        otherDuma = Constans.aktQuestion.kerdeskod + "_" + item.Item2 + "other=" + Convert.ToString(Constans.kipofoz(item.Item3.TextOther)) + ";";
                    }
                    //Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" +  Convert.ToString(item.Item1) + "=" + Convert.ToString(item.Item1) + ";" + otherDuma;
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + 
                        item.Item2 + "=" + item.Item2 + ";" + otherDuma;
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);
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