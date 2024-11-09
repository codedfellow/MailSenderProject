using MimeKit;

namespace DTOs.MailDtos;

public class SendMailDto
{
    public string mailBody { get; set; }
    public string mailSubject { get; set; }
    public List<CustomeMailAddress> receiversList { get; set; }
    public string copiedList { get; set; }
}

public class CustomeMailAddress{
    public string emailAddress { get; set; }
    public string emailName { get; set; }
}