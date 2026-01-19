using Shared.DTOs;

namespace Services.Service;

public interface IAuthService
{
    LoginResponseDto? VerifyEmail(LoginRequestDto loginReq);

    // NEW
    Task<RegisterTeacherResponseDto> RegisterTeacherAsync(RegisterTeacherRequestDto req);
}
