using DataAccessAndEntities.Entities;
using DataAccessAndEntities.Enums;
using DTOs.ScheduledMailsDto;
using MongoDB.Bson;
using ServicesInterfaces;

namespace Services;

public class MailSchedulingService : IMailSchedulingService
{
    private readonly BulkMailDbContext context;
    private readonly CancellationTokenSource tokenSource;
    CancellationToken token;
    
    public MailSchedulingService(BulkMailDbContext _context, CancellationTokenSource _tokenSource)
    {
        context = _context;
        tokenSource = _tokenSource;
        token = tokenSource.Token;
    }
    
    public async Task<bool> ScheduleMail(ScheduleMailDto model, ObjectId userId)
    {
        var user = await context.User.FindAsync(userId,token);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        string receipients = string.Join(";",model.receiversList.Select(x => x.emailAddress));
        
        var newScheduledMail = new ScheduledMail()
        {
            Subject = model.Subject,
            Body = model.Body,
            Sender = user.Email,
            Recipients = receipients,
            StartDateTime = model.StartDateTime,
            NextMailDateTime = model.StartDateTime,
            IsContinuous = model.IsContinuous,
            CreatedAt = DateTime.Now,
            ScheduleStatus = ScheduledMailStatus.Active,
            EndDate = model.EndDate,
            CreatedBy = userId
        };
        
        await context.ScheduledMail.AddAsync(newScheduledMail, token);
        await context.SaveChangesAsync(token);
        
        return true;
    }
}