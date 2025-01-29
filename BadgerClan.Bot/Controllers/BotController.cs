using BadgerClan.Bot.Bots;
using BadgerClan.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BadgerClan.Bot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        ILogger<BotController> _logger;
        IStrategy strategy;
        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
            strategy = new MyStrategy();
        }
        [HttpPost("/")]
        public MoveResponse GenerateMoveResponse(MoveRequest request)
        {
            var moves = new List<Move>();
            moves = strategy.GenerateMoves(request);
            return new MoveResponse(moves);
        }
    }
}
