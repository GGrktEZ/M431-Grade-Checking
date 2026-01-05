using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class prorectorsService : IprorectorsService
{
  private readonly IprorectorsRepository _repository;

    public prorectorsService(IprorectorsRepository repository)
    {
        _repository = repository;
    }

 public async Task<IEnumerable<prorectorsDto>> GetAllProrectorsAsync()
    {
   var prorectors = await _repository.GetAllAsync();
   return prorectors.Select(p => new prorectorsDto
  {
   prorector_id = p.prorector_id,
        first_name = p.first_name,
     last_name = p.last_name,
       email = p.email,
    department_id_1 = p.department_id_1,
            department_id_2 = p.department_id_2
      });
 }

    public async Task<prorectorsDto?> GetProrectorByIdAsync(int id)
    {
      var prorector = await _repository.GetByIdAsync(id);
        if (prorector == null) return null;

   return new prorectorsDto
  {
            prorector_id = prorector.prorector_id,
  first_name = prorector.first_name,
  last_name = prorector.last_name,
       email = prorector.email,
      department_id_1 = prorector.department_id_1,
   department_id_2 = prorector.department_id_2
        };
    }

    public async Task<prorectorsDto> CreateProrectorAsync(CreateprorectorsDto createDto)
    {
        var prorector = new prorectors
  {
            first_name = createDto.first_name,
        last_name = createDto.last_name,
       email = createDto.email,
  department_id_1 = createDto.department_id_1,
    department_id_2 = createDto.department_id_2
      };

        var created = await _repository.CreateAsync(prorector);
   return new prorectorsDto
 {
   prorector_id = created.prorector_id,
      first_name = created.first_name,
       last_name = created.last_name,
     email = created.email,
  department_id_1 = created.department_id_1,
    department_id_2 = created.department_id_2
        };
    }

    public async Task<prorectorsDto?> UpdateProrectorAsync(int id, UpdateprorectorsDto updateDto)
 {
        var prorector = new prorectors
        {
 first_name = updateDto.first_name,
      last_name = updateDto.last_name,
      email = updateDto.email,
            department_id_1 = updateDto.department_id_1,
 department_id_2 = updateDto.department_id_2
  };

 var updated = await _repository.UpdateAsync(id, prorector);
    if (updated == null) return null;

  return new prorectorsDto
        {
        prorector_id = updated.prorector_id,
      first_name = updated.first_name,
 last_name = updated.last_name,
   email = updated.email,
   department_id_1 = updated.department_id_1,
      department_id_2 = updated.department_id_2
        };
    }

    public async Task<bool> DeleteProrectorAsync(int id)
  {
    return await _repository.DeleteAsync(id);
    }
}
