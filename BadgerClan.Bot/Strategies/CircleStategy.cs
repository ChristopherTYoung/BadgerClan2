using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public class CircleStategy : IStrategy
    {
        private IEnumerable<UnitDto> ClosestEnemies { get; set; }
        private IEnumerable<UnitDto> MyTeam {  get; set; }
        public List<Move> GenerateMoves(MoveRequest request)
        {
            MyTeam = request.Units.Where(u => u.Team == request.YourTeamId);
            if (ClosestEnemies == null)
                ClosestEnemies = GetClosestEnemies(request);

            return new List<Move>();
        }

        public List<UnitDto> GetClosestEnemies(MoveRequest request)
        {
            var closestDistance = int.MaxValue;
            var closestTeam = new List<UnitDto>();
            foreach (var enemyId in request.TeamIds)
            {
                var enemyTeam = request.Units.Where(u => u.Team == enemyId).ToList();
                var coord = MyTeam.First().Location;
                var enemyDistance = enemyTeam.First().Location.Distance(coord);
                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;
                    closestTeam = enemyTeam;
                }
            }
            return closestTeam;
        }
    }
}
