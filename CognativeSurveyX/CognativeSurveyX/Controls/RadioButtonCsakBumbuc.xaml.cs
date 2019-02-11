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
	public partial class RadioButtonCsakBumbuc : ContentView
	{
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(RadioButtonCsakBumbuc),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RadioButtonCsakBumbuc)bindable).boxLabel.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(RadioButtonCsakBumbuc),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((RadioButtonCsakBumbuc)bindable).boxLabel.Text = (bool)newValue ? "⚫" : "⚪";

                    ((RadioButtonCsakBumbuc)bindable).CheckedChange?.Invoke(((RadioButtonCsakBumbuc)bindable), (bool)newValue);
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
                boxLabel.Text = (bool)_myIschecked ? "⚫" : "⚪";
                
            }

        }

        public bool _enModositok;
        public bool enModositok
        {
            get { return _enModositok; }
            set
            {
                this._enModositok = value;
            }

        }
        public RadioButtonCsakBumbuc ()
		{
			InitializeComponent ();
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