using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDTO>> GetProjects()
    {
        var projects = await _context.Projects.ToListAsync();
        if (projects is null)
        {
            return [];
        }

        var projectsDTOs = projects.Select(p => p.AsDTO());

        return projectsDTOs;
    }

    public async Task<ProjectDTO> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project is null)
        {
            return null;
        }

        var projectDto = new ProjectDTO(
            project.Id,
            project.Title,
            project.Repo,
            project.LiveLink,
            project.Description,
            project.Stack
        );

        return projectDto;
    }
}