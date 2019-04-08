using CognativeSurveyX.Modell;
using LabelHtml.Forms.Plugin.Abstractions;
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
	public partial class FCarousel : CarouselPage
    {
        //CarouselViewModell _vm;
        List<ContentPage> cpb = new List<ContentPage>();
        List<ContentPage> cpj = new List<ContentPage>();
        List<string> isour = new List<string>();
        List<StackLayout> slTomb = new List<StackLayout>();
        public static List<Tuple<int, string>> mySortTomb = new List<Tuple<int, string>>();
        public static List<Tuple<int, ContentPage,int,bool>> ContetntPageTomb = new List<Tuple<int, ContentPage,int,bool>>();
        public static Dictionary<int, string>  myParam= new Dictionary<int, string>();
        public FCarousel ()
		{
			InitializeComponent ();

            cpj.Clear();
            cpb.Clear();
            isour.Clear();
            slTomb.Clear();
            mySortTomb.Clear();
            ContetntPageTomb.Clear();
            myParam.Clear();

            Constans.valaszok = "";
            int index = -1;
            foreach (var item in Constans.aktQuestion.choices)
            {
                index = index + 1;
                mySortTomb.Add(Tuple.Create(Convert.ToInt32(Constans.aktQuestion.choicesKod[index]), item));
                myParam.Add(index, item);
            }
            mySortTomb.Add(Tuple.Create(100, "OK"));

            

            if (Constans.aktQuestion.random_choices == true)
            {
                var rand = new Random();
                for (var i = 1; i < index; i++)
                {

                    int random1 = rand.Next(0, index+1);
                    int random2 = rand.Next(0, index+1);
                    if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random1].Item2)))
                    {
                        random1 = index + 1000;
                    }
                    else if (!Constans.KellERotalni(Constans.ValaszParameter(mySortTomb[random2].Item2)))
                    {
                        random2 = index + 1000;
                    }
                    Debug.WriteLine("randomok:" + random1 + " - " + random2);
                    if (random1 != random2 && random1<index && random2<index)
                    {
                        bool kell = true;
                        if (mySortTomb[random1].Item2.Length > 3)
                        {
                            if (mySortTomb[random1].Item2.ToLower().Substring(mySortTomb[random1].Item2.Length - 2,2) == "-r") { kell = false; }
                        }
                        if (mySortTomb[random2].Item2.Length > 3)
                        {
                            if (mySortTomb[random2].Item2.ToLower().Substring(mySortTomb[random2].Item2.Length - 2,2) == "-r") { kell = false; }
                        }
                        if (kell)
                        {
                            var tmp = mySortTomb[random1];
                            mySortTomb[random1] = mySortTomb[random2];
                            mySortTomb[random2] = tmp;
                        }
                        
                    }
                    
                    //var ertek1 = myTomb.ContainsKey(random1);
                    //var ertek2 = myTomb.ContainsKey(random2);
                    
                    //myTomb.ContainsKey(random1)

                }
                var a = 2;
            }
            
            foreach (var itemTomb in mySortTomb)
            //foreach (var item in Constans.aktQuestion.choices)
            {
                var item =Constans.ValaszParameterNelkul(  itemTomb.Item2);
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

                Label sorszam = new Label();
                sorszam.Margin = new Thickness(1, 1, 1, 1);
                sorszam.Text = Constans.sorszamErtek();
                sorszam.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                slTeljes.Children.Add(sorszam);

                HtmlLabel kerdes = new HtmlLabel();
                kerdes.Margin = new Thickness(10, 0, 10, 0);
                kerdes.Text = Constans.ParamErtekeBeilleszt(Constans.aktQuestion.question_title);
                kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                slTeljes.Children.Add(kerdes);

                string duma = ((string)item).ToLower();
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                if (File.Exists(ffile))
                {
                    isour.Add(ffile);
                    StackLayout sl = new StackLayout();
                    sl.Padding = new Thickness(10, 10, 10, 10);
                    sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                    sl.VerticalOptions = LayoutOptions.FillAndExpand;
                    //sl.BackgroundColor = Color.Red;
                    Image im1 = new Image();
                    im1.Source = ImageSource.FromFile(ffile);
                    im1.Aspect = Aspect.AspectFit;
                    im1.HorizontalOptions = LayoutOptions.FillAndExpand;
                    im1.VerticalOptions = LayoutOptions.FillAndExpand;
                    sl.Children.Add(im1);
                    slTeljes.Children.Add(sl);

                    Label aa = new Label();
                    aa.Text = "salal";
                    slTeljes.Children.Add(aa);
                }
                else
                {
                    isour.Add(ffile);
                    StackLayout sl = new StackLayout();
                    sl.Padding = new Thickness(10, 10, 10, 10);
                    sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                    sl.VerticalOptions = LayoutOptions.FillAndExpand;
                    //sl.BackgroundColor = Color.Red;
                    Label im1 = new Label();
                    im1.Text = duma;
                    im1.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    im1.HorizontalOptions = LayoutOptions.FillAndExpand;
                    im1.VerticalOptions = LayoutOptions.FillAndExpand;
                    sl.Children.Add(im1);
                    slTeljes.Children.Add(sl);
                }
                

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
                myPage.Disappearing += MyPage_DisAppearing;
                if (itemTomb != mySortTomb[mySortTomb.Count() - 1])
                {
                    ContetntPageTomb.Add(Tuple.Create(itemTomb.Item1, myPage, 0,true));
                }
                else
                {
                    ContetntPageTomb.Add(Tuple.Create(itemTomb.Item1, myPage, 0, false));
                }
                cpb.Add(myPage);
            }
            //foreach (var item in Constans.aktQuestion.choices)
            //{
            foreach (var itemTomb in mySortTomb)
            {
                var item = itemTomb.Item2;
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

                Label sorszam = new Label();
                sorszam.Margin = new Thickness(1, 1, 1, 1);
                sorszam.Text = Constans.sorszamErtek();
                sorszam.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                slTeljes.Children.Add(sorszam);

                HtmlLabel kerdes = new HtmlLabel();
                kerdes.Margin = new Thickness(10, 0, 10, 0);
                kerdes.Text = Constans.ParamErtekeBeilleszt(Constans.aktQuestion.question_title);
                kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                slTeljes.Children.Add(kerdes);

                string duma = ((string)item).ToLower();
                string ffile = Path.Combine(Constans.myFilePath, duma.ToLower() + "_logo.png");
                isour.Add(ffile);
                if (File.Exists(ffile))
                {
                    isour.Add(ffile);
                    StackLayout sl = new StackLayout();
                    sl.Padding = new Thickness(10, 10, 10, 10);
                    sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                    sl.VerticalOptions = LayoutOptions.FillAndExpand;
                    //sl.BackgroundColor = Color.Red;
                    Image im1 = new Image();
                    im1.Source = ImageSource.FromFile(ffile);
                    im1.Aspect = Aspect.AspectFit;
                    im1.HorizontalOptions = LayoutOptions.FillAndExpand;
                    im1.VerticalOptions = LayoutOptions.FillAndExpand;
                    sl.Children.Add(im1);
                    slTeljes.Children.Add(sl);
                    Label aa = new Label();
                    aa.Text = "salal";
                    slTeljes.Children.Add(aa);
                }
                else
                {
                    isour.Add(ffile);
                    StackLayout sl = new StackLayout();
                    sl.Padding = new Thickness(10, 10, 10, 10);
                    sl.HorizontalOptions = LayoutOptions.FillAndExpand;
                    sl.VerticalOptions = LayoutOptions.FillAndExpand;
                    //sl.BackgroundColor = Color.Red;
                    Label im1 = new Label();
                    im1.Text = duma;
                    im1.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    im1.HorizontalOptions = LayoutOptions.FillAndExpand;
                    im1.VerticalOptions = LayoutOptions.FillAndExpand;
                    sl.Children.Add(im1);
                    slTeljes.Children.Add(sl);
                }
                

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
                myPage.Disappearing += MyPage_DisAppearing;
                if (itemTomb != mySortTomb[mySortTomb.Count() - 1])
                {
                    ContetntPageTomb.Add(Tuple.Create(itemTomb.Item1, myPage, 10, true));
                }
                else
                {
                    ContetntPageTomb.Add(Tuple.Create(itemTomb.Item1, myPage, 10, false));
                }
                cpj.Add(myPage);
            }




            Children.Add(cpb[1]);
            Children.Add(cpj[0]);
            Children.Add(cpj[1]);
            Constans.valaszok = "";
            this.CurrentPage = this.Children[1];
            

        }

        private void MyPage_DisAppearing(object sender, EventArgs e)
        {
            foreach (var item in ContetntPageTomb)
            {
                if (item.Item2.Id == ((ContentPage)sender).Id)
                {
                    if (item.Item4)
                    {
                        Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(item.Item1);
                    }
                    
                }
            }
            /*int idx = 0;
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
                idx = 0;
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

                            }
                            else
                            {
                                _Continue_Clicked();
                            }

                        }

                    }
                }
            }*/
            Debug.WriteLine(Constans.valaszok);

        }
        private void MyPage_Appearing(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            

            int idx = -1;
            int aktindex = -1;
            foreach(var item in cpj)
            {
                idx++;
                if (item.Id == ((ContentPage)sender).Id)
                {
                    if (idx > 0)
                    {
                        Debug.WriteLine("jobb oldal vót");
                        aktindex = idx;

                        foreach (var itemx in ContetntPageTomb)
                        {
                            if (itemx.Item2.Id == ((ContentPage)sender).Id)
                            {
                                //Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(itemx.Item1) + "=" + itemx.Item3 + ";";
                                Constans.valaszok = Constans.valaszok + "=" + itemx.Item3 + ";";
                            }
                        }
                        if (aktindex < cpb.Count()-1)
                        {
                            this.Children.RemoveAt(1);
                            this.Children.RemoveAt(0);
                            Children.Insert(0, cpb[aktindex + 1]);
                            Children.Insert(2, cpj[aktindex + 1]);
                            //Children.Add(cpj[aktindex]);
                            

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
                        if (idx>0)
                        {
                            Debug.WriteLine("bal oldal vót");
                            aktindex = idx;
                            foreach (var itemx in ContetntPageTomb)
                            {
                                if (itemx.Item2.Id == ((ContentPage)sender).Id)
                                {
                                    Constans.valaszok = Constans.valaszok + "=" + itemx.Item3 + ";";
                                }
                            }
                            if (aktindex < cpb.Count()-1)
                            {
                                this.Children.RemoveAt(2);
                                this.Children.RemoveAt(1);
                                Children.Insert(0, cpb[aktindex + 1]);
                                Children.Insert(2, cpj[aktindex + 1]);
                                
                                //Children.Add(cpj[aktindex]);
                                

                            }
                            else
                            {
                                _Continue_Clicked();
                            }

                        }
                        
                    }
                }
            }
            Debug.WriteLine(Constans.valaszok);

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
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new FPageStep());
        }
    }
}