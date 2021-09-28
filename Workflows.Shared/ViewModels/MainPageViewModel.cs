using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;
using Workflows.Shared.Services.Contracts;

namespace Workflows.Shared.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
       

        private readonly IPluginService _pluginService = Ioc.Default.GetRequiredService<IPluginService>();

        private readonly ILoggingService _logger = Ioc.Default.GetRequiredService<ILoggingService>();

    

        public MainPageViewModel()
        {
            WeakReferenceMessenger.Default.Register<LogChangedMessage>(this, (r, m) =>
            {
                OnPropertyChanged("LogHistory");
            });
            WeakReferenceMessenger.Default.Register<CategoriesChangedMessage>(this, (r, m) =>
            {
                OnPropertyChanged("Categories");
                
            });

        

        }

        public bool PluginsVisible
        {
            get
            {
                return _selectedCategory != null && _selectedCategory.Modules.Count > 0;
            }
        }

        private PluginCategory _selectedCategory;
        public PluginCategory SelectedCategory 
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                SetProperty(ref _selectedCategory, value);
                OnPropertyChanged(nameof(PluginsVisible));
            }
        }

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
