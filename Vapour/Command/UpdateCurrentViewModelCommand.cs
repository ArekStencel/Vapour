using System;
using System.Windows.Input;
using Vapour.Services;
using Vapour.State;
using Vapour.ViewModel;

namespace Vapour.Command
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;
        // private IAuthenticator _authenticator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;   
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                var viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Login:
                        _navigator.CurrentViewModel = new LoginViewModel(new Authenticator(new AuthenticationService()));
                        break;
                    case ViewType.Library:
                        _navigator.CurrentViewModel = new LibraryViewModel();
                        break;
                    case ViewType.Store:
                        _navigator.CurrentViewModel = new StoreViewModel();
                        break;
                    case ViewType.Community:
                        _navigator.CurrentViewModel = new CommunityViewModel();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}