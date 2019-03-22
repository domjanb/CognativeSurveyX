using CognativeSurveyX.Modell;
using LabelHtml.Forms.Plugin.Abstractions;
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
	public partial class FNumbers : ContentPage
	{
		public FNumbers ()
		{
			InitializeComponent ();
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

            Entry lblDuma = new Entry();
            lblDuma.Keyboard = Keyboard.Numeric;
            lblDuma.TextChanged += LblDuma_TextChanged;
            myStack.Children.Add(lblDuma);
            


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