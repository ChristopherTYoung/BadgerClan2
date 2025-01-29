using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public class MyStrategy : IStrategy
    {
        public List<Move> GenerateMoves(MoveRequest request)
        {
            var moves = new List<Move>();
            var Myteam = request.Units.Where(u => u.Team == request.YourTeamId).ToList();
            Pattern(Myteam);
            return moves;
        }

        public List<UnitDto> GetClosestEnemies()
        {
            throw new NotImplementedException();
        }

        public void Pattern(List<UnitDto> units)
        {
            units.ForEach(u => u.Location.MoveSouthEast(1));
            units.ForEach(u => u.Location.MoveSouthWest(1));
            units.ForEach(u => u.Location.MoveWest(1));
            units.ForEach(u => u.Location.MoveNorthWest(1));
            units.ForEach(u => u.Location.MoveNorthEast(1));
            units.ForEach(u => u.Location.MoveEast(1));
        }
    }
}
