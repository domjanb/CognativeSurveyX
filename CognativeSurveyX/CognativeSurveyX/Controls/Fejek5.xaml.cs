using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Fejek5 : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(Fejek5),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Fejek5)bindable).textLabel.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(Fejek5),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    Fejek5 checkbox = (Fejek5)bindable;
                    //((Fejek5)bindable).boxLabel.Text = (bool)newValue ? "\u2611" : "\u2610";
                    //checkbox.CheckedChanged?.Invoke(checkbox, (bool)newValue);
                    ((Fejek5)bindable).CheckedChange?.Invoke(checkbox, (int)newValue);
                }
                );
        public event EventHandler<int> CheckedChange;
        public event EventHandler<TextChangedEventArgs> EntryChange;
        public Fejek5 ()
		{
			InitializeComponent ();
		}
        public int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                this._Value = value;

            }

        }
        public string Text
        {
            set { SetValue(TextProprty, value); }
            get { return (string)GetValue(TextProprty); }
        }
        public bool IsChecked
        {
            set { SetValue(IsCheckedProperty, value); }
            get { return (bool)GetValue(IsCheckedProperty); }
        }
        void OnCheckBoxTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }
        private void OnTapped(object sender, EventArgs e)
        {
            bw1.Opacity = 0;
            bw2.Opacity = 0;
            bw3.Opacity = 0;
            bw4.Opacity = 0;
            bw5.Opacity = 0;
            StackLayout sl = (StackLayout)sender;
            if (sl.Id == fej1.Id)
            {
                _Value = 1;
                bw1.Opacity = 0.101;
            }
            else if (sl.Id == fej2.Id)
            {
                _Value = 2;
                bw2.Opacity = 0.101;
            }
            else if (sl.Id == fej3.Id)
            {
                _Value = 3;
                bw3.Opacity = 0.101;
            }
            else if (sl.Id == fej4.Id)
            {
                _Value = 4;
                bw4.Opacity = 0.101;
            }
            else if (sl.Id == fej5.Id)
            {
                _Value = 5;
                bw5.Opacity = 0.101;
            }

            CheckedChange?.Invoke(this, (int)Value);

        }
    }
}