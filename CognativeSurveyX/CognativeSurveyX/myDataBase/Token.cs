using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.myDataBase
{
    public class Token
    {
        public int Id { get; set; }
        public int access_token { get; set; }
        public int error_description { get; set; }

        public Token() { }
    }
}
