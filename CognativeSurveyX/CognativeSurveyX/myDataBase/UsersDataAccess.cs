using CognativeSurveyX.myDataBase;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CognativeSurveyX
{
    public class UsersDataAccess
    {
        //private SQLiteConnection database;
        private static object collisionLock = new object();
        SQLiteConnection database;

        

        public ObservableCollection<Cogazon> CogUser { get; set; }
        public ObservableCollection<Cogkerdiv> CogDataKerdiv { get; set; }
        public ObservableCollection<Cogdata> CogData { get; set; }
        public ObservableCollection<Cogparam> CogParam { get; set; }
        public ObservableCollection<MegszakadData> MegszakadData { get; set; }
        public ObservableCollection<Kozponti> Kozponti { get; set; }

        public UsersDataAccess()
        {
            
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            //database.DropTable<Cogdata>();
            //database.DropTable<Cogazon>();


            //database.DropTable<MegszakadData>();
            //database.DropTable<Kozponti>();

            if (!TableExists("Cogkerdiv"))
            {
                database.CreateTable<Cogkerdiv>();
            }
            if (!TableExists("Kozponti"))
            {
                database.CreateTable<Kozponti>();
            }
            if (!TableExists("MegszakadData"))
            {
                database.CreateTable<MegszakadData>();
            }
            if (!TableExists("Cogdata"))
            {
                database.CreateTable<Cogdata>();
            }
            if (!TableExists("Cogparam"))
            {
                database.CreateTable<Cogparam>();
            }
            if (!TableExists("Cogazon"))
            {
                database.CreateTable<Cogazon>();
            }
            //database.CreateTable<Cogkerdiv>();
            //database.CreateTable<Kozponti>();
            //database.CreateTable<MegszakadData>();

            //database.CreateTable<Cogdata>();
            //database.CreateTable<Cogparam>();
            
            
            //database.CreateTable<Cogazon>();

            this.CogUser = new ObservableCollection<Cogazon>(database.Table<Cogazon>());
            this.CogDataKerdiv = new ObservableCollection<Cogkerdiv>(database.Table<Cogkerdiv>());
            this.CogData = new ObservableCollection<Cogdata>(database.Table<Cogdata>());
            this.CogParam = new ObservableCollection<Cogparam>(database.Table<Cogparam>());
            this.MegszakadData = new ObservableCollection<MegszakadData>(database.Table<MegszakadData>());
            this.Kozponti = new ObservableCollection<Kozponti>(database.Table<Kozponti>());


            if (!database.Table<Cogazon>().Any())
            {
                //AddNewUser();
            }

        }


        public virtual bool TableExists(string tableName)
        {
            bool sw = false;
            try
            {
                //using (var connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), PathDataBase))
                {
                    string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
                    //string query = string.Format("SELECT name FROM sqlite_master WHERE type='table' ;");
                    var vissza = database.Query<TableName>(query);
                    SQLiteCommand cmd =  database.CreateCommand(query);
                    var item = database.Query<object>(query);
                    if (item.Count > 0)
                        sw = true;
                    return sw;
                }
            }
            catch (SQLiteException ex)
            {
                //Log.Info("SQLiteEx", ex.Message);
                //throw;
            }
            return sw;
        }
        private void AddNewUser()
        {
            //throw new NotImplementedException();
            /*var user0  =new Cogazon {
                uemail = "0",
                userid = 0
                };
            CogUser.Add(user0);
            CogUser.Add(new Cogazon {
                uemail = "0",
                //uname = "0",
                //upass = "0",
                userid = 0
                }                );*/
            /*this.CogUser.Add(new Cogazon
            {
                uemail = "0",
                //uname = "0",
                //upass = "0",
                userid = 0
                //usname = "0"
            });*/
            //var a = "2";

        }
        public IEnumerable<Cogazon> GetCogAzonAsSern(int Sern)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogazon>() where adat.id == Sern select adat;
                return query.AsEnumerable();
            }
        }

        public Object GetCogAzonAsSernO(int Sern)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogazon>() where adat.id == Sern select adat;
                return query ;
            }
        }

        public IEnumerable<Cogazon> GetCogAzon()
        {
            lock (collisionLock)
            {
                return database.Query<Cogazon>("Select * from Cogazon").AsEnumerable();
                
            }
        }
        public int SaveCogAzon(Cogazon cogAzonAdat)
        {
            lock (collisionLock)
            {
                if (cogAzonAdat.id != 0)
                {
                    database.Update(cogAzonAdat);
                    return cogAzonAdat.id;
                }
                else
                {
                    database.Insert(cogAzonAdat);
                    return cogAzonAdat.id;
                }
            }
        }
        public int DeleteCogAzon(Cogazon cogAzonAdat)
        {
            var id = cogAzonAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Cogazon>(id);
                }

            }
            this.CogUser.Remove(cogAzonAdat);
            return id;
        }
        public void DeleteCogAzonAll()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Cogazon>();
            }
                
            
            
        }





        //
        public IEnumerable<MegszakadData> GetMegszakadDataAll()
        {
            lock (collisionLock)
            {
                return database.Query<MegszakadData>("Select * from MegszakadData").AsEnumerable();

            }
        }
        public IEnumerable<MegszakadData> GetMegszakadDataK()
        {
            lock (collisionLock)
            {
                return database.Query<MegszakadData>("Select * from MegszakadData where bejegyzesTipus=1").AsEnumerable();

            }
        }
        public IEnumerable<MegszakadData> GetMegszakadDataAsProjidVerAlid(int projid, string kerdivver, int kerdivalid, int bejegyzestipus)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<MegszakadData>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid && adat.bejegyzesTipus== bejegyzestipus) select adat;
                return query.AsEnumerable();
            }
        }
        public int SaveMegszakadData(MegszakadData MegszakadDataAdat)
        {
            lock (collisionLock)
            {
                if (MegszakadDataAdat.id != 0)
                {
                    database.Update(MegszakadDataAdat);
                    return MegszakadDataAdat.id;
                }
                else
                {
                    database.Insert(MegszakadDataAdat);
                    return MegszakadDataAdat.id;
                }
            }
        }
        public int DeleteMegszakadData(MegszakadData MegszakadDataAdat)
        {
            var id = MegszakadDataAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<MegszakadData>(id);
                }

            }
            this.MegszakadData.Remove(MegszakadDataAdat);
            return id;
        }
        //








        public Object GetCogDataKerdivAsSernO(int Sern)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogkerdiv>() where adat.id == Sern select adat;
                return query;
            }
        }
        public IEnumerable<Cogkerdiv> GetCogDataKerdivAsSern(int Sern)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogkerdiv>() where adat.id == Sern select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogkerdiv> GetCogDataKerdivAsProjid(int projId)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogkerdiv>() where adat.projid == projId select adat;
                return query.AsEnumerable();
            }
        }


        public IEnumerable<Cogkerdiv> GetCogDataKerdiv()
        {
            lock (collisionLock)
            {
                return database.Query<Cogkerdiv>("Select * from CogKerdiv").AsEnumerable();

            }
        }
        public int SaveCogDataKerdiv(Cogkerdiv CogDataKerdivAdat)
        {
            lock (collisionLock)
            {
                if (CogDataKerdivAdat.id != 0)
                {
                    database.Update(CogDataKerdivAdat);
                    return CogDataKerdivAdat.id;
                }
                else
                {
                    database.Insert(CogDataKerdivAdat);
                    return CogDataKerdivAdat.id;
                }
            }
        }
        public int DeleteCogDataKerdiv(Cogkerdiv CogDataKerdivAdat)
        {
            var id = CogDataKerdivAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Cogkerdiv>(id);
                }

            }
            this.CogDataKerdiv.Remove(CogDataKerdivAdat);
            return id;
        }
        public void DeleteCogDataKerdivAll()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Cogkerdiv>();
            }



        }




        public IEnumerable<Cogdata> GetCogDataAsProjid(int projid)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogdata>() where adat.projid== projid select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogdata> GetCogDataAsProjidVerAlid(int projid, string kerdivver, int kerdivalid)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid) select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogdata> GetCogDataAsProjidVerAlidKErdes(int projid, string kerdivver, int kerdivalid,string kerdes)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.alid == kerdivalid && adat.kerdes == kerdes) select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogdata> GetCogDataAsProjidVer(int projid, string kerdivver)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver==kerdivver ) select adat;
                return query.AsEnumerable();
            }
        }
        /*public IEnumerable<Cogdata> GetCogDataAsParam(int projid, string kerdivver, int alid, bool feltoltve)
        {
            lock (collisionLock)
            {
                string queri1 = "SELECT * FROM [Cogdata] ";
                string queri2 = "WHERE ";
                string queri3a = "";
                string queri3b = "";
                if (projid != null)
                {
                    queri3a=""
                }

                //return  database.QueryAsync<Cogdata>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
                var query = from adat in database.Table<Cogdata>() where (adat.projid == projid && adat.kerdivver == kerdivver  && adat.                          ) select adat;
                //return query.AsEnumerable();
            }
        }*/
        public IEnumerable<Cogdata> GetCogDataFeltoltveE(bool fele)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogdata>() where adat.feltoltve == fele select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogdata> GetCogData()
        {
            lock (collisionLock)
            {
                return database.Query<Cogdata>("Select * from Cogdata").AsEnumerable();

            }
        }
        public int UpdateCogData(Cogdata CogDataAdat)
        {
            lock (collisionLock)
            {
                database.Update(CogDataAdat);
                return CogDataAdat.id;
            }


        }

        public int SaveCogData(Cogdata CogDataAdat)
        {
            lock (collisionLock)
            {
                if (CogDataAdat.id != 0)
                {
                    database.Update(CogDataAdat);
                    return CogDataAdat.id;
                }
                else
                {
                    database.Insert(CogDataAdat);
                    return CogDataAdat.id;
                }
            }
        }
        public int DeleteCogData(Cogdata CogDataAdat)
        {
            var id = CogDataAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Cogdata>(id);
                }

            }
            this.CogData.Remove(CogDataAdat);
            return id;
        }
        public void DeleteCogDataAll()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Cogdata>();
            }



        }

        public IEnumerable<Kozponti> GetKozpontiAsSzur(string szur)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Kozponti>() where (adat.kerdes == szur) select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Kozponti> GetKozponti()
        {
            lock (collisionLock)
            {
                return database.Query<Kozponti>("Select * from Kozponti").AsEnumerable();

            }
        }

        public int SaveKozponti(Kozponti Kozponti)
        {
            lock (collisionLock)
            {
                if (Kozponti.id != 0)
                {
                    database.Update(Kozponti);
                    return Kozponti.id;
                }
                else
                {
                    database.Insert(Kozponti);
                    return Kozponti.id;
                }
            }
        }
        public int DeleteKozponti(Kozponti Kozponti)
        {
            var id = Kozponti.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Kozponti>(id);
                }

            }
            this.Kozponti.Remove(Kozponti);
            return id;
        }
        public void DeleteKozpontiAll()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Kozponti >();
            }



        }



        public IEnumerable<Cogparam> GetCogparamAsProjid(int projid)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogparam>() where adat.projid == projid select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogparam> GetCogparamAsProjidVer(int projid, string kerdivver)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogparam>() where (adat.projid == projid && adat.kerdivver== kerdivver) select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogparam> GetCogparamAsProjidVerKerdes(int projid, string kerdivver,string kerdes)
        {
            lock (collisionLock)
            {
                var query = from adat in database.Table<Cogparam>() where (adat.projid == projid && adat.kerdivver == kerdivver && adat.kerdes == kerdes) select adat;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Cogparam> GetCogparam()
        {
            lock (collisionLock)
            {
                return database.Query<Cogparam>("Select * from Cogparam").AsEnumerable();

            }
        }
        public int UpdateCogparam(Cogparam CogparamAdat)
        {
            lock (collisionLock)
            {
                database.Update(CogparamAdat);
                return CogparamAdat.id;
            }


        }

        public int SaveCogparam(Cogparam CogparamAdat)
        {
            lock (collisionLock)
            {
                if (CogparamAdat.id != 0)
                {
                    database.Update(CogparamAdat);
                    return CogparamAdat.id;
                }
                else
                {
                    database.Insert(CogparamAdat);
                    return CogparamAdat.id;
                }
            }
        }
        public int DeleteCogparam(Cogparam CogparamAdat)
        {
            var id = CogparamAdat.id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Cogparam>(id);
                }

            }
            this.CogParam.Remove(CogparamAdat);
            return id;
        }
        public void DeleteCogparamAll()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Cogparam>();
            }



        }


    }
    public class TableName
    {
        public TableName() { }
        public string name { get; set; }
    }
}
