using DataAccess.Model;
using DataAccess.Repository;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace Services.Service;

public class teacher_classesService : Iteacher_classesService
{
    private readonly Iteacher_classesRepository _teacher_classesRepository;
    private readonly IclassesRepository _classesRepository;
  private readonly AppDbContext _db;

    public teacher_classesService(
    Iteacher_classesRepository teacher_classesRepository,
   IclassesRepository classesRepository,
      AppDbContext db)
    {
    _teacher_classesRepository = teacher_classesRepository;
        _classesRepository = classesRepository;
        _db = db;
    }

    private teacher_classesDto ToDto(teacher_classes teacher_classes)
    {
        return new teacher_classesDto
        {
  teacher_id = teacher_classes.teacher_id,
       class_id = teacher_classes.class_id,
            module_id = teacher_classes.module_id
        };
    }

    private teacher_classes ToModel(Createteacher_classesDto createDto)
    {
        return new teacher_classes
  {
teacher_id = createDto.teacher_id,
            class_id = createDto.class_id,
            module_id = createDto.module_id
    };
    }

    private classesDto ToClassDto(classes classes)
    {
    return new classesDto
        {
            class_id = classes.class_id,
   class_name = classes.class_name,
            description = classes.description
        };
    }

    private ModuleDto ToModuleDto(modules module)
    {
        return new ModuleDto
        {
            module_id = module.module_id,
            module_code = module.module_code,
            module_name = module.module_name,
            description = module.description
        };
    }

  /// <inheritdoc />
    public teacher_classesDto? Getteacher_classesByKeys(int teacherId, int classId, int moduleId)
    {
        teacher_classes? teacher_classes = _teacher_classesRepository.Getteacher_classesByKeys(teacherId, classId, moduleId);
        if (teacher_classes == null) return null;
        return ToDto(teacher_classes);
    }

    /// <inheritdoc />
    public IEnumerable<teacher_classesDto> GetAllteacher_classess()
    {
      return _teacher_classesRepository.GetAllteacher_classess().Select(x => ToDto(x));
    }

    /// <inheritdoc />
    public IEnumerable<classesDto> GetClassesByTeacherId(int teacherId)
 {
        var classIds = _teacher_classesRepository.GetClassIdsByTeacherId(teacherId);
var classes = _classesRepository.GetClassesByIds(classIds);
        return classes.Select(c => ToClassDto(c));
    }

    /// <inheritdoc />
    public IEnumerable<ModuleDto> GetModulesByTeacherId(int teacherId)
    {
   var moduleIds = _teacher_classesRepository.GetModuleIdsByTeacherId(teacherId);
        var modules = _db.modules.Where(m => moduleIds.Contains(m.module_id)).ToList();
      return modules.Select(m => ToModuleDto(m));
    }

    /// <inheritdoc />
    public bool Addteacher_classes(Createteacher_classesDto teacher_classes)
    {
        try
        {
    teacher_classes createdteacher_classes = ToModel(teacher_classes);
  _teacher_classesRepository.Addteacher_classes(createdteacher_classes);
   return true;
}
        catch
   {
      return false;
        }
    }

    /// <inheritdoc />
    public bool Deleteteacher_classes(int teacherId, int classId, int moduleId)
    {
   try
        {
    _teacher_classesRepository.Deleteteacher_classes(teacherId, classId, moduleId);
       return true;
        }
      catch
        {
            return false;
        }
    }
}
