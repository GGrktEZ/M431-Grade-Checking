using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Login : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;

    // Model
    protected LoginRequestDto loginDto = new();

    // Form / Validation
    protected EditContext _editContext = default!;

    // UI state
    protected string? ErrorMessage { get; set; }
    protected string? InfoMessage { get; set; }
    protected bool IsLoading { get; set; }

    protected override void OnInitialized()
    {
        _editContext = new EditContext(loginDto);
    }

    // 2FA: Beim Klick wird NUR eine Login-Mail gesendet (kein JWT sofort)
    protected async Task HandleSubmit(EditContext _)
    {
        ErrorMessage = null;
        InfoMessage = null;

        if (!_editContext.Validate())
            return;

        IsLoading = true;

        try
        {
            var response = await Http.PostAsJsonAsync("api/auth", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Login fehlgeschlagen (Daten falsch oder E-Mail nicht bestätigt).";
                return;
            }

            var res = await response.Content.ReadFromJsonAsync<StartLoginResponseDto>();

            if (res == null)
            {
                ErrorMessage = "Login fehlgeschlagen (Server-Antwort ungültig).";
                return;
            }

            if (!res.success)
            {
                ErrorMessage = string.IsNullOrWhiteSpace(res.message)
                    ? "Login fehlgeschlagen."
                    : res.message;
                return;
            }

            InfoMessage = string.IsNullOrWhiteSpace(res.message)
                ? "Bestätigungs-Mail wurde gesendet. Bitte Link klicken."
                : res.message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Request fehlgeschlagen: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
