using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using Microsoft.Extensions.Options;

namespace BulkMailBackgroundService;

public class ScheduledMailsBackgroundService : BackgroundService
{
    private int executionCount = 0;
    private readonly ILogger<ScheduledMailsBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;  

    public ScheduledMailsBackgroundService(ILogger<ScheduledMailsBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /*public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(SendMails, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }*/
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service running.");

        await SendMails(stoppingToken);
    }

    private async Task SendMails(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is working.");

        using (var scope = _serviceProvider.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IScopedProcessingService>();

            await scopedProcessingService.SendMails(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}