using System.Security.Cryptography;
using System.Text;

namespace Services.Service;

public static class LoginCode
{
    public static string Generate6DigitCode()
    {
        // 000000 - 999999
        int value = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return value.ToString("D6");
    }

    public static string Hash(string code)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(code));
        // hex ist praktisch fuer DB
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
