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
            Constans.pageNumber++;
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

        private static void ValaszokKiiratasa(string valasz)
        {
            UsersDataAccess adatBazis = new UsersDataAccess();
            var darabol = valasz.Split(Convert.ToChar(";"));
            foreach(var item in darabol)
            {
                if (item.Length>0)
                {
                    var darabol2 = item.Split(Convert.ToChar("="));
                    //long mostDate1 = TimeS(DateTime.Now);

                    var idd2 = adatBazis.SaveCogData(new Cogdata
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
                    });
                }
                
            }
            
            if (milyenANet() == paramNetkapcsolat)
            {
                feltoltAdat();
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
        public static string kipofoz(string duma)
        {
            string vissza = "";
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
    }

}
