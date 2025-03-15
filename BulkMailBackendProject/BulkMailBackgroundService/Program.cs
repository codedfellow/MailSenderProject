using BulkMailBackgroundService.Extensions;

namespace BulkMailBackgroundService
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                }).ConfigureServices((hostContext, services) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var config = serviceProvider.GetRequiredService<IConfiguration>();
                    services.AddServices(config);
                })
                .Build();

            await host.StartAsync();

            await host.WaitForShutdownAsync();
        }
    }
}