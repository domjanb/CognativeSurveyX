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
	public partial class Checkbox : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(Checkbox),
                propertyChanged: (bindable,oldValue,newValue) => 
                {
                    ((Checkbox)bindable).textLabel.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty TextOtherProprty =
            BindableProperty.Create(
                "TextOther",
                typeof(string),
                typeof(Sorbarendezo),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    //((Sorbarendezo)bindable).textOther. .textLabel.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(Checkbox),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Checkbox)bindable).textLabel.FontSize = (double)newValue;
                    ((Checkbox)bindable).boxLabel.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(Checkbox),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    Checkbox checkbox = (Checkbox)bindable;
                    ((Checkbox)bindable).boxLabel.Text = (bool)newValue ? "\u2611" : "\u2610";
                    //checkbox.CheckedChanged?.Invoke(checkbox, (bool)newValue);
                    ((Checkbox)bindable).CheckedChange?.Invoke(checkbox, (bool)newValue);
                }
                );
        public static readonly BindableProperty KellOtherProperty =
            BindableProperty.Create(
                "KellEOther",
                typeof(bool),
                typeof(Sorbarendezo),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    //((Gomb)bindable).boxLabel.Text = (bool)newValue ? "" : "";
                    //((Gomb)bindable).myFrame.BackgroundColor = (bool)newValue ? Color.White : Color.Aqua;
                    //((Gomb)bindable).myFrame.CornerRadius = (bool)newValue ? 0 : 20;
                    ((Checkbox)bindable).CheckedChange?.Invoke(((Checkbox)bindable), (bool)newValue);
                }
                );

        public event EventHandler<bool> CheckedChange;
        public event EventHandler<TextChangedEventArgs> EntryChange;
        public Checkbox ()
		{
			InitializeComponent ();
            
		}
        public bool _KellEOther;
        public bool KellEOther
        {
            get { return _KellEOther; }
            set
            {
                this._KellEOther = value;

                //otherSor.Height= _KellEOther ? GridLength(1,GridUnitType.Star) : 0;
                //otherSor.Height = _KellEOther ? 20 : 0;
                textOther.IsVisible = true;
            }

        }
        public string Text
        {
            set { SetValue(TextProprty, value); }
            get { return (string)GetValue(TextProprty); }
        }
        public string TextOther
        {
            set { SetValue(TextOtherProprty, value); }
            get { return (string)textOther.Text; }
        }
        public double FontSize
        {
            set { SetValue(FontSizeProperty, value); }
            get { return (double)GetValue(FontSizeProperty); }
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
        private void TextOther_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryChange?.Invoke(this, e);
        }
    }
}