using DataAccessAndEntities.Entities;
using DataAccessAndEntities.Enums;
using DTOs.Configurations;
using DTOs.EmailLogDtos;
using DTOs.MailDtos;
using Helpers;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
//using MailKit.Net.Smtp;
using MimeKit;
using IMailService = ServicesInterfaces.IMailService;

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
                CreatedAt = DateTime.UtcNow,
                MailType = MailTypeEnum.Immediate
            };

            await context.AddAsync(newMail, token);

            emailMessage.To.AddRange(receivers);
            emailMessage.Subject = model.mailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = model.mailBody };
            // return emailMessage;
            await Send(emailMessage);
            
            await context.SaveChangesAsync(token);

            return true;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<List<EmailLogDto>> GetEmailLogs(ObjectId userId)
    {
        var mailStatusEnums = Enum.GetValues(typeof(EmailStatusEnum)).Cast<EmailStatusEnum>();
        var mailTypesEnum = Enum.GetValues(typeof(MailTypeEnum)).Cast<MailTypeEnum>();
        var sentMails = (from log in context.EmailLog?.AsEnumerable()
            where log.CreatedBy == userId
            select new EmailLogDto()
            {
                MailId = log.MailId.ToString(),
                Subject = log.Subject,
                Sender = log.Sender,
                Recipients = log.Recipients,
                Body = log.Body,
                MailStatus = log.MailStatus,
                MailType = log.MailType,
                ScheduledMailId = log.ScheduledMailId.ToString(),
                MailStatusString = mailStatusEnums.Where(x => x == log.MailStatus).Select(x => EnumsHelper.GetMailStatusString(x)).FirstOrDefault(),
                MailTypeString = mailTypesEnum.Where(x => x == log.MailType).Select(x => EnumsHelper.GetMailTypeString(x)).FirstOrDefault(),
                CreatedAt = log.CreatedAt
            }).OrderByDescending(x => x.CreatedAt).ToList();
        
        return sentMails;
    }

    public async Task<EmailLogDto> GetSingleMaiLog(ObjectId mailId)
    {
        var mail = await context.EmailLog.FindAsync(mailId,token);

        if (mail is null)
        {
            throw new ArgumentException("No mail found");
        }

        var mailResponse = mail.ConvertToUserDto();
        mailResponse.MailStatusString = EnumsHelper.GetMailStatusString(mail.MailStatus);
        mailResponse.MailTypeString = EnumsHelper.GetMailTypeString(mail.MailType);
        
        return mailResponse;
    }

    private async Task Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient(new ProtocolLogger(Console.OpenStandardOutput())))
        {
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false,token);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password,token);
                await client.SendAsync(mailMessage,token);
            }
            catch(Exception ex)
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true,token);
            }
        }
    }
}