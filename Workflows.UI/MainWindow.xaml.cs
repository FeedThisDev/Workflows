using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;
using Workflows.Shared.Services.Contracts;
using Workflows.Shared.ViewModels;

namespace Workflows.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint; // for drag

        private readonly IWorkflowGridGenerator _gridGen = Ioc.Default.GetRequiredService<IWorkflowGridGenerator>();


        //public readonly Guid StartGuid = new Guid("9BB1E9B2-0D32-46A2-A85F-6BDA85897293");

        WorkflowItemViewModel _startArea; 

        public MainWindow()
        {
            InitializeComponent();
            _startArea = new WorkflowItemViewModel(null, new Guid("9BB1E9B2-0D32-46A2-A85F-6BDA85897293"));

            var gridGen = new WorkflowGridGenerator();
            var grid = gridGen.Generate(_startArea);
            WorkflowAreaBase.Children.Add(grid);

            WeakReferenceMessenger.Default.Register<AddedWorkflowItemMessage>(this, (r, m) =>
            {
                var grid = _gridGen.Generate(_startArea);
                WorkflowAreaBase.Children.Clear();
                WorkflowAreaBase.Children.Add(grid);

            });
        }

        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem == null)
                    return;
                // Find the data behind the ListViewItem
                var pluginModule = listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                if (!(pluginModule is PluginModule))
                    return;

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("pluginData", pluginModule);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }



    }
}
