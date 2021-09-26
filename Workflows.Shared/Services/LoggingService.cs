using System;
using System.Collections.Generic;
using System.Text;
using Workflows.Shared.Services.Contracts;

namespace Workflows.Shared.Services
{
    public class LoggingService : ILoggingService
    {
        public List<string> LogHistory { get; private set; } = new List<string>();


        public void Log(string message)
        {
            Log(message, null);
        }
        public void Log(string formatString, params object[] args)
        {
            string msg = args == null ? formatString : String.Format(formatString, args);
            string logEntry = $"{DateTime.Now}: {msg}{System.Environment.NewLine}" ;
            LogHistory.Add(logEntry);
        }
    }
}
