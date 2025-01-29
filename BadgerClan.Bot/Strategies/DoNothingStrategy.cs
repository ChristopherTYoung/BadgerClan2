using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public class DoNothingStrategy : IStrategy
    {
        public List<Move> GenerateMoves(MoveRequest request)
        {
            return new List<Move>();
        }

        public List<UnitDto> GetClosestEnemies()
        {
            throw new NotImplementedException();
        }
    }
}
