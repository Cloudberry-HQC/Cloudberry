using Poker.Enums;

namespace Poker.Interfaces
{
    public interface ICard
    {
        SuitOfCard Suit { get; set; }
        ValueOfCard Value { get; set; }
    }
}
