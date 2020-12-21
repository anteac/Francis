using Castle.Core.Internal;
using Francis.Database;
using Francis.Models;
using Francis.Models.Options;
using Francis.Services.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Linq;
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


        public IOmbiService CreateGlobal(OmbiOptions options = null)
        {
            var client = new HttpClient();
            var service = RestService.For<IOmbiService>(client);

            options = options ?? _options.Value;

            if (options.BaseUrl.IsNullOrEmpty() || options.ApiKey.IsNullOrEmpty())
            {
                return service;
            }

            client.BaseAddress = new Uri(_options.Value.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", _options.Value.ApiKey);

            return service;
        }

        public IBotOmbiService CreateForBot(OmbiOptions options = null)
        {
            var client = new HttpClient();
            var service = RestService.For<IBotOmbiService>(client);

            options = options ?? _options.Value;
            var botUser = _context.BotUsers.FirstOrDefault(x => x.TelegramId == _capture.Data.Chat.Id);

            if (botUser == null || options.BaseUrl.IsNullOrEmpty() || options.ApiKey.IsNullOrEmpty())
            {
                return service;
            }

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", options.ApiKey);
            client.DefaultRequestHeaders.Add("UserName", "Francis");

            return service;
        }
    }
}
