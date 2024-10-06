using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class StudyService : IStudyService
{
    private readonly AppDbContext _context;

    public StudyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudyDTO>> GetStudies()
    {
        var studies = await _context.Studies.ToListAsync();
        if(studies is null)
        {
            return null;
        }
        var studiesDTOs = studies.Select(study => study.AsDTO());
        
        return studiesDTOs;
    }

    public async Task<StudyDTO> GetStudyByIdAsync(int id)
    {
        var study = await _context.Studies.FirstOrDefaultAsync(study => study.Id == id);

        if (study is null)
        {
            return null;
        }

        var studyDTO = new StudyDTO(
            study.Id,
            study.Title,
            study.Institution,
            study.StartDate,
            study.EndDate
        );

        return studyDTO;
    }
}