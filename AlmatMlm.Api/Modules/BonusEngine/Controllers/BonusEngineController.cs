using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.BonusEngine.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class BonusEngineController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "BonusEngine", status = "stub" });
}
