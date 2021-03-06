﻿using CognativeSurveyX.Modell;
using LabelHtml.Forms.Plugin.Abstractions;
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
	public partial class FKepes : ContentPage
	{
        List<ImageButton> listButtons = new List<ImageButton>();
        public FKepes()
		{
			InitializeComponent ();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            myLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            myLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            myStack.VerticalOptions= LayoutOptions.FillAndExpand;
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
            
            int itemDb = Constans.aktQuestion.choices.Count;
            var regForm2 = new Grid();
            regForm2.HorizontalOptions = LayoutOptions.FillAndExpand;
            regForm2.VerticalOptions = LayoutOptions.FillAndExpand;
            regForm2.Padding = 15;
            
            regForm2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //regForm2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            for (var i = 0; i < itemDb / 2; i++)
            {
                regForm2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                //regForm2.RowDefinitions.Add(new RowDefinition { Height =  GridLength.Auto });
            }
            /*for (var i = 0; i < 1 ; i++)
            {
                //regForm2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(i, GridUnitType.Star) });
                //regForm2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                regForm2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
            }*/
            int sor = -1;
            int idx = 0;
            var oszlop = 0;
            foreach (var item in Constans.aktQuestion.choices)
            {
                
                idx++;
                oszlop = 1;
                if (idx % 2 == 1)
                {
                    sor++;
                    oszlop = 0;
                }
                ImageButton button = new ImageButton();
                string duma = ((string)item).ToLower();
                if (duma=="egyéb")
                {
                    duma = "other";
                }
                string ffile= Path.Combine( Constans.myFilePath , duma.ToLower() + "_logo.png"  );
                button.HorizontalOptions = LayoutOptions.FillAndExpand;
                button.VerticalOptions= LayoutOptions.FillAndExpand;
                button.Source= ImageSource.FromFile(ffile);
                button.Aspect = Aspect.AspectFill;
                button.VerticalOptions = LayoutOptions.FillAndExpand;
                button.HorizontalOptions = LayoutOptions.Fill;
                
                Debug.WriteLine(button.Height);
                Debug.WriteLine(button.HeightRequest);
                /*Image im = new Image();
                im.Source = ImageSource.FromFile(ffile);

                Debug.WriteLine(im.Width);
                Debug.WriteLine(im.Bounds.Width);*/
                //button.
                button.BackgroundColor = Color.Red;
                button.BorderWidth = 1;
                button.BorderColor = Color.Black;
                //button.Measure()
                
                
                //button.Text = Constans.bumbuc_false + "  " + item;
                //button.HorizontalOptions = LayoutOptions.Start;

                //button.BackgroundColor = Color.Red;
                //button.BackgroundColor = Color.Transparent;
                listButtons.Add(button);
                //button.Opacity = 1;
                button.Clicked += button_Clicked;
                regForm2.Children.Add(button,  oszlop,sor);
                
                
            }

            myStack.Children.Add(regForm2);
            //regForm2.RowDefinitions[1].Height =listButtons[1].Height;
            //regForm2.RowDefinitions[2].Height = new GridLength(2, GridUnitType.Star);
            myLayout.Children.Add(myScroll);
            this.Appearing += FKepes_Appearing;
            
        }

        private void FKepes_Appearing(object sender, EventArgs e)
        {
            //FKepes a=(Fkepes)sender ;
            //foreach(var item in (ContentPage)sender)
            {

            }
            //Debug.WriteLine(a);
            //Debug.WriteLine(a);

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