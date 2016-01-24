namespace Poker.Models.Card
{
    using Enums;
    using Interfaces;

    public class Card : ICard
    {
        public Card(SuitOfCard suit, ValueOfCard value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public SuitOfCard Suit { get; set; }

        public ValueOfCard Value { get; set; }
    }
}