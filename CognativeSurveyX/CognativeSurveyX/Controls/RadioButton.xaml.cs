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
	public partial class RadioButton : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(RadioButton),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RadioButton)bindable).textLabel.Text = (string)newValue;
                    if (((string)newValue).Length == 0)
                    {
                        ((RadioButton)bindable).textLabel.IsVisible = false;

                    }
                    else
                    {
                        ((RadioButton)bindable).textLabel.IsVisible = true;
                    }
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
                typeof(RadioButton),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RadioButton)bindable).textLabel.FontSize = (double)newValue;
                    ((RadioButton)bindable).boxLabel.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(RadioButton),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RadioButton)bindable).boxLabel.Text = (bool)newValue ? "⚫" : "⚪";
                    
                    ((RadioButton)bindable).CheckedChange?.Invoke(((RadioButton)bindable), (bool)newValue);
                }
                );
        
        
        
        public event EventHandler<bool> CheckedChange;
        public event EventHandler<TextChangedEventArgs> EntryChange;
        public bool _myIschecked;
        public bool myIschecked
        {
            get { return _myIschecked; }
            set
            {
                this._myIschecked = value;
                boxLabel.Text = (bool)_myIschecked ? "⚫" : "⚪"; 
                //CheckedChange?.Invoke(this, (bool)_myIschecked);

                //myFrame.BackgroundColor = _myIschecked ? Color.White : Color.Aqua;
                //myFrame.CornerRadius = _myIschecked ? 0 : 20;
            }

        }

        public bool _enModositok;
        public bool enModositok
        {
            get { return _enModositok; }
            set
            {
                this._enModositok = value;
                //boxLabel.Text = (bool)_myIschecked ? "⚫" : "⚪";
                //CheckedChange?.Invoke(this, (bool)_myIschecked);

                //myFrame.BackgroundColor = _myIschecked ? Color.White : Color.Aqua;
                //myFrame.CornerRadius = _myIschecked ? 0 : 20;
            }

        }


        public RadioButton ()
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
        public string TextOther
        {
            set { SetValue(TextOtherProprty, value); }
            get { return (string)textOther.Text; }
        }
        public string Text
        {
            set { SetValue(TextProprty, value); }
            get { return (string)GetValue(TextProprty); }
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
            if (!_enModositok)
            {
                CheckedChange?.Invoke(this, IsChecked);
            }
            //_myIschecked = !_myIschecked;
            //IsChecked =_myIschecked  ;
            //boxLabel.Text = (bool)_myIschecked ? "⚫" : "⚪";
            
        }
        private void TextOther_TextChanged(object sender, TextChangedEventArgs e)
        {
            EntryChange?.Invoke(this, e);
        }

    }
}