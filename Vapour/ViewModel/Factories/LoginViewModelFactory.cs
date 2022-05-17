using System;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;

        public LoginViewModelFactory(IAuthenticator authenticator, INavigator navigator)
        {
            _authenticator = authenticator;
            _navigator = navigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authenticator, _navigator);
        }
    }
}