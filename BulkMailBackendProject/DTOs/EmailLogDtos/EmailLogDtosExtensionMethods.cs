using DataAccessAndEntities.Entities;
using DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.EmailLogDtos
{
    public static class EmailLogDtosExtensionMethods
    {
        public static EmailLogDto ConvertToUserDto(this EmailLog mail)
        {
            return new EmailLogDto
            {
                MailId = mail.MailId.ToString(),
                Subject = mail.Subject,
                Sender = mail.Sender,
                Recipients = mail.Recipients,
                Body = mail.Body,
                MailStatus = mail.MailStatus,
                MailType = mail.MailType,
                ScheduledMailId = mail.ScheduledMailId.ToString(),
            };
        }
    }
}
