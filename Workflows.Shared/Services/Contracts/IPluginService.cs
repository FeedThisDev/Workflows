using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;

namespace Workflows.Shared.Contracts
{
    public interface IPluginService
    {
        public List<IPluginLibrary> PluginLibraries { get; }
        public List<PluginCategory> Categories { get; }

    }
}
