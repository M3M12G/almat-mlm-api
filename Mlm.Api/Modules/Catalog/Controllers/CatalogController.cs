using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mlm.Api.Infrastructure.Http;

namespace Mlm.Api.Modules.Catalog.Controllers;

public sealed class CatalogController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { module = "Catalog", status = "stub" });
}
