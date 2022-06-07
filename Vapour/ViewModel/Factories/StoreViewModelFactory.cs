using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class StoreViewModelFactory : IViewModelFactory<StoreViewModel>
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public StoreViewModelFactory(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
        }

        public StoreViewModel CreateViewModel()
        {
            return new StoreViewModel(_dataContext, _authenticator);
        }
    }
}