using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vapour.Command;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel.Base;

namespace Vapour.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand RegisterCommand { get; }

        private ICommand _navigateToLoginFormCommand;
        public ICommand NavigateToLoginFormCommand =>
            _navigateToLoginFormCommand ?? (_navigateToLoginFormCommand = new RelayCommand(
                o =>
                {
                    _navigator.CurrentViewModel = new LoginViewModel(_authenticator, _navigator, _dataContext);
                },
                o => true
            ));

        public RegisterViewModel(IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _dataContext = dataContext;

            RegisterCommand = new RegisterCommand(this, _authenticator, _navigator, _dataContext);
        }
    }
}
