
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Workflows.Shared.Models;
using Workflows.UI.Core.ViewModels;

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

        private void DropZone_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("pluginData"))
            {
                var module = e.Data.GetData("pluginData") as PluginModule;

                var vm = new WorkflowItemViewModel()
                {
                    SelectedModule = module,
                };
                var border = sender as Border;
                border.DataContext = vm;
            }
        }
    }
}
