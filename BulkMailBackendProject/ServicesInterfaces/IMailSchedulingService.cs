using DTOs.ScheduledMailsDto;
using MongoDB.Bson;

namespace ServicesInterfaces;

public interface IMailSchedulingService
{
    Task<bool> ScheduleMail(ScheduleMailDto model, ObjectId userId);
}