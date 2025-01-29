using BadgerClan.Bot.Strategies;
using BadgerClan.Shared;
using System.IO;

namespace BadgerClan.Bot.Services
{
    public class StrategyService
    {
        public IStrategy Strategy;
        public StrategyService()
        {
            Strategy = new MyStrategy();
        }

        public void ChangeStrategy(StrategyType stratType)
        {
            Strategy = stratType switch
            {
                StrategyType.MyStrategy => new MyStrategy(),
                StrategyType.OtherStrategy => new OtherStrategy(),
                _ => new MyStrategy()
            };
        }
    }
}
