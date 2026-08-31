using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.Ranks.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class RanksController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Ranks", status = "stub" });
}
