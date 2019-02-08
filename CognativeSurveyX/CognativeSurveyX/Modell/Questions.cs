using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.Modell
{
    public class Questions
    {
        public SurveyProperties survey_properties { get; set; }
        public IList<Question> questions { get; set; }
        
        public class SurveyProperties
        {
            public string intro_message { get; set; }
            public string end_message { get; set; }
            public bool skip_intro { get; set; }
        }

        public class Question
        {
            public string question_type { get; set; }
            public string question_title { get; set; }
            public string description { get; set; }
            public bool required { get; set; }
            public string kerdeskod { get; set; }
            public bool visible { get; set; }
            public bool random_choices { get; set; }
            public IList<string> choices { get; set; }
            public IList<string> rules { get; set; }
            public IList<string> items { get; set; }
        }

        


    }
}
