using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.Data
{
    public class RestApiModell
    {

        public bool error { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string proj_id { get; set; }
        public string kerdiv1_nev { get; set; }
        public string kerdiv1_ver { get; set; }
        public string kerdiv2_le { get; set; }
        public int darab { get; set; }
        public Kerdivadat[] kerdivadat { get; set; }

        public class RootObject
        {
            public bool _error;
            public bool error
            {
                get
                {
                    return _error;
                }
                set
                {
                    this._error = value;
                }
            }
            public string message { get; set; }
            public string id { get; set; }
            public string proj_id { get; set; }
            public string kerdiv1_nev { get; set; }
            public string kerdiv1_ver { get; set; }
            public string kerdiv2_le { get; set; }
            public int darab { get; set; }
            public Kerdivadat[] kerdivadat { get; set; }
        }

        public class Kerdivadat
        {
            public string id { get; set; }
            public string proj_id { get; set; }
            public string kerdiv1_nev { get; set; }
            public string kerdiv1_ver { get; set; }
            public int kerdiv2_le { get; set; }
            public string kerdiv1_title { get; set; }
            public string kerdivtip { get; set; }
            public string fugg_proj { get; set; }
            public string fugg_par { get; set; }
            public string fugg_par_ertek { get; set; }
        }

        public RestApiModell() { }
        
    }
}
