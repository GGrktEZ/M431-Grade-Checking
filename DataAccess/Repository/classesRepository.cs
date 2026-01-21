using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class classesRepository : IclassesRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public classesRepository(AppDbContext db)
    {
        _db = db;
    }
   /// <inheritdoc />
   public classes? GetclassesById(int id)
   {
       return _db.classes.Find(id);
   }

   /// <inheritdoc />
   public IEnumerable<classes> GetAllclassess()
   {
     return _db.classes.ToList();
   }

   /// <inheritdoc />
   public IEnumerable<classes> GetClassesByIds(IEnumerable<int> classIds)
   {
       return _db.classes.Where(c => classIds.Contains(c.class_id)).ToList();
   }

   /// <inheritdoc />
   public void Addclasses(classes newclasses)
   {
       _db.classes.Add(newclasses);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Updateclasses(classes updateclasses)
   {
       _db.classes.Update(updateclasses);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Deleteclasses(classes removeclasses)
   {
 _db.classes.Remove(removeclasses);
       _db.SaveChanges();
   }

}