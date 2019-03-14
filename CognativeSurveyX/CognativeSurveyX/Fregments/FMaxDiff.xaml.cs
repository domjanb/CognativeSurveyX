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
        List<MaxDiff> listCheckbox = new List<MaxDiff>();
        public static List<Tuple<int, MaxDiff>> myTomb = new List<Tuple<int, MaxDiff>>();
        public FMaxDiff ()
		{
			InitializeComponent ();
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
            foreach (var item in Constans.aktQuestion.choices)
            {
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


                myTomb.Add(Tuple.Create(idx, button));
                listCheckbox.Add(button);
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
            foreach (var item in listCheckbox)
            {
                idx++;
                if (item.Id == ((MaxDiff)sender).Id)
                {
                    item.myIscheckedBal = true;
                }
                else
                {
                    item.myIscheckedBal = false;
                }

            }
            ((MaxDiff)sender).enModositokBal = false;
            foreach (var item in myTomb)
            {

                if (item.Item2.myIscheckedBal)
                {
                    var akkod = Constans.aktQuestion.choicesKod[item.Item1 - 1];
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "_a=" + Convert.ToString(item.Item1);
                }
            }
            Debug.WriteLine("NyomiBal:" + ((MaxDiff)sender).Text);
        }
        private void Button_CheckedChangeJobb(object sender, bool e)
        {
            Constans.valaszok = "";
            ((MaxDiff)sender).enModositokJobb = true;
            int idx = 0;
            foreach (var item in listCheckbox)
            {
                idx++;
                if (item.Id == ((MaxDiff)sender).Id)
                {
                    item.myIscheckedJobb = true;
                }
                else
                {
                    item.myIscheckedJobb = false;
                }

            }
            ((MaxDiff)sender).enModositokJobb = false;
            foreach (var item in myTomb)
            {

                if (item.Item2.myIscheckedJobb)
                {
                    var akkod = Constans.aktQuestion.choicesKod[item.Item1 - 1];
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "_b=" + Convert.ToString(item.Item1);
                }
            }
            Debug.WriteLine("NyomiJobb:" + ((MaxDiff)sender).Text);
        }


        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}