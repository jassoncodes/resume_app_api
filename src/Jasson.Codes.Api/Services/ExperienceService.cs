using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class ExperienceService : IExperienceService
{
    private readonly AppDbContext _context;

    public ExperienceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExperienceDTO>> GetExperience()
    {
        var experiences = await _context.Experiences.Include(e => e.Activities).ToListAsync();

        if (experiences is null)
        {
            return [];
        }

        var experienceDtos = experiences.Select(exp =>
        {
            var activityDtos = exp.Activities
                        .Select(activity => new ActivityExperienceDTO(activity.Id, activity.ActivityTitle, activity.ActivityDescription, activity.ExperienceId))
                        .ToList();

            return exp.AsDTO(activityDtos);

        }).ToList();

        return experienceDtos;
    }

    public async Task<ExperienceDTO> GetExperienceByIdAsync(int id)
    {

        var experience = await _context.Experiences
            .Include(e => e.Activities)
            .SingleOrDefaultAsync(exp => exp.ExperienceId == id);

        if (experience is null)
        {
            return null;
        }

        var experienceDto = new ExperienceDTO(
            experience.ExperienceId,
            experience.Title,
            experience.Company,
            experience.StartDate,
            experience.EndDate,
            experience.Activities.Select(a => new ActivityExperienceDTO(a.Id, a.ActivityTitle, a.ActivityDescription, a.ExperienceId)).ToList()
        );

        return experienceDto;

    }
}