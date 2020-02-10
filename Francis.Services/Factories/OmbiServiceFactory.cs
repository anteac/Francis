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


        public IOmbiService Create(bool fromBot = true)
        {
            var client = new HttpClient();
            var service = RestService.For<IOmbiService>(client);

            try
            {
                client.BaseAddress = new Uri(_options.CurrentValue.BaseUrl);
                client.DefaultRequestHeaders.Add("ApiKey", _options.CurrentValue.ApiKey);

                if (!fromBot)
                {
                    return service;
                }

                var botUser = _context.BotUsers.Find(_capture.Data.Chat.Id);
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
                _logger.LogError(ex, $"An error occured while creating {nameof(IOmbiService)}");
            }

            return service;
        }
    }
}
