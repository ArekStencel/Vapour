using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.State;

namespace Vapour.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }

        public MainWindowViewModel(INavigator navigator)
        {
            Navigator = navigator;

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
