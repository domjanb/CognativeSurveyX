using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX
{
    [Table("Cogdata")]
    public class Cogdata : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                this._id = value;
                OnProperityChange(nameof(id));
            }

        }

        private string _egyedi1;
        public string egyedi1
        {
            get { return _egyedi1; }
            set
            {
                this._egyedi1 = value;
                OnProperityChange(nameof(egyedi1));
            }
        }

        private string _egyedi2;
        public string egyedi2
        {
            get { return _egyedi2; }
            set
            {
                this._egyedi2 = value;
                OnProperityChange(nameof(egyedi2));
            }
        }

        private string _egyedi3;
        public string egyedi3
        {
            get { return _egyedi3; }
            set
            {
                this._egyedi3 = value;
                OnProperityChange(nameof(egyedi3));
            }
        }

        private string _egyedi4;
        public string egyedi4
        {
            get { return _egyedi4; }
            set
            {
                this._egyedi4 = value;
                OnProperityChange(nameof(egyedi4));
            }
        }


        private string _kerdes;
        [NotNull]
        public string kerdes
        {
            get { return _kerdes; }
            set
            {
                this._kerdes = value;
                OnProperityChange(nameof(kerdes));
            }
        }

        private string _valasz;
        [NotNull]
        public string valasz
        {
            get { return _valasz; }
            set
            {
                this._valasz = value;
                OnProperityChange(nameof(valasz));
            }
        }

        private string _kerdivver;
        [NotNull]
        public string kerdivver
        {
            get { return _kerdivver; }
            set
            {
                this._kerdivver = value;
                OnProperityChange(nameof(kerdivver));
            }
        }

        private int _alid;
        [NotNull]
        public int alid
        {
            get { return _alid; }
            set
            {
                this._alid = value;
                OnProperityChange(nameof(alid));
            }
        }

        private int _projid;
        [NotNull]
        public int projid
        {
            get { return _projid; }
            set
            {
                this._projid = value;
                OnProperityChange(nameof(projid));
            }
        }

        private int _kerdivtip;
        [NotNull]
        public int kerdivtip
        {
            get { return _kerdivtip; }
            set
            {
                this._kerdivtip = value;
                OnProperityChange(nameof(kerdivtip));
            }
        }

        private long _kerdivdate;
        [NotNull]
        public long kerdivdate
        {
            get { return _kerdivdate; }
            set
            {
                this._kerdivdate = value;
                OnProperityChange(nameof(kerdivdate));
            }
        }
        private bool _feltoltve;
        [NotNull]
        public bool feltoltve
        {
            get { return _feltoltve; }
            set
            {
                this._feltoltve = value;
                OnProperityChange(nameof(_feltoltve));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperityChange(string propertyName)
        {
            //this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
