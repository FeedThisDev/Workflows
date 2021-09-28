using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Workflows.Shared.Models;

namespace Workflows.Shared.ViewModels
{
    public class WorkflowItemViewModel : ObservableObject
    {
        public ICommand DeleteMeCmd { get; }
        public Guid ID { get; private set; }
        public List<WorkflowItemViewModel> Outputs { get; set; }

        public List<WorkflowItemViewModel> Bottoms { get; set; }

        public WorkflowItemViewModel Input { get; set; }

        public WorkflowItemViewModel Top { get; set; }

        public WorkflowItemViewModel() { 
            Bottoms = new List<WorkflowItemViewModel>();
            Outputs = new List<WorkflowItemViewModel>();
            DeleteMeCmd = new RelayCommand(DeleteMe);
        }

        public WorkflowItemViewModel(WorkflowItemViewModel input, Guid id)
        {
            DeleteMeCmd = new RelayCommand(DeleteMe);
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

        public bool IsDeletable
        {
            get
            {
                return SelectedModule != null && Outputs.Count > 0;
            }
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
                OnPropertyChanged("IsDeletable");
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

        public void DeleteMe()
        {
            throw new NotImplementedException();
        }
    }
}
