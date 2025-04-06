using DataAccessAndEntities.Entities;
using DataAccessAndEntities.Enums;
using DTOs.Configurations;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Exception = System.Exception;

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
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation("Scoped Processing Service is runnning...", executionCount);

                var pastScheduledMails = await context.ScheduledMail.Where(x =>
                    x.ScheduleStatus == ScheduledMailStatus.Active && x.NextMailDateTime < DateTime.Now).ToListAsync();
                
                var todayScheduledMails = await context.ScheduledMail.Where(x =>
                    x.ScheduleStatus == ScheduledMailStatus.Active && x.NextMailDateTime.Date == DateTime.Now.Date).ToListAsync();
                
                var allMails = pastScheduledMails.Union(todayScheduledMails);
                int numberOfMails = allMails.Count();
        
                //_logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
                List<Task> tasks = new List<Task>();
                
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false,token);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password,token);
                    
                    //allMails.Select(async (mail) => SendScheduledMail(mail, client));

                    // await Parallel.ForEachAsync(allMails, async (mailToSend, token) =>
                    // {
                    //     await SendScheduledMail(mailToSend, client);
                    // });

                    foreach(var mailToSend in allMails)
                    {
                        await SendScheduledMail(mailToSend, client);
                    }
                }
        
                _logger.LogInformation("\n");
                _logger.LogInformation($"mails sent.");

                await Task.Delay(10000, stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("\n");
            _logger.LogError(e,$"{e.GetBaseException().Message}");
            throw;
        }
    }

    private async Task SendScheduledMail(ScheduledMail scheduledMail, SmtpClient client)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("noreply@elvisaghaulor.com", "noreply@elvisaghaulor.com"));

            string[] recipients = scheduledMail.Recipients.Split(";");
            
            List<MailboxAddress> receivers = new List<MailboxAddress>();
            foreach (var receiver in recipients)
            {
                MailboxAddress receiverMail = new MailboxAddress(receiver, receiver);
                receivers.Add(receiverMail);
            }
            
            emailMessage.To.AddRange(receivers);
            emailMessage.Subject = scheduledMail.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = scheduledMail.Body };
            // return emailMessage;
            await Send(emailMessage, client);   

            DateTime nextMailDateTime = scheduledMail.Frequency switch
            {
                MailFrequencyEnum.Daily => scheduledMail.NextMailDateTime.AddDays(1),
                MailFrequencyEnum.Weekly => scheduledMail.NextMailDateTime.AddDays(7),
                MailFrequencyEnum.Monthly => scheduledMail.NextMailDateTime.AddMonths(1),
                _ => throw new Exception("Next mail date could'nt be calculated")
            };

            scheduledMail.NextMailDateTime = nextMailDateTime;
            
            if (scheduledMail.EndDate.HasValue && scheduledMail.EndDate.Value.Date < nextMailDateTime.Date)
            {
                scheduledMail.ScheduleStatus = ScheduledMailStatus.Ended;
            }
            
            await context.SaveChangesAsync(token);

        }
        catch (Exception e)
        {
            _logger.LogError("\n");
            _logger.LogError(e,$"Send Scheduled Mail {scheduledMail.ScheduledMailId} failure... {e.GetBaseException().Message}");
            //throw;
        }
    }
    
    private async Task Send(MimeMessage mailMessage, SmtpClient client)
    {
            try
            {
                
                await client.SendAsync(mailMessage, token);
            }
            catch(Exception ex)
            {
                _logger.LogError("\n");
                _logger.LogError(ex,$"{ex.GetBaseException().Message}");
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
            }
    }
}
#endregion