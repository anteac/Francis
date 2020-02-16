using Francis.Database;
using Francis.Services.Extensions;
using Francis.Telegram.Client;
using Francis.Telegram.Extensions;
using Francis.Toolbox.Extensions;
using Francis.Toolbox.JsonConverters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Francis
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.UseCamelCasing(true);
                x.SerializerSettings.Converters.Add(new SpacedEnumConverter());
            });

            services.AddDbContext<BotDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(_configuration.GetConnectionString("Primary"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSingleton(x => x.GetRequiredService<IConfiguration>() as IConfigurationRoot);

            services.ConfigureJsonDefaults();
            services.AddApiServices(_configuration);
            services.AddTelegramService(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITelegramClient client, ILogger<Startup> logger)
        {
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            try
            {
                client.Initialize();
                logger.LogInformation("Telegram client started.");
            }
            catch
            {
                logger.LogInformation("Telegram client could not start.");
            }
        }
    }
}
