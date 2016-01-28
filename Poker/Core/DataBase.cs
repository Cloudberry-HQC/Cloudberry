namespace Poker.Core
{
    using Interfaces;
    using Models.PokerManagement;

    /// <summary>
    /// Class Database holds an array with the available players and instance of the table. 
    /// The instance of the table gives an access to the five cards on table.
    /// </summary>
    public class Database
    {
        private static Database instance;

        private Database()
        {
            this.Players = new IPlayer[6];
        }

        /// <summary>
        /// Array with all players in game.
        /// </summary>
        public IPlayer[] Players { get; }

        /// <summary>
        /// A propery for table that holds the cards on table.
        /// </summary>
        public PokerTable Table { get; set; }

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