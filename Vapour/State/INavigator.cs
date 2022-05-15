using System.Windows.Input;
using Vapour.ViewModel;

namespace Vapour.State
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}   