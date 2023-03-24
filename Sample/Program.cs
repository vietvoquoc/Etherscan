
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample;
using Sample.Services.Interfaces;
using Serilog;
using Serilog.Events;

var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(Environment.CurrentDirectory, "logs","myapp.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSampleDBContext(hostContext.Configuration);
        services.AddSampleServices(hostContext.Configuration);
    })
    .UseSerilog(logger)
    .Build();

await ProcessBlockRange(host.Services);

await host.RunAsync();

static async Task ProcessBlockRange(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var blockProvider = provider.GetService<IBlockProvider>();
    var configuration = hostProvider.GetService<IConfiguration>();
    var startBlock = configuration.GetValue<string>("StartBlock");
    var endBlock = configuration.GetValue<string>("EndBlock");
    await blockProvider.GetBlockByRange(int.Parse(startBlock), int.Parse(endBlock));
    Console.WriteLine();
}
