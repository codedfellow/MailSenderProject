import { EmailStatusEnum } from "../enums/email-status.enum";
import { MailTypeEnum } from "../enums/mail-type.enum";

export interface MailLogModel{
    mailId: string,
    subject?: string,
    sender?: string,
    recipients: string,
    recipientsArray: string,
    body: string,
    mailStatus: EmailStatusEnum,
    mailStatusString: string,
    mailType: MailTypeEnum,
    mailTypeString?: string,
    sheduledMailId?: string
}