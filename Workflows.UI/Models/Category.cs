using System;
using System.Collections.Generic;
using System.Text;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;

namespace Workflows.UI.Models
{
    public class Category
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public PluginCategoriesType PluginCategory {  get; set; }

        public List<PluginModule> AvailableModules { get; set; }
    }
}
