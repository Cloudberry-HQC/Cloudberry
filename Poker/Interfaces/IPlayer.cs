namespace Poker.Interfaces
{
    using System.Windows.Forms;
    using Enums;

    /// <summary>
    /// An interface that represents any player in game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The two available cards in the player's hand.
        /// </summary>
        ICard[] PlayerCards { get; set; }
       
        /// <summary>
        /// A property that indicates if the player is on turn.
        /// </summary>
        bool IsPlayerTurn { get; set; }
        
        /// <summary>
        /// A property that indicates the player's status.
        /// </summary>
        Label Status { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        string Name { get; set; }
         
        /// <summary>
        /// A property of the player.
        /// </summary>
        Panel Panel { get; }
 
        /// <summary>
        /// The available value of player's chips.
        /// </summary>
        int Chips { get; set; }
        
        /// <summary>
        /// The points according to the player's hand.
        /// </summary>
        double Power { get; set; }

        /// <summary>
        /// A factor that is used for determine the player's hand
        /// </summary>
        TypeOfTheHand HandFactor { get; set; }
         
        bool Turn { get; set; }

        bool FoldTurn { get; set; }
       
        /// <summary>
        /// A property that indicates if the player is folded.
        /// </summary>
        bool HasPlayerFolded { get; set; }
       
        /// <summary>
        /// The amount of chips for Call.
        /// </summary>
        int Call { get; set; }
        
        /// <summary>
        /// The amount of chips for Raise.
        /// </summary>
        int Raise { get; set; }
       
        /// <summary>
        /// Player's behavior if the choice is "Fold"
        /// </summary>
        void PlayerFold();
         
        /// <summary>
        /// Player's behavior if the choice is "Check".
        /// </summary>
        void PlayerCheck();
        
        /// <summary>
        /// Player's behavior if the choice is "Call".
        /// </summary>
        void PlayerCall();
        
        /// <summary>
        /// Player's behavior if the choice is "Raise".
        /// </summary>
        void PlayerRaised();
    }
}