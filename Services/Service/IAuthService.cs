using Services.DTO;

namespace Services.Service;

public interface IAuthService
{
    //Comment
    public LoginResponseDto? VerifyEmail(LoginRequestDto loginReq);
}
