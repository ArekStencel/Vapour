using System;
using System.Windows;
using System.Windows.Input;
using Vapour.Model;
using Vapour.Model.Dto;
using Vapour.State;
using Vapour.View;
using Vapour.ViewModel;
using Vapour.ViewModel.Factories;

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

        public async void Execute(object parameter)
        {
            var newUserDto = (RegisterUserDto) parameter;

            var result = await _authenticator.Register(newUserDto.Email, newUserDto.Name, newUserDto.Password,
                newUserDto.PasswordConfirm, newUserDto.Description, newUserDto.RoleId, newUserDto.WalletBalance);

            if (result == RegistrationResult.Success)
            {
                _navigator.CurrentViewModel = new LoginViewModel(_authenticator, _navigator, _dataContext);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}