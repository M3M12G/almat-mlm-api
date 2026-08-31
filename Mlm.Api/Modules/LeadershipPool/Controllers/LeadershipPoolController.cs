using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mlm.Api.Infrastructure.Http;

namespace Mlm.Api.Modules.LeadershipPool.Controllers;

public sealed class LeadershipPoolController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "LeadershipPool", status = "stub" });
}
