using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class ActivityService : IActivityService
{
    private readonly AppDbContext _context;

    public ActivityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityExperienceDTO>> GetActivities()
    {
        var activities = await _context.ExperienceActivies.ToListAsync();

        if (activities is null)
        {
            return [];
        }

        var activitiesDtos = activities.Select(act => act.AsDTO());

        return activitiesDtos;
    }

    public async Task<ActivityExperienceDTO> GetActivityByIdAsync(int Id)
    {
        var existActivity = await _context.ExperienceActivies.FirstOrDefaultAsync(e => e.Id == Id);
        if (existActivity is null)
        {
            return null;
        }

        var activityDto = new ActivityExperienceDTO(
            existActivity.Id,
            existActivity.ActivityTitle,
            existActivity.ActivityDescription,
            existActivity.ExperienceId
        );

        return activityDto;
    }
}