using System;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.Services;

namespace Vapour.State
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public User CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;
        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, int roleId)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword, roleId);
        }

        public async Task<bool> Login(string email, string password)
        {
            var success = true;

            try
            {
                CurrentUser = await _authenticationService.Login(email, password);
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}