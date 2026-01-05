using Shared.DTOs;

namespace Services.Service;

public interface IdepartmentsService
{
    Task<IEnumerable<departmentsDto>> GetAllDepartmentsAsync();
    Task<departmentsDto?> GetDepartmentByIdAsync(int id);
    Task<departmentsDto> CreateDepartmentAsync(CreatedepartmentsDto createDto);
    Task<departmentsDto?> UpdateDepartmentAsync(int id, UpdatedepartmentsDto updateDto);
    Task<bool> DeleteDepartmentAsync(int id);
}
