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
            TableManager testTableManager = new TableManager();

            var returnedValue = testTableManager.getRandomWord();

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }

        [Test]
        public void getAnyRandomWordTest()
        {
            TableManager testTableManager = new TableManager();

            var returnedValue = testTableManager.getRandomWord(false);

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }
        [Test]
        public void getWord([Values(217)]int id)
        {
            TableManager testTableManager = new TableManager();

            var returnedValue = testTableManager.getWord(id.ToString());

            Assert.NotNull(returnedValue);

            Assert.IsNotEmpty(returnedValue.RowKey);

            Assert.Pass(returnedValue.RowKey);
        }

        [Test]
        public void setWordUsedStatus([Values(217)]int id)
        {
            TableManager testTableManager = new TableManager();

            BigWordEntity myWord = testTableManager.getWord(id.ToString());


            testTableManager.setWordUsedStatus(myWord);


            var returnedValue = testTableManager.getWord(myWord.PartitionKey);

            Assert.NotNull(returnedValue);

            Assert.AreEqual(returnedValue.isUsed, myWord.isUsed);
            
        }

        [Test]
        public void resetWordUsedStatus([Values(217)]int id)
        {
            TableManager testTableManager = new TableManager();

            BigWordEntity myWord = testTableManager.getWord(id.ToString());


            testTableManager.resetWordUsedStatus(myWord);


            var returnedValue = testTableManager.getWord(myWord.PartitionKey);

            Assert.NotNull(returnedValue);

            Assert.AreEqual(returnedValue.isUsed, myWord.isUsed);

        }
        
        /*
        [Test]
        public void populateTable(
            [Values(@"C: \Users\Ayman\Documents\visual studio 2017\Projects\AzureProjects\TableStorageTest\bigwords.txt")]string filePath)
        {
            TableManager testTableManager = new TableManager();
            testTableManager.populateTable(filePath);
            Assert.Pass();
        }
        */
    }
}
