using System;
using SQLite;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;


namespace CognativeSurveyX
{
    [Table("CogKerdiv")]
    public class Cogkerdiv // : INotifyPropertyChanged
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

        private string _kerdiv1nev;
        public string kerdiv1nev
        {
            get { return _kerdiv1nev; }
            set
            {
                this._kerdiv1nev = value;
                //OnProperityChange(nameof(kerdiv1nev));
            }
        }
        private string _kerdiv1ver;
        public string kerdiv1ver
        {
            get { return _kerdiv1ver; }
            set
            {
                this._kerdiv1ver = value;
                //OnProperityChange(nameof(kerdiv1ver));
            }
        }
        private string _kerdivtitle;
        public string kerdivtitle
        {
            get { return _kerdivtitle; }
            set
            {
                this._kerdivtitle = value;
                //OnProperityChange(nameof(kerdivtitle));
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
                //OnProperityChange(nameof(kerdivtip));
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
                //OnProperityChange(nameof(projid));
            }
        }
        private int _fuggv_par;
        public int fuggv_par
        {
            get { return _fuggv_par; }
            set
            {
                this._fuggv_par = value;
                //OnProperityChange(nameof(fuggv_par));
            }
        }
        private int _fuggv_par_ertek;
        public int fuggv_par_ertek
        {
            get { return _fuggv_par_ertek; }
            set
            {
                this._fuggv_par_ertek = value;
                //OnProperityChange(nameof(fuggv_par_ertek));
            }
        }
        private int _fuggv_poj;
        public int fuggv_poj
        {
            get { return _fuggv_poj; }
            set
            {
                this._fuggv_poj = value;
                //OnProperityChange(nameof(fuggv_poj));
            }
        }
        private DateTime _kerdivdate;
        [NotNull]
        public DateTime kerdivdate
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
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
