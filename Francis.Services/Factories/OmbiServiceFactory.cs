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
        private readonly IOptionsMonitor<OmbiOptions> _options;


        public OmbiServiceFactory(
            BotDbContext context,
            DataCapture<Message> capture,
            ILogger<OmbiServiceFactory> logger,
            IOptionsMonitor<OmbiOptions> options)
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

            client.BaseAddress = new Uri(_options.CurrentValue.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", _options.CurrentValue.ApiKey);

            return service;
        }

        public IBotOmbiService CreateForBot()
        {
            var options = _options.CurrentValue;
            var botUser = _context.BotUsers.Find(_capture.Data.Chat.Id);

            if (botUser == null || options.BaseUrl == null || options.ApiKey == null)
            {
                return null;
            }

            var client = new HttpClient();
            var service = RestService.For<IBotOmbiService>(client);

            client.BaseAddress = new Uri(_options.CurrentValue.BaseUrl);
            client.DefaultRequestHeaders.Add("ApiKey", _options.CurrentValue.ApiKey);

            if (botUser.OmbiId == null)
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
