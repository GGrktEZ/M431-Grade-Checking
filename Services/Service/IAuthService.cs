using Shared.DTOs;

namespace Services.Service;

public interface IAuthService
{
    // Bestehend (direktes JWT Login)
    LoginResponseDto? VerifyEmail(LoginRequestDto loginReq);

    // Bestehend (Registration)
    Task<RegisterTeacherResponseDto> RegisterTeacherAsync(RegisterTeacherRequestDto req);

    // NEU (2FA / Login per Mail)
    Task<StartLoginResponseDto> StartEmail2FaLoginAsync(LoginRequestDto loginReq);

    Task<LoginResponseDto?> ConfirmEmail2FaLoginAsync(string email, string token);
    Task<StartLoginResponseDto> StartEmailCodeLoginAsync(StartLoginRequestDto req);
    Task<LoginResponseDto?> ConfirmEmailCodeLoginAsync(ConfirmLoginRequestDto req);

}
