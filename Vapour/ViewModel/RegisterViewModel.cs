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
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            RegisterCommand = new RegisterCommand(this, authenticator, navigator, dataContext);
        }
    }
}
