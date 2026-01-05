using DataAccess.Model;

namespace DataAccess.Repository;

public interface Iteacher_prorectorsRepository
{
    Task<IEnumerable<teacher_prorectors>> GetAllAsync();
    Task<teacher_prorectors?> GetByTeacherIdAsync(int teacherId);
    Task<teacher_prorectors> CreateAsync(teacher_prorectors teacherProrector);
    Task<teacher_prorectors?> UpdateAsync(int teacherId, teacher_prorectors teacherProrector);
    Task<bool> DeleteAsync(int teacherId);
}
