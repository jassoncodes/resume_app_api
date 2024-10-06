using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Models.DTOs;
using Jasson.Codes.Api.Interfaces;
using Jasson.Codes.Api.Models.Entities;

namespace Jasson.Codes.Api.Controllers;

[Route("api/[controller]")]
// [Route("contact")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly IContactService _contactService;

    public ContactController(AppDbContext context, IContactService contactService)
    {
        _context = context;
        _contactService = contactService;
    }

    // GET: api/ContactInfo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactInfo()
    {
        var contactInfoDTOs = await _contactService.GetContactInfoAsync();

        if (contactInfoDTOs is null)
        {
            return NotFound("No information found");
        }

        return Ok(contactInfoDTOs);
    }

    // GET: api/ContactInfo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDTO>> GetContactInfo(int id)
    {

        var contactInfo = await _contactService.GetContactInfoByIdAsync(id);

        if (contactInfo == null)
        {
            return NotFound($"ContactInfo with Id {id} not found");
        }

        return contactInfo;
    }

    // PUT: api/ContactInfo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutContactInfo(int id, UpdateContactDTO contactInfo)
    {
        if (id == 0)
        {
            return BadRequest($"The Id {id} provided is not a valid Id");
        }

        _context.Entry(contactInfo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContactInfoExists(id))
            {
                return NotFound($"ContactInfo with Id {id} not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/ContactInfo
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ContactDTO>> PostContactInfo(CreateContactDTO newContactInfoDTO)
    {

        var newContact = new Contact
        {
            Email = newContactInfoDTO.Email,
            Phone = newContactInfoDTO.Phone,
            Github = newContactInfoDTO.Github,
            Linkedin = newContactInfoDTO.Linkedin
        };

        _context.ContactInfo.Add(newContact);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetContactInfo", new { id = newContact.Id }, newContact);
    }

    // DELETE: api/ContactInfo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactInfo(int id)
    {
        var contactInfo = await _context.ContactInfo.FindAsync(id);
        if (contactInfo == null)
        {
            return NotFound($"ContactInfo with Id {id} not found");
        }

        _context.ContactInfo.Remove(contactInfo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContactInfoExists(int id)
    {
        return _context.ContactInfo.Any(e => e.Id == id);
    }
}

