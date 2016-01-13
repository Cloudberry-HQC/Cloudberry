namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Type = Models.Type;

    public partial class Form1 : Form
    {
        #region Variables
        //ProgressBar asd = new ProgressBar(); //elica: It is never used!!!
        //public int Nm;
        private const int InitialValueOfChips = 10000;
        private const int CountOfBots = 5;
        private readonly Panel playerPanel = new Panel();
        private readonly Panel firstBotPanel = new Panel();
        private readonly Panel secondBotPanel = new Panel();
        private readonly Panel thirdBotPanel = new Panel();
        private readonly Panel fourthBotPanel = new Panel();
        private readonly Panel fifthBotPanel = new Panel();
        private int initialCall = 500;         
        private int foldedPlayers = CountOfBots;    
        //elica: chips
        private int chips = InitialValueOfChips;     //elica: It was a puplic field!!! move to constant!
        private int firstBotChips = InitialValueOfChips;
        private int secondBotChips = InitialValueOfChips;
        private int thirdBotChips = InitialValueOfChips;
        private int fourthBotChips = InitialValueOfChips;
        private int fifthBotChips = InitialValueOfChips;

        private double type;
        private double rounds;
        private double firstBotPower;
        private double secondBotPower;
        private double thirdBotPower;
        private double fourthBotPower;
        private double fifthBotPower;
        private double playerPower;
        private double playerType = - 1;
        private double raise;
        private double firstBotType = - 1;
        private double secondBotType = - 1;
        private double thirdBotType = - 1;
        private double fourthBotType = - 1;
        private double fifthBotType = - 1;

        private bool isFirstBotTurn;     
        private bool isSecondBotTurn;
        private bool isThirdBotTurn;
        private bool isFourthBotTurn;
        private bool isFifthBotTurn;

        private bool B1Fturn;
        private bool B2Fturn;
        private bool B3Fturn;
        private bool B4Fturn;
        private bool B5Fturn;

        private bool hasPlayerFolded;
        private bool hasFirstBotFolded;
        private bool hasSecondBotFolded;
        private bool hasThirdBotFolded;
        private bool hasFourthBotFolded;
        private bool hasFifthBotFolded;
        private bool intsadded;
        private bool changed;

        private int playerCall; //Maybe playerCurrentCall is better?
        private int firstBotCall;
        private int secondBotCall;
        private int thirdBotCall;
        private int fourthBotCall;
        private int fifthBotCall;

        private int playerRaise;
        private int firstBotRaise;
        private int secondBotRaise;
        private int thirdBotRaise;
        private int fourthBotRaise;
        private int fifthBotRaise;

        private int height;
        private int width;
        private int winners;
        private int flop = 1; //should be removed as soon as we understand what are those vasiables used for
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;

        private int last = 123; // never used?
        private int raisedTurn = 1;

        private List<bool?> bools = new List<bool?>();
        private List<Type> Win = new List<Type>();
        private List<string> CheckWinners = new List<string>();
        private List<int> ints = new List<int>();

        private bool PFturn;
        private bool Pturn = true;
        private bool restart;
        private bool raising;

        Type sorted;
        string[] ImgCardsLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgCardsLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/

        private int[] availableCardsInGame = new int[17];     //elica: rename from reverce
        private Image[] cardsImageDeck = new Image[52];
        private PictureBox[] cardsHolder = new PictureBox[52];
        private Timer timer = new Timer();
        private Timer Updates = new Timer();

        private int t = 60;
        private int i;
        private int bigBlind = 500;
        private int smallBlind = 250;
        private int up = 10000000;
        private int turnCount;

        #endregion
        public Form1()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.initialCall = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            Shuffle();
            this.tbPot.Enabled = false;
            this.tbChips.Enabled = false;
            this.tbBotChips1.Enabled = false;
            this.tbBotChips2.Enabled = false;
            this.tbBotChips3.Enabled = false;
            this.tbBotChips4.Enabled = false;
            this.tbBotChips5.Enabled = false;
            this.tbChips.Text = "chips : " + this.chips.ToString();
            this.tbBotChips1.Text = "chips : " + this.firstBotChips.ToString();
            this.tbBotChips2.Text = "chips : " + this.secondBotChips.ToString();
            this.tbBotChips3.Text = "chips : " + this.thirdBotChips.ToString();
            this.tbBotChips4.Text = "chips : " + this.fourthBotChips.ToString();
            this.tbBotChips5.Text = "chips : " + this.fifthBotChips.ToString();
            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);
            this.Updates.Tick += Update_Tick;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = false;
            this.tbSB.Visible = false;
            this.bBB.Visible = false;
            this.bSB.Visible = false;
            this.tbRaise.Text = (this.bigBlind * 2).ToString();
        }
        async Task Shuffle()
        {
            this.bools.Add(this.PFturn);
            this.bools.Add(this.B1Fturn);
            this.bools.Add(this.B2Fturn);
            this.bools.Add(this.B3Fturn);
            this.bools.Add(this.B4Fturn);
            this.bools.Add(this.B5Fturn);
            this.bCall.Enabled = false;
            this.bRaise.Enabled = false;
            this.bFold.Enabled = false;
            this.bCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580;
            int vertical = 480;
            Random r = new Random();

            for (int i = this.ImgCardsLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = this.ImgCardsLocation[j];
                this.ImgCardsLocation[j] = this.ImgCardsLocation[i - 1];
                this.ImgCardsLocation[i - 1] = k;
            }

            for (int i = 0; i < 17; i++)
            {
                this.cardsImageDeck[i] = Image.FromFile(this.ImgCardsLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };

                foreach (var c in charsToRemove)
                {
                    this.ImgCardsLocation[i] = this.ImgCardsLocation[i].Replace(c, string.Empty);
                }

                this.availableCardsInGame[i] = int.Parse(this.ImgCardsLocation[i]) - 1;
                this.cardsHolder[i] = new PictureBox();
                this.cardsHolder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardsHolder[i].Height = 130;
                this.cardsHolder[i].Width = 80;
                this.Controls.Add(this.cardsHolder[i]);
                this.cardsHolder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards

                if (i < 2)
                {
                    if (this.cardsHolder[0].Tag != null)
                    {
                        this.cardsHolder[1].Tag = this.availableCardsInGame[1];
                    }

                    this.cardsHolder[0].Tag = this.availableCardsInGame[0];
                    this.cardsHolder[i].Image = this.cardsImageDeck[i];
                    this.cardsHolder[i].Anchor = (AnchorStyles.Bottom);
                    //cardsHolder[i].Dock = DockStyle.Top;
                    this.cardsHolder[i].Location = new Point(horizontal, vertical);
                    horizontal += this.cardsHolder[i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.cardsHolder[0].Left - 10, this.cardsHolder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.firstBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (i >= 2 && i < 4)
                    {
                        if (this.cardsHolder[2].Tag != null)
                        {
                            this.cardsHolder[3].Tag = this.availableCardsInGame[3];
                        }

                        this.cardsHolder[2].Tag = this.availableCardsInGame[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.cardsHolder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[i].Width;
                        this.cardsHolder[i].Visible = true;
                        this.Controls.Add(this.firstBotPanel);
                        this.firstBotPanel.Location = new Point(this.cardsHolder[2].Left - 10, this.cardsHolder[2].Top - 10);
                        this.firstBotPanel.BackColor = Color.DarkBlue;
                        this.firstBotPanel.Height = 150;
                        this.firstBotPanel.Width = 180;
                        this.firstBotPanel.Visible = false;

                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.secondBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (i >= 4 && i < 6)
                    {
                        if (this.cardsHolder[4].Tag != null)
                        {
                            this.cardsHolder[5].Tag = this.availableCardsInGame[5];
                        }

                        this.cardsHolder[4].Tag = this.availableCardsInGame[4];

                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.cardsHolder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[i].Width;
                        this.cardsHolder[i].Visible = true;
                        this.Controls.Add(this.secondBotPanel);
                        this.secondBotPanel.Location = new Point(this.cardsHolder[4].Left - 10, this.cardsHolder[4].Top - 10);
                        this.secondBotPanel.BackColor = Color.DarkBlue;
                        this.secondBotPanel.Height = 150;
                        this.secondBotPanel.Width = 180;
                        this.secondBotPanel.Visible = false;

                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.thirdBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (i >= 6 && i < 8)
                    {
                        if (this.cardsHolder[6].Tag != null)
                        {
                            this.cardsHolder[7].Tag = this.availableCardsInGame[7];
                        }

                        this.cardsHolder[6].Tag = this.availableCardsInGame[6];

                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;
                        this.cardsHolder[i].Anchor = (AnchorStyles.Top);
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[i].Width;
                        this.cardsHolder[i].Visible = true;
                        this.Controls.Add(this.thirdBotPanel);
                        this.thirdBotPanel.Location = new Point(this.cardsHolder[6].Left - 10, this.cardsHolder[6].Top - 10);
                        this.thirdBotPanel.BackColor = Color.DarkBlue;
                        this.thirdBotPanel.Height = 150;
                        this.thirdBotPanel.Width = 180;
                        this.thirdBotPanel.Visible = false;

                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fourthBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (i >= 8 && i < 10)
                    {
                        if (this.cardsHolder[8].Tag != null)
                        {
                            this.cardsHolder[9].Tag = this.availableCardsInGame[9];
                        }

                        this.cardsHolder[8].Tag = this.availableCardsInGame[8];

                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.cardsHolder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[i].Width;
                        this.cardsHolder[i].Visible = true;
                        this.Controls.Add(this.fourthBotPanel);
                        this.fourthBotPanel.Location = new Point(this.cardsHolder[8].Left - 10, this.cardsHolder[8].Top - 10);
                        this.fourthBotPanel.BackColor = Color.DarkBlue;
                        this.fourthBotPanel.Height = 150;
                        this.fourthBotPanel.Width = 180;
                        this.fourthBotPanel.Visible = false;

                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fifthBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (i >= 10 && i < 12)
                    {
                        if (this.cardsHolder[10].Tag != null)
                        {
                            this.cardsHolder[11].Tag = this.availableCardsInGame[11];
                        }

                        this.cardsHolder[10].Tag = this.availableCardsInGame[10];

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.cardsHolder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[i].Width;
                        this.cardsHolder[i].Visible = true;
                        this.Controls.Add(this.fifthBotPanel);
                        this.fifthBotPanel.Location = new Point(this.cardsHolder[10].Left - 10, this.cardsHolder[10].Top - 10);
                        this.fifthBotPanel.BackColor = Color.DarkBlue;
                        this.fifthBotPanel.Height = 150;
                        this.fifthBotPanel.Width = 180;
                        this.fifthBotPanel.Visible = false;

                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }

                if (i >= 12)
                {
                    this.cardsHolder[12].Tag = this.availableCardsInGame[12];

                    if (i > 12)
                    {
                        this.cardsHolder[13].Tag = this.availableCardsInGame[13];
                    }

                    if (i > 13)
                    {
                        this.cardsHolder[14].Tag = this.availableCardsInGame[14];
                    }

                    if (i > 14)
                    {
                        this.cardsHolder[15].Tag = this.availableCardsInGame[15];
                    }

                    if (i > 15)
                    {
                        this.cardsHolder[16].Tag = this.availableCardsInGame[16];

                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;

                    if (this.cardsHolder[i] != null)
                    {
                        this.cardsHolder[i].Anchor = AnchorStyles.None;
                        this.cardsHolder[i].Image = backImage;
                        //cardsHolder[i].Image = cardsImageDeck[i];
                        this.cardsHolder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion
                if (this.firstBotChips <= 0)
                {
                    this.B1Fturn = true;
                    this.cardsHolder[2].Visible = false;
                    this.cardsHolder[3].Visible = false;
                }
                else
                {
                    this.B1Fturn = false;
                    if (i == 3)
                    {
                        if (this.cardsHolder[3] != null)
                        {
                            this.cardsHolder[2].Visible = true;
                            this.cardsHolder[3].Visible = true;
                        }
                    }
                }

                if (this.secondBotChips <= 0)
                {
                    this.B2Fturn = true;
                    this.cardsHolder[4].Visible = false;
                    this.cardsHolder[5].Visible = false;
                }
                else
                {
                    this.B2Fturn = false;
                    if (i == 5)
                    {
                        if (this.cardsHolder[5] != null)
                        {
                            this.cardsHolder[4].Visible = true;
                            this.cardsHolder[5].Visible = true;
                        }
                    }
                }

                if (this.thirdBotChips <= 0)
                {
                    this.B3Fturn = true;
                    this.cardsHolder[6].Visible = false;
                    this.cardsHolder[7].Visible = false;
                }
                else
                {
                    this.B3Fturn = false;
                    if (i == 7)
                    {
                        if (this.cardsHolder[7] != null)
                        {
                            this.cardsHolder[6].Visible = true;
                            this.cardsHolder[7].Visible = true;
                        }
                    }
                }

                if (this.fourthBotChips <= 0)
                {
                    this.B4Fturn = true;
                    this.cardsHolder[8].Visible = false;
                    this.cardsHolder[9].Visible = false;
                }
                else
                {
                    this.B4Fturn = false;
                    if (i == 9)
                    {
                        if (this.cardsHolder[9] != null)
                        {
                            this.cardsHolder[8].Visible = true;
                            this.cardsHolder[9].Visible = true;
                        }
                    }
                }

                if (this.fifthBotChips <= 0)
                {
                    this.B5Fturn = true;
                    this.cardsHolder[10].Visible = false;
                    this.cardsHolder[11].Visible = false;
                }
                else
                {
                    this.B5Fturn = false;

                    if (i == 11)
                    {
                        if (this.cardsHolder[11] != null)
                        {
                            this.cardsHolder[10].Visible = true;
                            this.cardsHolder[11].Visible = true;
                        }
                    }
                }

                if (i == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            }     //elica: here ends the second loop!!!

            if (this.foldedPlayers == 5)    
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", 
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }

            if (this.i == 17)
            {
                this.bRaise.Enabled = true;
                this.bCall.Enabled = true;
                //bRaise.Enabled = true;    //elica: repeating code!!
                //bRaise.Enabled = true;
                this.bFold.Enabled = true;
            }
        }

        async Task Turns()
        {
            #region Rotating
            if (!this.PFturn)
            {
                if (this.Pturn)
                {
                    FixCall(this.pStatus, ref this.playerCall, ref this.playerRaise, 1);
                    //MessageBox.Show("Player's Turn");
                    this.pbTimer.Visible = true;
                    this.pbTimer.Value = 1000;
                    this.t = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.bRaise.Enabled = true;
                    this.bCall.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bFold.Enabled = true;
                    this.turnCount++;
                    FixCall(this.pStatus, ref this.playerCall, ref this.playerRaise, 2);
                }
            }

            if (this.PFturn || !this.Pturn)
            {
                await AllIn();
                if (this.PFturn && !this.hasPlayerFolded)
                {
                    if (this.bCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }

                await CheckRaise(0, 0);
                this.pbTimer.Visible = false;
                this.bRaise.Enabled = false;
                this.bCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bRaise.Enabled = false;
                this.bFold.Enabled = false;
                this.timer.Stop();
                this.isFirstBotTurn = true;

                if (!this.B1Fturn)
                {
                    if (this.isFirstBotTurn)
                    {
                        FixCall(this.b1Status, ref this.firstBotCall, ref this.firstBotRaise, 1);
                        FixCall(this.b1Status, ref this.firstBotCall, ref this.firstBotRaise, 2);
                        Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, this.B1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        AI(2, 3, ref this.firstBotChips, ref this.isFirstBotTurn, ref this.B1Fturn, this.b1Status, 0, this.firstBotPower, this.firstBotType);
                        this.turnCount++;
                        this.last = 1;
                        this.isFirstBotTurn = false;
                        this.isSecondBotTurn = true;
                    }
                }

                if (this.B1Fturn && !this.hasFirstBotFolded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.hasFirstBotFolded = true;
                }

                if (this.B1Fturn || !this.isFirstBotTurn)
                {
                    await CheckRaise(1, 1);
                    this.isSecondBotTurn = true;
                }

                if (!this.B2Fturn)
                {
                    if (this.isSecondBotTurn)
                    {
                        FixCall(this.b2Status, ref this.secondBotCall, ref this.secondBotRaise, 1);
                        FixCall(this.b2Status, ref this.secondBotCall, ref this.secondBotRaise, 2);
                        Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, this.B2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        AI(4, 5, ref this.secondBotChips, ref this.isSecondBotTurn, ref this.B2Fturn, this.b2Status, 1, this.secondBotPower, this.secondBotType);
                        this.turnCount++;
                        this.last = 2;
                        this.isSecondBotTurn = false;
                        this.isThirdBotTurn = true;
                    }
                }

                if (this.B2Fturn && !this.hasSecondBotFolded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.hasSecondBotFolded = true;
                }

                if (this.B2Fturn || !this.isSecondBotTurn)
                {
                    await CheckRaise(2, 2);
                    this.isThirdBotTurn = true;
                }

                if (!this.B3Fturn)
                {
                    if (this.isThirdBotTurn)
                    {
                        FixCall(this.b3Status, ref this.thirdBotCall, ref this.thirdBotRaise, 1);
                        FixCall(this.b3Status, ref this.thirdBotCall, ref this.thirdBotRaise, 2);
                        Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, this.B3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        AI(6, 7, ref this.thirdBotChips, ref this.isThirdBotTurn, ref this.B3Fturn, this.b3Status, 2, this.thirdBotPower, this.thirdBotType);
                        this.turnCount++;
                        this.last = 3;
                        this.isThirdBotTurn = false;
                        this.isFourthBotTurn = true;
                    }
                }

                if (this.B3Fturn && !this.hasThirdBotFolded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.hasThirdBotFolded = true;
                }

                if (this.B3Fturn || !this.isThirdBotTurn)
                {
                    await CheckRaise(3, 3);
                    this.isFourthBotTurn = true;
                }

                if (!this.B4Fturn)
                {
                    if (this.isFourthBotTurn)
                    {
                        FixCall(this.b4Status, ref this.fourthBotCall, ref this.fourthBotRaise, 1);
                        FixCall(this.b4Status, ref this.fourthBotCall, ref this.fourthBotRaise, 2);
                        Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, this.B4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref this.fourthBotChips, ref this.isFourthBotTurn, ref this.B4Fturn, this.b4Status, 3, this.fourthBotPower, this.fourthBotType);
                        this.turnCount++;
                        this.last = 4;
                        this.isFourthBotTurn = false;
                        this.isFifthBotTurn = true;
                    }
                }

                if (this.B4Fturn && !this.hasFourthBotFolded)
                {
                    this.bools.RemoveAt(4);
                    this.bools.Insert(4, null);
                    this.maxLeft--;
                    this.hasFourthBotFolded = true;
                }

                if (this.B4Fturn || !this.isFourthBotTurn)
                {
                    await CheckRaise(4, 4);
                    this.isFifthBotTurn = true;
                }

                if (!this.B5Fturn)
                {
                    if (this.isFifthBotTurn)
                    {
                        FixCall(this.b5Status, ref this.fifthBotCall, ref this.fifthBotRaise, 1);
                        FixCall(this.b5Status, ref this.fifthBotCall, ref this.fifthBotRaise, 2);
                        Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, this.B5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref this.fifthBotChips, ref this.isFifthBotTurn, ref this.B5Fturn, this.b5Status, 4, this.fifthBotPower, this.fifthBotType);
                        this.turnCount++;
                        this.last = 5;
                        this.isFifthBotTurn = false;
                    }
                }

                if (this.B5Fturn && !this.hasFifthBotFolded)
                {
                    this.bools.RemoveAt(5);
                    this.bools.Insert(5, null);
                    this.maxLeft--;
                    this.hasFifthBotFolded = true;
                }

                if (this.B5Fturn || !this.isFifthBotTurn)
                {
                    await CheckRaise(5, 5);
                    this.Pturn = true;
                }

                if (this.PFturn && !this.hasPlayerFolded)
                {
                    if (this.bCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }

                #endregion
                await AllIn();
                if (!this.restart)
                {
                    await Turns();
                }
                this.restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)        //elica: Empty if statement
            {
            }

            if (!foldedTurn || c1 == 0 && c2 == 1 && this.pStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = this.availableCardsInGame[c1];
                Straight[1] = this.availableCardsInGame[c2];
                Straight1[0] = Straight[2] = this.availableCardsInGame[12];
                Straight1[1] = Straight[3] = this.availableCardsInGame[13];
                Straight1[2] = Straight[4] = this.availableCardsInGame[14];
                Straight1[3] = Straight[5] = this.availableCardsInGame[15];
                Straight1[4] = Straight[6] = this.availableCardsInGame[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);
                #endregion

                for (int i = 0; i < 16; i++)
                {
                    if (this.availableCardsInGame[i] == int.Parse(this.cardsHolder[c1].Tag.ToString()) && this.availableCardsInGame[i + 1] == int.Parse(this.cardsHolder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(ref current, ref Power, ref vf, Straight1);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(ref current, ref Power, ref done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(ref current, ref Power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }

        private void rStraightFlush(ref double current, ref double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = (st1.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 8 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 9 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 8 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 9 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 8 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 9 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 8 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 9 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 7 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 7 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                this.type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 6 });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 6 });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    Power = this.type;
                }
            }
        }

        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (this.availableCardsInGame[this.i] % 4 == this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[this.i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[this.i] / 4 < f1.Max() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[this.i] % 4 != this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 != this.availableCardsInGame[this.i] % 4 && this.availableCardsInGame[this.i + 1] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 5)
                {
                    if (this.availableCardsInGame[this.i] % 4 == f1[0] % 4 && this.availableCardsInGame[this.i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 == f1[0] % 4 && this.availableCardsInGame[this.i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i + 1] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[this.i] / 4 < f1.Min() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (this.availableCardsInGame[this.i] % 4 == this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[this.i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[this.i] / 4 < f2.Max() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[this.i] % 4 != this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 != this.availableCardsInGame[this.i] % 4 && this.availableCardsInGame[this.i + 1] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 5)
                {
                    if (this.availableCardsInGame[this.i] % 4 == f2[0] % 4 && this.availableCardsInGame[this.i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 == f2[0] % 4 && this.availableCardsInGame[this.i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i + 1] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[this.i] / 4 < f2.Min() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (this.availableCardsInGame[this.i] % 4 == this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[this.i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[this.i] / 4 < f3.Max() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[this.i] % 4 != this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 != this.availableCardsInGame[this.i] % 4 && this.availableCardsInGame[this.i + 1] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 5)
                {
                    if (this.availableCardsInGame[this.i] % 4 == f3[0] % 4 && this.availableCardsInGame[this.i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 == f3[0] % 4 && this.availableCardsInGame[this.i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i + 1] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[this.i] / 4 < f3.Min() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (this.availableCardsInGame[this.i] % 4 == this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[this.i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[this.i] / 4 < f4.Max() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[this.i] % 4 != this.availableCardsInGame[this.i + 1] % 4 && this.availableCardsInGame[this.i] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 != this.availableCardsInGame[this.i] % 4 && this.availableCardsInGame[this.i + 1] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[this.i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[this.i + 1] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 5 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 5)
                {
                    if (this.availableCardsInGame[this.i] % 4 == f4[0] % 4 && this.availableCardsInGame[this.i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[this.i + 1] % 4 == f4[0] % 4 && this.availableCardsInGame[this.i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[this.i + 1] + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[this.i] / 4 < f4.Min() / 4 && this.availableCardsInGame[this.i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (this.availableCardsInGame[this.i] / 4 == 0 && this.availableCardsInGame[this.i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (this.availableCardsInGame[this.i + 1] / 4 == 0 && this.availableCardsInGame[this.i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f2.Length > 0)
                {
                    if (this.availableCardsInGame[this.i] / 4 == 0 && this.availableCardsInGame[this.i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (this.availableCardsInGame[this.i + 1] / 4 == 0 && this.availableCardsInGame[this.i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f3.Length > 0)
                {
                    if (this.availableCardsInGame[this.i] / 4 == 0 && this.availableCardsInGame[this.i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (this.availableCardsInGame[this.i + 1] / 4 == 0 && this.availableCardsInGame[this.i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f4.Length > 0)
                {
                    if (this.availableCardsInGame[this.i] / 4 == 0 && this.availableCardsInGame[this.i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (this.availableCardsInGame[this.i + 1] / 4 == 0 && this.availableCardsInGame[this.i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 5.5 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 4 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 4 });
                            this.sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        this.Win.Add(new Type() { Power = Power, Current = 4 });
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 3 });
                            this.sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 3 });
                            this.sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (this.availableCardsInGame[this.i] / 4 != this.availableCardsInGame[this.i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (this.availableCardsInGame[this.i] / 4 == this.availableCardsInGame[tc] / 4 && this.availableCardsInGame[this.i + 1] / 4 == this.availableCardsInGame[tc - k] / 4 ||
                                    this.availableCardsInGame[this.i + 1] / 4 == this.availableCardsInGame[tc] / 4 && this.availableCardsInGame[this.i] / 4 == this.availableCardsInGame[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.availableCardsInGame[this.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.availableCardsInGame[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[this.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.availableCardsInGame[this.i] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[this.i + 1] / 4 != 0 && this.availableCardsInGame[this.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[this.i] / 4) * 2 + (this.availableCardsInGame[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }

                        if (tc - k >= 12)
                        {
                            if (this.availableCardsInGame[tc] / 4 == this.availableCardsInGame[tc - k] / 4)
                            {
                                if (this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[this.i] / 4 && this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[this.i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.availableCardsInGame[this.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[this.i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[this.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[this.i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[this.i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[this.i + 1] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[this.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[this.i] / 4) * 2 + current * 100;
                                            this.Win.Add(new Type() { Power = Power, Current = 2 });
                                            this.sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                    }
                                    msgbox = true;
                                }

                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.availableCardsInGame[this.i] / 4 > this.availableCardsInGame[this.i + 1] / 4)
                                        {
                                            if (this.availableCardsInGame[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.availableCardsInGame[this.i] / 4 + current * 100;
                                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                                this.sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[this.i] / 4 + current * 100;
                                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                                this.sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (this.availableCardsInGame[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.availableCardsInGame[this.i + 1] + current * 100;
                                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                                this.sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[this.i + 1] / 4 + current * 100;
                                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                                this.sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (this.availableCardsInGame[this.i] / 4 == this.availableCardsInGame[this.i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.availableCardsInGame[this.i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 1 });
                            this.sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 1;
                            Power = (this.availableCardsInGame[this.i + 1] / 4) * 4 + current * 100;
                            this.Win.Add(new Type() { Power = Power, Current = 1 });
                            this.sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (this.availableCardsInGame[this.i + 1] / 4 == this.availableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.availableCardsInGame[this.i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.availableCardsInGame[this.i] / 4 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                this.sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.availableCardsInGame[this.i + 1] / 4) * 4 + this.availableCardsInGame[this.i] / 4 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                this.sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }
                        msgbox = true;
                    }

                    if (this.availableCardsInGame[this.i] / 4 == this.availableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.availableCardsInGame[this.i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.availableCardsInGame[this.i + 1] / 4 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                this.sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.availableCardsInGame[tc] / 4) * 4 + this.availableCardsInGame[this.i + 1] / 4 + current * 100;
                                this.Win.Add(new Type() { Power = Power, Current = 1 });
                                this.sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (this.availableCardsInGame[this.i] / 4 > this.availableCardsInGame[this.i + 1] / 4)
                {
                    current = -1;
                    Power = this.availableCardsInGame[this.i] / 4;
                    this.Win.Add(new Type() { Power = Power, Current = -1 });
                    this.sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }
                else
                {
                    current = -1;
                    Power = this.availableCardsInGame[this.i + 1] / 4;
                    this.Win.Add(new Type() { Power = Power, Current = -1 });
                    this.sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }

                if (this.availableCardsInGame[this.i] / 4 == 0 || this.availableCardsInGame[this.i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    this.Win.Add(new Type() { Power = Power, Current = -1 });
                    this.sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (this.cardsHolder[j].Visible)
                    this.cardsHolder[j].Image = this.cardsImageDeck[j];
            }

            if (current == this.sorted.Current)
            {
                if (Power == this.sorted.Power)
                {
                    this.winners++;
                    this.CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }

            if (currentText == lastly)//lastfixed
            {
                if (this.winners > 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbChips.Text = this.chips.ToString();
                        //playerPanel.Visible = true;

                    }

                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.firstBotChips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbBotChips1.Text = this.firstBotChips.ToString();
                        //firstBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.secondBotChips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbBotChips2.Text = this.secondBotChips.ToString();
                        //secondBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.thirdBotChips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbBotChips3.Text = this.thirdBotChips.ToString();
                        //thirdBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.fourthBotChips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbBotChips4.Text = this.fourthBotChips.ToString();
                        //fourthBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.fifthBotChips += int.Parse(this.tbPot.Text) /this.winners;
                        this.tbBotChips5.Text = this.fifthBotChips.ToString();
                        //fifthBotPanel.Visible = true;
                    }
                    //await Finish(1);
                }

                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //playerPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.firstBotChips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //firstBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.secondBotChips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //secondBotPanel.Visible = true;

                    }

                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.thirdBotChips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //thirdBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.fourthBotChips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //fourthBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.fifthBotChips += int.Parse(this.tbPot.Text);
                        //await Finish(1);
                        //fifthBotPanel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.initialCall = 0;
                        this.raisedTurn = 123;
                        this.rounds++;
                        if (!this.PFturn)
                        {
                            this.pStatus.Text = "";
                        }

                        if (!this.B1Fturn)
                        {
                            this.b1Status.Text = "";
                        }

                        if (!this.B2Fturn)
                        {
                            this.b2Status.Text = "";
                        }

                        if (!this.B3Fturn)
                        {
                            this.b3Status.Text = "";
                        }

                        if (!this.B4Fturn)
                        {
                            this.b4Status.Text = "";
                        }

                        if (!this.B5Fturn)
                        {
                            this.b5Status.Text = "";
                        }
                    }
                }
            }

            if (this.rounds == this.flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
                        this.playerCall = 0; this.playerRaise = 0;
                        this.firstBotCall = 0; this.firstBotRaise = 0;
                        this.secondBotCall = 0; this.secondBotRaise = 0;
                        this.thirdBotCall = 0; this.thirdBotRaise = 0;
                        this.fourthBotCall = 0; this.fourthBotRaise = 0;
                        this.fifthBotCall = 0; this.fifthBotRaise = 0;
                    }
                }
            }

            if (this.rounds == this.turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
                        this.playerCall = 0; this.playerRaise = 0;
                        this.firstBotCall = 0; this.firstBotRaise = 0;
                        this.secondBotCall = 0; this.secondBotRaise = 0;
                        this.thirdBotCall = 0; this.thirdBotRaise = 0;
                        this.fourthBotCall = 0; this.fourthBotRaise = 0;
                        this.fifthBotCall = 0; this.fifthBotRaise = 0;
                    }
                }
            }

            if (this.rounds == this.river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
                        this.playerCall = 0; this.playerRaise = 0;
                        this.firstBotCall = 0; this.firstBotRaise = 0;
                        this.secondBotCall = 0; this.secondBotRaise = 0;
                        this.thirdBotCall = 0; this.thirdBotRaise = 0;
                        this.fourthBotCall = 0; this.fourthBotRaise = 0;
                        this.fifthBotCall = 0; this.fifthBotRaise = 0;
                    }
                }
            }

            if (this.rounds == this.end && this.maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.pStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
                }

                if (!this.b1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, this.B1Fturn);
                }

                if (!this.b2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, this.B2Fturn);
                }

                if (!this.b3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, this.B3Fturn);
                }

                if (!this.b4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, this.B4Fturn);
                }

                if (!this.b5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, this.B5Fturn);
                }

                Winner(this.playerType, this.playerPower, "Player", this.chips, fixedLast);
                Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
                Winner(this.secondBotType, this.secondBotPower, "Bot 2", this.secondBotChips, fixedLast);
                Winner(this.thirdBotType, this.thirdBotPower, "Bot 3", this.thirdBotChips, fixedLast);
                Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
                Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
                this.restart = true;
                this.Pturn = true;
                this.PFturn = false;
                this.B1Fturn = false;
                this.B2Fturn = false;
                this.B3Fturn = false;
                this.B4Fturn = false;
                this.B5Fturn = false;

                if (this.chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.chips = f2.a;
                        this.firstBotChips += f2.a;
                        this.secondBotChips += f2.a;
                        this.thirdBotChips += f2.a;
                        this.fourthBotChips += f2.a;
                        this.fifthBotChips += f2.a;
                        this.PFturn = false;
                        this.Pturn = true;
                        this.bRaise.Enabled = true;
                        this.bFold.Enabled = true;
                        this.bCheck.Enabled = true;
                        this.bRaise.Text = "raise";
                    }
                }

                this.playerPanel.Visible = false;
                this.firstBotPanel.Visible = false;
                this.secondBotPanel.Visible = false;
                this.thirdBotPanel.Visible = false;
                this.fourthBotPanel.Visible = false;
                this.fifthBotPanel.Visible = false;

                this.playerCall = 0;  
                this.firstBotCall = 0; 
                this.secondBotCall = 0; 
                this.thirdBotCall = 0; 
                this.fourthBotCall = 0; 
                this.fifthBotCall = 0;

                this.playerRaise = 0;
                this.firstBotRaise = 0;
                this.secondBotRaise = 0;
                this.thirdBotRaise = 0;
                this.fourthBotRaise = 0;
                this.fifthBotRaise = 0;

                this.last = 0;
                this.initialCall = this.bigBlind;
                this.raise = 0;
                this.ImgCardsLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.rounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                this.type = 0;

                this.firstBotPower = 0;
                this.secondBotPower = 0;
                this.thirdBotPower = 0;
                this.fourthBotPower = 0;
                this.fifthBotPower = 0;

                this.firstBotType = -1;
                this.secondBotType = -1;
                this.thirdBotType = -1;
                this.fourthBotType = -1;
                this.fifthBotType = -1;

                this.ints.Clear();
                this.CheckWinners.Clear();
                this.winners = 0;
                this.Win.Clear();
                this.sorted.Current = 0;
                this.sorted.Power = 0;

                for (int os = 0; os < 17; os++)
                {
                    this.cardsHolder[os].Image = null;
                    this.cardsHolder[os].Invalidate();
                    this.cardsHolder[os].Visible = false;
                }

                this.tbPot.Text = "0";
                this.pStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }

        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }

                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }

                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }

                if (options == 2)
                {
                    if (cRaise != this.raise && cRaise <= this.raise)
                    {
                        this.initialCall = Convert.ToInt32(this.raise) - cRaise;
                    }

                    if (cCall != this.initialCall || cCall <= this.initialCall)
                    {
                        this.initialCall = this.initialCall - cCall;
                    }

                    if (cRaise == this.raise && this.raise > 0)
                    {
                        this.initialCall = 0;
                        this.bCall.Enabled = false;
                        this.bCall.Text = "Callisfuckedup";  
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            if (this.chips <= 0 && !this.intsadded)
            {
                if (this.pStatus.Text.Contains("raise"))
                {
                    this.ints.Add(this.chips);
                    this.intsadded = true;
                }

                if (this.pStatus.Text.Contains("Call"))
                {
                    this.ints.Add(this.chips);
                    this.intsadded = true;
                }
            }

            this.intsadded = false;
            if (this.firstBotChips <= 0 && !this.B1Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.firstBotChips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.secondBotChips <= 0 && !this.B2Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.secondBotChips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.thirdBotChips <= 0 && !this.B3Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.thirdBotChips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.fourthBotChips <= 0 && !this.B4Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.fourthBotChips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.fifthBotChips <= 0 && !this.B5Fturn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.fifthBotChips);
                    this.intsadded = true;
                }
            }

            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await Finish(2);
            }
            else
            {
                this.ints.Clear();
            }
            #endregion

            var abc = this.bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = this.bools.IndexOf(false);
                if (index == 0)
                {
                    this.chips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.chips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    this.firstBotChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.firstBotChips.ToString();
                    this.firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    this.secondBotChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.secondBotChips.ToString();
                    this.secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    this.thirdBotChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.thirdBotChips.ToString();
                    this.thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    this.fourthBotChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.fourthBotChips.ToString();
                    this.fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    this.fifthBotChips += int.Parse(this.tbPot.Text);
                    this.tbChips.Text = this.fifthBotChips.ToString();
                    this.fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    this.cardsHolder[j].Visible = false;
                }
                await Finish(1);
            }

            this.intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && this.rounds >= this.end)
            {
                await Finish(2);
            }
            #endregion


        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            this.playerPanel.Visible = false;
            this.firstBotPanel.Visible = false;
            this.secondBotPanel.Visible = false;
            this.thirdBotPanel.Visible = false;
            this.fourthBotPanel.Visible = false;
            this.fifthBotPanel.Visible = false;

            this.initialCall = this.bigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;

            this.firstBotPower = 0;
            this.secondBotPower = 0;
            this.thirdBotPower = 0;
            this.fourthBotPower = 0;
            this.fifthBotPower = 0;
            this.playerPower = 0;

            this.playerType = -1;
            this.raise = 0;

            this.firstBotType = -1;
            this.secondBotType = -1;
            this.thirdBotType = -1;
            this.fourthBotType = -1;
            this.fifthBotType = -1;

            this.isFirstBotTurn = false;
            this.isSecondBotTurn = false;
            this.isThirdBotTurn = false;
            this.isFourthBotTurn = false;
            this.isFifthBotTurn = false;

            this.B1Fturn = false;
            this.B2Fturn = false;
            this.B3Fturn = false;
            this.B4Fturn = false;
            this.B5Fturn = false;

            this.hasPlayerFolded = false;
            this.hasFirstBotFolded = false;
            this.hasSecondBotFolded = false;
            this.hasThirdBotFolded = false;
            this.hasFourthBotFolded = false;
            this.hasFifthBotFolded = false;

            this.PFturn = false;
            this.Pturn = true;
            this.restart = false;
            this.raising = false;

            this.playerCall = 0;
            this.firstBotCall = 0;
            this.secondBotCall = 0;
            this.thirdBotCall = 0;
            this.fourthBotCall = 0;
            this.fifthBotCall = 0;

            this.playerRaise = 0;
            this.firstBotRaise = 0;
            this.secondBotRaise = 0;
            this.thirdBotRaise = 0;
            this.fourthBotRaise = 0;
            this.fifthBotRaise = 0;

            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.flop = 1;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.tbPot.Text = "0";
            this.t = 60;
            this.up = 10000000;
            this.turnCount = 0;
            this.pStatus.Text = "";
            this.b1Status.Text = "";
            this.b2Status.Text = "";
            this.b3Status.Text = "";
            this.b4Status.Text = "";
            this.b5Status.Text = "";
            if (this.chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.chips = f2.a;
                    this.firstBotChips += f2.a;
                    this.secondBotChips += f2.a;
                    this.thirdBotChips += f2.a;
                    this.fourthBotChips += f2.a;
                    this.fifthBotChips += f2.a;
                    this.PFturn = false;
                    this.Pturn = true;
                    this.bRaise.Enabled = true;
                    this.bFold.Enabled = true;
                    this.bCheck.Enabled = true;
                    this.bRaise.Text = "raise";
                }
            }

            this.ImgCardsLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.cardsHolder[os].Image = null;
                this.cardsHolder[os].Invalidate();
                this.cardsHolder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";

            if (!this.pStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            }

            if (!this.b1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, this.B1Fturn);
            }

            if (!this.b2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, this.B2Fturn);
            }

            if (!this.b3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, this.B3Fturn);
            }

            if (!this.b4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, this.B4Fturn);
            }

            if (!this.b5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, this.B5Fturn);
            }

            Winner(this.playerType, this.playerPower, "Player", this.chips, fixedLast);
            Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
            Winner(this.secondBotType, this.secondBotPower, "Bot 2", this.secondBotChips, fixedLast);
            Winner(this.thirdBotType, this.thirdBotPower, "Bot 3", this.thirdBotChips, fixedLast);
            Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
            Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
        }
        void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, 
            Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }

            if (sFTurn)
            {
                this.cardsHolder[c1].Visible = false;
                this.cardsHolder[c2].Visible = false;
            }
        }

        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise);
            }
        }

        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
        }

        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            if (botPower <= 327 && botPower >= 321)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            if (botPower < 321 && botPower >= 303)//7 2
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            if (botPower <= 409 && botPower >= 407)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }

        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            this.raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            this.raising = false;
            sTurn = false;
            sChips -= this.initialCall;
            sStatus.Text = "Call " + this.initialCall;
            this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.initialCall).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "raise " + this.raise;
            this.tbPot.Text = (int.Parse(this.tbPot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.initialCall = Convert.ToInt32(this.raise);
            this.raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (this.initialCall <= 0)
            {
                Check(ref sTurn, sStatus);
            }

            if (this.initialCall > 0)
            {
                if (rnd == 1)
                {
                    if (this.initialCall <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }

                if (rnd == 2)
                {
                    if (this.initialCall <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = this.initialCall * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(sChips, n))
                    {
                        this.raise = this.initialCall * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.initialCall <= 0)
                {
                    Check(ref sTurn, sStatus);
                }

                if (this.initialCall > 0)
                {
                    if (this.initialCall >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (this.raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (this.initialCall >= RoundN(sChips, n) && this.initialCall <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (this.raise <= RoundN(sChips, n) && this.raise >= (RoundN(sChips, n)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (this.raise <= (RoundN(sChips, n)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.initialCall * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }

            if (this.rounds >= 2)
            {
                if (this.initialCall > 0)
                {
                    if (this.initialCall >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (this.raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (this.initialCall >= RoundN(sChips, n - rnd) && this.initialCall <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (this.raise <= RoundN(sChips, n - rnd) && this.raise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (this.raise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = this.initialCall * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }

                if (this.initialCall <= 0)
                {
                    this.raise = RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.initialCall <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (this.initialCall >= RoundN(botChips, n))
                {
                    if (botChips > this.initialCall)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= this.initialCall)
                    {
                        this.raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.tbPot.Text = (int.Parse(this.tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botChips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.raise = this.initialCall * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (this.pbTimer.Value <= 0)
            {
                this.PFturn = true;
                await Turns();
            }

            if (this.t > 0)
            {
                this.t--;
                this.pbTimer.Value = (this.t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (this.chips <= 0)
            {
                this.tbChips.Text = "chips : 0";
            }

            if (this.firstBotChips <= 0)
            {
                this.tbBotChips1.Text = "chips : 0";
            }

            if (this.secondBotChips <= 0)
            {
                this.tbBotChips2.Text = "chips : 0";
            }

            if (this.thirdBotChips <= 0)
            {
                this.tbBotChips3.Text = "chips : 0";
            }

            if (this.fourthBotChips <= 0)
            {
                this.tbBotChips4.Text = "chips : 0";
            }

            if (this.fifthBotChips <= 0)
            {
                this.tbBotChips5.Text = "chips : 0";
            }

            this.tbChips.Text = "chips : " + this.chips.ToString();
            this.tbBotChips1.Text = "chips : " + this.firstBotChips.ToString();
            this.tbBotChips2.Text = "chips : " + this.secondBotChips.ToString();
            this.tbBotChips3.Text = "chips : " + this.thirdBotChips.ToString();
            this.tbBotChips4.Text = "chips : " + this.fourthBotChips.ToString();
            this.tbBotChips5.Text = "chips : " + this.fifthBotChips.ToString();

            if (this.chips <= 0)
            {
                this.Pturn = false;
                this.PFturn = true;
                this.bCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bFold.Enabled = false;
                this.bCheck.Enabled = false;
            }

            if (this.up > 0)
            {
                this.up--;
            }

            if (this.chips >= this.initialCall)
            {
                this.bCall.Text = "Call " + this.initialCall.ToString();
            }
            else
            {
                this.bCall.Text = "All in";
                this.bRaise.Enabled = false;
            }

            if (this.initialCall > 0)
            {
                this.bCheck.Enabled = false;
            }
            else           //elica: change  if (initialCall <= 0) with else
            {
                this.bCheck.Enabled = true;
                this.bCall.Text = "Call";
                this.bCall.Enabled = false;
            }

            if (this.chips <= 0)
            {
                this.bRaise.Enabled = false;
            }

            int parsedValue;

            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.chips <= int.Parse(this.tbRaise.Text))
                {
                    this.bRaise.Text = "All in";
                }
                else
                {
                    this.bRaise.Text = "raise";
                }
            }

            if (this.chips < this.initialCall)
            {
                this.bRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            this.pStatus.Text = "Fold";
            this.Pturn = false;
            this.PFturn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (this.initialCall <= 0)
            {
                this.Pturn = false;
                this.pStatus.Text = "Check";
            }
            else
            {
                //pStatus.Text = "All in " + chips;

                this.bCheck.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            if (this.chips >= this.initialCall)
            {
                this.chips -= this.initialCall;
                this.tbChips.Text = "chips : " + this.chips.ToString();

                if (this.tbPot.Text != "")
                {
                    this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.initialCall).ToString();
                }
                else
                {
                    this.tbPot.Text = this.initialCall.ToString();
                }

                this.Pturn = false;
                this.pStatus.Text = "Call " + this.initialCall;
                this.playerCall = this.initialCall;
            }
            else if (this.chips <= this.initialCall && this.initialCall > 0)
            {
                this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.chips).ToString();
                this.pStatus.Text = "All in " + this.chips;
                this.chips = 0;
                this.tbChips.Text = "chips : " + this.chips.ToString();
                this.Pturn = false;
                this.bFold.Enabled = false;
                this.playerCall = this.chips;
            }
            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.PFturn);
            int parsedValue;
            if (this.tbRaise.Text != "" && int.TryParse(this.tbRaise.Text, out parsedValue))
            {
                if (this.chips > this.initialCall)
                {
                    if (this.raise * 2 > int.Parse(this.tbRaise.Text))
                    {
                        this.tbRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.chips >= int.Parse(this.tbRaise.Text))
                        {
                            this.initialCall = int.Parse(this.tbRaise.Text);
                            this.raise = int.Parse(this.tbRaise.Text);
                            this.pStatus.Text = "raise " + this.initialCall.ToString();
                            this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.initialCall).ToString();
                            this.bCall.Text = "Call";
                            this.chips -= int.Parse(this.tbRaise.Text);
                            this.raising = true;
                            this.last = 0;
                            this.playerRaise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            this.initialCall = this.chips;
                            this.raise = this.chips;
                            this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.chips).ToString();
                            this.pStatus.Text = "raise " + this.initialCall.ToString();
                            this.chips = 0;
                            this.raising = true;
                            this.last = 0;
                            this.playerRaise = Convert.ToInt32(this.raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.Pturn = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (this.tbAdd.Text == "")
            {
                //elica Empty if statement
            }
            else
            {
                this.chips += int.Parse(this.tbAdd.Text);
                this.firstBotChips += int.Parse(this.tbAdd.Text);
                this.secondBotChips += int.Parse(this.tbAdd.Text);
                this.thirdBotChips += int.Parse(this.tbAdd.Text);
                this.fourthBotChips += int.Parse(this.tbAdd.Text);
                this.fifthBotChips += int.Parse(this.tbAdd.Text);
            }

            this.tbChips.Text = "chips : " + this.chips.ToString();
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            this.tbBB.Text = this.bigBlind.ToString();
            this.tbSB.Text = this.smallBlind.ToString();

            if (this.tbBB.Visible == false)
            {
                this.tbBB.Visible = true;
                this.tbSB.Visible = true;
                this.bBB.Visible = true;
                this.bSB.Visible = true;
            }
            else
            {
                this.tbBB.Visible = false;
                this.tbSB.Visible = false;
                this.bBB.Visible = false;
                this.bSB.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tbSB.Text.Contains(",") || this.tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.tbSB.Text = this.smallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tbSB.Text = this.smallBlind.ToString();
                return;
            }

            if (int.Parse(this.tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.tbSB.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.tbSB.Text) >= 250 && int.Parse(this.tbSB.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.tbBB.Text.Contains(",") || this.tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.tbBB.Text = this.bigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.tbSB.Text = this.bigBlind.ToString();
                return;
            }

            if (int.Parse(this.tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.tbBB.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.tbBB.Text) >= 500 && int.Parse(this.tbBB.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }
        #endregion
    }
}