using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Logging;
using Shared.DTOs;

namespace Services.Service;

public class teachersService : IteachersService
{
    private readonly IteachersRepository _teachersRepository;
  private readonly ILogger<teachersService> _logger;

    public teachersService(IteachersRepository teachersRepository, ILogger<teachersService> logger)
    {
        _teachersRepository = teachersRepository;
  _logger = logger;
    }

    private teachersDto ToDto(teachers teachers)
    {
        return new teachersDto
        {
 teacher_id = teachers.teacher_id,
            first_name = teachers.first_name,
            last_name = teachers.last_name,
      email = teachers.email,
       department_id_1 = teachers.department_id_1,
            department_id_2 = teachers.department_id_2
        };
    }

    private teachers ToModel(teachersDto teachersDto)
    {
      return new teachers
        {
            teacher_id = teachersDto.teacher_id,
          first_name = teachersDto.first_name,
      last_name = teachersDto.last_name,
   email = teachersDto.email,
     password_hash = string.Empty // Not used in this context
        };
    }

    private teachers ToModel(CreateteachersDto createDto)
    {
        var hashedPassword = PasswordHasher.HashPassword(createDto.password);
      _logger.LogInformation("Creating teacher with email: {Email}", createDto.email);
        _logger.LogDebug("Original password: {Password}", createDto.password);
        _logger.LogDebug("Hashed password: {Hash}", hashedPassword);
        _logger.LogDebug("Hash length: {Length}", hashedPassword.Length);

     return new teachers
        {
   first_name = createDto.first_name,
        last_name = createDto.last_name,
  email = createDto.email,
department_id_1 = createDto.department_id_1,
          department_id_2 = createDto.department_id_2,
 password_hash = hashedPassword
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
        _logger.LogInformation("Adding teacher to database: {Email}, Password hash length: {Length}", 
          createdteachers.email, createdteachers.password_hash?.Length ?? 0);
        
     _teachersRepository.Addteachers(createdteachers);
   
     _logger.LogInformation("Teacher added with ID: {TeacherId}", createdteachers.teacher_id);
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
     teachersToUpdate.department_id_1 = teachers.department_id_1;
    teachersToUpdate.department_id_2 = teachers.department_id_2;
 // Note: Password is not updated via this endpoint
  
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