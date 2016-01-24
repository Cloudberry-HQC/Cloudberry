using Poker.Core;
using Poker.Interfaces;
using Poker.Models.Card;
using Poker.Models.PokerManagement;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Models.Player;
    
    using Poker.Models;
    public partial class Form1 : Form
    {
        #region Variables
        //ProgressBar asd = new ProgressBar(); //elica: It is never used!!!
        //public int Nm;
        private Human player = new Human("Player");
        private Bot firstBot = new Bot("FirstBot");
        private Bot secondBot = new Bot("SecondBot");
        private Bot thirdBot = new Bot("ThirdBot");
        private Bot fourthBot = new Bot("FourthBot");
        private Bot fifthBot = new Bot("FifthBot");
        private DataBase Db = DataBase.Instace;
        private PokerTable table;
        private Rules rules = Rules.Instance;
        private CardHandler cardHandler = new CardHandler();

        private const int InitialValueOfChips = 10000;
        private const int DefaultValueOfBigBlind = 500;
        private const int DefaultValueOfSmallBlind = 250;
        //private Panel player.Panel = new Panel();
        //private Panel firstBot.Panel = new Panel();
        //private Panel secondBot.Panel = new Panel();
        //private Panel thirdBotPanel = new Panel();
        //private Panel fourthBotPanel = new Panel();
        //private Panel fifthBot.Panel = new Panel();
        private int call = 500;
        private int foldedPlayers = 5;
        //elica: chips
        //private int chips = InitialValueOfChips;     
        //private int firstBotChips = InitialValueOfChips;
        //private int secondBotChips = InitialValueOfChips;
        //private int thirdBotChips = InitialValueOfChips;
        //private int fourthBotChips = InitialValueOfChips;
        //private int fifthBotChips = InitialValueOfChips;

        private double type;
        private double rounds;
        private double raise;
        //private double firstBotPower;
        //private double secondBotPower;
        //private double thirdBotPower;
        //private double fourthBotPower;
        //private double fifthBotPower;
        //private double playerPower;
        //private double playerType = -1;
        //private double firstBotType = -1;
        //private double secondBotType = -1;
        //private double thirdBotType = -1;
        //private double fourthBotType = -1;
        //private double fifthBotType = -1;

        //private bool isFirstBotTurn;
        //private bool isSecondBotTurn;
        //private bool isThirdBotTurn;
        //private bool isFourthBotTurn;
        //private bool isFifthBotTurn;

        //private bool B1Fturn;
        //private bool B2Fturn;
        //private bool B3Fturn;
        //private bool B4Fturn;
        //private bool B5Fturn;

        //private bool hasPlayerFolded;
        //private bool hasFirstBotFolded;
        //private bool hasSecondBotFolded;
        //private bool hasThirdBotFolded;
        //private bool hasFourthBotFolded;
        //private bool hasFifthBotFolded;
        private bool intsadded;
        private bool changed;

        //private int playerCall;
        //private int firstBotCall;
        //private int secondBotCall;
        //private int thirdBotCall;
        //private int fourthBotCall;
        //private int fifthBotCall;

        //private int playerRaise;
        //private int firstBotRaise;
        //private int secondBotRaise;
        //private int thirdBotRaise;
        //private int fourthBotRaise;
        //private int fifthBotRaise;

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

        //private bool PlayerFoldTturn;
        //private bool Playerturn = true;
        private bool restart;
        private bool raising;

        Type sorted = new Type();
        string[] ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

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
            //bools.Add(PlayerFoldTturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.Db.Players[0] = this.player;
            this.Db.Players[1] = this.firstBot;
            this.Db.Players[2] = this.secondBot;
            this.Db.Players[3] = this.thirdBot;
            this.Db.Players[4] = this.fourthBot;
            this.Db.Players[5] = this.fifthBot;

            //for (int i = 0; i < this.Db.Players.Length; i++)
            //{
            //    this.Db.Players[i].Cards[0] = 2*i;
            //    this.Db.Players[i].Cards[1] = 2*i + 1;
            //}

            this.table = new PokerTable();
            this.Db.Table = this.table;
            this.call = this.defaultBigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            Shuffle();
            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;   
            this.textBoxBotChips1.Enabled = false;
            this.textBoxBotChips2.Enabled = false;
            this.textBoxBotChips3.Enabled = false;
            this.textBoxBotChips4.Enabled = false;
            this.textBoxBotChips5.Enabled = false;

            this.textBoxChips.Text = "chips : " + this.player.Chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBot.Chips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondBot.Chips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdBot.Chips.ToString();
            this.textBoxBotChips4.Text = "chips : " + this.fourthBot.Chips.ToString();
            this.textBoxBotChips5.Text = "chips : " + this.fifthBot.Chips.ToString();

            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);
            this.Updates.Tick += Update_Tick;
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

        public TextBox[] ChipsTextBoxes
        {

            get
            {
                TextBox[] result = new TextBox[]
                {
                    this.textBoxChips, this.textBoxBotChips1, this.textBoxBotChips2,
                     this.textBoxBotChips3, this.textBoxBotChips4, this.textBoxBotChips5
                };
                return result;
            }

        }


        async Task Shuffle()
        {
            //this.bools.Add(this.PlayerFoldTturn);
            //this.bools.Add(this.B1Fturn);
            //this.bools.Add(this.B2Fturn);
            //this.bools.Add(this.B3Fturn);
            //this.bools.Add(this.B4Fturn);
            //this.bools.Add(this.B5Fturn);
            for (int i = 0; i < Db.Players.Length; i++)
            {
                this.bools.Add(this.Db.Players[i].FoldTurn);
            }
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("..\\..\\Resources\\Assets\\Back\\Back.png");
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
                var charsToRemove = new string[] { "..\\..\\Resources\\Assets\\Cards\\", ".png" };

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
                        this.player.PlayerCards[1] = this.SetCard(this.availableCardsInGame[1]);
                    }

                    this.cardsHolder[0].Tag = this.availableCardsInGame[0];
                    this.player.PlayerCards[0] = this.SetCard(this.availableCardsInGame[0]);
                    this.cardsHolder[cardsInGame].Image = this.cardsImageDeck[cardsInGame];
                    this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom);
                    //cardsHolder[cardsInGame].Dock = DockStyle.Top;
                    this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                    horizontal += this.cardsHolder[cardsInGame].Width;
                    this.Controls.Add(this.player.Panel);
                    this.player.Panel.Location = new Point(this.cardsHolder[0].Left - 10, this.cardsHolder[0].Top - 10);
                    //this.player.Panel.BackColor = Color.DarkBlue;
                    //this.player.Panel.Height = 150;
                    //this.player.Panel.Width = 180;
                    //this.player.Panel.Visible = false;
                }

                if (this.firstBot.Chips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 2 && cardsInGame < 4)     //elica: cards of the first bot with index 2 and 3
                    {
                        if (this.cardsHolder[2].Tag != null)
                        {
                            this.cardsHolder[3].Tag = this.availableCardsInGame[3];
                            this.firstBot.PlayerCards[1] = this.SetCard(this.availableCardsInGame[3]);
                        }

                        this.cardsHolder[2].Tag = this.availableCardsInGame[2];
                        this.firstBot.PlayerCards[0] = this.SetCard(this.availableCardsInGame[2]);
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.firstBot.Panel);
                        this.firstBot.Panel.Location = new Point(this.cardsHolder[2].Left - 10, this.cardsHolder[2].Top - 10);
                        //this.firstBot.Panel.BackColor = Color.DarkBlue;
                        //this.firstBot.Panel.Height = 150;
                        //this.firstBot.Panel.Width = 180;
                        //this.firstBot.Panel.Visible = false;

                        if (cardsInGame == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.secondBot.Chips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 4 && cardsInGame < 6)  // elica: cards of the second bot with index 4 and 5
                    {
                        if (this.cardsHolder[4].Tag != null)
                        {
                            this.cardsHolder[5].Tag = this.availableCardsInGame[5];
                            this.secondBot.PlayerCards[1] = this.SetCard(this.availableCardsInGame[5]);
                        }

                        this.cardsHolder[4].Tag = this.availableCardsInGame[4];
                        this.secondBot.PlayerCards[0] = this.SetCard(this.availableCardsInGame[4]);

                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.secondBot.Panel);
                        this.secondBot.Panel.Location = new Point(this.cardsHolder[4].Left - 10, this.cardsHolder[4].Top - 10);
                        //this.secondBot.Panel.BackColor = Color.DarkBlue;
                        //this.secondBot.Panel.Height = 150;
                        //this.secondBot.Panel.Width = 180;
                        //this.secondBot.Panel.Visible = false;

                        if (cardsInGame == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.thirdBot.Chips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 6 && cardsInGame < 8)    // elica: cards of the third bot with index 6 and 7
                    {
                        if (this.cardsHolder[6].Tag != null)
                        {
                            this.cardsHolder[7].Tag = this.availableCardsInGame[7];
                            this.thirdBot.PlayerCards[1] = this.SetCard(this.availableCardsInGame[7]);
                        }

                        this.cardsHolder[6].Tag = this.availableCardsInGame[6];
                        this.thirdBot.PlayerCards[0] = this.SetCard(this.availableCardsInGame[6]);
                         
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame;
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.thirdBot.Panel);
                        this.thirdBot.Panel.Location = new Point(this.cardsHolder[6].Left - 10, this.cardsHolder[6].Top - 10);
                        //this.thirdBot.Panel.BackColor = Color.DarkBlue;
                        //this.thirdBot.Panel.Height = 150;
                        //this.thirdBot.Panel.Width = 180;
                        //this.thirdBot.Panel.Visible = false;

                        if (cardsInGame == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fourthBot.Chips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 8 && cardsInGame < 10)  // elica: cards of the fourth bot with index 8 and 9
                    {
                        if (this.cardsHolder[8].Tag != null)
                        {
                            this.cardsHolder[9].Tag = this.availableCardsInGame[9];
                            this.fourthBot.PlayerCards[1] = this.SetCard(this.availableCardsInGame[9]);
                        }

                        this.cardsHolder[8].Tag = this.availableCardsInGame[8];
                        this.fourthBot.PlayerCards[0] = this.SetCard(this.availableCardsInGame[8]);

                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.fourthBot.Panel);
                        this.fourthBot.Panel.Location = new Point(this.cardsHolder[8].Left - 10, this.cardsHolder[8].Top - 10);
                        //this.fourthBot.Panel.BackColor = Color.DarkBlue;
                        //this.fourthBot.Panel.Height = 150;
                        //this.fourthBot.Panel.Width = 180;
                        //this.fourthBot.Panel.Visible = false;

                        if (cardsInGame == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.fifthBot.Chips > 0)
                {
                    this.foldedPlayers--;

                    if (cardsInGame >= 10 && cardsInGame < 12)   // elica: cards of the fifth bot with index 10 and 11
                    {
                        if (this.cardsHolder[10].Tag != null)
                        {
                            this.cardsHolder[11].Tag = this.availableCardsInGame[11];
                            this.fifthBot.PlayerCards[1] = this.SetCard(this.availableCardsInGame[11]);
                        }

                        this.cardsHolder[10].Tag = this.availableCardsInGame[10];
                        this.fifthBot.PlayerCards[0] = this.SetCard(this.availableCardsInGame[10]);

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.Controls.Add(this.fifthBot.Panel);
                        this.fifthBot.Panel.Location = new Point(this.cardsHolder[10].Left - 10, this.cardsHolder[10].Top - 10);
                        //this.fifthBot.Panel.BackColor = Color.DarkBlue;
                        //this.fifthBot.Panel.Height = 150;
                        //this.fifthBot.Panel.Width = 180;
                        //this.fifthBot.Panel.Visible = false;

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
                    this.table.CardsOnTable[0] = this.SetCard(this.availableCardsInGame[12]);

                    if (cardsInGame > 12)
                    {
                        this.cardsHolder[13].Tag = this.availableCardsInGame[13];
                        this.table.CardsOnTable[1] = this.SetCard(this.availableCardsInGame[13]);
                    }

                    if (cardsInGame > 13)
                    {
                        this.cardsHolder[14].Tag = this.availableCardsInGame[14];
                        this.table.CardsOnTable[2] = this.SetCard(this.availableCardsInGame[14]);
                    }

                    if (cardsInGame > 14)
                    {
                        this.cardsHolder[15].Tag = this.availableCardsInGame[15];
                        this.table.CardsOnTable[3] = this.SetCard(this.availableCardsInGame[15]);
                    }

                    if (cardsInGame > 15)
                    {
                        this.cardsHolder[16].Tag = this.availableCardsInGame[16];
                        this.table.CardsOnTable[4] = this.SetCard(this.availableCardsInGame[16]);

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

                //for (int i = 1; i <this.Db.Players.Length; i++)
                //{
                //    if (Db.Players[i].Chips<=0 )
                //    {
                //        this.Db.Players[i].FoldTurn = true;
                //        this.cardsHolder[i*2].Visible = false;
                //        this.cardsHolder[(i * 2)+1].Visible = false;
                //    }
                //}
                for (int i = 1; i <this.Db.Players.Length; i++)
                {
                    if (this.Db.Players[i].Chips<=0)
                    {
                        this.Db.Players[i].FoldTurn = true;
                        this.cardsHolder[2*i].Visible = false;
                        this.cardsHolder[2*i+1].Visible = false;
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

                //if (this.secondBotChips <= 0)
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

                //if (this.thirdBotChips <= 0)
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
            if (!this.player.FoldTurn)
            {
                if (this.player.IsPlayerTurn)
                {
                    FixCall(this.player, 1);
                    MessageBox.Show("Your turn");
                    this.pokerBetTimer.Visible = true;
                    this.pokerBetTimer.Value = 1000;
                    this.t = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    FixCall(this.player, 2);
                }
            }

            if (this.player.FoldTurn || !this.player.IsPlayerTurn)
            {
                await AllIn();
                if (this.player.FoldTurn && !this.player.HasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.player.HasPlayerFolded = true;
                    }
                }

                await CheckRaise( 0);
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
                        FixCall(firstBot, 1);
                        FixCall(firstBot, 2);
                        //Rules(this.firstBot);
                        this.rules.CheckForHand(this.firstBot);
                        MessageBox.Show(this.firstBot.Name);
                        AI(this.firstBot,2,3);
                        this.turnCount++;
                        this.last = 1;
                        this.firstBot.IsPlayerTurn = false;
                        this.secondBot.IsPlayerTurn = true;
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
                    await CheckRaise( 1);
                    this.secondBot.IsPlayerTurn = true;
                }

                if (!this.secondBot.FoldTurn)
                {
                    if (this.secondBot.IsPlayerTurn)
                    {
                        FixCall(secondBot, 1);
                        FixCall(secondBot, 2);
                        //Rules(this.secondBot);
                        this.rules.CheckForHand(this.secondBot);
                        MessageBox.Show(this.secondBot.Name);
                        AI(this.secondBot,4,5);
                        this.turnCount++;
                        this.last = 2;
                        this.secondBot.IsPlayerTurn = false;
                        this.thirdBot.IsPlayerTurn = true;
                    }
                }

                if (this.secondBot.FoldTurn && !this.secondBot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.secondBot.HasPlayerFolded = true;
                }

                if (this.secondBot.FoldTurn || !this.secondBot.IsPlayerTurn)
                {
                    await CheckRaise( 2);
                    this.thirdBot.IsPlayerTurn = true;
                }

                if (!this.thirdBot.FoldTurn)
                {
                    if (this.thirdBot.IsPlayerTurn)
                    {
                        FixCall(this.thirdBot, 1);
                        FixCall(this.thirdBot, 2);
                        //Rules(this.thirdBot);
                        this.rules.CheckForHand(this.thirdBot);
                        MessageBox.Show(this.thirdBot.Name);
                        AI(this.thirdBot,6,7);
                        this.turnCount++;
                        this.last = 3;
                        this.thirdBot.IsPlayerTurn = false;
                        this.fourthBot.IsPlayerTurn = true;
                    }
                }

                if (this.thirdBot.FoldTurn && !this.thirdBot.HasPlayerFolded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.thirdBot.HasPlayerFolded = true;
                }

                if (this.thirdBot.FoldTurn || !this.thirdBot.IsPlayerTurn)
                {
                    await CheckRaise(3 );
                    this.fourthBot.IsPlayerTurn = true;
                }

                if (!this.fourthBot.FoldTurn)
                {
                    if (this.fourthBot.IsPlayerTurn)
                    {
                        FixCall(this.fourthBot, 1);
                        FixCall(this.fourthBot, 2);
                        //Rules(this.fourthBot);
                        this.rules.CheckForHand(this.fourthBot);
                        MessageBox.Show(this.fourthBot.Name);
                        AI(this.fourthBot,8,9);
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
                    await CheckRaise( 4);
                    this.fifthBot.IsPlayerTurn = true;
                }

                if (!this.fifthBot.FoldTurn)
                {
                    if (this.fifthBot.IsPlayerTurn)
                    {
                        FixCall(this.fifthBot, 1);
                        FixCall(this.fifthBot, 2);
                        //Rules(this.fifthBot);
                        this.rules.CheckForHand(this.fifthBot);
                        MessageBox.Show(this.fifthBot.Name);
                        AI(this.fifthBot,10,11);
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
                    await CheckRaise( 5);
                    this.player.IsPlayerTurn = true;
                }

                if (this.player.FoldTurn && !this.player.HasPlayerFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.player.HasPlayerFolded = true;
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
        } //elica: end of method turns

        //void Rules(Player player)
        //{
        //    //if (player.Cards[1] == 0 && player.Cards[2] == 1)        //elica: Empty if statement
        //    //{
        //    //}

        //    if (!player.FoldTurn || player.Cards[1] == 0 && player.Cards[2] == 1 && this.player.Status.Text.Contains("Fold") == false)
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

        void Winner(Player player, string lastly)
        {
            if (lastly == " ")
            {
                lastly = this.fifthBot.Name;
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (this.cardsHolder[j].Visible)
                {
                    this.cardsHolder[j].Image = this.cardsImageDeck[j];
                }
                    
            }

            if (player.Current == this.sorted.Current)
            {
                if (player.Power == this.sorted.Power)
                {
                    this.winners++;
                    this.CheckWinners.Add(player.Name);

                    //TODO if statement to switch
                    if (player.Current == -1)
                    {
                        MessageBox.Show(player.Name + " High Card ");
                    }

                    if (player.Current == 1 || player.Current == 0)
                    {
                        MessageBox.Show(player.Name + " Pair ");
                    }

                    if (player.Current == 2)
                    {
                        MessageBox.Show(player.Name + " Two Pair ");
                    }

                    if (player.Current == 3)
                    {
                        MessageBox.Show(player.Name + " Three of a Kind ");
                    }

                    if (player.Current == 4)
                    {
                        MessageBox.Show(player.Name + " Straight ");
                    }

                    if (player.Current == 5 || player.Current == 5.5)
                    {
                        MessageBox.Show(player.Name + " Flush ");
                    }

                    if (player.Current == 6)
                    {
                        MessageBox.Show(player.Name + " Full House ");
                    }

                    if (player.Current == 7)
                    {
                        MessageBox.Show(player.Name + " Four of a Kind ");
                    }

                    if (player.Current == 8)
                    {
                        MessageBox.Show(player.Name + " Straight Flush ");
                    }

                    if (player.Current == 9)
                    {
                        MessageBox.Show(player.Name + " Royal Flush ! ");
                    }
                }
            }

            if (player.Name == lastly)//lastfixed
            {
                if (this.winners > 1)
                {
                    if (this.CheckWinners.Contains(this.player.Name))
                    {
                        this.player.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxChips.Text = this.player.Chips.ToString();
                        //player.Panel.Visible = true;

                    }

                    if (this.CheckWinners.Contains(this.firstBot.Name))
                    {
                        this.firstBot.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxBotChips1.Text = this.firstBot.Chips.ToString();
                        //firstBot.Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.secondBot.Name))
                    {
                        this.secondBot.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxBotChips2.Text = this.secondBot.Chips.ToString();
                        //secondBot.Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.thirdBot.Name))
                    {
                        this.thirdBot.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxBotChips3.Text = this.thirdBot.Chips.ToString();
                        //thirdBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.fourthBot.Name))
                    {
                        this.fourthBot.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxBotChips4.Text = this.fourthBot.Chips.ToString();
                        //fourthBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.fifthBot.Name))
                    {
                        this.fifthBot.Chips += int.Parse(this.textBoxPot.Text) /this.winners;
                        this.textBoxBotChips5.Text = this.fifthBot.Chips.ToString();
                        //fifthBot.Panel.Visible = true;
                    }
                    //await Finish(1);
                }

                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains(this.player.Name))
                    {
                        this.player.Chips += int.Parse(this.textBoxPot.Text) ;
                        
                        //player.Panel.Visible = true;

                    }

                    if (this.CheckWinners.Contains(this.firstBot.Name))
                    {
                        this.firstBot.Chips += int.Parse(this.textBoxPot.Text) ;
                        
                        //firstBot.Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.secondBot.Name))
                    {
                        this.secondBot.Chips += int.Parse(this.textBoxPot.Text) ;
                        
                        //secondBot.Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.thirdBot.Name))
                    {
                        this.thirdBot.Chips += int.Parse(this.textBoxPot.Text) ;
                       
                        //thirdBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.fourthBot.Name))
                    {
                        this.fourthBot.Chips += int.Parse(this.textBoxPot.Text) ;
                        
                        //fourthBotPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains(this.fifthBot.Name))
                    {
                        this.fifthBot.Chips += int.Parse(this.textBoxPot.Text) ;
                        
                        //fifthBot.Panel.Visible = true;
                    }
                }
            }
        }

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

                        foreach (var item in this.Db.Players)
                        {
                            item.Status.Text = "";
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
                        foreach (var item in this.Db.Players)
                        {
                            item.Call=0;
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
                        foreach (var item in this.Db.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                        //this.playerCall = 0;
                        //this.firstBotCall = 0;
                        //this.secondBotCall = 0;
                        //this.thirdBotCall = 0;
                        //this.fourthBotCall = 0;
                        //this.fifthBotCall = 0;

                        //this.playerRaise = 0;
                        //this.firstBotRaise = 0;
                        //this.secondBotRaise = 0;
                        //this.thirdBotRaise = 0;
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
                        foreach (var item in this.Db.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                        //this.playerCall = 0;
                        //this.firstBotCall = 0;
                        //this.secondBotCall = 0;
                        //this.thirdBotCall = 0;
                        //this.fourthBotCall = 0;
                        //this.fifthBotCall = 0;

                        //this.playerRaise = 0;
                        //this.firstBotRaise = 0;
                        //this.secondBotRaise = 0;
                        //this.thirdBotRaise = 0;
                        //this.fourthBotRaise = 0;
                        //this.fifthBotRaise = 0;
                    }
                }
            }

            if (this.rounds == this.end && this.maxLeft == 6)
            {
                string fixedLast = "qwerty";

                foreach (var player in this.Db.Players)
                {
                    if (!player.Status.Text.Contains("Fold"))
                    {
                        fixedLast = player.Name;
                        //Rules(player);
                        this.rules.CheckForHand(player);
                        Winner(player, fixedLast);
                        player.FoldTurn = false;
                    }
                }
                

                //Winner(this.playerType, this.playerPower, "Player", this.chips, fixedLast);
                //Winner(this.firstBotType, this.firstBotPower, "Bot 1", this.firstBotChips, fixedLast);
                //Winner(this.secondBotType, this.secondBotPower, "Bot 2", this.secondBotChips, fixedLast);
                //Winner(this.thirdBotType, this.thirdBotPower, "Bot 3", this.thirdBotChips, fixedLast);
                //Winner(this.fourthBotType, this.fourthBotPower, "Bot 4", this.fourthBotChips, fixedLast);
                //Winner(this.fifthBotType, this.fifthBotPower, "Bot 5", this.fifthBotChips, fixedLast);
                this.restart = true;
                this.player.IsPlayerTurn = true;
                //this.PlayerFoldTturn = false;
                //this.B1Fturn = false;
                //this.B2Fturn = false;
                //this.B3Fturn = false;
                //this.B4Fturn = false;
                //this.B5Fturn = false;

                if (this.player.Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.player.Chips = f2.a;
                        for (int i = 1; i <this.Db.Players.Length; i++)
                        {
                            this.Db.Players[i].Chips += f2.a;
                        }
                        //this.firstBotChips += f2.a;
                        //this.secondBotChips += f2.a;
                        //this.thirdBotChips += f2.a;
                        //this.fourthBotChips += f2.a;
                        //this.fifthBotChips += f2.a;
                        this.player.FoldTurn = false;
                        this.player.IsPlayerTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = "Raise";
                    }
                }

                //this.player.Panel.Visible = false;
                //this.firstBot.Panel.Visible = false;
                //this.secondBot.Panel.Visible = false;
                //this.thirdBotPanel.Visible = false;
                //this.fourthBotPanel.Visible = false;
                //this.fifthBot.Panel.Visible = false;
                foreach (var player in this.Db.Players)
                {
                    player.Panel.Visible = false;
                    player.Call = 0;
                    player.Raise = 0;
                    player.Power = 0;
                    player.Current = -1;
                }
                //this.playerCall = 0;
                //this.firstBotCall = 0;
                //this.secondBotCall = 0;
                //this.thirdBotCall = 0;
                //this.fourthBotCall = 0;
                //this.fifthBotCall = 0;

                //this.playerRaise = 0;
                //this.firstBotRaise = 0;
                //this.secondBotRaise = 0;
                //this.thirdBotRaise = 0;
                //this.fourthBotRaise = 0;
                //this.fifthBotRaise = 0;

                this.last = 0;
                this.call = this.defaultBigBlind;
                this.raise = 0;
                this.ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.rounds = 0;
                //this.playerPower = 0;
                //this.playerType = -1;
                this.type = 0;

                //this.firstBotPower = 0;
                //this.secondBotPower = 0;
                //this.thirdBotPower = 0;
                //this.fourthBotPower = 0;
                //this.fifthBotPower = 0;

                //this.firstBotType = -1;
                //this.secondBotType = -1;
                //this.thirdBotType = -1;
                //this.fourthBotType = -1;
                //this.fifthBotType = -1;

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

                this.textBoxPot.Text = "0";
                this.player.Status.Text = "";
                await Shuffle();
                await Turns();
            }
        }  

        void FixCall(Player player, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (player.Status.Text.Contains("Raise"))
                    {
                        var changeRaise = player.Status.Text.Substring(6);
                        player.Raise = int.Parse(changeRaise);
                    }

                    if (player.Status.Text.Contains("Call"))
                    {
                        var changeCall = player.Status.Text.Substring(5);
                        player.Call = int.Parse(changeCall);
                    }

                    if (player.Status.Text.Contains("Check"))
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
            if (this.player.Chips <= 0 && !this.intsadded)
            {
                if (this.player.Status.Text.Contains("Raise"))
                {
                    this.ints.Add(this.player.Chips);
                    this.intsadded = true;
                }

                if (this.player.Status.Text.Contains("Call"))
                {
                    this.ints.Add(this.player.Chips);
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

            if (this.secondBot.Chips <= 0 && !this.secondBot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.secondBot.Chips);
                    this.intsadded = true;
                }
                this.intsadded = false;
            }

            if (this.thirdBot.Chips <= 0 && !this.thirdBot.FoldTurn)
            {
                if (!this.intsadded)
                {
                    this.ints.Add(this.thirdBot.Chips);
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

                // TODO if statement to switch
                if (index == 0)
                {
                    this.player.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.player.Chips.ToString();
                    this.player.Panel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    this.firstBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.firstBot.Chips.ToString();
                    this.firstBot.Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    this.secondBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.secondBot.Chips.ToString();
                    this.secondBot.Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    this.thirdBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.thirdBot.Chips.ToString();
                    this.thirdBot.Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    this.fourthBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fourthBot.Chips.ToString();
                    this.fourthBot.Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    this.fifthBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.fifthBot.Chips.ToString();
                    this.fifthBot.Panel.Visible = true;
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
            this.player.Panel.Visible = false;
            this.firstBot.Panel.Visible = false;
            this.secondBot.Panel.Visible = false;
            this.thirdBot.Panel.Visible = false;
            this.fourthBot.Panel.Visible = false;
            this.fifthBot.Panel.Visible = false;

            this.call = this.defaultBigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;

            this.firstBot.Power = 0;
            this.secondBot.Power = 0;
            this.thirdBot.Power = 0;
            this.fourthBot.Power = 0;
            this.fifthBot.Power = 0;
            this.player.Power = 0;

            //this.playerType = -1;
            this.raise = 0;
            foreach (var player in this.Db.Players)
            {
                player.Current = -1;
                player.Call = 0;
                player.Raise = 0;
                player.FoldTurn = false;
                player.HasPlayerFolded = false;
                player.Status.Text = "";
            }
            for (int i = 1; i < this.Db.Players.Length; i++)
            {
                this.Db.Players[i].IsPlayerTurn = false;
            }
            this.player.IsPlayerTurn = true;
            //this.firstBotType = -1;
            //this.secondBotType = -1;
            //this.thirdBotType = -1;
            //this.fourthBotType = -1;
            //this.fifthBotType = -1;

            //this.isFirstBotTurn = false;
            //this.isSecondBotTurn = false;
            //this.isThirdBotTurn = false;
            //this.isFourthBotTurn = false;
            //this.isFifthBotTurn = false;

            //this.B1Fturn = false;
            //this.B2Fturn = false;
            //this.B3Fturn = false;
            //this.B4Fturn = false;
            //this.B5Fturn = false;

            //this.hasPlayerFolded = false;
            //this.hasFirstBotFolded = false;
            //this.hasSecondBotFolded = false;
            //this.hasThirdBotFolded = false;
            //this.hasFourthBotFolded = false;
            //this.hasFifthBotFolded = false;

            //this.PlayerFoldTturn = false;
            
            this.restart = false;
            this.raising = false;

            //this.playerCall = 0;
            //this.firstBotCall = 0;
            //this.secondBotCall = 0;
            //this.thirdBotCall = 0;
            //this.fourthBotCall = 0;
            //this.fifthBotCall = 0;

            //this.playerRaise = 0;
            //this.firstBotRaise = 0;
            //this.secondBotRaise = 0;
            //this.thirdBotRaise = 0;
            //this.fourthBotRaise = 0;
            //this.fifthBotRaise = 0;

            this.height = 0;
            this.width = 0;
            this.winners = 0;
            
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
            this.textBoxPot.Text = "0";
            this.t = 60;
            this.up = 10000000;
            this.turnCount = 0;
            //this.playerStatus.Text = "";
            //this.firstBotStatus.Text = "";
            //this.secondBotStatus.Text = "";
            //this.thirdBotStatus.Text = "";
            //this.fourthBotStatus.Text = "";
            //this.fifthBotStatus.Text = "";
            if (this.player.Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.player.Chips = f2.a;
                    this.firstBot.Chips += f2.a;
                    this.secondBot.Chips += f2.a;
                    this.thirdBot.Chips += f2.a;
                    this.fourthBot.Chips += f2.a;
                    this.fifthBot.Chips += f2.a;
                    this.player.FoldTurn = false;
                    this.player.IsPlayerTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }

            this.ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
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

            foreach (var player in this.Db.Players)
            {
                if (!player.Status.Text.Contains("Fold"))
                {
                    fixedLast = player.Name;
                    //Rules(player);
                    this.rules.CheckForHand(player);
                    Winner(player, fixedLast);
                }
            }
            
            
        }

        void AI(Player player, int indexFirstCard, int indexSecondCard)
        {
            if (!player.FoldTurn)
            {
                if (player.Current == -1)
                {
                    HighCard( player);
                }
                
                if (player.Current == 0)
                {
                    PairTable(player);
                }

                if (player.Current == 1)
                {
                    PairHand(player);
                }

                if (player.Current == 2)
                {
                    TwoPair(player);
                }

                if (player.Current == 3)
                {
                    ThreeOfAKind( player);
                }

                if (player.Current == 4)
                {
                    Straight(player);
                }

                if (player.Current == 5 || player.Current == 5.5)
                {
                    Flush(player);
                }

                if (player.Current == 6)
                {
                    FullHouse(player);
                }

                if (player.Current == 7)
                {
                    FourOfAKind(player);
                }

                if (player.Current == 8 || player.Current == 9)
                {
                    StraightFlush(player);
                }
            }

            if (player.FoldTurn)
            {
                this.cardsHolder[indexFirstCard].Visible = false;
                this.cardsHolder[indexSecondCard].Visible = false;
            }
        }

        private void HighCard( Player player)
        {
            HP(player, 20, 25);
        }

        private void PairTable(Player player)
        {
            HP(player, 16, 25);
        }

        private void PairHand(Player player)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (player.Power <= 199 && player.Power >= 140)
            {
                PH(player, rCall, 6, rRaise);
            }

            if (player.Power <= 139 && player.Power >= 128)
            {
                PH(player, rCall, 7, rRaise);
            }

            if (player.Power < 128 && player.Power >= 101)
            {
                PH(player, rCall, 9, rRaise);
            }
        }

        private void TwoPair(Player player)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (player.Power <= 290 && player.Power >= 246)
            {
                PH(player, rCall, 3, rRaise);
            }

            if (player.Power <= 244 && player.Power >= 234)
            {
                PH(player, rCall, 4, rRaise);
            }

            if (player.Power < 234 && player.Power >= 201)
            {
                PH(player, rCall, 4, rRaise);
            }
        }

        private void ThreeOfAKind(Player player)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Power <= 390 && player.Power >= 330)
            {
                Smooth( player, tRaise);
            }

            if (player.Power <= 327 && player.Power >= 321)//10  8
            {
                Smooth(player, tRaise);
            }

            if (player.Power < 321 && player.Power >= 303)//7 2
            {
                Smooth(player, tRaise);
            }
        }

        private void Straight(Player player)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Power <= 480 && player.Power >= 410)
            {
                Smooth(player, sRaise);
            }

            if (player.Power <= 409 &&player.Power >= 407)//10  8
            {
                Smooth(player, sRaise);
            }

            if (player.Power < 407 && player.Power >= 404)
            {
                Smooth(player, sRaise);
            }
        }

        private void Flush(Player player)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(player, fRaise);
        }

        private void FullHouse(Player player)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (player.Power <= 626 && player.Power >= 620)
            {
                Smooth(player, fhRaise);
            }

            if (player.Power < 620 && player.Power >= 602)
            {
                Smooth(player, fhRaise);
            }
        }

        private void FourOfAKind(Player playertPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Power <= 752 && player.Power >= 704)
            {
                Smooth(player, fkRaise);
            }
        }

        private void StraightFlush(Player player)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Power <= 913 && player.Power >= 804)
            {
                Smooth(player, sfRaise);
            }
        }

        private void Fold( Player player)
        {
            this.raising = false;
            player.Status.Text = "Fold";
            player.IsPlayerTurn = false;
            player.FoldTurn = true;
        }

        private void Check(Player player)
        {
            player.Status.Text = "Check";
            player.IsPlayerTurn = false;
            this.raising = false;
        }

        private void Call(Player player)
        {
            this.raising = false;
            player.IsPlayerTurn = false;
            player.Chips -= this.call;
            player.Status.Text = "Call " + this.call;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
        }

        private void Raised( Player player)
        {
            player.Chips -= Convert.ToInt32(this.raise);
            player.Status.Text = "raise " + this.raise;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.raise)).ToString();
            this.call = Convert.ToInt32(this.raise);
            this.raising = true;
            player.IsPlayerTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        /// <summary>
        /// Creates a choice generator for bots.
        /// This generator is used when bot has only high card or table pair hand.
        /// </summary>
        /// <param name="sChips">Bot's chips</param>     //botChips
        /// <param name="sTurn">Parameter that indicates if it's current player's turn</param>   //botTurn
        /// <param name="sFTurn">Parameter that indicates if current bot has folded or is all in</param>   //botFoldsTurn
        /// <param name="sStatus">The status text of the bot</param>    //botStatus
        /// <param name="botPower">The bot's hand category factor</param>   //botHandRankFactor
        /// <param name="n">The n parameter</param>
        /// <param name="n1">The n1 parameter</param>        
        private void HP(Player player, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                Check( player);
            }

            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(player.Chips, n))
                    {
                        Call(player);
                    }
                    else
                    {
                        Fold(player);
                    }
                }

                if (rnd == 2)
                {
                    if (this.call <= RoundN(player.Chips, n1))
                    {
                        Call(player);
                    }
                    else
                    {
                        Fold(player);
                    }
                }
            }

            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = this.call * 2;
                    Raised(player);
                }
                else
                {
                    if (this.raise <= RoundN(player.Chips, n))
                    {
                        this.raise = this.call * 2;
                        Raised(player);
                    }
                    else
                    {
                        Fold(player);
                    }
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldTurn = true;
            }
        }

        /// <summary>
        /// Choice maker for bots if they have a hand which is a pair or two pairs.
        /// Uses BotChoiceFormula formula.
        /// </summary>
        /// <param name="sChips">Bot's chips</param>  //botChips
        /// <param name="sTurn">Parameter that indicates if it's current player's turn</param>   //botTurn
        /// <param name="sFTurn">Parameter that indicates if current bot has folded or is all in</param>  //botFoldsTurn
        /// <param name="sStatus">The status text of the bot</param>  //botStatus
        /// <param name="n">The n parameter</param>
        /// <param name="n1">The n1 parameter</param>
        /// <param name="r">The randGenerator parameter</param>   //randGenerator
        private void PH(Player player, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    Check(player);
                }

                if (this.call > 0)
                {
                    if (this.call >= RoundN(player.Chips, n1))
                    {
                        Fold(player);
                    }

                    if (this.raise > RoundN(player.Chips, n))
                    {
                        Fold(player);
                    }

                    if (!player.FoldTurn)
                    {
                        if (this.call >= RoundN(player.Chips, n) && this.call <= RoundN(player.Chips, n1))
                        {
                            Call(player);
                        }

                        if (this.raise <= RoundN(player.Chips, n) && this.raise >= (RoundN(player.Chips, n)) / 2)
                        {
                            Call(player);
                        }

                        if (this.raise <= (RoundN(player.Chips, n)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(player.Chips, n);
                                Raised(player);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                Raised(player);
                            }
                        }

                    }
                }
            }

            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(player.Chips, n1 - rnd))
                    {
                        Fold(player);
                    }

                    if (this.raise > RoundN(player.Chips, n - rnd))
                    {
                        Fold(player);
                    }

                    if (!player.FoldTurn)
                    {
                        if (this.call >= RoundN(player.Chips, n - rnd) && this.call <= RoundN(player.Chips, n1 - rnd))
                        {
                            Call(player);
                        }

                        if (this.raise <= RoundN(player.Chips, n - rnd) && this.raise >= (RoundN(player.Chips, n - rnd)) / 2)
                        {
                            Call(player);
                        }

                        if (this.raise <= (RoundN(player.Chips, n - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(player.Chips, n - rnd);
                                Raised(player);
                            }
                            else
                            {
                                this.raise = this.call * 2;
                                Raised(player);
                            }
                        }
                    }
                }

                if (this.call <= 0)
                {
                    this.raise = RoundN(player.Chips, r - rnd);
                    Raised(player);
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldTurn = true;
            }
        }

        /// <summary>
        /// Choice maker for bots with a hand three of a kind or higher.
        /// Uses BotChoiceFormula formula.
        /// </summary>
        /// <param name="botChips">Bot's chips</param>
        /// <param name="botTurn">Parameter that indicates if it's current player's turn</param>
        /// <param name="botFTurn">Parameter that indicates if current bot has folded or is all in</param>
        /// <param name="botStatus">The status text of the bot</param>
        /// <param name="name">The current bot's name</param>   //  botName
        /// <param name="n">The n parameter</param>
        /// <param name="r">The randGenerator parameter</param>  //randGenerator       
        void Smooth(Player player,  int n)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                Check(player);
            }
            else
            {
                if (this.call >= RoundN(player.Chips, n))
                {
                    if (player.Chips > this.call)
                    {
                        Call(player);
                    }
                    else if (player.Chips <= this.call)
                    {
                        this.raising = false;
                        player.Turn = false;
                        player.Chips = 0;
                        player.Status.Text = "Call " + player.Chips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + player.Chips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (player.Chips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            Raised(player);
                        }
                        else
                        {
                            Call(player);
                        }
                    }
                    else
                    {
                        this.raise = this.call * 2;
                        Raised(player);
                    }
                }
            }

            if (player.Chips <= 0)
            {
                player.IsPlayerTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (this.pokerBetTimer.Value <= 0)
            {
                this.player.FoldTurn = true;
                await Turns();
            }

            if (this.t > 0)
            {
                this.t--;
                this.pokerBetTimer.Value = (this.t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            

            this.textBoxChips.Text = "chips : " + this.player.Chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBot.Chips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondBot.Chips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdBot.Chips.ToString();
            this.textBoxBotChips4.Text = "chips : " + this.fourthBot.Chips.ToString();
            this.textBoxBotChips5.Text = "chips : " + this.fifthBot.Chips.ToString();

            if (this.player.Chips <= 0)
            {
                this.player.IsPlayerTurn = false;
                this.player.FoldTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (this.up > 0)
            {
                this.up--;
            }

            if (this.player.Chips >= this.call)
            {
                this.buttonCall.Text = "Call " + this.call.ToString();
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (this.call > 0)
            {
                this.buttonCheck.Enabled = false;
            }
            else           //elica: change  if (call <= 0) with else
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            if (this.player.Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.player.Chips <= int.Parse(this.textBoxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }

            if (this.player.Chips < this.call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void buttonFold_IsClicked(object sender, EventArgs e)
        {
            this.player.Status.Text = "Fold";
            this.player.IsPlayerTurn = false;
            this.player.FoldTurn = true;
            await Turns();
        }

        private async void buttonCheck_IsClicked(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.player.IsPlayerTurn = false;
                this.player.Status.Text = "Check";
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
            //Rules(this.player);
            this.rules.CheckForHand(this.player);
            if (this.player.Chips >= this.call)
            {
                this.player.Chips -= this.call;
                this.textBoxChips.Text = "chips : " + this.player.Chips.ToString();

                if (this.textBoxPot.Text != "")
                {
                    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                }
                else
                {
                    this.textBoxPot.Text = this.call.ToString();
                }

                this.player.IsPlayerTurn = false;
                this.player.Status.Text = "Call " + this.call;
                this.player.Call = this.call;
            }
            else if (this.player.Chips <= this.call && this.call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.player.Chips).ToString();
                this.player.Status.Text = "All in " + this.player.Chips;
                this.player.Chips = 0;
                this.textBoxChips.Text = "chips : " + this.player.Chips.ToString();
                this.player.IsPlayerTurn = false;
                this.buttonFold.Enabled = false;
                this.player.Call = this.player.Chips;
            }
            await Turns();
        }

        private async void buttonRaise_IsClicked(object sender, EventArgs e)
        {
            //Rules(this.player);
            this.rules.CheckForHand(this.player);
            int parsedValue;
            if (this.textBoxRaise.Text != "" && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.player.Chips > this.call)
                {
                    if (this.raise * 2 > int.Parse(this.textBoxRaise.Text))
                    {
                        this.textBoxRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.player.Chips >= int.Parse(this.textBoxRaise.Text))
                        {
                            this.call = int.Parse(this.textBoxRaise.Text);
                            this.raise = int.Parse(this.textBoxRaise.Text);
                            this.player.Status.Text = "raise " + this.call.ToString();
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                            this.buttonCall.Text = "Call";
                            this.player.Chips -= int.Parse(this.textBoxRaise.Text);
                            this.raising = true;
                            this.last = 0;
                            this.player.Raise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            this.call = this.player.Chips;
                            this.raise = this.player.Chips;
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.player.Chips).ToString();
                            this.player.Status.Text = "raise " + this.call.ToString();
                            this.player.Chips = 0;
                            this.raising = true;
                            this.last = 0;
                            this.player.Raise = Convert.ToInt32(this.raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.player.IsPlayerTurn = false;
            await Turns();
        }

        private void buttonAddChips_IsClicked(object sender, EventArgs e)
        {
            if (this.tbAdd.Text == "")
            {
                //elica Empty if statement
            }
            else
            {
                this.player.Chips += int.Parse(this.tbAdd.Text);
                this.firstBot.Chips += int.Parse(this.tbAdd.Text);
                this.secondBot.Chips += int.Parse(this.tbAdd.Text);
                this.thirdBot.Chips += int.Parse(this.tbAdd.Text);
                this.fourthBot.Chips += int.Parse(this.tbAdd.Text);
                this.fifthBot.Chips += int.Parse(this.tbAdd.Text);
            }

            this.textBoxChips.Text = "chips : " + this.player.Chips.ToString();
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

        private ICard SetCard(int card)
        {
            var suit = this.cardHandler.GetSuit(card);
            var value = this.cardHandler.GetValue(card);
            return new Card(suit, value);
        }
    }
}