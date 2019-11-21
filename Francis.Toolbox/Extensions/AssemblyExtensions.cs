using System.IO;
using System.Reflection;

namespace Francis.Toolbox.Extensions
{
    public static class AssemblyExtensions
    {
        public static Stream GetResource(this Assembly assembly, string path)
        {
            return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}");
        }
    }
}
