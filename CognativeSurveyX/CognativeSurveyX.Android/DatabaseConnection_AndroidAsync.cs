using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CognativeSurveyX.Droid;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_AndroidAsync))]
namespace CognativeSurveyX.Droid
{
    public class DatabaseConnection_AndroidAsync : IDatabaseConnectionAsync
    {
        public DatabaseConnection_AndroidAsync() { }
        public SQLiteAsyncConnection DbConnection()
        {
           
            {
                var dbName = "myDb.db3";
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
                return new SQLiteAsyncConnection(path);
            }
        }
    }
}