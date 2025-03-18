
using BulkMailAPI.Extensions;
using Serilog;

namespace BulkMailAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();
            
            // Add services to the container.
            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.ConfigureMiddlewares();

            app.MapControllers();

            app.Run();
        }
    }
}
