using Sample.Models;

namespace Sample.Services
{
    public interface IBlockHttpClient
    {
        Task<BlockModel> GetBlockAsync(string blockNumber);
        Task<decimal> GetTransactionCountAsync(string blockNumber);
        Task<TransactionModel> GetTransactionModelAsync(string blockNumber, string tranIndex);
    }
}
