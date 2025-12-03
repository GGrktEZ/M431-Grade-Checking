using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class teachersRepository : IteachersRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public teachersRepository(AppDbContext db)
    {
        _db = db;
    }
   /// <inheritdoc />
   public teachers? GetteachersById(int id)
   {
       return _db.teachers.Find(id);
   }

   /// <inheritdoc />
   public IEnumerable<teachers> GetAllteacherss()
   {
       return _db.teachers.ToList();
   }

   /// <inheritdoc />
   public void Addteachers(teachers newteachers)
   {
       _db.teachers.Add(newteachers);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Updateteachers(teachers updateteachers)
   {
       _db.teachers.Update(updateteachers);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Deleteteachers(teachers removeteachers)
   {
       _db.teachers.Remove(removeteachers);
       _db.SaveChanges();
   }

}