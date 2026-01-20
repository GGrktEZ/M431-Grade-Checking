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

    // Bestehender Login (direkt JWT) - kannst du fuer Debug/Alt-Flow behalten
    [HttpPost("direct")]
    public ActionResult<LoginResponseDto?> Login(LoginRequestDto loginReq)
    {
        LoginResponseDto? loginRes = _authService.VerifyEmail(loginReq);
        return loginRes == null ? Unauthorized() : Ok(loginRes);
    }

    // 2FA Start: Passwort pruefen -> Login-Mail senden
    // Frontend klickt "Login" und bekommt nur eine Message zurueck.
    [HttpPost]
    public async Task<ActionResult<StartLoginResponseDto>> StartLogin(LoginRequestDto loginReq)
    {
        StartLoginResponseDto res = await _authService.StartEmail2FaLoginAsync(loginReq);

        if (!res.success)
            return Unauthorized(res);

        return Ok(res);
    }

    // 2FA Confirm: Link in Mail -> Token pruefen -> JWT erstellen
    // Wenn FrontendBaseUrl gesetzt ist, redirectet er zur Frontend Callback-Route und uebergibt token/teacherId.
    [HttpGet("confirm-login")]
    public async Task<IActionResult> ConfirmLogin([FromQuery] string email, [FromQuery] string token)
    {
        LoginResponseDto? loginRes = await _authService.ConfirmEmail2FaLoginAsync(email, token);

        if (loginRes == null)
            return BadRequest("Login-Link ungueltig oder abgelaufen.");

        string? frontendBase = _config["Auth:FrontendBaseUrl"]?.TrimEnd('/');

        if (!string.IsNullOrWhiteSpace(frontendBase))
        {
            string redirectUrl =
                $"{frontendBase}/auth-callback?token={Uri.EscapeDataString(loginRes.Token)}&teacherId={loginRes.teacher_id}";
            return Redirect(redirectUrl);
        }

        // Fallback: JSON anzeigen (falls du noch keinen Callback im Frontend hast)
        return Ok(loginRes);
    }

    // Bestehender Register-Endpoint (wie bei dir)
    [HttpPost("register")]
    public async Task<ActionResult<RegisterTeacherResponseDto>> Register(RegisterTeacherRequestDto req)
    {
        RegisterTeacherResponseDto res = await _authService.RegisterTeacherAsync(req);
        return res.success ? Ok(res) : BadRequest(res);
    }
}
