using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows;
using Workflows.Shared.Contracts;
using Workflows.Shared.Services;
using Workflows.Shared.Services.Contracts;
using Workflows.UI.Services;

namespace Workflows.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {



        public App()
        {
            Ioc.Default.ConfigureServices(
                   new ServiceCollection()
                   .AddSingleton<IPluginService, PluginService>()
                   .AddSingleton<ILoggingService, LoggingService>()
                   .BuildServiceProvider());
        }
    }

    
}
