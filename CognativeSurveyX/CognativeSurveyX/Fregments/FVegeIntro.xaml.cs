using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FVegeIntro : ContentPage
    {
        public FVegeIntro()
        {
            InitializeComponent();
            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myScroll = new ScrollView();
            var myStack = new StackLayout();
            myStack.VerticalOptions = LayoutOptions.FillAndExpand;
            myStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            myScroll.Content = myStack;

            //myLayout.Children.Add(myScroll);

            Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}