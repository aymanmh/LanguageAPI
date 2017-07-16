using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot.Test
{
    [TestFixture]
    public class TableManagerTests
    {
        [Test]
        public void getNewRandomWordTest()
        {
            TableStorageClient testTableManager = new TableStorageClient();

            var returnedValue = testTableManager.getRandomWord();

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }

        [Test]
        public void getAnyRandomWordTest()
        {
            //TableStorageClient testTableManager = new TableStorageClient();

            TableStorageClient testTableManager = new TableStorageClient("UseDevelopmentStorage=true;", "BigWords", "1218");

            var returnedValue = testTableManager.getRandomWord(false);

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }
        [Test]
        public void getWord([Values(217)]int id)
        {
            TableStorageClient testTableManager = new TableStorageClient();

            var returnedValue = testTableManager.getWord(id.ToString());

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }

        [Test]
        public void setWordUsedStatus([Values(217)]int id)
        {
            TableStorageClient testTableManager = new TableStorageClient();

            BigWordEntity myWord = testTableManager.getWord(id.ToString());


            testTableManager.setWordUsedStatus(myWord);


            var returnedValue = testTableManager.getWord(myWord.PartitionKey);

            Assert.NotNull(returnedValue);

            Assert.AreEqual(returnedValue.isUsed, myWord.isUsed);
            
        }

        [Test]
        public void resetWordUsedStatus([Values(217)]int id)
        {
            TableStorageClient testTableManager = new TableStorageClient();

            BigWordEntity myWord = testTableManager.getWord(id.ToString());


            testTableManager.resetWordUsedStatus(myWord);


            var returnedValue = testTableManager.getWord(myWord.PartitionKey);

            Assert.NotNull(returnedValue);

            Assert.AreEqual(returnedValue.isUsed, myWord.isUsed);

        }
        
        
        [Test]
        public void populateTable(
            [Values(@"bigwords.txt")]string filePath)
        {
            TableStorageClient testTableManager = new TableStorageClient();

            testTableManager.populateTable(filePath);
            Assert.Pass();
        }
       
    }
}
