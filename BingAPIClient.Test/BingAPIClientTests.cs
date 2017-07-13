using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot.Test
{
    [TestFixture]
    public class BingAPIClientTests
    {
        [Test]
        public void getTranslation([Values("trifle")] string text)
        {
            BingAPIClient myClient = new BingAPIClient();

            string translatedText = myClient.translate(text, BingAPIClient.ARABIC);

            Assert.NotNull(translatedText);

            Assert.Pass(translatedText);
        }
    }
}
