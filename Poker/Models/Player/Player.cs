using System;

namespace Poker.Models.Player
{
    using System.Drawing;
    using System.Windows.Forms;
    using Poker;
    public abstract class Player
    {
        private Panel panel;
        private int chips;
        private double power;
        private double current = -1;
        private bool foldTturn;
        private bool turn = true;
        private string name;
        private bool hasPlayerFolded;
        private bool isPlayerTurn;
        private int call;
        private int raise;
        private Label status;
        private int[] catds=new int[2];

        public Player(string name)
        {
            this.panel = new Panel();
            this.panel.BackColor = Color.DarkBlue;
            this.panel.Height = 150;
            this.panel.Width = 180;
            this.panel.Visible = false;
            this.chips = 100000;
            this.Name = name;
            this.Status = new Label();
        }

        public int[] Cards
        {
            get { return this.catds; }
            set { this.catds = value; }
        }
        public bool IsPlayerTurn
        {
            get { return this.isPlayerTurn; }
            set { this.isPlayerTurn = value; }
        }
        public Label Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string Name
        {

            get { return this.name; }
            set { this.name = value; }
        }
        public Panel Panel
        {
            get
            {
                return this.panel;
            }
        }

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

        public double Power
        {
            get
            {
                return this.power;
            }
            set
            {
                this.power = value;
            }
        }

        public double Current
        {
            get
            {
                return this.current;
            }
            set
            {
                this.current = value;
            }
        }

        public bool Turn
        {
            get
            {
                return this.turn;
            }
            set
            {
                this.turn = value;
            }
        }

        public bool FoldTurn
        {
            get
            {
                return this.foldTturn;
            }
            set
            {
                this.foldTturn = value;
            }
        }

        public bool HasPlayerFolded
        {
            get
            {
                return this.hasPlayerFolded;
            }
            set
            {
                this.hasPlayerFolded = value;
            }
        }

        public int Call
        {
            get
            {
                return this.call;

            }
            set
            {
                this.call = value;
            }
        }

        public int Raise
        {
            get
            {
                return this.raise;

            }
            set
            {
                this.raise = value;
            }
        }

        public void PlayerFold()
        {
            Launcher.Poker.Raising = false;
            this.Status.Text = "Fold";
            this.IsPlayerTurn = false;
            this.FoldTurn = true;
        }

        public void PlayerCheck()
        {
            this.Status.Text = "Check";
            this.IsPlayerTurn = false;
            Launcher.Poker.Raising  = false;
        }

        public void PlayerCall()
        {
          //  Form1.Raising = false;
            this.IsPlayerTurn = false;
            this.Chips -= call;
            this.Status.Text = "Call " + call;
            Launcher.Poker.TextBoxPot.Text = (int.Parse(Launcher.Poker.TextBoxPot.Text) + call).ToString();

        }

        public void PlayerRaised()
        {
            Launcher.Poker.Raise *= 2;
            this.Chips -= Convert.ToInt32(Launcher.Poker.Raise);
            this.Status.Text = "Raise " + this.raise;
            Launcher.Poker.TextBoxPot.Text = (int.Parse(Launcher.Poker.TextBoxPot.Text) + Convert.ToInt32(Launcher.Poker.Raise)).ToString();
            Launcher.Poker.CallValue = Convert.ToInt32(this.raise);
            Launcher.Poker.Raising = true;
            this.IsPlayerTurn = false;
        }
    }

}
