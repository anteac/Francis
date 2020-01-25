using Francis.Database.Options;
using Francis.Models.Ombi;
using Francis.Models.Telegram;
using Francis.Options;
using Francis.Services.Factories;
using Francis.Telegram.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Francis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly ITelegramClient _bot;
        private readonly OmbiServiceFactory _ombi;
        private readonly DatabaseOptionsUpdater _updater;
        private readonly IOptionsSnapshot<TelegramOptions> _telegramOptions;
        private readonly IOptionsSnapshot<OmbiOptions> _ombiOptions;
        private readonly ILogger<OptionsController> _logger;


        public OptionsController(
            ITelegramClient bot,
            OmbiServiceFactory ombi,
            DatabaseOptionsUpdater updater,
            IOptionsSnapshot<TelegramOptions> telegramOptions,
            IOptionsSnapshot<OmbiOptions> ombiOptions,
            ILogger<OptionsController> logger
        )
        {
            _bot = bot;
            _ombi = ombi;
            _updater = updater;
            _telegramOptions = telegramOptions;
            _ombiOptions = ombiOptions;
            _logger = logger;
        }


        [HttpGet("telegram")]
        public TelegramOptions GetTelegramOptions()
        {
            return _telegramOptions.Value;
        }

        [HttpPost("telegram")]
        public async Task<AboutTelegramBot> UpdateTelegramOptions(TelegramOptions options)
        {
            _updater.Save(options);
            _bot.Initialize();
            await _bot.Client.SendTextMessageAsync(options.AdminChat, "Hello Francis' administrator, if you received this message, this means your configuration is valid.");
            if (!Directory.Exists(options.MediaLocation)) throw new ArgumentException("Invalid media location");
            return await _bot.Client.GetMeAsync();
        }

        [HttpGet("ombi")]
        public OmbiOptions GetOmbiOptions()
        {
            return _ombiOptions.Value;
        }

        [HttpPost("ombi")]
        public async Task<AboutOmbi> UpdateOmbiOptions(OmbiOptions options)
        {
            _updater.Save(options);
            return await _ombi.Create().About();
        }
    }
}
