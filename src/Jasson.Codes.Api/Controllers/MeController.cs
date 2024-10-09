using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Jasson.Codes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeController : ControllerBase
{
    private readonly IMeService _meService;

    public MeController(IMeService meService)
    {
        _meService = meService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeDTO>>> GetMe()
    {

        var meInfo = await _meService.GetMeInfo();

        foreach (var item in meInfo)
        {
            if (item.About.Count == 0 &&
                item.Contact.Count == 0 &&
                item.Experience.Count == 0 &&
                item.Projects.Count == 0
                && item.Study.Count == 0)
            {
                return NotFound("No information found");
            }
        }

        return Ok(meInfo);
    }
}