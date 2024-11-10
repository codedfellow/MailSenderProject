using DataAccessAndEntities.Entities;
using DataAccessAndEntities.Enums;
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
    CancellationToken token;

    public MailService(BulkMailDbContext _context, CancellationTokenSource _tokenSource,
        IOptionsMonitor<EmailConfiguration> emailConfigMonitor)
    {
        context = _context;
        tokenSource = _tokenSource;
        _emailConfigMonitor = emailConfigMonitor;
        _emailConfig = _emailConfigMonitor.CurrentValue;
        token = tokenSource.Token;
    }

    public async Task<bool> SendMail(SendMailDto model, ObjectId userId)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("noreply@elvisaghaulor.com", "noreply@elvisaghaulor.com"));

            List<MailboxAddress> receivers = new List<MailboxAddress>();
            foreach (var receiver in model.receiversList)
            {
                MailboxAddress receiverMail = new MailboxAddress(receiver.emailName, receiver.emailAddress);
                receivers.Add(receiverMail);
            }

            List<string> receiverListMails = model.receiversList.Select(x => x.emailAddress).ToList();

            string receiversString = string.Join(";", receiverListMails);

            EmailLog newMail = new()
            {
                Subject = model.mailSubject,
                Sender = _emailConfig.From,
                Recipients = receiversString,
                Body = model.mailBody,
                MailStatus = EmailStatusEnum.Sent,
                CreatedBy = userId,
                CreatedAt = DateTime.Now,
                MailType = MailTypeEnum.Immediate
            };

            await context.AddAsync(newMail, token);

            emailMessage.To.AddRange(receivers);
            emailMessage.Subject = model.mailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = model.mailBody };
            // return emailMessage;
            Send(emailMessage);
            
            await context.SaveChangesAsync(token);

            return true;
        }
        catch (Exception e)
        {
            throw;
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