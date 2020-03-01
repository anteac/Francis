using Francis.Database;
using Francis.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly BotDbContext _context;


        public RequestsController(BotDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<RequestProgression> GetRequests()
        {
            return _context.RequestProgressions
                .OrderByDescending(x => x.CreationDate)
                .ToList();
        }
    }
}
