using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class BaseInputComponent : ObservableObject
    {
        public string Label {  get; set; }

        public InputTypes InputType { get; set; }

        public object DefaultValue { get; set; }

        public object Value { get; set; }

        public string Filter { get; set; }

    }
}
