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
	public partial class FSkala : ContentPage
	{
		public FSkala ()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            
            
            var idx = 0;
            foreach (var item in Constans.aktQuestion.choices)
            {
                idx++;
                CsillagSkala button = new CsillagSkala();
                button.ValaszDB = 7;
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

                //listCheckbox.Add(button);
                //button.Opacity = 1;
                //button.CheckedChange += Button_CheckedChange;
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);
        }

        private void button_CheckedChange(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Nyomi:" + ((CsillagSkala)sender).Text + Convert.ToString(e));
            Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(e);

        }


        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}