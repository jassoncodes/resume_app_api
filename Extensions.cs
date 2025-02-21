using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Jasson.Codes.Api;

public static class Extensions
{
    public static ContactDTO AsDTO(this Models.Entities.Contact contactInfo)
    {
        if (contactInfo is null)
        {
            throw new ArgumentNullException(nameof(contactInfo));
        }

        return new ContactDTO(
            contactInfo.Id,
            contactInfo.Email,
            contactInfo.Phone,
            contactInfo.Github,
            contactInfo.Linkedin
        );
    }

    public static StudyDTO AsDTO(this Study study)
    {
        if (study is null)
        {
            throw new ArgumentNullException(nameof(study));
        }

        return new StudyDTO(
            study.Id,
            study.Title,
            study.Institution,
            study.StartDate,
            study.EndDate
        );
    }

    public static ExperienceDTO AsDTO(this Experience experience, List<ActivityExperienceDTO> experienceActivities)
    {

        return new ExperienceDTO(
            experience.ExperienceId,
            experience.Title, experience.Company,
            experience.StartDate,
            experience.EndDate,
            experienceActivities
        );
    }


    public static ActivityExperienceDTO AsDTO(this ActivityExperience experienceActivities)
    {
        return new ActivityExperienceDTO(
            experienceActivities.Id,
            experienceActivities.ActivityTitle,
            experienceActivities.ActivityDescription,
            experienceActivities.ExperienceId
        );
    }

    //* ProjectAsDTO Extension Method
    public static ProjectDTO AsDTO(this Project project)
    {
        return new ProjectDTO(
            project.Id,
            project.Title,
            project.Repo,
            project.LiveLink,
            project.Description,
            project.Stack
        );
    }

    public static AboutDTO AsDTO(this About about)
    {
        return new AboutDTO(
            about.Name,
            about.Lastname,
            about.Description
        );
    }

    public static MeDTO AsDTO(this (List<AboutDTO> About,
              List<ContactDTO> ContactInfo,
              List<ExperienceDTO> Experience,
              List<ProjectDTO> Project,
              List<StudyDTO> Studies) me)
    {
        return new MeDTO(
            me.About,
            me.ContactInfo,
            me.Experience,
            me.Project,
            me.Studies
        );
    }


    public static void ApplyMigrations(this IApplicationBuilder app)
    {

        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();

    }
}
