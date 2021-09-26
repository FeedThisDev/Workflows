using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workflows.Shared.Models;

namespace Workflows.Shared.Contracts
{
    public interface IPluginLibrary
    {
        public string LibraryName { get; }

        public PluginCategoriesType Category { get; }

        public List<PluginModule> PluginModules { get; }

    }
}
