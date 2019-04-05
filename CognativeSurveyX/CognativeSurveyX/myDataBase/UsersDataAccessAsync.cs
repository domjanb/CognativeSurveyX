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
                var vi= await database.QueryAsync<MegszakadData>("Select * from MegszakadData where bejegyzesTipus=1");
                return vi;


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
                {
                    database.DeleteAsync<MegszakadData>(id);
                }

            }
            //this.MegszakadData.Remove(MegszakadDataAdat);
            return id;
        }










        public async Task<List<Cogkerdiv>> GetCogDataKerdivAsSernAsync(int Sern)
        {
            {
                /*var query = from adat in database.Table<Cogkerdiv>() where adat.id == Sern select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogkerdiv where id={Sern} ";
                var vi = await database.QueryAsync<Cogkerdiv>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogkerdiv>> GetCogDataKerdivAsProjidAsync(int projId)
        {
            {
                var queryy = $"Select * from Cogkerdiv where projid={projId} ";
                var vi = await database.QueryAsync<Cogkerdiv>(queryy);
                return vi;
                /*var query = from adat in database.Table<Cogkerdiv>() where adat.projid == projId select adat;
                return query.AsEnumerable();*/
            }
        }


        public async Task<List<Cogkerdiv>> GetCogDataKerdivAsync()
        {
            {
                var vi= database.QueryAsync<Cogkerdiv>("Select * from CogKerdiv").Result;
                return vi;

            }
        }
        public async Task<int> SaveCogDataKerdivAsync(Cogkerdiv CogDataKerdivAdat)
        {
            {
                if (CogDataKerdivAdat.id != 0)
                {
                    database.UpdateAsync(CogDataKerdivAdat);
                    return CogDataKerdivAdat.id;
                }
                else
                {
                    database.InsertAsync(CogDataKerdivAdat);
                    return CogDataKerdivAdat.id;
                }
            }
        }
        public async Task<int> DeleteCogDataKerdivAsync(Cogkerdiv CogDataKerdivAdat)
        {
            var id = CogDataKerdivAdat.id;
            if (id != 0)
            {

                {
                    database.DeleteAsync<Cogkerdiv>(id);
                }

            }
            //this.CogDataKerdiv.Remove(CogDataKerdivAdat);
            return id;
        }
        public async Task DeleteCogDataKerdivAllAsync()
        {
            {
                database.DeleteAllAsync<Cogkerdiv>();
            }



        }









        public async Task<List<Cogdata>> GetCogDataAsProjidAsync(int projid)
        {
            {
                /*var query = from adat in database.Table<Cogdata>() where adat.projid == projid select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogdata where projid={projid} ";
                var vi = await database.QueryAsync<Cogdata>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogdata>> GetCogDataAsProjidVerAlidAsync(int projid, string kerdivver, int kerdivalid)
        {
            {
                /*var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid) select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogdata where projid={projid} and  kerdivver={kerdivver} and  alid={kerdivalid}  ";
                var vi = await database.QueryAsync<Cogdata>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogdata>> GetCogDataAsProjidVerAlidKErdesAsync(int projid, string kerdivver, int kerdivalid, string kerdes)
        {
            {
                /*var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid && adat.kerdes == kerdes) select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogdata where projid={projid} and  kerdivver={kerdivver} and  alid={kerdivalid}  and  kerdes={kerdes} ";
                var vi = await database.QueryAsync<Cogdata>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogdata>> GetCogDataAsProjidVerAsync(int projid, string kerdivver)
        {
            {
                /*var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver) select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogdata where projid={projid} and  kerdivver={kerdivver}  ";
                var vi = await database.QueryAsync<Cogdata>(queryy);
                return vi;
            }
        }
       
        public async Task<List<Cogdata>> GetCogDataFeltoltveEAsync(bool fele)
        {
            {
                /*var query = from adat in database.Table<Cogdata>() where adat.feltoltve == fele select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogdata where feltoltve={fele} ";
                var vi = await database.QueryAsync<Cogdata>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogdata>> GetCogDataAsync()
        {
            {
                return database.QueryAsync<Cogdata>("Select * from Cogdata").Result;

            }
        }
        public async Task<int> UpdateCogDataAsync(Cogdata CogDataAdat)
        {
            {
                database.UpdateAsync(CogDataAdat);
                return CogDataAdat.id;
            }


        }

        public async Task<int> SaveCogDataAsync(Cogdata CogDataAdat)
        {
            {
                if (CogDataAdat.id != 0)
                {
                    database.UpdateAsync(CogDataAdat);
                    return CogDataAdat.id;
                }
                else
                {
                    database.InsertAsync(CogDataAdat);
                    return CogDataAdat.id;
                }
            }
        }
        public async Task<int> DeleteCogDataAsync(Cogdata CogDataAdat)
        {
            var id = CogDataAdat.id;
            if (id != 0)
            {
                {
                    database.DeleteAsync<Cogdata>(id);
                }

            }
            //this.CogData.Remove(CogDataAdat);
            return id;
        }
        public async Task DeleteCogDataAllAsync()
        {
            
            {
                database.DeleteAllAsync<Cogdata>();
            }



        }




















        public async Task<List<Cogparam>> GetCogparamAsProjidAsync(int projid)
        {
            {
                /*var query = from adat in database.Table<Cogparam>() where adat.projid == projid select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogparam where projid={projid}  ";
                var vi = await database.QueryAsync<Cogparam>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogparam>> GetCogparamAsProjidVerAsync(int projid, string kerdivver)
        {
            {

                /*var query = from adat in database.Table<Cogparam>() where (adat.projid == projid && adat.kerdivver == kerdivver) select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogparam where projid={projid} and  kerdivver={kerdivver}  ";
                var vi = database.QueryAsync<Cogparam>(queryy).Result;
                return vi;
            }
        }
        public async Task<List<Cogparam>> GetCogparamAsProjidVerKerdesAsync(int projid, string kerdivver, string kerdes)
        {
            {
                /*var query = from adat in database.Table<Cogparam>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.kerdes == kerdes) select adat;
                return query.AsEnumerable();*/
                var queryy = $"Select * from Cogparam where projid={projid} and  kerdivver={kerdivver} and  kerdes={kerdes} ";
                var vi =  await database.QueryAsync<Cogparam>(queryy);
                return vi;
            }
        }
        public async Task<List<Cogparam>> GetCogparamAsync()
        {
            {
                return database.QueryAsync<Cogparam>("Select * from Cogparam").Result;

            }
        }
        public async Task<int> UpdateCogparamAsync(Cogparam CogparamAdat)
        {
            {
                database.UpdateAsync(CogparamAdat);
                return CogparamAdat.id;
            }


        }

        public async Task<int> SaveCogparamAsync(Cogparam CogparamAdat)
        {
            {
                if (CogparamAdat.id != 0)
                {
                    database.UpdateAsync(CogparamAdat);
                    return CogparamAdat.id;
                }
                else
                {
                    database.InsertAsync(CogparamAdat);
                    return CogparamAdat.id;
                }
            }
        }
        public async Task<int> DeleteCogparamAsync(Cogparam CogparamAdat)
        {
            var id = CogparamAdat.id;
            if (id != 0)
            {
                {
                    database.DeleteAsync<Cogparam>(id);
                }

            }
            //this.CogParam.Remove(CogparamAdat);
            return id;
        }
        public async Task DeleteCogparamAllAsync()
        {
            {
                database.DeleteAllAsync<Cogparam>();
            }



        }












        /*public async Task<int> SaveCogDataAsync(Cogdata CogDataAdat)
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
        }*/
    }
}
