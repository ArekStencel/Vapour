using System;
using System.Windows.Input;
using Vapour.Model;
using Vapour.State;
using Vapour.View;
using Vapour.ViewModel;

namespace Vapour.Command
{
    public class RegisterCommand : ICommand
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _navigator = navigator;
            _dataContext = dataContext;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("register command");
        }

        public event EventHandler CanExecuteChanged;
    }
}