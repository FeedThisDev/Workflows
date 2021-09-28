using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Workflows.Shared.Models;

namespace Workflows.UI
{
    internal class WorkflowGridGenerator
    {
        internal Grid Generate(WorkflowItemArea startItem)
        {
            Grid DynamicGrid = new Grid();

            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Center;

            DynamicGrid.ShowGridLines = true;

            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            DynamicGrid.Children.Add(new WorkflowItemControl());

            if (startItem.SelectedModule == null)
            {
                return DynamicGrid;
            }
            ColumnDefinition outputColumn = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(outputColumn);

            var columnContent = Generate(startItem.Outputs[0]);

            Grid.SetColumn(columnContent, 1);
            Grid.SetRow(columnContent, 0);
            DynamicGrid.Children.Add(columnContent);

            return DynamicGrid;
        }
    }
}
