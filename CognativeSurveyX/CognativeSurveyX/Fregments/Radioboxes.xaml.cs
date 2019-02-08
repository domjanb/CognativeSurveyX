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
	public partial class Radioboxes : ContentPage
	{
        //string bumbuc_false = "\U0001F518";
        //string bumbuc_true = "\U000123FA";

        //string bumbuc_false = "\U000125CB";
        //string bumbuc_true = "\U000125C9";

        
        Color semiTransparentColor = new Color(0, 0, 0, 0.9);
        List<Button> listButtons = new List<Button>();
        //List<MyCheckBox> listmyButtons = new List<MyCheckBox>();
        List<Checkbox> listCheckbox = new List<Checkbox>();
        public Radioboxes ()
		{
			InitializeComponent ();
            //AbsoluteLayoutWithDisplayInfoPage();

            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            myStack.Children.Add(kerdes);


            foreach (var item in Constans.aktQuestion.choices)
            {
                /*Button button = new Button();
                button.Text = Constans.bumbuc_false + "  "+item;
                button.HorizontalOptions= LayoutOptions.Start;
                //button.FontSize = "Large";
                button.Font = Font.SystemFontOfSize(NamedSize.Small);
                button.BackgroundColor = Color.Transparent;
                
                listButtons.Add(button);
                //button.Opacity = 1;
                button.Clicked += button_Clicked;
                myStack.Children.Add(button);*/
            }


            /*foreach (var item in Constans.aktQuestion.choices)
            {
                MyCheckBox button = new MyCheckBox();
                button.text = Constans.bumbuc_false + "  " + item;
                button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.font= Font.SystemFontOfSize(NamedSize.Small);
                button.BackgroundColor = Color.Transparent;

                listmyButtons.Add(button);
                //button.Opacity = 1;
                button.Clicked += mybutton_Clicked;
                myStack.Children.Add(button);
            }*/
            foreach (var item in Constans.aktQuestion.choices)
            {
                Checkbox button = new Checkbox();
                button.Text = Constans.bumbuc_false + "  " + item;
                button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;

                listCheckbox.Add(button);
                //button.Opacity = 1;
                button.CheckedChange += Button_CheckedChange;
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);

           /* MyCheckBox mc = new MyCheckBox();
            //mc.IsCheckedChanged += Mc_IsCheckedChanged;
            //mc.IsCheckedChanged += Mc_IsCheckedChanged1;
            mc.ertesito += fgv();
            //mc.ertesito2 += ertesiti2();
            mc.Ertesoto3 += Mc_Ertesoto3;
            mc.Ertesoto4 += Mc_Ertesoto4;
            mc.isCheckedSend += myCheckboxisChecked;
            
            mc.label = "bebebebebe";
            myLayout.Children.Add(mc);
            Debug.WriteLine("kezdodik");*/

            /*myCheckbox cb = new myCheckbox();
            cb.Shape = Shape.Rectangle;
            cb.IsCheckedChanged += Cb_IsCheckedChanged;
            myLayout.Children.Add(cb);

            myCheckbox cb2 = new myCheckbox();
            cb2.Shape=Shape.Circle;
            cb2.IsCheckedChanged += Cb_IsCheckedChanged;
            myLayout.Children.Add(cb2);*/





        }

        private void Button_CheckedChange(object sender, bool e)
        {
            //throw new NotImplementedException();
        }

        private void mybutton_Clicked(object sender, int e)
        {
            /*MyCheckBox mostNyomi = (MyCheckBox)sender;
            foreach (MyCheckBox button in listmyButtons)
            {
                string eleje = button.text;
                string eleje2 = Convert.ToString(eleje.ElementAt(0));
                string vege = eleje.Substring(1, eleje.Length - 1);
                if (button.Id == mostNyomi.Id)
                {
                    button.text = Constans.bumbuc_true + vege;
                }
                else
                {
                    button.text = Constans.bumbuc_false + vege;
                }
            }*/
        }

        /*private void myCheckboxisChecked(object sender, int e)
        {
            //throw new NotImplementedException();
        }

        private void Mc_Ertesoto4(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("NNNNNYYYYYYEEEERRRRRRT4");
            Debug.WriteLine(sender);
            Debug.WriteLine(e);
        }


        private void Mc_Ertesoto3(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("NNNNNYYYYYYEEEERRRRRRT3");
            Debug.WriteLine(sender);
            Debug.WriteLine(e);
        }

        private MyCheckBox.Ertesito fgv()
        {
            //throw new NotImplementedException();
            Debug.WriteLine("NNNNNYYYYYYEEEERRRRRRT");
            return null;
        }

        private void Mc_IsCheckedChanged1(object sender, string e)
        {
            Debug.WriteLine("NNNNNYYYYYYEEEERRRRRRT");
            //throw new NotImplementedException();
        }

        private void Mc_IsCheckedChanged(object sender, MyCheckBox e)
        {
            Debug.WriteLine("NNNNNYYYYYYEEEERRRRRRT");
            //throw new NotImplementedException();
        }*/

        private void button_Clicked(object sender, EventArgs e)
        {
            /*Button mostNyomi=(Button)sender;
            foreach(Button button in listButtons)
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
            }


            Debug.WriteLine("nyomi");*/


        }
        

        /*private void Cb_IsCheckedChanged(object sender, TappedEventArgs e)
        {
            if (e.Parameter.Equals(true))
            {
                Debug.WriteLine("valami volt - true");
            }
            else
            {
                Debug.WriteLine("valami volt - false");
            }
            

        }*/
        private void _Continue_Clicked(object sender, EventArgs e)
        {
            
            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
        public void AbsoluteLayoutWithDisplayInfoPage(IDisplay display)
        {
            var inchX = display.WidthRequestInInches(1);
            var inchY = display.HeightRequestInInches(1);
            var ww = display.Width;
            var ww3 = this.Width;
        }
    }
}