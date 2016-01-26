namespace Poker.Enums
{
    /// <summary>
    /// An enumeration that holds the different types of hand and their corresponding integer values.
    /// </summary>
    public enum TypeOfTheHand
    {
        HighCard = -1,
        PairTable = 0,
        Pair = 1,
        TwoPairs = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        Flush = 5,
        FullHouse = 6,
        FourOfAKind = 7,
        StraightFlush = 8,
        RoyalFlush = 9
    }
}
