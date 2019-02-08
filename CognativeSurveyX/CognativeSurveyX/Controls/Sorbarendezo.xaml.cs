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
	public partial class Sorbarendezo  : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(Sorbarendezo),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Sorbarendezo)bindable).textLabel.Text = (string)newValue;
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
        public static readonly BindableProperty SorszamProprty =
            BindableProperty.Create(
                "SorszamText",
                typeof(string),
                typeof(Sorbarendezo),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Sorbarendezo)bindable).sorszamLabel.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(Sorbarendezo),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Sorbarendezo)bindable).textLabel.FontSize = (double)newValue;
                    ((Sorbarendezo)bindable).sorszamLabel.FontSize = (double)newValue;
                    ((Sorbarendezo)bindable).textOther.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(Sorbarendezo),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    //((Gomb)bindable).boxLabel.Text = (bool)newValue ? "" : "";
                    //((Gomb)bindable).myFrame.BackgroundColor = (bool)newValue ? Color.White : Color.Aqua;
                    //((Gomb)bindable).myFrame.CornerRadius = (bool)newValue ? 0 : 20;
                    ((Sorbarendezo)bindable).CheckedChange?.Invoke(((Sorbarendezo)bindable), (bool)newValue);
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
                    ((Sorbarendezo)bindable).CheckedChange?.Invoke(((Sorbarendezo)bindable), (bool)newValue);
                }
                );


        public event EventHandler<bool> CheckedChange;
        public event EventHandler<TextChangedEventArgs> EntryChange;
        public Sorbarendezo ()
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
        public bool _myIschecked;
        public bool myIschecked
        {
            get { return _myIschecked; }
            set
            {
                this._myIschecked = value;
                //myFrame.BackgroundColor = _myIschecked ? Color.White : Color.Aqua;
                myFrame.CornerRadius = _myIschecked ? 0 : 20;
                sorszamLabel.IsVisible = _myIschecked ? true : false;
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
        public string SorszamText
        {
            set { SetValue(SorszamProprty, value); }
            get { return (string)GetValue(SorszamProprty); }
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