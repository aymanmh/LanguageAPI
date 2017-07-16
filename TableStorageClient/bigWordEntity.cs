using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageBot;

namespace LanguageBot
{
    public class BigWordEntity : TableEntity
    {
        public BigWordEntity(string word, string id)
        {
            this.PartitionKey = id;
            this.RowKey = word.ToLower();
            this.isUsed = false;
        }

        public BigWordEntity() { }

        public bool isUsed { get; set; }
    }
}
