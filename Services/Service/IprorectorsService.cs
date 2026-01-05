using Shared.DTOs;

namespace Services.Service;

public interface IprorectorsService
{
  Task<IEnumerable<prorectorsDto>> GetAllProrectorsAsync();
    Task<prorectorsDto?> GetProrectorByIdAsync(int id);
    Task<prorectorsDto> CreateProrectorAsync(CreateprorectorsDto createDto);
    Task<prorectorsDto?> UpdateProrectorAsync(int id, UpdateprorectorsDto updateDto);
    Task<bool> DeleteProrectorAsync(int id);
}
