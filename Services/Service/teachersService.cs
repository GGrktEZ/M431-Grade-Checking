using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;

namespace Services.Service;

public class teachersService : IteachersService
{
    private readonly IteachersRepository _teachersRepository;

    public teachersService(IteachersRepository teachersRepository)
    {
  _teachersRepository = teachersRepository;
    }

    private teachersDto ToDto(teachers teachers)
    {
        return new teachersDto
        {
   teacher_id = teachers.teacher_id,
   first_name = teachers.first_name,
      last_name = teachers.last_name,
 email = teachers.email
        };
}

    private teachers ToModel(teachersDto teachersDto)
    {
        return new teachers
        {
      teacher_id = teachersDto.teacher_id,
   first_name = teachersDto.first_name,
  last_name = teachersDto.last_name,
        email = teachersDto.email
        };
    }

    private teachers ToModel(CreateteachersDto createDto)
  {
   return new teachers
 {
  first_name = createDto.first_name,
      last_name = createDto.last_name,
            email = createDto.email
};
    }

   /// <inheritdoc />
   public teachersDto? GetteachersById(int id)
   {
       teachers? teachers = _teachersRepository.GetteachersById(id);
       if (teachers == null) return null;
   teachersDto teachersDto = ToDto(teachers);
       return teachersDto;
   }

   /// <inheritdoc />
   public IEnumerable<teachersDto> GetAllteacherss()
   {
   return _teachersRepository.GetAllteacherss().Select(x => ToDto(x));
   }

   /// <inheritdoc />
    public int Addteachers(CreateteachersDto teachers)
    {
        teachers createdteachers = ToModel(teachers);
   _teachersRepository.Addteachers(createdteachers);
        return createdteachers.teacher_id;
    }

   /// <inheritdoc />
   public bool Updateteachers(int id, UpdateteachersDto teachers)
   {
       teachers teachersToUpdate = _teachersRepository.GetteachersById(id);
       if (teachersToUpdate == null) return false;
       
       // Update properties from DTO
   teachersToUpdate.first_name = teachers.first_name;
       teachersToUpdate.last_name = teachers.last_name;
       teachersToUpdate.email = teachers.email;
  
       _teachersRepository.Updateteachers(teachersToUpdate);
       return true;
   }

   /// <inheritdoc />
    public bool Deleteteachers(int id)
    {
 teachers teachers = _teachersRepository.GetteachersById(id);
   if (teachers == null) return false;
        _teachersRepository.Deleteteachers(teachers);
   return true;
    }
}