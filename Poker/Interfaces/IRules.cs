namespace Poker.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// This interface gives the ability for identifying the player's hand and determines the winner.
    /// </summary>
    public interface IRules
    {
        void FixWinners();
         
        /// <summary>
        /// Determines the winner.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="lastly">String parameter.</param>
        void Winner(IPlayer player);
        
        /// <summary>
        /// Checks the type of the player's hand
        /// </summary>
        /// <param name="player">Current player.</param>
        void CheckForHand(IPlayer player);

        List<string> CheckWinners { get; set; }

        Type Sorted { get; set; }

        double Type { get; set; }

        List<Type> Win { get; set; }

        int Winners { get; set; }
      
    }
}
