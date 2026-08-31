using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mlm.Api.Infrastructure.Http;

namespace Mlm.Api.Modules.Network.Controllers;

public sealed class NetworkController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Network", status = "stub" });
}
