using Microsoft.AspNetCore.Components;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiAuthenticationStateProvider _authProvider;
        private readonly NavigationManager _navigationManager;

        public AuthService(HttpClient httpClient, ApiAuthenticationStateProvider authProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authProvider = authProvider;
            _navigationManager = navigationManager;
        }

        public async Task<(bool Success, string? ErrorMessage)> LoginAsync(string email, string password)
        {
            try
            {
                // Create the login request model
                var loginModel = new LoginRequestModel
                {
                    Email = email,
                    Password = password
                };

                // Send the login request to the API
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5031/api/Authentication/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    // If login is successful, retrieve the JWT token from the response
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    // Mark user as authenticated
                    _authProvider.MarkUserAsAuthenticated(loginResponse.Token);

                    return (true, null); // Successful login, no error message
                }
                else
                {
                    return (false, "Invalid login attempt."); // Unsuccessful login with error message
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failure with the exception message
                return (false, $"Error: {ex.Message}");
            }
        }

        public void NavigateAfterLogin(string? returnUrl = null)
        {
            _navigationManager.NavigateTo(returnUrl ?? "/");
        }
    }

    public class LoginRequestModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }

}
