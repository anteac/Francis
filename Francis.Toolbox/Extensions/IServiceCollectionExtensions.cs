using Francis.Toolbox.JsonConverters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Francis.Toolbox.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureJsonDefaults(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings().ConfigureJsonDefaults();

            return services;
        }

        public static JsonSerializerSettings ConfigureJsonDefaults(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Converters.Add(new SpacedEnumConverter());
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            return settings;
        }
    }
}
