namespace Vapour.ViewModel.Factories
{
    public class LibraryViewModelFactory : IViewModelFactory<LibraryViewModel>
    {
        public LibraryViewModel CreateViewModel()
        {
            return new LibraryViewModel();
        }
    }
}