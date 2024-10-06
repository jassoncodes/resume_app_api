using System.ComponentModel.DataAnnotations;

namespace Jasson.Codes.Api.Models.DTOs;

//* ContactInfoDTOs
public record ContactDTO(int Id, string Email, string Phone, string Github, string Linkedin);
public record CreateContactDTO(string Email, string Phone, string Github, string Linkedin);
public record UpdateContactDTO(string Email, string Phone, string Github, string Linkedin);


//* StudiesDTOs
public record StudyDTO(
    int Id,
    string Title,
    string Institution,
    string StartDate,
    string EndDate
);

public record CreateStudyDTO(
    [Required]
    string Title,

    [Required]
    string Institution,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string StartDate,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string EndDate
);


public record UpdateStudyDTO(
    [Required]
    string Title,

    [Required]
    string Institution,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string StartDate,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "EndDate must be in the format YYYY-MM.")]
    string EndDate
);


//* ExperienceDTOs
public record ExperienceDTO(
    int Id,
    string Title,
    string Company,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string StartDate,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "EndDate must be in the format YYYY-MM.")]
    string EndDate,

    List<ActivityExperienceDTO> ExperienceActivities
);


public record CreateExperienceDTO(

    string Title,
    string Company,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string StartDate,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "EndDate must be in the format YYYY-MM.")]
    string EndDate

);

public record UpdateExperienceDTO(

    string Title,
    string Company,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "StartDate must be in the format YYYY-MM.")]
    string StartDate,

    [Required]
    [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "EndDate must be in the format YYYY-MM.")]
    string EndDate

);


//* Experience_ActivityDTOs
// public record ActivityExperienceDTO(int Id, string ActivityTitle, string ActivityDescription);
public record ActivityExperienceDTO(int Id, string ActivityTitle, string ActivityDescription, int ExperienceId);

public record CreateActivityDTO(string ActivityTitle, string ActivityDescription, int ExperienceId);

public record UpdateActivityDTO(string ActivityTitle, string ActivityDescription, int ExperienceId);


//* ProjectsDTOs
public record ProjectDTO(
    int Id,
    string Title,
    string Repo,
    string LiveLink,
    string Description,
    string Stack
);
public record CreateProjectDTO(
    string Title,
    string Repo,
    string LiveLink,
    string Description,
    string Stack
);

public record UpdateProjectDTO(
    string Title,
    string Repo,
    string LiveLink,
    string Description,
    string Stack
);

public record AboutDTO(
    string Name,
    string Lastname,
    string Description
);


public record MeDTO(
    List<AboutDTO> About,
    List<ContactDTO> Contact,
    List<ExperienceDTO> Experience,
    List<ProjectDTO> Projects,
    List<StudyDTO> Study
);