using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;
using System.IO;

namespace Francis.Toolbox.Logging
{
    public class NewtonsoftJsonFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            output.WriteLine(JsonConvert.SerializeObject((LogMessage)logEvent));
        }
    }
}
