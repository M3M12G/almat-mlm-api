using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.Identity.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class IdentityController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Identity", status = "stub" });
}
