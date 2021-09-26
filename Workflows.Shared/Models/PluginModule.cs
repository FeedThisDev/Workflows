using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class PluginModule
    {
        public string Name { get; private set; }

        public string IconFilePath { get; private set; }

        public List<BaseInputComponent> ConfigValues { get; private set; }

        public PluginModule(string name, string icon, List<BaseInputComponent> configValues)
        {
            Name = name;
            IconFilePath = icon;    
            ConfigValues = configValues;    
        }
    }
}
