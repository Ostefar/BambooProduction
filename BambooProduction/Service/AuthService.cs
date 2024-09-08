using BambooProduction.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BambooProduction.Service
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _userManager = userManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> GetUserRoleAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(authState.User);

            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);
                return role.FirstOrDefault() ?? "No role assigned";
            }

            return "No role assigned";
        }
    }
}
