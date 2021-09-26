using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;
using Workflows.Shared.Services.Contracts;
//using Microsoft.CSharp.M

namespace Workflows.UI.Core.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        private readonly IPluginService _pluginService = Ioc.Default.GetRequiredService<IPluginService>();

        private readonly ILoggingService _logger = Ioc.Default.GetRequiredService<ILoggingService>(); 

        public IReadOnlyList<PluginCategory> Categories
        {
            get
            {
                return _pluginService.Categories;
            }
        }

        public IReadOnlyList<string> LogHistory
        {
            get
            {
                return _logger.LogHistory;
            }
        }
    }
}
