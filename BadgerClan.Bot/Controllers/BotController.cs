using BadgerClan.Bot.Services;
using BadgerClan.Bot.Strategies;
using BadgerClan.Logic;
using BadgerClan.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BadgerClan.Bot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        ILogger<BotController> _logger;
        StrategyService _strategyService;
        public BotController(ILogger<BotController> logger, StrategyService strategyService)
        {
            _logger = logger;
            _strategyService = strategyService;
        }

        [HttpPost("/")]
        public MoveResponse GenerateMoveResponse(MoveRequest request)
        {
            _logger.LogInformation("Hit");
            var moves = new List<Move>();
            moves = _strategyService.Strategy.GenerateMoves(request);
            return new MoveResponse(moves);
        }

        [HttpPost("/changestrategy")]
        public void ChangeStrategy(StrategyDTO strat)
        {
            _strategyService.ChangeStrategy(strat.StratType);
        }
    }
}
