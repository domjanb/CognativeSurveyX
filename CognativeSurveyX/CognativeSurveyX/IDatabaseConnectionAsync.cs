using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace CognativeSurveyX
{
    public interface IDatabaseConnectionAsync
    {
        SQLiteAsyncConnection DbConnection();
    }
}
