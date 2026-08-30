using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlmatMlm.Api.Modules.Wallet.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class WalletController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Wallet", status = "stub" });
}
