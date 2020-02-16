using Francis.Models.Ombi;
using Francis.Models.Options;
using Francis.Models.Telegram;
using Francis.Toolbox.Logging;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using Serilog.Events;
using System;
using System.IO;

namespace Francis.Typings
{
    public static class TypingsGenerator
    {
        private static readonly Type[] _exportedClasses = new[]
        {
            typeof(TelegramOptions),
            typeof(OmbiOptions),

            typeof(AboutTelegramBot),
            typeof(AboutOmbi),

            typeof(LogMessage),
            typeof(RuntimeError),
        };

        private static readonly Type[] _exportedEnums = new[]
        {
            typeof(LogEventLevel),
        };

        public static void Generate(ConfigurationBuilder builder)
        {
            if (Directory.Exists(builder.Context.TargetDirectory))
            {
                Directory.Delete(builder.Context.TargetDirectory, recursive: true);
            }

            builder.Global(configuration =>
            {
                configuration.CamelCaseForProperties();
                configuration.UseModules();
            });

            builder.Substitute(typeof(DateTimeOffset), new RtSimpleTypeName("Date"));

            builder.ExportAsClasses(_exportedClasses, configuration =>
            {
                configuration.DontIncludeToNamespace();
                configuration.WithPublicProperties(property => property.ForceNullable());
            });

            builder.ExportAsEnums(_exportedEnums, configuration =>
            {
                configuration.DontIncludeToNamespace();
                configuration.UseString();
            });
        }
    }
}
