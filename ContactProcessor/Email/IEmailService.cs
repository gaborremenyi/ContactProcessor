namespace ContactProcessor.Email
{
    public interface IEmailService
    {
        void Send(string hostName, string emailFrom, string emailTo, string subject, string body);
    }
}