using DataAccessAndEntities.Enums;
using DTOs.MailDtos;

namespace DTOs.ScheduledMailsDto;

public class ScheduleMailDto
{
    public string Subject { get; set; }
    public List<CustomMailAddress> ReceiversList { get; set; }
    public string Body { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime StartDateTime { get; set; }
    public bool IsContinuous { get; set; }
    public MailFrequencyEnum Frequency { get; set; }
}