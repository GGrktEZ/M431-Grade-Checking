using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class studentsRepository : IstudentsRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public studentsRepository(AppDbContext db)
    {
        _db = db;
    }
   /// <inheritdoc />
   public students? GetstudentsById(int id)
   {
       return _db.students.Find(id);
   }

   /// <inheritdoc />
   public IEnumerable<students> GetAllstudentss()
   {
       return _db.students.ToList();
   }

   /// <inheritdoc />
   public void Addstudents(students newstudents)
   {
       _db.students.Add(newstudents);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Updatestudents(students updatestudents)
   {
       _db.students.Update(updatestudents);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Deletestudents(students removestudents)
   {
       _db.students.Remove(removestudents);
       _db.SaveChanges();
   }

}