using DataAccess.Model;
using DataAccess.Repository;
using Services.DTO;

namespace Services.Service;

public class classesService : IclassesService
{
    private readonly IclassesRepository _classesRepository;

    public classesService(IclassesRepository classesRepository)
    {
        _classesRepository = classesRepository;
    }

    private classesDto ToDto(classes classes)
    {
        return new classesDto
        {
            class_id = classes.class_id,
            class_name = classes.class_name,
            description = classes.description,
            teacher_id = classes.teacher_id
        };
    }

    private classes ToModel(classesDto classesDto)
    {
        return new classes
        {
            class_id = classesDto.class_id,
            class_name = classesDto.class_name,
            description = classesDto.description,
            teacher_id = classesDto.teacher_id
        };
    }

    private classes ToModel(CreateclassesDto createDto)
    {
        return new classes
        {
            class_name = createDto.class_name,
            description = createDto.description,
            teacher_id = createDto.teacher_id
        };
    }


   /// <inheritdoc />
   public classesDto? GetclassesById(int id)
   {
       classes? classes = _classesRepository.GetclassesById(id);
       if (classes == null) return null;
       classesDto classesDto = ToDto(classes);
       return classesDto;
   }

   /// <inheritdoc />
   public IEnumerable<classesDto> GetAllclassess()
   {
       return _classesRepository.GetAllclassess().Select(x => ToDto(x));
   }

   /// <inheritdoc />
   public int Addclasses(CreateclassesDto classes)
   {
       classes createdclasses = ToModel(classes);
       _classesRepository.Addclasses(createdclasses);
       return createdclasses.class_id;
   }

   /// <inheritdoc />
   public bool Updateclasses(int id, UpdateclassesDto classes)
   {
       classes classesToUpdate = _classesRepository.GetclassesById(id);
       if (classesToUpdate == null) return false;

       // Update properties from DTO
       classesToUpdate.class_name = classes.class_name;
       classesToUpdate.description = classes.description;
       classesToUpdate.teacher_id = classes.teacher_id;

       _classesRepository.Updateclasses(classesToUpdate);
       return true;
   }

   /// <inheritdoc />
    public bool Deleteclasses(int id)
    {
        classes classes = _classesRepository.GetclassesById(id);
        if (classes == null) return false;
        _classesRepository.Deleteclasses(classes);
        return true;
    }

}