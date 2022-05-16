namespace Vapour.ViewModel.Factories
{
    public interface IViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}