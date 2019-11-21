using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Francis.Database.Options
{
    public static class DatabaseOptionsExtensions
    {
        public static IServiceCollection AddDatabaseOptions(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddTransient(typeof(OptionsFactory<>));
            services.AddTransient<DatabaseOptionsUpdater>();
            services.Replace(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(DatabaseOptionsFactory<>)));

            return services;
        }

        public static IServiceCollection ConfigureFromDatabase<TOptions>(this IServiceCollection services)
             where TOptions : class, new()
        {
            services.AddDatabaseOptions();

            services.AddSingleton<DatabaseOptionsDefinition, DatabaseOptionsDefinition<TOptions>>();

            return services;
        }
    }
}
