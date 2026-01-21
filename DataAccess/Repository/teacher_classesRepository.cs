using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class teacher_classesRepository : Iteacher_classesRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public teacher_classesRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public teacher_classes? Getteacher_classesByKeys(int teacherId, int classId, int moduleId)
    {
        return _db.teacher_classes
       .FirstOrDefault(tc => tc.teacher_id == teacherId && tc.class_id == classId && tc.module_id == moduleId);
    }

    /// <inheritdoc />
    public IEnumerable<teacher_classes> GetAllteacher_classess()
    {
        return _db.teacher_classes.ToList();
    }

/// <inheritdoc />
    public IEnumerable<int> GetClassIdsByTeacherId(int teacherId)
    {
     return _db.teacher_classes
            .Where(tc => tc.teacher_id == teacherId)
 .Select(tc => tc.class_id)
       .Distinct()
 .ToList();
    }

    /// <inheritdoc />
    public IEnumerable<int> GetModuleIdsByTeacherId(int teacherId)
    {
        return _db.teacher_classes
        .Where(tc => tc.teacher_id == teacherId)
          .Select(tc => tc.module_id)
       .Distinct()
   .ToList();
    }

    /// <inheritdoc />
    public void Addteacher_classes(teacher_classes newteacher_classes)
    {
        _db.teacher_classes.Add(newteacher_classes);
        _db.SaveChanges();
  }

    /// <inheritdoc />
    public void Deleteteacher_classes(int teacherId, int classId, int moduleId)
    {
        var entity = Getteacher_classesByKeys(teacherId, classId, moduleId);
      if (entity != null)
        {
       _db.teacher_classes.Remove(entity);
  _db.SaveChanges();
        }
    }
}
