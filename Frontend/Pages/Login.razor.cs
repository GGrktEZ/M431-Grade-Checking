using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages
{
    public partial class Login
    {
        private bool IsLoading;
        private string? ErrorMessage;
        private string? InfoMessage;

        private bool IsCodeStep;

        private StartLoginRequestDto startReq = new();
        private ConfirmLoginRequestDto confirmReq = new();

        private async Task SendCode()
        {
            ErrorMessage = null;
            InfoMessage = null;
            IsLoading = true;

            try
            {
                var res = await Http.PostAsJsonAsync("api/auth", startReq);
                if (!res.IsSuccessStatusCode)
                {
                    ErrorMessage = "Login fehlgeschlagen (Daten falsch oder E-Mail nicht bestätigt).";
                    return;
                }

                var dto = await res.Content.ReadFromJsonAsync<StartLoginResponseDto>();
                if (dto == null || !dto.success)
                {
                    ErrorMessage = dto?.message ?? "Login fehlgeschlagen.";
                    return;
                }

                InfoMessage = dto.message;
                IsCodeStep = true;

                // Email in ConfirmRequest übernehmen
                confirmReq.email = startReq.email;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ConfirmCode()
        {
            ErrorMessage = null;
            InfoMessage = null;
            IsLoading = true;

            try
            {
                var res = await Http.PostAsJsonAsync("api/auth/confirm", confirmReq);
                if (!res.IsSuccessStatusCode)
                {
                    ErrorMessage = "Code ungültig oder abgelaufen.";
                    return;
                }

                var login = await res.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (login == null || string.IsNullOrWhiteSpace(login.Token) || login.teacher_id <= 0)
                {
                    ErrorMessage = "Login fehlgeschlagen.";
                    return;
                }

                AuthState.Token = login.Token;
                AuthState.TeacherId = login.teacher_id;

                Nav.NavigateTo("/formular");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void BackToPassword()
        {
            IsCodeStep = false;
            confirmReq.code = "";
        }
    }
}