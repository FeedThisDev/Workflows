using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Workflows.Shared.Models;
using Workflows.Shared.Services.Contracts;
using Workflows.Shared.ViewModels;

namespace Workflows.UI
{
    public class WorkflowGridGenerator : IWorkflowGridGenerator
    {
        public Grid Generate(WorkflowItemViewModel viewModel)
        {
            Grid DynamicGrid = new Grid();

            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Center;

            DynamicGrid.ShowGridLines = true;

            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            DynamicGrid.Margin = new Thickness(15, 15, 15, 15);

            var userControl = new WorkflowItemControl();
            userControl.DataContext = viewModel;



            ColumnDefinition leftColumn = new ColumnDefinition();
            leftColumn.MinWidth = 100;
            DynamicGrid.ColumnDefinitions.Add(leftColumn);

            Grid.SetColumn(userControl, 0);
            Grid.SetRow(userControl, 0);
            DynamicGrid.Children.Add(userControl);

            if (viewModel.SelectedModule == null)
            {
                return DynamicGrid;
            }

            ColumnDefinition rightColumn = new ColumnDefinition();
            rightColumn.MinWidth = 100;
            DynamicGrid.ColumnDefinitions.Add(rightColumn);

            var columnContent = Generate(viewModel.Outputs[0]);

            Grid.SetColumn(columnContent, 1);
            Grid.SetRow(columnContent, 0);
            DynamicGrid.Children.Add(columnContent);

            return DynamicGrid;
        }
    }
}
