using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class class_enrollmentsRepository : Iclass_enrollmentsRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public class_enrollmentsRepository(AppDbContext db)
    {
        _db = db;
    }
   /// <inheritdoc />
   public class_enrollments? Getclass_enrollmentsById(int id)
   {
       return _db.class_enrollments.Find(id);
   }

   /// <inheritdoc />
   public IEnumerable<class_enrollments> GetAllclass_enrollmentss()
   {
       return _db.class_enrollments.ToList();
   }

   /// <inheritdoc />
   public void Addclass_enrollments(class_enrollments newclass_enrollments)
   {
       _db.class_enrollments.Add(newclass_enrollments);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Updateclass_enrollments(class_enrollments updateclass_enrollments)
   {
       _db.class_enrollments.Update(updateclass_enrollments);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Deleteclass_enrollments(class_enrollments removeclass_enrollments)
   {
       _db.class_enrollments.Remove(removeclass_enrollments);
       _db.SaveChanges();
   }

}