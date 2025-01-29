using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public class OtherStrategy : IStrategy
    {
        public List<Move> GenerateMoves(MoveRequest request)
        {
            var moves = new List<Move>();
            foreach (var unit in request.Units)
            {
                if (unit != null)
                    moves.Add(new Move(MoveType.Walk, unit.Id, new Coordinate(unit.Location.Q, unit.Location.R + 1)));
            }
            return moves;
        }
    }
}
