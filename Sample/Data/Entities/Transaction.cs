using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data
{
    public class Transaction
    {
        public int transactionID { get; set; }
        public string hash { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public decimal gas { get; set; }
        public decimal gasPrice { get; set; }
        public int transactionIndex { get; set; }

        public int blockID { get; set; }
        public virtual Block block { get; set; }
    }
}
