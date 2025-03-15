using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BulkMailBackgroundService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            
            var serviceProvider = services.BuildServiceProvider();
            
            var mongoSettingsOption = (IOptions<MongoDBSettings>)serviceProvider.GetService(typeof(IOptions<MongoDBSettings>));
            var mongoDbSettings = mongoSettingsOption.Value;
            
            services.AddTransient<CancellationTokenSource>();
            
            /*services.AddDbContext<BulkMailDbContext>(options =>
            {
                options.UseMongoDB(mongoDbSettings.ConnectionURI, mongoDbSettings.DatabaseName);
            }, ServiceLifetime.Singleton, ServiceLifetime.Singleton);*/
            
            services.AddDbContext<BulkMailDbContext>(options =>
            {
                options.UseMongoDB(mongoDbSettings.ConnectionURI, mongoDbSettings.DatabaseName);
            });

            #region snippet1
            services.AddHostedService<ScheduledMailsBackgroundService>();
            #endregion
            
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            
            return services;
        }
}