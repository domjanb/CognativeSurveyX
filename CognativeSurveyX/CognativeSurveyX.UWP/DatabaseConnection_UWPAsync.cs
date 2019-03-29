using System;
using System.IO;
using SQLite;
using Xamarin.Forms;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognativeSurveyX.UWP;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_UWPAsync))]

namespace CognativeSurveyX.UWP
{
    public class DatabaseConnection_UWPAsync : IDatabaseConnectionAsync
    {
        public SQLiteAsyncConnection DbConnection()
        {
            
            {
                var dbName = "myDb.db3";
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
                return new SQLiteAsyncConnection(path);
            }
        }
    }
}
