using DataAccessAndEntities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ScheduledMailsDto
{
    public record ScheduledMailDto
    {
        public string ScheduledMailId { get; set; }
        public string? Subject { get; set; }
        public string? Sender { get; set; }
        public string? Recipients { get; set; }
        public string? Body { get; set; }
        public DateTime NextMailDateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsContinuous { get; set; }
        public ScheduledMailStatus ScheduleStatus { get; set; }
        public string? SchduleStatusString {
            get;
            set;
        }
    }
}
