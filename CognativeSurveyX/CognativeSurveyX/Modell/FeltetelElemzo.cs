using System;
using System.Collections.Generic;
using System.Text;

namespace CognativeSurveyX.Modell
{
    public class FeltetelElemzo
    {
        private string _Feltetel;
        public string Feltetel
        {
            get { return _Feltetel; }
            set
            {
                this._Feltetel = value;
            }

        }

        public string FeltetelVizsgalo()
        {
            var vissza_string = _Feltetel;
            if (_Feltetel.ToLower() == "(true)")
            {
                vissza_string = "true";
            }
            else if (_Feltetel.ToLower() == "(false)")
            {
                vissza_string = "false";
            }
            else
            {
                while (_Feltetel.Contains("("))
                {
                    vissza_string = zarojelKeres(vissza_string);
                    if (!vissza_string.Contains("("))
                    {
                        _Feltetel = vissza_string;
                    }

                }
            }
            


            return vissza_string;
        }

        private string zarojelKeres(string feltetel)
        {
            string vissza_string = "";
            string v1 = "";
            string v2 = "";
            int uccso_nyito = 0;
            int uccso_zaro = 0;
            uccso_nyito = feltetel.LastIndexOf("(");
            if (uccso_nyito > -1)
            {
                uccso_zaro = feltetel.IndexOf(")", uccso_nyito);
                v1 = feltetel.Substring(0, uccso_nyito );
                vissza_string = feltetel.Substring(uccso_nyito+1 , uccso_zaro-uccso_nyito-1 );
                v2 = feltetel.Substring(uccso_zaro+1);
            }
            else
            {
                vissza_string = feltetel;
            }
            vissza_string = space_torol(vissza_string);
            while (VanBenneRalacio(vissza_string) == true)
            {
                vissza_string = vissza_string.Trim();
                string relacioMegoldas = RalaciotMegold(vissza_string);
                vissza_string = relacioMegoldas;

            }
            while (VanEBenneLogika(vissza_string) > 0)
            {
                vissza_string = logikatMegold(vissza_string.Trim());
            }
            if (vissza_string == "")
            {

            }
            string vvisszaString = (v1 + vissza_string + v2);



            return vvisszaString.Trim();
        }

        

        private string RalaciotMegold(string feltetel2)
        {
            string vissza_truefalse_string = " false ";
            var feltetel = feltetel2.Replace("!=", "<>");
            string bal_oldal= "";
            string jobb_oldal= "";
            string balbal_oldal= "";
            string jobbjobb_oldal= "";
            string origString = "";
            if (VanBenneRalacio(feltetel) == true)
            {

                int kezd1= feltetel.ToLower().IndexOf(">");
                int kezd2 = feltetel.ToLower().IndexOf(">=");
                int kezd3 = feltetel.ToLower().IndexOf("=");
                int kezd4 = feltetel.ToLower().IndexOf("<");
                int kezd5 = feltetel.ToLower().IndexOf("<=");
                int kezd6 = feltetel.ToLower().IndexOf("<>");
                if (kezd1 == kezd2) { kezd1 = -1; }
                if (kezd3 == kezd2 + 1) { kezd3 = -1; }
                if (kezd3 == kezd5 + 1) { kezd3 = -1; }
                if (kezd4 == kezd5) { kezd4 = -1; }
                if (kezd6 == kezd4) { kezd4 = -1; }
                if (kezd6 == kezd1) { kezd4 = -1; }
                int csereRange1= 1;
                int csereRange2= 0;
                int relacio = 0;
                if (kezd1 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, 6) == kezd2)
                {
                    relacio = 1;
                    int kezd0 = feltetel.ToLower().IndexOf(">");
                    origString = feltetel.Substring(0, kezd0).Trim();
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 1);

                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                    }

                }

