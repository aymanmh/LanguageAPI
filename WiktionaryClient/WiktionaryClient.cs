using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class WiktionaryClient
    {
        private string endpointURL;

        public WiktionaryClient()
        {
            try
            {
                endpointURL = ConfigurationManager.AppSettings["endpoint"];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public WiktionaryClient(string endpointURL)
        {
            this.endpointURL = endpointURL;
        }

        public string getWiktionaryEntryUrl(string word)
        {
            try
            {
                HttpResponse<string> response = Unirest.get(endpointURL + "action=query&format=json&&titles=" + word)
               .header("Accept", "application/json")
               .asJson<string>();

                if (response.Code != 200)
                    throw new Exception($"Wiktionary API returned error code {response.Code} - message {response.Body}");

                var json = JsonConvert.DeserializeObject<dynamic>(response.Body);

                if (((Newtonsoft.Json.Linq.JProperty)((Newtonsoft.Json.Linq.JContainer)json.query.pages).First).Name.ToString().Equals("-1"))
                    return String.Empty;

                return "https://en.wiktionary.org/wiki/" + word;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
