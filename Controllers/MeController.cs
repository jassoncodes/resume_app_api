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
    public async Task<ActionResult<MeDTO>> GetMe()
    {

        var meInfo = await _meService.GetMeInfo();
        if (!meInfo.Any())
        {
            return NoContent();
        }

        return Ok(meInfo);

    }
}