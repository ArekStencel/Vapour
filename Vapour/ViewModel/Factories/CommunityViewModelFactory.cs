namespace Vapour.ViewModel.Factories
{
    public class CommunityViewModelFactory : IViewModelFactory<CommunityViewModel>
    {
        public CommunityViewModel CreateViewModel()
        {
            return new CommunityViewModel();
        }
    }
}