namespace Poker.Interfaces
{
    using Enums;

    public interface ICard
    {
        SuitOfCard Suit { get; set; }
        ValueOfCard Value { get; set; }
        int NumberInGame { get; set; }
    }
}