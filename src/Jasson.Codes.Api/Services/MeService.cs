using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using NuGet.Protocol;

namespace Jasson.Codes.Api.Services;

public class MeService : IMeService
{
    private readonly IAboutService _aboutService;
    private readonly IContactService _contacService;
    private readonly IExperienceService _experienceService;
    private readonly IProjectService _projectService;
    private readonly IStudyService _studyService;

    public MeService(
        IAboutService aboutService,
        IContactService contacService,
        IExperienceService experienceService,
        IProjectService projectService,
        IStudyService studyService
    )
    {
        _aboutService = aboutService;
        _contacService = contacService;
        _experienceService = experienceService;
        _projectService = projectService;
        _studyService = studyService;
    }

    public async Task<IEnumerable<MeDTO>> GetMeInfo()
    {
        try
        {
            var aboutInfo = (await _aboutService.GetAboutInfo()).ToList();

            var contactInfo = (await _contacService.GetContactInfoAsync()).ToList();

            var experienceInfo = (await _experienceService.GetExperience()).ToList();

            var projectsInfo = (await _projectService.GetProjects()).ToList();

            var studiesInfo = (await _studyService.GetStudies()).ToList();

            var meDTO = new MeDTO(
                aboutInfo,
                contactInfo,
                experienceInfo,
                projectsInfo,
                studiesInfo
            );

            return [meDTO];

        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            return [];
        }
    }
}