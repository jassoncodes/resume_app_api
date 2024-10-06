using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Models.Entities;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudyController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStudyService _studyService;

    public StudyController(AppDbContext context, IStudyService studyService)
    {
        _context = context;
        _studyService = studyService;
    }

    // GET: api/Studies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudyDTO>>> GetStudies()
    {
        var studies = await _studyService.GetStudies();
        if (studies is null)
        {
            return NotFound("Not studies were found");
        }

        return Ok(studies);
    }

    // GET: api/Studies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudyDTO>> GetStudies(int id)
    {
        var study = await _studyService.GetStudyByIdAsync(id);

        if (study == null)
        {
            return NotFound($"Study with Id {id} not found");
        }

        return Ok(study);
    }

    // PUT: api/Studies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudy(int id, UpdateStudyDTO studyDTO)
    {
        if (id == 0)
        {
            return BadRequest($"The Id {id} provided is not a valid Id");
        }

        var study = await _context.Studies.FindAsync(id);

        if (study is null)
        {
            return NotFound($"Study with Id {id} not found");
        }

        study.Title = studyDTO.Title;
        study.Institution = studyDTO.Institution;
        study.StartDate = studyDTO.StartDate;
        study.EndDate = study.EndDate;


        _context.Entry(study).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudyExists(id))
            {
                return NotFound($"The Id {id} provided is not a valid Id");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Studies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<CreateStudyDTO>> PostStudy(CreateStudyDTO newStudyDTO)
    {

        var newStudy = new Study
        {
            Title = newStudyDTO.Title,
            Institution = newStudyDTO.Institution,
            StartDate = newStudyDTO.StartDate,
            EndDate = newStudyDTO.EndDate
        };

        _context.Studies.Add(newStudy);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudies", new { id = newStudy.Id }, newStudy);
    }

    // DELETE: api/Studies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudy(int id)
    {
        var study = await _context.Studies.FindAsync(id);
        if (study == null)
        {
            return NotFound($"Study with Id {id} not found");
        }

        _context.Studies.Remove(study);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudyExists(int id)
    {
        return _context.Studies.Any(e => e.Id == id);
    }
}
