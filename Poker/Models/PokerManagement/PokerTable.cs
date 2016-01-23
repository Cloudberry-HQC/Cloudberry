using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;

namespace Poker.Models.PokerManagement
{
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
