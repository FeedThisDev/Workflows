using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class LogChangedMessage : ValueChangedMessage<string>
    {
        public LogChangedMessage(string value) : base(value)
        {
        }
    }
}
