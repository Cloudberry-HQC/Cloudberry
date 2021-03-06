﻿namespace Poker.Interfaces
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
        /// <param name="player">HandFactor player.</param>
        /// <param name="lastly">String parameter.</param>
        void Winner(IPlayer player);
        
        /// <summary>
        /// Checks the type of the player's hand
        /// </summary>
        /// <param name="player">HandFactor player.</param>
        void CheckForHand(IPlayer player);

        List<string> CheckWinners { get; set; }

        Hand Sorted { get; set; }

        double Type { get; set; }

        List<Hand> Win { get; set; }

        int Winners { get; set; }
      
    }
}
