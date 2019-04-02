using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using Xamarin.Forms;
using System.IO;
using CognativeSurveyX.Fregments;
using CognativeSurveyX.Controls;
using Plugin.Connectivity;
using System.Linq;
using CognativeSurveyX.Data;
using System.Diagnostics;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using CognativeSurveyX.myDataBase;

namespace CognativeSurveyX.Modell
{
    class Constans
    {
        public struct ParamData
        {
            public ParamData(string idValue, string param1Value, string param2Value, string param3Value)
            {
                IdData = idValue;
                Param1Data = param1Value;
                Param2Data = param2Value;
                Param3Data = param3Value;
            }

            public string IdData { get; private set; }
            public string Param1Data { get; private set; }
            public string Param2Data { get; private set; }
            public string Param3Data { get; private set; }
        }

        public struct LayoutTomb
        {
            public LayoutTomb(string neve, StackLayout layout)
            {
                Neve = neve;
                Layout = layout;
                
            }

            public string Neve { get; private set; }
            public StackLayout Layout { get; private set; }
            
        }
        

        public static void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            int zipDarab = 0;
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                        zipDarab = zipDarab + 1;
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources

                }
            }
        }
        public static string RemoveNewLines(string input)
        {
            return input.Replace("\r\n", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace("\r", string.Empty);
        }
        //public static ParamData param = new List<Constans.ParamData>();
        public static Dictionary<string, StackLayout> myLayout = new Dictionary<string, StackLayout>();
        
        public static Dictionary<string, string> myParam = new Dictionary<string, string>();
        public static List<Tuple<string, string, string,int>> myParam2 = new List<Tuple<string, string,string,int>>();

        public static int paramNetkapcsolat = 1;
        public static string csekbox_false = "☐";
        public static string csekbox_true = "☒";

        public static  string bumbuc_false = "⚪";
        public static string bumbuc_true = "⚫";
        public static Questions aktSurvey = new Questions();
        public static Questions.Question aktQuestion = new Questions.Question();
        public static int pageNumber = -100;
        public static Color BackgroundColor = Color.FromRgb(58, 153, 212);
        public static Color MainTextColor = Color.White;
        public static string webUrl = "http://qnr.cognative.hu/cogsurv/regist_ios2.php";
        public static string uploadUrl = "http://qnr.cognative.hu/cogsurv/file_upload_ios.php";
        public static string webUrlfeltolt = "http://qnr.cognative.hu/cogsurv/feltolt_xam.php";
        public static string downUrl = "http://qnr.cognative.hu/cogsurv/";
        public static string myZipPath = "";
        public static string myFilePath = "";
        public static string myZipFile = "";
        public static object errorDuma = "";
        public static List<string> kellZip = new List<string>();
        public static int kellZipIndex = 0;
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static Exception exception;
        public static string valaszok = "";

        public static string kerdivId = "";
        public static string kerdivVer = "";
        public static string kerdivUser = "33";
        public static string kerdivTip = "";
        public static int kerdivAlid = 0;

        public static double kerdivGPSLongitude = 0;
        public static double kerdivGPSLatitude = 0;

        public static string kerdivPlatformGep = "";
        public static string kerdivPlatformSoftver = "";


        //List<string> paramFromId = new List<string, int>();
        //public static string 

        public static  void nextPage()
        {
            ValaszokKiiratasa(valaszok);
            //bool ruleTovabb=ruleTombVizsgal();
            bool ruleTovabb = true;
            if (ruleTovabb)
            {
                bool keresem = true;
                while (keresem)
                {

                    Constans.pageNumber++;
                    if (pageNumber <= aktSurvey.questions.Count + 1)
                    {
                        int idxa = 0;
                        foreach (var item in aktSurvey.questions)
                        {
                            idxa++;
                            if (idxa == pageNumber)
                            {
                                if (item.visible) { keresem = false; break; }
                            }

                        }
                    }
                    else
                    {
                        keresem = false;
                    }
                    
                }
            }
            //Constans.pageNumber++;
            int idx = 0;
            //aktQuestion = "";
            foreach (var item in aktSurvey.questions)
            {
                idx++;
                if (idx == pageNumber)
                {
                    aktQuestion = item;
                }
                
            }
            if (pageNumber  == aktSurvey.questions.Count + 1)
            {
                aktQuestion.kerdeskod = "VEGE";
                aktQuestion.question_title = aktSurvey.survey_properties.end_message;
                aktQuestion.question_type = "vegeIntro";
                valaszok = "VEGE=VEGE";
            }
            if (pageNumber == aktSurvey.questions.Count + 2)
            {
                aktQuestion.question_type = "vege";
            }

                if (Constans.aktQuestion.question_type == "Radioboxes")
            {
                //Navigation.PushModalAsync(new Radioboxes());
                
            }
        }

        public static string sorszamErtek()
        {
            string visszaErtek = "";

            return "SN:"+vezetonulla(kerdivId,5)+"_"+ vezetonulla(kerdivVer, 3)+"_"  + vezetonulla(Convert.ToString(kerdivAlid), 4);
        }

        private static string vezetonulla(string duma, int darab)
        {
            string nulla = "";
            for (var i = 1; i <= darab; i++)
            {
                nulla = nulla + "0";
            }
            return nulla + duma;
        }

        private static bool ruleTombVizsgal()
        {
            bool vissza = true;
            UsersDataAccess adatBazis = new UsersDataAccess();
            if (aktQuestion.rules != null)
            {
                foreach (var aktrule in aktQuestion.rules)
                {
                    var rulesTomb = aktrule.Split(Convert.ToChar(" "));
                    string rulesTomb0 = rulesTomb[0];
                    if (rulesTomb0.ToLower().Equals("if"))
                    {
                        MegszakadData megszakadtData = new MegszakadData
                        {
                            alid = kerdivAlid,
                            szoveg = aktrule,
                            bejegyzesTipus =2,
                            kerdivver = kerdivVer,
                            projid = Convert.ToInt16(kerdivId),
                            kerdivdate = TimeS(DateTime.Now)
                        };
                        adatBazis.SaveMegszakadData(megszakadtData);

                        vissza = ruleElemez(aktrule);
                        if (!vissza)
                        {
                            break;
                        }
                    }

                }
            }
            
            return vissza;
        }

        private static bool ruleElemez(string aktrule)
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            bool vissza = true;


            string feltetel = aktrule.Substring(3).Trim();
            if (feltetel.IndexOf("(") >= 0)
            {
                string feltetelTorzs = feltetelTorzsKeres(feltetel);
                string feltetelFeladat = feltetel.Substring(feltetelTorzs.Length).Trim();
                var feltetelElemzo = new FeltetelElemzo();
                feltetelElemzo.Feltetel = feltetelTorzs;
                string feltetel_vissza = (feltetelElemzo.FeltetelVizsgalo().ToLower().Trim());
                if (feltetel_vissza.ToLower().Equals("true"))
                {
                    if (feltetelFeladat.Substring(0, "nogo(".Length).Equals("NoGO("))
                    {
                        vissza = false;
                        //string hibajegy = feltetelFeladat.Substring(6, feltetelFeladat.Length - 1);
                        //break;
                    }
                    else if (feltetelFeladat.Substring(0, "PTE(".Length).Equals("PTE("))
                    {
                        var al1 = feltetelFeladat.Length;
                        var al2 = feltetelFeladat.IndexOf(";");

                        string paramKod = feltetelFeladat.Substring(4, feltetelFeladat.IndexOf(";") - 4);
                        string paramErtek = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        Cogparam cogparam = new Cogparam
                        {
                            alid = kerdivAlid,
                            egyedi1 = "",
                            egyedi2 = "",
                            egyedi3 = "",
                            egyedi4 = "",
                            kerdes = "PT" + paramKod,
                            valasz = paramErtek,
                            kerdivtip = Convert.ToInt16(kerdivTip),
                            kerdivver = kerdivVer,
                            projid = Convert.ToInt16(kerdivId),
                            kerdivdate = TimeS(DateTime.Now)
                        };
                        adatBazis.SaveCogparam(cogparam);
                        //kirakParamRecord(vKerdivid, vKerdivalid, vKerdivtip, vKerdivver, 0, "PT" + paramKod, paramErtek, "", "", "", "", vDate);
                    }
                    else if (feltetelFeladat.Substring(0, "VQ(".Length).Equals("VQ("))
                    {
                        string varOut = feltetelFeladat.Substring("VQ(".Length, feltetelFeladat.IndexOf(";") - "VQ(".Length);
                        string paramErtek = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        foreach (var kerdes in aktSurvey.questions)
                        {
                            if (kerdes.kerdeskod.Equals(varOut))
                            {
                                kerdes.visible = Convert.ToBoolean(paramErtek);
                            }
                        }
                    }
                    else if (feltetelFeladat.Substring(0, "VA(".Length).Equals("VA("))
                    {
                        string varOut = feltetelFeladat.Substring("VA(".Length, feltetelFeladat.IndexOf(",") - "VA(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "VA(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varVisible = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        visibleChoiceBeallit(varOut, Convert.ToInt16(varOutErtek), Convert.ToBoolean(varVisible.ToLower()));

                    }
                    else if (feltetelFeladat.Substring(0, "VI(".Length).Equals("VI("))
                    {
                        string varOut = feltetelFeladat.Substring("VI(".Length, feltetelFeladat.IndexOf(",") - "VI(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "VI(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varVisible = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        visibleItemBeallit(varOut, Convert.ToInt16(varOutErtek), Convert.ToBoolean(varVisible.ToLower()));

                    }
                    else if (feltetelFeladat.Substring(0, "QLIE(".Length).Equals("QLIE("))
                    {
                        string varOut = feltetelFeladat.Substring("QLIE(".Length, feltetelFeladat.IndexOf(",") - "QLIE(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "QLIE(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varIn = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        string varInErtek = keresErtek(varIn);



                        if (varOut.ToLower().Equals("pt"))
                        {
                            Cogparam cogparam = new Cogparam
                            {
                                alid = kerdivAlid,
                                egyedi1 = "",
                                egyedi2 = "",
                                egyedi3 = "",
                                egyedi4 = "",
                                kerdes = "PT" + varOutErtek,
                                valasz = varInErtek,
                                kerdivtip = Convert.ToInt16(kerdivTip),
                                kerdivver = kerdivVer,
                                projid = Convert.ToInt16(kerdivId),
                                kerdivdate = TimeS(DateTime.Now)
                            };
                            adatBazis.SaveCogparam(cogparam);
                            //kirakParamRecord(vKerdivid, vKerdivalid, vKerdivtip, vKerdivver, 0, "PT" + varOutErtek, varInErtek, "", "", "", "", vDate);
                        }
                        else
                        {
                            foreach (var kerdes in aktSurvey.questions)
                            {
                                if (kerdes.kerdeskod.Equals(varOut))
                                {
                                    kerdes.items[Convert.ToInt16(varOutErtek) - 1] = varInErtek;
                                    break;
                                }
                            }
                        }

                    }
                    else if (feltetelFeladat.Substring(0, "QLE(".Length).Equals("QLE("))
                    {
                        string varOut = feltetelFeladat.Substring(4, feltetelFeladat.IndexOf(",") - 4);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "QLE(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varIn = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        string varInErtek = keresErtek(varIn);

                        if (varOut.ToLower().Equals("pt"))
                        {
                            Cogparam cogparam = new Cogparam
                            {
                                alid = kerdivAlid,
                                egyedi1 = "",
                                egyedi2 = "",
                                egyedi3 = "",
                                egyedi4 = "",
                                kerdes = "PT" + varOutErtek,
                                valasz = varInErtek,
                                kerdivtip = Convert.ToInt16(kerdivTip),
                                kerdivver = kerdivVer,
                                projid = Convert.ToInt16(kerdivId),
                                kerdivdate = TimeS(DateTime.Now)
                            };
                            adatBazis.SaveCogparam(cogparam);
                            //kirakParamRecord(vKerdivid, vKerdivalid, vKerdivtip, vKerdivver, 0, "PT" + varOutErtek, varInErtek, "", "", "", "", vDate);
                        }
                        else
                        {
                            foreach (var kerdes in aktSurvey.questions)
                            {
                                if (kerdes.kerdeskod.Equals(varOut))
                                {
                                    kerdes.choices[Convert.ToInt16(varOutErtek) - 1] = varInErtek;
                                    break;
                                }
                            }
                        }

                    }
                }
            }



            return vissza;
        }

        private static bool ruleElemezFolytat(string aktrule)
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            bool vissza = true;


            string feltetel = aktrule.Substring(3).Trim();
            if (feltetel.IndexOf("(") >= 0)
            {
                string feltetelTorzs = feltetelTorzsKeres(feltetel);
                string feltetelFeladat = feltetel.Substring(feltetelTorzs.Length).Trim();
                var feltetelElemzo = new FeltetelElemzo();
                feltetelElemzo.Feltetel = feltetelTorzs;
                string feltetel_vissza = (feltetelElemzo.FeltetelVizsgalo().ToLower().Trim());
                if (feltetel_vissza.ToLower().Equals("true"))
                {
                    if (feltetelFeladat.Substring(0, "nogo(".Length).Equals("NoGO("))
                    {
                        //vissza = false;
                        //string hibajegy = feltetelFeladat.Substring(6, feltetelFeladat.Length - 1);
                        //break;
                    }
                    else if (feltetelFeladat.Substring(0, "PTE(".Length).Equals("PTE("))
                    {
                        /*var al1 = feltetelFeladat.Length;
                        var al2 = feltetelFeladat.IndexOf(";");

                        string paramKod = feltetelFeladat.Substring(4, feltetelFeladat.IndexOf(";") - 4);
                        string paramErtek = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        Cogparam cogparam = new Cogparam
                        {
                            alid = kerdivAlid,
                            egyedi1 = "",
                            egyedi2 = "",
                            egyedi3 = "",
                            egyedi4 = "",
                            kerdes = "PT" + paramKod,
                            valasz = paramErtek,
                            kerdivtip = Convert.ToInt16(kerdivTip),
                            kerdivver = kerdivVer,
                            projid = Convert.ToInt16(kerdivId),
                            kerdivdate = TimeS(DateTime.Now)
                        };
                        adatBazis.SaveCogparam(cogparam);*/
                        
                    }
                    else if (feltetelFeladat.Substring(0, "VQ(".Length).Equals("VQ("))
                    {
                        string varOut = feltetelFeladat.Substring("VQ(".Length, feltetelFeladat.IndexOf(";") - "VQ(".Length);
                        string paramErtek = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        foreach (var kerdes in aktSurvey.questions)
                        {
                            if (kerdes.kerdeskod.Equals(varOut))
                            {
                                kerdes.visible = Convert.ToBoolean(paramErtek);
                            }
                        }
                    }
                    else if (feltetelFeladat.Substring(0, "VA(".Length).Equals("VA("))
                    {
                        string varOut = feltetelFeladat.Substring("VA(".Length, feltetelFeladat.IndexOf(",") - "VA(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "VA(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varVisible = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        visibleChoiceBeallit(varOut, Convert.ToInt16(varOutErtek), Convert.ToBoolean(varVisible.ToLower()));

                    }
                    else if (feltetelFeladat.Substring(0, "VI(".Length).Equals("VI("))
                    {
                        string varOut = feltetelFeladat.Substring("VI(".Length, feltetelFeladat.IndexOf(",") - "VI(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "VI(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varVisible = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        visibleItemBeallit(varOut, Convert.ToInt16(varOutErtek), Convert.ToBoolean(varVisible.ToLower()));

                    }
                    else if (feltetelFeladat.Substring(0, "QLIE(".Length).Equals("QLIE("))
                    {
                        string varOut = feltetelFeladat.Substring("QLIE(".Length, feltetelFeladat.IndexOf(",") - "QLIE(".Length);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "QLIE(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varIn = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        string varInErtek = keresErtek(varIn);



                        if (varOut.ToLower().Equals("pt"))
                        {
                            /*Cogparam cogparam = new Cogparam
                            {
                                alid = kerdivAlid,
                                egyedi1 = "",
                                egyedi2 = "",
                                egyedi3 = "",
                                egyedi4 = "",
                                kerdes = "PT" + varOutErtek,
                                valasz = varInErtek,
                                kerdivtip = Convert.ToInt16(kerdivTip),
                                kerdivver = kerdivVer,
                                projid = Convert.ToInt16(kerdivId),
                                kerdivdate = TimeS(DateTime.Now)
                            };
                            adatBazis.SaveCogparam(cogparam);*/
                            //kirakParamRecord(vKerdivid, vKerdivalid, vKerdivtip, vKerdivver, 0, "PT" + varOutErtek, varInErtek, "", "", "", "", vDate);
                        }
                        else
                        {
                            foreach (var kerdes in aktSurvey.questions)
                            {
                                if (kerdes.kerdeskod.Equals(varOut))
                                {
                                    kerdes.items[Convert.ToInt16(varOutErtek) - 1] = varInErtek;
                                    break;
                                }
                            }
                        }

                    }
                    else if (feltetelFeladat.Substring(0, "QLE(".Length).Equals("QLE("))
                    {
                        string varOut = feltetelFeladat.Substring(4, feltetelFeladat.IndexOf(",") - 4);
                        feltetelFeladat = feltetelFeladat.Substring(varOut.Length + "QLE(".Length + 1);
                        string varOutErtek = feltetelFeladat.Substring(0, feltetelFeladat.IndexOf(";"));
                        string varIn = feltetelFeladat.Substring(feltetelFeladat.IndexOf(";") + 1, feltetelFeladat.Length - feltetelFeladat.IndexOf(";") - 2);
                        string varInErtek = keresErtek(varIn);

                        if (varOut.ToLower().Equals("pt"))
                        {
                            /*Cogparam cogparam = new Cogparam
                            {
                                alid = kerdivAlid,
                                egyedi1 = "",
                                egyedi2 = "",
                                egyedi3 = "",
                                egyedi4 = "",
                                kerdes = "PT" + varOutErtek,
                                valasz = varInErtek,
                                kerdivtip = Convert.ToInt16(kerdivTip),
                                kerdivver = kerdivVer,
                                projid = Convert.ToInt16(kerdivId),
                                kerdivdate = TimeS(DateTime.Now)
                            };
                            adatBazis.SaveCogparam(cogparam);*/
                            //kirakParamRecord(vKerdivid, vKerdivalid, vKerdivtip, vKerdivver, 0, "PT" + varOutErtek, varInErtek, "", "", "", "", vDate);
                        }
                        else
                        {
                            foreach (var kerdes in aktSurvey.questions)
                            {
                                if (kerdes.kerdeskod.Equals(varOut))
                                {
                                    kerdes.choices[Convert.ToInt16(varOutErtek) - 1] = varInErtek;
                                    break;
                                }
                            }
                        }

                    }
                }
            }



            return vissza;
        }

        public static string ParamErtekeBeilleszt(string duma)
        {
            string vissza_string = duma;
            var kezd = vissza_string.IndexOf("<<");
            while (kezd > 0 && kezd!= vissza_string.Length)
            {
                var vege = vissza_string.IndexOf(">>", kezd + 1);
                if (vege>0)
                {
                    var duma1 = vissza_string.Substring(0, kezd );
                    var duma2a = "PT" + vissza_string.Substring(kezd + 2, vege - kezd-2 ).Trim();
                    var duma2 = keresErtek(duma2a);
                    var duma3 = vissza_string.Substring(vege+2);
                    vissza_string = duma1 + duma2 + duma3;
                }
                kezd = vissza_string.IndexOf("<<",kezd+2);
            }

            return vissza_string;
        }
        private static string keresErtek(string valtozo)
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            string vissza_string = "";
            if (valtozo.Equals("."))
            {
                vissza_string = "";
            }
            else if (valtozo.Substring(0, 2).ToLower().Equals("pt"))
            {
                string ptIndex = valtozo.Substring(2);

                var alapAdatokParam = adatBazis.GetCogparamAsProjidVer(Convert.ToInt16(Constans.kerdivId), Constans.kerdivVer);
                //let alapAdatokParam = 
                //coredataOperations.fetchDataCogParamWhereSern(
                //  sern: Int16(_vKerdivalid), projAzon: _vKerdivid, ver: _vKerdivver) as [CogParam]
                foreach (var alapAdat in alapAdatokParam)
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
                    foreach (var kerdes in Constans.aktSurvey.questions)
                    {
                        if (kerdes.question_type == valtozo)
                        {
                            vartipus = 0;
                        }

                    }
                }


                var adatValaszok = adatBazis.GetCogDataAsProjidVerAlid(Convert.ToInt16(Constans.kerdivId), Constans.kerdivVer,kerdivAlid);
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

        private static void visibleItemBeallit(string varOut, int varOutErtek, bool trueFalse)
        {
            foreach (var kerdes in aktSurvey.questions)
            {
                if (kerdes.kerdeskod.Equals(varOut))
                {
                    kerdes.itemVisible[varOutErtek - 1] = trueFalse;
                    break;
                }
            }

            bool kellE = false;
            foreach (var kerdes in aktSurvey.questions)
            {
                if (kerdes.kerdeskod.Equals(varOut))
                {
                    foreach (var csojszVi in kerdes.itemVisible)
                    {
                        if (csojszVi)
                        {
                            kellE = true;
                            break;
                        }
                    }
                }
            }

            if (!kellE)
            {
                foreach (var kerdes in aktSurvey.questions)
                {
                    kerdes.visible = false;
                }
            }
        }

        private static void visibleChoiceBeallit(string varOut, int varOutErtek, bool trueFalse)
        {
            foreach(var kerdes in aktSurvey.questions)
            {
                if (kerdes.kerdeskod.Equals(varOut))
                {
                    kerdes.choicesVisible[varOutErtek - 1] = trueFalse;
                    break;
                 }
            }

            bool kellE = false;
            foreach (var kerdes in aktSurvey.questions)
            {
                if (kerdes.kerdeskod.Equals(varOut))
                {
                    foreach(var csojszVi in kerdes.choicesVisible)
                    {
                        if (csojszVi )
                        {
                            kellE = true;
                            break;
                        }
                    }
                }
            }
                
            if (!kellE)
            {
                foreach (var kerdes in aktSurvey.questions)
                {
                    kerdes.visible = false;
                }
            }
        }

        private static string feltetelTorzsKeres(string feltetel)
        {
            string visszaS = feltetel;
            int nyitDb = 0;
            int zarDb = 0;
            int idx = 0;
            foreach(var betu in feltetel.ToCharArray())
            {
                if (betu == Convert.ToChar("("))
                {
                    nyitDb += 1;
                }
                if (betu == Convert.ToChar(")"))
                {
                    zarDb += 1;
                }
                if (nyitDb > 0 && nyitDb == zarDb)
                {
                    visszaS = feltetel.Substring(0, idx+1);
                    break;
                }
                idx += 1;
            }
            return visszaS;

        }

        private static void ValaszokKiiratasa(string valasz)
        {
            UsersDataAccessAsync adatBazis = new UsersDataAccessAsync();

            //utolso kiirt kérdés
            //van_e már ez a projekt
            /*var megszakadtKerdivek = adatBazis.GetMegszakadDataAsProjidVerAlid(Convert.ToInt16(kerdivId), kerdivVer, kerdivAlid, 1);
            foreach (var item in megszakadtKerdivek)
            {
                item.szoveg = Constans.aktQuestion.kerdeskod;
                adatBazis.SaveMegszakadData(item);
            }*/


            var darabol = valasz.Split(Convert.ToChar(";"));
            foreach(var item in darabol)
            {
                if (item.Length>0)
                {
                    var darabol2 = item.Split(Convert.ToChar("="));
                    //long mostDate1 = TimeS(DateTime.Now);
                    darabol2[0] = "bee";
                    darabol2[1] = "111";
                    var idd2 =  adatBazis.SaveCogDataAsync(new Cogdata
                    {
                        kerdes = darabol2[0],
                        valasz = darabol2[1],
                        kerdivdate = TimeS(DateTime.Now),
                        egyedi2 = kerdivPlatformGep,
                        egyedi3 = kerdivPlatformSoftver,
                        kerdivver = kerdivVer,
                        kerdivtip = Convert.ToInt16(kerdivTip),
                        projid = Convert.ToInt16(kerdivId),
                        alid = kerdivAlid,
                        egyedi1 = Convert.ToString(kerdivGPSLongitude) + ";" + Convert.ToString(kerdivGPSLatitude),
                        feltoltve = false
                    }).Result;
                    Debug.WriteLine("idd2=" + idd2);
                    Debug.WriteLine("idd2=" + idd2);
                    /*var idd2 = adatBazis.SaveCogData(new Cogdata
                    {
                        kerdes = darabol2[0],
                        valasz = darabol2[1],
                        kerdivdate = TimeS(DateTime.Now),
                        egyedi2 = kerdivPlatformGep,
                        egyedi3 = kerdivPlatformSoftver,
                        kerdivver = kerdivVer,
                        kerdivtip = Convert.ToInt16(kerdivTip),
                        projid = Convert.ToInt16(kerdivId),
                        alid = kerdivAlid,
                        egyedi1 = Convert.ToString(kerdivGPSLongitude) + ";" + Convert.ToString(kerdivGPSLatitude),
                        feltoltve = false
                    });*/
                }
                
            }
            
            if (milyenANet() == paramNetkapcsolat)
            {
                feltoltAdat();
                feltoltFile();
            }

        }
        public static long TimeS(DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }
        private async static void feltoltAdat()
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            var rs = new Data.RestService();
            var allData = adatBazis.GetCogDataFeltoltveE(false);
            var A = 1;
            //if (allData.Count() > 0)
            {
                foreach (var item in allData)
                {
                    if (milyenANet() == paramNetkapcsolat)
                    {
                        Cogdata cogdata = (Cogdata)item;
                        RestApiModell visszaUpload = await rs.kerdesUpload(cogdata);
                        if (visszaUpload.message == "OK")
                        {
                            cogdata.feltoltve = true;
                            adatBazis.UpdateCogData(cogdata);
                        }
                    }

                }
            }



        }
        private async static void feltoltFile()
        {
            CrossFileUploader.Current.FileUploadCompleted += Current_FileUploadCompleted;
            
            CrossFileUploader.Current.FileUploadError += Current_FileUploadError;
            string pt1 = Path.Combine(Constans.myZipPath, "photo");
            if (Directory.Exists(pt1))
            {
                foreach(var item in Directory.GetFiles(pt1))
                {
                    string fileneve = Path.GetFileName(item);
                    string dirneve = Path.GetDirectoryName(item);
                    Debug.WriteLine(fileneve);
                    CrossFileUploader.Current.UploadFileAsync(
                        Constans.uploadUrl, 
                        new FilePathItem("uploadedfile", item), new Dictionary<string, string>()
                    {
                        /*<HEADERS HERE>*/
                    });

                    var a = 2;
                }
                
            }
            



        }

        

        private static void Current_FileUploadError(object sender, FileUploadResponse e)
        {
            //isBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("File Upload", "Upload Failed", "Ok");
                Debug.WriteLine("File Upload", "Upload Failed", "Ok");
                //progress.IsVisible = false;
                //progress.Progress = 0.0f;
            });
        }

        private static void Current_FileUploadCompleted(object sender, FileUploadResponse e)
        {
            //isBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("File Upload", "Upload Completed", "Ok");
                Debug.WriteLine("File Upload", "Upload Completed", "Ok");
                //progress.IsVisible = false;
                //progress.Progress = 0.0f;
            });
        }
        public static string kipofoz(string duma)
        {
            string vissza = "";
            if (duma != null)
            {
                char sepa = Convert.ToChar(";");
                var ujduma = duma.Split(sepa);
                if (ujduma.Length == 1)
                {
                    vissza = duma;
                }
                else
                {
                    vissza = "";
                    foreach (var item in ujduma)
                    {
                        vissza = vissza + item + "\n";

                    }
                }
            }
            

            return vissza;
        }
        public static int milyenANet()
        {
            int vissza = 0;
            if (CrossConnectivity.Current.IsConnected)
            {
                var wifi = Plugin.Connectivity.Abstractions.ConnectionType.WiFi;
                var mobilnet = Plugin.Connectivity.Abstractions.ConnectionType.Cellular;
                var connectionTypes = CrossConnectivity.Current.ConnectionTypes;
                if (connectionTypes.Contains(wifi) && connectionTypes.Contains(mobilnet))
                {
                    vissza = 1;
                }
                else if (connectionTypes.Contains(wifi))
                {
                    vissza = 2;
                }
                else if (connectionTypes.Contains(mobilnet))
                {
                    vissza = 3;
                }
                else
                {
                    vissza = 4;
                }
            }
            if (vissza != 0)
            {
                paramNetkapcsolat = vissza;
            }
            return vissza;

        }
        public static bool isValidFloat(string duma)
        {
            bool visszaBool = true;
            double doubleResult;
            var vissza = double.TryParse(duma, out doubleResult);
            if (doubleResult == 0) { visszaBool = false; }
            return visszaBool;
            //return double.IsNaN(Convert.ToDouble(duma));
        }
        public static bool isValidInt(string duma)
        {
            bool visszaBool = true;
            int intResult;
            var vissza = int.TryParse(duma, out intResult);
            if (intResult == 0) { visszaBool = false; }
            return visszaBool;
            //return double.IsNaN(Convert.ToDouble(duma));
        }

        public static int Length(string v)
        {
            int vissza = 0;
            if (v != null)
            {
                vissza = v.Length;
            }

            return vissza;
        }
        public static string BTrim(string text)
        {
            String vissza = text;
            if (text != null)
            {
                if (text.Length > 0)
                {
                    var text2 = text.TrimEnd(' ');
                    vissza = text2.TrimStart(' ');
                }
            }


            return vissza;
        }
        public static string ValaszParameterNelkul(string text)
        {
            String vissza = text;
            if (text != null)
            {
                if (text.Length > 0)
                {
                    var kezd = text.LastIndexOf(";");
                    if (kezd > 0)
                    {
                        var vege = text.Substring(kezd + 1);
                        if (isValidInt(vege))
                        {
                            vissza = text.Substring(0, kezd);
                        }
                    }
                    
                }
            }
            return vissza;
        }

        public static string ValaszParameter(string text)
        {
            String vissza = "";
            if (text != null)
            {
                if (text.Length > 0)
                {
                    var kezd = text.LastIndexOf(";");
                    if (kezd > 0)
                    {
                        var vege = text.Substring(kezd + 1);
                        if (isValidInt(vege))
                        {
                            vissza = vege;
                        }
                    }
                }
            }
            return vissza;
        }
        public static bool KellERotalni(string text)
        {
            bool vissza = true;

            if (text=="2" || text =="3" || text == "6" || text == "7"){
                vissza = false;
            }
            return vissza;
        }
        public static bool VanEOpen(string text)
        {
            bool vissza = false;

            if (text == "1" || text == "3" || text == "5" || text == "7")
            {
                vissza = true;
            }
            return vissza;
        }
    }

}
