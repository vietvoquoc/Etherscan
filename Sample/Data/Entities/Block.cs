using System.ComponentModel.DataAnnotations;

namespace Sample.Data
{
    public class Block
    {
        public Block()
        {
            Transactions = new List<Transaction>();
        }
        public int blockID { get; set; }
        public int blockNumber { get; set; }
        public string hash { get; set; }
        public string parentHash { get; set; }
        public string miner { get; set; }
        public decimal blockReward { get; set; }
        public decimal gasLimit { get; set; }
        public decimal gasUsed { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
