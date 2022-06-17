using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel;
using Vapour.ViewModel.Factories;

namespace Vapour.Command
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _dataContext = dataContext;
            _loginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var success = await _authenticator.Login(_loginViewModel.Email, parameter.ToString());

            if (success)
            {
                _navigator.CurrentViewModel = new WelcomeViewModel(_navigator, _authenticator, _dataContext);
            }
        }
    }
}
