using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Services.Service;

/// <summary>
/// Provides password hashing and verification using Argon2id algorithm.
/// </summary>
public class PasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 4;
    private const int MemorySize = 65536; // 64 MB
    private const int Parallelism = 1;

    /// <summary>
    /// Hashes a password using Argon2id algorithm.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>A base64-encoded string containing the salt and hash.</returns>
    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Hash the password
        byte[] hash = HashPasswordInternal(password, salt);

        // Combine salt and hash
        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // Return as base64 string
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Verifies a password against a hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="hashedPassword">The base64-encoded hash to verify against.</param>
    /// <returns>True if the password matches the hash, false otherwise.</returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            // Decode the base64 hash
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Extract the hash
            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

            // Hash the provided password with the same salt
            byte[] computedHash = HashPasswordInternal(password, salt);

            // Compare the hashes
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Internal method to hash a password with a given salt using Argon2id.
    /// </summary>
    private static byte[] HashPasswordInternal(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = Parallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };

        return argon2.GetBytes(HashSize);
    }
}
