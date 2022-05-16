using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Vapour.Database;
using Vapour.Model;
using Vapour.Services;
using Vapour.State;
using Vapour.ViewModel;

namespace Vapour
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dataContext = new VapourDatabaseEntities();
            var seeder = new DataSeeder(dataContext);
            seeder.Seed();

            var serviceProvider = CreateServiceProvider();
            var authentication = serviceProvider.GetRequiredService<IAuthenticationService>();
            authentication.Register("dobrypiesio@interia.pl", "piesio", "dobrypiesio", "dobrypiesio");



            var window = new MainWindow();
            window.DataContext = new MainWindowViewModel();
            window.Show();
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddScoped<INavigator, Navigator>();

            return services.BuildServiceProvider();
        }

    }
}
