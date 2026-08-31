using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.Payments.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class PaymentsController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Payments", status = "stub" });
}
