using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Vapour.Annotations;
using Vapour.Command;
using Vapour.ViewModel;
using Vapour.ViewModel.Factories;

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
    }
}