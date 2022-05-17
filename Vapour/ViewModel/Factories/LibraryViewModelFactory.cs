using Vapour.Model;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class LibraryViewModelFactory : IViewModelFactory<LibraryViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly VapourDatabaseEntities _dataContext;

        public LibraryViewModelFactory(IAuthenticator authenticator, VapourDatabaseEntities dataContext)
        {
            _authenticator = authenticator;
            _dataContext = dataContext;
        }

        public LibraryViewModel CreateViewModel()
        {
            return new LibraryViewModel(_authenticator, _dataContext);
        }
    }
}