using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class LibraryViewModelFactory : IViewModelFactory<LibraryViewModel>
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public LibraryViewModelFactory(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
        }

        public LibraryViewModel CreateViewModel()
        {
            return new LibraryViewModel(_dataContext, _authenticator);
        }
    }
}