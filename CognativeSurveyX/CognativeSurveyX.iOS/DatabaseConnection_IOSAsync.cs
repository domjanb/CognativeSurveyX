using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;




using System.IO;
using CognativeSurveyX.iOS;
using Foundation;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_IOSAsync))]
namespace CognativeSurveyX.iOS
{
    public class DatabaseConnection_IOSAsync : IDatabaseConnectionAsync
    {

        public DatabaseConnection_IOSAsync() { }
        public SQLiteAsyncConnection DbConnection()
        {
            
            {
                var dbName = "myDb.db3";
                string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libFolder = Path.Combine(personalFolder, "..", "Library");
                var path = Path.Combine(libFolder, dbName);
                return new SQLiteAsyncConnection(path);
            }
        }
    }
}