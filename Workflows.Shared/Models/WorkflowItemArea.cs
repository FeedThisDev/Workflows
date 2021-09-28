using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class WorkflowItemArea
    {
      
            public Guid ID { get; set; }
            public List<WorkflowItemArea> Outputs { get; set; }

            public WorkflowItemArea Bottom { get; set; }

            public PluginModule SelectedModule { get; set; }
    }
}
