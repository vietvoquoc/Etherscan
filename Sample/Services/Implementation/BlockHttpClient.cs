using Microsoft.Net.Http.Headers;
using Polly;
using Sample.Data;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class BlockHttpClient : IBlockHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientSettings _settings;
        public BlockHttpClient(IHttpClientFactory httpClientFactory, HttpClientSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            _httpClient = httpClientFactory.CreateClient("BlockChainAPI");
            _settings = settings;
            _httpClient.BaseAddress = new Uri(settings.BaseAddress);

            _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
        }

        public async Task<BlockModel> GetBlockAsync(string blockNumber)
        {
            var req = new HttpRequestMessage();
            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "module", "proxy" },
                { "action", "eth_getBlockByNumber" },
                { "boolean", "false" }, // can set to true to get whole of transaction in one request
                { "tag", blockNumber },
                { "apikey", _settings.ApiKey }
            });
            var response = await _httpClient.SendAsync(req);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<BlockModel>>();
            return result.result;
        }

        public async Task<decimal> GetTransactionCountAsync(string blockNumber)
        {
            var req = new HttpRequestMessage();
            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "module", "proxy" },
                { "action", "eth_getBlockTransactionCountByNumber" },
                { "tag", blockNumber },
                { "apikey", _settings.ApiKey }
            });
            var response = await _httpClient.SendAsync(req);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
            return result.result.ToDecimal();
        }

        public async Task<TransactionModel> GetTransactionModelAsync(string blockNumber, string tranIndex)
        {
            var req = new HttpRequestMessage();
            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "module", "proxy" },
                { "action", "eth_getTransactionByBlockNumberAndIndex" },
                { "index", tranIndex },
                { "tag", blockNumber },
                { "apikey", _settings.ApiKey }
            });
            var response = await _httpClient.SendAsync(req);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<TransactionModel>>();
            return result.result;
        }
    }
}
