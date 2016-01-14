namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        #region Variables
        //ProgressBar asd = new ProgressBar(); //elica: It is never used!!!
        //public int Nm;
        private const int InitialValueOfChips = 10000;
        private const int DefaultValueOfBigBlind = 500;
        private const int DefaultValueOfSmallBlind = 250;
        private Panel playerPanel = new Panel();
        private Panel firstBotPanel = new Panel();
        private Panel secondBotPanel = new Panel();
        private Panel thirdBotPanel = new Panel();
        private Panel fourthBotPanel = new Panel();
        private Panel fifthBotPanel = new Panel();
        private int call = 500;
        private int foldedPlayers = 5;
        //elica: chips
        private int chips = InitialValueOfChips;     
        private int firstBotChips = InitialValueOfChips;
        private int secondBotChips = InitialValueOfChips;
        private int thirdBotChips = InitialValueOfChips;
        private int fourthBotChips = InitialValueOfChips;
        private int fifthBotChips = InitialValueOfChips;

        private double type;
        private double rounds;
        private double raise;
        private double firstBotPower;
        private double secondBotPower;
        private double thirdBotPower;
        private double fourthBotPower;
        private double fifthBotPower;
        private double playerPower;
        private double playerType = -1;
        private double firstBotType = -1;
        private double secondBotType = -1;
        private double thirdBotType = -1;
        private double fourthBotType = -1;
        private double fifthBotType = -1;

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

        private int playerCall;
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
        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;

        private int last = 123;
        private int raisedTurn = 1;

        private List<bool?> bools = new List<bool?>();
        private List<Type> Win = new List<Type>();
        private List<string> CheckWinners = new List<string>();
        private List<int> ints = new List<int>();

        private bool PFturn;
        private bool Pturn = true;
        private bool restart;
        private bool raising;

        Poker.Type sorted;
        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        private int[] availableCardsInGame = new int[17];
        private Image[] cardsImageDeck = new Image[52];
        private PictureBox[] cardsHolder = new PictureBox[52];
        private Timer timer = new Timer();
        private Timer Updates = new Timer();

        private int t = 60;
        private int globalShit;
        private int defaultBigBlind = DefaultValueOfBigBlind;
        private int defaultSmallBlind = DefaultValueOfSmallBlind;
        private int up = 10000000;
        private int turnCount;

        #endregion
        public Form1()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.call = this.defaultBigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            Shuffle();
            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;   //elica: text box of the player  ????
            this.textBoxBotChips1.Enabled = false;
            this.textBoxBotChips2.Enabled = false;
            this.textBoxBotChips3.Enabled = false;
            this.textBoxBotChips4.Enabled = false;
            this.textBoxBotChips5.Enabled = false;

            this.textBoxChips.Text = "chips : " + this.chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBotChips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondBotChips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdBotChips.ToString();
            this.textBoxBotChips4.Text = "chips : " + this.fourthBotChips.ToString();
            this.textBoxBotChips5.Text = "chips : " + this.fifthBotChips.ToString();

            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += this.timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);
            this.Updates.Tick += this.Update_Tick;
            //this.textBoxBigBlind.Visible = true;     //elica: repeating..next few line they become false!!
            //this.textBoxSmallBlind.Visible = true;
            //this.buttonBigBlind.Visible = true;     
            //this.buttonSmallBlind.Visible = true;
            //this.textBoxBigBlind.Visible = true;
            //this.textBoxSmallBlind.Visible = true;
            //this.buttonBigBlind.Visible = true;
            //this.buttonSmallBlind.Visible = true;
            this.textBoxBigBlind.Visible = false;
            this.textBoxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;
            this.textBoxRaise.Text = (this.defaultBigBlind * 2).ToString();
        }
        async Task Shuffle()
        {
            this.bools.Add(PFturn);
            this.bools.Add(B1Fturn);
            this.bools.Add(B2Fturn);
            this.bools.Add(B3Fturn);
            this.bools.Add(B4Fturn);
            this.bools.Add(B5Fturn);
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580;
            int vertical = 480;
            Random random = new Random();

            //elica: the method swapped places the cards
            for (int countOfCards = this.ImgLocation.Length; countOfCards > 0; countOfCards--)
            {
                int randomNumber = random.Next(countOfCards);
                var pathToCard = this.ImgLocation[randomNumber];
                this.ImgLocation[randomNumber] = this.ImgLocation[countOfCards - 1];
                this.ImgLocation[countOfCards - 1] = pathToCard;
            }

            for (int cardsInGame = 0; cardsInGame < 17; cardsInGame++)
            {
                this.cardsImageDeck[cardsInGame] = Image.FromFile(ImgLocation[cardsInGame]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };

                foreach (var c in charsToRemove)
                {
                    this.ImgLocation[cardsInGame] = this.ImgLocation[cardsInGame].Replace(c, string.Empty);
                }

                this.availableCardsInGame[cardsInGame] = int.Parse(this.ImgLocation[cardsInGame]) - 1;
                this.cardsHolder[cardsInGame] = new PictureBox();
                this.cardsHolder[cardsInGame].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardsHolder[cardsInGame].Height = 130;
                this.cardsHolder[cardsInGame].Width = 80;
                this.Controls.Add(this.cardsHolder[cardsInGame]);
                this.cardsHolder[cardsInGame].Name = "pb" + cardsInGame.ToString();
                await Task.Delay(200);
                #region Throwing Cards

                if (cardsInGame < 2)
                {
                    if (this.cardsHolder[0].Tag != null)
                    {
                        this.cardsHolder[1].Tag = this.availableCardsInGame[1];
                    }

                    this.cardsHolder[0].Tag = this.availableCardsInGame[0];
                    this.cardsHolder[cardsInGame].Image = this.cardsImageDeck[cardsInGame];
                    this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom);
                    //cardsHolder[globalShit].Dock = DockStyle.Top;
                    this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                    horizontal += this.cardsHolder[cardsInGame].Width;
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

                    if (cardsInGame >= 2 && cardsInGame < 4)     //elica: cards of the first bot with index 2 and 3
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
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.firstBotPanel);
                        this.firstBotPanel.Location = new Point(this.cardsHolder[2].Left - 10, this.cardsHolder[2].Top - 10);
                        this.firstBotPanel.BackColor = Color.DarkBlue;
                        this.firstBotPanel.Height = 150;
                        this.firstBotPanel.Width = 180;
                        this.firstBotPanel.Visible = false;

                        if (cardsInGame == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.secondBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 4 && cardsInGame < 6)  // elica: cards of the second bot with index 4 and 5
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
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.secondBotPanel);
                        this.secondBotPanel.Location = new Point(this.cardsHolder[4].Left - 10, this.cardsHolder[4].Top - 10);
                        this.secondBotPanel.BackColor = Color.DarkBlue;
                        this.secondBotPanel.Height = 150;
                        this.secondBotPanel.Width = 180;
                        this.secondBotPanel.Visible = false;

                        if (cardsInGame == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.thirdBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 6 && cardsInGame < 8)    // elica: cards of the third bot with index 6 and 7
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
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.thirdBotPanel);
                        this.thirdBotPanel.Location = new Point(this.cardsHolder[6].Left - 10, this.cardsHolder[6].Top - 10);
                        this.thirdBotPanel.BackColor = Color.DarkBlue;
                        this.thirdBotPanel.Height = 150;
                        this.thirdBotPanel.Width = 180;
                        this.thirdBotPanel.Visible = false;

                        if (cardsInGame == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fourthBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 8 && cardsInGame < 10)  // elica: cards of the fourth bot with index 8 and 9
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
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.fourthBotPanel);
                        this.fourthBotPanel.Location = new Point(this.cardsHolder[8].Left - 10, this.cardsHolder[8].Top - 10);
                        this.fourthBotPanel.BackColor = Color.DarkBlue;
                        this.fourthBotPanel.Height = 150;
                        this.fourthBotPanel.Width = 180;
                        this.fourthBotPanel.Visible = false;

                        if (cardsInGame == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fifthBotChips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 10 && cardsInGame < 12)   // elica: cards of the fifth bot with index 10 and 11
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
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.fifthBotPanel);
                        this.fifthBotPanel.Location = new Point(this.cardsHolder[10].Left - 10, this.cardsHolder[10].Top - 10);
                        this.fifthBotPanel.BackColor = Color.DarkBlue;
                        this.fifthBotPanel.Height = 150;
                        this.fifthBotPanel.Width = 180;
                        this.fifthBotPanel.Visible = false;

                        if (cardsInGame == 11)
                        {
                            check = false;
                        }
                    }
                }

                // TODO if statement to switch
                if (cardsInGame >= 12)    // elica: cards on the table with index 12, 13, 14, 15, 16
                {
                   
                    this.cardsHolder[12].Tag = this.availableCardsInGame[12];

                    if (cardsInGame > 12)
                    {
                        this.cardsHolder[13].Tag = this.availableCardsInGame[13];
                    }

                    if (cardsInGame > 13)
                    {
                        this.cardsHolder[14].Tag = this.availableCardsInGame[14];
                    }

                    if (cardsInGame > 14)
                    {
                        this.cardsHolder[15].Tag = this.availableCardsInGame[15];
                    }

                    if (cardsInGame > 15)
                    {
                        this.cardsHolder[16].Tag = this.availableCardsInGame[16];

                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;

                    if (this.cardsHolder[cardsInGame] != null)
                    {
                        this.cardsHolder[cardsInGame].Anchor = AnchorStyles.None;
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[globalShit].Image = cardsImageDeck[globalShit];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion
                if (this.firstBotChips <= 0)
                {
                    B1Fturn = true;
                    this.cardsHolder[2].Visible = false;
                    this.cardsHolder[3].Visible = false;
                }
                else
                {
                    B1Fturn = false;
                    if (cardsInGame == 3)
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
                    B2Fturn = true;
                    this.cardsHolder[4].Visible = false;
                    this.cardsHolder[5].Visible = false;
                }
                else
                {
                    B2Fturn = false;
                    if (cardsInGame == 5)
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
                    B3Fturn = true;
                    this.cardsHolder[6].Visible = false;
                    this.cardsHolder[7].Visible = false;
                }
                else
                {
                    B3Fturn = false;
                    if (cardsInGame == 7)
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
                    B4Fturn = true;
                    this.cardsHolder[8].Visible = false;
                    this.cardsHolder[9].Visible = false;
                }
                else
                {
                    B4Fturn = false;
                    if (cardsInGame == 9)
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
                    B5Fturn = true;
                    this.cardsHolder[10].Visible = false;
                    this.cardsHolder[11].Visible = false;
                }
                else
                {
                    B5Fturn = false;

                    if (cardsInGame == 11)
                    {
                        if (this.cardsHolder[11] != null)
                        {
                            this.cardsHolder[10].Visible = true;
                            this.cardsHolder[11].Visible = true;
                        }
                    }
                }

                if (cardsInGame == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }  //elica: end of the second loop

           
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

            //elica: it is unnecessary
            //this.globalShit = 17;
            //if (this.globalShit == 17)
            //{
                this.buttonRaise.Enabled = true;
                this.buttonCall.Enabled = true;
                //this.buttonRaise.Enabled = true;
                //this.buttonRaise.Enabled = true;
                this.buttonFold.Enabled = true;
            //}
        }

        async Task Turns()
        {
            #region Rotating
            if (!PFturn)
            {
                if (Pturn)
                {
                    FixCall(playerStatus, ref this.playerCall, ref this.playerRaise, 1);
                    //MessageBox.Show("Player's turn");
                    pbTimer.Visible = true;
                    pbTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
                    timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    turnCount++;
                    FixCall(playerStatus, ref this.playerCall, ref this.playerRaise, 2);
                }
            }

            if (PFturn || !Pturn)
            {
                await AllIn();
                if (PFturn && !this.hasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }

                await CheckRaise(0, 0);
                pbTimer.Visible = false;
                this.buttonRaise.Enabled = false;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                timer.Stop();
                this.isFirstBotTurn = true;

                if (!B1Fturn)
                {
                    if (this.isFirstBotTurn)
                    {
                        FixCall(firstBotStatus, ref this.firstBotCall, ref this.firstBotRaise, 1);
                        FixCall(firstBotStatus, ref this.firstBotCall, ref this.firstBotRaise, 2);
                        Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, B1Fturn);
                        MessageBox.Show("Bot 1's turn");
                        AI(2, 3, ref this.firstBotChips, ref this.isFirstBotTurn, ref B1Fturn, firstBotStatus, 0, this.firstBotPower, this.firstBotType);
                        turnCount++;
                        last = 1;
                        this.isFirstBotTurn = false;
                        this.isSecondBotTurn = true;
                    }
                }

                if (B1Fturn && !this.hasFirstBotFolded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    this.hasFirstBotFolded = true;
                }

                if (B1Fturn || !this.isFirstBotTurn)
                {
                    await CheckRaise(1, 1);
                    this.isSecondBotTurn = true;
                }

                if (!B2Fturn)
                {
                    if (this.isSecondBotTurn)
                    {
                        FixCall(this.secondBotStatus, ref this.secondBotCall, ref this.secondBotRaise, 1);
                        FixCall(this.secondBotStatus, ref this.secondBotCall, ref this.secondBotRaise, 2);
                        Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, B2Fturn);
                        MessageBox.Show("Bot 2's turn");
                        AI(4, 5, ref this.secondBotChips, ref this.isSecondBotTurn, ref B2Fturn, this.secondBotStatus, 1, this.secondBotPower, this.secondBotType);
                        turnCount++;
                        last = 2;
                        this.isSecondBotTurn = false;
                        this.isThirdBotTurn = true;
                    }
                }

                if (B2Fturn && !this.hasSecondBotFolded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    this.hasSecondBotFolded = true;
                }

                if (B2Fturn || !this.isSecondBotTurn)
                {
                    await CheckRaise(2, 2);
                    this.isThirdBotTurn = true;
                }

                if (!B3Fturn)
                {
                    if (this.isThirdBotTurn)
                    {
                        FixCall(thirdBotStatus, ref this.thirdBotCall, ref this.thirdBotRaise, 1);
                        FixCall(thirdBotStatus, ref this.thirdBotCall, ref this.thirdBotRaise, 2);
                        Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, B3Fturn);
                        MessageBox.Show("Bot 3's turn");
                        AI(6, 7, ref this.thirdBotChips, ref this.isThirdBotTurn, ref B3Fturn, thirdBotStatus, 2, this.thirdBotPower, this.thirdBotType);
                        turnCount++;
                        last = 3;
                        this.isThirdBotTurn = false;
                        this.isFourthBotTurn = true;
                    }
                }

                if (B3Fturn && !this.hasThirdBotFolded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    this.hasThirdBotFolded = true;
                }

                if (B3Fturn || !this.isThirdBotTurn)
                {
                    await CheckRaise(3, 3);
                    this.isFourthBotTurn = true;
                }

                if (!B4Fturn)
                {
                    if (this.isFourthBotTurn)
                    {
                        FixCall(fourthBotStatus, ref this.fourthBotCall, ref this.fourthBotRaise, 1);
                        FixCall(fourthBotStatus, ref this.fourthBotCall, ref this.fourthBotRaise, 2);
                        Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, B4Fturn);
                        MessageBox.Show("Bot 4's turn");
                        AI(8, 9, ref this.fourthBotChips, ref this.isFourthBotTurn, ref B4Fturn, fourthBotStatus, 3, this.fourthBotPower, this.fourthBotType);
                        turnCount++;
                        last = 4;
                        this.isFourthBotTurn = false;
                        this.isFifthBotTurn = true;
                    }
                }

                if (B4Fturn && !this.hasFourthBotFolded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    this.hasFourthBotFolded = true;
                }

                if (B4Fturn || !this.isFourthBotTurn)
                {
                    await CheckRaise(4, 4);
                    this.isFifthBotTurn = true;
                }

                if (!B5Fturn)
                {
                    if (this.isFifthBotTurn)
                    {
                        FixCall(fifthBotStatus, ref this.fifthBotCall, ref this.fifthBotRaise, 1);
                        FixCall(fifthBotStatus, ref this.fifthBotCall, ref this.fifthBotRaise, 2);
                        Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, B5Fturn);
                        MessageBox.Show("Bot 5's turn");
                        AI(10, 11, ref this.fifthBotChips, ref this.isFifthBotTurn, ref B5Fturn, fifthBotStatus, 4, this.fifthBotPower, this.fifthBotType);
                        turnCount++;
                        last = 5;
                        this.isFifthBotTurn = false;
                    }
                }

                if (B5Fturn && !this.hasFifthBotFolded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    this.hasFifthBotFolded = true;
                }

                if (B5Fturn || !this.isFifthBotTurn)
                {
                    await CheckRaise(5, 5);
                    Pturn = true;
                }

                if (PFturn && !this.hasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }

                #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        } //elica: end of method turns

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)        //elica: Empty if statement
            {
            }

            if (!foldedTurn || c1 == 0 && c2 == 1 && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] Straight1 = new int[5];      // cards on the table
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

                for (int card = 0; card < 16; card++)
                {
                    if (this.availableCardsInGame[card] == int.Parse(this.cardsHolder[c1].Tag.ToString()) &&
                        this.availableCardsInGame[card + 1] == int.Parse(this.cardsHolder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(card, ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(card, ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(card, ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(card, ref current, ref Power, ref vf, Straight1);
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
                        rHighCard(card, ref current, ref Power);
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
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
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
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                    Power = type;
                }
            }
        }

        private void rFlush(int card, ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[card + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[card] / 4 < f1.Max() / 4 &&
                            this.availableCardsInGame[card + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
                        this.availableCardsInGame[card + 1] % 4 == f1[0] % 4)
                    {
                        if (this.availableCardsInGame[card + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 5)
                {
                    if (this.availableCardsInGame[card] % 4 == f1[0] % 4 &&
                        this.availableCardsInGame[card] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[card + 1] % 4 == f1[0] % 4 &&
                        this.availableCardsInGame[card + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[card] / 4 < f1.Min() / 4 &&
                        this.availableCardsInGame[card + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[card + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[card] / 4 < f2.Max() / 4 &&
                            this.availableCardsInGame[card + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
                        this.availableCardsInGame[card + 1] % 4 == f2[0] % 4)
                    {
                        if (this.availableCardsInGame[card + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 5)
                {
                    if (this.availableCardsInGame[card] % 4 == f2[0] % 4 &&
                        this.availableCardsInGame[card] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[card + 1] % 4 == f2[0] % 4 &&
                        this.availableCardsInGame[card + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[card] / 4 < f2.Min() / 4 &&
                        this.availableCardsInGame[card + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[card + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[card] / 4 < f3.Max() / 4 &&
                            this.availableCardsInGame[card + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
                        this.availableCardsInGame[card + 1] % 4 == f3[0] % 4)
                    {
                        if (this.availableCardsInGame[card + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 5)
                {
                    if (this.availableCardsInGame[card] % 4 == f3[0] % 4 &&
                        this.availableCardsInGame[card] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[card + 1] % 4 == f3[0] % 4 &&
                        this.availableCardsInGame[card + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[card] / 4 < f3.Min() / 4 &&
                        this.availableCardsInGame[card + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (this.availableCardsInGame[card + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.availableCardsInGame[card] / 4 < f4.Max() / 4 &&
                            this.availableCardsInGame[card] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 4)//different cards in hand
                {
                    if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
                        this.availableCardsInGame[card] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[card] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
                        this.availableCardsInGame[card + 1] % 4 == f4[0] % 4)
                    {
                        if (this.availableCardsInGame[card + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.availableCardsInGame[card + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 5)
                {
                    if (this.availableCardsInGame[card] % 4 == f4[0] % 4 &&
                        this.availableCardsInGame[card] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.availableCardsInGame[card + 1] % 4 == f4[0] % 4 &&
                        this.availableCardsInGame[card + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.availableCardsInGame[card + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.availableCardsInGame[card] / 4 < f4.Min() / 4 &&
                        this.availableCardsInGame[card + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (this.availableCardsInGame[card] / 4 == 0 &&
                        this.availableCardsInGame[card] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.availableCardsInGame[card + 1] / 4 == 0 &&
                        this.availableCardsInGame[card + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f2.Length > 0)
                {
                    if (this.availableCardsInGame[card] / 4 == 0 &&
                        this.availableCardsInGame[card] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.availableCardsInGame[card + 1] / 4 == 0 &&
                        this.availableCardsInGame[card + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f3.Length > 0)
                {
                    if (this.availableCardsInGame[card] / 4 == 0 &&
                        this.availableCardsInGame[card] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.availableCardsInGame[card + 1] / 4 == 0 &&
                        this.availableCardsInGame[card + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f4.Length > 0)
                {
                    if (this.availableCardsInGame[card] / 4 == 0 &&
                        this.availableCardsInGame[card] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.availableCardsInGame[card + 1] / 4 == 0 &&
                        this.availableCardsInGame[card + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(int card, ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (this.availableCardsInGame[card] / 4 != this.availableCardsInGame[card + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if ((this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc] / 4 &&
                                    this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc - k] / 4) ||
                                    (this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc] / 4 &&
                                    this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc - k] / 4))
                                {
                                    if (!msgbox)
                                    {
                                        if (this.availableCardsInGame[card] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.availableCardsInGame[card + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (this.availableCardsInGame[card + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.availableCardsInGame[card] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (this.availableCardsInGame[card + 1] / 4 != 0 && this.availableCardsInGame[card] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[card] / 4) * 2 + (this.availableCardsInGame[card + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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

        private void rPairTwoPair(int card, ref double current, ref double Power)
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
                                if (this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[card] / 4 &&
                                    this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[card + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.availableCardsInGame[card + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[card] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[card] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[card + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[card + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[card + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (this.availableCardsInGame[card] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[card] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win
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
                                        if (this.availableCardsInGame[card] / 4 > this.availableCardsInGame[card + 1] / 4)
                                        {
                                            if (this.availableCardsInGame[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.availableCardsInGame[card] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[card] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win
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
                                                Power = 13 + this.availableCardsInGame[card + 1] + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[card + 1] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win
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

        private void rPairFromHand(int card, ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (this.availableCardsInGame[card] / 4 == this.availableCardsInGame[card + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.availableCardsInGame[card] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 1;
                            Power = (this.availableCardsInGame[card + 1] / 4) * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.availableCardsInGame[card + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.availableCardsInGame[card] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.availableCardsInGame[card + 1] / 4) * 4 + this.availableCardsInGame[card] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }
                        msgbox = true;
                    }

                    if (this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.availableCardsInGame[card] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + this.availableCardsInGame[card + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.availableCardsInGame[tc] / 4) * 4 + this.availableCardsInGame[card + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(int card, ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (this.availableCardsInGame[card] / 4 > this.availableCardsInGame[card + 1] / 4)
                {
                    current = -1;
                    Power = this.availableCardsInGame[card] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = this.availableCardsInGame[card + 1] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (this.availableCardsInGame[card] / 4 == 0 || this.availableCardsInGame[card + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                {
                    this.cardsHolder[j].Image = this.cardsImageDeck[j];
                }
                    
            }

            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);

                    //TODO if statement to switch
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
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxChips.Text = this.chips.ToString();
                        //playerPanel.Visible = true;

                    }

                    if (CheckWinners.Contains("Bot 1"))
                    {
                        this.firstBotChips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxBotChips1.Text = this.firstBotChips.ToString();
                        //firstBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 2"))
                    {
                        this.secondBotChips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxBotChips2.Text = this.secondBotChips.ToString();
                        //secondBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 3"))
                    {
                        this.thirdBotChips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxBotChips3.Text = this.thirdBotChips.ToString();
                        //thirdBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 4"))
                    {
                        this.fourthBotChips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxBotChips4.Text = this.fourthBotChips.ToString();
                        //fourthBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 5"))
                    {
                        this.fifthBotChips += int.Parse(this.textBoxPot.Text) / winners;
                        this.textBoxBotChips5.Text = this.fifthBotChips.ToString();
                        //fifthBotPanel.Visible = true;
                    }
                    //await Finish(1);
                }

                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //playerPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 1"))
                    {
                        this.firstBotChips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //firstBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 2"))
                    {
                        this.secondBotChips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //secondBotPanel.Visible = true;

                    }

                    if (CheckWinners.Contains("Bot 3"))
                    {
                        this.thirdBotChips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //thirdBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 4"))
                    {
                        this.fourthBotChips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //fourthBotPanel.Visible = true;
                    }

                    if (CheckWinners.Contains("Bot 5"))
                    {
                        this.fifthBotChips += int.Parse(this.textBoxPot.Text);
                        //await Finish(1);
                        //fifthBotPanel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        this.raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!PFturn)
                        {
                            playerStatus.Text = "";
                        }

                        if (!B1Fturn)
                        {
                            firstBotStatus.Text = "";
                        }

                        if (!B2Fturn)
                        {
                            secondBotStatus.Text = "";
                        }

                        if (!B3Fturn)
                        {
                            thirdBotStatus.Text = "";
                        }

                        if (!B4Fturn)
                        {
                            fourthBotStatus.Text = "";
                        }

                        if (!B5Fturn)
                        {
                            fifthBotStatus.Text = "";
                        }
                    }
                }
            }

            if (rounds == this.flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
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
                    }
                }
            }

            if (rounds == this.turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
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
                    }
                }
            }

            if (rounds == this.river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.cardsHolder[j].Image != this.cardsImageDeck[j])
                    {
                        this.cardsHolder[j].Image = this.cardsImageDeck[j];
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
                    }
                }
            }

            if (rounds == this.end && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, PFturn);
                }

                if (!this.firstBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, B1Fturn);
                }

                if (!this.secondBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, B2Fturn);
                }

                if (!this.thirdBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, B3Fturn);
                }

                if (!this.fourthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, B4Fturn);
                }

                if (!this.fifthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, B5Fturn);
                }

                Winner(this.playerType, this.playerPower, "Player", this.chips, fixedLast);
                Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
                Winner(this.secondBotType, this.secondBotPower, "Bot 2", this.secondBotChips, fixedLast);
                Winner(this.thirdBotType, this.thirdBotPower, "Bot 3", this.thirdBotChips, fixedLast);
                Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
                Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
                restart = true;
                Pturn = true;
                PFturn = false;
                B1Fturn = false;
                B2Fturn = false;
                B3Fturn = false;
                B4Fturn = false;
                B5Fturn = false;

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
                        PFturn = false;
                        Pturn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = "raise";
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

                last = 0;
                call = this.defaultBigBlind;
                this.raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                type = 0;

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

                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;

                for (int os = 0; os < 17; os++)
                {
                    this.cardsHolder[os].Image = null;
                    this.cardsHolder[os].Invalidate();
                    this.cardsHolder[os].Visible = false;
                }

                this.textBoxPot.Text = "0";
                playerStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }  

        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
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
                        call = Convert.ToInt32(this.raise) - cRaise;
                    }

                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }

                    if (cRaise == this.raise && this.raise > 0)
                    {
                        call = 0;
                        this.buttonCall.Enabled = false;
                        this.buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            if (this.chips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("raise"))
                {
                    ints.Add(this.chips);
                    intsadded = true;
                }

                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(this.chips);
                    intsadded = true;
                }
            }

            intsadded = false;
            if (this.firstBotChips <= 0 && !B1Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(this.firstBotChips);
                    intsadded = true;
                }
                intsadded = false;
            }

            if (this.secondBotChips <= 0 && !B2Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(this.secondBotChips);
                    intsadded = true;
                }
                intsadded = false;
            }

            if (this.thirdBotChips <= 0 && !B3Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(this.thirdBotChips);
                    intsadded = true;
                }
                intsadded = false;
            }

            if (this.fourthBotChips <= 0 && !B4Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(this.fourthBotChips);
                    intsadded = true;
                }
                intsadded = false;
            }

            if (this.fifthBotChips <= 0 && !B5Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(this.fifthBotChips);
                    intsadded = true;
                }
            }

            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);

                // TODO if statement to switch
                if (index == 0)
                {
                    this.chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.chips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    this.firstBotChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.firstBotChips.ToString();
                    this.firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    this.secondBotChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.secondBotChips.ToString();
                    this.secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    this.thirdBotChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.thirdBotChips.ToString();
                    this.thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    this.fourthBotChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fourthBotChips.ToString();
                    this.fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    this.fifthBotChips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fifthBotChips.ToString();
                    this.fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    this.cardsHolder[j].Visible = false;
                }
                await Finish(1);
            }

            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= this.end)
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

            call = this.defaultBigBlind;
            this.raise = 0;
            foldedPlayers = 5;
            type = 0;
            rounds = 0;

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

            B1Fturn = false;
            B2Fturn = false;
            B3Fturn = false;
            B4Fturn = false;
            B5Fturn = false;

            this.hasPlayerFolded = false;
            this.hasFirstBotFolded = false;
            this.hasSecondBotFolded = false;
            this.hasThirdBotFolded = false;
            this.hasFourthBotFolded = false;
            this.hasFifthBotFolded = false;

            PFturn = false;
            Pturn = true;
            restart = false;
            raising = false;

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

            height = 0;
            width = 0;
            winners = 0;
            this.flop = 1;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            maxLeft = 6;
            last = 123; raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            this.textBoxPot.Text = "0";
            t = 60; up = 10000000; turnCount = 0;
            playerStatus.Text = "";
            this.firstBotStatus.Text = "";
            this.secondBotStatus.Text = "";
            this.thirdBotStatus.Text = "";
            this.fourthBotStatus.Text = "";
            this.fifthBotStatus.Text = "";
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
                    PFturn = false;
                    Pturn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "raise";
                }
            }

            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
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

            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, PFturn);
            }

            if (!this.firstBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", ref this.firstBotType, ref this.firstBotPower, B1Fturn);
            }

            if (!this.secondBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", ref this.secondBotType, ref this.secondBotPower, B2Fturn);
            }

            if (!this.thirdBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", ref this.thirdBotType, ref this.thirdBotPower, B3Fturn);
            }

            if (!this.fourthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", ref this.fourthBotType, ref this.fourthBotPower, B4Fturn);
            }

            if (!this.fifthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", ref this.fifthBotType, ref this.fifthBotPower, B5Fturn);
            }

            this.Winner(this.playerType, this.playerPower, "Player", this.chips, fixedLast);
            this.Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
            this.Winner(this.secondBotType, this.secondBotPower, "Bot 2", this.secondBotChips, fixedLast);
            this.Winner(this.thirdBotType, this.thirdBotPower, "Bot 3", this.thirdBotChips, fixedLast);
            this.Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
            this.Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
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
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "raise " + this.raise;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
            call = Convert.ToInt32(this.raise);
            raising = true;
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
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }

            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
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
                    if (call <= RoundN(sChips, n1))
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
                    this.raise = call * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(sChips, n))
                    {
                        this.raise = call * 2;
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
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }

                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (this.raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
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
                                this.raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }

            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (this.raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
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
                                this.raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }

                if (call <= 0)
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
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + botChips).ToString();
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
                        this.raise = call * 2;
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
            if (pbTimer.Value <= 0)
            {
                PFturn = true;
                await Turns();
            }

            if (t > 0)
            {
                t--;
                pbTimer.Value = (t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (this.chips <= 0)
            {
                this.textBoxChips.Text = "chips : 0";
            }

            if (this.firstBotChips <= 0)
            {
                this.textBoxBotChips1.Text = "chips : 0";
            }

            if (this.secondBotChips <= 0)
            {
                this.textBoxBotChips2.Text = "chips : 0";
            }

            if (this.thirdBotChips <= 0)
            {
                this.textBoxBotChips3.Text = "chips : 0";
            }

            if (this.fourthBotChips <= 0)
            {
                this.textBoxBotChips4.Text = "chips : 0";
            }

            if (this.fifthBotChips <= 0)
            {
                this.textBoxBotChips5.Text = "chips : 0";
            }

            this.textBoxChips.Text = "chips : " + this.chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBotChips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondBotChips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdBotChips.ToString();
            this.textBoxBotChips4.Text = "chips : " + this.fourthBotChips.ToString();
            this.textBoxBotChips5.Text = "chips : " + this.fifthBotChips.ToString();

            if (this.chips <= 0)
            {
                Pturn = false;
                PFturn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (up > 0)
            {
                up--;
            }

            if (this.chips >= call)
            {
                this.buttonCall.Text = "Call " + call.ToString();
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (call > 0)
            {
                this.buttonCheck.Enabled = false;
            }
            else           //elica: change  if (call <= 0) with else
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            if (this.chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.chips <= int.Parse(this.textBoxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "raise";
                }
            }

            if (this.chips < call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void buttonFold_IsClicked(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            Pturn = false;
            PFturn = true;
            await Turns();
        }

        private async void buttonCheck_IsClicked(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                Pturn = false;
                playerStatus.Text = "Check";
            }
            else
            {
                //playerStatus.Text = "All in " + chips;

                this.buttonCheck.Enabled = false;
            }
            await Turns();
        }

        private async void buttonCall_IsClicked(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, PFturn);
            if (this.chips >= call)
            {
                this.chips -= call;
                this.textBoxChips.Text = "chips : " + this.chips.ToString();

                if (this.textBoxPot.Text != "")
                {
                    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + call).ToString();
                }
                else
                {
                    this.textBoxPot.Text = call.ToString();
                }

                Pturn = false;
                playerStatus.Text = "Call " + call;
                this.playerCall = call;
            }
            else if (this.chips <= call && call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.chips).ToString();
                playerStatus.Text = "All in " + this.chips;
                this.chips = 0;
                this.textBoxChips.Text = "chips : " + this.chips.ToString();
                Pturn = false;
                this.buttonFold.Enabled = false;
                this.playerCall = this.chips;
            }
            await Turns();
        }

        private async void buttonRaise_IsClicked(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, PFturn);
            int parsedValue;
            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.chips > call)
                {
                    if (this.raise * 2 > int.Parse(this.textBoxRaise.Text))
                    {
                        this.textBoxRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.chips >= int.Parse(this.textBoxRaise.Text))
                        {
                            call = int.Parse(this.textBoxRaise.Text);
                            this.raise = int.Parse(this.textBoxRaise.Text);
                            playerStatus.Text = "raise " + call.ToString();
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + call).ToString();
                            this.buttonCall.Text = "Call";
                            this.chips -= int.Parse(this.textBoxRaise.Text);
                            raising = true;
                            last = 0;
                            this.playerRaise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            call = this.chips;
                            this.raise = this.chips;
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.chips).ToString();
                            playerStatus.Text = "raise " + call.ToString();
                            this.chips = 0;
                            raising = true;
                            last = 0;
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
            Pturn = false;
            await Turns();
        }

        private void buttonAddChips_IsClicked(object sender, EventArgs e)
        {
            if (tbAdd.Text == "")
            {
                //elica Empty if statement
            }
            else
            {
                this.chips += int.Parse(tbAdd.Text);
                this.firstBotChips += int.Parse(tbAdd.Text);
                this.secondBotChips += int.Parse(tbAdd.Text);
                this.thirdBotChips += int.Parse(tbAdd.Text);
                this.fourthBotChips += int.Parse(tbAdd.Text);
                this.fifthBotChips += int.Parse(tbAdd.Text);
            }

            this.textBoxChips.Text = "chips : " + this.chips.ToString();
        }

        private void buttonOptions_IsClicked(object sender, EventArgs e)
        {
            this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();

            if (this.textBoxBigBlind.Visible == false)
            {
                this.textBoxBigBlind.Visible = true;
                this.textBoxSmallBlind.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttonSmallBlind.Visible = true;
            }
            else
            {
                this.textBoxBigBlind.Visible = false;
                this.textBoxSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttonSmallBlind.Visible = false;
            }
        }

        private void buttonSmallBlind_IsClicked(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxSmallBlind.Text.Contains(",") || this.textBoxSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.textBoxSmallBlind.Text) >= 250 && int.Parse(this.textBoxSmallBlind.Text) <= 100000)
            {
                this.defaultSmallBlind = int.Parse(this.textBoxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void buttonBigBlind_IsClicked(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBigBlind.Text.Contains(",") || this.textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (!int.TryParse(this.textBoxBigBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.textBoxBigBlind.Text) >= 500 && int.Parse(this.textBoxBigBlind.Text) <= 200000)
            {
                this.defaultBigBlind = int.Parse(this.textBoxBigBlind.Text);
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