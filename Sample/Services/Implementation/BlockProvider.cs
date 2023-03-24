using Microsoft.Extensions.Logging;
using Sample.Data;
using Sample.Services.Interfaces;
using System.Reflection.Metadata;

namespace Sample.Services.Implementation
{
    public class BlockProvider : IBlockProvider
    {
        private readonly IBlockHttpClient _blockHttpClient;
        private readonly SampleDbContext _sampleDbContext;
        private readonly ILogger _blockProviderLogger;
        public BlockProvider(IBlockHttpClient blockHttpClient,
            SampleDbContext sampleDbContext,
            ILoggerFactory loggerFactory)
        {
            _blockHttpClient = blockHttpClient;
            _sampleDbContext = sampleDbContext;
            _blockProviderLogger = loggerFactory.CreateLogger("SampleLogger");
        }
        public async Task GetBlockByRange(int from, int to)
        {
            await _sampleDbContext.Database.EnsureCreatedAsync();
            for (int i = from; i <= to; i++)
            {
                using (_blockProviderLogger.BeginScope("Collect infos for BlockId {0}", i))
                {
                    var blockNumber = i.ToHex();
                    _blockProviderLogger.LogInformation($"Get Block number {blockNumber}");

                    try
                    {
                        // get block
                        var block = await _blockHttpClient.GetBlockAsync(blockNumber);

                        // if exist
                        if (block != null)
                        {
                            // get transaction count
                            var transactionCount = await _blockHttpClient.GetTransactionCountAsync(blockNumber);
                            _blockProviderLogger.LogInformation($"Get Block transaction count is {transactionCount}");

                            // get transaction
                            if (transactionCount > 0)
                            {
                                var Block = new Block()
                                {
                                    blockNumber = (int)block.number.ToDecimal(),
                                    blockReward = block.blockReward,
                                    gasLimit = block.gasLimit.ToDecimal(),
                                    gasUsed = block.gasUsed.ToDecimal(),
                                    hash = block.hash,
                                    miner = block.miner,
                                    parentHash = block.parentHash
                                };

                                for (int t = 0; t < transactionCount; t++)
                                {
                                    var transactionIndex = t.ToHex();
                                    _blockProviderLogger.LogInformation($"Get transaction index {transactionIndex}");
                                    var transaction = await _blockHttpClient.GetTransactionModelAsync(blockNumber, transactionIndex);
                                    Block.Transactions.Add(new Transaction()
                                    {
                                        hash = transaction.hash,
                                        from = transaction.from,
                                        to = transaction.to,
                                        value = transaction.value.ToDecimal(),
                                        gas = transaction.gas.ToDecimal(),
                                        gasPrice = transaction.gasPrice.ToDecimal(),
                                        transactionIndex = t,
                                    });
                                }
                                _sampleDbContext.Blocks.Add(Block);
                                _sampleDbContext.SaveChanges();
                            }
                        }
                        _blockProviderLogger.LogInformation($"Done Block number {blockNumber}");
                        _blockProviderLogger.LogInformation("----------------------------------");
                    }
                    catch (Exception ex)
                    {
                        _blockProviderLogger.LogError(ex, "Get Block number {0} failed", blockNumber);
                        continue;
                    }
                }
            }
        }
    }
}
