using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.BonusEngine.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class BonusEngineController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "BonusEngine", status = "stub" });
}
