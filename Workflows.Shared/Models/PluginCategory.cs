using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class PluginCategory
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public PluginCategoriesType CategoriesType { get; set; }

        public List<PluginModule> Modules { get; set; } = new List<PluginModule>();

        public bool IsEnabled
        {
            get
            {
                return Modules.Count > 0;   
            }
        }


    }
}
