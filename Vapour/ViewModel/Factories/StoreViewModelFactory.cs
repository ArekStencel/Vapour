using Vapour.Model;
using Vapour.Services;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class StoreViewModelFactory : IViewModelFactory<StoreViewModel>
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;

        public StoreViewModelFactory(VapourDatabaseEntities dataContext, IAuthenticator authenticator, IUserService userService)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
            _userService = userService;
        }

        public StoreViewModel CreateViewModel()
        {
            return new StoreViewModel(_dataContext, _authenticator, _userService);
        }
    }
}