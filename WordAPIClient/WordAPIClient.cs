using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
using Newtonsoft.Json.Serialization;

namespace LanguageBot
{
    [Serializable]
    public class WordAPIClient
    {
        private string endpointURL;
        private string apiKey;
        private static int dailyRequestCounter = 0;
        private static DateTime CurrentDate = DateTime.Now;
        //sometimes Pronunciation has differnt type and deserialization fails, will handle this later
        private string missingPronunciation; 
        public WordAPIClient()
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

        public WordAPIClient(string endpointURL, string apiKey)
        {
            this.endpointURL = endpointURL;
            this.apiKey = apiKey;
        }

        public WordDefinition getWordEntry(string word)
        {
            try
            {
                if (checkDailyLimit() == false)
                    throw new Exception("Daily API calls limit reached.");

                HttpResponse<string> response = Unirest.get(endpointURL + word)
                .header("X-Mashape-Key", apiKey)
                .header("Accept", "application/json")
                .asJson<string>();

                if (response.Code == 404)
                    return null;
                else if (response.Code != 200)
                    throw new Exception($"WordAPI returned error {response.Code} - message:{response.Body}");

                WordDefinition wdEntry =  JsonConvert.DeserializeObject<WordDefinition>(response.Body, new JsonSerializerSettings
                {
                    Error = HandleDeserializationError
                });

                if(!String.IsNullOrEmpty(missingPronunciation) && wdEntry.pronunciation == null)//populate manually
                {
                    wdEntry.pronunciation = new Pronunciation();
                    wdEntry.pronunciation.all = missingPronunciation;
                }

                return wdEntry;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //an ugly way of handling the error...
        private void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            if(currentError.Contains("pronunciation"))
            {
                string beforeString = "value \"";
                int beforeStringIndex = currentError.IndexOf(beforeString);
                string afterString = "\" to";
                int afterStringIndex = currentError.IndexOf(afterString);


                missingPronunciation = currentError.Substring(
                    beforeStringIndex + beforeString.Length,
                    afterStringIndex - (beforeStringIndex + beforeString.Length) 
                    );

            }
            errorArgs.ErrorContext.Handled = true;
        }

        private bool checkDailyLimit()
        {
            if(CurrentDate.Date.Equals(DateTime.Now) == false)
            {
                CurrentDate = DateTime.Now;
                dailyRequestCounter = 0;
            }

            if (dailyRequestCounter++ > 80)
                return false;

            return true;
        }
    }
}
