using System;
using System.Windows.Input;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel;
using Vapour.ViewModel.Factories;

namespace Vapour.Command
{
    public class NavigateToRegisterFormCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly VapourDatabaseEntities _dataContext;

        public NavigateToRegisterFormCommand(INavigator navigator, IAuthenticator authenticator, VapourDatabaseEntities dataContext)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _dataContext = dataContext;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigator.CurrentViewModel = new RegisterViewModel(_authenticator, _navigator, _dataContext);
        }

        public event EventHandler CanExecuteChanged;
    }
}