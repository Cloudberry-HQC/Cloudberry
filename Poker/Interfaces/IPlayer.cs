namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IPlayer
    {
        //int[] Cards { get; set; }
        ICard[] PlayerCards { get; set; }

        bool IsPlayerTurn { get; set; }

        Label Status { get; set; }

        string Name { get; set; }

        Panel Panel { get; }

        int Chips { get; set; }

        double Power { get; set; }

        double Current { get; set; }

        bool Turn { get; set; }

        bool FoldTurn { get; set; }

        bool HasPlayerFolded { get; set; }

        int Call { get; set; }

        int Raise { get; set; }
    }
}