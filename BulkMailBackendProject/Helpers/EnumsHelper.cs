using DataAccessAndEntities.Enums;

namespace Helpers;

public static class EnumsHelper
{
    public static String GetMailStatusString(EmailStatusEnum emailStatus)
    {
        string returnVal = emailStatus switch
        {
            EmailStatusEnum.Sent => nameof(emailStatus.Sent),
            EmailStatusEnum.Pending => nameof(emailStatus.Pending),
            EmailStatusEnum.Failed => nameof(emailStatus.Failed),
            _ => ""
        };

        return returnVal;
    }
    
    public static String GetMailTypeString(MailTypeEnum mailType)
    {
        string returnVal = mailType switch
        {
            MailTypeEnum.Immediate => nameof(MailTypeEnum.Immediate),
            MailTypeEnum.Scheduled => nameof(MailTypeEnum.Scheduled),
            _ => ""
        };

        return returnVal;
    }
    
    public static String GetScheduleStatusString(ScheduledMailStatus status)
    {
        string returnVal = status switch
        {
            ScheduledMailStatus.Active => nameof(ScheduledMailStatus.Active),
            ScheduledMailStatus.Cancelled => nameof(ScheduledMailStatus.Cancelled),
            ScheduledMailStatus.Ended => nameof(ScheduledMailStatus.Ended),
            _ => ""
        };

        return returnVal;
    }
}