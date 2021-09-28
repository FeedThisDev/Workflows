using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class PluginModule : ObservableObject
    {
        public PluginModule() { }
        public string Name { get;  set; }

        public string ImagePath { get;  set; }

        public List<BaseInputComponent> ConfigValues { get;  set; }

        public PluginModule(string name, string icon, List<BaseInputComponent> configValues)
        {
            Name = name;
            ImagePath = icon;    
            ConfigValues = configValues;    
        }

    }
}
