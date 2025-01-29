using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public class MyStrategy : IStrategy
    {
        public List<Move> GenerateMoves(MoveRequest request)
        {
            var moves = new List<Move>();
            foreach(var unit in request.Units)
            {
                if (unit != null)
                    moves.Add(new Move(MoveType.Walk, unit.Id, new Coordinate(unit.Location.Q-1, unit.Location.R)));
            }
            return moves;
        }

        public List<UnitDto> GetClosestEnemies()
        {
            throw new NotImplementedException();
        }
    }
}
