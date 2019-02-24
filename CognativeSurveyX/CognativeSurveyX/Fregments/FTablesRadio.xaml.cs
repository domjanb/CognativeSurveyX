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
	public partial class FTablesRadio : ContentPage
	{
        List<TablesRadio> listTablesRadio = new List<TablesRadio>();
        public FTablesRadio ()
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
            foreach (var item in Constans.aktQuestion.items)
            {
                idx++;
                TablesRadio button = new TablesRadio();
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

            Constans.valaszok = "";
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
            Constans.valaszok = Constans.valaszok.Substring(0, Constans.valaszok.Length);
        }

        
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }

    }
}