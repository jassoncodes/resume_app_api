using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class AboutService : IAboutService
{
    private readonly AppDbContext _context;

    public AboutService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<AboutDTO>> GetAboutInfo()
    {
        var about = await _context.AboutInfo.SingleOrDefaultAsync();

        if (about is null)
        {
            return null;
        }

        var aboutDTO = new AboutDTO(
            about.Name,
            about.Lastname,
            about.Description
        );

        List<AboutDTO> aboutInfo = [aboutDTO];

        return aboutInfo;
    }
}