using System;
using System.Collections.Generic;
using System.Text;


using Newtonsoft.Json;
using System.Threading.Tasks;
using CognativeSurveyX.myDataBase;
using CognativeSurveyX.Modell;
using System.Diagnostics;
using System.Net.Http;

namespace CognativeSurveyX.Data
{
    public class RestService
    {
        HttpClient client2;
        //RestApiModell posts;
        TokenDatabaseController tokenDatabase = new TokenDatabaseController();

        public RestService()
        {

            client2 = new HttpClient();
            client2.MaxResponseContentBufferSize = 256000;
            client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            
            //var client2 = new RestSharp.Portable.HttpClient.RestClient("http://qnr.cognative.hu/cogsurv/");
            //var request2 = new RestSharp.Portable.RestRequest("regist_ios.php");


            //var response = client.Execute<RestApiModell>(request);
            //var result = response.Result;

            
        }
        public async Task<RestApiModell> Reggi(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("user_name", user.user_name));
            postData.Add(new KeyValuePair<string, string>("user_surname", user.user_surnamed));
            postData.Add(new KeyValuePair<string, string>("user_kod", user.user_kod));
            postData.Add(new KeyValuePair<string, string>("user_password", user.user_password));
            postData.Add(new KeyValuePair<string, string>("user_email", user.user_emil));
            var content = new FormUrlEncodedContent(postData);
            var uri = Constans.webUrl;
            //Debug.WriteLine("egynek jo");
            var response = await PostResponseReggi<Token>(uri,content);
            return response;

        }
        public  Task<RestApiModell> Reggi2(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("user_name", user.user_name));
            postData.Add(new KeyValuePair<string, string>("user_surname", user.user_surnamed));
            postData.Add(new KeyValuePair<string, string>("user_kod", user.user_kod));
            postData.Add(new KeyValuePair<string, string>("user_password", user.user_password));
            postData.Add(new KeyValuePair<string, string>("user_email", user.user_emil));
            var content = new FormUrlEncodedContent(postData);
            var uri = Constans.webUrl;
            //Debug.WriteLine("egynek jo");
            var response =  PostResponseReggi<Token>(uri, content);
            return response;

        }
        public async Task<RestApiModell> PostResponseReggi<T>(string uri,FormUrlEncodedContent content) where T: class
        {
            //Debug.WriteLine("kodeaaaaaa " );
            try
            {
                //Debug.WriteLine("kodeaaaaaa2 ");
                //Debug.WriteLine(client2);
                //Debug.WriteLine(uri);
                //Debug.WriteLine(content);
                //var response = await client2.GetStringAsync(uri);
                var response = await client2.PostAsync(uri, content);
                //Debug.WriteLine("kode " + Convert.ToString(response));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(jsonResult);
                    try
                    {
                        var responseObject = JsonConvert.DeserializeObject<RestApiModell>(jsonResult);
                        Debug.WriteLine(responseObject);
                        return responseObject;
                    }
                    catch
                    {
                        return null; 
                    }
                    
                }
            } catch {
                return null; 
            }
            return null;
            
        }
        
        public async Task<T> PostResponse<T>(string uri, string jsonstring) where T : class
        {
            var token =  tokenDatabase.GetToken();
            
            string ContentType = "application/json";
            client2.DefaultRequestHeaders.Authorization= new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",Convert.ToString(token.access_token));
            try
            {
                var result = await client2.PostAsync(uri, new StringContent(jsonstring, Encoding.UTF8, ContentType));
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var contentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return contentResp;
                    }
                    catch { return null; }
                }



            }
            catch { return null; }
            return null;
            

        }

        public async Task<T> GetResponse<T>(string uri) where T : class
        {
            var token = tokenDatabase.GetToken();
            client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Convert.ToString(token.access_token));
            try
            {
                var response = await client2.GetAsync(uri);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("json "+JsonResult);
                    try
                    {
                        var contentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return contentResp;
                    }
                    catch { return null; }
                    
                }
                
            }
            catch { return null; }
            return null;
            

        }
        /*public async Task<RestApiModell> RefreshDataAsync()
        {
            
            String vissza = "";
            var content = new StringContent(
            JsonConvert.SerializeObject(new {
                user_name = name,
                user_surname = name2,
                user_kod = code,
                user_password = pass,
                user_email = emil

            }));
            
            //var uri = new Uri(string.Format("http://qnr.cognative.hu/cogsurv/regist_ios2.php", content));
            var uri = new Uri("http://qnr.cognative.hu/cogsurv/regist_ios2.php");
            //var response = await client2.GetAsync(uri);
            string result;
            try {
                result = await client2.GetStringAsync("http://qnr.cognative.hu/cogsurv/regist_ios2.php");
                var posts = JsonConvert.DeserializeObject<RestApiModell>(result);
            }
            catch {
                result = "Connection error?";
            }
            //var result = await client2.GetStringAsync(uri);
            
            //var posts = JsonConvert.DeserializeObject<RestApiModell>(result);
        //var a = "aaa";
            //var vissza = response.Content.ReadAsStringAsync;
            return posts;
        }*/

        //public string name { get; internal set; }
        //public string name2 { get; internal set; }
        //public string code { get; internal set; }
        //public string pass { get; internal set; }
        //public string emil { get; internal set; }
    
        /*public async Task<RestApiModell> GetJSON()
        {
            //RestApiModell vissza ;
            //Check network status   
            if (NetworkCheck.IsInternet())
            {

                var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    user_name = name,
                    user_surname = name2,
                    user_kod = code,
                    user_password = pass,
                    user_email = emil

                }));
                var uri = new Uri(string.Format("http://qnr.cognative.hu/cogsurv/regist_ios2.php", content));

                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync(uri);
                String contactsJson = await response.Content.ReadAsStringAsync();
                if (contactsJson != "")
                {
                    //Converting JSON Array Objects into generic list  
                    RestApiModell vissza = JsonConvert.DeserializeObject<RestApiModell>(contactsJson);
                }
                //vissza = contactsJson;

                //Binding listview with server response    
                //listviewConacts.ItemsSource = ObjContactList.contacts;
            }
            else
            {
                await DisplayAlert("JSONParsing", "No network is available.", "Ok");
            }
            //Hide loader after server response    
            //ProgressLoader.IsVisible = false;
            return vissza;
        }*/
        /*public RestApiModell ReggiFutAsync()
        {
            using (var client = new RestClient(new Uri("http://qnr.cognative.hu/cogsurv/regist_ios2.php")))
            {
                var request = new RestRequest("name /{ name }", RestSharp.Portable.Method.POST);
                //var request = new RestSharp.Portable.RestRequest();
                request.AddParameter("user_name", name);
                request.AddParameter("user_surname", name2);
                request.AddParameter("user_kod", code);
                request.AddParameter("user_password", pass);
                request.AddParameter("user_email", emil);
                var result =  client.Execute<RestApiModell>(request);
                RestApiModell vissza = result.Result.Data;
                return vissza;
            }
        }*/
    }
}
