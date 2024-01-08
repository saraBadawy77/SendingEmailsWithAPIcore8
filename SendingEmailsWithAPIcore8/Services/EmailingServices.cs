
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendingEmailsWithAPIcore8.Setting;

namespace SendingEmailsWithAPIcore8.Services
{
    public class EmailingServices : IEmailingServices
    {

        private readonly EmailSettings _emailSettings;

        public EmailingServices(IOptions<EmailSettings>emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string mailto, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.gmailuser),
                Subject=subject
            };

            email.To.Add(MailboxAddress.Parse(mailto));
            var bulider = new BodyBuilder();
            if (attachments == null)
            {
                Byte[] fileBytes;


                foreach (var file in attachments)
                {
                    if (file.Length> 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                        bulider.Attachments.Add(file.FileName,fileBytes,ContentType.Parse(file.ContentType));
                    }
                }
            }

            bulider.HtmlBody = body;
            email.Body = bulider.ToMessageBody();
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName,_emailSettings.gmailuser));

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.smtpserver,_emailSettings.smtpport,SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.gmailuser,_emailSettings.gmailpassword);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
