namespace Frontend;

public class AuthState
{
    public string? Token { get; set; }
    public int? TeacherId { get; set; }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Token) && TeacherId.HasValue;
}
