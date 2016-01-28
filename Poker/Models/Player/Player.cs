namespace Poker.Models.Player
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using GlobalConstants;
    using Interfaces;

    /// <summary>
    /// Abstract class that holds the common behaviour of the real and artificial player.
    /// </summary>
    public abstract class Player : IPlayer
    {
        private int chips;
        //private int[] catds=new int[2];

        protected Player(string name)
        {
            this.Panel = new Panel
            {
                BackColor = Color.DarkBlue,
                Height = 150,
                Width = 180,
                Visible = false
            };
            this.chips = 10000;
            this.Name = name;
            this.Current = -1;
            this.Status = new Label();
            this.PlayerCards = new ICard[GlobalConstants.NumberOfCards];
        }

        public ICard[] PlayerCards { get; set; }

        public bool IsPlayerTurn { get; set; }

        public Label Status { get; set; }

        public string Name { get; set; }

        public Panel Panel { get; }

        public int Chips
        {
            get
            {
                if (this.chips<0)
                {
                    return 0;
                }
                return this.chips;
            }
            set
            {
                this.chips = value;
            }
        }

        public double Power { get; set; }

        public double Current { get; set; }

        public bool Turn { get; set; } = true;

        public bool FoldTurn { get; set; }

        public bool HasPlayerFolded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public void PlayerFold()
        {
            Launcher.Poker.Raising = false;
            this.Status.Text = GlobalConstants.FoldMessage;
            this.IsPlayerTurn = false;
            this.FoldTurn = true;
        }

        public void PlayerCheck()
        {
            this.Status.Text = GlobalConstants.CheckMessage;
            this.IsPlayerTurn = false;
            Launcher.Poker.Raising = false;
        }
        
        public void PlayerCall()
        {
            //  Form1.Raising = false;
            this.IsPlayerTurn = false;
            this.Chips -= Launcher.Poker.CallValue;
            this.Status.Text = GlobalConstants.CallMessage + " " + Launcher.Poker.CallValue;
            Launcher.Poker.TextBoxPot.Text = (int.Parse(Launcher.Poker.TextBoxPot.Text) + Launcher.Poker.CallValue).ToString();
            Launcher.Poker.Raising = false;
        }

        public void PlayerRaised()
        {
           // Launcher.Poker.Raise *= 2;
            this.Chips -= Convert.ToInt32(Launcher.Poker.Raise);
            this.Status.Text = GlobalConstants.RaiseMessage + " " + Launcher.Poker.Raise;
            Launcher.Poker.TextBoxPot.Text = (int.Parse(Launcher.Poker.TextBoxPot.Text) + Convert.ToInt32(Launcher.Poker.Raise)).ToString();
            Launcher.Poker.CallValue = Convert.ToInt32(Launcher.Poker.Raise);
            Launcher.Poker.Raising = true;
            this.IsPlayerTurn = false;
        }
    }
}