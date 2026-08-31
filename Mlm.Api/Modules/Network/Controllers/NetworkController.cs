using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.Network.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class NetworkController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Network", status = "stub" });
}
