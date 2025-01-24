using DTOs.EmailLogDtos;
using DTOs.MailDtos;
using MongoDB.Bson;

namespace ServicesInterfaces;

public interface IMailService
{
    Task<bool> SendMail(SendMailDto model, ObjectId userId);
    Task<List<EmailLogDto>> GetEmailLogs(ObjectId userId);
}