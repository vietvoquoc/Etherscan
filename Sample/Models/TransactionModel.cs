using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class TransactionModel
    {
        public string blockHash { get; set; }
        public string blockNumber { get; set; }
        public string hash { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string value { get; set; }
        public string gas { get; set; }
        public string gasPrice { get; set; }        
        public string transactionIndex { get; set; }
        
    }
}
