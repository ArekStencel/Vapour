using System;
using System.Windows.Input;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel;

namespace Vapour.Command
{
    public class NavigateToStoreViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public NavigateToStoreViewModelCommand(INavigator navigator, VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _dataContext = dataContext;
            _authenticator = authenticator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigator.CurrentViewModel = new StoreViewModel(_dataContext, _authenticator);
        }

        public event EventHandler CanExecuteChanged;
    }
}