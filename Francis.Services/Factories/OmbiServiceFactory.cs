using Castle.Core.Internal;
using Francis.Database;
using Francis.Models;
using Francis.Models.Options;
using Francis.Services.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Net.Http;
using Telegram.Bot.Types;

namespace Francis.Services.Factories
{
    public class OmbiServiceFactory
    {
        private readonly BotDbContext _context;
        private readonly DataCapture<Message> _capture;
        private readonly ILogger<OmbiServiceFactory> _logger;
        private readonly IOptionsSnapshot<OmbiOptions> _options;


        public OmbiServiceFactory(
            BotDbContext context,
            DataCapture<Message> capture,
            ILogger<OmbiServiceFactory> logger,
            IOptionsSnapshot<OmbiOptions> options)
        {
            _context = context;
            _capture = capture;
            _logger = logger;
            _options = options;
        }


        public IOmbiService CreateGlobal()
        {
            var client = new HttpClient();
            var service = RestService.For<IOmbiService>(client);

            var options = _options.Value;

            if (options.BaseUrl.IsNullOrEmpty() || options.ApiKey.IsNullOrEmpty())
            {
                return service;
            }

            client.BaseAddress = new Uri(_options.Value.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", _options.Value.ApiKey);

            return service;
        }

        public IBotOmbiService CreateForBot()
        {
            var client = new HttpClient();
            var service = RestService.For<IBotOmbiService>(client);

            var options = _options.Value;
            var botUser = _context.BotUsers.Find(_capture.Data.Chat.Id);

            if (botUser == null || options.BaseUrl.IsNullOrEmpty() || options.ApiKey.IsNullOrEmpty())
            {
                return service;
            }

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", options.ApiKey);

            if (botUser.OmbiId.IsNullOrEmpty())
            {
                return service;
            }

            try
            {
                var ombiUser = service.GetUser(botUser.OmbiId).Result;
                client.DefaultRequestHeaders.Add("UserName", ombiUser.UserName);

                if (botUser.UserName != ombiUser.UserName)
                {
                    botUser.UserName = ombiUser.UserName;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while trying to contact Ombi");
            }

            return service;
        }
    }
}
