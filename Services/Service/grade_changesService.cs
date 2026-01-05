using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class grade_changesService : Igrade_changesService
{
    private readonly Igrade_changesRepository _repository;

    public grade_changesService(Igrade_changesRepository repository)
    {
    _repository = repository;
    }

    public async Task<IEnumerable<grade_changesDto>> GetAllGradeChangesAsync()
    {
     var gradeChanges = await _repository.GetAllAsync();
        return gradeChanges.Select(gc => new grade_changesDto
    {
    change_id = gc.change_id,
        class_id = gc.class_id,
   student_id = gc.student_id,
          module_id = gc.module_id,
     teacher_id = gc.teacher_id,
            prorector_id = gc.prorector_id,
   assessment_title = gc.assessment_title,
      old_grade_value = gc.old_grade_value,
   new_grade_value = gc.new_grade_value,
 comment = gc.comment
        });
    }

    public async Task<grade_changesDto?> GetGradeChangeByIdAsync(int id)
    {
        var gradeChange = await _repository.GetByIdAsync(id);
    if (gradeChange == null) return null;

        return new grade_changesDto
        {
         change_id = gradeChange.change_id,
      class_id = gradeChange.class_id,
            student_id = gradeChange.student_id,
     module_id = gradeChange.module_id,
   teacher_id = gradeChange.teacher_id,
    prorector_id = gradeChange.prorector_id,
     assessment_title = gradeChange.assessment_title,
         old_grade_value = gradeChange.old_grade_value,
new_grade_value = gradeChange.new_grade_value,
  comment = gradeChange.comment
     };
    }

    public async Task<IEnumerable<grade_changesDto>> GetGradeChangesByClassIdAsync(int classId)
{
        var gradeChanges = await _repository.GetByClassIdAsync(classId);
        return gradeChanges.Select(gc => new grade_changesDto
  {
  change_id = gc.change_id,
     class_id = gc.class_id,
      student_id = gc.student_id,
          module_id = gc.module_id,
            teacher_id = gc.teacher_id,
   prorector_id = gc.prorector_id,
            assessment_title = gc.assessment_title,
            old_grade_value = gc.old_grade_value,
 new_grade_value = gc.new_grade_value,
            comment = gc.comment
        });
    }

    public async Task<IEnumerable<grade_changesDto>> GetGradeChangesByStudentIdAsync(int studentId)
    {
    var gradeChanges = await _repository.GetByStudentIdAsync(studentId);
        return gradeChanges.Select(gc => new grade_changesDto
        {
change_id = gc.change_id,
     class_id = gc.class_id,
            student_id = gc.student_id,
            module_id = gc.module_id,
         teacher_id = gc.teacher_id,
        prorector_id = gc.prorector_id,
            assessment_title = gc.assessment_title,
          old_grade_value = gc.old_grade_value,
            new_grade_value = gc.new_grade_value,
comment = gc.comment
        });
    }

    public async Task<IEnumerable<grade_changesDto>> GetGradeChangesByTeacherIdAsync(int teacherId)
    {
        var gradeChanges = await _repository.GetByTeacherIdAsync(teacherId);
        return gradeChanges.Select(gc => new grade_changesDto
        {
 change_id = gc.change_id,
       class_id = gc.class_id,
          student_id = gc.student_id,
   module_id = gc.module_id,
          teacher_id = gc.teacher_id,
prorector_id = gc.prorector_id,
       assessment_title = gc.assessment_title,
            old_grade_value = gc.old_grade_value,
  new_grade_value = gc.new_grade_value,
      comment = gc.comment
 });
    }

    public async Task<IEnumerable<grade_changesDto>> GetGradeChangesByProrectorIdAsync(int prorectorId)
    {
      var gradeChanges = await _repository.GetByProrectorIdAsync(prorectorId);
    return gradeChanges.Select(gc => new grade_changesDto
        {
  change_id = gc.change_id,
     class_id = gc.class_id,
            student_id = gc.student_id,
          module_id = gc.module_id,
            teacher_id = gc.teacher_id,
  prorector_id = gc.prorector_id,
        assessment_title = gc.assessment_title,
old_grade_value = gc.old_grade_value,
          new_grade_value = gc.new_grade_value,
       comment = gc.comment
    });
    }

  public async Task<grade_changesDto> CreateGradeChangeAsync(Creategrade_changesDto createDto)
  {
        var gradeChange = new grade_changes
        {
    class_id = createDto.class_id,
            student_id = createDto.student_id,
            module_id = createDto.module_id,
      teacher_id = createDto.teacher_id,
    prorector_id = createDto.prorector_id,
            assessment_title = createDto.assessment_title,
       old_grade_value = createDto.old_grade_value,
 new_grade_value = createDto.new_grade_value,
            comment = createDto.comment
        };

     var created = await _repository.CreateAsync(gradeChange);
        return new grade_changesDto
        {
   change_id = created.change_id,
          class_id = created.class_id,
            student_id = created.student_id,
   module_id = created.module_id,
            teacher_id = created.teacher_id,
  prorector_id = created.prorector_id,
          assessment_title = created.assessment_title,
            old_grade_value = created.old_grade_value,
       new_grade_value = created.new_grade_value,
    comment = created.comment
        };
    }

    public async Task<grade_changesDto?> UpdateGradeChangeAsync(int id, Updategrade_changesDto updateDto)
    {
      var gradeChange = new grade_changes
        {
      class_id = updateDto.class_id,
      student_id = updateDto.student_id,
            module_id = updateDto.module_id,
            teacher_id = updateDto.teacher_id,
    prorector_id = updateDto.prorector_id,
            assessment_title = updateDto.assessment_title,
            old_grade_value = updateDto.old_grade_value,
      new_grade_value = updateDto.new_grade_value,
        comment = updateDto.comment
        };

        var updated = await _repository.UpdateAsync(id, gradeChange);
   if (updated == null) return null;

     return new grade_changesDto
   {
 change_id = updated.change_id,
      class_id = updated.class_id,
            student_id = updated.student_id,
            module_id = updated.module_id,
      teacher_id = updated.teacher_id,
            prorector_id = updated.prorector_id,
        assessment_title = updated.assessment_title,
            old_grade_value = updated.old_grade_value,
            new_grade_value = updated.new_grade_value,
     comment = updated.comment
        };
    }

    public async Task<bool> DeleteGradeChangeAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
