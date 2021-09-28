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

        public List<WorkflowItemViewModel> Bottoms { get; set; }

        public WorkflowItemViewModel Input { get; set; }

        public WorkflowItemViewModel Top { get; set; }

        public WorkflowItemViewModel() { 
            Bottoms = new List<WorkflowItemViewModel>();
            Outputs = new List<WorkflowItemViewModel>();
        }

        public WorkflowItemViewModel(WorkflowItemViewModel input, Guid id)
        {
            Bottoms = new List<WorkflowItemViewModel>();
            Outputs = new List<WorkflowItemViewModel>();
            Input = input;
            ID = id;
            Input = input;

            if (input == null)
                return;

            if (input.Outputs.Any(x => x.ID == id))
                return;

            input.Outputs.Add(this);
        }

        public WorkflowItemViewModel(WorkflowItemViewModel top, WorkflowItemViewModel input, Guid id)
        {
            Bottoms = new List<WorkflowItemViewModel>();
            Outputs = new List<WorkflowItemViewModel>();
            Top = top;
            Input = input;
            ID = id;

            if(top != null && !top.Bottoms.Any(x => x.ID == id))
            {
                top.Bottoms.Add(this);
            }

            if (input != null && !input.Outputs.Any(x => x.ID == id))
            {
                input.Outputs.Add(this);
            }
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
