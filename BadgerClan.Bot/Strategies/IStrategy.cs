using BadgerClan.Logic;

namespace BadgerClan.Bot.Strategies
{
    public interface IStrategy
    {
        List<Move> GenerateMoves(MoveRequest request);
    }
}
