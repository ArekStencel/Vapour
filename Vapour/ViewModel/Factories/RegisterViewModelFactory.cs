using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class RegisterViewModelFactory : IViewModelFactory<RegisterViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly VapourDatabaseEntities _dataContext;

        public RegisterViewModelFactory(IAuthenticator authenticator, INavigator navigator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _dataContext = dataContext;
        }


        public RegisterViewModel CreateViewModel()
        {
            return new RegisterViewModel(_authenticator, _navigator, _dataContext);
        }
    }
}
