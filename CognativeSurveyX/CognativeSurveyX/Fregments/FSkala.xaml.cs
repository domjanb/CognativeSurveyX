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
	public partial class FSkala : ContentPage
	{
        public static List<Tuple<int, string, CsillagSkala>> mySortTomb = new List<Tuple<int, string, CsillagSkala>>();
        public FSkala ()
		{
			InitializeComponent ();
            mySortTomb.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.items)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(index+1), item, new CsillagSkala()));
            }
            //Constans.aktQuestion.random_items = true;
            if (Constans.aktQuestion.random_items == true)
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
                    if (random1 != random2 && random1 <= index && random2 <= index)
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

            
            
            var idx = 0;
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul(itemTomb.Item2);

                Label lbl = new Label();
                lbl.Text = item;
                myStack.Children.Add(lbl);

                CsillagSkala button = itemTomb.Item3;

                
                idx++;
                button.ValaszDB = Constans.aktQuestion.choices.Count();
                button.FontMeret = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.CheckedChange += button_CheckedChange;
                button.Text = item;

                if (idx % 2 != 1)
                {
                    //button.BackgroundColor = Color.AliceBlue;
                }
                else
                {
                    //button.BackgroundColor = Color.AntiqueWhite;
                }

                
                /*if (!Constans.aktQuestion.choicesVisible[idx - 1])
                {
                    button.IsVisible = false;
                }*/
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);
        }

        private void button_CheckedChange(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Nyomi:" + ((CsillagSkala)sender).Text + Convert.ToString(e));
            //Constans.valaszok = Constans.aktQuestion.kerdeskod + "_" + itemTomb.Item1 + "=" + Convert.ToString(e);


        }


        private void _Continue_Clicked(object sender, EventArgs e)
        {
            foreach (var itemTomb in mySortTomb)
            {
                if (itemTomb.Item3.Value != 0)
                {
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + itemTomb.Item1 + "=" + Convert.ToString(itemTomb.Item3.Value) + ";";
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}