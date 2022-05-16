using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public interface IRootViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}