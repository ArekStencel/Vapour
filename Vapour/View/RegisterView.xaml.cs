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
using Vapour.Command;
using Vapour.Model.Dto;

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

        public ICommand RegisterCommand
        {
            get => (ICommand)GetValue(RegisterCommandProperty);
            set => SetValue(RegisterCommandProperty, value);
        }

        public static readonly DependencyProperty RegisterCommandProperty =
            DependencyProperty.Register(nameof(RegisterCommand), typeof(ICommand), typeof(RegisterView), new PropertyMetadata(null));

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterCommand != null)
            {
                var newUserDto = new RegisterUserDto()
                {
                    Email = Email.Text,
                    Password = pbPassword.Password,
                    PasswordConfirm = pbPasswordConfirm.Password,
                    Name = Name.Text    
                };

                RegisterCommand.Execute(newUserDto);
            }
        }

    }
}
