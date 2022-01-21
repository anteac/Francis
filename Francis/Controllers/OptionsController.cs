using Francis.Database.Options;
using Francis.Models.Ombi;
using Francis.Models.Options;
using Francis.Models.Telegram;
using Francis.Services.Clients;
using Francis.Services.Factories;
using Francis.Telegram.Client;
using Francis.Telegram.Extensions;
using Francis.Toolbox.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

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


        public OptionsController(
            ITelegramClient bot,
            OmbiServiceFactory ombi,
            DatabaseOptionsUpdater updater,
            IOptionsSnapshot<TelegramOptions> telegramOptions,
            IOptionsSnapshot<OmbiOptions> ombiOptions
        )
        {
            _bot = bot;
            _ombi = ombi;
            _updater = updater;
            _telegramOptions = telegramOptions;
            _ombiOptions = ombiOptions;
        }


        [HttpGet("telegram")]
        public TelegramOptions GetTelegramOptions()
        {
            return _telegramOptions.Value;
        }

        [HttpPost("telegram")]
        public async Task<AboutTelegramBot> UpdateTelegramOptions(TelegramOptions options)
        {
            //TODO: Don't check unchanged settings
            //TODO: Answer more descriptive messages
            //TODO: Use Uri instead of string concatenation
            _updater.Save(options);
            await _bot.Initialize();
            await _bot.SendMessage(options.AdminChat, "Hello Francis administrator, if you received this message, this means your configuration is valid.");
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
            return await _ombi.CreateGlobal(options).About();
        }
    }
}
