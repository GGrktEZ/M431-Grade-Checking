using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class gradesRepository : IgradesRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public gradesRepository(AppDbContext db)
    {
        _db = db;
    }
   /// <inheritdoc />
   public grades? GetgradesById(int id)
   {
     return _db.grades.Find(id);
   }

   /// <inheritdoc />
   public IEnumerable<grades> GetAllgradess()
   {
       return _db.grades.ToList();
   }

   /// <inheritdoc />
   public void Addgrades(grades newgrades)
   {
   _db.grades.Add(newgrades);
       _db.SaveChanges();
   }

   /// <inheritdoc />
   public void Updategrades(grades updategrades)
   {
    _db.grades.Update(updategrades);
       _db.SaveChanges();
   }

 /// <inheritdoc />
   public void Deletegrades(grades removegrades)
   {
       _db.grades.Remove(removegrades);
       _db.SaveChanges();
   }

}