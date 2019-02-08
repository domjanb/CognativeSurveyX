using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TablesRadioFejlec : ContentView
	{
        //List<string> Valaszok = new List<string>();
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Items",
                typeof(List<string>),
                typeof(TablesRadio),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    //((TablesRadio)bindable).lbl.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty valaszdbProperty =
            BindableProperty.Create(
                "ValaszDB",
                typeof(int),
                typeof(TablesRadioFejlec),
                defaultValue: 2,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var a = 2;
                    //frameInit();
                    //((Kinyilo)bindable).textLabel.FontSize = (double)newValue;
                    //((Kinyilo)bindable).boxLabel.FontSize = (double)newValue;
                }
                );

        
        Label lbl = new Label();
        Grid sor = new Grid();
        List<RadioButton> listCheckbox = new List<RadioButton>();
        List<BoxView> listGr = new List<BoxView>();
        public TablesRadioFejlec()
        {
            InitializeComponent();
            /*var sor = new Grid();
            
            */
            myLayout.Children.Add(sor);
        }
        public void frameInit()
        {
            myLayout.Children.Remove(sor);
            sor.Margin = new Thickness(2, 2, 2, 2);
            BoxView bwTop = new BoxView();
            bwTop.WidthRequest = 1;
            bwTop.BackgroundColor = Color.Black;
            bwTop.VerticalOptions = LayoutOptions.Fill;
            bwTop.HorizontalOptions = LayoutOptions.Fill;
            bwTop.Opacity = 0.01;
            //bw.VerticalOptions = LayoutOptions.Fill;
            sor.Children.Add(bwTop);

            sor.HorizontalOptions = LayoutOptions.FillAndExpand;
            sor.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            sor.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            for (var i = 1; i <= ValaszDB; i++)
            {
                sor.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            //lbl.Text = Text;
            //lbl.BackgroundColor = Color.Pink;
            sor.Children.Add(lbl);
            sor.Children.Add(bwTop);

            for (var i = 0; i < ValaszDB; i++)
            {
                TapGestureRecognizer gr = new TapGestureRecognizer();
                gr.Tapped += Gr_Tapped;
                //listGr.Add(gr);

                Label rb = new Label();
                //rb.BackgroundColor = Color.Peru;
                rb.HorizontalOptions = LayoutOptions.Center;
                rb.VerticalOptions = LayoutOptions.Center;
                rb.Text = Items[i];
                BoxView bwTop2 = new BoxView();
                bwTop2.WidthRequest = 1;
                bwTop2.BackgroundColor = Color.Black;
                bwTop2.VerticalOptions = LayoutOptions.Fill;
                bwTop2.HorizontalOptions = LayoutOptions.Fill;
                bwTop2.Opacity = 0.01;
                bwTop2.GestureRecognizers.Add(gr);
                listGr.Add(bwTop2);
                //bw.VerticalOptions = LayoutOptions.Fill;

                sor.Children.Add(rb, i + 1, 0);
                sor.Children.Add(bwTop2, i + 1, 0);
            }

            myLayout.Children.Add(sor);
        }

        private void Gr_Tapped(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Debug.WriteLine("valami vót");
            Debug.WriteLine(sender.GetType());
            int idx = -1;
            foreach (var item in listGr)
            {
                idx++;
                if (sender == item)
                {

                    int idx2 = -1;
                    foreach (var item2 in listCheckbox)
                    {
                        idx2++;
                        if (idx2 == idx)
                        {
                            item2.IsChecked = !item2.IsChecked;
                        }

                    }
                    break;
                }
            }
        }


        public List<string> _Items;
        public List<string> Items
        {
            get { return _Items; }
            set
            {
                this._Items = value;
                ValaszDB = Items.Count();
                frameInit();

            }

        }
        /*public string Valaszok
        {
            set { SetValue(TextProprty, value); }
            get { return (string)GetValue(TextProprty); }
        }*/




        public int ValaszDB
        {
            set
            {
                SetValue(valaszdbProperty, value);
                //frameInit();
            }
            get { return (int)GetValue(valaszdbProperty); }
        }
    }
}