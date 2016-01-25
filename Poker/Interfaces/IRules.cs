

using System.Collections.Generic;
using Poker.Models.PokerManagement;

namespace Poker.Interfaces
{
  public  interface IRules
    {
        void FixWinners();

        void Winner(IPlayer player, string lastly);

        void CheckForHand(IPlayer player);

        List<string> CheckWinners { get; set; }

        Type Sorted { get; set; }

        double Type { get; set; }

        List<Type> Win { get; set; }

        int Winners { get; set; }

        
        

    }
}
