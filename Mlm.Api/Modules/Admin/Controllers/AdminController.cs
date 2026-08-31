using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class AdminController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Admin", status = "stub" });
}
