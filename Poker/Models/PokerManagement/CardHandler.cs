namespace Poker.Models.PokerManagement
{
    using Card;
    using Enums;
    using Interfaces;

    /// <summary>
    /// A static class that is used for creation of the card. 
    /// </summary>
    public static class CardHandler
    {
        /// <summary>
        /// Gets the suit of the card.
        /// </summary>
        /// <param name="cardAsNumber">HandFactor value of the card as integer number.</param>
        /// <returns>Returns the suit of the card according to the input integer number.</returns>
        public static SuitOfCard GetSuit(int cardAsNumber)
        {
            int reminderSuit = cardAsNumber % 4;

            SuitOfCard suit = SuitOfCard.Clubs;
            switch (reminderSuit)
            {
                case 0:
                    suit = SuitOfCard.Clubs;
                    break;
                case 1:
                    suit = SuitOfCard.Diamonds;
                    break;
                case 2:
                    suit = SuitOfCard.Hearts;
                    break;
                case 3:
                    suit = SuitOfCard.Spades;
                    break;
            }

            return suit;
        }

        /// <summary>
        /// Gets the rank of the card.
        /// </summary>
        /// <param name="cardAsNumber">Current value of the card as integer number.</param>
        /// <returns>Returns the rank of the card according to the input integer number.</returns>
        public static ValueOfCard GetValue(int cardAsNumber)
        {
            int reminderValue = cardAsNumber / 4;

            ValueOfCard value = ValueOfCard.Ace;
            switch (reminderValue)
            {
                case 0:
                    value = ValueOfCard.Ace;
                    break;
                case 1:
                    value = ValueOfCard.Two;
                    break;
                case 2:
                    value = ValueOfCard.Three;
                    break;
                case 3:
                    value = ValueOfCard.Four;
                    break;
                case 4:
                    value = ValueOfCard.Five;
                    break;
                case 5:
                    value = ValueOfCard.Six;
                    break;
                case 6:
                    value = ValueOfCard.Seven;
                    break;
                case 7:
                    value = ValueOfCard.Eight;
                    break;
                case 8:
                    value = ValueOfCard.Nine;
                    break;
                case 9:
                    value = ValueOfCard.Ten;
                    break;
                case 10:
                    value = ValueOfCard.Jack;
                    break;
                case 11:
                    value = ValueOfCard.Queen;
                    break;
                case 12:
                    value = ValueOfCard.King;
                    break;
            }
            return value;
        }

        
        /// <summary>
        /// Gets the card as object.
        /// </summary>
        /// <param name="cardAsNumber">HandFactor value of the card as integer number.</param>
        /// <returns>Returns a new card.</returns>
        public static ICard GetCard(int cardAsNumber)
        {
            ICard card = new Card(cardAsNumber);
            return card;
        }
    }
}