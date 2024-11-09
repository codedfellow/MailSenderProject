using DTOs.MailDtos;
using MongoDB.Bson;

namespace ServicesInterfaces;

public interface IMailService
{
    Task<bool> SendMail(SendMailDto model, ObjectId userId);
}