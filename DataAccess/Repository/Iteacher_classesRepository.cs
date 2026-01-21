using DataAccess.Model;

namespace DataAccess.Repository;

public interface Iteacher_classesRepository
{
    /// <summary>
    /// Gets a teacher_classes by composite key.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <param name="classId">The class ID.</param>
    /// <param name="moduleId">The module ID.</param>
    /// <returns>The teacher_classes model with the specified keys.</returns>
    public teacher_classes? Getteacher_classesByKeys(int teacherId, int classId, int moduleId);

    /// <summary>
    /// Gets all teacher_classes.
    /// </summary>
    /// <returns>A list of teacher_classes models.</returns>
    public IEnumerable<teacher_classes> GetAllteacher_classess();

    /// <summary>
    /// Gets all classes for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID to filter by.</param>
    /// <returns>A list of class IDs for the specified teacher.</returns>
    public IEnumerable<int> GetClassIdsByTeacherId(int teacherId);

    /// <summary>
    /// Gets all modules for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID to filter by.</param>
    /// <returns>A list of module IDs for the specified teacher.</returns>
    public IEnumerable<int> GetModuleIdsByTeacherId(int teacherId);

    /// <summary>
    /// Adds the given model to the Database.
    /// </summary>
    /// <param name="newteacher_classes">The teacher_classes to get added.</param>
    public void Addteacher_classes(teacher_classes newteacher_classes);

    /// <summary>
    /// Deletes the given teacher_classes.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <param name="classId">The class ID.</param>
    /// <param name="moduleId">The module ID.</param>
    public void Deleteteacher_classes(int teacherId, int classId, int moduleId);
}
