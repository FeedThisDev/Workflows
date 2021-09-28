using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class AddedWorkflowItem : ValueChangedMessage<Guid>
    {
        public AddedWorkflowItem(Guid value) : base(value)
        {
        }
    }
}
