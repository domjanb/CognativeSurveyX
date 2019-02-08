using CognativeSurveyX.Controls;
using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Fkepes2 : ContentPage
	{
        List<ImageButton> listButtons = new List<ImageButton>();
        List<StackLayout> listLayout = new List<StackLayout>();
        public static List<Tuple<int , ImageButton>> myTomb = new List<Tuple<int, ImageButton>>();
        
        public Fkepes2 ()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;


            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);


            int itemDb = Constans.aktQuestion.choices.Count;

            for (var i = 0; i < itemDb / 2; i++)
            {
                Constans.myLayout.Add("neve" + Convert.ToString(i),
                    new StackLayout { Orientation = StackOrientation.Horizontal,
                        VerticalOptions  = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        
                        

                    });
            }

            int sor = -1;
            int idx = 0;
            var oszlop = 0;
            int nevindex = 1;
            
            
            foreach (var item in Constans.aktQuestion.choices)
            {
                
                oszlop = 1;
                //if (idx % 2 != 1)
                if (idx < 2)
                {
                    idx++;
                    
                }
                else
                {
                    nevindex = nevindex + 1;
                    idx = 1;
                    sor++;
                    oszlop = 0;
                    
                }

                var neve = "neve" + Convert.ToString(nevindex);
                ImageButton button = new ImageButton();
                string duma = ((string)item).ToLower();
                if (duma == "egyéb")
                {
                    duma = "other";
                }
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                button.Source = ImageSource.FromFile(ffile);
                int padding = Convert.ToInt16(Constans.ScreenWidth / 7/2);
                button.Padding = new Thickness(0, 10, 0, 10);
                button.BorderWidth = 0;
                button.BorderColor = Color.Gray;

                button.VerticalOptions = LayoutOptions.FillAndExpand;
                button.HorizontalOptions = LayoutOptions.FillAndExpand;
                button.Aspect = Aspect.AspectFill;
                listButtons.Add(button);
                myTomb.Add(Tuple.Create(idx, button) );

                button.Clicked += button_Clicked;
                foreach(var itemL in Constans.myLayout)
                {
                    if (itemL.Key == neve)
                    {
                        Debug.WriteLine("sl.Height");
                        StackLayout sl = (StackLayout)(itemL.Value);
                        Debug.WriteLine(sl.Height);
                        itemL.Value.Children.Add(button);
                        Debug.WriteLine(button.Height);
                        sl.MinimumHeightRequest = button.Height;
                        
                        Debug.WriteLine(sl.Height);

                    }
                }

        }
            foreach (var  itemL in Constans.myLayout)
            {
                myStack.Children.Add(itemL.Value);

                
            }
            myLayout.Children.Add(myScroll);
        }
        private void button_Clicked(object sender, EventArgs e)
        {
            ImageButton mostNyomi = (ImageButton)sender;
            foreach (var btn in myTomb)
            {
                if (btn.Item2.Id== mostNyomi.Id)
                {
                    mostNyomi.BorderWidth = 1;
                    mostNyomi.BorderColor = Color.Black;
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(btn.Item1);
                }
                else
                {
                    btn.Item2.BorderWidth = 0;
                }
            }
                /*foreach (ImageButton button in listButtons)
            {
                if (button.Id == mostNyomi.Id)
                {
                    button.BorderWidth = 1;
                    
                    Constans.valaszok = 
                }
                else
                {
                    button.BorderWidth = 0;
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