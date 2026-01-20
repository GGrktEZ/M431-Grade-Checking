namespace Shared.DTOs;

public class ConfirmLoginRequestDto
{
    public string email { get; set; } = "";
    public string code { get; set; } = "";
}
