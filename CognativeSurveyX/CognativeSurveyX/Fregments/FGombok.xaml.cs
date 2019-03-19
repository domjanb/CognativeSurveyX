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
	public partial class FGombok : ContentPage
	{
        public static List<Tuple<int, string, Gomb>> mySortTomb = new List<Tuple<int, string, Gomb>>();
        //List<Gomb> listButtons = new List<Gomb>();
        public FGombok ()
		{
			InitializeComponent ();
            mySortTomb.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item, new Gomb
                {
                    Text = "",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    BackgroundColor = Color.Transparent
                }));
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
            myStack.VerticalOptions = LayoutOptions.FillAndExpand;
            myStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            myScroll.Content = myStack;


            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);


            int mostIndex = 0;
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul(itemTomb.Item2);
                //RadioButton button = itemTomb.Item3;
                mostIndex++;
                Gomb button = itemTomb.Item3;
                button.Text = item;

                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                button.Padding = new Thickness(padding, 0, padding, 0);
                if (mostIndex == Constans.aktQuestion.choices.Count)
                {
                   
                    button.Isuccso = true;
                }
                else
                {
                    
                    button.Isuccso = false;
                }
                button.CheckedChange += button_CheckedChange;
                


                if (!Constans.aktQuestion.choicesVisible[mostIndex-1])
                {
                    button.IsVisible = false;
                }
                myStack.Children.Add(button);
            }


            myLayout.Children.Add(myScroll);
        }

        private void button_CheckedChange(object sender, bool e)
        {
            Debug.WriteLine("nyomi");
            Gomb mostNyomi = (Gomb)sender;
            int idx = 0;
            foreach (var item in mySortTomb)
            {
                idx++;
                if (item.Item3.Id == mostNyomi.Id)
                {
                    item.Item3.myIschecked = true;
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(idx);
                }
                else
                {
                    item.Item3.myIschecked= false;
                }
            }
            

        }

        
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}