using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Workflows.Shared.Models;
using Workflows.Shared.ViewModels;

namespace Workflows.Shared.Services.Contracts
{
    public interface IWorkflowGridGenerator
    {
        public Grid Generate(WorkflowItemViewModel startItem);
    }
}
