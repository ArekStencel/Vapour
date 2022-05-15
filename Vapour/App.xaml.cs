using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Vapour.Database;
using Vapour.Model;
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

            var window = new MainWindow();
            window.DataContext = new MainWindowViewModel();
            window.Show();
            base.OnStartup(e);
        }
    }
}
