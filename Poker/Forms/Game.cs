namespace Poker.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Core;
    using GlobalConstants;
    using Interfaces;
    using Models.Card;
    using Models.Player;
    using Models.PokerManagement;
    using Type = Type;

    public partial class Game : Form
    {
        //ProgressBar asd = new ProgressBar(); //elica: It is never used!!!
        //public int Nm;
        private readonly IPlayer humanPlayer = new Human(GlobalConstants.HumanPlayerName);
        private readonly IPlayer firstBot = new Bot(GlobalConstants.FirstBotPlayerName);
        private readonly IPlayer secondbot = new Bot(GlobalConstants.SecondbotPlayerName);
        private readonly IPlayer thirdbot = new Bot(GlobalConstants.ThirdbotPlayerName);
        private readonly IPlayer fourthBot = new Bot(GlobalConstants.FourthBotPlayerName);
        private readonly IPlayer fifthBot = new Bot(GlobalConstants.FifthBotPlayerName);

        private readonly Database db = Database.Instace;
        private readonly PokerTable table;
        private  Rules rules = Rules.Instance;
       // private readonly CardHandler cardHandler = new CardHandler();

        //private const int InitialValueOfChips = 10000;
        private const int DefaultValueOfBigBlind = 500;
        private const int DefaultValueOfSmallBlind = 250;
        //private Panel player.Panel = new Panel();
        //private Panel firstBot.Panel = new Panel();
        //private Panel secondbot.Panel = new Panel();
        //private Panel thirdbotPanel = new Panel();
        //private Panel fourthBotPanel = new Panel();
        //private Panel fifthBot.Panel = new Panel();
        private int call = 500;
        private int foldedPlayers = 5;
        //elica: chips
        //private int chips = InitialValueOfChips;     
        //private int firstBotChips = InitialValueOfChips;
        //private int secondbotChips = InitialValueOfChips;
        //private int thirdbotChips = InitialValueOfChips;
        //private int fourthBotChips = InitialValueOfChips;
        //private int fifthBotChips = InitialValueOfChips;

      //  private double type;
        private double rounds;
        private double raise;
        //private double firstBotPower;
        //private double secondbotPower;
        //private double thirdbotPower;
        //private double fourthBotPower;
        //private double fifthBotPower;
        //private double playerPower;
        //private double playerType = -1;
        //private double firstBotType = -1;
        //private double secondbotType = -1;
        //private double thirdbotType = -1;
        //private double fourthBotType = -1;
        //private double fifthBotType = -1;

        //private bool isFirstBotTurn;
        //private bool isSecondbotTurn;
        //private bool isThirdbotTurn;
        //private bool isFourthBotTurn;
        //private bool isFifthBotTurn;

        //private bool B1Fturn;
        //private bool B2Fturn;
        //private bool B3Fturn;
        //private bool B4Fturn;
        //private bool B5Fturn;

        //private bool hasPlayerFolded;
        //private bool hasFirstBotFolded;
        //private bool hasSecondbotFolded;
        //private bool hasThirdbotFolded;
        //private bool hasFourthBotFolded;
        //private bool hasFifthBotFolded;
        private bool intsadded;
        private bool changed;

        //private int playerCall;
        //private int firstBotCall;
        //private int secondbotCall;
        //private int thirdbotCall;
        //private int fourthBotCall;
        //private int fifthBotCall;

        //private int playerRaise;
        //private int firstBotRaise;
        //private int secondbotRaise;
        //private int thirdbotRaise;
        //private int fourthBotRaise;
        //private int fifthBotRaise;

        private int height;
        private int width;

    //    private int winners;
        private readonly int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;

        private int last = 123;
        private int raisedTurn = 1;

        private readonly List<bool?> bools = new List<bool?>();
     //   private readonly List<Type> Win = new List<Type>();
      //  private readonly List<string> CheckWinners = new List<string>();
        private readonly List<int> ints = new List<int>();

        //private bool PlayerFoldTturn;
        //private bool Playerturn = true;
        private bool restart;
        private bool raising;

      //  readonly Type sorted = new Type();
        string[] ImgLocation = Directory.GetFiles(GlobalConstants.PlayingCardsDirectoryPath,
            GlobalConstants.PlayingCardsWithPngExtension,
            SearchOption.TopDirectoryOnly);

        private readonly int[] availableCardsInGame = new int[17];
        private readonly Image[] cardsImageDeck = new Image[52];
        private readonly PictureBox[] cardsHolder = new PictureBox[52];
        private readonly Timer timer = new Timer();
        private readonly Timer Updates = new Timer();

        private int t = 60;
        private int globalShit;
        private int defaultBigBlind = DefaultValueOfBigBlind;
        private int defaultSmallBlind = DefaultValueOfSmallBlind;
        private int up = 10000000;
        private int turnCount;

        public Game()
        {
            //bools.Add(PlayerFoldTturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.db.Players[0] = this.humanPlayer;
            this.db.Players[1] = this.firstBot;
            this.db.Players[2] = this.secondbot;
            this.db.Players[3] = this.thirdbot;
            this.db.Players[4] = this.fourthBot;
            this.db.Players[5] = this.fifthBot;

            //for (int i = 0; i < this.db.Players.Length; i++)
            //{
            //    this.db.Players[i].Cards[0] = 2*i;
            //    this.db.Players[i].Cards[1] = 2*i + 1;
            //}

            this.table = new PokerTable();
            this.db.Table = this.table;
            this.call = this.defaultBigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();
            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;
            this.textBoxBotChips1.Enabled = false;
            this.textBoxBotChips2.Enabled = false;
            this.textBoxBotChips3.Enabled = false;
            this.textBoxBotChips4.Enabled = false;
            this.textBoxBotChips5.Enabled = false;

            this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips;
            this.textBoxBotChips1.Text = "chips : " + this.firstBot.Chips;
            this.textBoxBotChips2.Text = "chips : " + this.secondbot.Chips;
            this.textBoxBotChips3.Text = "chips : " + this.thirdbot.Chips;
            this.textBoxBotChips4.Text = "chips : " + this.fourthBot.Chips;
            this.textBoxBotChips5.Text = "chips : " + this.fifthBot.Chips;

            this.timer.Interval = 1 * 1 * 1000;
            this.timer.Tick += this.timer_Tick;
            this.Updates.Interval = 1 * 1 * 100;
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

        public Button ButtonCall
        { get { return this.buttonCall; } }

        public TextBox TextBoxPot
        { get { return this.textBoxPot; } }

        public double Rounds
        {
            get { return this.rounds; }
            set { this.rounds = value; }
        }

        public bool Raising
        {
            get { return this.raising; }
            set { this.raising = value; }
        }

        public double Raise
        {
            get { return this.raise; }
            set { this.raise = value; }
        }

        public Image[] CardsImageDeck
        {
            get { return this.cardsImageDeck; }
        }

        public int CallValue
        {
            get { return this.call; }
            set { this.call = value; }
        }
        public PictureBox[] CardsHolder
        {
            get { return this.cardsHolder; }
        }

        public TextBox[] ChipsTextBoxes
        {

            get
            {
                TextBox[] result =
                {
                    this.textBoxChips,
                    this.textBoxBotChips1,
                    this.textBoxBotChips2,
                    this.textBoxBotChips3,
                    this.textBoxBotChips4,
                    this.textBoxBotChips5
                };

                return result;
            }
        }

        async Task Shuffle()
        {
            //this.bools.Add(this.humanPlayerFoldTturn);
            //this.bools.Add(this.B1Fturn);
            //this.bools.Add(this.B2Fturn);
            //this.bools.Add(this.B3Fturn);
            //this.bools.Add(this.B4Fturn);
            //this.bools.Add(this.B5Fturn);
            for (int i = 0; i < this.db.Players.Length; i++)
            {
                this.bools.Add(this.db.Players[i].FoldTurn);
            }
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap(GlobalConstants.PlayingCardsBackPath);
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
                this.cardsImageDeck[cardsInGame] = Image.FromFile(this.ImgLocation[cardsInGame]);
                var charsToRemove = new string[] { GlobalConstants.PlayingCardsPath,
                    GlobalConstants.PlayingCardsExtension };

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
                    if (cardsInGame < 2)
                    {
                        if (this.cardsHolder[0].Tag != null)
                        {
                            this.cardsHolder[1].Tag = int.Parse(this.ImgLocation[1]) - 1;
                            this.humanPlayer.PlayerCards[1] = new Card((int)this.cardsHolder[1].Tag);
                            this.humanPlayer.PlayerCards[1].NumberInGame = cardsInGame;
                        }
                        else
                        {
                            this.cardsHolder[0].Tag = int.Parse(this.ImgLocation[0]) - 1;
                            this.humanPlayer.PlayerCards[0] = new Card((int)this.cardsHolder[0].Tag);
                            this.humanPlayer.PlayerCards[0].NumberInGame = cardsInGame;
                        }
                        this.cardsHolder[cardsInGame].Image = this.cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom);
                        //cardsHolder[cardsInGame].Dock = DockStyle.Top;
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.Controls.Add(this.humanPlayer.Panel);
                        this.humanPlayer.Panel.Location = new Point(this.cardsHolder[0].Left - 10, this.cardsHolder[0].Top - 10);
                        //this.humanPlayer.Panel.BackColor = Color.DarkBlue;
                        //this.humanPlayer.Panel.Height = 150;
                        //this.humanPlayer.Panel.Width = 180;
                        //this.humanPlayer.Panel.Visible = false;
                    }

             
                for (int i = 1; i < this.db.Players.Length; i++)
                {
                    if (this.db.Players[i].Chips > 0)
                    {
                        this.foldedPlayers--;

                        if (cardsInGame >= 2 * i && cardsInGame < 2 * i + 2)
                        {
                            if (this.cardsHolder[2 * i].Tag != null)
                            {
                                this.cardsHolder[2 * i + 1].Tag = int.Parse(this.ImgLocation[2 * i + 1]) - 1;
                                this.db.Players[i].PlayerCards[1] = new Card((int)this.cardsHolder[2 * i + 1].Tag);
                                this.db.Players[i].PlayerCards[1].NumberInGame = cardsInGame;
                            }
                            else
                            {
                                this.cardsHolder[2 * i].Tag = int.Parse(this.ImgLocation[2 * i]) - 1;
                                this.db.Players[i].PlayerCards[0] = new Card((int)this.cardsHolder[2 * i].Tag);
                                this.db.Players[i].PlayerCards[0].NumberInGame = cardsInGame;
                            }
                            if (!check)
                            {
                                switch (i)
                                {
                                    case 1:
                                        horizontal = 15;
                                        vertical = 420;

                                        break;
                                    case 2:
                                        horizontal = 75;
                                        vertical = 65;

                                        break;
                                    case 3:
                                        horizontal = 590;
                                        vertical = 25;

                                        break;
                                    case 4:
                                        horizontal = 1115;
                                        vertical = 65;

                                        break;

                                    case 5:
                                        horizontal = 1160;
                                        vertical = 420;

                                        break;
                                }
                            }
                            check = true;
                            this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                            this.cardsHolder[cardsInGame].Image = backImage;
                            //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                            this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                            horizontal += this.cardsHolder[cardsInGame].Width;
                            this.cardsHolder[cardsInGame].Visible = true;
                            this.Controls.Add(this.db.Players[i].Panel);
                            this.db.Players[i].Panel.Location = new Point(this.cardsHolder[2 * i].Left - 10, this.cardsHolder[2 * i].Top - 10);

                            if (cardsInGame == 2 * i + 1)
                            {
                                check = false;
                            }
                        }
                    }
                }


                // TODO if statement to switch
                
                if (cardsInGame >= 12)    // elica: cards on the table with index 12, 13, 14, 15, 16
                {
                  

                    this.cardsHolder[12].Tag = int.Parse(this.ImgLocation[12]) - 1;
                    this.table.CardsOnTable[0] = new Card((int)this.cardsHolder[12].Tag);
                    this.table.CardsOnTable[0].NumberInGame = cardsInGame;
                    if (cardsInGame == 13)
                    {
                        this.cardsHolder[13].Tag = int.Parse(this.ImgLocation[13]) - 1;
                        this.table.CardsOnTable[1] = new Card((int)this.cardsHolder[13].Tag);
                        this.table.CardsOnTable[1].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame ==14)
                    {
                        this.cardsHolder[14].Tag = int.Parse(this.ImgLocation[14]) - 1;
                        this.table.CardsOnTable[2] = new Card((int)this.cardsHolder[14].Tag);
                        this.table.CardsOnTable[2].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame ==15)
                    {
                        this.cardsHolder[15].Tag = int.Parse(this.ImgLocation[15]) - 1;
                        this.table.CardsOnTable[3] = new Card((int)this.cardsHolder[15].Tag);
                        this.table.CardsOnTable[3].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame ==16)
                    {
                        this.cardsHolder[16].Tag = int.Parse(this.ImgLocation[16]) - 1;
                        this.table.CardsOnTable[4] = new Card((int)this.cardsHolder[16].Tag);
                        this.table.CardsOnTable[4].NumberInGame = cardsInGame;
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
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion

                //for (int i = 1; i <this.db.Players.Length; i++)
                //{
                //    if (db.Players[i].Chips<=0 )
                //    {
                //        this.db.Players[i].FoldTurn = true;
                //        this.cardsHolder[i*2].Visible = false;
                //        this.cardsHolder[(i * 2)+1].Visible = false;
                //    }
                //}
                for (int i = 1; i < this.db.Players.Length; i++)
                {
                    if (this.db.Players[i].Chips <= 0)
                    {
                        this.db.Players[i].FoldTurn = true;
                        this.cardsHolder[2 * i].Visible = false;
                        this.cardsHolder[2 * i + 1].Visible = false;
                    }
                }
                //if (this.firstBotChips <= 0)
                //{
                //    this.B1Fturn = true;
                //    this.cardsHolder[2].Visible = false;
                //    this.cardsHolder[3].Visible = false;
                //}

                //// Valentin:it is unnecessary
                ////else
                ////{
                ////    B1Fturn = false;
                ////    if (cardsInGame == 3)
                ////    {
                ////        if (this.cardsHolder[3] != null)
                ////        {
                ////            this.cardsHolder[2].Visible = true;
                ////            this.cardsHolder[3].Visible = true;
                ////        }
                ////    }
                ////}

                //if (this.secondbotChips <= 0)
                //{
                //    this.B2Fturn = true;
                //    this.cardsHolder[4].Visible = false;
                //    this.cardsHolder[5].Visible = false;
                //}
                //// Valentin:it is unnecessary
                ////else
                ////{
                ////    B2Fturn = false;
                ////    if (cardsInGame == 5)
                ////    {
                ////        if (this.cardsHolder[5] != null)
                ////        {
                ////            this.cardsHolder[4].Visible = true;
                ////            this.cardsHolder[5].Visible = true;
                ////        }
                ////    }
                ////}

                //if (this.thirdbotChips <= 0)
                //{
                //    this.B3Fturn = true;
                //    this.cardsHolder[6].Visible = false;
                //    this.cardsHolder[7].Visible = false;
                //}
                //// Valentin:it is unnecessary
                ////else
                ////{
                ////    B3Fturn = false;
                ////    if (cardsInGame == 7)
                ////    {
                ////        if (this.cardsHolder[7] != null)
                ////        {
                ////            this.cardsHolder[6].Visible = true;
                ////            this.cardsHolder[7].Visible = true;
                ////        }
                ////    }
                ////}

                //if (this.fourthBotChips <= 0)
                //{
                //    this.B4Fturn = true;
                //    this.cardsHolder[8].Visible = false;
                //    this.cardsHolder[9].Visible = false;
                //}
                //// Valentin:it is unnecessary
                ////else
                ////{
                ////    B4Fturn = false;
                ////    if (cardsInGame == 9)
                ////    {
                ////        if (this.cardsHolder[9] != null)
                ////        {
                ////            this.cardsHolder[8].Visible = true;
                ////            this.cardsHolder[9].Visible = true;
                ////        }
                ////    }
                ////}

                //if (this.fifthBotChips <= 0)
                //{
                //    this.B5Fturn = true;
                //    this.cardsHolder[10].Visible = false;
                //    this.cardsHolder[11].Visible = false;
                //}
                // Valentin:it is unnecessary
                //else
                //{
                //    B5Fturn = false;

                //    if (cardsInGame == 11)
                //    {
                //        if (this.cardsHolder[11] != null)
                //        {
                //            this.cardsHolder[10].Visible = true;
                //            this.cardsHolder[11].Visible = true;
                //        }
                //    }
                //}

                if (cardsInGame == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            }  //elica: end of the second loop

            if (this.foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show(GlobalConstants.PlayAgainMessage,
                    GlobalConstants.WinMessage,
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
            //else
            //{
            //    this.foldedPlayers = 5;
            //}

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
            if (!this.humanPlayer.FoldTurn)
            {
                if (this.humanPlayer.IsPlayerTurn)
                {
                    this.FixCall(this.humanPlayer, 1);
                    MessageBox.Show(GlobalConstants.YourTurnMessage);
                    this.pokerBetTimer.Visible = true;
                    this.pokerBetTimer.Value = 1000;
                    this.t = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.humanPlayer, 2);
                }
            }

            if (this.humanPlayer.FoldTurn || !this.humanPlayer.IsPlayerTurn)
            {
                await this.AllIn();
                if (this.humanPlayer.FoldTurn && !this.humanPlayer.HasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains(GlobalConstants.AllInMessage) == false ||
                        this.buttonRaise.Text.Contains(GlobalConstants.AllInMessage) == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.humanPlayer.HasPlayerFolded = true;
                    }
                }

                await this.CheckRaise(0);
                this.pokerBetTimer.Visible = false;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.timer.Stop();
                this.firstBot.IsPlayerTurn = true;

                if (!this.firstBot.FoldTurn)
                {
                    if (this.firstBot.IsPlayerTurn)
                    {
                        this.FixCall(this.firstBot, 1);
                        this.FixCall(this.firstBot, 2);
                        //Rules(this.firstBot);
                        this.rules.CheckForHand(this.firstBot);
                        MessageBox.Show(this.firstBot.Name);
                        AiMenager.CheckHand(this.firstBot);
                        this.turnCount++;
                        this.last = 1;
                        this.firstBot.IsPlayerTurn = false;
                        this.secondbot.IsPlayerTurn = true;
                    }
                }

                if (this.firstBot.FoldTurn && !this.firstBot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.firstBot.HasPlayerFolded = true;
                }

                if (this.firstBot.FoldTurn || !this.firstBot.IsPlayerTurn)
                {
                    await this.CheckRaise(1);
                    this.secondbot.IsPlayerTurn = true;
                }

                if (!this.secondbot.FoldTurn)
                {
                    if (this.secondbot.IsPlayerTurn)
                    {
                        this.FixCall(this.secondbot, 1);
                        this.FixCall(this.secondbot, 2);
                        //Rules(this.secondbot);
                        this.rules.CheckForHand(this.secondbot);
                        MessageBox.Show(this.secondbot.Name);
                        AiMenager.CheckHand(this.secondbot);
                        this.turnCount++;
                        this.last = 2;
                        this.secondbot.IsPlayerTurn = false;
                        this.thirdbot.IsPlayerTurn = true;
                    }
                }

                if (this.secondbot.FoldTurn && !this.secondbot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.secondbot.HasPlayerFolded = true;
                }

                if (this.secondbot.FoldTurn || !this.secondbot.IsPlayerTurn)
                {
                    await this.CheckRaise(2);
                    this.thirdbot.IsPlayerTurn = true;
                }

                if (!this.thirdbot.FoldTurn)
                {
                    if (this.thirdbot.IsPlayerTurn)
                    {
                        this.FixCall(this.thirdbot, 1);
                        this.FixCall(this.thirdbot, 2);
                        //Rules(this.thirdbot);
                        this.rules.CheckForHand(this.thirdbot);
                        MessageBox.Show(this.thirdbot.Name);
                        AiMenager.CheckHand(this.thirdbot);
                        this.turnCount++;
                        this.last = 3;
                        this.thirdbot.IsPlayerTurn = false;
                        this.fourthBot.IsPlayerTurn = true;
                    }
                }

                if (this.thirdbot.FoldTurn && !this.thirdbot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.thirdbot.HasPlayerFolded = true;
                }

                if (this.thirdbot.FoldTurn || !this.thirdbot.IsPlayerTurn)
                {
                    await this.CheckRaise(3);
                    this.fourthBot.IsPlayerTurn = true;
                }

                if (!this.fourthBot.FoldTurn)
                {
                    if (this.fourthBot.IsPlayerTurn)
                    {
                        this.FixCall(this.fourthBot, 1);
                        this.FixCall(this.fourthBot, 2);
                        //Rules(this.fourthBot);
                        this.rules.CheckForHand(this.fourthBot);
                        MessageBox.Show(this.fourthBot.Name);
                        AiMenager.CheckHand(this.fourthBot);
                        this.turnCount++;
                        this.last = 4;
                        this.fourthBot.IsPlayerTurn = false;
                        this.fifthBot.IsPlayerTurn = true;
                    }
                }

                if (this.fourthBot.FoldTurn && !this.fourthBot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(4);
                    this.bools.Insert(4, null);
                    this.maxLeft--;
                    this.fourthBot.HasPlayerFolded = true;
                }

                if (this.fourthBot.FoldTurn || !this.fourthBot.IsPlayerTurn)
                {
                    await this.CheckRaise(4);
                    this.fifthBot.IsPlayerTurn = true;
                }

                if (!this.fifthBot.FoldTurn)
                {
                    if (this.fifthBot.IsPlayerTurn)
                    {
                        this.FixCall(this.fifthBot, 1);
                        this.FixCall(this.fifthBot, 2);
                        //Rules(this.fifthBot);
                        this.rules.CheckForHand(this.fifthBot);
                        MessageBox.Show(this.fifthBot.Name);
                        AiMenager.CheckHand(this.fifthBot);
                        this.turnCount++;
                        this.last = 5;
                        this.fifthBot.IsPlayerTurn = false;
                    }
                }

                if (this.fifthBot.FoldTurn && !this.fifthBot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(5);
                    this.bools.Insert(5, null);
                    this.maxLeft--;
                    this.fifthBot.HasPlayerFolded = true;
                }

                if (this.fifthBot.FoldTurn || !this.fifthBot.IsPlayerTurn)
                {
                    await this.CheckRaise(5);
                    this.humanPlayer.IsPlayerTurn = true;
                }

                if (this.humanPlayer.FoldTurn && !this.humanPlayer.HasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains(GlobalConstants.AllInMessage) == false ||
                        this.buttonRaise.Text.Contains(GlobalConstants.AllInMessage) == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.humanPlayer.HasPlayerFolded = true;
                    }
                }

                #endregion
                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }
                this.restart = false;
            }
        } //elica: end of method turns

        //void Rules(Player player)
        //{
        //    //if (player.Cards[1] == 0 && player.Cards[2] == 1)        //elica: Empty if statement
        //    //{
        //    //}

        //    if (!player.FoldTurn || player.Cards[1] == 0 && player.Cards[2] == 1 && this.humanPlayer.Status.Text.Contains("Fold") == false)
        //    {
        //        #region Variables

        //        bool done = false;
        //        bool vf = false;
        //        int[] cardsOnTable = new int[5];      // cards on the table
        //        int[] Straight = new int[7];
        //        Straight[0] = this.availableCardsInGame[player.Cards[0]];
        //        Straight[1] = this.availableCardsInGame[player.Cards[1]];
        //        cardsOnTable[0] = Straight[2] = this.availableCardsInGame[12];
        //        cardsOnTable[1] = Straight[3] = this.availableCardsInGame[13];
        //        cardsOnTable[2] = Straight[4] = this.availableCardsInGame[14];
        //        cardsOnTable[3] = Straight[5] = this.availableCardsInGame[15];
        //        cardsOnTable[4] = Straight[6] = this.availableCardsInGame[16];
        //        var clubs = Straight.Where(o => o % 4 == 0).ToArray();         //  clubs
        //        var diamonds = Straight.Where(o => o % 4 == 1).ToArray();     //  diamonds
        //        var hearts = Straight.Where(o => o % 4 == 2).ToArray();       //  hearts
        //        var spades = Straight.Where(o => o % 4 == 3).ToArray();        //  spades
        //        var st1 = clubs.Select(o => o / 4).Distinct().ToArray();
        //        var st2 = diamonds.Select(o => o / 4).Distinct().ToArray();
        //        var st3 = hearts.Select(o => o / 4).Distinct().ToArray();
        //        var st4 = spades.Select(o => o / 4).Distinct().ToArray();
        //        Array.Sort(Straight);
        //        Array.Sort(st1);
        //        Array.Sort(st2);
        //        Array.Sort(st3);
        //        Array.Sort(st4);
        //        #endregion

        //        for (int card = 0; card < 16; card++)
        //        {
        //            if (this.availableCardsInGame[card] == int.Parse(this.cardsHolder[player.Cards[0]].Tag.ToString()) &&
        //                this.availableCardsInGame[card + 1] == int.Parse(this.cardsHolder[player.Cards[1]].Tag.ToString()))
        //            {
        //                //Pair from Hand current = 1

        //                rPairFromHand(card,  player);

        //                #region Pair or Two Pair from Table current = 2 || 0
        //                rPairTwoPair(card, player);
        //                #endregion

        //                #region Two Pair current = 2
        //                rTwoPair(card, player);
        //                #endregion

        //                #region Three of a kind current = 3
        //                rThreeOfAKind(player, Straight);
        //                #endregion

        //                #region Straight current = 4
        //                rStraight(player, Straight);
        //                #endregion

        //                #region Flush current = 5 || 5.5
        //                rFlush(card, player, ref vf, cardsOnTable);
        //                #endregion

        //                #region Full House current = 6
        //                rFullHouse(player, ref done, Straight);
        //                #endregion

        //                #region Four of a Kind current = 7
        //                rFourOfAKind(player, Straight);
        //                #endregion

        //                #region Straight Flush current = 8 || 9
        //                rStraightFlush(player, st1, st2, st3, st4);
        //                #endregion

        //                #region High Card current = -1
        //                rHighCard(card, player  );
        //                #endregion
        //            }
        //        }
        //    }
        //}

        //private void rStraightFlush( Player player, int[] st1, int[] st2, int[] st3, int[] st4)
        //{
        //    if (player.Current >= -1)
        //    {
        //        if (st1.Length >= 5)
        //        {
        //            if (st1[0] + 4 == st1[4])
        //            {
        //                player.Current = 8;
        //                player.Power = (st1.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 8 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
        //            {
        //                player.Current = 9;
        //                player.Power = (st1.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 9 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (st2.Length >= 5)
        //        {
        //            if (st2[0] + 4 == st2[4])
        //            {
        //                player.Current = 8;
        //                player.Power = (st2.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 8 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
        //            {
        //                player.Current = 9;
        //                player.Power = (st2.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 9 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (st3.Length >= 5)
        //        {
        //            if (st3[0] + 4 == st3[4])
        //            {
        //                player.Current = 8;
        //                player.Power = (st3.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 8 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
        //            {
        //                player.Current = 9;
        //                player.Power = (st3.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 9 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (st4.Length >= 5)
        //        {
        //            if (st4[0] + 4 == st4[4])
        //            {
        //                player.Current = 8;
        //                player.Power = (st4.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 8 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
        //            {
        //                player.Current = 9;
        //                player.Power = (st4.Max()) / 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 9 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }
        //    }
        //}

        //private void rFourOfAKind( Player player, int[] Straight)
        //{
        //    if (player.Current >= -1)
        //    {
        //        for (int j = 0; j <= 3; j++)
        //        {
        //            if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
        //                Straight[j] / 4 == Straight[j + 3] / 4)
        //            {
        //                player.Current = 7;
        //                player.Power = (Straight[j] / 4) * 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 7 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
        //            {
        //                player.Current = 7;
        //                player.Power = 13 * 4 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 7 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }
        //    }
        //}

        //private void rFullHouse(Player player, ref bool done, int[] Straight)
        //{
        //    if (player.Current >= -1)
        //    {
        //        this.type = player.Power;
        //        for (int j = 0; j <= 12; j++)
        //        {
        //            var fh = Straight.Where(o => o / 4 == j).ToArray();
        //            if (fh.Length == 3 || done)
        //            {
        //                if (fh.Length == 2)
        //                {
        //                    if (fh.Max() / 4 == 0)
        //                    {
        //                        player.Current = 6;
        //                        player.Power = 13 * 2 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 6 });
        //                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                        break;
        //                    }

        //                    if (fh.Max() / 4 > 0)
        //                    {
        //                        player.Current = 6;
        //                        player.Power = fh.Max() / 4 * 2 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 6 });
        //                        this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                        break;
        //                    }
        //                }

        //                if (!done)
        //                {
        //                    if (fh.Max() / 4 == 0)
        //                    {
        //                        player.Power = 13;
        //                        done = true;
        //                        j = -1;
        //                    }
        //                    else
        //                    {
        //                        player.Power = fh.Max() / 4;
        //                        done = true;
        //                        j = -1;
        //                    }
        //                }
        //            }
        //        }

        //        if (player.Current != 6)
        //        {
        //            player.Power = this.type;
        //        }
        //    }
        //}

        //private void rFlush(int card, Player player, ref bool vf, int[] Straight1)
        //{
        //    if (player.Current >= -1)
        //    {
        //        //addition cardsOnDesk = Streight1, f1=clubs, f2=diamonds, f3=hearts, f4=spades
        //        var clubs = Straight1.Where(o => o % 4 == 0).ToArray();    //clubs
        //        var diamonds = Straight1.Where(o => o % 4 == 1).ToArray();     //diamonds
        //        var hearts = Straight1.Where(o => o % 4 == 2).ToArray();      //hearts
        //        var spades = Straight1.Where(o => o % 4 == 3).ToArray();      //spades
        //        if (clubs.Length == 3 || clubs.Length == 4)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == clubs[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > clubs.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }

        //                if (this.availableCardsInGame[card + 1] / 4 > clubs.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else if (this.availableCardsInGame[card] / 4 < clubs.Max() / 4 &&
        //                    this.availableCardsInGame[card + 1] / 4 < clubs.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = clubs.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (clubs.Length == 4)//different cards in hand
        //        {
        //            if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == clubs[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > clubs.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = clubs.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
        //                this.availableCardsInGame[card + 1] % 4 == clubs[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card + 1] / 4 > clubs.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = clubs.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (clubs.Length == 5)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == clubs[0] % 4 &&
        //                this.availableCardsInGame[card] / 4 > clubs.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 == clubs[0] % 4 &&
        //                this.availableCardsInGame[card + 1] / 4 > clubs.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //            else if (this.availableCardsInGame[card] / 4 < clubs.Min() / 4 &&
        //                this.availableCardsInGame[card + 1] / 4 < clubs.Min())
        //            {
        //                player.Current = 5;
        //                player.Power = clubs.Max() + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //        }

        //        if (diamonds.Length == 3 || diamonds.Length == 4)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == diamonds[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > diamonds.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }

        //                if (this.availableCardsInGame[card + 1] / 4 > diamonds.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else if (this.availableCardsInGame[card] / 4 < diamonds.Max() / 4 &&
        //                    this.availableCardsInGame[card + 1] / 4 < diamonds.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = diamonds.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (diamonds.Length == 4)//different cards in hand
        //        {
        //            if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == diamonds[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > diamonds.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = diamonds.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
        //                this.availableCardsInGame[card + 1] % 4 == diamonds[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card + 1] / 4 > diamonds.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = diamonds.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (diamonds.Length == 5)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == diamonds[0] % 4 &&
        //                this.availableCardsInGame[card] / 4 > diamonds.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 == diamonds[0] % 4 &&
        //                this.availableCardsInGame[card + 1] / 4 > diamonds.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //            else if (this.availableCardsInGame[card] / 4 < diamonds.Min() / 4 &&
        //                this.availableCardsInGame[card + 1] / 4 < diamonds.Min())
        //            {
        //                player.Current = 5;
        //                player.Power = diamonds.Max() + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //        }

        //        if (hearts.Length == 3 || hearts.Length == 4)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == hearts[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > hearts.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }

        //                if (this.availableCardsInGame[card + 1] / 4 > hearts.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else if (this.availableCardsInGame[card] / 4 < hearts.Max() / 4 &&
        //                    this.availableCardsInGame[card + 1] / 4 < hearts.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = hearts.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (hearts.Length == 4)//different cards in hand
        //        {
        //            if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == hearts[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > hearts.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = hearts.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
        //                this.availableCardsInGame[card + 1] % 4 == hearts[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card + 1] / 4 > hearts.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = hearts.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (hearts.Length == 5)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == hearts[0] % 4 &&
        //                this.availableCardsInGame[card] / 4 > hearts.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 == hearts[0] % 4 &&
        //                this.availableCardsInGame[card + 1] / 4 > hearts.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //            else if (this.availableCardsInGame[card] / 4 < hearts.Min() / 4 &&
        //                this.availableCardsInGame[card + 1] / 4 < hearts.Min())
        //            {
        //                player.Current = 5;
        //                player.Power = hearts.Max() + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //        }

        //        if (spades.Length == 3 || spades.Length == 4)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == spades[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > spades.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }

        //                if (this.availableCardsInGame[card + 1] / 4 > spades.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else if (this.availableCardsInGame[card] / 4 < spades.Max() / 4 &&
        //                    this.availableCardsInGame[card] / 4 < spades.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = spades.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (spades.Length == 4)//different cards in hand
        //        {
        //            if (this.availableCardsInGame[card] % 4 != this.availableCardsInGame[card + 1] % 4 &&
        //                this.availableCardsInGame[card] % 4 == spades[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card] / 4 > spades.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = spades.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 != this.availableCardsInGame[card] % 4 &&
        //                this.availableCardsInGame[card + 1] % 4 == spades[0] % 4)
        //            {
        //                if (this.availableCardsInGame[card + 1] / 4 > spades.Max() / 4)
        //                {
        //                    player.Current = 5;
        //                    player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //                else
        //                {
        //                    player.Current = 5;
        //                    player.Power = spades.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                    vf = true;
        //                }
        //            }
        //        }

        //        if (spades.Length == 5)
        //        {
        //            if (this.availableCardsInGame[card] % 4 == spades[0] % 4 &&
        //                this.availableCardsInGame[card] / 4 > spades.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }

        //            if (this.availableCardsInGame[card + 1] % 4 == spades[0] % 4 &&
        //                this.availableCardsInGame[card + 1] / 4 > spades.Min() / 4)
        //            {
        //                player.Current = 5;
        //                player.Power = this.availableCardsInGame[card + 1] + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //            else if (this.availableCardsInGame[card] / 4 < spades.Min() / 4 &&
        //                this.availableCardsInGame[card + 1] / 4 < spades.Min())
        //            {
        //                player.Current = 5;
        //                player.Power = spades.Max() + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                vf = true;
        //            }
        //        }
        //        //ace
        //        if (clubs.Length > 0)
        //        {
        //            if (this.availableCardsInGame[card] / 4 == 0 &&
        //                this.availableCardsInGame[card] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (this.availableCardsInGame[card + 1] / 4 == 0 &&
        //                this.availableCardsInGame[card + 1] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (diamonds.Length > 0)
        //        {
        //            if (this.availableCardsInGame[card] / 4 == 0 &&
        //                this.availableCardsInGame[card] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (this.availableCardsInGame[card + 1] / 4 == 0 &&
        //                this.availableCardsInGame[card + 1] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (hearts.Length > 0)
        //        {
        //            if (this.availableCardsInGame[card] / 4 == 0 &&
        //                this.availableCardsInGame[card] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (this.availableCardsInGame[card + 1] / 4 == 0 &&
        //                this.availableCardsInGame[card + 1] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }

        //        if (spades.Length > 0)
        //        {
        //            if (this.availableCardsInGame[card] / 4 == 0 &&
        //                this.availableCardsInGame[card] % 4 == spades[0] % 4 && vf && spades.Length > 0)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }

        //            if (this.availableCardsInGame[card + 1] / 4 == 0 &&
        //                this.availableCardsInGame[card + 1] % 4 == spades[0] % 4 && vf)
        //            {
        //                player.Current = 5.5;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }
        //    }
        //}

        //private void rStraight( Player player, int[] Straight)
        //{
        //    if (player.Current >= -1)
        //    {
        //        var op = Straight.Select(o => o / 4).Distinct().ToArray();
        //        for (int j = 0; j < op.Length - 4; j++)
        //        {
        //            if (op[j] + 4 == op[j + 4])
        //            {
        //                if (op.Max() - 4 == op[j])
        //                {
        //                    player.Current = 4;
        //                    player.Power = op.Max() + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 4 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                }
        //                else
        //                {
        //                    player.Current = 4;
        //                    player.Power = op[j + 4] + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 4 });
        //                    this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //                }
        //            }

        //            if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
        //            {
        //                player.Current = 4;
        //                player.Power = 13 + player.Current * 100;
        //                this.Win.Add(new Type() { Power = player.Power, Current = 4 });
        //                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //            }
        //        }
        //    }
        //}

        //private void rThreeOfAKind( Player player, int[] Straight)
        //{
        //    if (player.Current >= -1)
        //    {
        //        for (int j = 0; j <= 12; j++)
        //        {
        //            var fh = Straight.Where(o => o / 4 == j).ToArray();
        //            if (fh.Length == 3)
        //            {
        //                if (fh.Max() / 4 == 0)
        //                {
        //                    player.Current = 3;
        //                    player.Power = 13 * 3 + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 3 });
        //                    this.sorted = this.Win
        //                        .OrderByDescending(op => op.Current)
        //                        .ThenByDescending(op => op.Power)
        //                        .First();
        //                }
        //                else
        //                {
        //                    player.Current = 3;
        //                    player.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 3 });
        //                    this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
        //                }
        //            }
        //        }
        //    }
        //}

        //private void rTwoPair(int card,  Player player)
        //{
        //    if (player.Current >= -1)
        //    {
        //        bool msgbox = false;
        //        for (int tc = 16; tc >= 12; tc--)
        //        {
        //            int max = tc - 12;
        //            if (this.availableCardsInGame[card] / 4 != this.availableCardsInGame[card + 1] / 4)
        //            {
        //                for (int k = 1; k <= max; k++)
        //                {
        //                    if (tc - k < 12)
        //                    {
        //                        max--;
        //                    }
        //                    if (tc - k >= 12)
        //                    {
        //                        if ((this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc] / 4 &&
        //                            this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc - k] / 4) ||
        //                            (this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc] / 4 &&
        //                            this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc - k] / 4))
        //                        {
        //                            if (!msgbox)
        //                            {
        //                                if (this.availableCardsInGame[card] / 4 == 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = 13 * 4 + (this.availableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
        //                                }

        //                                if (this.availableCardsInGame[card + 1] / 4 == 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = 13 * 4 + (this.availableCardsInGame[card] / 4) * 2 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
        //                                }

        //                                if (this.availableCardsInGame[card + 1] / 4 != 0 && this.availableCardsInGame[card] / 4 != 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = (this.availableCardsInGame[card] / 4) * 2 + (this.availableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
        //                                }
        //                            }
        //                            msgbox = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void rPairTwoPair(int card,  Player player )
        //{
        //    if (player.Current >= -1)
        //    {
        //        bool msgbox = false;
        //        bool msgbox1 = false;
        //        for (int tc = 16; tc >= 12; tc--)
        //        {
        //            int max = tc - 12;
        //            for (int k = 1; k <= max; k++)
        //            {
        //                if (tc - k < 12)
        //                {
        //                    max--;
        //                }

        //                if (tc - k >= 12)
        //                {
        //                    if (this.availableCardsInGame[tc] / 4 == this.availableCardsInGame[tc - k] / 4)
        //                    {
        //                        if (this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[card] / 4 &&
        //                            this.availableCardsInGame[tc] / 4 != this.availableCardsInGame[card + 1] / 4 && player.Current == 1)
        //                        {
        //                            if (!msgbox)
        //                            {
        //                                if (this.availableCardsInGame[card + 1] / 4 == 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = (this.availableCardsInGame[card] / 4) * 2 + 13 * 4 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win
        //                                        .OrderByDescending(op => op.Current)
        //                                        .ThenByDescending(op => op.Power)
        //                                        .First();
        //                                }

        //                                if (this.availableCardsInGame[card] / 4 == 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = (this.availableCardsInGame[card + 1] / 4) * 2 + 13 * 4 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win
        //                                        .OrderByDescending(op => op.Current)
        //                                        .ThenByDescending(op => op.Power)
        //                                        .First();
        //                                }

        //                                if (this.availableCardsInGame[card + 1] / 4 != 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win
        //                                        .OrderByDescending(op => op.Current)
        //                                        .ThenByDescending(op => op.Power)
        //                                        .First();
        //                                }

        //                                if (this.availableCardsInGame[card] / 4 != 0)
        //                                {
        //                                    player.Current = 2;
        //                                    player.Power = (this.availableCardsInGame[tc] / 4) * 2 + (this.availableCardsInGame[card] / 4) * 2 + player.Current * 100;
        //                                    this.Win.Add(new Type() { Power = player.Power, Current = 2 });
        //                                    this.sorted = this.Win
        //                                        .OrderByDescending(op => op.Current)
        //                                        .ThenByDescending(op => op.Power)
        //                                        .First();
        //                                }
        //                            }
        //                            msgbox = true;
        //                        }

        //                        if (player.Current == -1)
        //                        {
        //                            if (!msgbox1)
        //                            {
        //                                if (this.availableCardsInGame[card] / 4 > this.availableCardsInGame[card + 1] / 4)
        //                                {
        //                                    if (this.availableCardsInGame[tc] / 4 == 0)
        //                                    {
        //                                        player.Current = 0;
        //                                        player.Power = 13 + this.availableCardsInGame[card] / 4 + player.Current * 100;
        //                                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                                        this.sorted = this.Win
        //                                            .OrderByDescending(op => op.Current)
        //                                            .ThenByDescending(op => op.Power)
        //                                            .First();
        //                                    }
        //                                    else
        //                                    {
        //                                        player.Current = 0;
        //                                        player.Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[card] / 4 + player.Current * 100;
        //                                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                                        this.sorted = this.Win
        //                                            .OrderByDescending(op => op.Current)
        //                                            .ThenByDescending(op => op.Power)
        //                                            .First();
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (this.availableCardsInGame[tc] / 4 == 0)
        //                                    {
        //                                        player.Current = 0;
        //                                        player.Power = 13 + this.availableCardsInGame[card + 1] + player.Current * 100;
        //                                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                                        this.sorted = this.Win
        //                                            .OrderByDescending(op => op.Current)
        //                                            .ThenByDescending(op => op.Power)
        //                                            .First();
        //                                    }
        //                                    else
        //                                    {
        //                                        player.Current = 0;
        //                                        player.Power = this.availableCardsInGame[tc] / 4 + this.availableCardsInGame[card + 1] / 4 + player.Current * 100;
        //                                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                                        this.sorted = this.Win
        //                                            .OrderByDescending(op => op.Current)
        //                                            .ThenByDescending(op => op.Power)
        //                                            .First();
        //                                    }
        //                                }
        //                            }
        //                            msgbox1 = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void rPairFromHand(int card,  Player player)
        //{
        //    if (player.Current >= -1)
        //    {
        //        bool msgbox = false;
        //        if (this.availableCardsInGame[card] / 4 == this.availableCardsInGame[card + 1] / 4)
        //        {
        //            if (!msgbox)
        //            {
        //                if (this.availableCardsInGame[card] / 4 == 0)
        //                {
        //                    player.Current = 1;
        //                    player.Power = 13 * 4 + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                    this.sorted = this.Win
        //                        .OrderByDescending(op => op.Current)
        //                        .ThenByDescending(op => op.Power)
        //                        .First();
        //                }
        //                else
        //                {
        //                    player.Current = 1;
        //                    player.Power = (this.availableCardsInGame[card + 1] / 4) * 4 + player.Current * 100;
        //                    this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                    this.sorted = this.Win
        //                        .OrderByDescending(op => op.Current)
        //                        .ThenByDescending(op => op.Power)
        //                        .First();
        //                }
        //            }
        //            msgbox = true;
        //        }

        //        for (int tc = 16; tc >= 12; tc--)
        //        {
        //            if (this.availableCardsInGame[card + 1] / 4 == this.availableCardsInGame[tc] / 4)
        //            {
        //                if (!msgbox)
        //                {
        //                    if (this.availableCardsInGame[card + 1] / 4 == 0)
        //                    {
        //                        player.Current = 1;
        //                        player.Power = 13 * 4 + this.availableCardsInGame[card] / 4 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                        this.sorted = this.Win
        //                            .OrderByDescending(op => op.Current)
        //                            .ThenByDescending(op => op.Power)
        //                            .First();
        //                    }
        //                    else
        //                    {
        //                        player.Current = 1;
        //                        player.Power = (this.availableCardsInGame[card + 1] / 4) * 4 + this.availableCardsInGame[card] / 4 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                        this.sorted = this.Win
        //                            .OrderByDescending(op => op.Current)
        //                            .ThenByDescending(op => op.Power)
        //                            .First();
        //                    }
        //                }
        //                msgbox = true;
        //            }

        //            if (this.availableCardsInGame[card] / 4 == this.availableCardsInGame[tc] / 4)
        //            {
        //                if (!msgbox)
        //                {
        //                    if (this.availableCardsInGame[card] / 4 == 0)
        //                    {
        //                        player.Current = 1;
        //                        player.Power = 13 * 4 + this.availableCardsInGame[card + 1] / 4 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                        this.sorted = this.Win
        //                            .OrderByDescending(op => op.Current)
        //                            .ThenByDescending(op => op.Power)
        //                            .First();
        //                    }
        //                    else
        //                    {
        //                        player.Current = 1;
        //                        player.Power = (this.availableCardsInGame[tc] / 4) * 4 + this.availableCardsInGame[card + 1] / 4 + player.Current * 100;
        //                        this.Win.Add(new Type() { Power = player.Power, Current = 1 });
        //                        this.sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
        //                    }
        //                }
        //                msgbox = true;
        //            }
        //        }
        //    }
        //}

        //private void rHighCard(int card,  Player player)
        //{
        //    if (player.Current == -1)
        //    {
        //        if (this.availableCardsInGame[card] / 4 > this.availableCardsInGame[card + 1] / 4)
        //        {
        //            player.Current = -1;
        //            player.Power = this.availableCardsInGame[card] / 4;
        //            this.Win.Add(new Type() { Power = player.Power, Current = -1 });
        //            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //        }
        //        else
        //        {
        //            player.Current = -1;
        //            player.Power = this.availableCardsInGame[card + 1] / 4;
        //            this.Win.Add(new Type() { Power = player.Power, Current = -1 });
        //            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //        }

        //        if (this.availableCardsInGame[card] / 4 == 0 || this.availableCardsInGame[card + 1] / 4 == 0)
        //        {
        //            player.Current = -1;
        //            player.Power = 13;
        //            this.Win.Add(new Type() { Power = player.Power, Current = -1 });
        //            this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        //        }
        //    }
        //}

        //void Winner(IPlayer player, string lastly)
        //{
        //    if (lastly == " ")
        //    {
        //        lastly = this.fifthBot.Name;
        //    }

        //    for (int j = 0; j <= 16; j++)
        //    {
        //        //await Task.Delay(5);
        //        if (this.cardsHolder[j].Visible)
        //        {
        //            this.cardsHolder[j].Image = this.cardsImageDeck[j];
        //        }

        //    }

        //    if (player.Current == this.sorted.Current)
        //    {
        //        if (player.Power == this.sorted.Power)
        //        {
        //            this.winners++;
        //            this.CheckWinners.Add(player.Name);

        //            //TODO if statement to switch
        //            if (player.Current == -1)
        //            {
        //                MessageBox.Show(player.Name + " High Card ");
        //            }

        //            if (player.Current == 1 || player.Current == 0)
        //            {
        //                MessageBox.Show(player.Name + " Pair ");
        //            }

        //            if (player.Current == 2)
        //            {
        //                MessageBox.Show(player.Name + " Two Pair ");
        //            }

        //            if (player.Current == 3)
        //            {
        //                MessageBox.Show(player.Name + " Three of a Kind ");
        //            }

        //            if (player.Current == 4)
        //            {
        //                MessageBox.Show(player.Name + " Straight ");
        //            }

        //            if (player.Current == 5 || player.Current == 5.5)
        //            {
        //                MessageBox.Show(player.Name + " Flush ");
        //            }

        //            if (player.Current == 6)
        //            {
        //                MessageBox.Show(player.Name + " Full House ");
        //            }

        //            if (player.Current == 7)
        //            {
        //                MessageBox.Show(player.Name + " Four of a Kind ");
        //            }

        //            if (player.Current == 8)
        //            {
        //                MessageBox.Show(player.Name + " Straight Flush ");
        //            }

        //            if (player.Current == 9)
        //            {
        //                MessageBox.Show(player.Name + " Royal Flush ! ");
        //            }
        //        }
        //    }

        //    if (player.Name == lastly)//lastfixed
        //    {
        //        if (this.winners > 1)
        //        {
        //            if (this.CheckWinners.Contains(this.humanPlayer.Name))
        //            {
        //                this.humanPlayer.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxChips.Text = this.humanPlayer.Chips.ToString();
        //                //player.Panel.Visible = true;

        //            }

        //            if (this.CheckWinners.Contains(this.firstBot.Name))
        //            {
        //                this.firstBot.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxBotChips1.Text = this.firstBot.Chips.ToString();
        //                //firstBot.Panel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.secondbot.Name))
        //            {
        //                this.secondbot.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxBotChips2.Text = this.secondbot.Chips.ToString();
        //                //secondbot.Panel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.thirdbot.Name))
        //            {
        //                this.thirdbot.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxBotChips3.Text = this.thirdbot.Chips.ToString();
        //                //thirdbotPanel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.fourthBot.Name))
        //            {
        //                this.fourthBot.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxBotChips4.Text = this.fourthBot.Chips.ToString();
        //                //fourthBotPanel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.fifthBot.Name))
        //            {
        //                this.fifthBot.Chips += int.Parse(this.textBoxPot.Text) / this.winners;
        //                this.textBoxBotChips5.Text = this.fifthBot.Chips.ToString();
        //                //fifthBot.Panel.Visible = true;
        //            }
        //            //await Finish(1);
        //        }

        //        if (this.winners == 1)
        //        {
        //            if (this.CheckWinners.Contains(this.humanPlayer.Name))
        //            {
        //                this.humanPlayer.Chips += int.Parse(this.textBoxPot.Text);

        //                //player.Panel.Visible = true;

        //            }

        //            if (this.CheckWinners.Contains(this.firstBot.Name))
        //            {
        //                this.firstBot.Chips += int.Parse(this.textBoxPot.Text);

        //                //firstBot.Panel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.secondbot.Name))
        //            {
        //                this.secondbot.Chips += int.Parse(this.textBoxPot.Text);

        //                //secondbot.Panel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.thirdbot.Name))
        //            {
        //                this.thirdbot.Chips += int.Parse(this.textBoxPot.Text);

        //                //thirdbotPanel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.fourthBot.Name))
        //            {
        //                this.fourthBot.Chips += int.Parse(this.textBoxPot.Text);

        //                //fourthBotPanel.Visible = true;
        //            }

        //            if (this.CheckWinners.Contains(this.fifthBot.Name))
        //            {
        //                this.fifthBot.Chips += int.Parse(this.textBoxPot.Text);

        //                //fifthBot.Panel.Visible = true;
        //            }
        //        }
        //    }
        //}

        async Task CheckRaise(int currentTurn)
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
                //maxLeft-playerLeft(callnebager)
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;

                        foreach (var item in this.db.Players)
                        {
                            item.Status.Text = String.Empty;
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
                        foreach (var item in this.db.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }

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
                        foreach (var item in this.db.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                        //this.humanPlayerCall = 0;
                        //this.firstBotCall = 0;
                        //this.secondbotCall = 0;
                        //this.thirdbotCall = 0;
                        //this.fourthBotCall = 0;
                        //this.fifthBotCall = 0;

                        //this.humanPlayerRaise = 0;
                        //this.firstBotRaise = 0;
                        //this.secondbotRaise = 0;
                        //this.thirdbotRaise = 0;
                        //this.fourthBotRaise = 0;
                        //this.fifthBotRaise = 0;
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
                        foreach (var item in this.db.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                        //this.humanPlayerCall = 0;
                        //this.firstBotCall = 0;
                        //this.secondbotCall = 0;
                        //this.thirdbotCall = 0;
                        //this.fourthBotCall = 0;
                        //this.fifthBotCall = 0;

                        //this.humanPlayerRaise = 0;
                        //this.firstBotRaise = 0;
                        //this.secondbotRaise = 0;
                        //this.thirdbotRaise = 0;
                        //this.fourthBotRaise = 0;
                        //this.fifthBotRaise = 0;
                    }
                }
            }

            if (this.rounds == this.end && this.maxLeft == 6)
            {
                string fixedLast = String.Empty; //= "qwerty";

                foreach (var player in this.db.Players)
                {
                    if (!player.Status.Text.Contains(GlobalConstants.FoldMessage))
                    {
                        fixedLast = player.Name;
                        //Rules(player);
                        this.rules.CheckForHand(player);
                        this.rules.Winner(player, fixedLast);
                        player.FoldTurn = false;
                    }
                }


                //Winner(this.humanPlayerType, this.humanPlayerPower, "Player", this.chips, fixedLast);
                //Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
                //Winner(this.secondbotType, this.secondbotPower, "Bot 2", this.secondbotChips, fixedLast);
                //Winner(this.thirdbotType, this.thirdbotPower, "Bot 3", this.thirdbotChips, fixedLast);
                //Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
                //Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
                this.restart = true;
                this.humanPlayer.IsPlayerTurn = true;
                //this.humanPlayerFoldTturn = false;
                //this.B1Fturn = false;
                //this.B2Fturn = false;
                //this.B3Fturn = false;
                //this.B4Fturn = false;
                //this.B5Fturn = false;

                if (this.humanPlayer.Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.humanPlayer.Chips = f2.a;
                        for (int i = 1; i < this.db.Players.Length; i++)
                        {
                            this.db.Players[i].Chips += f2.a;
                        }
                        //this.firstBotChips += f2.a;
                        //this.secondbotChips += f2.a;
                        //this.thirdbotChips += f2.a;
                        //this.fourthBotChips += f2.a;
                        //this.fifthBotChips += f2.a;
                        this.humanPlayer.FoldTurn = false;
                        this.humanPlayer.IsPlayerTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = GlobalConstants.RaiseMessage;
                    }
                }

                //this.humanPlayer.Panel.Visible = false;
                //this.firstBot.Panel.Visible = false;
                //this.secondbot.Panel.Visible = false;
                //this.thirdbotPanel.Visible = false;
                //this.fourthBotPanel.Visible = false;
                //this.fifthBot.Panel.Visible = false;
                foreach (var player in this.db.Players)
                {
                    player.Panel.Visible = false;
                    player.Call = 0;
                    player.Raise = 0;
                    player.Power = 0;
                    player.Current = -1;
                }
                //this.humanPlayerCall = 0;
                //this.firstBotCall = 0;
                //this.secondbotCall = 0;
                //this.thirdbotCall = 0;
                //this.fourthBotCall = 0;
                //this.fifthBotCall = 0;

                //this.humanPlayerRaise = 0;
                //this.firstBotRaise = 0;
                //this.secondbotRaise = 0;
                //this.thirdbotRaise = 0;
                //this.fourthBotRaise = 0;
                //this.fifthBotRaise = 0;

                this.last = 0;
                this.call = this.defaultBigBlind;
                this.raise = 0;
                this.ImgLocation = Directory.GetFiles(
                    GlobalConstants.PlayingCardsDirectoryPath,
                    GlobalConstants.PlayingCardsWithPngExtension,
                    SearchOption.TopDirectoryOnly);

                this.bools.Clear();
                this.rounds = 0;
                //this.humanPlayerPower = 0;
                //this.humanPlayerType = -1;
                this.rules.Type = 0;

                //this.firstBotPower = 0;
                //this.secondbotPower = 0;
                //this.thirdbotPower = 0;
                //this.fourthBotPower = 0;
                //this.fifthBotPower = 0;

                //this.firstBotType = -1;
                //this.secondbotType = -1;
                //this.thirdbotType = -1;
                //this.fourthBotType = -1;
                //this.fifthBotType = -1;

                this.ints.Clear();
               
                //this.CheckWinners.Clear();
                //this.winners = 0;
                //this.Win.Clear();
                //this.sorted.Current = 0;
                //this.sorted.Power = 0;

                for (int os = 0; os < 17; os++)
                {
                    this.cardsHolder[os].Image = null;
                    this.cardsHolder[os].Invalidate();
                    this.cardsHolder[os].Visible = false;
                }

                this.textBoxPot.Text = "0";
                this.humanPlayer.Status.Text = String.Empty;
                await this.Shuffle();
                await this.Turns();
            }
        }

        void FixCall(IPlayer player, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (player.Status.Text.Contains(GlobalConstants.RaiseMessage))
                    {
                        var changeRaise = player.Status.Text.Substring(6);
                        player.Raise = int.Parse(changeRaise);
                    }

                    if (player.Status.Text.Contains(GlobalConstants.CallMessage))
                    {
                        var changeCall = player.Status.Text.Substring(5);
                        player.Call = int.Parse(changeCall);
                    }

                    if (player.Status.Text.Contains(GlobalConstants.CheckMessage))
                    {
                        player.Raise = 0;
                        player.Call = 0;
                    }
                }

                if (options == 2)
                {
                    if (player.Raise != this.raise && player.Raise <= this.raise)
                    {
                        this.call = Convert.ToInt32(this.raise) - player.Raise;
                    }

                    if (player.Call != this.call || player.Call <= this.call)
                    {
                        this.call = this.call - player.Call;
                    }

                    if (player.Raise == this.raise && this.raise > 0)
                    {
                        this.call = 0;
                        this.buttonCall.Enabled = false;
                        this.buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            if (this.humanPlayer.Chips <= 0 && !this.intsadded)
            {
                if (this.humanPlayer.Status.Text.Contains(GlobalConstants.RaiseMessage))
                {
                    this.ints.Add(this.humanPlayer.Chips);
                    this.intsadded = true;
                }

                if (this.humanPlayer.Status.Text.Contains(GlobalConstants.CallMessage))
                {
                    this.ints.Add(this.humanPlayer.Chips);
                    this.intsadded = true;
                }
            }

            this.intsadded = false;
            if (this.firstBot.Chips <= 0 && !this.firstBot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.firstBot.Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.secondbot.Chips <= 0 && !this.secondbot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.secondbot.Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.thirdbot.Chips <= 0 && !this.thirdbot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.thirdbot.Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.fourthBot.Chips <= 0 && !this.fourthBot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.fourthBot.Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.fifthBot.Chips <= 0 && !this.fifthBot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.fifthBot.Chips);
                    this.intsadded = true;
                }
            }

            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
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

                // TODO if statement to switch
                if (index == 0)
                {
                    this.humanPlayer.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.humanPlayer.Chips.ToString();
                    this.humanPlayer.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.HumanPlayerWinMessage);
                }

                if (index == 1)
                {
                    this.firstBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.firstBot.Chips.ToString();
                    this.firstBot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.FirstBotPlayerWinMessage);
                }

                if (index == 2)
                {
                    this.secondbot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.secondbot.Chips.ToString();
                    this.secondbot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.SecondbotPlayerWinMessage);
                }

                if (index == 3)
                {
                    this.thirdbot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.thirdbot.Chips.ToString();
                    this.thirdbot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.ThirdbotPlayerWinMessage);
                }

                if (index == 4)
                {
                    this.fourthBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fourthBot.Chips.ToString();
                    this.fourthBot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.FourthBotPlayerWinMessage);
                }

                if (index == 5)
                {
                    this.fifthBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fifthBot.Chips.ToString();
                    this.fifthBot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.FifthBotPlayerWinMessage);
                }

                for (int j = 0; j <= 16; j++)
                {
                    this.cardsHolder[j].Visible = false;
                }
                await this.Finish(1);
            }

            this.intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && this.rounds >= this.end)
            {
                await this.Finish(2);
            }
            #endregion


        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                this.rules.FixWinners();
            }
            this.humanPlayer.Panel.Visible = false;
            this.firstBot.Panel.Visible = false;
            this.secondbot.Panel.Visible = false;
            this.thirdbot.Panel.Visible = false;
            this.fourthBot.Panel.Visible = false;
            this.fifthBot.Panel.Visible = false;

            this.call = this.defaultBigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.rules.Type = 0;
            this.rounds = 0;

            this.firstBot.Power = 0;
            this.secondbot.Power = 0;
            this.thirdbot.Power = 0;
            this.fourthBot.Power = 0;
            this.fifthBot.Power = 0;
            this.humanPlayer.Power = 0;

            //this.humanPlayerType = -1;
            this.raise = 0;
            foreach (var player in this.db.Players)
            {
                player.Current = -1;
                player.Call = 0;
                player.Raise = 0;
                player.FoldTurn = false;
                player.HasPlayerFolded = false;
                player.Status.Text = String.Empty;
            }
            for (int i = 1; i < this.db.Players.Length; i++)
            {
                this.db.Players[i].IsPlayerTurn = false;
            }
            this.humanPlayer.IsPlayerTurn = true;
            //this.firstBotType = -1;
            //this.secondbotType = -1;
            //this.thirdbotType = -1;
            //this.fourthBotType = -1;
            //this.fifthBotType = -1;

            //this.isFirstBotTurn = false;
            //this.isSecondbotTurn = false;
            //this.isThirdbotTurn = false;
            //this.isFourthBotTurn = false;
            //this.isFifthBotTurn = false;

            //this.B1Fturn = false;
            //this.B2Fturn = false;
            //this.B3Fturn = false;
            //this.B4Fturn = false;
            //this.B5Fturn = false;

            //this.hasPlayerFolded = false;
            //this.hasFirstBotFolded = false;
            //this.hasSecondbotFolded = false;
            //this.hasThirdbotFolded = false;
            //this.hasFourthBotFolded = false;
            //this.hasFifthBotFolded = false;

            //this.humanPlayerFoldTturn = false;

            this.restart = false;
            this.raising = false;

            //this.humanPlayerCall = 0;
            //this.firstBotCall = 0;
            //this.secondbotCall = 0;
            //this.thirdbotCall = 0;
            //this.fourthBotCall = 0;
            //this.fifthBotCall = 0;

            //this.humanPlayerRaise = 0;
            //this.firstBotRaise = 0;
            //this.secondbotRaise = 0;
            //this.thirdbotRaise = 0;
            //this.fourthBotRaise = 0;
            //this.fifthBotRaise = 0;

            this.height = 0;
            this.width = 0;
            //this.winners = 0;
            this.rules.Winners = 0;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.rules.CheckWinners.Clear();

            this.ints.Clear();
            this.rules.Win.Clear();
            this.rules.Sorted.Current = 0;
            this.rules.Sorted.Power = 0;
            this.textBoxPot.Text = "0";
            this.t = 60;
            this.up = 10000000;
            this.turnCount = 0;
            //this.humanPlayerStatus.Text = "";
            //this.firstBotStatus.Text = "";
            //this.secondbotStatus.Text = "";
            //this.thirdbotStatus.Text = "";
            //this.fourthBotStatus.Text = "";
            //this.fifthBotStatus.Text = "";
            if (this.humanPlayer.Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.humanPlayer.Chips = f2.a;
                    this.firstBot.Chips += f2.a;
                    this.secondbot.Chips += f2.a;
                    this.thirdbot.Chips += f2.a;
                    this.fourthBot.Chips += f2.a;
                    this.fifthBot.Chips += f2.a;
                    this.humanPlayer.FoldTurn = false;
                    this.humanPlayer.IsPlayerTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = GlobalConstants.RaiseMessage;
                }
            }

            this.ImgLocation = Directory.GetFiles(GlobalConstants.PlayingCardsDirectoryPath,
                GlobalConstants.PlayingCardsWithPngExtension,
                SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.cardsHolder[os].Image = null;
                this.cardsHolder[os].Invalidate();
                this.cardsHolder[os].Visible = false;
            }
            await this.Shuffle();
            //await Turns();
        }

        //void FixWinners()
        //{
        //    this.Win.Clear();
        //    this.sorted.Current = 0;
        //    this.sorted.Power = 0;
        //    string fixedLast = String.Empty; //"qwerty";

        //    foreach (var player in this.db.Players)
        //    {
        //        if (!player.Status.Text.Contains(GlobalConstants.FoldMessage))
        //        {
        //            fixedLast = player.Name;
        //            //Rules(player);
        //            this.rules.CheckForHand(player);
        //            this.Winner(player, fixedLast);
        //        }
        //    }


        //}
        
        //void Ai(IPlayer player, int indexFirstCard, int indexSecondCard)
        //{
        //    if (!player.FoldTurn)
        //    {
        //        if (player.Current == -1)
        //        {
        //            this.HighCard(player);
        //        }

        //        if (player.Current == 0)
        //        {
        //            this.PairTable(player);
        //        }

        //        if (player.Current == 1)
        //        {
        //            this.PairHand(player);
        //        }

        //        if (player.Current == 2)
        //        {
        //            this.TwoPair(player);
        //        }

        //        if (player.Current == 3)
        //        {
        //            this.ThreeOfAKind(player);
        //        }

        //        if (player.Current == 4)
        //        {
        //            this.Straight(player);
        //        }

        //        if (player.Current == 5 || player.Current == 5.5)
        //        {
        //            this.Flush(player);
        //        }

        //        if (player.Current == 6)
        //        {
        //            this.FullHouse(player);
        //        }

        //        if (player.Current == 7)
        //        {
        //            this.FourOfAKind(player);
        //        }

        //        if (player.Current == 8 || player.Current == 9)
        //        {
        //            this.StraightFlush(player);
        //        }
        //    }

        //    if (player.FoldTurn)
        //    {
        //        this.cardsHolder[indexFirstCard].Visible = false;
        //        this.cardsHolder[indexSecondCard].Visible = false;
        //    }
        //}

        //private void HighCard(IPlayer player)
        //{
        //    this.HP(player, 20, 25);
        //}

        //private void PairTable(IPlayer player)
        //{
        //    this.HP(player, 16, 25);
        //}

        //private void PairHand(IPlayer player)
        //{
        //    Random rPair = new Random();
        //    int rCall = rPair.Next(10, 16);
        //    int rRaise = rPair.Next(10, 13);
        //    if (player.Power <= 199 && player.Power >= 140)
        //    {
        //        this.PH(player, rCall, 6, rRaise);
        //    }

        //    if (player.Power <= 139 && player.Power >= 128)
        //    {
        //        this.PH(player, rCall, 7, rRaise);
        //    }

        //    if (player.Power < 128 && player.Power >= 101)
        //    {
        //        this.PH(player, rCall, 9, rRaise);
        //    }
        //}

        //private void TwoPair(IPlayer player)
        //{
        //    Random rPair = new Random();
        //    int rCall = rPair.Next(6, 11);
        //    int rRaise = rPair.Next(6, 11);
        //    if (player.Power <= 290 && player.Power >= 246)
        //    {
        //        this.PH(player, rCall, 3, rRaise);
        //    }

        //    if (player.Power <= 244 && player.Power >= 234)
        //    {
        //        this.PH(player, rCall, 4, rRaise);
        //    }

        //    if (player.Power < 234 && player.Power >= 201)
        //    {
        //        this.PH(player, rCall, 4, rRaise);
        //    }
        //}

        //private void ThreeOfAKind(IPlayer player)
        //{
        //    Random tk = new Random();
        //    int tCall = tk.Next(3, 7);
        //    int tRaise = tk.Next(4, 8);
        //    if (player.Power <= 390 && player.Power >= 330)
        //    {
        //        this.Smooth(player, tRaise);
        //    }

        //    if (player.Power <= 327 && player.Power >= 321)//10  8
        //    {
        //        this.Smooth(player, tRaise);
        //    }

        //    if (player.Power < 321 && player.Power >= 303)//7 2
        //    {
        //        this.Smooth(player, tRaise);
        //    }
        //}

        //private void Straight(IPlayer player)
        //{
        //    Random str = new Random();
        //    int sCall = str.Next(3, 6);
        //    int sRaise = str.Next(3, 8);
        //    if (player.Power <= 480 && player.Power >= 410)
        //    {
        //        this.Smooth(player, sRaise);
        //    }

        //    if (player.Power <= 409 && player.Power >= 407)//10  8
        //    {
        //        this.Smooth(player, sRaise);
        //    }

        //    if (player.Power < 407 && player.Power >= 404)
        //    {
        //        this.Smooth(player, sRaise);
        //    }
        //}

        //private void Flush(IPlayer player)
        //{
        //    Random fsh = new Random();
        //    int fCall = fsh.Next(2, 6);
        //    int fRaise = fsh.Next(3, 7);
        //    this.Smooth(player, fRaise);
        //}

        //private void FullHouse(IPlayer player)
        //{
        //    Random flh = new Random();
        //    int fhCall = flh.Next(1, 5);
        //    int fhRaise = flh.Next(2, 6);
        //    if (player.Power <= 626 && player.Power >= 620)
        //    {
        //        this.Smooth(player, fhRaise);
        //    }

        //    if (player.Power < 620 && player.Power >= 602)
        //    {
        //        this.Smooth(player, fhRaise);
        //    }
        //}

        //private void FourOfAKind(IPlayer player)
        //{
        //    Random fk = new Random();
        //    int fkCall = fk.Next(1, 4);
        //    int fkRaise = fk.Next(2, 5);
        //    if (player.Power <= 752 && player.Power >= 704)
        //    {
        //        this.Smooth(player, fkRaise);
        //    }
        //}

        //private void StraightFlush(IPlayer player)
        //{
        //    Random sf = new Random();
        //    int sfCall = sf.Next(1, 3);
        //    int sfRaise = sf.Next(1, 3);
        //    if (player.Power <= 913 && player.Power >= 804)
        //    {
        //        this.Smooth(player, sfRaise);
        //    }
        //}

        //private void Fold(IPlayer player)
        //{
        //    this.raising = false;
        //    player.Status.Text = GlobalConstants.FoldMessage;
        //    player.IsPlayerTurn = false;
        //    player.FoldTurn = true;
        //}

        //private void Check(IPlayer player)
        //{
        //    player.Status.Text = GlobalConstants.CheckMessage;
        //    player.IsPlayerTurn = false;
        //    this.raising = false;
        //}

        //private void Call(IPlayer player)
        //{
        //    this.raising = false;
        //    player.IsPlayerTurn = false;
        //    player.Chips -= this.call;
        //    player.Status.Text = GlobalConstants.CallMessage + " " + this.call;
        //    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
        //}

        //private void Raised(IPlayer player)
        //{
        //    player.Chips -= Convert.ToInt32(this.raise);
        //    player.Status.Text = GlobalConstants.RaiseMessage + this.raise;
        //    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
        //    this.call = Convert.ToInt32(this.raise);
        //    this.raising = true;
        //    player.IsPlayerTurn = false;
        //}

        //private static double RoundN(int sChips, int n)
        //{
        //    double a = Math.Round(sChips / n / 100d, 0) * 100;
        //    return a;
        //}

        ///// <summary>
        ///// Creates a choice generator for bots.
        ///// This generator is used when bot has only high card or table pair hand.
        ///// </summary>
        ///// <param name="sChips">Bot's chips</param>     //botChips
        ///// <param name="sTurn">Parameter that indicates if it's current player's turn</param>   //botTurn
        ///// <param name="sFTurn">Parameter that indicates if current bot has folded or is all in</param>   //botFoldsTurn
        ///// <param name="sStatus">The status text of the bot</param>    //botStatus
        ///// <param name="botPower">The bot's hand category factor</param>   //botHandRankFactor
        ///// <param name="n">The n parameter</param>
        ///// <param name="n1">The n1 parameter</param>        
        //private void HP(IPlayer player, int n, int n1)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 4);
        //    if (this.call <= 0)
        //    {
        //        this.Check(player);
        //    }

        //    if (this.call > 0)
        //    {
        //        if (rnd == 1)
        //        {
        //            if (this.call <= RoundN(player.Chips, n))
        //            {
        //                this.Call(player);
        //            }
        //            else
        //            {
        //                this.Fold(player);
        //            }
        //        }

        //        if (rnd == 2)
        //        {
        //            if (this.call <= RoundN(player.Chips, n1))
        //            {
        //                this.Call(player);
        //            }
        //            else
        //            {
        //                this.Fold(player);
        //            }
        //        }
        //    }

        //    if (rnd == 3)
        //    {
        //        if (this.raise == 0)
        //        {
        //            this.raise = this.call * 2;
        //            this.Raised(player);
        //        }
        //        else
        //        {
        //            if (this.raise <= RoundN(player.Chips, n))
        //            {
        //                this.raise = this.call * 2;
        //                this.Raised(player);
        //            }
        //            else
        //            {
        //                this.Fold(player);
        //            }
        //        }
        //    }

        //    if (player.Chips <= 0)
        //    {
        //        player.FoldTurn = true;
        //    }
        //}

        ///// <summary>
        ///// Choice maker for bots if they have a hand which is a pair or two pairs.
        ///// Uses BotChoiceFormula formula.
        ///// </summary>
        ///// <param name="sChips">Bot's chips</param>  //botChips
        ///// <param name="sTurn">Parameter that indicates if it's current player's turn</param>   //botTurn
        ///// <param name="sFTurn">Parameter that indicates if current bot has folded or is all in</param>  //botFoldsTurn
        ///// <param name="sStatus">The status text of the bot</param>  //botStatus
        ///// <param name="n">The n parameter</param>
        ///// <param name="n1">The n1 parameter</param>
        ///// <param name="r">The randGenerator parameter</param>   //randGenerator
        //private void PH(IPlayer player, int n, int n1, int r)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 3);
        //    if (this.rounds < 2)
        //    {
        //        if (this.call <= 0)
        //        {
        //            this.Check(player);
        //        }

        //        if (this.call > 0)
        //        {
        //            if (this.call >= RoundN(player.Chips, n1))
        //            {
        //                this.Fold(player);
        //            }

        //            if (this.raise > RoundN(player.Chips, n))
        //            {
        //                this.Fold(player);
        //            }

        //            if (!player.FoldTurn)
        //            {
        //                if (this.call >= RoundN(player.Chips, n) && this.call <= RoundN(player.Chips, n1))
        //                {
        //                    this.Call(player);
        //                }

        //                if (this.raise <= RoundN(player.Chips, n) && this.raise >= (RoundN(player.Chips, n)) / 2)
        //                {
        //                    this.Call(player);
        //                }

        //                if (this.raise <= (RoundN(player.Chips, n)) / 2)
        //                {
        //                    if (this.raise > 0)
        //                    {
        //                        this.raise = RoundN(player.Chips, n);
        //                        this.Raised(player);
        //                    }
        //                    else
        //                    {
        //                        this.raise = this.call * 2;
        //                        this.Raised(player);
        //                    }
        //                }

        //            }
        //        }
        //    }

        //    if (this.rounds >= 2)
        //    {
        //        if (this.call > 0)
        //        {
        //            if (this.call >= RoundN(player.Chips, n1 - rnd))
        //            {
        //                this.Fold(player);
        //            }

        //            if (this.raise > RoundN(player.Chips, n - rnd))
        //            {
        //                this.Fold(player);
        //            }

        //            if (!player.FoldTurn)
        //            {
        //                if (this.call >= RoundN(player.Chips, n - rnd) && this.call <= RoundN(player.Chips, n1 - rnd))
        //                {
        //                    this.Call(player);
        //                }

        //                if (this.raise <= RoundN(player.Chips, n - rnd) && this.raise >= (RoundN(player.Chips, n - rnd)) / 2)
        //                {
        //                    this.Call(player);
        //                }

        //                if (this.raise <= (RoundN(player.Chips, n - rnd)) / 2)
        //                {
        //                    if (this.raise > 0)
        //                    {
        //                        this.raise = RoundN(player.Chips, n - rnd);
        //                        this.Raised(player);
        //                    }
        //                    else
        //                    {
        //                        this.raise = this.call * 2;
        //                        this.Raised(player);
        //                    }
        //                }
        //            }
        //        }

        //        if (this.call <= 0)
        //        {
        //            this.raise = RoundN(player.Chips, r - rnd);
        //            this.Raised(player);
        //        }
        //    }

        //    if (player.Chips <= 0)
        //    {
        //        player.FoldTurn = true;
        //    }
        //}

        ///// <summary>
        ///// Choice maker for bots with a hand three of a kind or higher.
        ///// Uses BotChoiceFormula formula.
        ///// </summary>
        ///// <param name="botChips">Bot's chips</param>
        ///// <param name="botTurn">Parameter that indicates if it's current player's turn</param>
        ///// <param name="botFTurn">Parameter that indicates if current bot has folded or is all in</param>
        ///// <param name="botStatus">The status text of the bot</param>
        ///// <param name="name">The current bot's name</param>   //  botName
        ///// <param name="n">The n parameter</param>
        ///// <param name="r">The randGenerator parameter</param>  //randGenerator       
        //void Smooth(IPlayer player, int n)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 3);
        //    if (this.call <= 0)
        //    {
        //        this.Check(player);
        //    }
        //    else
        //    {
        //        if (this.call >= RoundN(player.Chips, n))
        //        {
        //            if (player.Chips > this.call)
        //            {
        //                this.Call(player);
        //            }
        //            else if (player.Chips <= this.call)
        //            {
        //                this.raising = false;
        //                player.Turn = false;
        //                player.Chips = 0;
        //                player.Status.Text = GlobalConstants.CallMessage + " " + player.Chips;
        //                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + player.Chips).ToString();
        //            }
        //        }
        //        else
        //        {
        //            if (this.raise > 0)
        //            {
        //                if (player.Chips >= this.raise * 2)
        //                {
        //                    this.raise *= 2;
        //                    this.Raised(player);
        //                }
        //                else
        //                {
        //                    this.Call(player);
        //                }
        //            }
        //            else
        //            {
        //                this.raise = this.call * 2;
        //                this.Raised(player);
        //            }
        //        }
        //    }

        //    if (player.Chips <= 0)
        //    {
        //        player.IsPlayerTurn = true;
        //    }
        //}

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (this.pokerBetTimer.Value <= 0)
            {
                this.humanPlayer.FoldTurn = true;
                await this.Turns();
            }

            if (this.t > 0)
            {
                this.t--;
                this.pokerBetTimer.Value = (this.t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {


            this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBot.Chips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondbot.Chips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdbot.Chips.ToString();
            this.textBoxBotChips4.Text = "chips : " + this.fourthBot.Chips.ToString();
            this.textBoxBotChips5.Text = "chips : " + this.fifthBot.Chips.ToString();

            if (this.humanPlayer.Chips <= 0)
            {
                this.humanPlayer.IsPlayerTurn = false;
                this.humanPlayer.FoldTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (this.up > 0)
            {
                this.up--;
            }

            if (this.humanPlayer.Chips >= this.call)
            {
                this.buttonCall.Text = GlobalConstants.CallMessage + " " + this.call;
            }
            else
            {
                this.buttonCall.Text = GlobalConstants.AllInMessage;
                this.buttonRaise.Enabled = false;
            }

            if (this.call > 0)
            {
                this.buttonCheck.Enabled = false;
            }
            else           //elica: change  if (call <= 0) with else
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = GlobalConstants.CallMessage;
                this.buttonCall.Enabled = false;
            }

            if (this.humanPlayer.Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.humanPlayer.Chips <= int.Parse(this.textBoxRaise.Text))
                {
                    this.buttonRaise.Text = GlobalConstants.AllInMessage;
                }
                else
                {
                    this.buttonRaise.Text = GlobalConstants.RaiseMessage;
                }
            }

            if (this.humanPlayer.Chips < this.call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void buttonFold_IsClicked(object sender, EventArgs e)
        {
            this.humanPlayer.Status.Text = GlobalConstants.FoldMessage;
            this.humanPlayer.IsPlayerTurn = false;
            this.humanPlayer.FoldTurn = true;
            await this.Turns();
        }

        private async void buttonCheck_IsClicked(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.humanPlayer.IsPlayerTurn = false;
                this.humanPlayer.Status.Text = GlobalConstants.CheckMessage;
            }
            else
            {
                //playerStatus.Text = "All in " + chips;

                this.buttonCheck.Enabled = false;
            }
            await this.Turns();
        }

        private async void buttonCall_IsClicked(object sender, EventArgs e)
        {
            //Rules(this.humanPlayer);
            this.rules.CheckForHand(this.humanPlayer);
            if (this.humanPlayer.Chips >= this.call)
            {
                this.humanPlayer.Chips -= this.call;
                this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();

                if (this.textBoxPot.Text != String.Empty)
                {
                    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                }
                else
                {
                    this.textBoxPot.Text = this.call.ToString();
                }

                this.humanPlayer.IsPlayerTurn = false;
                this.humanPlayer.Status.Text = GlobalConstants.CallMessage + " " + this.call;
                this.humanPlayer.Call = this.call;
            }
            else if (this.humanPlayer.Chips <= this.call && this.call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.humanPlayer.Chips).ToString();
                this.humanPlayer.Status.Text = GlobalConstants.AllInMessage + " " + this.humanPlayer.Chips;
                this.humanPlayer.Chips = 0;
                this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();
                this.humanPlayer.IsPlayerTurn = false;
                this.buttonFold.Enabled = false;
                this.humanPlayer.Call = this.humanPlayer.Chips;
            }
            await this.Turns();
        }

        private async void buttonRaise_IsClicked(object sender, EventArgs e)
        {
            //Rules(this.humanPlayer);
            this.rules.CheckForHand(this.humanPlayer);
            int parsedValue;
            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.humanPlayer.Chips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.textBoxRaise.Text))
                    {
                        this.textBoxRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show(GlobalConstants.RaiseErrorMessage);
                        return;
                    }
                    else
                    {
                        if (this.humanPlayer.Chips >= int.Parse(this.textBoxRaise.Text))
                        {
                            this.call = int.Parse(this.textBoxRaise.Text);
                            this.raise = int.Parse(this.textBoxRaise.Text);
                            this.humanPlayer.Status.Text = GlobalConstants.RaiseMessage + " " + this.call;
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                            this.buttonCall.Text = GlobalConstants.CallMessage; //"Call";
                            this.humanPlayer.Chips -= int.Parse(this.textBoxRaise.Text);
                            this.raising = true;
                            this.last = 0;
                            this.humanPlayer.Raise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            this.call = this.humanPlayer.Chips;
                            this.raise = this.humanPlayer.Chips;
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.humanPlayer.Chips).ToString();
                            this.humanPlayer.Status.Text = GlobalConstants.RaiseMessage + " " + this.call;
                            this.humanPlayer.Chips = 0;
                            this.raising = true;
                            this.last = 0;
                            this.humanPlayer.Raise = Convert.ToInt32(this.raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(GlobalConstants.NumberOnlyFieldErrorMessage);
                return;
            }
            this.humanPlayer.IsPlayerTurn = false;
            await this.Turns();
        }

        private void buttonAddChips_IsClicked(object sender, EventArgs e)
        {
            if (this.tbAdd.Text == String.Empty)
            {
                //elica Empty if statement
            }
            else
            {
                this.humanPlayer.Chips += int.Parse(this.tbAdd.Text);
                this.firstBot.Chips += int.Parse(this.tbAdd.Text);
                this.secondbot.Chips += int.Parse(this.tbAdd.Text);
                this.thirdbot.Chips += int.Parse(this.tbAdd.Text);
                this.fourthBot.Chips += int.Parse(this.tbAdd.Text);
                this.fifthBot.Chips += int.Parse(this.tbAdd.Text);
            }

            this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();
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
                MessageBox.Show(GlobalConstants.SmallBlindRoundNumberErrorMessage);
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show(GlobalConstants.NumberOnlyFieldErrorMessage);
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show(GlobalConstants.SmallBlindMaxValueMessage);
                this.textBoxSmallBlind.Text = this.defaultSmallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show(GlobalConstants.SmallBlindMinValueMessage);
            }

            if (int.Parse(this.textBoxSmallBlind.Text) >= 250 && int.Parse(this.textBoxSmallBlind.Text) <= 100000)
            {
                this.defaultSmallBlind = int.Parse(this.textBoxSmallBlind.Text);
                MessageBox.Show(GlobalConstants.SavedChangesMessage);
            }
        }

        private void buttonBigBlind_IsClicked(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBigBlind.Text.Contains(",") || this.textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show(GlobalConstants.BigBlindRoundNumberErrorMessage);
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (!int.TryParse(this.textBoxBigBlind.Text, out parsedValue))
            {
                MessageBox.Show(GlobalConstants.NumberOnlyFieldErrorMessage);
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show(GlobalConstants.BigBlindMaxValueMessage);
                this.textBoxBigBlind.Text = this.defaultBigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show(GlobalConstants.BigBlindMinValueMessage);
            }

            if (int.Parse(this.textBoxBigBlind.Text) >= 500 && int.Parse(this.textBoxBigBlind.Text) <= 200000)
            {
                this.defaultBigBlind = int.Parse(this.textBoxBigBlind.Text);
                MessageBox.Show(GlobalConstants.SavedChangesMessage);
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }
        #endregion

        //private ICard SetCard(int card)
        //{
        //    var suit = this.cardHandler.GetSuit(card);
        //    var value = this.cardHandler.GetValue(card);
        //    return new Card(suit, value);
        //}
    }
}