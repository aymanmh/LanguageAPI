using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using unirest_net.http;

namespace LanguageBot
{
    [Serializable]
    public class BingAPIClient
    {
        private string endpointURL;
        private string subscribtionKey;
        private AzureAuthToken authTokenManager;
        public const string ARABIC = "ar";
        public const string MALAY = "ms";
        public const string JAPANESE = "ja";

        public BingAPIClient()
        {
            try
            {
                endpointURL = ConfigurationManager.AppSettings["endpoint"];
                subscribtionKey = ConfigurationManager.AppSettings["APIKey"];
                authTokenManager = new AzureAuthToken(subscribtionKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //pass as parameters
        public BingAPIClient(string endpointURL, string subscribtionKey)
        {           
            try
            {
                this.endpointURL = endpointURL;
                this.subscribtionKey = subscribtionKey;
                authTokenManager = new AzureAuthToken(subscribtionKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string translate(string word, string toLangauge)
        {
            try
            {
                string translation = string.Empty;
                string authToken = authTokenManager.GetAccessToken();
                StringBuilder fullQuery = new StringBuilder(endpointURL);
                fullQuery.AppendFormat("text={0}&to={1}", word, toLangauge);

                HttpResponse<string> response = Unirest.get(fullQuery.ToString())
               .header("Authorization", authToken)
               .asString();

                if (response.Code != 200)
                    throw new Exception($"BingTranslate returned code{response.Code} - message:{response.Body}");

                using (MemoryStream mStrm = new MemoryStream(Encoding.UTF8.GetBytes(response.Body)))
                {

                    DataContractSerializer dcs = new DataContractSerializer(Type.GetType("System.String"));
                    translation = (string)dcs.ReadObject(mStrm);
                }

                return translation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
