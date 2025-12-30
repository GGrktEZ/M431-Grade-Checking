using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class class_enrollmentsService : Iclass_enrollmentsService
{
    private readonly Iclass_enrollmentsRepository _class_enrollmentsRepository;

    public class_enrollmentsService(Iclass_enrollmentsRepository class_enrollmentsRepository)
    {
  _class_enrollmentsRepository = class_enrollmentsRepository;
    }

    private class_enrollmentsDto ToDto(class_enrollments class_enrollments)
    {
        return new class_enrollmentsDto
        {
   enrollment_id = class_enrollments.enrollment_id,
   class_id = class_enrollments.class_id,
            student_id = class_enrollments.student_id
        };
    }

    private class_enrollments ToModel(class_enrollmentsDto class_enrollmentsDto)
    {
        return new class_enrollments
        {
   enrollment_id = class_enrollmentsDto.enrollment_id,
        class_id = class_enrollmentsDto.class_id,
  student_id = class_enrollmentsDto.student_id
   };
    }

    private class_enrollments ToModel(Createclass_enrollmentsDto createDto)
{
   return new class_enrollments
  {
class_id = createDto.class_id,
            student_id = createDto.student_id
        };
  }

   /// <inheritdoc />
   public class_enrollmentsDto? Getclass_enrollmentsById(int id)
   {
   class_enrollments? class_enrollments = _class_enrollmentsRepository.Getclass_enrollmentsById(id);
       if (class_enrollments == null) return null;
       class_enrollmentsDto class_enrollmentsDto = ToDto(class_enrollments);
 return class_enrollmentsDto;
   }

   /// <inheritdoc />
   public IEnumerable<class_enrollmentsDto> GetAllclass_enrollmentss()
   {
       return _class_enrollmentsRepository.GetAllclass_enrollmentss().Select(x => ToDto(x));
   }

   /// <inheritdoc />
    public int Addclass_enrollments(Createclass_enrollmentsDto class_enrollments)
    {
        class_enrollments createdclass_enrollments = ToModel(class_enrollments);
   _class_enrollmentsRepository.Addclass_enrollments(createdclass_enrollments);
        return createdclass_enrollments.enrollment_id;
    }

   /// <inheritdoc />
   public bool Updateclass_enrollments(int id, Updateclass_enrollmentsDto class_enrollments)
   {
       class_enrollments class_enrollmentsToUpdate = _class_enrollmentsRepository.Getclass_enrollmentsById(id);
  if (class_enrollmentsToUpdate == null) return false;
       
       // Update properties from DTO
       class_enrollmentsToUpdate.class_id = class_enrollments.class_id;
  class_enrollmentsToUpdate.student_id = class_enrollments.student_id;
       
 _class_enrollmentsRepository.Updateclass_enrollments(class_enrollmentsToUpdate);
       return true;
   }

   /// <inheritdoc />
    public bool Deleteclass_enrollments(int id)
    {
class_enrollments class_enrollments = _class_enrollmentsRepository.Getclass_enrollmentsById(id);
 if (class_enrollments == null) return false;
   _class_enrollmentsRepository.Deleteclass_enrollments(class_enrollments);
        return true;
    }
}