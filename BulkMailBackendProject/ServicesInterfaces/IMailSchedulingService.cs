using DTOs.ScheduledMailsDto;
using MongoDB.Bson;

namespace ServicesInterfaces;

public interface IMailSchedulingService
{
    Task<bool> ScheduleMail(ScheduleMailDto model, ObjectId userId);
    Task<List<ScheduledMailDto>> GetScheduledMails(ObjectId userId);
}