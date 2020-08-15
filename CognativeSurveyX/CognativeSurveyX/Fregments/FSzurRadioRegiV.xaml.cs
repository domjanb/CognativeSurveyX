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
	public partial class FSzurRadioRegiV : ContentPage
	{
        UsersDataAccess adatBazis = new UsersDataAccess();
        List<RadioButton> listCheckbox = new List<RadioButton>();
        Label uzeno = new Label();
        Entry szuroMezo = new Entry();
        StackLayout myStack2 = new StackLayout();
        IEnumerable<Kozponti>  kozpontiAdatok;
        
        public FSzurRadioRegiV()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myScroll.Content = myStack;


            Label sorszam = new Label();
            sorszam.Margin = new Thickness(1, 1, 1, 1);
            sorszam.Text = Constans.sorszamErtek();
            sorszam.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
            myStack.Children.Add(sorszam);

            HtmlLabel kerdes = new HtmlLabel();
            kerdes.Text = Constans.ParamErtekeBeilleszt(Constans.aktQuestion.question_title);
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

            uzeno.Text = "";
            uzeno.TextColor = Color.Red;
            uzeno.IsVisible = false;
            myStack.Children.Add(uzeno);

            
            szuroMezo.Placeholder = "Ide ird az adatot";
            
            szuroMezo.IsVisible = true;
            szuroMezo.Margin = new Thickness(10, 0, 10, 0);
            szuroMezo.TextChanged += szuroMezo_TextChanged;
            myStack.Children.Add(szuroMezo);

            //IEnumerable<Kozponti> kozpontiAdatok = adatBazis.GetKozpontiAsSzur(Constans.aktQuestion.choices[0]) ;
            kozpontiAdatok = adatBazis.GetKozpontiAsSzur(Constans.aktQuestion.choices[0]);
            //Debug.WriteLine("szuromezo:" + Constans.aktQuestion.choices[0]);
            //Debug.WriteLine("kozpontidarab:" + kozpontiAdatok.Count().ToString());
            //Debug.WriteLine(kozpontiAdatok.Count());
            if (kozpontiAdatok.Count()<10)
            {
                foreach (var item in kozpontiAdatok)
                {
                    //Debug.WriteLine("kozponti valasz:" + item.valasz);
                    RadioButton button = new RadioButton();
                    button.Text = item.valasz;
                    button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    button.BackgroundColor = Color.Transparent;
                    int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                    button.Padding = new Thickness(padding, 0, padding, 0);
                    listCheckbox.Add(button);
                    button.IsVisible = true;
                    //button.Opacity = 1;
                    button.CheckedChange += Button_CheckedChange;
                    myStack.Children.Add(button);
                }
            }
            
            if (Constans.aktQuestion.choices.Count < (-30))
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
            //Debug.WriteLine(kozpontiAdatok.Count());

            Entry szuro = (Entry)sender;
            uzeno.Text = "";
            uzeno.IsVisible = false;
            if (szuro.Text.Count() > 2)
            {
                string duma = szuro.Text;
                string kisbetus = duma.ToLower();
                int allDarab = 0;
                int joDarab = 0;
                foreach (var item in kozpontiAdatok)
                {
                    allDarab++;
                    string kisbetus2 = item.valasz.ToLower();
                    //Debug.WriteLine(kisbetus2);
                    //Debug.WriteLine(kisbetus2.IndexOf(kisbetus));
                    if (kisbetus2.IndexOf(kisbetus) >=0 )
                    {
                        Debug.WriteLine("van");
                        joDarab++;
                    }
                }
                uzeno.Text = Convert.ToString(joDarab) + "/" + Convert.ToString(allDarab);
                uzeno.IsVisible = true;
                if (joDarab < 15)
                {
                    if (kozpontiAdatok.Count() < 10)
                    {
                        //int idx = 0;
                        foreach (var item in listCheckbox)
                        {

                            string duma3 = item.Text;
                            string kisbetus3 = duma3.ToLower();
                            //Debug.WriteLine(kisbetus3);
                            //Debug.WriteLine(kisbetus3.IndexOf(kisbetus));
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
                        foreach (var item in kozpontiAdatok)
                        {
                            idx++;
                            string kisbetus2 = item.valasz.ToLower();
                            //Debug.WriteLine(kisbetus2);
                            //Debug.WriteLine(kisbetus2.IndexOf(kisbetus));
                            if (kisbetus2.IndexOf(kisbetus) >= 0)
                            {
                                RadioButton button = new RadioButton();
                                button.Text = item.valasz;
                                //button.HorizontalOptions = LayoutOptions.Start;
                                //button.FontSize = "Large";
                                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                button.BackgroundColor = Color.Transparent;
                                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                                button.Padding = new Thickness(padding, 0, padding, 0);
                                
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
            szuroMezo.Text = "";
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

                //if (item.Id == ((RadioButton)sender).Id)
                //{
                //    if (item.IsChecked)
                //    {
                //        item.IsChecked = false;
                //    }
                //    else
                //    {
                //        item.IsChecked = true;
                //        Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(e);
                //    }

                //}
                //else
                //{
                //    item.IsChecked = false;
                //}


                //if (item.Id == ((RadioButton)sender).Id)
                //{
                //    if (!item.IsChecked)
                //    {
                //        item.myIschecked = true;
                //        Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + Convert.ToString(e);
                //    }
                //    else
                //    {
                //        item.myIschecked = false;
                //        Constans.valaszok = "";
                //    }

                //}
                //else
                //{
                //    item.myIschecked = false;
                //}


            }
            ((RadioButton)sender).enModositok = false;
            Debug.WriteLine("Nyomi:" + ((RadioButton)sender).Text);
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {
            var folytat = true;
            string szoveg = szuroMezo.Text;
            
            if (szoveg!=null)
            {
                if (szoveg.Length > 0)
                {
                    szoveg = szoveg.Replace(";", ",");
                }
                else
                {
                    szoveg = "";
                }

            }
            else
            {
                szoveg = "";
            }
            bool voltPipa = false;
            string pipaltSzoveg = "";
            uzeno.IsVisible = false;
            uzeno.Text = "";
            foreach (var item in listCheckbox)
            {
                Debug.WriteLine(item.Text.Replace(";", ",").ToLower() + " ?=?" +szoveg.ToLower());
                
                if (item.Text.Replace(";", ",").ToLower() == szoveg.ToLower())
                {
                    Debug.WriteLine("egyenlo");
                    uzeno.Text = "Volt már ilyen név";
                    uzeno.IsVisible = true;
                    folytat = false;
                }
                if (item.IsChecked)
                {
                    voltPipa = true;
                    pipaltSzoveg = item.Text;
                    
                    //break;
                }
            }
            
            if (folytat) {
                if (szoveg.Length > 0)
                {
                    //Entry szuro = (Entry)szuroMezo;

                    Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + szoveg;
                    Constans.valaszokKozponti = "Ismerosok" + "=" + szoveg;
                }
                else
                {
                    if (voltPipa)
                    {
                        Constans.valaszok = Constans.aktQuestion.kerdeskod + "=" + pipaltSzoveg;
                    }

                }
                if (voltPipa || szoveg.Length > 0)
                {
                    if (!uzeno.IsVisible)
                    {
                        Constans.nextPage();
                        Navigation.PushModalAsync(new FPage());
                    }
                }
            }
            
            
            
        }

    }
}