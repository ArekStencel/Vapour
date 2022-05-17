using System;
using System.Windows.Input;
using Vapour.State;
using Vapour.ViewModel.Factories;

namespace Vapour.Command
{
    public class LogoutCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IRootViewModelFactory _viewModelFactory;
        private readonly IAuthenticator _authenticator;

        public LogoutCommand(INavigator navigator, IRootViewModelFactory viewModelFactory, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _authenticator = authenticator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _authenticator.Logout();
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Login);
        }

        public event EventHandler CanExecuteChanged;
    }
}