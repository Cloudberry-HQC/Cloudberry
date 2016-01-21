namespace Poker.Core
{
    using Poker.Models.Player;
    class DataBase
    {
        Player[] players = new Player[6];
        public DataBase()
        {

        }
        public Player[] Players { get { return this.players; } }

    }
}