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
        private SQLiteConnection database;

        public ObservableCollection<Cogazon> CogUser { get; set; }
        public ObservableCollection<Cogkerdiv> CogDataKerdiv { get; set; }
        public ObservableCollection<Cogdata> CogData { get; set; }
        public ObservableCollection<Cogparam> CogParam { get; set; }

        public UsersDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
           
            database.CreateTable<Cogazon>();
            database.CreateTable<Cogkerdiv>();
            database.CreateTable<Cogdata>();
            database.CreateTable<Cogparam>();


            this.CogUser = new ObservableCollection<Cogazon>(database.Table<Cogazon>());
            this.CogDataKerdiv = new ObservableCollection<Cogkerdiv>(database.Table<Cogkerdiv>());
            this.CogData = new ObservableCollection<Cogdata>(database.Table<Cogdata>());
            this.CogParam = new ObservableCollection<Cogparam>(database.Table<Cogparam>());

            if (!database.Table<Cogazon>().Any())
            {
                //AddNewUser();
            }

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
}
