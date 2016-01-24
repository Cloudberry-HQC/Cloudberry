namespace Poker.Core
{
    using Interfaces;
    using Models.Player;
    using Models.PokerManagement;

    public class Database
    {
        private static Database instance = null;
        readonly IPlayer[] players = new Player[6];
        private PokerTable table;
        private Database()
        {

        }
        public IPlayer[] Players { get { return this.players; } }

        public PokerTable Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        public static Database Instace
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                    return instance;
                }
                return instance;
            }
        }
    }
}