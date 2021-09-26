using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Workflows.Shared.Models
{
    public class PluginCategory : ObservableObject
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public PluginCategoriesType CategoriesType { get; set; }

        private ObservableCollection<PluginModule> _modules = new ObservableCollection<PluginModule>();
        public ObservableCollection<PluginModule> Modules
        {
            get {  return _modules; }
            set { 
                SetProperty(ref _modules, value);
                OnPropertyChanged("IsEnabled");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _modules.Count > 0;   
            }
        }


    }
}
