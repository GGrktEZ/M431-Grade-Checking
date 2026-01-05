using Shared.DTOs;

namespace Services.Service;

public interface Igrade_changesService
{
    Task<IEnumerable<grade_changesDto>> GetAllGradeChangesAsync();
    Task<grade_changesDto?> GetGradeChangeByIdAsync(int id);
    Task<IEnumerable<grade_changesDto>> GetGradeChangesByClassIdAsync(int classId);
    Task<IEnumerable<grade_changesDto>> GetGradeChangesByStudentIdAsync(int studentId);
    Task<IEnumerable<grade_changesDto>> GetGradeChangesByTeacherIdAsync(int teacherId);
    Task<IEnumerable<grade_changesDto>> GetGradeChangesByProrectorIdAsync(int prorectorId);
    Task<grade_changesDto> CreateGradeChangeAsync(Creategrade_changesDto createDto);
    Task<grade_changesDto?> UpdateGradeChangeAsync(int id, Updategrade_changesDto updateDto);
    Task<bool> DeleteGradeChangeAsync(int id);
}
