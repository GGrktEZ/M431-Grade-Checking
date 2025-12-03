using DataAccess.Model;

namespace DataAccess.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _db;
    
    /// <summary>
    /// Sets the DbContext reference.
    /// </summary>
    /// <param name="db">The database context to be used for data operations.</param>
    public AuthRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public teachers? GetTeacherByNameAndEmail(string firstName, string lastName, string email)
    {
        return _db.teachers.FirstOrDefault(t => 
         t.first_name == firstName && 
   t.last_name == lastName && 
     t.email == email);
    }
}