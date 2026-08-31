using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mlm.Api.Modules.Catalog.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public sealed class CatalogController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Catalog", status = "stub" });
}
