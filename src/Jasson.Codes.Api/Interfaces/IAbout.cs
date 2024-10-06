using Jasson.Codes.Api.Models.DTOs;

namespace Jasson.Codes.Api.Interfaces;

public interface IAboutService
{
    public Task<IEnumerable<AboutDTO>> GetAboutInfo();
}