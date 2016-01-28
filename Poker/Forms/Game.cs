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
    using Enums;
    using GlobalConstants;
    using Interfaces;
    using Models.Player;
    using Models.PokerManagement;

    public partial class Game : Form
    {
        private readonly IPlayer humanPlayer = new Human(GlobalConstants.HumanPlayerName);
        private readonly IPlayer firstBot = new Bot(GlobalConstants.FirstBotPlayerName);
        private readonly IPlayer secondBot = new Bot(GlobalConstants.SecondbotPlayerName);
        private readonly IPlayer thirdBot = new Bot(GlobalConstants.ThirdbotPlayerName);
        private readonly IPlayer fourthBot = new Bot(GlobalConstants.FourthBotPlayerName);
        private readonly IPlayer fifthBot = new Bot(GlobalConstants.FifthBotPlayerName);
        private readonly Database dataBase = Database.Instace;
        private readonly PokerTable table;
        private readonly List<bool?> inactivePlayers;
        private readonly List<int> ints;
        private readonly int[] availableCardsInGame;
        private readonly Image[] cardsImageDeck;
        private readonly PictureBox[] cardsHolder;
        private readonly Timer timer;
        private readonly Timer updates;

        private IRules rules = new Rules();
        private int call = GlobalConstants.DefaultValueOfBigBlind;
        private int foldedPlayers = 5;
        private double rounds;
        private double raise;
        private bool intsadded;
        private bool changed;
        private int height;
        private int width;

        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;

        private int last;
        private int raisedTurn = 1;

        private bool restart;
        private bool raising;
        string[] imgLocation = Directory.GetFiles(GlobalConstants.PlayingCardsDirectoryPath,
           GlobalConstants.PlayingCardsWithPngExtension,
           SearchOption.TopDirectoryOnly);

        private int t = 60;
        private int defaultBigBlind = GlobalConstants.DefaultValueOfBigBlind;
        private int defaultSmallBlind = GlobalConstants.DefaultValueOfSmallBlind;
        private int up = 10000000;
        private int turnCount;

        public Game()
        {
            this.availableCardsInGame = new int[17];
            this.cardsImageDeck = new Image[52];
            this.cardsHolder = new PictureBox[52];
            this.timer = new Timer();
            this.updates = new Timer();
            this.ints = new List<int>();
            this.inactivePlayers = new List<bool?>();
            this.dataBase.Players[0] = this.humanPlayer;
            this.dataBase.Players[1] = this.firstBot;
            this.dataBase.Players[2] = this.secondBot;
            this.dataBase.Players[3] = this.thirdBot;
            this.dataBase.Players[4] = this.fourthBot;
            this.dataBase.Players[5] = this.fifthBot;
            this.table = new PokerTable();
            this.dataBase.Table = this.table;
            this.call = this.defaultBigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.updates.Start();
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
            this.textBoxBotChips2.Text = "chips : " + this.secondBot.Chips;
            this.textBoxBotChips3.Text = "chips : " + this.thirdBot.Chips;
            this.textBoxBotChips4.Text = "chips : " + this.fourthBot.Chips;
            this.textBoxBotChips5.Text = "chips : " + this.fifthBot.Chips;

            this.timer.Interval = 1 * 1 * 1000;
            this.timer.Tick += this.Timer_Tick;
            this.updates.Interval = 1 * 1 * 100;
            this.updates.Tick += this.Update_Tick;
            this.textBoxBigBlind.Visible = false;
            this.textBoxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;
            this.textBoxRaise.Text = (this.defaultBigBlind * 2).ToString();
        }

        public Button ButtonCall
        {
            get { return this.buttonCall; }
        }

        public TextBox TextBoxPot
        {
            get { return this.textBoxPot; }
        }

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

        /// <summary>
        /// Deals cards to all players and the community cards.
        /// Enables card controls and panel controls for the players.
        /// </summary>
        public async Task Shuffle()
        {
            for (int i = 0; i < this.dataBase.Players.Length; i++)
            {
                this.inactivePlayers.Add(this.dataBase.Players[i].FoldTurn);
            }

            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap(GlobalConstants.PlayingCardsBackPath);
            int horizontal = GlobalConstants.PlayerPanelCoordinateX;
            int vertical = GlobalConstants.PlayerPanelCoordinateY;
            Random random = new Random();

            for (int countOfCards = this.imgLocation.Length; countOfCards > 0; countOfCards--)
            {
                int randomNumber = random.Next(countOfCards);
                var pathToCard = this.imgLocation[randomNumber];
                this.imgLocation[randomNumber] = this.imgLocation[countOfCards - 1];
                this.imgLocation[countOfCards - 1] = pathToCard;
            }

            for (int cardsInGame = 0; cardsInGame < GlobalConstants.CountOfTheAvailableCardsInGame; cardsInGame++)
            {
                this.cardsImageDeck[cardsInGame] = Image.FromFile(this.imgLocation[cardsInGame]);
                var charsToRemove = new string[]
                {
                    GlobalConstants.PlayingCardsPath,
                    GlobalConstants.PlayingCardsExtension
                };

                foreach (var c in charsToRemove)
                {
                    this.imgLocation[cardsInGame] = this.imgLocation[cardsInGame].Replace(c, string.Empty);
                }

                this.availableCardsInGame[cardsInGame] = int.Parse(this.imgLocation[cardsInGame]) - 1;
                this.cardsHolder[cardsInGame] = new PictureBox();
                this.cardsHolder[cardsInGame].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardsHolder[cardsInGame].Height = 130;
                this.cardsHolder[cardsInGame].Width = 80;
                this.Controls.Add(this.cardsHolder[cardsInGame]);
                this.cardsHolder[cardsInGame].Name = "pb" + cardsInGame.ToString();
                await Task.Delay(200);

                if (cardsInGame < 2)
                {
                    if (cardsInGame < 2)
                    {
                        if (this.cardsHolder[0].Tag != null)
                        {
                            this.cardsHolder[1].Tag = int.Parse(this.imgLocation[1]) - 1;
                            this.humanPlayer.PlayerCards[1] = CardHandler.GetCard((int) this.cardsHolder[1].Tag);
                            this.humanPlayer.PlayerCards[1].NumberInGame = cardsInGame;
                        }
                        else
                        {
                            this.cardsHolder[0].Tag = int.Parse(this.imgLocation[0]) - 1;
                            this.humanPlayer.PlayerCards[0] = CardHandler.GetCard((int) this.cardsHolder[0].Tag);
                            this.humanPlayer.PlayerCards[0].NumberInGame = cardsInGame;
                        }

                        this.cardsHolder[cardsInGame].Image = this.cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Anchor = AnchorStyles.Bottom;
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsHolder[cardsInGame].Width;
                        this.Controls.Add(this.humanPlayer.Panel);
                        this.humanPlayer.Panel.Location = new Point(this.cardsHolder[0].Left - 10,
                            this.cardsHolder[0].Top - 10);
                    }
                }

                for (int i = 1; i < this.dataBase.Players.Length; i++)
                {
                    if (this.dataBase.Players[i].Chips > 0)
                    {
                        this.foldedPlayers--;

                        if (cardsInGame >= 2 * i && cardsInGame < 2 * i + 2)
                        {
                            if (this.cardsHolder[2 * i].Tag != null)
                            {
                                this.cardsHolder[2 * i + 1].Tag = int.Parse(this.imgLocation[2 * i + 1]) - 1;
                                this.dataBase.Players[i].PlayerCards[1] =
                                    CardHandler.GetCard((int)this.cardsHolder[2 * i + 1].Tag);
                                this.dataBase.Players[i].PlayerCards[1].NumberInGame = cardsInGame;
                            }
                            else
                            {
                                this.cardsHolder[2 * i].Tag = int.Parse(this.imgLocation[2 * i]) - 1;
                                this.dataBase.Players[i].PlayerCards[0] =
                                    CardHandler.GetCard((int)this.cardsHolder[2 * i].Tag);
                                this.dataBase.Players[i].PlayerCards[0].NumberInGame = cardsInGame;
                            }

                            if (!check)
                            {
                                switch (i)
                                {
                                    case 1:
                                        horizontal = GlobalConstants.FirstBotPanelCoordinateX;
                                        vertical = GlobalConstants.FirstBotPanelCoordinateY;

                                        break;
                                    case 2:
                                        horizontal = GlobalConstants.SecondBotPanelCoordinateX;
                                        vertical = GlobalConstants.SecondBotPanelCoordinateY;

                                        break;
                                    case 3:
                                        horizontal = GlobalConstants.ThirdBotPanelCoordinateX;
                                        vertical = GlobalConstants.ThirdBotPanelCoordinateY;

                                        break;
                                    case 4:
                                        horizontal = GlobalConstants.FourthBotPanelCoordinateX;
                                        vertical = GlobalConstants.FourthBotPanelCoordinateY;

                                        break;

                                    case 5:
                                        horizontal = GlobalConstants.FifthBotPanelCoordinateX;
                                        vertical = GlobalConstants.FifthBotPanelCoordinateY;

                                        break;
                                }
                            }

                            check = true;
                            this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                            this.cardsHolder[cardsInGame].Image = backImage;
                            this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                            horizontal += this.cardsHolder[cardsInGame].Width;
                            this.cardsHolder[cardsInGame].Visible = true;
                            this.Controls.Add(this.dataBase.Players[i].Panel);
                            this.dataBase.Players[i].Panel.Location = new Point(this.cardsHolder[2 * i].Left - 10,
                                this.cardsHolder[2 * i].Top - 10);

                            if (cardsInGame == 2 * i + 1)
                            {
                                check = false;
                            }
                        }
                    }
                }

               
                if (cardsInGame >= 12) 
                {

                    if (cardsInGame == 12)
                    {
                        this.cardsHolder[12].Tag = int.Parse(this.imgLocation[12]) - 1;
                        this.table.CardsOnTable[0] = CardHandler.GetCard((int)this.cardsHolder[12].Tag);
                        this.table.CardsOnTable[0].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame == 13)
                    {
                        this.cardsHolder[13].Tag = int.Parse(this.imgLocation[13]) - 1;
                        this.table.CardsOnTable[1] = CardHandler.GetCard((int)this.cardsHolder[13].Tag);
                        this.table.CardsOnTable[1].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame == 14)
                    {
                        this.cardsHolder[14].Tag = int.Parse(this.imgLocation[14]) - 1;
                        this.table.CardsOnTable[2] = CardHandler.GetCard((int)this.cardsHolder[14].Tag);
                        this.table.CardsOnTable[2].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame == 15)
                    {
                        this.cardsHolder[15].Tag = int.Parse(this.imgLocation[15]) - 1;
                        this.table.CardsOnTable[3] = CardHandler.GetCard((int)this.cardsHolder[15].Tag);
                        this.table.CardsOnTable[3].NumberInGame = cardsInGame;
                    }

                    if (cardsInGame == 16)
                    {
                        this.cardsHolder[16].Tag = int.Parse(this.imgLocation[16]) - 1;
                        this.table.CardsOnTable[4] = CardHandler.GetCard((int)this.cardsHolder[16].Tag);
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
                        this.cardsHolder[cardsInGame].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                for (int i = 1; i < this.dataBase.Players.Length; i++)
                {
                    if (this.dataBase.Players[i].Chips <= 0)
                    {
                        this.dataBase.Players[i].FoldTurn = true;
                        this.cardsHolder[2 * i].Visible = false;
                        this.cardsHolder[2 * i + 1].Visible = false;
                    }
                }
                
                if (cardsInGame == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            } 

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
            
            this.buttonRaise.Enabled = true;
            this.buttonCall.Enabled = true;
            this.buttonFold.Enabled = true;
           
        }

        /// <summary>
        /// Rotates through all the players until the game is finished
        /// (restart = false).
        /// Updates all player's data.
        /// Each player makes a decision according to the current hand.
        /// </summary>
        public async Task Turns()
        {
            //Rotating
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
                        this.inactivePlayers.RemoveAt(0);
                        this.inactivePlayers.Insert(0, null);
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
                        this.rules.CheckForHand(this.firstBot);
                        MessageBox.Show(this.firstBot.Name);
                        AiMenager.CheckHand(this.firstBot);
                        this.turnCount++;
                        this.last = 1;
                        this.firstBot.IsPlayerTurn = false;
                        this.secondBot.IsPlayerTurn = true;
                    }
                }

                if (this.firstBot.FoldTurn && !this.firstBot.HasPlayerFolded)
                {
                    this.inactivePlayers.RemoveAt(1);
                    this.inactivePlayers.Insert(1, null);
                    this.maxLeft--;
                    this.firstBot.HasPlayerFolded = true;
                }

                if (this.firstBot.FoldTurn || !this.firstBot.IsPlayerTurn)
                {
                    await this.CheckRaise(1);
                    this.secondBot.IsPlayerTurn = true;
                }

                if (!this.secondBot.FoldTurn)
                {
                    if (this.secondBot.IsPlayerTurn)
                    {
                        this.FixCall(this.secondBot, 1);
                        this.FixCall(this.secondBot, 2);
                        this.rules.CheckForHand(this.secondBot);
                        MessageBox.Show(this.secondBot.Name);
                        AiMenager.CheckHand(this.secondBot);
                        this.turnCount++;
                        this.last = 2;
                        this.secondBot.IsPlayerTurn = false;
                        this.thirdBot.IsPlayerTurn = true;
                    }
                }

                if (this.secondBot.FoldTurn && !this.secondBot.HasPlayerFolded)
                {
                    this.inactivePlayers.RemoveAt(2);
                    this.inactivePlayers.Insert(2, null);
                    this.maxLeft--;
                    this.secondBot.HasPlayerFolded = true;
                }

                if (this.secondBot.FoldTurn || !this.secondBot.IsPlayerTurn)
                {
                    await this.CheckRaise(2);
                    this.thirdBot.IsPlayerTurn = true;
                }

                if (!this.thirdBot.FoldTurn)
                {
                    if (this.thirdBot.IsPlayerTurn)
                    {
                        this.FixCall(this.thirdBot, 1);
                        this.FixCall(this.thirdBot, 2);
                        this.rules.CheckForHand(this.thirdBot);
                        MessageBox.Show(this.thirdBot.Name);
                        AiMenager.CheckHand(this.thirdBot);
                        this.turnCount++;
                        this.last = 3;
                        this.thirdBot.IsPlayerTurn = false;
                        this.fourthBot.IsPlayerTurn = true;
                    }
                }

                if (this.thirdBot.FoldTurn && !this.thirdBot.HasPlayerFolded)
                {
                    this.inactivePlayers.RemoveAt(3);
                    this.inactivePlayers.Insert(3, null);
                    this.maxLeft--;
                    this.thirdBot.HasPlayerFolded = true;
                }

                if (this.thirdBot.FoldTurn || !this.thirdBot.IsPlayerTurn)
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
                    this.inactivePlayers.RemoveAt(4);
                    this.inactivePlayers.Insert(4, null);
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
                    this.inactivePlayers.RemoveAt(5);
                    this.inactivePlayers.Insert(5, null);
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
                        this.inactivePlayers.RemoveAt(0);
                        this.inactivePlayers.Insert(0, null);
                        this.maxLeft--;
                        this.humanPlayer.HasPlayerFolded = true;
                    }
                }

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }
                this.restart = false;
            }
        }
       

        /// <summary>
        /// This method is called each turn, if needed.
        /// Checks if bet is raised in the current turn.
        /// After last round determines the winners 
        /// and finalizes the game.
        /// </summary>
        /// <param name="currentTurn">The current turn</param>
        public async Task CheckRaise(int currentTurn)
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
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft ||
                        this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;

                        foreach (var item in this.dataBase.Players)
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
                        foreach (var item in this.dataBase.Players)
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
                        foreach (var item in this.dataBase.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                        
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
                        foreach (var item in this.dataBase.Players)
                        {
                            item.Call = 0;
                            item.Raise = 0;
                        }
                       
                    }
                }
            }

            if (this.rounds == this.end && this.maxLeft == 6)
            {
                string fixedLast = String.Empty; 

                foreach (var player in this.dataBase.Players)
                {
                    if (!player.Status.Text.Contains(GlobalConstants.FoldMessage))
                    {
                        fixedLast = player.Name;
                        this.rules.CheckForHand(player);
                        this.rules.Winner(player);
                        player.FoldTurn = false;
                    }
                }

                this.restart = true;
                this.humanPlayer.IsPlayerTurn = true;
                
                if (this.humanPlayer.Chips <= 0)
                {
                    AddChips addChips = new AddChips();
                    addChips.ShowDialog();
                    if (addChips.AmountOfChips != 0)
                    {
                        this.humanPlayer.Chips = addChips.AmountOfChips;
                        for (int i = 1; i < this.dataBase.Players.Length; i++)
                        {
                            this.dataBase.Players[i].Chips += addChips.AmountOfChips;
                        }
                        
                        this.humanPlayer.FoldTurn = false;
                        this.humanPlayer.IsPlayerTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = GlobalConstants.RaiseMessage;
                    }
                }

               foreach (var player in this.dataBase.Players)
                {
                    player.Panel.Visible = false;
                    player.Call = 0;
                    player.Raise = 0;
                    player.Power = 0;
                    player.HandFactor = TypeOfTheHand.HighCard;
                }
               
                this.last = 0;
                this.call = this.defaultBigBlind;
                this.raise = 0;
                this.imgLocation = Directory.GetFiles(
                    GlobalConstants.PlayingCardsDirectoryPath,
                    GlobalConstants.PlayingCardsWithPngExtension,
                    SearchOption.TopDirectoryOnly);

                this.inactivePlayers.Clear();
                this.rounds = 0;
                this.rules.Type = 0;
                this.ints.Clear();
                
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

        /// <summary>
        /// Adjusts player's raise and call statistics with 2 options.
        /// Option 1 - adjustments are made based on the choice the player
        /// has made the previous turn, if there was a previous turn.
        /// Option 2 - adjustments are made based on current game statistics.
        /// </summary>
        /// <param name="player">The current player with his status.</param>
        /// <param name="options">The options parameter</param>        
        public void FixCall(IPlayer player, int options)
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

        /// <summary>
        /// Checks for players who haven't folded or made an all in.
        /// If all players have made an all in, waits for the final round and determines winners.
        /// If some players have folded and 2 or more players are all in,
        /// waits for the final round and determines winners.
        /// If only one player has not folded gives him the winnings.
        /// </summary>    
        public async Task AllIn()
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
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }

            #endregion

            var countOfActivePlayers = this.inactivePlayers.Count(x => x == false);

            #region LastManStanding

            if (countOfActivePlayers == 1)
            {
                int index = this.inactivePlayers.IndexOf(false);

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
                    this.secondBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.secondBot.Chips.ToString();
                    this.secondBot.Panel.Visible = true;
                    MessageBox.Show(GlobalConstants.SecondbotPlayerWinMessage);
                }

                if (index == 3)
                {
                    this.thirdBot.Chips += int.Parse(this.textBoxPot.Text);
                    this.textBoxChips.Text = this.thirdBot.Chips.ToString();
                    this.thirdBot.Panel.Visible = true;
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

            if (countOfActivePlayers <= 6 && countOfActivePlayers > 1 && this.rounds >= this.end)
            {
                await this.Finish(2);
            }

            #endregion


        }

        /// <summary>
        /// Reinitializes variables, disables players panels, 
        /// clears lists and text messages.
        /// </summary>
        /// <param name="n">The n parameter.</param>
        public async Task Finish(int n)
        {
            if (n == 2)
            {
                this.rules.FixWinners();
            }
            this.humanPlayer.Panel.Visible = false;
            this.firstBot.Panel.Visible = false;
            this.secondBot.Panel.Visible = false;
            this.thirdBot.Panel.Visible = false;
            this.fourthBot.Panel.Visible = false;
            this.fifthBot.Panel.Visible = false;

            this.call = this.defaultBigBlind;
            this.raise = 0;
            this.foldedPlayers = 5;
            this.rules.Type = 0;
            this.rounds = 0;

            this.firstBot.Power = 0;
            this.secondBot.Power = 0;
            this.thirdBot.Power = 0;
            this.fourthBot.Power = 0;
            this.fifthBot.Power = 0;
            this.humanPlayer.Power = 0;
            this.raise = 0;

            foreach (var player in this.dataBase.Players)
            {
                player.HandFactor = TypeOfTheHand.HighCard;
                player.Call = 0;
                player.Raise = 0;
                player.FoldTurn = false;
                player.HasPlayerFolded = false;
                player.Status.Text = String.Empty;
            }

            for (int i = 1; i < this.dataBase.Players.Length; i++)
            {
                this.dataBase.Players[i].IsPlayerTurn = false;
            }
            this.humanPlayer.IsPlayerTurn = true;
            this.restart = false;
            this.raising = false;
            this.height = 0;
            this.width = 0;
            this.rules.Winners = 0;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.inactivePlayers.Clear();
            this.rules.CheckWinners.Clear();

            this.ints.Clear();
            this.rules.Win.Clear();
            this.rules.Sorted.HandFactor = 0;
            this.rules.Sorted.Power = 0;
            this.textBoxPot.Text = "0";
            this.t = 60;
            this.up = 10000000;
            this.turnCount = 0;
            
            if (this.humanPlayer.Chips <= 0)
            {
                AddChips addChips = new AddChips();
                addChips.ShowDialog();
                if (addChips.AmountOfChips != 0)
                {
                    this.humanPlayer.Chips = addChips.AmountOfChips;
                    this.firstBot.Chips += addChips.AmountOfChips;
                    this.secondBot.Chips += addChips.AmountOfChips;
                    this.thirdBot.Chips += addChips.AmountOfChips;
                    this.fourthBot.Chips += addChips.AmountOfChips;
                    this.fifthBot.Chips += addChips.AmountOfChips;
                    this.humanPlayer.FoldTurn = false;
                    this.humanPlayer.IsPlayerTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = GlobalConstants.RaiseMessage;
                }
            }

            this.imgLocation = Directory.GetFiles(GlobalConstants.PlayingCardsDirectoryPath,
                GlobalConstants.PlayingCardsWithPngExtension,
                SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.cardsHolder[os].Image = null;
                this.cardsHolder[os].Invalidate();
                this.cardsHolder[os].Visible = false;
            }
            await this.Shuffle();
           
        }
      
       
        /// <summary>
        /// Checks if human player's time is expired.
        /// If the player time is expired it is considered "Fold".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private async void Timer_Tick(object sender, object e)
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

        /// <summary>
        /// Updates status for all players, every tick of the timer.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private void Update_Tick(object sender, object e)
        {


            this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();
            this.textBoxBotChips1.Text = "chips : " + this.firstBot.Chips.ToString();
            this.textBoxBotChips2.Text = "chips : " + this.secondBot.Chips.ToString();
            this.textBoxBotChips3.Text = "chips : " + this.thirdBot.Chips.ToString();
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
            else 
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

        /// <summary>
        /// Event for the button "Fold". 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>       
        private async void ButtonFold_IsClicked(object sender, EventArgs e)
        {
            this.humanPlayer.Status.Text = GlobalConstants.FoldMessage;
            this.humanPlayer.IsPlayerTurn = false;
            this.humanPlayer.FoldTurn = true;
            await this.Turns();
        }

        /// <summary>
        /// Event for the button "Check".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>       
        private async void ButtonCheck_IsClicked(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.humanPlayer.IsPlayerTurn = false;
                this.humanPlayer.Status.Text = GlobalConstants.CheckMessage;
            }
            else
            {
               this.buttonCheck.Enabled = false;
            }
            await this.Turns();
        }

        /// <summary>
        /// Event for the button "Call".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private async void ButtonCall_IsClicked(object sender, EventArgs e)
        {
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

        /// <summary> 
        /// Event for the button "Raise".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private async void ButtonRaise_IsClicked(object sender, EventArgs e)
        {
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
                            this.buttonCall.Text = GlobalConstants.CallMessage;
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

        /// <summary>
        /// Event for the button "AddChips".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private void ButtonAddChips_IsClicked(object sender, EventArgs e)
        {
            int parsedValue;
            int defaultChipsValue = 0;
            bool isValue = int.TryParse(this.tbAdd.Text, out parsedValue);
            if (!isValue)
            {
                MessageBox.Show("This is a number only field");
                this.tbAdd.Text = defaultChipsValue.ToString();
            }
            else
            {
                if (parsedValue < 0)
                {
                    MessageBox.Show("The value must be positive number.");
                    this.tbAdd.Text = defaultChipsValue.ToString();
                }
                else
                {
                    this.humanPlayer.Chips += parsedValue;
                    this.firstBot.Chips += parsedValue;
                    this.secondBot.Chips += parsedValue;
                    this.thirdBot.Chips += parsedValue;
                    this.fourthBot.Chips += parsedValue;
                    this.fifthBot.Chips += parsedValue;
                }

            }

            this.textBoxChips.Text = "chips : " + this.humanPlayer.Chips.ToString();
        }

        /// <summary>
        /// Event for the button "Options".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>     
        private void ButtonOptions_IsClicked(object sender, EventArgs e)
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

        /// <summary>
        /// Event for the button "Small Blind".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>        
        private void ButtonSmallBlind_IsClicked(object sender, EventArgs e)
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

        /// <summary>
        /// Event for the button "Big Blind".
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param> 
        private void ButtonBigBlind_IsClicked(object sender, EventArgs e)
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

        /// <summary>
        /// Layout change event.
        /// Describes changes in layout dimensions.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>       
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

    }
}