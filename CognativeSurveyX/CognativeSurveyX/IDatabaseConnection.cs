using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace CognativeSurveyX
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
