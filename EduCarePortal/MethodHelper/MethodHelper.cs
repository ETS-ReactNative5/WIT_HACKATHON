using EduCarePortal.Models;
using IBM.Cloud.SDK.Core.Authentication.BasicAuth;
using IBM.Watson.LanguageTranslator.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;

namespace EduCarePortal.MethodHelper
{
    public class MethodHelpers
    {
        public Dictionary<string,string> GetSupportedLangs()
        {
            Dictionary<string, string> languageList = new Dictionary<string, string>();
            try
            {
                IamAuthenticator authenticator = new IamAuthenticator(
                    apikey: "o1IZscyN4KLM5hTvs_TVbhdf6_BylgDT1clM35Xlr2oK"
                );

                LanguageTranslatorService languageTranslator = new LanguageTranslatorService("2018-05-01", authenticator);
                languageTranslator.SetServiceUrl("https://api.eu-gb.language-translator.watson.cloud.ibm.com/instances/fe321fb1-62b9-4c3d-8752-37081a369884");

                var result = languageTranslator.ListIdentifiableLanguages();
                if (result.Result.Languages.Count() > 0)
                {
                    foreach (var item in result.Result.Languages)
                    {
                        languageList.Add(item.Language, item.Name);
                    }
                }
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return languageList;
        }
        public async Task<string> TranslateContent(string content, string toLang)
        {
            try
            {
                TranslateBody translateBody = new TranslateBody();
                translateBody.source = "en";
                translateBody.target = toLang;
                translateBody.text = new List<string>() { content }.ToArray();

                HttpClient httpClient = new HttpClient();
                string dataUrl = "https://api.eu-gb.language-translator.watson.cloud.ibm.com/instances/fe321fb1-62b9-4c3d-8752-37081a369884/v3/translate?version=2018-05-01";
                var authHeaders = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("apikey" + ":" + "o1IZscyN4KLM5hTvs_TVbhdf6_BylgDT1clM35Xlr2oK")));
                httpClient.DefaultRequestHeaders.Authorization = authHeaders;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, dataUrl) { Content = new StringContent(JsonConvert.SerializeObject(translateBody, Formatting.None), Encoding.UTF8, "application/json") };
                var response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var requestResult = response.Content.ReadAsStringAsync();
                    TranslationModel translatedmodel = JsonConvert.DeserializeObject<TranslationModel>(requestResult.Result);
                    return translatedmodel.translations[0].translation;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public void TriggerTranscribe(string fileID)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string dataUrl = "https://webjobsapp20200605031608.scm.azurewebsites.net/api/triggeredwebjobs/VideoTranscribeJob/run?arguments="+ fileID;
                var authHeaders = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("$WebJobsApp20200605031608" + ":" + "nmaArd0fXT7pqjfZimpf3BlllrbyXDhQygt3egFpTb7HgHZToTFdfRwMNXwQ")));
                httpClient.DefaultRequestHeaders.Authorization = authHeaders;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, dataUrl);
                var response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    
                }
            }
            catch (Exception e)
            {
                
            }
        }
    }
}