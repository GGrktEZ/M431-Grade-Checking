using DataAccess.Model;

namespace DataAccess.Repository;

public interface IAuthRepository
{
    teachers? GetTeacherByEmail(string email);

    // Registration update on existing teacher
    bool TryRegisterExistingTeacher(
        string email,
        string passwordHash,
        string tokenHash,
        DateTime expiresAtUtc,
        DateTime requestedAtUtc
    );

    // NEU: 2FA Login Token
    bool SetLoginToken(string email, string tokenHash, DateTime expiresAtUtc);

    teachers? GetTeacherByEmailAndLoginTokenHash(string email, string tokenHash);

    bool ClearLoginToken(string email);
}
