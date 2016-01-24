namespace Poker.Models.Player
{
    using System.Drawing;
    using System.Windows.Forms;
    using Interfaces;

    public abstract class Player : IPlayer
    {
        private const int NumberOfCards = 2;
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
        //private int[] catds=new int[2];
        private ICard[] playerCards;

        protected Player(string name)
        {
            this.panel = new Panel
            {
                BackColor = Color.DarkBlue,
                Height = 150,
                Width = 180,
                Visible = false
            };
            this.chips = 10000;
            this.Name = name;
            this.Status = new Label();
            this.playerCards = new ICard[NumberOfCards];
        }

        public ICard[] PlayerCards
        {
            get { return this.playerCards; }
            set { this.playerCards = value; }
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

        //public void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        //{
        //    raising = false;
        //    sStatus.Text = "Fold";
        //    sTurn = false;
        //    sFTurn = true;
        //}

        //    public void Check()
        //    {
        //        cStatus.Text = "Check";
        //        cTurn = false;
        //        raising = false;
        //    }

        //    public void Call(ref int sChips, ref bool sTurn, Label sStatus)
        //    {
        //        Form1.Raising = false;
        //        sTurn = false;
        //        sChips -= call;
        //        sStatus.Text = "Call " + call;
        //        Form1.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + call).ToString();
        //    }

        //    public void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        //    {
        //        sChips -= Convert.ToInt32(this.raise);
        //        sStatus.Text = "raise " + this.raise;
        //        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
        //        call = Convert.ToInt32(this.raise);
        //        raising = true;
        //        sTurn = false;
        //    }
    }
}