using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class AddedWorkflowItemMessage : ValueChangedMessage<Guid>
    {
        public AddedWorkflowItemMessage(Guid value) : base(value)
        {
        }
    }
}
