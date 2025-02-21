using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IStudyService
{
    public Task<IEnumerable<StudyDTO>> GetStudies();
    public Task<StudyDTO> GetStudyByIdAsync(int id);
}