using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FCarouselString : CarouselPage
    {
        //CarouselViewModell _vm;
        List<ContentPage> cpb = new List<ContentPage>();
        List<ContentPage> cpj = new List<ContentPage>();
        List<string> isour = new List<string>();
        List<StackLayout> slTomb = new List<StackLayout>();
        public FCarouselString()
        {
            InitializeComponent();
            Constans.valaszok = "";
            /*BindingContext = _vm = new CarouselViewModell();
            List<string> isour = new List<string>();
            foreach (var item in Constans.aktQuestion.choices)
            {
                string duma = ((string)item).ToLower();
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                isour.Add(ffile);
            }
            CarouselView cw = new CarouselView();
            cw.ItemsSource = isour;
            
            myLayout.Children.Add(cw);*/
            /*CarouselPage carouselPage = new CarouselPage();
            carouselPage.Children.Add(new FCarouselL());
            carouselPage.Children.Add(new FCarouselC());
            carouselPage.Children.Add(new FCarouselR());*/
            //Navigation.PushModalAsync(carouselPage);


            foreach (var item in Constans.aktQuestion.choices)
            {
                StackLayout slTeljes = new StackLayout();
                var fejlecL = new StackLayout();
                fejlecL.BackgroundColor = Color.Aqua;
                fejlecL.HorizontalOptions = LayoutOptions.FillAndExpand;
                fejlecL.VerticalOptions = LayoutOptions.Start;
                fejlecL.Padding = 20;

                var fejlecD = new Label();
                fejlecD.Text = "Cognative Touchpoint";
                fejlecD.HorizontalOptions = LayoutOptions.Center;
                fejlecL.Children.Add(fejlecD);

                slTeljes.Children.Add(fejlecL);


                string duma = ((string)item).ToLower();
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                isour.Add(ffile);
                StackLayout sl = new StackLayout();
                sl.Padding = new Thickness(10, 10, 10, 10);
                sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                sl.VerticalOptions = LayoutOptions.FillAndExpand;
                //sl.BackgroundColor = Color.Red;
                Label im1 = new Label();
                //im1.Source = ImageSource.FromFile(ffile);
                //im1.Aspect = Aspect.AspectFit;
                im1.Text = duma;
                im1.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                im1.HorizontalOptions = LayoutOptions.CenterAndExpand;
                im1.VerticalOptions = LayoutOptions.CenterAndExpand;
                sl.Children.Add(im1);
                slTeljes.Children.Add(sl);

                StackLayout sl2 = new StackLayout();
                //sl2.BackgroundColor = Color.Blue;
                Label btn = new Label();
                btn.Text = "<-rossz ---- klassz->";
                btn.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                btn.HorizontalOptions = LayoutOptions.CenterAndExpand;
                btn.VerticalOptions = LayoutOptions.End;
                sl2.Children.Add(btn);
                Label lbl = new Label();
                slTeljes.Children.Add(sl2);

                /*TapGestureRecognizer tgr = new TapGestureRecognizer();
                tgr.Tapped += Gr_Tapped;
                sl.GestureRecognizers.Add(tgr);*/
                slTomb.Add(slTeljes);

                var myPage = new ContentPage();
                myPage.Content = slTeljes;
                /*myPage.Appearing += (sender, e) =>
                {
                    System.Console.WriteLine("Page 1: Appeared!");
                };*/
                myPage.Appearing += MyPage_Appearing;
                cpb.Add(myPage);
            }
            foreach (var item in Constans.aktQuestion.choices)
            {
                StackLayout slTeljes = new StackLayout();
                var fejlecL = new StackLayout();
                fejlecL.BackgroundColor = Color.Aqua;
                fejlecL.HorizontalOptions = LayoutOptions.FillAndExpand;
                fejlecL.VerticalOptions = LayoutOptions.Start;
                fejlecL.Padding = 20;

                var fejlecD = new Label();
                fejlecD.Text = "Cognative Touchpoint";
                fejlecD.HorizontalOptions = LayoutOptions.Center;
                fejlecL.Children.Add(fejlecD);

                slTeljes.Children.Add(fejlecL);


                string duma = ((string)item).ToLower();
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                isour.Add(ffile);
                StackLayout sl = new StackLayout();
                sl.Padding = new Thickness(10, 10, 10, 10);
                sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                sl.VerticalOptions = LayoutOptions.FillAndExpand;
                //sl.BackgroundColor = Color.Red;
                /*Image im1 = new Image();
                im1.Source = ImageSource.FromFile(ffile);
                im1.Aspect = Aspect.AspectFit;
                im1.HorizontalOptions = LayoutOptions.FillAndExpand;
                im1.VerticalOptions = LayoutOptions.FillAndExpand;*/
                Label im1 = new Label();
                //im1.Source = ImageSource.FromFile(ffile);
                //im1.Aspect = Aspect.AspectFit;
                im1.Text = duma;
                im1.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                im1.HorizontalOptions = LayoutOptions.CenterAndExpand;
                im1.VerticalOptions = LayoutOptions.CenterAndExpand;
                sl.Children.Add(im1);
                slTeljes.Children.Add(sl);

                StackLayout sl2 = new StackLayout();
                //sl2.BackgroundColor = Color.Blue;
                Label btn = new Label();
                btn.Text = "<-rossz ---- klassz->";
                btn.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                btn.HorizontalOptions = LayoutOptions.CenterAndExpand;
                btn.VerticalOptions = LayoutOptions.End;
                sl2.Children.Add(btn);
                Label lbl = new Label();
                slTeljes.Children.Add(sl2);

                /*TapGestureRecognizer tgr = new TapGestureRecognizer();
                tgr.Tapped += Gr_Tapped;
                sl.GestureRecognizers.Add(tgr);*/
                slTomb.Add(slTeljes);

                var myPage = new ContentPage();
                myPage.Content = slTeljes;
                /*myPage.Appearing += (sender, e) =>
                {
                    System.Console.WriteLine("Page 1: Appeared!");
                };*/
                myPage.Appearing += MyPage_Appearing;
                cpj.Add(myPage);
            }




            Children.Add(cpb[1]);
            Children.Add(cpj[0]);
            Children.Add(cpj[1]);
            this.CurrentPage = this.Children[1];


        }

        private void MyPage_Appearing(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int idx = -1;
            int aktindex = -1;
            foreach (var item in cpj)
            {
                idx++;
                if (item.Id == ((ContentPage)sender).Id)
                {
                    if (idx > 0)
                    {
                        Debug.WriteLine("jobb oldal vót");
                        aktindex = idx;

                        if (aktindex < cpb.Count() - 1)
                        {
                            this.Children.RemoveAt(1);
                            this.Children.RemoveAt(0);
                            Children.Insert(0, cpb[aktindex + 1]);
                            Children.Insert(2, cpj[aktindex + 1]);
                            //Children.Add(cpj[aktindex]);
                            Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(aktindex + 1) + "=10;";

                        }
                        else
                        {
                            _Continue_Clicked();
                        }

                    }

                }
            }
            if (aktindex < 0)
            {
                idx = -1;
                foreach (var item in cpb)
                {
                    idx++;
                    if (item.Id == ((ContentPage)sender).Id)
                    {
                        if (idx > 0)
                        {
                            Debug.WriteLine("bal oldal vót");
                            aktindex = idx;
                            if (aktindex < cpb.Count() - 1)
                            {
                                this.Children.RemoveAt(2);
                                this.Children.RemoveAt(1);
                                Children.Insert(0, cpb[aktindex + 1]);
                                Children.Insert(2, cpj[aktindex + 1]);
                                //Children.Add(cpj[aktindex]);
                                Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(aktindex + 1) + "=0;";

                            }
                            else
                            {
                                _Continue_Clicked();
                            }

                        }

                    }
                }
            }


            /*foreach (var item in this.Children)
            {
                if (item.Id==)
            }*/

        }

        private void Gr_Tapped(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var a = 2;
        }

        /*private void Btn_Clicked(object sender, EventArgs e)
        {
            var greyPage2 = new ContentPage();
            greyPage2.Content = slTomb[5];
            cp[0] = greyPage2;
            this.Children.Remove(cp[0]);
            var szulo = this.Parent;

            this.Children[0]= cp[1];
            foreach(var item in this.Children)
            {
               var itemX = (ContentPage)item;
                foreach (var item2 in itemX.Children)
                {

                }
                var a = 2;
            }
        }*/

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            Debug.WriteLine("Position " + e.NewValue + " selected.");
        }

        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            Debug.WriteLine("Scrolled to " + e.NewValue + " percent.");
            Debug.WriteLine("Direction = " + e.Direction);
        }
        private void _Continue_Clicked()
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}