using Poker.Enums;
using Poker.Interfaces;

namespace Poker.Models.Card
{
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
