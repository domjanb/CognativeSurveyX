using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.Modell
{
    class MasterPageItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}
