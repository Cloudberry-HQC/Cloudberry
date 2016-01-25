namespace Poker.Models.PokerManagement
{
    using System;
    using Enums;
    using Interfaces;

    public static class CardHandler
    {
       

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
    }
}