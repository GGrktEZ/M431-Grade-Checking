using DataAccess.Model;
using DataAccess.Repository;
using Services.DTO;

namespace Services.Service;

public class studentsService : IstudentsService
{
    private readonly IstudentsRepository _studentsRepository;

    public studentsService(IstudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    private studentsDto ToDto(students students)
    {
        return new studentsDto
        {
            student_id = students.student_id,
            first_name = students.first_name,
            last_name = students.last_name,
            email = students.email
        };
    }

    private students ToModel(studentsDto studentsDto)
    {
        return new students
        {
            student_id = studentsDto.student_id,
            first_name = studentsDto.first_name,
            last_name = studentsDto.last_name,
            email = studentsDto.email
        };
    }

    private students ToModel(CreatestudentsDto createDto)
    {
        return new students
        {
            first_name = createDto.first_name,
            last_name = createDto.last_name,
            email = createDto.email
        };
    }


   /// <inheritdoc />
   public studentsDto? GetstudentsById(int id)
   {
       students? students = _studentsRepository.GetstudentsById(id);
       if (students == null) return null;
       studentsDto studentsDto = ToDto(students);
       return studentsDto;
   }

   /// <inheritdoc />
   public IEnumerable<studentsDto> GetAllstudentss()
   {
       return _studentsRepository.GetAllstudentss().Select(x => ToDto(x));
   }

   /// <inheritdoc />
    public int Addstudents(CreatestudentsDto students)
    {
        students createdstudents = ToModel(students);
        _studentsRepository.Addstudents(createdstudents);
        return createdstudents.student_id;
    }

   /// <inheritdoc />
   public bool Updatestudents(int id, UpdatestudentsDto students)
   {
       students studentsToUpdate = _studentsRepository.GetstudentsById(id);
    if (studentsToUpdate == null) return false;
       
       // Update properties from DTO
       studentsToUpdate.first_name = students.first_name;
 studentsToUpdate.last_name = students.last_name;
       studentsToUpdate.email = students.email;
       
       _studentsRepository.Updatestudents(studentsToUpdate);
       return true;
   }

   /// <inheritdoc />
    public bool Deletestudents(int id)
    {
    students students = _studentsRepository.GetstudentsById(id);
   if (students == null) return false;
  _studentsRepository.Deletestudents(students);
        return true;
    }
}