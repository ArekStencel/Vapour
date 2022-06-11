using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class CommunityViewModelFactory : IViewModelFactory<CommunityViewModel>
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public CommunityViewModelFactory(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
        }

        public CommunityViewModel CreateViewModel()
        {
            return new CommunityViewModel(_dataContext, _authenticator);
        }
    }
}