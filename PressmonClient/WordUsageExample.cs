using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LanguageBot
{
    [Serializable]
    public class WordUsageExample
    {
        public string res { get; set; }
        public int from { get; set; }
        public int size { get; set; }
        public int quota_daily_used { get; set; }
        public int quota_daily { get; set; }
        public string lang { get; set; }
        public int time { get; set; }
        public string query { get; set; }
        public string collection { get; set; }
        public string max_score { get; set; }
        public int hits_total { get; set; }
        public string scope { get; set; }

        public List<Hit> hits { get; set; }

    }

    [Serializable]
    public class Hit
    {
        public string country { get; set; }
        public string source { get; set; }
        public int pub_day { get; set; }
        public double score { get; set; }
        public string body { get; set; }
        public int pub_year { get; set; }
        public string id { get; set; }
        public int article_id { get; set; }
        public int len { get; set; }
        public string url { get; set; }
        public int pub_month { get; set; }
        public string title { get; set; }

        
    }
}
