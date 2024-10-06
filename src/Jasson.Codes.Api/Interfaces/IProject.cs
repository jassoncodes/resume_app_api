using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IProjectService
{
    public Task<IEnumerable<ProjectDTO>> GetProjects();
    public Task<ProjectDTO> GetProjectByIdAsync(int Id);
}