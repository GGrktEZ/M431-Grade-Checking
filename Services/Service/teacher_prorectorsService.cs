using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class teacher_prorectorsService : Iteacher_prorectorsService
{
    private readonly Iteacher_prorectorsRepository _repository;

    public teacher_prorectorsService(Iteacher_prorectorsRepository repository)
    {
     _repository = repository;
    }

public async Task<IEnumerable<teacher_prorectorsDto>> GetAllTeacherProrectorsAsync()
 {
     var teacherProrectors = await _repository.GetAllAsync();
        return teacherProrectors.Select(tp => new teacher_prorectorsDto
        {
            teacher_id = tp.teacher_id,
            prorector_id_1 = tp.prorector_id_1,
  prorector_id_2 = tp.prorector_id_2
        });
    }

    public async Task<teacher_prorectorsDto?> GetTeacherProrectorByTeacherIdAsync(int teacherId)
  {
        var teacherProrector = await _repository.GetByTeacherIdAsync(teacherId);
        if (teacherProrector == null) return null;

  return new teacher_prorectorsDto
        {
         teacher_id = teacherProrector.teacher_id,
            prorector_id_1 = teacherProrector.prorector_id_1,
      prorector_id_2 = teacherProrector.prorector_id_2
        };
    }

  public async Task<teacher_prorectorsDto> CreateTeacherProrectorAsync(Createteacher_prorectorsDto createDto)
  {
    var teacherProrector = new teacher_prorectors
    {
            teacher_id = createDto.teacher_id,
            prorector_id_1 = createDto.prorector_id_1,
            prorector_id_2 = createDto.prorector_id_2
 };

     var created = await _repository.CreateAsync(teacherProrector);
        return new teacher_prorectorsDto
        {
  teacher_id = created.teacher_id,
            prorector_id_1 = created.prorector_id_1,
            prorector_id_2 = created.prorector_id_2
        };
    }

    public async Task<teacher_prorectorsDto?> UpdateTeacherProrectorAsync(int teacherId, Updateteacher_prorectorsDto updateDto)
    {
        var teacherProrector = new teacher_prorectors
    {
      teacher_id = teacherId,
      prorector_id_1 = updateDto.prorector_id_1,
            prorector_id_2 = updateDto.prorector_id_2
        };

        var updated = await _repository.UpdateAsync(teacherId, teacherProrector);
        if (updated == null) return null;

    return new teacher_prorectorsDto
   {
        teacher_id = updated.teacher_id,
    prorector_id_1 = updated.prorector_id_1,
   prorector_id_2 = updated.prorector_id_2
        };
    }

    public async Task<bool> DeleteTeacherProrectorAsync(int teacherId)
    {
      return await _repository.DeleteAsync(teacherId);
  }
}
