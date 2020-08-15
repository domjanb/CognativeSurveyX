using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX
{
    [Table("Kozponti")]
    public class Kozponti //: INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                this._id = value;
                //OnProperityChange(nameof(id));
            }
        }

        private string _kerdes;
        public string kerdes
        {
            get { return _kerdes; }
            set
            {
                this._kerdes = value;
                //OnProperityChange(nameof(kerdes));
            }
        }

        private string _valasz;
        public string valasz
        {
            get { return _valasz; }
            set
            {
                this._valasz = value;
                //OnProperityChange(nameof(valasz));
            }
        }
        

        private long _kerdivdate;
        public long kerdivdate
        {
            get { return _kerdivdate; }
            set
            {
                this._kerdivdate = value;
                //OnProperityChange(nameof(kerdivdate));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperityChange(string propertyName)
        {
            //this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
