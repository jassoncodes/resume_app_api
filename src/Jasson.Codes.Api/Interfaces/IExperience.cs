using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IExperienceService
{
    public Task<IEnumerable<ExperienceDTO>> GetExperience();
    public Task<ExperienceDTO> GetExperienceByIdAsync(int Id);
}