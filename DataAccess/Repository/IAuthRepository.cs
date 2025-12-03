using DataAccess.Model;

namespace DataAccess.Repository;

public interface IAuthRepository
{
    /// <summary>
    /// Gets a teacher by first name, last name, and email.
    /// </summary>
    /// <param name="firstName">The teacher's first name.</param>
    /// <param name="lastName">The teacher's last name.</param>
    /// <param name="email">The teacher's email address.</param>
    /// <returns>The teacher if found, otherwise null.</returns>
    teachers? GetTeacherByNameAndEmail(string firstName, string lastName, string email);
}