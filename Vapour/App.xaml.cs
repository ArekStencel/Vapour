using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Vapour.Database;
using Vapour.Model;
using Vapour.Services;
using Vapour.State;
using Vapour.ViewModel;
using Vapour.ViewModel.Factories;

namespace Vapour
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceProvider = CreateServiceProvider();

            var dataContext = serviceProvider.GetRequiredService<VapourDatabaseEntities>();
            var seeder = new DataSeeder(dataContext);
            seeder.Seed();

            var window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LibraryViewModel>, LibraryViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CommunityViewModel>, CommunityViewModelFactory>();
            services.AddSingleton<IViewModelFactory<StoreViewModel>, StoreViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LoginViewModel>, LoginViewModelFactory>();
            services.AddSingleton<IViewModelFactory<RegisterViewModel>, RegisterViewModelFactory>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<VapourDatabaseEntities>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }

    }
}
