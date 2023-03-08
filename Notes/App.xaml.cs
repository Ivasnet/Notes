using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.ViewModel;
using System.Windows;
using DataBase;
using DataBase.Abstractions;

namespace Notes
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
            .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<MainViewModel>();

            services.AddDbServices();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbase = _host.Services.GetRequiredService<INotesDataAccess>();

            dbase.CreateIfNotExistsDataBase();

            var viewModel = _host.Services.GetRequiredService<MainViewModel>();
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();


            mainWindow.DataContext = viewModel;

            mainWindow.Show();

            base.OnStartup(e);
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
