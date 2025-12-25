using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using System.Net.Http.Json;

namespace Frontend.Pages;

public partial class Login : ComponentBase
{
 [Inject]
    private HttpClient Http { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private LoginRequestDto loginDto = new();
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    private async Task HandleLogin()
    {
        IsLoading = true;
        ErrorMessage = null;

  try
        {
            var response = await Http.PostAsJsonAsync("api/Auth", loginDto);

     if (response.IsSuccessStatusCode)
     {
     var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
 
    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
         {
   // Store token (you may want to use local storage or session storage)
  // For now, just navigate to a success page or dashboard
         Navigation.NavigateTo("/");
      }
            }
       else
   {
       ErrorMessage = "Invalid email or password";
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
