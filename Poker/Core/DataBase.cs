namespace Poker.Core
{
    using Interfaces;
    using Models.Player;
    using Models.PokerManagement;

    /// <summary>
    /// Class Database holds an array with the available players and instance of the table. 
    /// The instance of the table gives an access to the five cards on table.
    /// </summary>
    public class Database
    {
        private static Database instance = null;
        private readonly IPlayer[] players = new Player[6];
        private PokerTable table;

        private Database()
        {

        }

        /// <summary>
        /// Array with all players in game.
        /// </summary>
        public IPlayer[] Players { get { return this.players; } }

        /// <summary>
        /// A propery for table that holds the cards on table.
        /// </summary>
        public PokerTable Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        /// <summary>
        /// A property for database.
        /// </summary>
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