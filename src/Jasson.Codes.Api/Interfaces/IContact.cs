using Jasson.Codes.Api.Data;
using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IContactService
{
    public Task<IEnumerable<ContactDTO>> GetContactInfoAsync();

    public Task<ContactDTO> GetContactInfoByIdAsync(int id);
}