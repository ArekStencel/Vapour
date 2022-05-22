using System;
using System.Threading.Tasks;
using System.Windows;
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
            var result = await _authenticationService.Register(email, username, password, confirmPassword, description, roleId, walletBalance);

            switch (result)
            {
                case RegistrationResult.Success:
                    MessageBox.Show("Konto zostało utworzone!", "Sukces", MessageBoxButton.OK);
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    MessageBox.Show("Podany adres e-mail jest już w użyciu!", "Błąd", MessageBoxButton.OK);
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    MessageBox.Show("Podane hasła są różne!", "Błąd", MessageBoxButton.OK);
                    break;
                case RegistrationResult.IncorrectEmail:
                    MessageBox.Show("Błądny adres e-mail!", "Błąd", MessageBoxButton.OK);
                    break;
                case RegistrationResult.IncorrectName:
                    MessageBox.Show("Nazwa użytkownika jest błędna lub za krótka", "Błąd", MessageBoxButton.OK);
                    break;
                case RegistrationResult.WeakPassword:
                    MessageBox.Show("Podane hasło jest za słabe (powinno zawierać co najmniej 8 znaków)", "Błąd", MessageBoxButton.OK);
                    break;
                default:
                    MessageBox.Show("Wystąpił błąd", "Błąd", MessageBoxButton.OK);
                    break;
            }

            return result;
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
                MessageBox.Show("Błędne dane logowania", "Błąd", MessageBoxButton.OK);
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}