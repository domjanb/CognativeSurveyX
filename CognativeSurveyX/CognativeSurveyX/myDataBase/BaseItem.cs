using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.myDataBase
{
    class BaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
