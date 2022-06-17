using System.Windows.Input;
using Vapour.Command;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel.Base;
using Vapour.ViewModel.Factories;

namespace Vapour.ViewModel
{
    public class WelcomeViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly VapourDatabaseEntities _dataContext;
        
        public WelcomeViewModel(INavigator navigator, IAuthenticator authenticator, VapourDatabaseEntities dataContext)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _dataContext = dataContext;
            NavigateToStoreViewModelCommand = new NavigateToStoreViewModelCommand(_navigator, _dataContext, _authenticator);

            InitializeValues();
        }

        private void InitializeValues()
        {
            WelcomeText = $"Witaj {_authenticator.CurrentUser.Name} w Vapour. \n\n Najlepszej platformie do grania! \n Aby zakupić grę \n przejdź do sklepu";
        }

        public ICommand NavigateToStoreViewModelCommand { get; }
        
        private string _welcomeText;
        public string WelcomeText
        {
            get => _welcomeText;
            set
            {
                _welcomeText = value;
                OnPropertyChanged(nameof(WelcomeText));
            }
        }
    }
}