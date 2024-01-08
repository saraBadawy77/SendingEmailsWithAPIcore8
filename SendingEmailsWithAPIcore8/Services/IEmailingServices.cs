namespace SendingEmailsWithAPIcore8.Services
{
    public interface IEmailingServices
    {
        Task SendEmailAsync(string mailto,string subject,string body, IList<IFormFile> attachments =null);
    }
}
