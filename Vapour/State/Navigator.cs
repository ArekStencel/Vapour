using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Vapour.Annotations;
using Vapour.Command;
using Vapour.ViewModel;

namespace Vapour.State
{
    public class Navigator : BaseViewModel, INavigator
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}