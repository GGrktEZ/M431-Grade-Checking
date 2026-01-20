using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Register : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    private RegisterExistingTeacherDto  registerDto = new();
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }
    private bool IsSuccess { get; set; }

    private async Task HandleRegister()
    {
        IsLoading = true;
        ErrorMessage = null;
        IsSuccess = false;

        try
        {
            var response = await Http.PostAsJsonAsync("api/teachers", registerDto);

            if (response.IsSuccessStatusCode)
            {
                IsSuccess = true;
                registerDto = new RegisterExistingTeacherDto ();
                StateHasChanged();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ErrorMessage = string.IsNullOrWhiteSpace(errorContent)
                    ? $"Registration failed: {response.StatusCode}"
                    : errorContent;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
