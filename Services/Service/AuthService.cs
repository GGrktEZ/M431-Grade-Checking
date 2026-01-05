using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Service;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IAuthRepository authRepository, IConfiguration config, ILogger<AuthService> logger)
    {
        _authRepository = authRepository;
        _config = config;
        _logger = logger;
    }

    public LoginResponseDto? VerifyEmail(LoginRequestDto loginReq)
    {
        _logger.LogInformation("Login attempt for email: {Email}", loginReq.email);
        
        teachers? teacher = _authRepository.GetTeacherByEmail(loginReq.email);

        if (teacher == null)
        {
            _logger.LogWarning("Teacher not found for email: {Email}", loginReq.email);
            return null;
        }

        _logger.LogInformation("Teacher found: ID={TeacherId}, Email={Email}", teacher.teacher_id, teacher.email);

        // Check if password_hash is null or empty
        if (string.IsNullOrEmpty(teacher.password_hash))
        {
            _logger.LogWarning("Password hash is null or empty for teacher ID: {TeacherId}", teacher.teacher_id);
            return null;
        }

        _logger.LogInformation("Password hash exists, length: {Length}", teacher.password_hash.Length);
        _logger.LogDebug("Stored hash: {Hash}", teacher.password_hash);
        _logger.LogDebug("Provided password: {Password}", loginReq.password);

        // Verify the password using Argon2
        bool isPasswordValid = PasswordHasher.VerifyPassword(loginReq.password, teacher.password_hash);
        _logger.LogInformation("Password verification result: {Result}", isPasswordValid);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Password verification failed for teacher ID: {TeacherId}", teacher.teacher_id);
            return null;
        }

        _logger.LogInformation("Login successful for teacher ID: {TeacherId}", teacher.teacher_id);
        return new LoginResponseDto { Token = GenerateJwtToken(teacher) };
    }

    private string GenerateJwtToken(teachers teacher)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

        Claim[] claims = new[]
      {
       new Claim(JwtRegisteredClaimNames.Sub, teacher.teacher_id.ToString()),
    new Claim(JwtRegisteredClaimNames.Email, teacher.email),
            new Claim(JwtRegisteredClaimNames.GivenName, teacher.first_name),
  new Claim(JwtRegisteredClaimNames.FamilyName, teacher.last_name)
      };

        SecurityTokenDescriptor tokenDescriptor = new
       SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
    new SymmetricSecurityKey(key),
         SecurityAlgorithms.HmacSha256Signature
       )
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
