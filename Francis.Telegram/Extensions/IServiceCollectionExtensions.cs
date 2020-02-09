using Francis.Database.Options;
using Francis.Models;
using Francis.Models.Options;
using Francis.Telegram.Answers;
using Francis.Telegram.Answers.CallbackAnswers;
using Francis.Telegram.Answers.MessageAnswers;
using Francis.Telegram.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Francis.Extensions
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
            services.ConfigureFromDatabase<TelegramOptions>();

            services.AddSingleton<ITelegramClient, TelegramClient>();

            services.AddScoped(typeof(DataCapture<>));

            services.AddScoped<MessageAnswer, StartAnswer>();
            services.AddScoped<MessageAnswer, StartAnonymousAnswer>();
            services.AddScoped<MessageAnswer, DiskUsageAnswer>();
            services.AddScoped<MessageAnswer, HelpAnswer>();
            services.AddScoped<MessageAnswer, SelectRequestTypeAnswer>();

            services.AddScoped<CallbackAnswer, NextMovieAnswer>();
            services.AddScoped<CallbackAnswer, NextTvShowAnswer>();
            services.AddScoped<CallbackAnswer, SelectTvSeasonsAnswer>();
            services.AddScoped<CallbackAnswer, RequestMovieAnswer>();
            services.AddScoped<CallbackAnswer, ApproveMovieAnswer>();
            services.AddScoped<CallbackAnswer, DenyMovieAnswer>();
            services.AddScoped<CallbackAnswer, RequestTvAnswer>();
            services.AddScoped<CallbackAnswer, ApproveTvAnswer>();
            services.AddScoped<CallbackAnswer, DenyTvAnswer>();

            return services;
        }
    }
}
