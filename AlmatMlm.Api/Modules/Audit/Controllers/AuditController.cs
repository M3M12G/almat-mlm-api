using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.Audit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class AuditController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Audit", status = "stub" });
}
