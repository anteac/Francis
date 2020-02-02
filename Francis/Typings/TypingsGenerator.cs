using Francis.Models.Ombi;
using Francis.Models.Telegram;
using Francis.Models.Options;
using Reinforced.Typings.Fluent;
using System;
using System.IO;

namespace Francis.Typings
{
    public static class TypingsGenerator
    {
        private static readonly Type[] _exportedTypes = new[]
        {
            typeof(TelegramOptions),
            typeof(OmbiOptions),

            typeof(AboutTelegramBot),
            typeof(AboutOmbi),
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

            builder.ExportAsClasses(_exportedTypes, configuration =>
            {
                configuration.DontIncludeToNamespace();
                configuration.WithPublicProperties(property => property.ForceNullable());
            });
        }
    }
}
