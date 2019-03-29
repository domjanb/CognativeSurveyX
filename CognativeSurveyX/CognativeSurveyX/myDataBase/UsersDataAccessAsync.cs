using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CognativeSurveyX.myDataBase
{

    public class UsersDataAccessAsync
    {
        private static object collisionLock = new object();
        private SQLiteAsyncConnection database;
        public UsersDataAccessAsync()
        {
            database = DependencyService.Get<IDatabaseConnectionAsync>().DbConnection();

            database.CreateTableAsync<Cogazon>();
            database.CreateTableAsync<Cogkerdiv>();
            database.CreateTableAsync<Cogdata>();
            database.CreateTableAsync<Cogparam>();
            database.CreateTableAsync<MegszakadData>();

        }
        public async Task<List<Cogazon>> GetAsyncCogAzonAsSern(int Sern)
        {
            //lock (collisionLock)
            {
                //var query = from adat in database.Table<Cogazon>() where adat.id == Sern select adat;
                //return query.ElementAtAsync();
                string qString = "select * from Cogazon where id='" + Convert.ToString(Sern) + "'";
                var x = await database.QueryAsync<Cogazon>(qString);
                return x;
                    //var obj2 = (from c in conn.Table<Customer>() where c.Id == obj.Id select c).ToListAsync().Result
            }

        }
        public async Task<List<Cogazon>> GetCogAzonAsync()
        {
            //lock (collisionLock)
            {
                string qString = "Select * from Cogazon";
                var x = database.QueryAsync<Cogazon>(qString).Result;
                return x;
                //return database.Query<Cogazon>("Select * from Cogazon").AsEnumerable();

            }
        }
        /*public async Task QueryAsync()
        {

            database.QueryAsync
            await connection.QueryAsync<Customer>("select * from Customer");
        }*/
    }
}
