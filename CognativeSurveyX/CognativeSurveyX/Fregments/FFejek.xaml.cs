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
	public partial class FFejek : ContentPage
	{
		public FFejek ()
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

            
            Fejek5 button = new Fejek5();

            button.EntryChange += Button_EntryChange;
            button.CheckedChange += Button_CheckedChange;
            //button.CheckedChange += button_CheckedChange;
            //button.Text = item;

            //button.BackgroundColor = Color.AntiqueWhite;
             myStack.Children.Add(button);
            
            myLayout.Children.Add(myScroll);
        }

        private void Button_CheckedChange(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Nyomi:" + ((Fejek5)sender).Value + " - " + Convert.ToString(e));
            Debug.WriteLine("valami volt..");
            Fejek5 button = (Fejek5)sender;
            Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(button.Value);
        }

        private void Button_EntryChange(object sender, TextChangedEventArgs e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("valami volt..");
        }

        /*private void button_CheckedChange(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Nyomi:" + ((TablesRadio)sender).Text + Convert.ToString(e));

        }*/


        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}