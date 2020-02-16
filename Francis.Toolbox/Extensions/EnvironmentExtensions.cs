using System;

namespace Francis.Toolbox.Extensions
{
    public static class EnvironmentContext
    {
        public static bool InContainer()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
            bool.TryParse(env, out var result);
            return result;
        }
    }
}
