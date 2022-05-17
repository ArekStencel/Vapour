using Vapour.Model;

namespace Vapour.ViewModel.Factories
{
    public class StoreViewModelFactory : IViewModelFactory<StoreViewModel>
    {
        private readonly VapourDatabaseEntities _dataContext;

        public StoreViewModelFactory(VapourDatabaseEntities dataContext)
        {
            _dataContext = dataContext;
        }

        public StoreViewModel CreateViewModel()
        {
            return new StoreViewModel(_dataContext);
        }
    }
}