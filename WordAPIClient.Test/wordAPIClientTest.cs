using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LanguageBot.Test
{
    [TestFixture]    
    public class wordClientTest
    {
        [Test]
        public void getExisitingWordEntry([Values("unhinged","power", "zephyr", "uncanny")] string word)
        {
                WordAPIClient myManager = new WordAPIClient();

                WordDefinition myWord = myManager.getWordEntry(word);

                Assert.NotNull(myWord);

            //format the result
                string  formatedResult = buildEntry(myWord);
                Assert.Pass(formatedResult);            

        }

        [Test]
        public void getNonExisitingWordEntry([Values( "sdfsd")] string word)
        {
            WordAPIClient myManager = new WordAPIClient();

            WordDefinition myWord = myManager.getWordEntry(word);

            Assert.Null(myWord);
        }

        private string buildEntry(WordDefinition wordDef)
        {
            StringBuilder entry = new StringBuilder();

            if(wordDef.word != null)
                entry.AppendFormat($"{wordDef.word}: ");
            if(wordDef.pronunciation != null)
                if(wordDef.pronunciation.all != null)
                    entry.AppendFormat($"/{wordDef.pronunciation.all}/ ");

            entry.AppendFormat($" Frequency: {wordDef.frequency}/7\n\n");

            for (int i = 0; i < wordDef.results.Count && i < 3; i++)
            {
                if(wordDef.results[i].partOfSpeech != null)
                    entry.AppendFormat($"{wordDef.results[i].partOfSpeech}: ");
                if(wordDef.results[i].definition != null)
                    entry.AppendFormat($"{wordDef.results[i].definition}");

                entry.Append("\n\n");

                if (wordDef.results[i].examples != null)
                    if (wordDef.results[i].examples.Count > 0)
                        entry.AppendFormat($"\"{wordDef.results[i].examples[0]}\"\n\n");

                if (wordDef.results[i].synonyms != null)
                {
                    if (wordDef.results[i].synonyms.Count > 0)
                        entry.Append("Synonyms:");

                    for (int j = 0; j < wordDef.results[i].synonyms.Count; j++)
                    {
                        //will get max 8
                        if ((j + 1) == wordDef.results[i].synonyms.Count || (j + 1) == 8)
                        {
                            entry.AppendFormat($"{wordDef.results[i].synonyms[j]}\n\n");
                            break;
                        }
                        entry.AppendFormat($"{wordDef.results[i].synonyms[j]}, ");
                    }
                    entry.Append("\n\n\n\n");
                }
            }

            return entry.ToString();
        }
    }
}
