using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;

namespace Workflows.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;

        public MainWindow()
        {
            //WorkflowItemArea startArea = new WorkflowItemArea();



            InitializeComponent();

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
