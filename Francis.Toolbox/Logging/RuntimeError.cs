using System;

namespace Francis.Toolbox.Logging
{
    public class RuntimeError
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }


        public static explicit operator RuntimeError(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            return new RuntimeError
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
            };
        }
    }
}
