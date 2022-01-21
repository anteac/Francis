using Francis.Database.Options;
using Francis.Models;
using Francis.Models.Options;
using Francis.Telegram.Answers;
using Francis.Telegram.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Francis.Telegram.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
            services.ConfigureFromDatabase<TelegramOptions>();

            services.AddSingleton<ITelegramClient, TelegramClient>();

            services.AddScoped(typeof(DataCapture<>));
            services.AddScoped(typeof(AnswerContext<>));
            services.AddScoped<AnswerContext>();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(
                x => !x.IsAbstract && typeof(ITelegramAnswer).IsAssignableFrom(x)
            );

            foreach (var type in types)
            {
                services.AddScoped(typeof(ITelegramAnswer), type);
            }

            return services;
        }
    }
}
