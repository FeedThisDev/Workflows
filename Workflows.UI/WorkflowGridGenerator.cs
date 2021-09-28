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

            //DynamicGrid.Margin = new Thickness(15, 15, 15, 15);


            RowDefinition topRow = new RowDefinition();
            DynamicGrid.RowDefinitions.Add(topRow);

            ColumnDefinition leftColumn = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(leftColumn);

            var userControl = new WorkflowItemControl();
            userControl.DataContext = viewModel;

            Grid.SetColumn(userControl, 0);
            Grid.SetRow(userControl, 0);
            DynamicGrid.Children.Add(userControl);

            if (viewModel.SelectedModule == null)
            {
                return DynamicGrid;
            }

            ColumnDefinition rightColumn = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(rightColumn);

            var columnContent = Generate(viewModel.Outputs[0]);

            Grid.SetColumn(columnContent, 1);
            Grid.SetRow(columnContent, 0); 
            DynamicGrid.Children.Add(columnContent);

            RowDefinition childRow = new RowDefinition();
            DynamicGrid.RowDefinitions.Add(childRow);

            var parallelUserControl = Generate(viewModel.Bottoms[0]);
            Grid.SetColumnSpan(parallelUserControl, 999);
            Grid.SetColumn(parallelUserControl, 0);
            Grid.SetRow(parallelUserControl, 1);
            DynamicGrid.Children.Add(parallelUserControl);

            return DynamicGrid;
        }


       

    }
}
