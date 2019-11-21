using Francis.Services.Clients;
using Francis.Telegram.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly ITelegramClient _bot;
        private readonly IOmbiService _ombi;
        private readonly ILogger<MonitoringController> _logger;


        public MonitoringController(ITelegramClient bot, IOmbiService ombi, ILogger<MonitoringController> logger)
        {
            _bot = bot;
            _ombi = ombi;
            _logger = logger;
        }


        [HttpGet("telegram/status")]
        public bool GetTelegramStatus()
        {
            return _bot.Running;
        }

        [HttpGet("ombi/status")]
        public async Task GetOmbiStatus()
        {
            await _ombi.About();
        }

        [HttpGet("logs")]
        public string[] GetLogFiles()
        {
            return Directory.GetFiles("logs")
                .Select(x => new FileInfo(x))
                .OrderByDescending(x => x.LastWriteTimeUtc)
                .Select(x => x.Name)
                .ToArray();
        }

        [HttpGet("logs/{file}")]
        public FileStreamResult GetLogFiles(string file)
        {
            var path = Path.Combine("logs", file);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return File(stream, "text/plain; charset=UTF-8");
        }
    }
}
