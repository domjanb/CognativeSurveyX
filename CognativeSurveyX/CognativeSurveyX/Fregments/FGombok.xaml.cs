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
        List<Gomb> listButtons = new List<Gomb>();
        public FGombok ()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myStack.VerticalOptions = LayoutOptions.FillAndExpand;
            myStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);
            //myLayout.Children.Add(kerdes);

            int mostIndex = 0;
            foreach (var item in Constans.aktQuestion.choices)
            {
                mostIndex++;
                Gomb button = new Gomb
                {
                    Text = item,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    BackgroundColor = Color.Transparent
                };
                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                button.Padding = new Thickness(padding, 0, padding, 0);
                listButtons.Add(button);
                if (mostIndex == Constans.aktQuestion.choices.Count)
                {
                   
                    button.Isuccso = true;
                }
                else
                {
                    
                    button.Isuccso = false;
                }
                
                //button.CheckedChange += Button_CheckedChange1;
                button.CheckedChange += button_CheckedChange;
                //button.CheckedChange += button_Clicked;
                /*Thickness margin = new Thickness();
                margin.Bottom = -10;
                margin.Top = -0;
                margin.Left= 0;
                margin.Right = 0;
                Frame fr = new Frame
                {
                    OutlineColor = Color.Black,
                    BackgroundColor = Color.Aqua,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    CornerRadius = 10,
                    Margin = margin,
                    Content = button,
                    Content = new Label
                    {
                        Text = "I've been framed!",
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        FontAttributes = FontAttributes.Italic,
                        TextColor = Color.Blue
                    }
                };*/


                if (!Constans.aktQuestion.choicesVisible[mostIndex-1])
                {
                    button.IsVisible = false;
                }
                myStack.Children.Add(button);
            }


            myLayout.Children.Add(myScroll);
        }

        private void Button_CheckedChange1(object sender, bool e)
        {
            
            Debug.WriteLine("nyomi");
        }

        private void button_CheckedChange(object sender, bool e)
        {
            Debug.WriteLine("nyomi");
            Gomb mostNyomi = (Gomb)sender;
            int idx = 0;
            foreach (Gomb button in listButtons)
            {
                idx++;
                if (button.Id == mostNyomi.Id)
                {
                    button.myIschecked = true;
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(idx);
                }
                else
                {
                    button.myIschecked= false;
                }
            }
            

        }

        private void button_Clicked(object sender, EventArgs e)
        {
            /*Button mostNyomi = (Button)sender;
            foreach (Button button in listButtons)
            {
                string eleje = button.Text;
                string eleje2 = Convert.ToString(eleje.ElementAt(0));
                string vege = eleje.Substring(1, eleje.Length - 1);
                if (button.Id == mostNyomi.Id)
                {
                    button.Text = Constans.bumbuc_true + vege;
                }
                else
                {
                    button.Text = Constans.bumbuc_false + vege;
                }
            }*/


            Debug.WriteLine("nyomi");


        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}