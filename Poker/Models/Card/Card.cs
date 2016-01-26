namespace Poker.Models.Card
{
    using Enums;
    using Interfaces;
    using PokerManagement;

    /// <summary>
    /// Class Card represents any card dealt in the game.
    /// </summary>
    public class Card : ICard
    {
        public Card(int cardAsNumber)
        {
            this.Suit = CardHandler.GetSuit(cardAsNumber);
            this.Value = CardHandler.GetValue(cardAsNumber);
        }

        //The suit of the card
        public SuitOfCard Suit { get; set; }

        //The rank of the card
        public ValueOfCard Value { get; set; }

        //Card number in order of deal
        public int NumberInGame { get; set; }
    }
}