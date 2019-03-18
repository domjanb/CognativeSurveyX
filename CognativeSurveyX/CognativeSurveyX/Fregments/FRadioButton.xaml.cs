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
        List<RadioButton> listCheckbox = new List<RadioButton>();
        public static List<Tuple<int, string>> mySortTomb = new List<Tuple<int, string>>();
        public static List<Tuple<int, RadioButton>> myTomb = new List<Tuple<int, RadioButton>>();
        public FRadioButton ()
		{
			InitializeComponent ();
            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item));
            }
            mySortTomb.Add(Tuple.Create(100, "OK"));


            if (Constans.aktQuestion.random_choices == true)
            {
                var rand = new Random();
                for (var i = 1; i < index; i++)
                {

                    int random1 = rand.Next(0, index + 1);
                    int random2 = rand.Next(0, index + 1);
                    Debug.WriteLine("randomok:" + random1 + " - " + random2);
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

            HtmlLabel kerdes = new HtmlLabel();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            int idx = 0;
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = itemTomb.Item2;
                idx++;
                RadioButton button = new RadioButton();
                string buttonDuma = item;
                if (item.Length > 2)
                {
                    if (item.Substring(item.Length - 2, 2) == ";O")
                    {
                        button.KellEOther = true;
                        buttonDuma = item.Substring(0, item.Length - 2 - 1);
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
                myTomb.Add(Tuple.Create(idx,button));
                listCheckbox.Add(button);
                    //button.Opacity = 1;
                    button.CheckedChange += Button_CheckedChange;
                    myStack.Children.Add(button);
                }
                //myLayout
                myLayout.Children.Add(myScroll);
            }
        private void Button_CheckedChange(object sender, bool e)
        {
            //throw new NotImplementedException();
            //Debug.WriteLine("volt nyomi");
            Constans.valaszok = "";
            ((RadioButton)sender).enModositok = true;
            int idx = 0;
            foreach (var item in listCheckbox)
            {
                idx++;
                if (item.Id == ((RadioButton)sender).Id)
                {
                    item.myIschecked = true;
                }
                else
                {
                    item.myIschecked = false;
                }
                
            }
            ((RadioButton)sender).enModositok = false;
            foreach (var item in myTomb)
            {

                if (item.Item2.myIschecked)
                {
                    var akkod = Constans.aktQuestion.choicesKod[item.Item1-1];
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(item.Item1);
                }
            }

            Debug.WriteLine("Nyomi:" + ((RadioButton)sender).Text);

        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}