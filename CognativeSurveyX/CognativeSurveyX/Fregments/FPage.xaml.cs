using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognativeSurveyX.Fregments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FPage : ContentPage
	{
        
        public FPage ()
		{
            
            InitializeComponent ();
            

            //Constans.nextPage();
            if (Constans.pageNumber <= Constans.aktSurvey.questions.Count()+1)
            {
                if (Constans.aktQuestion.question_type == "Radioboxes")
                {
                    //Navigation.PushModalAsync(new Radioboxes());
                    //Navigation.PushModalAsync(new FCheckBox());
                    Navigation.PushModalAsync(new FRadioButton());
                    //Navigation.PushModalAsync(new FMaxDiff());


                }
                else if (Constans.aktQuestion.question_type == "Maxdiff")
                {
                    Navigation.PushModalAsync(new FMaxDiff());
                }
                else if (Constans.aktQuestion.question_type == "Tilitoli")
                {
                    //Navigation.PushModalAsync(new FCarousel());
                    Navigation.PushModalAsync(new FKepes());
                }
                else if (Constans.aktQuestion.question_type == "Kepes")
                {
                    Navigation.PushModalAsync(new FKepes());
                    //Navigation.PushModalAsync(new FCarousel());
                    //Navigation.PushModalAsync(new FCarouselString());
                }
                else if (Constans.aktQuestion.question_type == "Number")
                {
                    Navigation.PushModalAsync(new FNumbers());
                }
                else if (Constans.aktQuestion.question_type == "String")
                {
                    Navigation.PushModalAsync(new FString());
                }
                else if (Constans.aktQuestion.question_type == "Gombok")
                {
                    Navigation.PushModalAsync(new FGombok());
                }
                else if (Constans.aktQuestion.question_type == "SzurRadio2")
                {
                    Navigation.PushModalAsync(new FSzurCheckbox2());
                }
                else if (Constans.aktQuestion.question_type == "SzurRadio")
                {
                    Navigation.PushModalAsync(new FSzurRadio());
                }
                else if (Constans.aktQuestion.question_type == "TablesRadio")
                {
                    Navigation.PushModalAsync(new FTablesRadio());
                }
                else if (Constans.aktQuestion.question_type == "Checkboxes")
                {
                    Navigation.PushModalAsync(new FCheckBox());
                }
                else if (Constans.aktQuestion.question_type == "SorbaRendezo")
                {
                    Navigation.PushModalAsync(new FSorbarendezo());
                }
                else if (Constans.aktQuestion.question_type == "Skalak")
                {
                    Navigation.PushModalAsync(new FSkala());
                }
                else if (Constans.aktQuestion.question_type == "Radioboxes2")
                {
                    Navigation.PushModalAsync(new FFejek());
                }
                else if (Constans.aktQuestion.question_type == "vegeIntro")
                {
                    Navigation.PushModalAsync(new FVegeIntro());
                }
                else if (Constans.aktQuestion.question_type == "Photo")
                {
                    Navigation.PushModalAsync(new FPhoto());
                }
                else if (Constans.aktQuestion.question_type == "vege")
                {
                    Navigation.PushModalAsync(new MainPage3());
                }
            }
            else
            {
                Navigation.PushModalAsync(new MainPage3());
            }
            
            //break;
        }
        
    }
}