using MimeKit;

namespace DTOs.MailDtos;

public class SendMailDto
{
    public string mailBody { get; set; }
    public string mailSubject { get; set; }
    public List<CustomMailAddress> receiversList { get; set; }
    public string? copiedList { get; set; }
}

public class CustomMailAddress{
    public string emailAddress { get; set; }
    public string? emailName { get; set; }
}