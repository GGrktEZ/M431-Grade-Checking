using DataAccess.Model;

namespace DataAccess.Repository;

public interface IAuthRepository
{
    teachers? GetTeacherByEmail(string email);

    // NEW: registration update on existing teacher
    bool TryRegisterExistingTeacher(
        string email,
        string passwordHash,
        string tokenHash,
        DateTime expiresAtUtc,
        DateTime requestedAtUtc
    );
}
