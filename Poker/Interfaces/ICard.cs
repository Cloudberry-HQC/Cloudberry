namespace Poker.Interfaces
{
    using Enums;

    /// <summary>
    /// An interface that represents the card and holds the properties for the suit and rank of the card.
    /// </summary>
    public interface ICard
    {
         /// <summary>
        /// The suit of the card.
        /// </summary>
        SuitOfCard Suit { get; set; }

        /// <summary>
        /// The rank of the card.
        /// </summary>
        ValueOfCard Value { get; set; }

        /// <summary>
        /// Card number in order of deal.
        /// </summary>
        int NumberInGame { get; set; }
    }
}