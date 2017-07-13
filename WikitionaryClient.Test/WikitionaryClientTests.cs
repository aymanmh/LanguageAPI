using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot.Test
{
    [TestFixture]
    public class WikitionaryClientTests
    {
        [Test]
        public void getWikitionaryEntryUrlTest([Values("tryst","jdhskdjf")] string word)
        {
            WikitionaryClient myWikiClient = new WikitionaryClient();

            string url = myWikiClient.getWikitionaryEntryUrl(word);

            Assert.NotNull(url);
            if(url == "")
                Assert.Pass("No Wiki entry was found.");
            else
                Assert.Pass(url);

        }
    }
}
