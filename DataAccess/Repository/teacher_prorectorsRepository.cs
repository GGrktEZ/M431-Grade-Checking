using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class teacher_prorectorsRepository : Iteacher_prorectorsRepository
{
    private readonly AppDbContext _context;

 public teacher_prorectorsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<teacher_prorectors>> GetAllAsync()
  {
      return await _context.teacher_prorectors.ToListAsync();
    }

    public async Task<teacher_prorectors?> GetByTeacherIdAsync(int teacherId)
    {
        return await _context.teacher_prorectors.FindAsync(teacherId);
    }

    public async Task<teacher_prorectors> CreateAsync(teacher_prorectors teacherProrector)
    {
     _context.teacher_prorectors.Add(teacherProrector);
  await _context.SaveChangesAsync();
        return teacherProrector;
    }

    public async Task<teacher_prorectors?> UpdateAsync(int teacherId, teacher_prorectors teacherProrector)
{
        var existing = await _context.teacher_prorectors.FindAsync(teacherId);
        if (existing == null) return null;

        existing.prorector_id_1 = teacherProrector.prorector_id_1;
     existing.prorector_id_2 = teacherProrector.prorector_id_2;

        await _context.SaveChangesAsync();
 return existing;
    }

public async Task<bool> DeleteAsync(int teacherId)
    {
  var teacherProrector = await _context.teacher_prorectors.FindAsync(teacherId);
  if (teacherProrector == null) return false;

        _context.teacher_prorectors.Remove(teacherProrector);
        await _context.SaveChangesAsync();
   return true;
    }
}
