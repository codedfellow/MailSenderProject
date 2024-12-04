using DataAccessAndEntities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace DataAccessAndEntities.Entities
{
    [Collection("EmailLog")]
    public class EmailLog : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MailId { get; set; }
        [BsonElement("subject")]
        public string Subject { get; set; }
        [BsonElement("sender")]
        public string Sender { get; set; }
        [BsonElement("recipients")]
        public string Recipients { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("mail_status")]
        public EmailStatusEnum MailStatus { get; set; }
        [BsonElement("mail_type")]
        public MailTypeEnum MailType { get; set; }
        [BsonElement("scheduled_mail_id")]
        public ObjectId? ScheduledMailId { get; set; }
    }
}
