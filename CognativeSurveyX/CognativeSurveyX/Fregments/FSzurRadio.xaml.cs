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
	public partial class FSzurRadio : ContentPage
	{
        List<RadioButton> listCheckbox = new List<RadioButton>();
        Label uzeno = new Label();
        StackLayout myStack2 = new StackLayout();
        public FSzurRadio ()
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

            uzeno.Text = "";
            uzeno.TextColor = Color.Red;
            uzeno.IsVisible = false;
            myStack.Children.Add(uzeno);

            Entry szuroMezo = new Entry();
            szuroMezo.Placeholder = "Ide ird az adatot";
            
            szuroMezo.IsVisible = true;
            szuroMezo.Margin = new Thickness(10, 0, 10, 0);
            szuroMezo.TextChanged += szuroMezo_TextChanged;
            myStack.Children.Add(szuroMezo);

            if (Constans.aktQuestion.choices.Count < 30)
            {
                int idx = 0;
                foreach (var item in Constans.aktQuestion.choices)
                {
                    idx++;
                    RadioButton button = new RadioButton();
                    button.Text = item;
                    //button.HorizontalOptions = LayoutOptions.Start;
                    //button.FontSize = "Large";
                    button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    button.BackgroundColor = Color.Transparent;
                    int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                    button.Padding = new Thickness(padding, 0, padding, 0);
                    if (!Constans.aktQuestion.choicesVisible[idx - 1])
                    {
                        button.IsVisible = false;
                    }
                    listCheckbox.Add(button);
                    button.IsVisible = false;
                    //button.Opacity = 1;
                    button.CheckedChange += Button_CheckedChange;
                    myStack.Children.Add(button);
                }
            }
            /*Label kerdes2 = new Label();
            kerdes2.Text = "lalalallala";
            kerdes2.BackgroundColor = Color.Chocolate;
            myStack2.Children.Add(kerdes2);
            myStack2.Margin = new Thickness(10, 100, 10, 100);
            myStack2.Padding= new Thickness(10, 100, 10, 100);*/
            myStack.Children.Add(myStack2);
            myLayout.Children.Add(myScroll);
        }

        private void szuroMezo_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry szuro = (Entry)sender;
            if (szuro.Text.Count() > 2)
            {
                string duma = szuro.Text;
                string kisbetus = duma.ToLower();
                int allDarab = 0;
                int joDarab = 0;
                foreach (var item in Constans.aktQuestion.choices)
                {
                    allDarab++;
                    string kisbetus2 = item.ToLower();
                    //Debug.WriteLine(kisbetus2);
                    //Debug.WriteLine(kisbetus2.IndexOf(kisbetus));
                    if (kisbetus2.IndexOf(kisbetus) >=0 )
                    {
                        joDarab++;
                    }
                }
                uzeno.Text = Convert.ToString(joDarab) + "/" + Convert.ToString(allDarab);
                uzeno.IsVisible = true;
                if (joDarab < 15)
                {
                    if (Constans.aktQuestion.choices.Count < 30)
                    {
                        int idx = 0;
                        foreach (var item in listCheckbox)
                        {

                            string duma3 = item.Text;
                            string kisbetus3 = duma3.ToLower();
                            Debug.WriteLine(kisbetus3);
                            Debug.WriteLine(kisbetus3.IndexOf(kisbetus));
                            if (kisbetus3.IndexOf(kisbetus) >= 0)
                            {
                                item.IsVisible = true;
                            }

                        }
                    }
                    else
                    {
                        myStack2.Children.Clear();
                        int idx = 0;
                        foreach (var item in Constans.aktQuestion.choices)
                        {
                            idx++;
                            string kisbetus2 = item.ToLower();
                            //Debug.WriteLine(kisbetus2);
                            //Debug.WriteLine(kisbetus2.IndexOf(kisbetus));
                            if (kisbetus2.IndexOf(kisbetus) >= 0)
                            {
                                RadioButton button = new RadioButton();
                                button.Text = item;
                                //button.HorizontalOptions = LayoutOptions.Start;
                                //button.FontSize = "Large";
                                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                button.BackgroundColor = Color.Transparent;
                                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                                button.Padding = new Thickness(padding, 0, padding, 0);
                                if (!Constans.aktQuestion.choicesVisible[idx - 1])
                                {
                                    button.IsVisible = false;
                                }
                                listCheckbox.Add(button);
                                //button.IsVisible = false;
                                //button.Opacity = 1;
                                button.CheckedChange += Button_CheckedChange;
                                myStack2.Children.Add(button);
                            }

                            

                        }
                    }
                        
                }
            }
        }

        private void Button_CheckedChange(object sender, bool e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("volt nyomi");
            ((RadioButton)sender).enModositok = true;
            int idx = 0;
            foreach (var item in listCheckbox)
            {
                idx++;
                if (item.Id == ((RadioButton)sender).Id)
                {
                    item.myIschecked = true;
                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(e);
                }
                else
                {
                    item.myIschecked = false;
                }

            }
            ((RadioButton)sender).enModositok = false;
            Debug.WriteLine("Nyomi:" + ((RadioButton)sender).Text);
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}