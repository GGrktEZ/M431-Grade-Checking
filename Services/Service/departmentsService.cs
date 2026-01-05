using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class departmentsService : IdepartmentsService
{
    private readonly IdepartmentsRepository _repository;

    public departmentsService(IdepartmentsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<departmentsDto>> GetAllDepartmentsAsync()
    {
  var departments = await _repository.GetAllAsync();
        return departments.Select(d => new departmentsDto
        {
        department_id = d.department_id,
   department_name = d.department_name
        });
    }

    public async Task<departmentsDto?> GetDepartmentByIdAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null) return null;

        return new departmentsDto
    {
     department_id = department.department_id,
       department_name = department.department_name
      };
    }

    public async Task<departmentsDto> CreateDepartmentAsync(CreatedepartmentsDto createDto)
    {
        var department = new departments
        {
            department_name = createDto.department_name
  };

var created = await _repository.CreateAsync(department);
        return new departmentsDto
        {
            department_id = created.department_id,
       department_name = created.department_name
        };
    }

    public async Task<departmentsDto?> UpdateDepartmentAsync(int id, UpdatedepartmentsDto updateDto)
    {
        var department = new departments
        {
  department_name = updateDto.department_name
    };

        var updated = await _repository.UpdateAsync(id, department);
        if (updated == null) return null;

    return new departmentsDto
    {
     department_id = updated.department_id,
       department_name = updated.department_name
      };
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
