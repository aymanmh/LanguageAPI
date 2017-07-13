using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;


namespace LanguageBot
{
    [Serializable]
    public class PressmonClient
    {
        private string endpointURL;
        private string apiKey;

        //read from app.config
        public PressmonClient()
        {
            try
            {
                endpointURL = ConfigurationManager.AppSettings["endpoint"];
                apiKey = ConfigurationManager.AppSettings["APIKey"];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //pass as parameters
        public PressmonClient(string endpointURL, string apiKey)
        {
            this.endpointURL = endpointURL;
            this.apiKey = apiKey;
        }

        public WordUsageExample getWordUsage(string word)
        {
            try
            {

                StringBuilder fullQuery = new StringBuilder(endpointURL);
                //q: word to search, l=languge english, size:max results
                fullQuery.AppendFormat("q={0}&key={1}&l=en&size=4&format=json", word, apiKey);
                HttpResponse<string> response = Unirest.get(fullQuery.ToString())
               .header("Accept", "application/json")
               .asJson<string>();

                if (response.Code != 200)
                    throw new Exception($"WordAPI return error code{response.Code} - message:{response.Body}");

                return JsonConvert.DeserializeObject<WordUsageExample>(response.Body);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
