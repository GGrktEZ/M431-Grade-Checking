using DataAccess.Model;

namespace DataAccess.Repository;

public interface IAuthRepository
{
    /// <summary>
    /// Gets a teacher by email address.
    /// </summary>
    /// <param name="email">The teacher's email address.</param>
    /// <returns>The teacher if found, otherwise null.</returns>
    teachers? GetTeacherByEmail(string email);
}