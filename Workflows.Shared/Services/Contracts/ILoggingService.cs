using System;
using System.Collections.Generic;
using System.Text;

namespace Workflows.Shared.Services.Contracts
{
    public interface ILoggingService
    {
        public List<string> LogHistory { get;  }

        public void Log(string message);
        public void Log(string message,  params object[] args);

    }
}
