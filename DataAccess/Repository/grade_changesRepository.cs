using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class grade_changesRepository : Igrade_changesRepository
{
    private readonly AppDbContext _context;

    public grade_changesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<grade_changes>> GetAllAsync()
    {
  return await _context.grade_changes.ToListAsync();
    }

    public async Task<grade_changes?> GetByIdAsync(int id)
    {
        return await _context.grade_changes.FindAsync(id);
    }

    public async Task<IEnumerable<grade_changes>> GetByClassIdAsync(int classId)
    {
        return await _context.grade_changes
  .Where(gc => gc.class_id == classId)
     .ToListAsync();
    }

    public async Task<IEnumerable<grade_changes>> GetByStudentIdAsync(int studentId)
    {
        return await _context.grade_changes
    .Where(gc => gc.student_id == studentId)
    .ToListAsync();
    }

    public async Task<IEnumerable<grade_changes>> GetByTeacherIdAsync(int teacherId)
    {
        return await _context.grade_changes
     .Where(gc => gc.teacher_id == teacherId)
          .ToListAsync();
    }

    public async Task<IEnumerable<grade_changes>> GetByProrectorIdAsync(int prorectorId)
    {
      return await _context.grade_changes
            .Where(gc => gc.prorector_id == prorectorId)
.ToListAsync();
    }

    public async Task<grade_changes> CreateAsync(grade_changes gradeChange)
    {
        _context.grade_changes.Add(gradeChange);
        await _context.SaveChangesAsync();
        return gradeChange;
    }

  public async Task<grade_changes?> UpdateAsync(int id, grade_changes gradeChange)
    {
        var existing = await _context.grade_changes.FindAsync(id);
        if (existing == null) return null;

        existing.class_id = gradeChange.class_id;
  existing.student_id = gradeChange.student_id;
        existing.module_id = gradeChange.module_id;
        existing.teacher_id = gradeChange.teacher_id;
  existing.prorector_id = gradeChange.prorector_id;
     existing.assessment_title = gradeChange.assessment_title;
        existing.old_grade_value = gradeChange.old_grade_value;
        existing.new_grade_value = gradeChange.new_grade_value;
        existing.comment = gradeChange.comment;

    await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var gradeChange = await _context.grade_changes.FindAsync(id);
        if (gradeChange == null) return false;

        _context.grade_changes.Remove(gradeChange);
await _context.SaveChangesAsync();
        return true;
    }
}
