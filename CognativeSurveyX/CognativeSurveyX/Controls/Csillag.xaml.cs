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
	public partial class Csillag : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(Csillag),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Csillag)bindable).textLabel.Text = (string)newValue;
                    if (((string)newValue).Length == 0)
                    {
                        ((Csillag)bindable).textLabel.IsVisible = false;

                    }
                    else
                    {
                        ((Csillag)bindable).textLabel.IsVisible = true;
                    }
                }
                );
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(Csillag),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Csillag)bindable).textLabel.FontSize = (double)newValue;
                    ((Csillag)bindable).boxLabel.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(Csillag),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Csillag)bindable).BackgroundColor = Color.Gray.MultiplyAlpha(0.1);
                    ((Csillag)bindable).boxLabel.Text = (bool)newValue ? "⭐" : "☆";
                    if ((bool)newValue)
                    {
                        if (((Csillag)bindable).FontMeret < Device.GetNamedSize(NamedSize.Large, typeof(Label)))
                        {
                            ((Csillag)bindable).boxLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                            
                        }
                        ((Csillag)bindable).BackgroundColor = Color.Yellow.MultiplyAlpha(0.1);

                    }
                    else
                    {
                        ((Csillag)bindable).boxLabel.FontSize = ((Csillag)bindable).FontMeret;
                        
                    }

                    ((Csillag)bindable).CheckedChange?.Invoke(((Csillag)bindable), (bool)newValue);
                }
                );



        public event EventHandler<bool> CheckedChange;

        public bool _myIschecked;
        public bool myIschecked
        {
            get { return _myIschecked; }
            set
            {
                this._myIschecked = value;
                boxLabel.Text = (bool)_myIschecked ? "⭐" : "☆";
                if ((bool)_myIschecked)
                {
                    if (FontMeret < Device.GetNamedSize(NamedSize.Large, typeof(Label)))
                    {
                        boxLabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    }
                    this.BackgroundColor = Color.Yellow.MultiplyAlpha(0.1);
                }
                else
                {
                    boxLabel.FontSize = FontMeret;
                    this.BackgroundColor = Color.Gray.MultiplyAlpha(0.1);

                }
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
        public double _FontMeret;
        public double FontMeret
        {
            get { return _FontMeret; }
            set
            {
                this._FontMeret = FontSize;
                
            }
        }

        public Csillag ()
		{
			InitializeComponent ();
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
    }
}