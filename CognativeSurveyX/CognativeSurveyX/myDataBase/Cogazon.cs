using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;


namespace CognativeSurveyX
{
    [Table("CogAzon")]
    public class Cogazon //: INotifyPropertyChanged
    {
        private int _id;
       [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            { this._id = value;
                //OnProperityChange(nameof(id));
            }

        }

        private string _uemail;
        public string uemail
        {
            get { return _uemail; }
            set
            {
                this._uemail = value;
                //OnProperityChange(nameof(uemail));
            }
        }

        private string _uname;
        public string uname
        {
            get { return _uname; }
            set
            {
                this._uname = value;
                //OnProperityChange(nameof(uname));
            }
        }

        private string _upass;
        public string upass
        {
            get { return _upass; }
            set
            {
                this._upass = value;
                //OnProperityChange(nameof(upass));
            }
        }

        private string _usname;
        public string usname
        {
            get { return _usname; }
            set
            {
                this._usname = value;
                //OnProperityChange(nameof(usname));
            }
        }

        private int _userid;
        public int userid
        {
            get { return _userid; }
            set
            {
                this._userid = value;
                //OnProperityChange(nameof(userid));
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperityChange(string propertyName)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
