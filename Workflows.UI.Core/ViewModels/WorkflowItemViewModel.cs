using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using Workflows.Shared.Models;

namespace Workflows.UI.Core.ViewModels
{
    public class WorkflowItemViewModel : ObservableObject
    {
        public WorkflowItemViewModel()
        {
            Outputs = new List<WorkflowItemViewModel>();
        }

        public WorkflowItemViewModel Input { get; set; }
        public List<WorkflowItemViewModel> Outputs { get; set; }

        private PluginModule _selectedModule;
        public PluginModule SelectedModule
        {
            get
            {
                return _selectedModule;
            }
            set
            {
                SetProperty(ref _selectedModule, value);

                OnPropertyChanged("Label");
                OnPropertyChanged("ImagePath");
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public string Label
        {
            get
            {
                if (SelectedModule == null)
                    return "Drag mod here";
                return SelectedModule.Name;
            }
        }

        public string ImagePath
        {
            get
            {
                if (SelectedModule == null)
                    return "/Assets/placeholder.png";
                return SelectedModule.ImagePath;
            }
        }
    }
}
