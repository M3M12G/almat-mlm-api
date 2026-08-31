using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mlm.Api.Infrastructure.Http;

namespace Mlm.Api.Modules.Admin.Controllers;

public sealed class AdminController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Admin", status = "stub" });
}
