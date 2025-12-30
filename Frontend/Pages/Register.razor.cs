using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    private HttpClient Http { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private CreateteachersDto registerDto = new();
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
                registerDto = new CreateteachersDto(); // Reset form
                StateHasChanged();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ErrorMessage = $"Registration failed: {response.StatusCode}";
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
