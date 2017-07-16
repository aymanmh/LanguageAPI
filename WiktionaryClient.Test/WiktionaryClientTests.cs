using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot.Test
{
    [TestFixture]
    public class WiktionaryClientTests
    {
        [Test]
        public void getWikitionaryEntryUrlTest([Values("tryst","jdhskdjf")] string word)
        {
            WiktionaryClient myWikiClient = new WiktionaryClient();

            string url = myWikiClient.getWikitionaryEntryUrl(word);

            Assert.NotNull(url);
            if(url == "")
                Assert.Pass("No Wiki entry was found.");
            else
                Assert.Pass(url);

        }
    }
}
