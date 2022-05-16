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
            authentication.Register("piesio@test.pl", "piesio", "dobrypiesio", "dobrypiesio");
            // var loggedUser = authentication.Login("dobrypiesio@interia.pl", "dobrypiesio");

            var window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            // todo add passwordhasher
            // services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddScoped<MainWindowViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }

    }
}
