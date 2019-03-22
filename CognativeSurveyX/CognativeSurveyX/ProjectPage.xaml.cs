using CognativeSurveyX.Modell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CognativeSurveyX.myDataBase;
using CognativeSurveyX.Data;
using CognativeSurveyX.Fregments;
using Newtonsoft.Json;
using CognativeSurveyX.Controls;

namespace CognativeSurveyX
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
        IDownloader downloader = DependencyService.Get<IDownloader>();
        IDisplay display = DependencyService.Get<IDisplay>();
        IPath mypt = DependencyService.Get<IPath>();
        IPlatformInfo myPlatform = DependencyService.Get<IPlatformInfo>();

        List<Gomb> listOfButtons = new List<Gomb>();
        ScrollView myScroll = new ScrollView();

        UsersDataAccess adatBazis = new UsersDataAccess();

        public ProjectPage ()
		{
			InitializeComponent ();

            gpsBeallit();
            Constans.webUrl = "http://qnr.cognative.hu/cogsurv/fresh_xam.php";
            Constans.myZipPath = mypt.MyPath;
            Constans.ScreenHeight = display.Height;
            Constans.ScreenWidth = display.Width;
            downloader.OnFileDownloaded += OnFileDownloaded;
            Debug.WriteLine("iiiiiiiiiitttttttttteeeeeeeeeennnnnn");
            Debug.WriteLine(Constans.myZipPath);
            
            if (myPlatform != null)
            {
                Constans.kerdivPlatformGep = myPlatform.GetModell();
                Constans.kerdivPlatformSoftver = myPlatform.GetVersion();
            }
            int netTipus = Constans.milyenANet();
            if (netTipus != 0)
            {
                //UsersDataAccess adatBazis = new UsersDataAccess();
                User user2 = new User();
                var reggiAdatok = adatBazis.GetCogAzon();
                foreach (Cogazon item in reggiAdatok)
                {
                    user2.user_name = item.uname;
                    user2.user_surnamed = item.usname;
                    user2.user_kod = Convert.ToString(item.userid);
                    user2.user_password = item.upass;
                    user2.user_emil = item.uemail;
                }

                useradat(user2);
            }
            else
            {
                gombokKipakol();
            }
                
            

        }


        private async Task<RestApiModell> useradat(User user2)
        {
            bool kellgomb = false;
            var ReferenceDate = new DateTime(1970, 1, 1);
            var ma = DateTime.Now;
            var rs = new RestService();
            RestApiModell visszaMost = await rs.Reggi(user2);
            Debug.WriteLine("vissza -" + visszaMost.error);
            //UsersDataAccess adatBazis = new UsersDataAccess();

            Debug.WriteLine(visszaMost.darab);
            if (visszaMost.darab > 0)
            {
                foreach (var item in adatBazis.GetCogDataKerdiv())
                {
                    item.volte = false;
                    adatBazis.SaveCogDataKerdiv(item);
                }
            }
            for (int i = 0; i < visszaMost.darab; i++)
            {
                bool megLett = false;
                DateTime CacheUtcTime = ReferenceDate.AddSeconds(Convert.ToInt64(visszaMost.kerdivadat[i].kerdiv2_le));
                if (CacheUtcTime < ma)
                {
                    //adatokat felpakolni
                    //adatokat törölni
                    //törölni a recordot ha van
                    foreach (var item in adatBazis.CogDataKerdiv)
                    {
                        if (item.projid == Convert.ToInt16(visszaMost.kerdivadat[i].proj_id)
                             && item.kerdiv1ver == visszaMost.kerdivadat[i].kerdiv1_ver)
                        {
                            adatBazis.DeleteCogDataKerdiv(item);
                            var zipFileName = Path.Combine(Constans.myZipPath,"cognative", "kerdiv_" + visszaMost.kerdivadat[i].proj_id + "_" + visszaMost.kerdivadat[i].kerdiv1_ver+".zip");
                            if (File.Exists(zipFileName))
                            {
                                File.Delete(zipFileName);
                            }
                            
                            var konyvtarName = Path.Combine(Constans.myZipPath, "cognative", "kerdiv_" + visszaMost.kerdivadat[i].proj_id + "_" + visszaMost.kerdivadat[i].kerdiv1_ver );
                            if (Directory.Exists(konyvtarName))
                            {
                                Directory.Delete(konyvtarName,true);

                            }
                            
                        }
                    }
                }
                else
                {
                    var kerdivAdatok = adatBazis.GetCogDataKerdivAsProjid(Convert.ToInt16(visszaMost.kerdivadat[i].proj_id));
                    
                    foreach (var item in kerdivAdatok)
                    {
                        if (item.kerdiv1nev == visszaMost.kerdivadat[i].kerdiv1_nev)
                        {
                            if (item.kerdiv1ver == visszaMost.kerdivadat[i].kerdiv1_ver)
                            {
                                item.volte = true;
                                adatBazis.SaveCogDataKerdiv(item);
                                megLett = true;
                                var zipFileName = "kerdiv_" + visszaMost.kerdivadat[i].proj_id + "_" + visszaMost.kerdivadat[i].kerdiv1_ver;
                                if (!File.Exists(Constans.myZipPath + "/cognative/" + zipFileName + "/" + visszaMost.kerdivadat[i].kerdiv1_nev + ".json"))
                                {
                                    Debug.WriteLine("zipes:" + zipFileName);
                                    var Url = Constans.downUrl + zipFileName + ".zip";
                                    Constans.kellZip.Add(Url);
                                }
                            }
                            else
                            {
                                megLett = true;
                                //ha nem jó a verzió akkor udpdateljük az adatokat
                                var idd = adatBazis.SaveCogDataKerdiv(new Cogkerdiv
                                {
                                    volte=true,
                                    id = item.id,
                                    kerdiv1nev = visszaMost.kerdivadat[i].kerdiv1_nev,
                                    kerdiv1ver = visszaMost.kerdivadat[i].kerdiv1_ver,
                                    kerdivtitle = visszaMost.kerdivadat[i].kerdiv1_title,
                                    kerdivtip = Convert.ToInt16(visszaMost.kerdivadat[i].kerdivtip),
                                    projid = Convert.ToInt16(visszaMost.kerdivadat[i].proj_id),
                                    fuggv_par = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_par),
                                    fuggv_par_ertek = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_par_ertek),
                                    fuggv_poj = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_proj),
                                    kerdivdate = CacheUtcTime


                                });
                                var zipFileName = "kerdiv_" + visszaMost.kerdivadat[i].proj_id + "_" + visszaMost.kerdivadat[i].kerdiv1_ver;
                                if (!File.Exists(Constans.myZipPath + "/cognative/" + zipFileName + "/" + visszaMost.kerdivadat[i].kerdiv1_nev + ".json"))
                                {
                                    Debug.WriteLine("zipes:" + zipFileName);
                                    var Url = Constans.downUrl + zipFileName + ".zip";
                                    Constans.kellZip.Add(Url);
                                }
                                //var kerdivAdatokFeltoltveE = adatBazis.GetCogDataAsProjidVer(Convert.ToInt16(visszaMost.kerdivadat[i].proj_id), item.kerdiv1ver);
                                //var idd = kerdivAdatokAdatbazisba(visszaMost);
                            }
                            break;
                        }
                        
                    }
                    
                    
                    if (!megLett)
                    {
                        var idd = adatBazis.SaveCogDataKerdiv(new Cogkerdiv
                        {
                            volte = true,
                            kerdiv1nev = visszaMost.kerdivadat[i].kerdiv1_nev,
                            kerdiv1ver = visszaMost.kerdivadat[i].kerdiv1_ver,
                            kerdivtitle = visszaMost.kerdivadat[i].kerdiv1_title,
                            kerdivtip = Convert.ToInt16(visszaMost.kerdivadat[i].kerdivtip),
                            projid = Convert.ToInt16(visszaMost.kerdivadat[i].proj_id),
                            fuggv_par = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_par),
                            fuggv_par_ertek = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_par_ertek),
                            fuggv_poj = Convert.ToInt16(visszaMost.kerdivadat[i].fugg_proj),
                            kerdivdate = CacheUtcTime


                        });
                        var zipFileName = "kerdiv_" + visszaMost.kerdivadat[i].proj_id + "_" + visszaMost.kerdivadat[i].kerdiv1_ver;
                        if (!File.Exists(Constans.myZipPath + "/cognative/" + zipFileName + "/" + visszaMost.kerdivadat[i].kerdiv1_nev + ".json"))
                        {
                            Debug.WriteLine("zipes:" + zipFileName);
                            var Url = Constans.downUrl + zipFileName + ".zip";
                            Constans.kellZip.Add(Url);
                        }
                    }
                }
            }
            Debug.WriteLine("lassukl");
            foreach (var item in adatBazis.GetCogDataKerdiv())
            {
                Debug.WriteLine(item.volte);
                if (!item.volte)
                {
                    //adatBazis.DeleteCogDataKerdiv(item);
                    adatBazis.DeleteCogDataKerdiv(item);
                    var zipFileName = Path.Combine(Constans.myZipPath, "cognative", "kerdiv_" + item.projid + "_" + item.kerdiv1ver + ".zip");
                    if (File.Exists(zipFileName))
                    {
                        File.Delete(zipFileName);
                    }

                    var konyvtarName = Path.Combine(Constans.myZipPath, "cognative", "kerdiv_" + item.projid + "_" + item.kerdiv1ver);
                    if (Directory.Exists(konyvtarName))
                    {
                        Directory.Delete(konyvtarName, true);

                    }
                }
            }
            Debug.WriteLine("lassuk2");
            foreach (var item in adatBazis.GetCogDataKerdiv())
            {
                Debug.WriteLine(item.volte);
                
            }
            Debug.WriteLine("lassuk3");
            foreach (var item in adatBazis.CogDataKerdiv)
            {
                Debug.WriteLine(item.volte);

            }
            gombokKipakol();
            if (Constans.kellZip.Count > 0)
            {
                //var aktUrl = Constans.kellZip.ElementAt(Constans.kellZipIndex);
                var aktUrl = Constans.kellZip.ElementAt(0);
                downloader.DownloadFile(aktUrl, "cognative");
                Constans.kellZipIndex++;
            }
            



            return visszaMost;
        }
        private void gombokKipakol()
        {
            

            myLayout.Margin = new Thickness(10, 0, 10, 0);
            var myStack = new StackLayout();
            myStack.VerticalOptions = LayoutOptions.FillAndExpand;
            myStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            myScroll.Content = myStack;
            /*Label kerdes = new Label();
            kerdes.Text = Constans.aktQuestion.question_title;
            kerdes.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myStack.Children.Add(kerdes);*/
            //myLayout.Children.Add(kerdes);

            var indexMost = -1;
            Debug.WriteLine( adatBazis.CogDataKerdiv.Count());
            foreach (var item in adatBazis.GetCogDataKerdiv())
            {
                indexMost++;
                Gomb button = new Gomb
                {
                    Text = item.kerdivtitle,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    BackgroundColor = Color.Transparent
                };
                int padding = Convert.ToInt16(Constans.ScreenWidth / 7);
                button.Padding = new Thickness(padding, 0, padding, 0);
                listOfButtons.Add(button);
                button.CheckedChange += button_CheckedChange;
                var zipFileName = "kerdiv_" + item.projid + "_" + item.kerdiv1ver;
                button.IsVisible = false;
                if (File.Exists(Constans.myZipPath + "/cognative/" + zipFileName + "/" + item.kerdiv1nev + ".json") && item.volte)
                {
                    button.IsVisible = true;
                }
                else
                {
                    var a = 2;
                }
                Constans.myParam2.Add(Tuple.Create(Convert.ToString(button.Id), zipFileName, item.kerdiv1nev, item.id));
                myStack.Children.Add(button);
            }
            


            myLayout.Children.Add(myScroll);

        }

        private void button_CheckedChange(object sender, bool e)
        {
            Gomb button = (Gomb)sender;
            foreach (var itemT in Constans.myParam2)
            {
                if (Convert.ToString(button.Id) == itemT.Item1)
                {
                    string ffilenev = itemT.Item3 + ".json";
                    Constans.myFilePath = Path.Combine(Constans.myZipPath, "cognative", itemT.Item2);
                    String ffile = Path.Combine(Constans.myZipPath, "cognative", itemT.Item2, ffilenev);
                    Debug.WriteLine("ffileneve: " + ffile);
                    string jsonString = File.ReadAllText(ffile);
                    jsonString = Constans.RemoveNewLines(jsonString);

                    
                    Questions responseObject = JsonConvert.DeserializeObject<Questions>(jsonString);

                    foreach (Questions.Question itemQ in responseObject.questions)
                    {
                        List<string> listValasz = new List<string>();
                        if (itemQ.question_type != "SzurRadio2")
                        {
                            bool vanvalasz = true;
                            if (itemQ.choices != null)
                            {
                                foreach (var itemQV in itemQ.choices)
                                {
                                    int inte = 0;
                                    int kezd = itemQV.IndexOf(Convert.ToChar("-"));
                                    if (kezd != null)
                                    {
                                        if (kezd > 0)
                                        {
                                            string str = itemQV.Substring(0, kezd);
                                            if (Constans.isValidFloat(str))
                                            {
                                                inte = Convert.ToInt32(str);
                                            }
                                            
                                        }
                                    }
                                    if (inte == 0)
                                    {
                                        vanvalasz = false;
                                        break;
                                    }
                                }
                                if (!vanvalasz)
                                {
                                    int idx = 0;
                                    foreach (var itemQV in itemQ.choices)
                                    {
                                        idx++;
                                        //itemQ.choicesKod[idx]=(Convert.ToString(idx));
                                        //itemQ.choicesKod.Add(Convert.ToString(idx));
                                        listValasz.Add(Convert.ToString(idx));

                                    }

                                }
                                itemQ.choicesKod = (listValasz);
                            }


                        }
                        else
                        {
                            int idx = 0;
                            foreach (var itemQV in itemQ.choices)
                            {
                                idx++;
                                int inte = 0;
                                int kezd = itemQV.IndexOf(Convert.ToChar("-"));
                                if (kezd != null)
                                {
                                    if (kezd > 0)
                                    {
                                        string str = itemQV.Substring(0, kezd);
                                        inte = Convert.ToInt32(str);


                                    }
                                }
                                if (inte > 0)
                                {
                                    //itemQ.choicesKod.Add(Convert.ToString(inte));
                                    //itemQ.choicesKod[idx] = (Convert.ToString(inte));
                                    listValasz.Add(Convert.ToString(inte));

                                }
                                else
                                {
                                    //itemQ.choicesKod[idx]="0";
                                    listValasz.Add("0");
                                }
                            }
                            itemQ.choicesKod = (listValasz);
                        }
                    }
                    foreach (Questions.Question itemQ in responseObject.questions)
                    {
                        List<bool> listLatszik = new List<bool>();
                        if (itemQ.choices != null)
                        {
                            foreach (var itemQV in itemQ.choices)
                            {
                                listLatszik.Add(true);
                            }
                            itemQ.choicesVisible = (listLatszik);
                        }

                    }
                    foreach (Questions.Question itemQ in responseObject.questions)
                    {
                        List<bool> listLatszik = new List<bool>();
                        if (itemQ.items != null)
                        {
                            foreach (var itemQV in itemQ.items)
                            {
                                listLatszik.Add(true);
                            }
                            itemQ.itemVisible = (listLatszik);
                        }


                    }


                    Constans.aktQuestion = responseObject.questions.ElementAt(0);
                    Constans.aktSurvey = responseObject;
                    Constans.pageNumber = 1;
                    var visszaz = adatBazis.GetCogDataKerdivAsSern(itemT.Item4);
                    if (visszaz.Count() == 1)
                    {
                        foreach (Cogkerdiv itemz in visszaz)
                        {
                            Constans.kerdivId = Convert.ToString(itemz.projid);
                            Constans.kerdivVer = itemz.kerdiv1ver;
                            Constans.kerdivAlid = kerdivAlidKeres(itemz.projid);
                            Constans.kerdivTip = Convert.ToString(itemz.kerdivtip);
                        }
                    }


                    Navigation.PushModalAsync(new FPage());
                    break;

                }
            }
        }

        private void gombokKipakol2()
        {

            //UsersDataAccess adatBazis = new UsersDataAccess();

            var stack = new StackLayout();
            myScroll.Content = stack;
            var regForm2 = new Grid();
            regForm2.HorizontalOptions = LayoutOptions.Center;
            //regForm2.BackgroundColor = Color.LightGray;
            regForm2.Padding = 5;
            regForm2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            regForm2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
            regForm2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            regForm2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            var indexMost = -1;
            foreach (var item in adatBazis.CogDataKerdiv)
            {
                indexMost++;
                var zipFileName = "kerdiv_" + item.projid + "_" + item.kerdiv1ver;

                var buttonM = new Button();
                buttonM.Text = item.kerdivtitle;
                regForm2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                regForm2.Children.Add(buttonM, 1, indexMost);
                buttonM.IsVisible = false;
                if (File.Exists(Constans.myZipPath + "/cognative/" + zipFileName + "/" + item.kerdiv1nev + ".json"))
                {
                    buttonM.IsVisible = true;
                }
                buttonM.Clicked += (aktButton, eredmeny) =>
                {
                    Button button = (Button)aktButton;
                    foreach (var itemT in Constans.myParam2)
                    {
                        if (Convert.ToString(button.Id) == itemT.Item1)
                        {
                            string ffilenev = itemT.Item3 + ".json";
                            Constans.myFilePath = Path.Combine(Constans.myZipPath, "cognative", itemT.Item2);
                            String ffile = Path.Combine(Constans.myZipPath, "cognative", itemT.Item2, ffilenev);
                            Debug.WriteLine("ffileneve: " + ffile);
                            string jsonString = File.ReadAllText(ffile);
                            jsonString = Constans.RemoveNewLines(jsonString);

                            Questions responseObject = JsonConvert.DeserializeObject<Questions>(jsonString);

                            foreach (Questions.Question itemQ in responseObject.questions)
                            {
                                List<string> listValasz = new List<string>();
                                if (itemQ.question_type != "SzurRadio2")
                                {
                                    bool vanvalasz = true;
                                    if (itemQ.choices != null)
                                    {
                                        foreach (var itemQV in itemQ.choices)
                                        {
                                            int inte = 0;
                                            int kezd = itemQV.IndexOf(Convert.ToChar("-"));
                                            if (kezd != null)
                                            {
                                                if (kezd > 0)
                                                {
                                                    string str = itemQV.Substring(0, kezd);
                                                    inte = Convert.ToInt32(str);
                                                }
                                            }
                                            if (inte == 0)
                                            {
                                                vanvalasz = false;
                                                break;
                                            }
                                        }
                                        if (!vanvalasz)
                                        {
                                            int idx = 0;
                                            foreach (var itemQV in itemQ.choices)
                                            {
                                                idx++;
                                                //itemQ.choicesKod[idx]=(Convert.ToString(idx));
                                                //itemQ.choicesKod.Add(Convert.ToString(idx));
                                                listValasz.Add(Convert.ToString(idx));

                                            }

                                        }
                                        itemQ.choicesKod = (listValasz);
                                    }


                                }
                                else
                                {
                                    int idx = 0;
                                    foreach (var itemQV in itemQ.choices)
                                    {
                                        idx++;
                                        int inte = 0;
                                        int kezd = itemQV.IndexOf(Convert.ToChar("-"));
                                        if (kezd != null)
                                        {
                                            if (kezd > 0)
                                            {
                                                string str = itemQV.Substring(0, kezd);
                                                inte = Convert.ToInt32(str);


                                            }
                                        }
                                        if (inte > 0)
                                        {
                                            //itemQ.choicesKod.Add(Convert.ToString(inte));
                                            //itemQ.choicesKod[idx] = (Convert.ToString(inte));
                                            listValasz.Add(Convert.ToString(inte));

                                        }
                                        else
                                        {
                                            //itemQ.choicesKod[idx]="0";
                                            listValasz.Add("0");
                                        }
                                    }
                                    itemQ.choicesKod = (listValasz);
                                }
                            }
                            foreach (Questions.Question itemQ in responseObject.questions)
                            {
                                List<bool> listLatszik = new List<bool>();
                                if (itemQ.choices != null)
                                {
                                    foreach (var itemQV in itemQ.choices)
                                    {
                                        listLatszik.Add(true);
                                    }
                                    itemQ.choicesVisible = (listLatszik);
                                }

                            }
                            foreach (Questions.Question itemQ in responseObject.questions)
                            {
                                List<bool> listLatszik = new List<bool>();
                                if (itemQ.items != null)
                                {
                                    foreach (var itemQV in itemQ.items)
                                    {
                                        listLatszik.Add(true);
                                    }
                                    itemQ.itemVisible = (listLatszik);
                                }


                            }
                            Constans.aktQuestion = responseObject.questions.ElementAt(0);
                            Constans.aktSurvey = responseObject;
                            Constans.pageNumber = 1;
                            var visszay = adatBazis.GetCogDataKerdiv();
                            Debug.WriteLine(visszay.Count());
                            foreach (Cogkerdiv itemy in visszay)
                            {
                                Debug.WriteLine(itemy.id);
                            }

                            var visszax = adatBazis.GetCogDataKerdivAsSern(itemT.Item4);
                            Debug.WriteLine(visszax.Count());
                            foreach (Cogkerdiv itemx in visszax)
                            {
                                Constans.kerdivId = Convert.ToString(itemx.projid);
                                Constans.kerdivVer = itemx.kerdiv1ver;
                                Constans.kerdivAlid = kerdivAlidKeres(itemx.projid);
                                Constans.kerdivTip = Convert.ToString(itemx.kerdivtip);
                            }


                            Navigation.PushModalAsync(new FPage());
                            break;

                        }
                    }
                };

                Constans.myParam.Add(Convert.ToString(buttonM.Id), zipFileName);
                Constans.myParam2.Add(Tuple.Create(Convert.ToString(buttonM.Id), zipFileName, item.kerdiv1nev, item.id));

                //listOfButtons.Add(buttonM);

            }
            stack.Children.Add(regForm2);

        }
        private int kerdivAlidKeres(int projid)
        {
            int visszaTero = 1;
            //UsersDataAccess adatBazis = new UsersDataAccess();
            var kerdivek = adatBazis.GetCogDataAsProjid(projid);
            foreach (var item in kerdivek)
            {
                if (item.alid > visszaTero)
                {
                    visszaTero = item.alid;
                }
            }
            return visszaTero + 1;
        }
        private async void gpsBeallit()
        {
            
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Constans.kerdivGPSLongitude = location.Longitude;
                    Constans.kerdivGPSLatitude = location.Latitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            Debug.WriteLine(Constans.errorDuma);
            if (e.FileSaved)
            {
                //DisplayAlert("XF Downloader", "File Saved Successfully", "Close");
                Debug.WriteLine("mentett zip:" + e.ZipFileMentett);
                ExtractZipFile(Constans.myZipPath + "/cognative/" + e.ZipFileMentett, null, Constans.myZipPath + "/cognative/");

                foreach (var itemT in Constans.myParam2)
                {
                    if ((itemT.Item2 + ".zip") == e.ZipFileMentett)
                    {
                        if (File.Exists(Constans.myZipPath + "/cognative/" + itemT.Item2 + "/" + itemT.Item3 + ".json"))
                        {
                            foreach (var button in listOfButtons)
                            {
                                if (Convert.ToString(button.Id) == itemT.Item1)
                                {
                                    button.IsVisible = true;
                                }
                            }
                        }
                    }

                }
                for (var zipIndex = 0; zipIndex < Constans.kellZip.Count; zipIndex++)
                {
                    if (Constans.downUrl + e.ZipFileMentett == Constans.kellZip.ElementAt(zipIndex))
                    {
                        Constans.kellZip.Remove(Constans.downUrl + e.ZipFileMentett);
                        Constans.kellZipIndex--;
                        break;
                    }
                }
                if (Constans.kellZip.Count > 0 && Constans.kellZipIndex <= Constans.kellZip.Count)
                {
                    var aktUrl = Constans.kellZip.ElementAt(Constans.kellZipIndex);
                    downloader.DownloadFile(aktUrl, "cognative");
                    //ExtractZipFile(Constans.myZipPath + "/cognative/" + e.ZipFileMentett, null, Constans.myZipPath + "/cognative/");
                    Constans.kellZipIndex++;

                }

            }
            else
            {
                DisplayAlert("XF Downloader", "Error while saving the file", "Close");
            }

        }

        public void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            int zipDarab = 0;
            ZipFile zf = null;
            Debug.WriteLine("kicsomagolás jon:" + archiveFilenameIn);
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
                /*foreach (var itemT in Constans.myParam2)
                {
                    if ((itemT.Item2 + ".zip") == archiveFilenameIn)
                    {

                        foreach (var button in listOfButtons)
                        {
                            if (Convert.ToString(button.Id) == itemT.Item1)
                            {
                                button.IsVisible = true;
                            }
                        }
                    }

                }*/
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



    }
}