using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly AppDbContext _context;

    public ProjectController(AppDbContext context, IProjectService projectService)
    {
        _context = context;
        _projectService = projectService;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProject()
    {

        var projects = await _projectService.GetProjects();
        if (projects is null)
        {
            return NotFound("No projects were found");
        }

        return Ok(projects);
    }

    // GET: api/Projects/7
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(int id)
    {
        if (id == 0)
        {
            return BadRequest($"The Id {id} provided is not a valid Id");
        }

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project is null)
        {
            return NotFound($"Study with Id {id} not found");
        }

        return Ok(project);

    }

    // POST: api/Project
    [HttpPost]
    public async Task<ActionResult<CreateProjectDTO>> PostProject(CreateProjectDTO createProjectDTO)
    {
        var newProject = new Project
        {
            Title = createProjectDTO.Title,
            Repo = createProjectDTO.Repo,
            LiveLink = createProjectDTO.LiveLink,
            Description = createProjectDTO.Description,
            Stack = createProjectDTO.Stack
        };

        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProject", new { id = newProject.Id }, newProject);
    }

    //PUT: api/Project/7
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, UpdateProjectDTO updateProjectDTO)
    {
        if (id == 0)
        {
            return BadRequest($"The Id {id} provided is not a valid Id");
        }

        var project = await _context.Projects.FindAsync(id);
        if (project is null)
        {
            return NotFound($"Project with Id {id} not found");
        }

        project.Title = updateProjectDTO.Title;
        project.Repo = updateProjectDTO.Repo;
        project.LiveLink = updateProjectDTO.LiveLink;
        project.Stack = updateProjectDTO.Stack;

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            if (!ProjectExists(id))
            {
                return NotFound($"The Id {id} provided is not a valid Id");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Project/7
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound($"Project with Id {id} not found");
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProjectExists(int id)
    {
        return _context.Studies.Any(e => e.Id == id);
    }
}