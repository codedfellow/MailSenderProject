using DataAccessAndEntities.Entities;
using DataAccessAndEntities.Enums;
using DTOs.ScheduledMailsDto;
using Helpers;
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

        string receipients = string.Join(";",model.ReceiversList.Select(x => x.emailAddress));
        
        var newScheduledMail = new ScheduledMail()
        {
            Subject = model.Subject,
            Body = model.Body,
            Sender = user.Email,
            Recipients = receipients,
            StartDateTime = model.StartDateTime,
            NextMailDateTime = model.StartDateTime,
            IsContinuous = model.IsContinuous,
            CreatedAt = DateTime.UtcNow,
            ScheduleStatus = ScheduledMailStatus.Active,
            Frequency = model.Frequency,
            EndDate = model.EndDate,
            CreatedBy = userId
        };
        
        await context.ScheduledMail.AddAsync(newScheduledMail, token);
        await context.SaveChangesAsync(token);
        
        return true;
    }

    public async Task<List<ScheduledMailDto>> GetScheduledMails(ObjectId userId)
    {
        var allScheduledMails = context.ScheduledMail.Where(x => x.CreatedBy == userId).ToList();
        
        List<ScheduledMailDto> scheduledMailsResponse = new List<ScheduledMailDto>();

        for (int i = 0; i < allScheduledMails.Count; i++)
        {
            var currentMail = allScheduledMails[i];
            var nextToAdd = currentMail.ConvertToUserDto();
            nextToAdd.SchduleStatusString = EnumsHelper.GetScheduleStatusString(nextToAdd.ScheduleStatus);
            nextToAdd.FrequencyString = EnumsHelper.GetScheduleMailFrequencyString(nextToAdd.Frequency);
            scheduledMailsResponse.Add(nextToAdd);
        }
        
        return scheduledMailsResponse;
    }
}