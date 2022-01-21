using Francis.Database.Options;
using Francis.Models.Options;
using Francis.Services.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Francis.Services.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OmbiOptions>(configuration.GetSection("Ombi"));
            services.ConfigureFromDatabase<OmbiOptions>();

            services.AddScoped<OmbiServiceFactory>();
            services.AddScoped(provider => provider.GetRequiredService<OmbiServiceFactory>().CreateGlobal());
            services.AddScoped(provider => provider.GetRequiredService<OmbiServiceFactory>().CreateForBot());

            return services;
        }
    }
}
