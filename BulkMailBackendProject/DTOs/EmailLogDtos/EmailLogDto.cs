using DataAccessAndEntities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.EmailLogDtos
{
    public record EmailLogDto
    {
        public ObjectId MailId { get; set; }
        public string? Subject { get; set; }
        public string? Sender { get; set; }
        public string? Recipients { get; set; }
        public string? Body { get; set; }
        public EmailStatusEnum MailStatus { get; set; }
        public EmailStatusEnum MailStatusString { get; set; }
        public MailTypeEnum MailType { get; set; }
        public MailTypeEnum MailTypeString { get; set; }
        public ObjectId? ScheduledMailId { get; set; }
    }
}
