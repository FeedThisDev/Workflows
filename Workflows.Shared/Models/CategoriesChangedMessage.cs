using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Models
{
    public class CategoriesChangedMessage : ValueChangedMessage<PluginCategoriesType>
    {
        public CategoriesChangedMessage(PluginCategoriesType value) : base(value)
        {
        }
    }
}
