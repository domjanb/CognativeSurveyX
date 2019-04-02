using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            /*this.CogUserAsync = new ObservableCollection<Cogazon>(database.Table<Cogazon>());
            this.CogDataKerdivAsync = new ObservableCollection<Cogkerdiv>(database.Table<Cogkerdiv>());
            this.CogDataAsync = new ObservableCollection<Cogdata>(database.Table<Cogdata>());
            this.CogParamAsync = new ObservableCollection<Cogparam>(database.Table<Cogparam>());
            this.MegszakadDataAsync = new ObservableCollection<MegszakadData>(database.Table<MegszakadData>());*/

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
        public async Task<int> SaveCogAzonAsync(Cogazon cogAzonAdat)
        {
                if (cogAzonAdat.id != 0)
                {
                    var vi=database.UpdateAsync(cogAzonAdat).Result;
                    return cogAzonAdat.id;
                }
                else
                {
                    var vi=database.InsertAsync(cogAzonAdat);
                    return cogAzonAdat.id;
                }
        }
        public async Task<int> DeleteCogAzonAsync(Cogazon cogAzonAdat)
        {
            var id = cogAzonAdat.id;
            if (id != 0)
            {
                    var vi=database.DeleteAsync<Cogazon>(id).Result;
            }
            //this.CogUserAsync.Remove(cogAzonAdat);
            return id;
        }
        public async Task DeleteCogAzonAllAsync()
        {
            {
                var vi=database.DeleteAllAsync<Cogazon>().Result;
            }
        }





        public async Task<List<MegszakadData>> GetMegszakadDataAllAsync()
        {
            {

                //var x = await database.QueryAsync<Cogazon>(qString);
                //return x;

                var vi=await database.QueryAsync<MegszakadData>("Select * from MegszakadData");
                return vi;

            }
        }
        public async Task<List<MegszakadData>> GetMegszakadDataKAsync()
        {
            {
                return await database.QueryAsync<MegszakadData>("Select * from MegszakadData where bejegyzesTipus=1");


            }
        }
        public async Task<List<MegszakadData>> GetMegszakadDataAsProjidVerAlidAsync(int projid, string kerdivver, int kerdivalid, int bejegyzestipus)
        {

            {
                var queryy = $"Select * from MegszakadData where projid={projid} and kerdivver={kerdivver} and alid={kerdivalid} and bejegyzesTipus={bejegyzestipus}";
                var vi=await database.QueryAsync<MegszakadData>(queryy);
                return vi;
                /*var query = await (from adat in database.Table<MegszakadData>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid && adat.bejegyzesTipus == bejegyzestipus) select adat);
                return query;*/
            }
        }
        public async Task<int> SaveMegszakadDataAsync(MegszakadData MegszakadDataAdat)
        {

            {
                if (MegszakadDataAdat.id != 0)
                {
                    var vi=database.UpdateAsync(MegszakadDataAdat).Result;
                    return MegszakadDataAdat.id;
                }
                else
                {
                    var vi=database.InsertAsync(MegszakadDataAdat).Result;
                    return MegszakadDataAdat.id;
                }
            }
        }
        public async Task<int> DeleteMegszakadDataAsync(MegszakadData MegszakadDataAdat)
        {
            var id = MegszakadDataAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.DeleteAsync<MegszakadData>(id);
                }

            }
            //this.MegszakadData.Remove(MegszakadDataAdat);
            return id;
        }










        public async Task<int> SaveCogDataAsync(Cogdata CogDataAdat)
        {
            if (CogDataAdat.id != 0)
                {
                var vis = database.UpdateAsync(CogDataAdat).Result;
                    return CogDataAdat.id;
                }
                else
                {
                    var vis= database.InsertAsync(CogDataAdat).Result;
                    return CogDataAdat.id;
                }
        }
    }
}
