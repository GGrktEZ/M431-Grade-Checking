using Shared.DTOs;

namespace Services.Service;

public interface Iteacher_classesService
{
    /// <summary>
    /// Gets the teacher_classes with the given composite keys.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <param name="classId">The class ID.</param>
    /// <param name="moduleId">The module ID.</param>
    /// <returns>The teacher_classesDto of the Model with the specified keys.</returns>
    teacher_classesDto? Getteacher_classesByKeys(int teacherId, int classId, int moduleId);

    /// <summary>
  /// Gets all teacher_classess.
    /// </summary>
    /// <returns>A list of all teacher_classesDtos.</returns>
    IEnumerable<teacher_classesDto> GetAllteacher_classess();

    /// <summary>
    /// Gets all classes for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID to filter by.</param>
    /// <returns>A list of class DTOs for the specified teacher.</returns>
    IEnumerable<classesDto> GetClassesByTeacherId(int teacherId);

    /// <summary>
    /// Gets all modules for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID to filter by.</param>
    /// <returns>A list of module DTOs for the specified teacher.</returns>
    IEnumerable<ModuleDto> GetModulesByTeacherId(int teacherId);

    /// <summary>
    /// Adds a teacher_classes to the database via the Createteacher_classesDto.
    /// </summary>
    /// <param name="teacher_classes">A Dto to be added to the database.</param>
    /// <returns>True if successful, false otherwise.</returns>
    bool Addteacher_classes(Createteacher_classesDto teacher_classes);

    /// <summary>
/// Deletes a teacher_classes with the specified composite keys.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <param name="classId">The class ID.</param>
    /// <param name="moduleId">The module ID.</param>
 /// <returns>Whether or not the deletion was successful.</returns>
    bool Deleteteacher_classes(int teacherId, int classId, int moduleId);
}
