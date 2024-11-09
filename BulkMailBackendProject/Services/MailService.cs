using DataAccessAndEntities.Entities;
using DTOs.Configurations;
using DTOs.MailDtos;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using ServicesInterfaces;
//using MailKit.Net.Smtp;
using MimeKit;

namespace Services;

public class MailService : IMailService
{
    private readonly BulkMailDbContext context;
    private readonly CancellationTokenSource tokenSource;
    private readonly IOptionsMonitor<EmailConfiguration> _emailConfigMonitor;
    private readonly EmailConfiguration _emailConfig;
    
    public MailService(BulkMailDbContext _context, CancellationTokenSource _tokenSource, IOptionsMonitor<EmailConfiguration> emailConfigMonitor)
    {
        context = _context;
        tokenSource = _tokenSource;
        _emailConfigMonitor = emailConfigMonitor;
        _emailConfig = _emailConfigMonitor.CurrentValue;
    }
    public async Task<bool> SendMail(SendMailDto model, ObjectId userId)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("noreply@elvisaghaulor.com","noreply@elvisaghaulor.com"));
        
        List<MailboxAddress> receivers = new List<MailboxAddress>();
        foreach (var receiver in model.receiversList)
        {
            MailboxAddress receiverMail = new MailboxAddress(receiver.emailName, receiver.emailAddress);
            receivers.Add(receiverMail);
        }
        emailMessage.To.AddRange(receivers);
        emailMessage.Subject = model.mailSubject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = model.mailBody };
        // return emailMessage;
        Send(emailMessage);
        return true;
    }
    
    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}