using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.Accounting.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class AccountingController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Accounting", status = "stub" });
}
