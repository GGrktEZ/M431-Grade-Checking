using DataAccess.Model;

namespace DataAccess.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _db;

    public AuthRepository(AppDbContext db)
    {
        _db = db;
    }

    public teachers? GetTeacherByEmail(string email)
    {
        return _db.teachers.FirstOrDefault(t => t.email.ToLower() == email.ToLower());
    }

    public bool TryRegisterExistingTeacher(
        string email,
        string passwordHash,
        string tokenHash,
        DateTime expiresAtUtc,
        DateTime requestedAtUtc
    )
    {
        teachers? teacher = _db.teachers.FirstOrDefault(t => t.email.ToLower() == email.ToLower());

        if (teacher == null)
            return false;

        // Regel: keine neuen Lehrer; nur registrieren wenn noch nicht registriert
        // (du kannst alternativ auch auf email_confirmed checken – hier ist es klar über password_hash)
        if (!string.IsNullOrEmpty(teacher.password_hash))
            return false;

        teacher.password_hash = passwordHash;
        teacher.email_confirmed = false;
        teacher.email_confirmation_token_hash = tokenHash;
        teacher.email_confirmation_expires_at = expiresAtUtc;
        teacher.registration_requested_at = requestedAtUtc;
        teacher.email_confirmed_at = null;

        _db.SaveChanges();
        return true;
    }
}
