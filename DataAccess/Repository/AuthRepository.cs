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
        string e = (email ?? "").Trim().ToLowerInvariant();
        return _db.teachers.FirstOrDefault(t => t.email.ToLower() == e);
    }

    public bool TryRegisterExistingTeacher(
        string email,
        string passwordHash,
        string tokenHash,
        DateTime expiresAtUtc,
        DateTime requestedAtUtc
    )
    {
        string e = (email ?? "").Trim().ToLowerInvariant();
        teachers? teacher = _db.teachers.FirstOrDefault(t => t.email.ToLower() == e);

        if (teacher == null)
            return false;

        // Bestehende Regel (dein aktueller Stand): nur wenn noch nicht registriert
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

    // =========================
    // NEU: 2FA Login Token
    // =========================

    public bool SetLoginToken(string email, string tokenHash, DateTime expiresAtUtc)
    {
        string e = (email ?? "").Trim().ToLowerInvariant();
        teachers? teacher = _db.teachers.FirstOrDefault(t => t.email.ToLower() == e);
        if (teacher == null)
            return false;

        teacher.login_token_hash = tokenHash;
        teacher.login_token_expires_at = expiresAtUtc;

        _db.SaveChanges();
        return true;
    }

    public teachers? GetTeacherByEmailAndLoginTokenHash(string email, string tokenHash)
    {
        string e = (email ?? "").Trim().ToLowerInvariant();
        string h = tokenHash ?? "";

        return _db.teachers.FirstOrDefault(t =>
            t.email.ToLower() == e &&
            t.login_token_hash == h
        );
    }

    public bool ClearLoginToken(string email)
    {
        string e = (email ?? "").Trim().ToLowerInvariant();
        teachers? teacher = _db.teachers.FirstOrDefault(t => t.email.ToLower() == e);
        if (teacher == null)
            return false;

        teacher.login_token_hash = null;
        teacher.login_token_expires_at = null;

        _db.SaveChanges();
        return true;
    }
}
