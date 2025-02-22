using DataAccessAndEntities.Entities;
using DTOs.EmailLogDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ScheduledMailsDto
{
    public static class ScheduledMailDtosExtensionMethods
    {
        public static ScheduledMailDto ConvertToUserDto(this ScheduledMail mail)
        {
            return new ScheduledMailDto
            {
                ScheduledMailId = mail.ScheduledMailId.ToString(),
                Subject = mail.Subject,
                Sender = mail.Sender,
                Recipients = mail.Recipients,
                Body = mail.Body,
                NextMailDateTime = mail.NextMailDateTime,
                EndDate = mail.EndDate,
                StartDateTime = mail.StartDateTime,
                IsContinuous = mail.IsContinuous,
                ScheduleStatus = mail.ScheduleStatus,
                Frequency = mail.Frequency,
            };
        }
    }
}
