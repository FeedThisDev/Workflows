
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Workflows.Shared.Models;
using Workflows.Shared.ViewModels;

namespace Workflows.UI
{

    /// <summary>
    /// Interaction logic for WorkflowItemControl.xaml
    /// </summary>
    public partial class WorkflowItemControl : UserControl
    {
        public WorkflowItemControl()
        {
            InitializeComponent();
        }

        private void DropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("pluginData") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropZone_Drop(object dropZone, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("pluginData"))
            {
                var border = dropZone as Border;
                var vm = border.DataContext as WorkflowItemViewModel;

                var module = e.Data.GetData("pluginData") as PluginModule;

                vm.SelectedModule = module;

                vm.Outputs.Add(new WorkflowItemViewModel(vm, new System.Guid()));

                WeakReferenceMessenger.Default.Send(new AddedWorkflowItemMessage(new System.Guid()));
            }
        }
    }
}
