using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.LeadershipPool.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class LeadershipPoolController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "LeadershipPool", status = "stub" });
}
