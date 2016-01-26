namespace Poker.Models.PokerManagement
{
    using Interfaces;

    /// <summary>
    /// Class PokerTable holds an array with five cards that are on table.
    /// </summary>
    public class PokerTable
    {
        private const int NumberOfTableCard = 5;

        private ICard[] cardsOnTable;

        public PokerTable()
        {
            this.cardsOnTable = new ICard[NumberOfTableCard];
        }

        /// <summary>
        /// Array of cards that are on the table.
        /// </summary>
        public ICard[] CardsOnTable
        {
            get { return this.cardsOnTable; }
        }
    }
}