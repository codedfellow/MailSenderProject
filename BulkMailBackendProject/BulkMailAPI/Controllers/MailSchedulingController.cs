using DTOs.Configurations;
using DTOs.ScheduledMailsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesInterfaces;

namespace BulkMailAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MailSchedulingController : Controller
{
    private readonly IMailSchedulingService _mailSchedulingService;
    private readonly SessionInfo _sessionInfo;
    
    public MailSchedulingController(IMailSchedulingService mailSchedulingService, SessionInfo sessionInfo)
    {
        _mailSchedulingService = mailSchedulingService;
        _sessionInfo = sessionInfo;
    }

    [HttpPost]
    [Route("schedule-mail")]
    public async Task<IActionResult> ScheduleMail(ScheduleMailDto model)
    {
        var response = await _mailSchedulingService.ScheduleMail(model, _sessionInfo.UserId);
        return Ok(new { message = "Mail scheduled successfully." });
    }
}