using Microsoft.AspNetCore.Mvc;
using Laroa.Domain.Interfaces.Services;
using Laroa.Domain;

namespace Laroa.Api.Controllers
{
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            this._mailService = mailService;
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception exception)
            {
                throw;
            }

        }
    }
}
