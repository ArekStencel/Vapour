using System;
using Vapour.State;

namespace Vapour.ViewModel.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IViewModelFactory<LibraryViewModel> _libraryViewModelFactory;
        private readonly IViewModelFactory<StoreViewModel> _storeViewModelFactory;
        private readonly IViewModelFactory<CommunityViewModel> _communityViewModelFactory;
        private readonly IViewModelFactory<LoginViewModel> _loginViewModelFactory;

        public RootViewModelFactory(IViewModelFactory<LibraryViewModel> libraryViewModelFactory, 
            IViewModelFactory<StoreViewModel> storeViewModelFactory, 
            IViewModelFactory<CommunityViewModel> communityViewModelFactory, 
            IViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            _libraryViewModelFactory = libraryViewModelFactory;
            _storeViewModelFactory = storeViewModelFactory;
            _communityViewModelFactory = communityViewModelFactory;
            _loginViewModelFactory = loginViewModelFactory;
        }


        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel();
                case ViewType.Store:
                    return _storeViewModelFactory.CreateViewModel();
                case ViewType.Community:
                    return _communityViewModelFactory.CreateViewModel();
                case ViewType.Library:
                    return _libraryViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}