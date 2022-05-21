using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vapour.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        public ICommand RegisterFormCommand
        {
            get => (ICommand)GetValue(RegisterFormCommandProperty);
            set => SetValue(RegisterFormCommandProperty, value);
        }

        public static readonly DependencyProperty RegisterFormCommandProperty =
            DependencyProperty.Register("RegisterFormCommand", typeof(ICommand), typeof(RegisterView), new PropertyMetadata(null));

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterFormCommand != null)
            {
                // RegisterFormCommand.Execute();
            }
        }

    }
}
