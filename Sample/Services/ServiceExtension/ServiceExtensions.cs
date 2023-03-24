using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Extensions.Http;
using Polly;
using Sample.Services;
using Sample.Services.Implementation;
using Sample.Services.Interfaces;

namespace Sample
{
    public static class ServiceExtensions
    {
        public static void AddSampleServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = configuration.GetValue<string>("ApiKey");
            var baseAddress = configuration.GetValue<string>("BaseAddress");
            services.AddHttpClient("BlockChainAPI")
                .AddPolicyHandler(GetRetryPolicy());
            services.AddSingleton(new HttpClientSettings
            {
                ApiKey = apiKey,
                BaseAddress =  baseAddress
            });
            services.AddTransient<IBlockHttpClient, BlockHttpClient>();
            services.AddTransient<IBlockProvider, BlockProvider>();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
