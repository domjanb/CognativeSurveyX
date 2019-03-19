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
        //List<Checkbox> listCheckbox = new List<Checkbox>();
        //public static List<Tuple<int, Checkbox>> myTomb = new List<Tuple<int, Checkbox>>();
        public static List<Tuple<int, string, Checkbox>> mySortTomb = new List<Tuple<int, string, Checkbox>>();

        public FCheckBox ()
		{
			InitializeComponent ();
            mySortTomb.Clear();
            //myTomb.Clear();



            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item, new Checkbox()));
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
                        random1 = index + 1000;
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
                var a = 2;
            }

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
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul(itemTomb.Item2);
                idx++;
                Checkbox button = itemTomb.Item3;
                string buttonDuma = item;
                if (item.Length > 2)
                {
                    if (Constans.VanEOpen(Constans.ValaszParameter(itemTomb.Item2)))
                    {
                        button.KellEOther = true;
                    }
                }
                button.Text = buttonDuma;
                //button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;
                button.Margin = new Thickness(10, 0, 10, 0);
                button.Padding = new Thickness(1, -5, 1, -5);
                //myTomb.Add(Tuple.Create(idx, button));

                if (!Constans.aktQuestion.choicesVisible[idx-1])
                {
                    button.IsVisible = false;
                }
                //listCheckbox.Add(button);
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
                else
                {
                    Button_CheckedChange(sender, true);
                }
            }
            
        }

        private void Button_CheckedChange(object sender, bool e)
        {
            Constans.valaszok = "";
            foreach (var item in mySortTomb)
            {
                if (item.Item3.IsChecked)
                {
                    string otherDuma = "";
                    if (item.Item3.KellEOther)
                    {
                        var akkod=Constans.aktQuestion.choicesKod[item.Item1];
                        otherDuma = Constans.aktQuestion.kerdeskod + "other_" + Convert.ToString(item.Item1) + "=" + Convert.ToString(Constans.kipofoz(item.Item3.TextOther)) + ";";
                    }
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(item.Item1) + "=" + Convert.ToString(item.Item1) + ";" + otherDuma;
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}