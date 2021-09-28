using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workflows.Shared.Models;

namespace Workflows.Shared.ViewModels
{
    public class WorkflowItemViewModel : ObservableObject
    {
        public Guid ID { get; private set; }
        public List<WorkflowItemViewModel> Outputs { get; set; }

        public WorkflowItemViewModel Bottom { get; set; }

        public WorkflowItemViewModel Input { get; set; }

        public WorkflowItemViewModel() { }

        public WorkflowItemViewModel(WorkflowItemViewModel parent, Guid id)
        {
            Outputs = new List<WorkflowItemViewModel>();
            Input = parent;
            ID = id;
            Input = parent;

            if (parent == null)
                return;

            if (parent.Outputs.Any(x => x.ID == id))
                return;

            parent.Outputs.Add(this);
        }


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
