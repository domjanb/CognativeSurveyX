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
	public partial class FTablesRadio : ContentPage
	{
        List<TablesRadio> listTablesRadio = new List<TablesRadio>();
        public static List<Tuple<int, string, TablesRadio>> mySortTomb = new List<Tuple<int, string, TablesRadio>>();
        public FTablesRadio ()
		{
			InitializeComponent ();
            mySortTomb.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.items)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(index +1), item, new TablesRadio()));
            }

            if (Constans.aktQuestion.random_items == true)
            {
                var rand = new Random();
                for (var i = 1; i < index; i++)
                {

                    int random1 = rand.Next(0, index + 1);
                    int random2 = rand.Next(0, index + 1);
                    if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random1].Item2)))
                    {
                        random1 = index + 1000;
                    }
                    else if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random2].Item2)))
                    {
                        random2 = index + 1000;
                    }
                    if (random1 != random2 && random1 <= index && random2 <= index)
                    {
                        bool kell = true;
                        if (mySortTomb[random1].Item2.Length > 3)
                        {

                            if (mySortTomb[random1].Item2.ToLower().Substring(mySortTomb[random1].Item2.Length - 2, 2) == "-r") { kell = false; }
                        }
                        if (mySortTomb[random2].Item2.Length > 3)
                        {
                            if (mySortTomb[random2].Item2.ToLower().Substring(mySortTomb[random2].Item2.Length - 2, 2) == "-r") { kell = false; }
                        }
                        if (kell)
                        {
                            var tmp = mySortTomb[random1];
                            mySortTomb[random1] = mySortTomb[random2];
                            mySortTomb[random2] = tmp;
                        }

                    }

                }
            }
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
            //string duma = Constans.aktQuestion.question_title;
            
            kerdes.Text = Constans.ParamErtekeBeilleszt(Constans.aktQuestion.question_title);
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);

           
            
            List<string> fejlec = new List<string>();
            foreach (var item in Constans.aktQuestion.choices)
            {
                fejlec.Add(item);
                
            }
            TablesRadioFejlec button2 = new TablesRadioFejlec();
            button2.Items = fejlec;
            //button2.BackgroundColor = Color.Red;
            myStack.Children.Add(button2);
            var idx = 0;
            var joszin = true;
            //foreach (var item in Constans.aktQuestion.items)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = Constans.ValaszParameterNelkul(itemTomb.Item2);
                TablesRadio button = itemTomb.Item3;
                idx++;
                
                button.ValaszDB = Constans.aktQuestion.choices.Count();
                button.CheckedChange += button_CheckedChange;
                button.Text = item;

                //button.FontSize = "Large";
                //button.BackgroundColor = Color.Transparent;
                if (!Constans.aktQuestion.itemVisible[idx - 1])
                {
                    button.IsVisible = false;
                }
                else
                {
                    joszin = !joszin;
                }
                //if (idx % 2 != 1)
                if (joszin)
                {
                    button.BackgroundColor = Color.AliceBlue;
                }
                else
                {
                    button.BackgroundColor = Color.AntiqueWhite;
                }

                //listCheckbox.Add(button);
                
                listTablesRadio.Add(button);
                //button.Opacity = 1;
                //button.CheckedChange += Button_CheckedChange;
                myStack.Children.Add(button);
            }
            myLayout.Children.Add(myScroll);
        }

        private void button_CheckedChange(object sender, int e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("Nyomi:" + ((TablesRadio)sender).Text + Convert.ToString(e));

            /*Constans.valaszok = "";
            var idx = 0;
            foreach(TablesRadio item in listTablesRadio)
            {
                if (item.Value != null)
                {
                    if (item.Value > 0)
                    {
                        Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(idx) + "=" + Convert.ToString(item.Value) + ";"; 
                    }
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);*/
        }

        
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.valaszok = "";
            var idx = 0;
            foreach (var itemTomb in mySortTomb)
            {
                var item = itemTomb.Item3;
                if (item.Value != null)
                {
                    if (item.Value > 0)
                    {
                        Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(itemTomb.Item1) + "=" + Convert.ToString(item.Value) + ";";
                    }
                }
            }
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}