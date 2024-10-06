using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IExperienceService _experienceService;
    private readonly IActivityService _activityService;

    public ActivityController(AppDbContext context, IExperienceService experienceService, IActivityService activityService)
    {
        _context = context;
        _experienceService = experienceService;
        _activityService = activityService;
    }

    // GET: api/Activity
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityExperienceDTO>>> GetActivities()
    {
        var activities = await _context.ExperienceActivies.ToListAsync();
        var activitiesDtos = activities.Select(act => act.AsDTO());

        return Ok(activitiesDtos);
    }

    // GET: api/Activity/7
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityExperienceDTO>> GetActivitiesById(int id)
    {
        var activity = await _activityService.GetActivityByIdAsync(id);

        if (activity is null)
        {
            return NotFound($"Activity with Id {id} not found");
        }
        return Ok(activity);
    }

    // POPST: api/Activity
    [HttpPost]
    public async Task<ActionResult<List<ActivityExperienceDTO>>> PostActivities(List<CreateActivityDTO> createActivities)
    {

        List<CreateActivityDTO> activitiesList = createActivities;
        var createdActivities = new List<ActivityExperience>();
        var firstActivityExperienceId = createActivities.First().ExperienceId;
        var existsExperience = await _experienceService.GetExperienceByIdAsync(firstActivityExperienceId);

        if (existsExperience is null)
        {
            return NotFound($"Experience with Id {firstActivityExperienceId} not found");
        }

        activitiesList.ForEach(activity =>
        {
            var newActivityExp = new ActivityExperience
            {
                ActivityTitle = activity.ActivityTitle,
                ActivityDescription = activity.ActivityDescription,
                ExperienceId = activity.ExperienceId
            };

            _context.Add(newActivityExp);
            createdActivities.Add(newActivityExp);
        });

        await _context.SaveChangesAsync();

        var createdActivitiesDtos = createdActivities.Select(act => new ActivityExperienceDTO(act.Id, act.ActivityTitle, act.ActivityDescription, act.ExperienceId)).ToList();

        return CreatedAtAction(nameof(GetActivities), null, createdActivitiesDtos);
    }

    // PUT: api/Activity/7
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(int id, UpdateActivityDTO updateActivityDTO)
    {
        if (id == 0)
        {
            return BadRequest($"Activity with Id {id} not found");
        }

        var activity = await _context.ExperienceActivies.FindAsync(id);
        if (activity is null)
        {
            return NotFound($"Activity with Id {id} not found");
        }

        activity.ActivityTitle = updateActivityDTO.ActivityTitle;
        activity.ActivityDescription = updateActivityDTO.ActivityDescription;
        activity.ExperienceId = updateActivityDTO.ExperienceId;

        _context.Entry(activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivityExists(id))
            {
                return NotFound($"Activity with Id {id} not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();

    }

    // DELETE: api/Activity/7
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _context.ExperienceActivies.FindAsync(id);
        if (!ActivityExists(id))
        {
            return NotFound($"Activity with Id {id} not found");
        }

        _context.ExperienceActivies.Remove(activity);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool ActivityExists(int id)
    {
        return _context.ExperienceActivies.Any(e => e.Id == id);
    }

}