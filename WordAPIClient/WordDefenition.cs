using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot
{
    [Serializable]
    public class WordDefinition
    {
        public string word { get; set; }

        public List<Result> results { get; set; }

        public Pronunciation pronunciation { get; set; }

        public double frequency { get; set; }    
    }

    [Serializable]
    public class Pronunciation
    {
        public string all { get; set; }
        public string noun { get; set; }
        public string verb { get; set; }
    }

    [Serializable]
    public class Result
    {
        public string definition { get; set; }
        public string partOfSpeech { get; set; }

        public List<string> synonyms { get; set; }

        public List<string> typeOf { get; set; }

        public Syllable syllables { get; set; }
        public List<string> hasTypes { get; set; }

        public List<string> examples { get; set; }
    }

    [Serializable]
    public class Syllable
    {
        public int count { get; set; }
        List<string> list { get; set; }
    }
}
