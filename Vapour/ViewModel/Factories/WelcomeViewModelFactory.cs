using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class WelcomeViewModelFactory : IViewModelFactory<WelcomeViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly VapourDatabaseEntities _dataContext;

        public WelcomeViewModelFactory(INavigator navigator, IAuthenticator authenticator, VapourDatabaseEntities dataContext)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _dataContext = dataContext;
        }

        public WelcomeViewModel CreateViewModel()
        {
            return new WelcomeViewModel(_navigator, _authenticator, _dataContext);
        }
    }
}