using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Toolbox.Logging
{
    public class LogMessage
    {
        public string Message { get; set; }
        public LogEventLevel Level { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public RuntimeError Exception { get; set; }


        public static explicit operator LogMessage(LogEvent logEvent) => new LogMessage
        {
            Message = logEvent.MessageTemplate.Render(logEvent.Properties),
            Level = logEvent.Level,
            Timestamp = logEvent.Timestamp,
            Properties = logEvent.Properties.ToDictionary(x => x.Key, x => x.Value.ToString()),
            Exception = (RuntimeError)logEvent.Exception,
        };
    }
}
