using DTOs.Configurations;
using DTOs.MailDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesInterfaces;

namespace BulkMailAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;
    private readonly SessionInfo _sessionInfo;
    
    public MailController(IMailService mailService, SessionInfo sessionInfo)
    {
        _mailService = mailService;
        _sessionInfo = sessionInfo;
    }
    
    [HttpPost]
    [Route("send-mail")]
    [Authorize]
    public async Task<IActionResult> SendMail([FromBody] SendMailDto model)
    {
        var response = await _mailService.SendMail(model,_sessionInfo.UserId);
        return Ok(new { token = response, message = "Mail sent successfully" });
    }
}