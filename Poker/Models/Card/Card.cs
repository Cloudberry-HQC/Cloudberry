using Poker.Models.PokerManagement;

namespace Poker.Models.Card
{
    using Enums;
    using Interfaces;
    

    public class Card : ICard
    {
        public Card(int card)
        {
           this.Suit=CardHandler.GetSuit(card);
            this.Value = CardHandler.GetValue(card);
        }

        public SuitOfCard Suit { get; set; }

        public int NumberInGame { get; set; }

        public ValueOfCard Value { get; set; }
    }
}