﻿using CognativeSurveyX.Controls;
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
	public partial class FSorbarendezo : ContentPage
	{
        List<Sorbarendezo> listButtons = new List<Sorbarendezo>();
        List<int> sorszamTomb = new List<int>();
        public FSorbarendezo ()
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

            int idx = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                idx++;
                Sorbarendezo button = new Sorbarendezo();
                string buttonDuma = item;
                if (item.Substring(item.Length - 2, 2) == ";O")
                {
                    button.KellEOther = true;
                    buttonDuma = item.Substring(0, item.Length - 2-1);
                }
                button.Text = buttonDuma;
                //button.HorizontalOptions = LayoutOptions.Start;
                //button.FontSize = "Large";
                button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                button.BackgroundColor = Color.Transparent;
                if (!Constans.aktQuestion.choicesVisible[idx])
                {
                    button.IsVisible = false;
                }
                listButtons.Add(button);

                //button.Opacity = 1;
                button.CheckedChange += Button_CheckedChange;
                button.EntryChange += Button_EntryChange;
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);
        }

        private void Button_EntryChange(object sender, TextChangedEventArgs e)
        {
            
            Sorbarendezo mostNyomi = (Sorbarendezo)sender;
            Debug.WriteLine(mostNyomi.TextOther);
            Constans.valaszok = "";
            if (mostNyomi.TextOther.Length > 0)
            {
                if (!mostNyomi.myIschecked)
                {
                    int ujszam = 1;
                    if (sorszamTomb.Count > 0)
                    {
                        ujszam = sorszamTomb.Last() + 1;
                    }
                    sorszamTomb.Add(ujszam);
                    mostNyomi.myIschecked = true;
                    mostNyomi.SorszamText = Convert.ToString(ujszam);
                }
            }
            else
            {
                /*if (mostNyomi.myIschecked)
                {
                    int inntettoriol = Convert.ToInt16(mostNyomi.SorszamText);
                    int maxi = sorszamTomb.Last();
                    for (int i = inntettoriol; i <= maxi; i++)
                    {
                        sorszamTomb.Remove(i);
                    }
                    foreach (Sorbarendezo item in listButtons)
                    {
                        int aktsorszam = Convert.ToInt16(item.SorszamText);
                        if (aktsorszam >= inntettoriol)
                        {
                            item.SorszamText = "0";
                            item.myIschecked = false;
                        }

                    }
                }*/
            }
            int idxi = 0;
            foreach (Sorbarendezo item in listButtons)
            {
                idxi++;
                if (item.myIschecked)
                {
                    string otherDuma = "";
                    if (item.KellEOther)
                    {
                        otherDuma = Constans.aktQuestion.kerdeskod + "other_" + Convert.ToString(idxi) + "=" + Convert.ToString(Constans.kipofoz(item.TextOther)) + ";";
                    }

                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(idxi) + "=" + Convert.ToString(item.SorszamText) + ";" + otherDuma; ;
                }
            }

        }

        private void Button_CheckedChange(object sender, bool e)
        {
            Debug.WriteLine("nyomi");
            Sorbarendezo mostNyomi = (Sorbarendezo)sender;
            Constans.valaszok = "";
            if (mostNyomi.myIschecked)
            {
                int inntettoriol = Convert.ToInt16(mostNyomi.SorszamText);
                int maxi = sorszamTomb.Last();
                for (int i= inntettoriol; i <= maxi;i++)
                {
                    sorszamTomb.Remove(maxi);
                }
                mostNyomi.myIschecked = false;
                mostNyomi.SorszamText = "0";
                int idx = 0;
                foreach (Sorbarendezo item in listButtons)
                {
                    idx++;
                    if (item.myIschecked)
                    {
                        int aktsorszam = Convert.ToInt16(item.SorszamText);
                        if (aktsorszam > inntettoriol)
                        {
                            item.SorszamText = Convert.ToString(Convert.ToInt16(aktsorszam) - 1);
                            //item.myIschecked = false;
                        }
                        

                    }
                    
                        
                }

            }
            else
            {
                foreach (Sorbarendezo button in listButtons)
                {

                    if (button.Id == mostNyomi.Id)
                    {
                        button.myIschecked = true;
                        int ujszam = 1;
                        if (sorszamTomb.Count > 0)
                        {
                            ujszam = sorszamTomb.Last() + 1;
                        }
                        sorszamTomb.Add(ujszam);
                        button.myIschecked = true;
                        button.SorszamText = Convert.ToString(ujszam);
                    }
                    else
                    {
                        //button.myIschecked = false;
                    }
                }
            }
            int idxi = 0;
            foreach (Sorbarendezo item in listButtons)
            {
                idxi++;
                if (item.myIschecked)
                {
                    string otherDuma = "";
                    if (item.KellEOther)
                    {
                        otherDuma = Constans.aktQuestion.kerdeskod + "other_" + Convert.ToString(idxi) + "=" + Convert.ToString(Constans.kipofoz(item.TextOther)) + ";";
                    }

                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(idxi) + "=" + Convert.ToString(item.SorszamText) + ";" + otherDuma; ;
                }
            }



        }
        private async void _Continue_Clicked(object sender, EventArgs e)
        {
            var x =  szuinez((Button)sender);
            ((Button)sender).BackgroundColor = Color.Red;
            ai.IsEnabled = true;
            ai.IsVisible = true;
            ai.IsRunning = true;
            
            
            
        }
        
        private async Task<bool> szuinez(Button btn)
        {
            
            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
            return true;
        }

    }
}