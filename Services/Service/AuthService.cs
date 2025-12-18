using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
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

    public AuthService(IAuthRepository authRepository, IConfiguration config)
    {
        _authRepository = authRepository;
        _config = config;
    }

    public LoginResponseDto? VerifyEmail(LoginRequestDto loginReq)
    {
        teachers? teacher = _authRepository.GetTeacherByNameAndEmail(
  loginReq.first_name,
 loginReq.last_name,
   loginReq.email);

        if (teacher == null) return null;

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
