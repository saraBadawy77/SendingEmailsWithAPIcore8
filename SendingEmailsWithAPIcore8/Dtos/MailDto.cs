namespace SendingEmailsWithAPIcore8.Dtos
{
    public class MailDto
    {
        public string mailto { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public IList<IFormFile> attachments { get; set; }

    }
}
