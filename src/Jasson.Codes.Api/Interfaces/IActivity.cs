using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IActivityService
{
    public Task<ActivityExperienceDTO> GetActivityByIdAsync(int Id);
}