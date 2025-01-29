using BadgerClan.Logic;

namespace BadgerClan.Bot.Bots
{
    public interface IStrategy
    {
        List<Move> GenerateMoves(MoveRequest request);
    }
}
