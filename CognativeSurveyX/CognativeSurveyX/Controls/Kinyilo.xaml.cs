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
	public partial class Kinyilo : ContentView
	{
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(Kinyilo),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Kinyilo)bindable).textLabel.Text = (string)newValue;
                }
                );
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(Kinyilo),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Kinyilo)bindable).textLabel.FontSize = (double)newValue;
                    ((Kinyilo)bindable).boxLabel.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(Kinyilo),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Kinyilo)bindable).boxLabel.Text = (bool)newValue ? "-" : "+";
                    //((Gomb)bindable).myFrame.BackgroundColor = (bool)newValue ? Color.White : Color.Aqua;
                    //((Gomb)bindable).myFrame.CornerRadius = (bool)newValue ? 0 : 20;
                    ((Kinyilo)bindable).CheckedChange?.Invoke(((Kinyilo)bindable), (bool)newValue);
                }
                );
        public static readonly BindableProperty IsUccsoProperty =
            BindableProperty.Create(
                "IsUccso",
                typeof(bool),
                typeof(Kinyilo),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Kinyilo)bindable).myFrame.Margin = (bool)newValue ? ((Kinyilo)bindable).margin2 : ((Kinyilo)bindable).margin1;
                }
                );
        public event EventHandler<bool> CheckedChange;

        Thickness margin1 = new Thickness
        {
            Bottom = -20,
            Top = -0,
            Left = 0,
            Right = 0,
        };
        Thickness margin2 = new Thickness
        {
            Bottom = 0,
            Top = -0,
            Left = 0,
            Right = 0,
        };
        public Kinyilo()
        {
            InitializeComponent();
            /*Thickness margin = new Thickness();
            margin.Bottom = 0;
            margin.Top = -0;
            margin.Left = 0;
            margin.Right = 0;
            if (_Uccsoe)
            {
                myFrame.Margin = margin;
            }*/

        }
        public bool _myIschecked;
        public bool myIschecked
        {
            get { return _myIschecked; }
            set
            {
                this._myIschecked = value;
                myFrame.BackgroundColor = _myIschecked ? Color.White : Color.Aqua;
                myFrame.CornerRadius = _myIschecked ? 0 : 20;
            }

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
        public bool Isuccso
        {
            set { SetValue(IsUccsoProperty, value); }
            get { return (bool)GetValue(IsUccsoProperty); }
        }
        void OnCheckBoxTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }
    }
}