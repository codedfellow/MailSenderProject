using BulkMailBackgroundService.Extensions;
using Serilog;

namespace BulkMailBackgroundService
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                using var host = Host.CreateDefaultBuilder(args)
                    .ConfigureLogging((hostContext, loggingBuilder) =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddSerilog(dispose: true);
                        //loggingBuilder.ClearProviders();
                        //loggingBuilder.AddConsole();
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
            catch (Exception e)
            {
                Log.Error(e.GetBaseException().Message);
                throw;
            }
        }
    }
}