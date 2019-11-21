using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Database.Options
{
    public class DatabaseOptionsFactory<TOptions> : IOptionsFactory<TOptions>
        where TOptions : class, new()
    {
        private readonly IServiceProvider _provider;
        private readonly IEnumerable<DatabaseOptionsDefinition> _definitions;
        private readonly OptionsFactory<TOptions> _rawOptions;


        public DatabaseOptionsFactory(
            IServiceProvider provider,
            IEnumerable<DatabaseOptionsDefinition> definitions,
            OptionsFactory<TOptions> rawOptions
        )
        {
            _provider = provider;
            _definitions = definitions;
            _rawOptions = rawOptions;
        }


        public TOptions Create(string name = null)
        {
            var options = _rawOptions.Create(name);

            if (!_definitions.Any(x => x.Type == typeof(TOptions)))
            {
                return options;
            }

            var context = _provider.GetRequiredService<BotDbContext>();

            var definition = _definitions.First(x => x.Type == typeof(TOptions));
            foreach (var property in definition.Type.GetProperties())
            {
                var entry = context.Options.Find($"{definition.Name}:{property.Name}");
                if (entry == null)
                {
                    continue;
                }

                var value = JsonConvert.DeserializeObject(entry.Value, property.PropertyType);
                property.SetValue(options, value);
            }

            return options;
        }
    }
}
