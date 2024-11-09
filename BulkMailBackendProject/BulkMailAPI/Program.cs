
using BulkMailAPI.Extensions;

namespace BulkMailAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.ConfigureMiddlewares();

            app.MapControllers();

            app.Run();
        }
    }
}
