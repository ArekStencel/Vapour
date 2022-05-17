using System;
using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;

        public LoginViewModelFactory(IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _dataContext = dataContext;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authenticator, _navigator, _dataContext);
        }
    }
}