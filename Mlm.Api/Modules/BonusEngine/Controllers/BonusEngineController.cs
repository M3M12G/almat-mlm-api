using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mlm.Api.Infrastructure.Http;

namespace Mlm.Api.Modules.BonusEngine.Controllers;

public sealed class BonusEngineController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "BonusEngine", status = "stub" });
}
