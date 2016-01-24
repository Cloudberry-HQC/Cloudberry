namespace Poker.Models.PokerManagement
{
    using Interfaces;

    public class PokerTable
    {
        private const int NumberOfTableCard = 5;

        private ICard[] cardsOnTable;

        public PokerTable()
        {
            this.cardsOnTable = new ICard[NumberOfTableCard];
        }

        public ICard[] CardsOnTable
        {
            get { return this.cardsOnTable; }
        }
    }
}