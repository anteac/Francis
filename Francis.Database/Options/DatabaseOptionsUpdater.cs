using Francis.Database.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Database.Options
{
    public class DatabaseOptionsUpdater
    {
        private readonly IServiceProvider _provider;
        private readonly IConfigurationRoot _configuration;
        private readonly IEnumerable<DatabaseOptionsDefinition> _definitions;


        public DatabaseOptionsUpdater(
            IServiceProvider provider,
            IConfigurationRoot configuration,
            IEnumerable<DatabaseOptionsDefinition> definitions
        )
        {
            _configuration = configuration;
            _provider = provider;
            _definitions = definitions;
        }


        public void Save<TOptions>(TOptions options, string name = null)
        {
            var definition = _definitions.FirstOrDefault(x => x.Type == typeof(TOptions));
            if (definition == null)
            {
                throw new InvalidOperationException("Only options that have been registered can be saved");
            }

            var context = _provider.GetRequiredService<BotDbContext>();

            foreach (var property in definition.Type.GetProperties())
            {
                var key = $"{definition.Name}:{property.Name}";
                var value = JsonConvert.SerializeObject(property.GetValue(options));

                var entry = context.Options.FirstOrDefault(x => x.Id == key);
                if (entry == null)
                {
                    context.Options.Add(new OptionValue { Id = key, Value = value });
                }
                else
                {
                    entry.Value = value;
                }
            }

            context.SaveChanges();

            _configuration.Reload();
        }
    }
}
