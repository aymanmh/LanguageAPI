using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot.Test
{
    [TestFixture]
    public class PressmonClientTester
    {
        [Test]
        public void getWordUsage([Values("venial","abarcy","rehash","sdfsdf$^t2")] string word)
        {
            PressmonClient mypClient = new PressmonClient();

            WordUsageExample myWord = mypClient.getWordUsage(word);
            Assert.NotNull(myWord);
            if (myWord.hits_total > 0)
                Assert.Pass(myWord.hits.First().body);
            else
                Assert.Pass("no examples were found");

        }
    }
}
