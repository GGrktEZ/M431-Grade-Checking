using Services.DTO;
using Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="AuthService"/>.
    /// </summary>
    /// <param name="authService">The reference to the AuthService.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public ActionResult<LoginResponseDto?> Login(LoginRequestDto loginReq)
    {
        LoginResponseDto? loginRes = _authService.VerifyEmail(loginReq);
        return loginRes == null ? Unauthorized() : Ok(loginRes);
    }
}
