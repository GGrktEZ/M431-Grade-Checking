using Shared.DTOs;

namespace Services.Service;

public interface Iteacher_prorectorsService
{
    Task<IEnumerable<teacher_prorectorsDto>> GetAllTeacherProrectorsAsync();
    Task<teacher_prorectorsDto?> GetTeacherProrectorByTeacherIdAsync(int teacherId);
    Task<teacher_prorectorsDto> CreateTeacherProrectorAsync(Createteacher_prorectorsDto createDto);
    Task<teacher_prorectorsDto?> UpdateTeacherProrectorAsync(int teacherId, Updateteacher_prorectorsDto updateDto);
    Task<bool> DeleteTeacherProrectorAsync(int teacherId);
}
