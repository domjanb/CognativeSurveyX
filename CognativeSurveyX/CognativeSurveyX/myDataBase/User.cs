using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.myDataBase
{
    public class User
    {
        public int Id { get; set; }
        public String user_name { get; set; }
        public String user_surnamed { get; set; }
        public String user_kod { get; set; }
        public String user_password { get; set; }
        public String user_emil { get; set; }

        public User() { }
        public User(string user_name,string user_surnamed, string user_kod,string user_password,string user_emil)
        {
            this.user_emil = user_emil;
            this.user_kod = user_kod;
            this.user_name = user_name;
            this.user_password = user_password;
            this.user_surnamed = user_surnamed;
        }
    }
}
