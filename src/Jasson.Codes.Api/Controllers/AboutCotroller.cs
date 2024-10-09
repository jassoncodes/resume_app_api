using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IAboutService _aboutService;

    public AboutController(AppDbContext context, IAboutService aboutService)
    {
        _context = context;
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AboutDTO>>> GetAbout()
    {
        var about = await _aboutService.GetAboutInfo();

        if (!about.Any())
        {
            return NotFound("No information found");
        }

        return Ok(about);
    }
}