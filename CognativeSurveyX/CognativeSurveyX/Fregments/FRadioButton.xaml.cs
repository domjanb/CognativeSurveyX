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
	public partial class FRadioButton : ContentPage
	{
        public static List<Tuple<int, string,RadioButton>> mySortTomb = new List<Tuple<int, string, RadioButton>>();
        public FRadioButton ()
		{
			InitializeComponent ();
            mySortTomb.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item,new RadioButton()));
            }

            if (Constans.aktQuestion.random_choices == true)
            {
                var rand = new Random();
                for (var i = 1; i < index; i++)
                {

                    int random1 = rand.Next(0, index + 1);
                    int random2 = rand.Next(0, index + 1);
                    if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random1].Item2)))
                    {
                        random1 = index+1000;
                    }
                    else if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random2].Item2)))
                    {
                        random2 = index + 1000;
                    }
                    if (random1 != random2 && random1 < index && random2 < index)
                    {
                        bool kell = true;
                        if (mySortTomb[random1].Item2.Length > 3)
                        {
                            
                            if (mySortTomb[random1].Item2.ToLower().Substring(mySortTomb[random1].Item2.Length - 2, 2) == "-r") { kell = false; }
                        }
                        if (mySortTomb[random2].Item2.Length > 3)
                        {
                            if (mySortTomb[random2].Item2.ToLower().Substring(mySortTomb[random2].Item2.Length - 2, 2) == "-r") { kell = false; }
                        }
                        if (kell)
                        {
                            var tmp = mySortTomb[random1];
                            mySortTomb[random1] = mySortTomb[random2];
                            mySortTomb[random2] = tmp;
                        }

                    }

                }
            }


            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            HtmlLabel kerdes = new HtmlLabel();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            int idx = 0;
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul( itemTomb.Item2);
                idx++;
                RadioButton button = itemTomb.Item3;
                string buttonDuma = item;
                if (item.Length > 2)
                {
                    if (Constans.VanEOpen( Constans.ValaszParameter(itemTomb.Item2)))
                    {
                        button.KellEOther = true;
                    }
                }
                
                button.Text = buttonDuma;
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;
                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                button.Padding = new Thickness(padding, 0, padding, 0);
                if (!Constans.aktQuestion.choicesVisible[idx - 1])
                {
                    button.IsVisible = false;
                }
                button.CheckedChange += Button_CheckedChange;
                button.EntryChange += Button_EntryChange;
                myStack.Children.Add(button);
                }
                myLayout.Children.Add(myScroll);
            }

        private void Button_EntryChange(object sender, TextChangedEventArgs e)
        {
            RadioButton mostNyomi = (RadioButton)sender;
            if (mostNyomi.TextOther.Length > 0)
            {
                if (!mostNyomi.IsChecked)
                {
                    mostNyomi.IsChecked = true;
                }
                else
                {
                    Button_CheckedChange(sender, true);
                }
            }
        }

        private void Button_CheckedChange(object sender, bool e)
        {


            Constans.valaszok = "";
            ((RadioButton)sender).enModositok = true;
            foreach (var item in mySortTomb)
            {
                if (item.Item3.Id == ((RadioButton)sender).Id)
                {
                    var otherDuma = "";
                    item.Item3.myIschecked = true;
                    if (item.Item3.KellEOther)
                    {
                        var akkod = item.Item1;
                        otherDuma = Constans.aktQuestion.kerdeskod + "other" +  "=" + Convert.ToString(Constans.kipofoz(item.Item3.TextOther)) + ";";
                    }
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod +  "=" + Convert.ToString(item.Item1) + ";" + otherDuma;
                    Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);
                }
                else
                {
                    item.Item3.myIschecked = false;
                }
            }
            ((RadioButton)sender).enModositok = false;


            //Debug.WriteLine("Nyomi:" + ((RadioButton)sender).Text);

        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}