using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class gradesService : IgradesService
{
    private readonly IgradesRepository _gradesRepository;

    public gradesService(IgradesRepository gradesRepository)
    {
        _gradesRepository = gradesRepository;
    }

    private gradesDto ToDto(grades grades)
    {
        return new gradesDto
        {
            grade_id = grades.grade_id,
            enrollment_id = grades.enrollment_id,
            grade_value = grades.grade_value,
            grade_timestamp = grades.grade_timestamp,
            comment = grades.comment
        };
    }

    private grades ToModel(gradesDto gradesDto)
    {
        return new grades
        {
            grade_id = gradesDto.grade_id,
            enrollment_id = gradesDto.enrollment_id,
            grade_value = gradesDto.grade_value,
            grade_timestamp = gradesDto.grade_timestamp,
            comment = gradesDto.comment
        };
    }

    private grades ToModel(CreategradesDto createDto)
    {
        return new grades
        {
            enrollment_id = createDto.enrollment_id,
            grade_value = createDto.grade_value,
            grade_timestamp = createDto.grade_timestamp,
            comment = createDto.comment
        };
    }


   /// <inheritdoc />
   public gradesDto? GetgradesById(int id)
   {
       grades? grades = _gradesRepository.GetgradesById(id);
       if (grades == null) return null;
       gradesDto gradesDto = ToDto(grades);
       return gradesDto;
   }

   /// <inheritdoc />
   public IEnumerable<gradesDto> GetAllgradess()
   {
       return _gradesRepository.GetAllgradess().Select(x => ToDto(x));
   }

 /// <inheritdoc />
    public int Addgrades(CreategradesDto grades)
    {
  grades createdgrades = ToModel(grades);
        _gradesRepository.Addgrades(createdgrades);
        return createdgrades.grade_id;
  }

   /// <inheritdoc />
   public bool Updategrades(int id, UpdategradesDto grades)
   {
       grades gradesToUpdate = _gradesRepository.GetgradesById(id);
     if (gradesToUpdate == null) return false;
       
       // Update properties from DTO
       gradesToUpdate.enrollment_id = grades.enrollment_id;
   gradesToUpdate.grade_value = grades.grade_value;
       gradesToUpdate.grade_timestamp = grades.grade_timestamp;
  gradesToUpdate.comment = grades.comment;
     
     _gradesRepository.Updategrades(gradesToUpdate);
       return true;
   }

   /// <inheritdoc />
    public bool Deletegrades(int id)
    {
  grades grades = _gradesRepository.GetgradesById(id);
    if (grades == null) return false;
      _gradesRepository.Deletegrades(grades);
        return true;
    }
}