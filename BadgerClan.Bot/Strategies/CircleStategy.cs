using BadgerClan.Logic;
using BadgerClan.Logic.Bot;
using System.Drawing;

namespace BadgerClan.Bot.Strategies
{
    public class CircleStategy : IStrategy
    {
        private IEnumerable<UnitDto> ClosestEnemies { get; set; }
        private Direction ClosestEnemyDirection { get; set; }
        private IEnumerable<UnitDto> MyTeam {  get; set; }
        public List<Move> GenerateMoves(MoveRequest request)
        {
            MyTeam = request.Units.Where(u => u.Team == request.YourTeamId);
            List<Move> moves = new List<Move>();
            GetClosestEnemies(request);

            if (ClosestEnemies.First().Location.Distance(MyTeam.First().Location) < 6)
                moves = SplitUp(moves);
            else moves = MoveTowardsEnemy(moves);

            return moves;
        }

        private void GetEnemyDirection()
        {
            var myLocation = MyTeam.FirstOrDefault().Location;
            var enemyLocation = ClosestEnemies.FirstOrDefault().Location;
            var nonAbsoluteLocation = enemyLocation - myLocation;
            if (nonAbsoluteLocation.Q < 0)
            {
                if (nonAbsoluteLocation.R < 0) ClosestEnemyDirection = Direction.NorthWest;
                else if (nonAbsoluteLocation.R > 0) ClosestEnemyDirection = Direction.SouthWest;
                else ClosestEnemyDirection = Direction.West;
            }
            else
            {
                if (nonAbsoluteLocation.R < 0) ClosestEnemyDirection = Direction.NorthEast;
                else if (nonAbsoluteLocation.R > 0) ClosestEnemyDirection = Direction.SouthEast;
                else ClosestEnemyDirection = Direction.East;
            }
        }

        public void GetClosestEnemies(MoveRequest request)
        {
            var closestDistance = int.MaxValue;
            foreach (var enemyId in request.TeamIds)
            {
                var enemyTeam = request.Units.Where(u => u.Team == enemyId).ToList();
                var coord = MyTeam.First().Location;
                var enemyDistance = enemyTeam.First().Location.Distance(coord);
                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;
                    ClosestEnemies = enemyTeam;
                    GetEnemyDirection();
                }
            }
        }
        
        public List<Move> SplitUp(List<Move> moves)
        {
            int firstHalf = MyTeam.Count() / 2;
            var teamArray = MyTeam.ToArray();
            for (int i = 0; i < MyTeam.Count() - 1; i++)
            {
                if (i < firstHalf)
                {
                    var coord = ClosestEnemyDirection switch
                    {
                        Direction.NorthEast => teamArray[i].Location.MoveSouthWest(1),
                        Direction.East => teamArray[i].Location.MoveNorthEast(1),
                        Direction.SouthEast => teamArray[i].Location.MoveEast(1),
                        Direction.SouthWest => teamArray[i].Location.MoveWest(1),
                        Direction.West => teamArray[i].Location.MoveNorthWest(1),
                        Direction.NorthWest => teamArray[i].Location.MoveNorthEast(1),
                        _ => teamArray[i].Location
                    };
                    moves.Add(new Move(MoveType.Walk, teamArray[i].Id, coord));
                }
                else
                {
                    var coord = ClosestEnemyDirection switch
                    {
                        Direction.NorthEast => teamArray[i].Location.MoveEast(1),
                        Direction.East => teamArray[i].Location.MoveSouthEast(1),
                        Direction.SouthEast => teamArray[i].Location.MoveSouthWest(1),
                        Direction.SouthWest => teamArray[i].Location.MoveSouthEast(1),
                        Direction.West => teamArray[i].Location.MoveSouthWest(1),
                        Direction.NorthWest => teamArray[i].Location.MoveWest(1),
                        _ => teamArray[i].Location
                    };
                    moves.Add(new Move(MoveType.Walk, teamArray[i].Id, coord));
                }
            }
            return moves;
        }

        public List<Move> MoveTowardsEnemy(List<Move> moves)
        {
            foreach(var unit in MyTeam)
            {
                var coord = ClosestEnemyDirection switch
                {
                    Direction.NorthEast => unit.Location.MoveNorthEast(1),
                    Direction.East => unit.Location.MoveEast(1),
                    Direction.SouthEast => unit.Location.MoveSouthEast(1),
                    Direction.SouthWest => unit.Location.MoveSouthWest(1),
                    Direction.West => unit.Location.MoveWest(1),
                    Direction.NorthWest => unit.Location.MoveNorthWest(1),
                    _ => unit.Location
                };
                moves.Add(new Move(MoveType.Walk, unit.Id, coord));
            }
            return moves;
        }
    }
    public enum Direction { NorthEast, East, SouthEast, SouthWest, West, NorthWest }
}
