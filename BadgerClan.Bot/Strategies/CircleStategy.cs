using BadgerClan.Logic;
using BadgerClan.Logic.Bot;
using System.Drawing;

namespace BadgerClan.Bot.Strategies
{
    public class CircleStategy : IStrategy
    {
        private IEnumerable<UnitDto> ClosestEnemies { get; set; }
        private Direction ClosestEnemyDirection { get; set; }
        private IEnumerable<UnitDto> MyTeam { get; set; }
        public List<Move> GenerateMoves(MoveRequest request)
        {
            MyTeam = request.Units.Where(u => u.Team == request.YourTeamId);
            List<Move> moves = new List<Move>();

            if (ClosestEnemies == null)
                GetClosestEnemies(request);

            if (ClosestEnemies.First().Location.Distance(MyTeam.First().Location) <= 3)
            {
                GetClosestEnemies(request);
                moves = AttackEnemy(moves);
            }
            else if (ClosestEnemies.First().Location.Distance(MyTeam.First().Location) <= 4)
                moves = SplitUp(moves);
            else
            {
                GetClosestEnemies(request);
                moves = MoveTowardsEnemy(moves);
            }
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
            foreach (var enemyId in request.TeamIds.Where(t => t != request.YourTeamId))
            {
                var enemyTeam = request.Units.Where(u => u.Team == enemyId).ToList();
                var coord = MyTeam.First().Location;
                var enemyDistance = enemyTeam.FirstOrDefault().Location.Distance(coord);
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
            int teamCount = MyTeam.Count() - 1;
            int firstHalf = teamCount / 2;
            var teamArray = MyTeam.ToArray();
            for (int i = 0; i < teamCount; i++)
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
            foreach (var unit in MyTeam)
            {
                var coord = ClosestEnemies.First().Location;
                moves.Add(new Move(MoveType.Walk, unit.Id, unit.Location.Toward(coord)));
            }
            return moves;
        }

        public List<Move> AttackEnemy(List<Move> moves)
        {
            foreach (var unit in MyTeam)
            {
                var coord = FindClosestEnemy(unit);
                if (coord != unit.Location)
                {
                    if (unit.Health < 4) moves.Add(new Move(MoveType.Medpac, unit.Id, unit.Location));
                    else moves.Add(new Move(MoveType.Attack, unit.Id, unit.Location.Toward(coord)));
                }
            }
            return moves;
        }

        public Coordinate FindClosestEnemy(UnitDto unit)
        {
            Coordinate coord = unit.Location;
            var closestDistance = int.MaxValue;
            foreach (var enemy in ClosestEnemies)
            {
                var enemyLoc = enemy.Location;
                var enemyDistance = enemyLoc.Distance(unit.Location);
                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;
                    coord = enemyLoc;
                }
            }
            return coord;
        }
    }
    public enum Direction { NorthEast, East, SouthEast, SouthWest, West, NorthWest }
}
