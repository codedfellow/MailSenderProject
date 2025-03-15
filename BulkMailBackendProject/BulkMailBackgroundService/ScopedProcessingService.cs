using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BulkMailBackgroundService;

#region snippet1
internal interface IScopedProcessingService
{
    Task SendMails(CancellationToken stoppingToken);
}

internal class ScopedProcessingService : IScopedProcessingService
{
    private int executionCount = 0;
    private readonly ILogger _logger;

    private readonly BulkMailDbContext context;
    private readonly CancellationTokenSource tokenSource;
    private readonly IOptionsMonitor<EmailConfiguration> _emailConfigMonitor;
    private readonly EmailConfiguration _emailConfig;
    CancellationToken token;

    public ScopedProcessingService(BulkMailDbContext _context, CancellationTokenSource _tokenSource,
        IOptionsMonitor<EmailConfiguration> emailConfigMonitor, ILogger<ScopedProcessingService> logger)
    {
        context = _context;
        tokenSource = _tokenSource;
        _emailConfigMonitor = emailConfigMonitor;
        _emailConfig = _emailConfigMonitor.CurrentValue;
        token = tokenSource.Token;
        _logger = logger;
    }

    public async Task SendMails(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            executionCount++;

            _logger.LogInformation(
                "Scoped Processing Service is working. Count: {Count}", executionCount);
            
            var sentMais = await context.EmailLog.ToListAsync();
            var numberOfSentMails = sentMais.Count;
        
            //_logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
        
            _logger.LogInformation($"{numberOfSentMails} mails sent.");

            await Task.Delay(10000, stoppingToken);
        }
    }
    
    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false,token);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password,token);
                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true,token);
            }
        }
    }
}
#endregion