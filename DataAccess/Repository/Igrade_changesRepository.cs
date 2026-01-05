using DataAccess.Model;

namespace DataAccess.Repository;

public interface Igrade_changesRepository
{
    Task<IEnumerable<grade_changes>> GetAllAsync();
    Task<grade_changes?> GetByIdAsync(int id);
    Task<IEnumerable<grade_changes>> GetByClassIdAsync(int classId);
    Task<IEnumerable<grade_changes>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<grade_changes>> GetByTeacherIdAsync(int teacherId);
    Task<IEnumerable<grade_changes>> GetByProrectorIdAsync(int prorectorId);
    Task<grade_changes> CreateAsync(grade_changes gradeChange);
    Task<grade_changes?> UpdateAsync(int id, grade_changes gradeChange);
    Task<bool> DeleteAsync(int id);
}
