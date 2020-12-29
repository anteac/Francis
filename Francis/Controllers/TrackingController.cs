using Francis.Database;
using Francis.Database.Entities;
using Francis.Models;
using Francis.Telegram.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly BotDbContext _context;
        private readonly ITelegramClient _client;


        public TrackingController(BotDbContext context, ITelegramClient client)
        {
            _context = context;
            _client = client;
        }


        [HttpGet("users")]
        public async Task<List<EnhancedBotUser>> GetUsers()
        {
            var users = _context.BotUsers.AsEnumerable();
            var enhanced = users.Select(async x => await EnhancedBotUser.Create(x, _client));

            return (await Task.WhenAll(enhanced)).ToList();
                
        }

        [HttpGet("requests")]
        public List<RequestProgression> GetRequests()
        {
            return _context.RequestProgressions
                .OrderByDescending(x => x.CreationDate)
                .ToList();
        }
    }
}
