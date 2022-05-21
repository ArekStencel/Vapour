using System;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.Services;
using Vapour.ViewModel;

namespace Vapour.State
{
    public class Authenticator : BaseViewModel, IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public bool IsLoggedIn => CurrentUser != null;
        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, string description, int roleId, decimal walletBalance)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword, description, roleId, walletBalance);
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