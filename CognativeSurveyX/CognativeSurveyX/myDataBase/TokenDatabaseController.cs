using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CognativeSurveyX.myDataBase
{
    public class TokenDatabaseController
    {
        static object locker = new object();
        SQLiteConnection database;

        public TokenDatabaseController()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Token>();
        }
        private string aa() { return "a"; }

        public Token GetToken()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().First();
                }
            }
        }
        public int SaveToken(Token token)
        {
            int vissza;
            lock (locker)
            {
                if (token.Id!=0)
                {
                    database.Update(token);
                    //return token.Id;
                    vissza = token.Id;
                }
                else
                {
                    //return database.Insert(token);
                    vissza = database.Insert(token);
                }
            }
            return vissza;
        }
        public int DeleteToken(int Id)
        {
            lock (locker)
            {
                return database.Delete<Token>(Id);
            }
        }
    }
}
