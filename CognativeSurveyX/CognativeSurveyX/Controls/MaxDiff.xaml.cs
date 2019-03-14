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
    public partial class MaxDiff : ContentView
    {
        public static readonly BindableProperty TextProprty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(MaxDiff),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((MaxDiff)bindable).textLabel.Text = (string)newValue;
                }
                );

        public static readonly BindableProperty BackgroundColorProprty =
            BindableProperty.Create(
                "BackgroundColor",
                typeof(Color),
                typeof(MaxDiff),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((MaxDiff)bindable).boxLabelBal.BackgroundColor = (Color)newValue;
                    ((MaxDiff)bindable).textLabel.BackgroundColor = (Color)newValue;
                    ((MaxDiff)bindable).boxLabelJobb.BackgroundColor = (Color)newValue;
                }
                );

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(MaxDiff),
                Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((MaxDiff)bindable).textLabel.FontSize = (double)newValue;
                    ((MaxDiff)bindable).boxLabelBal.FontSize = (double)newValue;
                    ((MaxDiff)bindable).boxLabelJobb.FontSize = (double)newValue;
                }
                );
        public static readonly BindableProperty IsCheckedBalProperty =
            BindableProperty.Create(
                "IsCheckedBal",
                typeof(bool),
                typeof(MaxDiff),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    MaxDiff checkbox = (MaxDiff)bindable;
                    ((MaxDiff)bindable).boxLabelBal.Text = (bool)newValue ? "⚫" : "⚪";
                    ((MaxDiff)bindable).CheckedChangeBal?.Invoke(checkbox, (bool)newValue);
                }
                );
        public static readonly BindableProperty IsCheckedJobbProperty =
            BindableProperty.Create(
                "IsCheckedJobb",
                typeof(bool),
                typeof(MaxDiff),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    MaxDiff checkbox = (MaxDiff)bindable;
                    ((MaxDiff)bindable).boxLabelJobb.Text = (bool)newValue ? "⚫" : "⚪";
                    //checkbox.CheckedChanged?.Invoke(checkbox, (bool)newValue);
                    ((MaxDiff)bindable).CheckedChangeJobb?.Invoke(checkbox, (bool)newValue);
                }
                );
        public bool _myIscheckedBal;
        public bool myIscheckedBal
        {
            get { return _myIscheckedBal; }
            set
            {
                this._myIscheckedBal = value;
                boxLabelBal.Text = (bool)_myIscheckedBal ? "⚫" : "⚪";
                
            }

        }
        public bool _enModositokBal;
        public bool enModositokBal
        {
            get { return _enModositokBal; }
            set
            {
                this._enModositokBal = value;
            }

        }

        public bool _myIscheckedJobb;
        public bool myIscheckedJobb
        {
            get { return _myIscheckedJobb; }
            set
            {
                this._myIscheckedJobb = value;
                boxLabelJobb.Text = (bool)_myIscheckedJobb ? "⚫" : "⚪";

            }

        }
        public bool _enModositokJobb;
        public bool enModositokJobb
        {
            get { return _enModositokJobb; }
            set
            {
                this._enModositokJobb = value;
            }

        }

        public event EventHandler<bool> CheckedChangeBal;
        public event EventHandler<bool> CheckedChangeJobb;
        
        public MaxDiff()
        {
            InitializeComponent();
            TapGestureRecognizer grBal = new TapGestureRecognizer();
            grBal.Tapped += GrBal_Tapped; ;
            boxLabelBal.GestureRecognizers.Add(grBal);

            TapGestureRecognizer grJobb = new TapGestureRecognizer();
            grJobb.Tapped += GrJobb_Tapped; ;
            boxLabelJobb.GestureRecognizers.Add(grJobb);

        }

        private void GrJobb_Tapped(object sender, EventArgs e)
        {
            IsCheckedJobb = !IsCheckedJobb;
            if (!_enModositokJobb)
            {
                CheckedChangeJobb?.Invoke(this, IsCheckedJobb);
            }

        }
        private void GrBal_Tapped(object sender, EventArgs e)
        {
            IsCheckedBal = !IsCheckedBal;
            if (!_enModositokBal)
            {
                CheckedChangeBal?.Invoke(this, IsCheckedBal);
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
        public Color BackgroundColor
        {
            set { SetValue(BackgroundColorProperty, value); }
            get { return (Color)GetValue(BackgroundColorProperty); }
        }
        public bool IsCheckedBal
        {
            set { SetValue(IsCheckedBalProperty, value); }
            get { return (bool)GetValue(IsCheckedBalProperty); }
        }
        public bool IsCheckedJobb
        {
            set { SetValue(IsCheckedJobbProperty, value); }
            get { return (bool)GetValue(IsCheckedJobbProperty); }
        }
        
    }
}