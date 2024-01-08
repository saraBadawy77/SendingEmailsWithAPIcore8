using Microsoft.AspNetCore.Mvc;
using SendingEmailsWithAPIcore8.Dtos;
using SendingEmailsWithAPIcore8.Services;

[ApiController]
[Route("api/[controller]")]
public class MailingController : ControllerBase
{
    private readonly IEmailingServices _mailService;

    public MailingController(IEmailingServices mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromForm] MailDto dto)
    {
        await _mailService.SendEmailAsync(dto.mailto, dto.subject, dto.body, dto.attachments);
        return Ok();
    }
}
