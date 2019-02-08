using CognativeSurveyX.Modell;
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
	public partial class FString : ContentPage
	{
		public FString ()
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

            Entry lblDuma = new Entry();
            lblDuma.TextChanged += LblDuma_TextChanged;
            myStack.Children.Add(lblDuma);
            /*foreach (var item in Constans.aktQuestion.choices)
            {
                Button button = new Button();
                button.Text = Constans.bumbuc_false + "  " + item;
                button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.Font = Font.SystemFontOfSize(NamedSize.Small);
                button.BackgroundColor = Color.Transparent;
                listButtons.Add(button);
                //button.Opacity = 1;
                button.Clicked += button_Clicked;
                myStack.Children.Add(button);
            }*/


            myLayout.Children.Add(myScroll);
        }

        private void LblDuma_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry button = (Entry)sender;
            Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(button.Text);
            var a = 2;
        }

        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}