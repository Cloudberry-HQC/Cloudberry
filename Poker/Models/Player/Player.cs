namespace Poker.Models.Player
{
    using System.Windows.Forms;

    public abstract class Player
    {
        private Panel panel;
        private int chips;

        protected Player()
        {
            this.panel = new Panel();
            this.chips = 100000;
        }
    }
}