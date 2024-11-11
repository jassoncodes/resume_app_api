using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IMeService
{
    public Task<IEnumerable<MeDTO>> GetMeInfo();
}