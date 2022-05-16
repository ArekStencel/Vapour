namespace Vapour.ViewModel.Factories
{
    public class StoreViewModelFactory : IViewModelFactory<StoreViewModel>
    {
        public StoreViewModel CreateViewModel()
        {
            return new StoreViewModel();
        }
    }
}