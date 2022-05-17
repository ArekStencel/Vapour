using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vapour.Command;
using Vapour.Model;
using Vapour.State;
using Vapour.ViewModel.Factories;

namespace Vapour.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IRootViewModelFactory _viewModelFactory;
        private readonly VapourDatabaseEntities _dataContext;
        public IAuthenticator Authenticator { get; }
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainWindowViewModel(INavigator navigator, IAuthenticator authenticator, IRootViewModelFactory viewModelFactory, VapourDatabaseEntities dataContext)
        {
            _viewModelFactory = viewModelFactory;
            _dataContext = dataContext;
            Authenticator = authenticator;
            Navigator = navigator;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
