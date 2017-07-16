using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageBot
{
    public class TableStorageClient
    {
        private CloudStorageAccount storageAccount;
        private CloudTableClient myTableClient;
        private CloudTable myTable;
        private int numberOfWords;
        private string tableName;

        public TableStorageClient()
        {
            try
            {
                this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                this.tableName = CloudConfigurationManager.GetSetting("TableName");
                this.numberOfWords = int.Parse(CloudConfigurationManager.GetSetting("numberOfWords"));

                initTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TableStorageClient(string storageAccount,string tableName,string numberOfWords)
        {
            try
            {
                
                this.storageAccount = CloudStorageAccount.Parse(storageAccount);
                this.tableName = tableName;
                this.numberOfWords = int.Parse(numberOfWords.ToString());

                initTable();
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void initTable()
        {
            myTableClient = this.storageAccount.CreateCloudTableClient();
            myTable = myTableClient.GetTableReference(tableName);
            myTable.CreateIfNotExists();
        }

            public void populateTable(string filePath)
        {
            try
            {
                //myTable.DeleteIfExists(); //this will take long time and the next statment throws an exception, delete manuallt instead.
                myTable.CreateIfNotExists();

                int counter = 0; //will use it as ID
                string line;

                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    myTable.Execute(TableOperation.Insert(new BigWordEntity(line, counter.ToString())));
                    counter++;
                }
                file.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BigWordEntity getRandomWord(bool isNewWord = true)
        {
            int maxTries = numberOfWords; //to avoid a higly unlikely but possible infinite looping
            int tryCount = 0;
            Random rnd = new Random();
            int randomId = 0;
            while (tryCount++ < numberOfWords)
            {
                randomId = rnd.Next(0, numberOfWords);

                try
                {
                    TableQuery<BigWordEntity> query = new TableQuery<BigWordEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, randomId.ToString()));

                    // Execute the retrieve operation.
                    var retrievedResult = myTable.ExecuteQuery(query);

                    if (retrievedResult.Count() == 0)
                        throw new Exception($"word id {randomId} was not found in the table");

                    if (isNewWord == false)
                        return retrievedResult.First();

                    // retrun the result
                    if (retrievedResult.First().isUsed == false)
                    {
                        return retrievedResult.First();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            throw new Exception("Unable to find new words, all words have been used!.");
        }

        public BigWordEntity getWord(string Id)
        {

            try
            {
                TableQuery<BigWordEntity> query = new TableQuery<BigWordEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, Id.ToString()));

                // Execute the retrieve operation.
                var retrievedResult = myTable.ExecuteQuery(query);
                if (retrievedResult.Count() == 0)
                    throw new Exception($"Failed to retrieve word ID {Id} from table {tableName}");

                return retrievedResult.First();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void setWordUsedStatus(BigWordEntity theWord)
        {
            try
            {
                theWord.isUsed = true;
                myTable.Execute(TableOperation.Replace(theWord));

            }
            catch (Exception ex)
            {
                throw ex;            }
        }

        public void resetWordUsedStatus(BigWordEntity theWord)
        {
            try
            {
                theWord.isUsed = false;
                myTable.Execute(TableOperation.Replace(theWord));
            }
            catch(Exception ex)
            {
                throw ex;            }
        }
    }
}
