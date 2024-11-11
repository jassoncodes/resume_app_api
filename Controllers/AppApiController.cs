using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jasson.Codes.Api.Controllers;

[ApiController]
[Route("api/")]
public class AppApiController : ControllerBase
{
    private readonly EndpointDataSource _endPointDataSource;

    public AppApiController(EndpointDataSource endPointDataSource)
    {
        _endPointDataSource = endPointDataSource;
    }
    // enlista todos los endpoints disponibles
    [HttpGet]
    public IActionResult GetApiRoutes()
    {
        var routes = _endPointDataSource.Endpoints
            .Where(e => e is RouteEndpoint)
            .Select(e =>
            {
                var routeEndpoint = e as RouteEndpoint;
                return new
                {
                    RouteTemplate = routeEndpoint.RoutePattern.RawText
                };
            }).ToList();

        return Ok(routes);
    }
}