using Poker.Models.PokerManagement;

namespace Poker.Core
{
    using Poker.Models.Player;
    class DataBase
    {
        private static DataBase instance = null;
        Player[] players = new Player[6];
        private PokerTable table;
        private DataBase()
        {

        }
        public Player[] Players { get { return this.players; } }

        public PokerTable Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        public static DataBase Instace
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase();
                    return instance;
                }
                return instance;
            }
        }
    }
}