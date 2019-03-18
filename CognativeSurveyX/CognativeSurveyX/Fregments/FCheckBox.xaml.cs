using CognativeSurveyX.Controls;
using CognativeSurveyX.Modell;
using LabelHtml.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FCheckBox : ContentPage
	{
        List<Checkbox> listCheckbox = new List<Checkbox>();
        public static List<Tuple<int, Checkbox>> myTomb = new List<Tuple<int, Checkbox>>();
        
        public FCheckBox ()
		{
			InitializeComponent ();
            myTomb.Clear();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            HtmlLabel kerdes = new HtmlLabel();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            kerdes.Margin = new Thickness(1, 1, 1, 10);
            myStack.Children.Add(kerdes);
            int idx = 0;
            foreach (var item in Constans.aktQuestion.choices)
            {
                idx++;
                string buttonDuma = item;
                Checkbox button = new Checkbox();
                if (item.Substring(item.Length - 2, 2) == ";O")
                {
                    button.KellEOther = true;
                    buttonDuma = item.Substring(0, item.Length - 2 - 1);
                }
                button.Text = buttonDuma;
                //button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;
                button.Margin = new Thickness(10, 0, 10, 0);
                button.Padding = new Thickness(1, -5, 1, -5);
                myTomb.Add(Tuple.Create(idx, button));

                if (!Constans.aktQuestion.choicesVisible[idx-1])
                {
                    button.IsVisible = false;
                }
                listCheckbox.Add(button);
                //button.Opacity = 1;
                button.CheckedChange += Button_CheckedChange;
                button.EntryChange += Button_EntryChange;
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);
        }

        private void Button_EntryChange(object sender, TextChangedEventArgs e)
        {
            Checkbox mostNyomi = (Checkbox)sender;
            if (mostNyomi.TextOther.Length > 0)
            {
                if (!mostNyomi.IsChecked)
                {
                    mostNyomi.IsChecked = true;
                }
            }
            
        }

        private void Button_CheckedChange(object sender, bool e)
        {
            Constans.valaszok = "";
            foreach (var item in myTomb)
            {
                
                if (item.Item2.IsChecked)
                {
                    string otherDuma = "";
                    if (item.Item2.KellEOther)
                    {
                        var akkod=Constans.aktQuestion.choicesKod[item.Item1-1];
                        otherDuma = Constans.aktQuestion.kerdeskod + "other_" + Convert.ToString(item.Item1) + "=" + Convert.ToString(Constans.kipofoz(item.Item2.TextOther)) + ";";
                    }
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(item.Item1) + "=" + Convert.ToString(item.Item1) + ";" + otherDuma;
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);
            var a = 2;
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}