                else if (kezd3 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, kezd6) == kezd3)
                {
                    relacio = 3;
                    int kezd0= feltetel.ToLower().IndexOf("=");
                    origString = feltetel.Substring(0, kezd0 ).Trim();
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    //androidos
                    //jobb_oldal=feltetel.substring(balbal_oldal.length()+bal_oldal.length()+1);
                    //jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 2, feltetel.Length-(balbal_oldal.Length + bal_oldal.Length + 2));
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 1);

                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length+1 );
                    }
                }
                else if (kezd4 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, kezd6) == kezd4)
                {
                    relacio = 4;
                    int kezd0= feltetel.ToLower().IndexOf("<");
                    bal_oldal = feltetel.Substring(0, kezd0).Trim();
                    origString = bal_oldal;
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 1);
                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                    }
                }
                else if (kezd2 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, kezd6) == kezd2)
                {
                    relacio = 2;
                    int kezd0= feltetel.ToLower().IndexOf(">=");
                    origString = feltetel.Substring(0, kezd0).Trim();
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 2);

                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                    }

                }
                else if (kezd5 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, kezd6) == kezd5)
                {
                    relacio = 5;
                    int kezd0= feltetel.ToLower().IndexOf("<=");
                    origString = feltetel.Substring(0, kezd0).Trim();
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 2);

                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                    }
                }


                else if (kezd6 >= 0 && mini(kezd1, kezd2, kezd3, kezd4, kezd5, kezd6) == kezd6)
                {
                    relacio = 6;
                    int kezd0= feltetel.ToLower().IndexOf("<>");
                    origString = feltetel.Substring(0, kezd0).Trim();
                    bal_oldal = balSpacenalVissza(origString);
                    balbal_oldal = "";
                    if (bal_oldal != origString)
                    {
                        balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                    };
                    jobb_oldal = feltetel.Substring(balbal_oldal.Length + bal_oldal.Length + 2);

                    origString = jobb_oldal;
                    jobb_oldal = jobbSpacenalVissza(origString);
                    jobbjobb_oldal = "";
                    if (jobb_oldal != origString)
                    {
                        //androidos
                        //jobbjobb_oldal = origString.substring(jobb_oldal.length()+1);
                        jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                    }
                }
                if (!isValidFloat(bal_oldal))
                {
                    while ((VanBenneMatek1(bal_oldal)))
                    {
                        bal_oldal = matekVissza1(bal_oldal);
                    }
                    while ((VanBenneMatek2(bal_oldal)))
                    {
                        bal_oldal = matekVissza2(bal_oldal);
                    }
                    if (bal_oldal.Trim().Length> 0)
                    {
                        if (!logikaE(bal_oldal))
                        {
                            if (!isValidFloat(bal_oldal))
                            {
                                bal_oldal = KeresErtek(bal_oldal);
                            }
                        }
                    }
                }
                if (!isValidFloat(jobb_oldal))
                {
                    while ((VanBenneMatek1(jobb_oldal)))
                    {
                        jobb_oldal = matekVissza1(jobb_oldal);
                    }
                    while ((VanBenneMatek2(jobb_oldal)))
                    {
                        jobb_oldal = matekVissza2(jobb_oldal);
                    }
                    if (jobb_oldal.Trim().Length > 0)
                    {
                        if (!logikaE(jobb_oldal))
                        {
                            if (!isValidFloat(jobb_oldal))
                            {
                                jobb_oldal = KeresErtek(jobb_oldal);
                            }
                        }
                    }
                }


                if (relacio != 6 && bal_oldal.Trim().Length == 0 && jobb_oldal.Trim().Length == 0)
                {
                    vissza_truefalse_string = " true ";
                }
                else if (relacio == 6 && bal_oldal.Trim().Length == 0 && jobb_oldal.Trim().Length == 0)
                {
                    vissza_truefalse_string = " false ";
                }
                else if (relacio != 6 && bal_oldal.Trim().Length >= 0 && jobb_oldal.Trim().Length == 0)
                {
                    vissza_truefalse_string = " false ";
                }
                else if (relacio != 6 && bal_oldal.Trim().Length == 0 && jobb_oldal.Trim().Length >= 0)
                {
                    vissza_truefalse_string = " false ";
                }
                else if (relacio == 6 && bal_oldal.Trim().Length == 0 && jobb_oldal.Trim().Length >= 0)
                {
                    vissza_truefalse_string = " true ";
                }
                else if (relacio == 6 && bal_oldal.Trim().Length >= 0 && jobb_oldal.Trim().Length == 0)
                {
                    vissza_truefalse_string = " true ";
                }
                else
                {
                    if (isValidFloat(bal_oldal) && isValidFloat(jobb_oldal))
                    {
                        double b = Convert.ToDouble(bal_oldal);
                        double j = Convert.ToDouble(jobb_oldal);
    

                    if (relacio == 1 && (b > j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                        if (relacio == 2 && (b >= j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                        if (relacio == 3 && (b == j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                        if (relacio == 4 && (b < j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                        if (relacio == 5 && (b <= j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                        if (relacio == 6 && (b != j))
                        {
                            vissza_truefalse_string = " true ";
                        }
                    }
                }
            }
            string vvissza_truefalse_string = balbal_oldal + vissza_truefalse_string + jobbjobb_oldal;
            return vvissza_truefalse_string;
        }

        



        private string space_torol(string ebben)
        {
            string visszaDuma = ebben;
            bool voltCsere = true;
            while (voltCsere)
            {
                voltCsere = false;
                var visszaDumaOld = visszaDuma;
                visszaDuma = visszaDuma.Replace(" +", "+");
                visszaDuma = visszaDuma.Replace(" -", "-");
                visszaDuma = visszaDuma.Replace(" /", "/");
                visszaDuma = visszaDuma.Replace(" *", "*");
                visszaDuma = visszaDuma.Replace(" =", "=");
                visszaDuma = visszaDuma.Replace(" <", "<");
                visszaDuma = visszaDuma.Replace(" >", ">");
                visszaDuma = visszaDuma.Replace(" !", "!");
                visszaDuma = visszaDuma.Replace("+ ", "+");
                visszaDuma = visszaDuma.Replace("- ", "-");
                visszaDuma = visszaDuma.Replace("/ ", "/");
                visszaDuma = visszaDuma.Replace("* ", "*");
                visszaDuma = visszaDuma.Replace("= ", "=");
                visszaDuma = visszaDuma.Replace("< ", "<");
                visszaDuma = visszaDuma.Replace("> ", ">");
                visszaDuma = visszaDuma.Replace("! ", "!");
                if (visszaDuma != visszaDumaOld)
                {
                    visszaDumaOld = visszaDuma;
                    voltCsere = true;
                }

            }



            return visszaDuma;
        }
        private string matekVissza1(string feltetel)
        {
            string vissza_string = "";
            string vissza_string1= "";
            string vissza_string2= "";

            string bal_oldal = "";
            string jobb_oldal = "";
            string balbal_oldal= "";
            string jobbjobb_oldal = "";
            string origString = "";
            int kezdm1= feltetel.ToLower().IndexOf("*");
            int kezdm2= feltetel.ToLower().IndexOf("/");
            int matekJel= 0;
            int kezdJel = -1;
            if (kezdm1 >= 0 && mini(kezdm1, kezdm2, -1, -1, -1, -1) == kezdm1) { matekJel = 1; kezdJel = kezdm1; }
            if (kezdm2 >= 0 && mini(kezdm1, kezdm2, -1, -1, -1, -1) == kezdm2) { matekJel = 2; kezdJel = kezdm2; }
            bal_oldal = feltetel.Substring(0, kezdJel).Trim();
            origString = bal_oldal;
            bal_oldal = balSpacenalMateknelVissza(origString);
            balbal_oldal = "";
            if (bal_oldal != origString)
            {
                balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
            };
            jobb_oldal = "";

            jobb_oldal = feltetel.Substring(bal_oldal.Length + 1);
            origString = jobb_oldal;
            jobb_oldal = jobbSpacenalMateknelVissza(origString);
            jobbjobb_oldal = "";
            if (jobb_oldal != origString)
            {
                jobbjobb_oldal = origString.Substring(jobb_oldal.Length);
            };

            if (!isValidFloat(bal_oldal))
            {
                //HANEM INT, AKKOR VÁLTOZÓ VAGY PARAMÉTER
                while ((VanBenneMatek1(bal_oldal)))
                {
                    bal_oldal = matekVissza1(bal_oldal);
                }
                while ((VanBenneMatek2(bal_oldal)))
                {
                    bal_oldal = matekVissza2(bal_oldal);
                }
                if (!isValidFloat(bal_oldal))
                {
                    vissza_string1 = KeresErtek(bal_oldal);
                }
                else
                {
                    vissza_string1 = bal_oldal;
                }
            }
            else
            {
                vissza_string1 = bal_oldal;
            }
            if (!isValidFloat(jobb_oldal))
            {
                //HANEM INT, AKKOR VÁLTOZÓ VAGY PARAMÉTER
                while ((VanBenneMatek1(jobb_oldal)))
                {
                    jobb_oldal = matekVissza1(jobb_oldal);
                }
                while ((VanBenneMatek2(jobb_oldal)))
                {
                    jobb_oldal = matekVissza2(jobb_oldal);
                }
                if (!isValidFloat(jobb_oldal))
                {
                    vissza_string2 = KeresErtek(jobb_oldal);
                }
                else
                {
                    vissza_string2 = jobb_oldal;
                }
            }
            else
            {
                vissza_string2 = jobb_oldal;
            }

            if (isValidFloat(vissza_string1) && isValidFloat(vissza_string2) && matekJel == 2)
            {
                var vissza = Convert.ToDouble(vissza_string1) / Convert.ToDouble(vissza_string2);
                vissza_string = Convert.ToString(vissza);

            }
            if (isValidFloat(vissza_string1) && isValidFloat(vissza_string2) && matekJel == 1)
            {
                var vissza = Convert.ToDouble(vissza_string1)*Convert.ToDouble(vissza_string2);
                vissza_string = Convert.ToString(vissza);

            }
            string vvissza_string = (balbal_oldal + vissza_string + jobbjobb_oldal);
            return vvissza_string;
        }
        private string matekVissza2(string feltetel)
        {
            string vissza_string = "";
            string vissza_string1 = "";
            string vissza_string2 = "";

            string bal_oldal = "";
            string jobb_oldal = "";
            string balbal_oldal = "";
            string jobbjobb_oldal = "";
            string origString = "";
            int kezdm1 = feltetel.ToLower().IndexOf("+");
            int kezdm2 = feltetel.ToLower().IndexOf("-");
            int matekJel = 0;
            int kezdJel = -1;
            if (kezdm1 >= 0 && mini(kezdm1, kezdm2, -1, -1, -1, -1) == kezdm1) { matekJel = 1; kezdJel = kezdm1; }
            if (kezdm2 >= 0 && mini(kezdm1, kezdm2, -1, -1, -1, -1) == kezdm2) { matekJel = 2; kezdJel = kezdm2; }
            bal_oldal = feltetel.Substring(0, kezdJel).Trim();
            origString = bal_oldal;
            bal_oldal = balSpacenalMateknelVissza(origString);
            balbal_oldal = "";
            if (bal_oldal != origString)
            {
                balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
            };
            jobb_oldal = "";

            jobb_oldal = feltetel.Substring(bal_oldal.Length + 1);
            origString = jobb_oldal;
            jobb_oldal = jobbSpacenalMateknelVissza(origString);
            jobbjobb_oldal = "";
            if (jobb_oldal != origString)
            {
                jobbjobb_oldal = origString.Substring(jobb_oldal.Length);
            };

            if (!isValidFloat(bal_oldal))
            {
                //HANEM INT, AKKOR VÁLTOZÓ VAGY PARAMÉTER
                while ((VanBenneMatek1(bal_oldal)))
                {
                    bal_oldal = matekVissza1(bal_oldal);
                }
                while ((VanBenneMatek2(bal_oldal)))
                {
                    bal_oldal = matekVissza2(bal_oldal);
                }
                if (!isValidFloat(bal_oldal))
                {
                    vissza_string1 = KeresErtek(bal_oldal);
                }
                else
                {
                    vissza_string1 = bal_oldal;
                }
            }
            else
            {
                vissza_string1 = bal_oldal;
            }
            if (!isValidFloat(jobb_oldal))
            {
                //HANEM INT, AKKOR VÁLTOZÓ VAGY PARAMÉTER
                while ((VanBenneMatek1(jobb_oldal)))
                {
                    jobb_oldal = matekVissza1(jobb_oldal);
                }
                while ((VanBenneMatek2(jobb_oldal)))
                {
                    jobb_oldal = matekVissza2(jobb_oldal);
                }
                if (!isValidFloat(jobb_oldal))
                {
                    vissza_string2 = KeresErtek(jobb_oldal);
                }
                else
                {
                    vissza_string2 = jobb_oldal;
                }
            }
            else
            {
                vissza_string2 = jobb_oldal;
            }

            if (isValidFloat(vissza_string1) && isValidFloat(vissza_string2) && matekJel == 2)
            {
                var vissza = Convert.ToDouble(vissza_string1) - Convert.ToDouble(vissza_string2);
                vissza_string = Convert.ToString(vissza);

            }
            if (isValidFloat(vissza_string1) && isValidFloat(vissza_string2) && matekJel == 1)
            {
                var vissza = Convert.ToDouble(vissza_string1) + Convert.ToDouble(vissza_string2);
                vissza_string = Convert.ToString(vissza);

            }
            string vvissza_string = (balbal_oldal + vissza_string + jobbjobb_oldal);
            return vvissza_string;
        }
        private string KeresErtek(string valtozo)
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            string vissza_string= "";
            if (valtozo.Equals("."))
            {
                vissza_string = "";
            }
            else if (valtozo.Substring(0, 2).ToLower().Equals("pt"))
            {
                string ptIndex= valtozo.Substring(2);

                var alapAdatokParam = adatBazis.GetCogparamAsProjidVer(Convert.ToInt16(Constans.kerdivId), Constans.kerdivVer);
                //let alapAdatokParam = 
                //coredataOperations.fetchDataCogParamWhereSern(
                //  sern: Int16(_vKerdivalid), projAzon: _vKerdivid, ver: _vKerdivver) as [CogParam]
                foreach(var alapAdat in alapAdatokParam)
                {
                    if (alapAdat.kerdes == valtozo)
                    {
                        vissza_string = alapAdat.valasz;
                        break;
                    }
                }
            }

            else
            {
                int vartipus = 2;
                //0 normal
                //1 param
                //2 multi
                if (valtozo.Substring(0, 1).Trim().ToLower().Equals("pt"))
                {
                    vartipus = 1;
                }
                else
                {
                    foreach(var kerdes in Constans.aktSurvey.questions)
                    {
                        if (kerdes.question_type == valtozo)
                        {
                            vartipus = 0;
                        }

                    }
                }


                var adatValaszok=adatBazis.GetCogDataAsProjidVerAlid(Convert.ToInt16(Constans.kerdivId), Constans.kerdivVer,Constans.kerdivAlid);
                bool vissza = false;
                foreach (var adatValasz in adatValaszok)
                {
                    if (vartipus < 3)
                    {
                        if (adatValasz.kerdes.Equals(valtozo))
                        {
                            vissza_string = (adatValasz.valasz.Trim());
                            vissza = true;
                        }
                    }
                    else
                    {
                        int kezd = valtozo.IndexOf("_");
                        string v1 = valtozo.Substring(0, kezd - 1);
                        string v2 = valtozo.Substring(kezd + 1);
                        if ((adatValasz.kerdes.Trim().Equals(v1))
                            //&&  String(adatValasz.kisid) == v2  
                            )
                        {
                            vissza_string = (adatValasz.valasz.Trim());
                            vissza = true;
                        }
                    }
                }

            }
              
                return vissza_string;
        }
        private string balSpacenalVissza(string duma)
        {
            string visszaString = "";
            int uccso = duma.LastIndexOf(" ");
            if (uccso == -1)
            {
                visszaString = duma;
            }
            else
            {
                visszaString = duma.Substring(uccso+1);

            }
            return visszaString;

        }
        private string jobbSpacenalVissza(string duma)
        {
            string visszaString = "";
            int elso = duma.IndexOf(" ");
            if (elso == -1)
            {
                visszaString = duma;
            }
            else
            {
                visszaString = duma.Substring(0,elso);

            }
            return visszaString;
        }
        private string balSpacenalMateknelVissza(string duma)
        {
            string visszaString = "";
            int uccso1= duma.IndexOf(" ");
            int uccso2= duma.IndexOf("*");
            int uccso3= duma.IndexOf("/");
            int uccso4= duma.IndexOf("+");
            int uccso5= duma.IndexOf("-");
            if (uccso1 < 0 && uccso2 < 0 && uccso3 < 0 && uccso4 < 0 && uccso5 < 0)
            {
                visszaString = duma;
            }
            else
            {
                int minima = mini(uccso1, uccso2, uccso3, uccso4, uccso5, -1);
                if (uccso1 >= 0 && minima == uccso1)
                {
                    visszaString = duma.Substring(uccso1);
                }
                else if (uccso2 >= 0 && minima == uccso2)
                {
                    visszaString = duma.Substring(uccso2);
                }
                else if (uccso3 >= 0 && minima == uccso3)
                {
                    visszaString = duma.Substring(uccso3);
                }
                else if (uccso4 >= 0 && minima == uccso4)
                {
                    visszaString = duma.Substring(uccso4);
                }
                else if (uccso5 >= 0 && minima == uccso5)
                {
                    visszaString = duma.Substring(uccso5);
                }

            }

            return visszaString;
        }
        private string jobbSpacenalMateknelVissza(string duma)
        {
            string visszaString = "";
            int elso1 = duma.IndexOf(" ");
            int elso2 = duma.IndexOf("*");
            int elso3 = duma.IndexOf("/");
            int elso4 = duma.IndexOf("+");
            int elso5 = duma.IndexOf("-");
            if (elso1 < 0 && elso2 < 0 && elso3 < 0 && elso4 < 0 && elso5 < 0)
            {
                visszaString = duma;
            }
            else
            {
                int minima = mini(elso1, elso2, elso3, elso4, elso5, -1);
                if (elso1 >= 0 && minima == elso1)
                {
                    visszaString = duma.Substring(0, elso1);
                }
                else if (elso2 >= 0 && minima == elso2)
                {
                    visszaString = duma.Substring(0, elso2);
                }
                else if (elso3 >= 0 && minima == elso3)
                {
                    visszaString = duma.Substring(0, elso3);
                }
                else if (elso4 >= 0 && minima == elso4)
                {
                    visszaString = duma.Substring(0, elso4);
                }
                else if (elso5 >= 0 && minima == elso5)
                {
                    visszaString = duma.Substring(0, elso5);
                }
            }

            return visszaString;
        }


        private int mini(int kezd1, int kezd2, int kezd3, int kezd4, int kezd5, int kezd6)
        {
            int vissza = 0;

            int[] sortomb = new int[] { 0, 0, 0, 0, 0, 0 } ;
            int sortombindex = -1;
            if (kezd1>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd1;}
            if (kezd2>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd2;}
            if (kezd3>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd3;}
            if (kezd4>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd4;}
            if (kezd5>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd5;}
            if (kezd6>=0) { sortombindex += 1;  sortomb[sortombindex] = kezd6;}
        
            bool csere = true;
            while (csere)
            {
                csere = false;
                for (int i=0; i<sortombindex;i++)
                {
                    if (sortomb[i] > sortomb[i + 1])
                    {
                        int tmp= sortomb[i];
                        sortomb[i] = sortomb[i + 1];
                        sortomb[i + 1] = tmp;
                        csere = true;
                    }
                }
            }
    
            if (kezd1==sortomb[0]) { vissza = kezd1; }
            if (kezd2==sortomb[0]) {vissza=kezd2; }
            if (kezd3==sortomb[0]) {vissza=kezd3; }
            if (kezd4==sortomb[0]) {vissza=kezd4; }
            if (kezd5==sortomb[0]) {vissza=kezd5; }
            if (kezd6==sortomb[0]) {vissza=kezd6; }
    
            return vissza;
        }
        private bool VanBenneRalacio(string feltetel)
        {
            bool vissza_truefalse= false;
            int kezd1 = feltetel.ToLower().IndexOf(">");
            int kezd2 = feltetel.ToLower().IndexOf("=");
            int kezd3 = feltetel.ToLower().IndexOf("<");
            if (kezd1>=0 || kezd2>=0 || kezd3>=0 ) {
                vissza_truefalse = true;
            }
            return vissza_truefalse;
        }
        private bool VanBenneMatek1(string feltetel)
        {
            bool vissza_truefalse = false;
            int kezd1 = feltetel.ToLower().IndexOf("*");
            int kezd2 = feltetel.ToLower().IndexOf("/");
            if (kezd1 >= 0 || kezd2 >= 0 )
            {
                vissza_truefalse = true;
            }
            return vissza_truefalse;
        }
        private bool VanBenneMatek2(string feltetel)
        {
            bool vissza_truefalse = false;
            int kezd1 = feltetel.ToLower().IndexOf("+");
            int kezd2 = feltetel.ToLower().IndexOf("-");
            if (kezd1 >= 0 || kezd2 >= 0)
            {
                vissza_truefalse = true;
            }
            return vissza_truefalse;
        }
        private string logikatMegold(string feltetel)
        {
            var vissza_logika= "";
            //ha and van benne
            string bal_oldal= "";
            string jobb_oldal= "";
            string balbal_oldal = "";
            string jobbjobb_oldal = "";
            string origString = "";
            int kezd1 = -1;
            int logikaAndVagyOr= VanEBenneLogika(feltetel);
            if (logikaAndVagyOr >= 1)
            {
                if (logikaAndVagyOr == 1)
                {
                    kezd1 = feltetel.ToLower().IndexOf(" and ");
                }
                else if (logikaAndVagyOr == 2)
                {
                    kezd1 = feltetel.ToLower().IndexOf(" or ");
                }
                bal_oldal = feltetel.Substring(0, kezd1).Trim();
                origString = bal_oldal;
                bal_oldal = balSpacenalVissza(origString);
                balbal_oldal = "";
                if (bal_oldal != origString)
                {
                    balbal_oldal = origString.Substring(0, origString.Length - bal_oldal.Length);
                }
                //androidos
                //jobb_oldal = feltetel.substring(kezd1+4).trim();
                //jobb_oldal = feltetel.Substring(kezd1 + 4, feltetel.Length).Trim();
                jobb_oldal = feltetel.Substring(kezd1 + 4).Trim();

                origString = jobb_oldal;
                jobb_oldal = jobbSpacenalVissza(origString);
                jobbjobb_oldal = "";
                if (jobb_oldal != origString)
                {
                    //androidos
                    //jobbjobb_oldal = origString.substring(jobb_oldal.length() + 1);
                    //jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1, origString.Length);
                    jobbjobb_oldal = origString.Substring(jobb_oldal.Length + 1);
                }
                bool bal_oldal_logika_e= logikaE(bal_oldal);
                bool jobb_oldal_logika_e = logikaE(jobb_oldal);
                if (bal_oldal_logika_e == false || jobb_oldal_logika_e == false)
                {
                }
                else if (logikaAndVagyOr == 1)
                {
                    if (bal_oldal.Equals("true") && jobb_oldal.Equals("true"))
                    {
                        vissza_logika = " true ";
                    }
                    else if (bal_oldal.Equals("false") && jobb_oldal.Equals("false"))
                    {
                        vissza_logika = " true ";
                    }
                    else if (bal_oldal.Equals("true") && jobb_oldal.Equals("false"))
                    {
                        vissza_logika = " false ";
                    }
                    else if (bal_oldal.Equals("false") && jobb_oldal.Equals("true"))
                    {
                            vissza_logika = " false ";
                    }
                }
                else if (logikaAndVagyOr == 2)
                {
                    if (bal_oldal.Equals("true") && jobb_oldal.Equals("true"))
                    {
                        vissza_logika = " true ";
                    }
                    else if (bal_oldal.Equals("false") && jobb_oldal.Equals("false"))
                    {
                        vissza_logika = " false ";
                    }
                    else if (bal_oldal.Equals("true") && jobb_oldal.Equals("false"))
                    {
                        vissza_logika = " true ";
                    }
                    else if (bal_oldal.Equals("false") && jobb_oldal.Equals("true"))
                    {
                        vissza_logika = " true ";
                    }
                }
            }

            string vvissza_logika= balbal_oldal + vissza_logika + jobbjobb_oldal;
            return vvissza_logika;

        }
        private int VanEBenneLogika(string feltetel)
        {
            int vissza_logika = 0;
            int kezd1 = feltetel.ToLower().IndexOf(" and ");
            int kezd2 = feltetel.ToLower().IndexOf(" or ");
            if (kezd1 >= 0)
            {
                vissza_logika = 1;
            }
            if (kezd2 >= 0)
            {
                vissza_logika = 2;
            }

            return vissza_logika;
        }
        private bool logikaE(string oldal)
        {
            bool vissza_logika= false;
            if (oldal.ToLower().Trim().Equals("true"))
            {
                vissza_logika = true;
            }
            else if (oldal.ToLower().Trim().Equals("false"))
            {
                vissza_logika = true;
            }
            return vissza_logika;

        }


        private bool isValidFloat(string duma)
        {
            bool visszaBool = true;
            double doubleResult;
            var vissza= double.TryParse(duma, out doubleResult);
            if (doubleResult == 0) { visszaBool = false; }
            return visszaBool;
            //return double.IsNaN(Convert.ToDouble(duma));
        }

    }

}
