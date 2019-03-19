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
	public partial class FMaxDiff : ContentPage
	{
        //List<MaxDiff> listCheckbox = new List<MaxDiff>();
        //public static List<Tuple<int, MaxDiff>> myTomb = new List<Tuple<int, MaxDiff>>();
        public static List<Tuple<int, string, MaxDiff>> mySortTomb = new List<Tuple<int, string, MaxDiff>>();

        public FMaxDiff ()
		{
			InitializeComponent ();
            mySortTomb.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item, new MaxDiff()));
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
            bool joszin = true;
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul(itemTomb.Item2);
                idx++;
                MaxDiff button = new MaxDiff();
                button.Text = item;
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;
                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                button.Padding = new Thickness(padding, 0, padding, 0);
                if (!Constans.aktQuestion.choicesVisible[idx - 1])
                {
                    button.IsVisible = false;
                }
                else
                {
                    joszin = !joszin;
                }
                //if (idx % 2 != 1)
                if (joszin)
                {
                    button.BackgroundColor = Color.AliceBlue;
                }
                else
                {
                    button.BackgroundColor = Color.AntiqueWhite;
                }


                //myTomb.Add(Tuple.Create(idx, button));
                //listCheckbox.Add(button);
                button.CheckedChangeBal += Button_CheckedChangeBal;
                button.CheckedChangeJobb += Button_CheckedChangeJobb;
                myStack.Children.Add(button);
            }
            //myLayout
            myLayout.Children.Add(myScroll);
        }
        private void Button_CheckedChangeBal(object sender, bool e)
        {
            Constans.valaszok = "";
            ((MaxDiff)sender).enModositokBal = true;
            int idx = 0;
            foreach (var item in mySortTomb)
            {
                idx++;
                if (item.Item3.Id == ((MaxDiff)sender).Id)
                {
                    item.Item3.myIscheckedBal = true;
                }
                else
                {
                    item.Item3.myIscheckedBal = false;
                }

            }
            ((MaxDiff)sender).enModositokBal = false;
            foreach (var item in mySortTomb)
            {

                if (item.Item3.myIscheckedBal)
                {
                    var akkod = Constans.aktQuestion.choicesKod[item.Item1];
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "_a=" + Convert.ToString(item.Item1);
                }
            }
            //Debug.WriteLine("NyomiBal:" + ((MaxDiff)sender).Text);
        }
        private void Button_CheckedChangeJobb(object sender, bool e)
        {
            Constans.valaszok = "";
            ((MaxDiff)sender).enModositokJobb = true;
            int idx = 0;
            foreach (var item in mySortTomb)
            {
                idx++;
                if (item.Item3.Id == ((MaxDiff)sender).Id)
                {
                    item.Item3.myIscheckedJobb = true;
                }
                else
                {
                    item.Item3.myIscheckedJobb = false;
                }

            }
            ((MaxDiff)sender).enModositokJobb = false;
            foreach (var item in mySortTomb)
            {

                if (item.Item3.myIscheckedJobb)
                {
                    var akkod = Constans.aktQuestion.choicesKod[item.Item1];
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "_b=" + Convert.ToString(item.Item1);
                }
            }
            //Debug.WriteLine("NyomiJobb:" + ((MaxDiff)sender).Text);
        }


        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}