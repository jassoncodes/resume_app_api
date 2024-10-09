using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Controllers;

//api/experience
[Route("api/[controller]")]
[ApiController]
public class ExperienceController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IExperienceService _experienceService;

    public ExperienceController(AppDbContext context, IExperienceService experienceService)
    {
        _context = context;
        _experienceService = experienceService;
    }

    // GET: api/Experience
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperienceDTO>>> GetExperience()
    {

        var experiences = await _experienceService.GetExperience();

        if (!experiences.Any())
        {
            return NotFound("No experience information found");
        }

        return Ok(experiences);
    }

    // GET: api/Experience/7
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperienceDTO>> GetExperience(int id)
    {
        var experience = await _experienceService.GetExperienceByIdAsync(id);

        if (experience is null)
        {
            return NotFound($"Experience with Id {id} not found");
        }

        return Ok(experience);
    }

    // POST: api/Experience
    [HttpPost]
    public async Task<ActionResult<CreateExperienceDTO>> PostExperience(CreateExperienceDTO createExperienceDTO)
    {

        var newExperience = new Experience
        {
            Title = createExperienceDTO.Title,
            Company = createExperienceDTO.Company,
            StartDate = createExperienceDTO.StartDate,
            EndDate = createExperienceDTO.EndDate
        };

        _context.Experiences.Add(newExperience);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetExperience", new { id = newExperience.ExperienceId }, newExperience);
    }

    // PUT: api/Experience/7
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExperience(int id, UpdateExperienceDTO updateExperienceDTO)
    {
        if (id == 0)
        {
            return BadRequest($"Experience with Id {id} not found");
        }

        var experience = await _context.Experiences.FindAsync(id);

        if (experience is null)
        {
            return NotFound($"Experience with Id {id} not found");
        }

        experience.Title = updateExperienceDTO.Title;
        experience.Company = updateExperienceDTO.Company;
        experience.StartDate = updateExperienceDTO.StartDate;
        experience.EndDate = updateExperienceDTO.EndDate;

        _context.Entry(experience).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExperienceExists(id))
            {
                return NotFound($"Experience with Id {id} not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Experience/7
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(int id)
    {

        var experience = await _context.Experiences.FindAsync(id);

        if (experience is null)
        {
            return NotFound($"Experience with Id {id} not found");
        }

        _context.Experiences.Remove(experience);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExperienceExists(int id)
    {
        return _context.Experiences.Any(e => e.ExperienceId == id);
    }

}