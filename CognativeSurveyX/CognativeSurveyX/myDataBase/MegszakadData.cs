using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.myDataBase
{
    [Table("MegszakadData")]
    public class MegszakadData
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
        private string _kerdivver;
        public string kerdivver
        {
            get { return _kerdivver; }
            set
            {
                this._kerdivver = value;
                //OnProperityChange(nameof(kerdiv1ver));
            }
        }
        private int _projid;
        public int projid
        {
            get { return _projid; }
            set
            {
                this._projid = value;
            }
        }
        private string _szoveg;
        public string szoveg
        {
            get { return _szoveg; }
            set
            {
                this._szoveg = value;
            }
        }
        private int _alid;
        public int alid
        {
            get { return _alid; }
            set
            {
                this._alid = value;
            }
        }
        private int _bejegyzesTipus;
        public int bejegyzesTipus
        {
            get { return _bejegyzesTipus; }
            set
            {
                this._bejegyzesTipus = value;
            }
        }
        private long _kerdivdate;
        public long kerdivdate
        {
            get { return _kerdivdate; }
            set
            {
                this._kerdivdate = value;
                OnProperityChange(nameof(kerdivdate));
            }
        }
    }
}
