using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class BlockModel
    {
        public string gasLimit { get; set; }
        public string gasUsed { get; set; }
        public string hash { get; set; }
        public string miner { get; set; }
        public string number { get; set; }
        public string parentHash { get; set; }
        public decimal blockReward { get; set; }
    }
}
