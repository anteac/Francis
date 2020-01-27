using Francis.Database;
using Francis.Telegram.Client;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Francis.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly BotDbContext _context;

        private readonly ITelegramClient _telegramClient;


        public AuthenticationController(BotDbContext context, ITelegramClient telegramClient)
        {
            _context = context;
            _telegramClient = telegramClient;
        }


        [HttpHead]
        public NoContentResult Ping() => NoContent();

        [HttpGet]
        public async Task<ActionResult> Auth(string clientId)
        {
            var user = _context.BotUsers.FirstOrDefault(x => x.ClientId == clientId);
            if (user == null)
            {
                return NotFound(null);
            }

            var bot = await _telegramClient.Client.GetMeAsync();

            var assembly = Assembly.GetExecutingAssembly();
            var templateName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith("authentication.html"));
            var templateStream = assembly.GetManifestResourceStream(templateName);

            using var reader = new StreamReader(templateStream);

            var templateString = reader.ReadToEnd()
                .Replace($"[Product]", bot.Username)
                .Replace($"[ClientId]", user.ClientId);

            return File(Encoding.UTF8.GetBytes(templateString), "text/html");
        }
    }
}
