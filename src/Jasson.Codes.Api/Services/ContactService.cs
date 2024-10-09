using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Jasson.Codes.Api.Services;

public class ContactService : IContactService
{
    private readonly AppDbContext _context;

    public ContactService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactDTO>> GetContactInfoAsync()
    {
        var contactInfo = await _context.ContactInfo.ToListAsync();

        if (contactInfo is null)
        {
            return [];
        }

        var contactInfoDTOs = contactInfo.Select(constactInfo => constactInfo.AsDTO());

        return contactInfoDTOs;
    }

    public async Task<ContactDTO> GetContactInfoByIdAsync(int id)
    {
        var contactInfo = await _context.ContactInfo.FindAsync(id);

        if (contactInfo == null)
        {
            return null;
        }

        return contactInfo.AsDTO();
    }
}