using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Service;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;

    public AuthController(IAuthService authService, IConfiguration config)
    {
        _authService = authService;
        _config = config;
    }

    // Optional: Direkt-Login (Debug)
    [HttpPost("direct")]
    public ActionResult<LoginResponseDto?> Login([FromBody] LoginRequestDto loginReq)
    {
        LoginResponseDto? loginRes = _authService.VerifyEmail(loginReq);
        return loginRes == null ? Unauthorized() : Ok(loginRes);
    }

    // 2FA Schritt 1: Email+Passwort -> Code senden
    [HttpPost]
    public async Task<ActionResult<StartLoginResponseDto>> StartLogin([FromBody] StartLoginRequestDto req)
    {
        StartLoginResponseDto res = await _authService.StartEmailCodeLoginAsync(req);

        if (!res.success)
            return Unauthorized(res);

        return Ok(res);
    }

    // 2FA Schritt 2: Code bestaetigen -> JWT
    [HttpPost("confirm")]
    public async Task<ActionResult<LoginResponseDto>> ConfirmLogin([FromBody] ConfirmLoginRequestDto req)
    {
        var loginRes = await _authService.ConfirmEmailCodeLoginAsync(req);

        if (loginRes == null)
            return Unauthorized("Code ungueltig oder abgelaufen.");

        return Ok(loginRes);
    }

    // Register bleibt wie bei dir
    [HttpPost("register")]
    public async Task<ActionResult<RegisterTeacherResponseDto>> Register(RegisterTeacherRequestDto req)
    {
        RegisterTeacherResponseDto res = await _authService.RegisterTeacherAsync(req);
        return res.success ? Ok(res) : BadRequest(res);
    }
}
