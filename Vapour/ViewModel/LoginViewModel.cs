using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vapour.Command;
using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
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

        public ICommand LoginCommand { get; }

        public ICommand NavigateToRegisterFormCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            LoginCommand = new LoginCommand(this, authenticator, navigator, dataContext);
            NavigateToRegisterFormCommand = new NavigateToRegisterFormCommand(navigator, authenticator, dataContext);
        }

    }
}
