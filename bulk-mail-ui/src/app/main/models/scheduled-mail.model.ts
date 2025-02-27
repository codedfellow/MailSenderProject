import { MailFrequencyEnum } from "../enums/mail-frequency.enum";
import { ScheduledMailStatus } from "../enums/scheduled-mail-status.enum"

export interface ScheduledMailModel {
    scheduledMailId: string,
    subject?: string,
    sender?: string,
    recipients?: string,
    body: string,
    nextMailDateTime: Date,
    endDate?: Date,
    startDateTime: Date,
    isContinuous: boolean,
    scheduleStatus: ScheduledMailStatus,
    schduleStatusString?: string,
    frequency: MailFrequencyEnum,
    frequencyString?: string,
}