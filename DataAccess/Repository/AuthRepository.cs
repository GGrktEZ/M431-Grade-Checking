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
    public teachers? GetTeacherByEmail(string email)
    {
        // Use case-insensitive comparison for email lookup
        return _db.teachers.FirstOrDefault(t => t.email.ToLower() == email.ToLower());
    }
}