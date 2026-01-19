using Shared.DTOs;
using Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

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

    // NEW
    [HttpPost("register")]
    public async Task<ActionResult<RegisterTeacherResponseDto>> Register(RegisterTeacherRequestDto req)
    {
        RegisterTeacherResponseDto res = await _authService.RegisterTeacherAsync(req);
        return res.success ? Ok(res) : BadRequest(res);
    }
}
