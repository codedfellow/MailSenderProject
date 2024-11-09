using DataAccessAndEntities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.EntityFrameworkCore;

namespace DataAccessAndEntities.Entities
{
    [Collection("ScheduledMail")]
    public class ScheduledMail : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ScheduledMailId { get; set; }
        [BsonElement("subject")]
        public string Subject { get; set; }
        [BsonElement("sender")]
        public string Sender { get; set; }
        [BsonElement("recipients")]
        public string Recipients { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("next_mail_date_time")]
        public DateTime NextMailDateTime { get; set; }
        [BsonElement("end_date")]
        public DateTime? EndDate { get; set; }
        [BsonElement("start_date_time")]
        public DateTime StartDateTime { get; set; }
        [BsonElement("is_continuous")]
        public bool IsContinuous { get; set; }
        [BsonElement("schedule_status")]
        public ScheduledMailStatus ScheduleStatus { get; set; }
    }
}
