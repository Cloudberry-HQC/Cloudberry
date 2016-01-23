using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models.PokerManagement
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Threading.Tasks;
    using Poker.Models.Player;
    using Poker.Core;

    public class PokerTable
    {
        private string[] imgLocation;
        private int[] availableCardsInGame;
        private Image[] cardsImageDeck;
        private PictureBox[] cardsHolder;
        private Timer timer;
        private Timer updates;
        private Bitmap backImage;
        private int horizontal = 580;
        private int vertical = 480;
        private Random random = new Random();
        private bool check;
        private Control.ControlCollection controls;
        private DataBase dataBase;
        private bool maximizeBox;
        private bool minimizeBox;
        private bool restart;

        public PokerTable(Control.ControlCollection controls, bool maximizeBox, bool minimizeBox, bool restart)
        {
            this.imgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png",
                SearchOption.TopDirectoryOnly);
            this.availableCardsInGame = new int[17];
            this.cardsImageDeck = new Image[52];
            this.cardsHolder = new PictureBox[52];
            this.timer = new Timer();
            this.updates = new Timer();
            this.backImage = new Bitmap("..\\..\\Resources\\Assets\\Back\\Back.png");
            this.controls = controls;
            this.dataBase = DataBase.Instace;
            this.maximizeBox = maximizeBox;
            this.minimizeBox = minimizeBox;
            this.restart = restart;
        }


        public Image[] CardsImageDeck
        {
            get { return this.cardsImageDeck; }
        }
        public int[] AvailableCardsInGame
        {
            get { return this.availableCardsInGame; }
        }

        public PictureBox[] CardsHolder
        {
            get { return this.cardsHolder; }
        }

        public Timer Timer { get; set; }
        public Timer Updates { get; set; }


        public async void DealCards()
        {
            for (int countOfCards = this.imgLocation.Length; countOfCards > 0; countOfCards--)
            {
                int randomNumber = this.random.Next(countOfCards);
                var pathToCard = this.imgLocation[randomNumber];
                this.imgLocation[randomNumber] = this.imgLocation[countOfCards - 1];
                this.imgLocation[countOfCards - 1] = pathToCard;
            }

            for (int cardsInGame = 0; cardsInGame < 17; cardsInGame++)
            {
                this.cardsImageDeck[cardsInGame] = Image.FromFile(this.imgLocation[cardsInGame]);
                var charsToRemove = new string[] { "..\\..\\Resources\\Assets\\Cards\\", ".png" };

                foreach (var c in charsToRemove)
                {
                    this.imgLocation[cardsInGame] = this.imgLocation[cardsInGame].Replace(c, string.Empty);
                }

                this.availableCardsInGame[cardsInGame] = int.Parse(this.imgLocation[cardsInGame]) - 1;
                this.cardsHolder[cardsInGame] = new PictureBox();
                this.cardsHolder[cardsInGame].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardsHolder[cardsInGame].Height = 130;
                this.cardsHolder[cardsInGame].Width = 80;
                this.controls.Add(this.cardsHolder[cardsInGame]);
                this.cardsHolder[cardsInGame].Name = "pb" + cardsInGame.ToString();
                await Task.Delay(200);

                #region Throwing Cards

                if (cardsInGame < 2)
                {
                    if (this.cardsHolder[0].Tag != null)
                    {
                        this.cardsHolder[1].Tag = this.availableCardsInGame[1];
                    }
                    var player = this.dataBase.Players.FirstOrDefault(pl => pl.Name == "Player");
                    this.cardsHolder[0].Tag = this.availableCardsInGame[0];
                    this.cardsHolder[cardsInGame].Image = this.cardsImageDeck[cardsInGame];
                    this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom);
                    //cardsHolder[cardsInGame].Dock = DockStyle.Top;
                    this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                    this.horizontal += this.cardsHolder[cardsInGame].Width;
                    this.controls.Add(player.Panel);
                    player.Panel.Location = new Point(this.cardsHolder[0].Left - 10, this.cardsHolder[0].Top - 10);

                }

                var firstBot = this.dataBase.Players.FirstOrDefault(bot => bot.Name == "FirstBot");
                if (firstBot.Chips > 0)
                {
                    //this.foldedPlayers--;

                    if (cardsInGame >= 2 && cardsInGame < 4) //elica: cards of the first bot with index 2 and 3
                    {
                        if (this.cardsHolder[2].Tag != null)
                        {
                            this.cardsHolder[3].Tag = this.availableCardsInGame[3];
                        }

                        this.cardsHolder[2].Tag = this.availableCardsInGame[2];
                        if (!this.check)
                        {
                            this.horizontal = 15;
                            this.vertical = 420;
                        }

                        this.check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = this.backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.controls.Add(firstBot.Panel);
                        firstBot.Panel.Location = new Point(this.cardsHolder[2].Left - 10,
                            this.cardsHolder[2].Top - 10);

                        if (cardsInGame == 3)
                        {
                            this.check = false;
                        }
                    }
                }

                var secondBot = this.dataBase.Players.FirstOrDefault(bot => bot.Name == "SecondBot");
                if (secondBot.Chips > 0)
                {
                    //this.foldedPlayers--;

                    if (cardsInGame >= 4 && cardsInGame < 6) // elica: cards of the second bot with index 4 and 5
                    {
                        if (this.cardsHolder[4].Tag != null)
                        {
                            this.cardsHolder[5].Tag = this.availableCardsInGame[5];
                        }

                        this.cardsHolder[4].Tag = this.availableCardsInGame[4];

                        if (!this.check)
                        {
                            this.horizontal = 75;
                            this.vertical = 65;
                        }

                        this.check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.controls.Add(secondBot.Panel);
                        secondBot.Panel.Location = new Point(this.cardsHolder[4].Left - 10,
                            this.cardsHolder[4].Top - 10);

                        if (cardsInGame == 5)
                        {
                            this.check = false;
                        }
                    }
                }

                var thirdBot = this.dataBase.Players.FirstOrDefault(bot => bot.Name == "ThirdBot");
                if (thirdBot.Chips > 0)
                {
                    //this.foldedPlayers--;

                    if (cardsInGame >= 6 && cardsInGame < 8) // elica: cards of the third bot with index 6 and 7
                    {
                        if (this.cardsHolder[6].Tag != null)
                        {
                            this.cardsHolder[7].Tag = this.availableCardsInGame[7];
                        }

                        this.cardsHolder[6].Tag = this.availableCardsInGame[6];

                        if (!this.check)
                        {
                            this.horizontal = 590;
                            this.vertical = 25;
                        }

                        this.check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top);
                        this.cardsHolder[cardsInGame].Image = backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame;
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.controls.Add(thirdBot.Panel);
                        thirdBot.Panel.Location = new Point(this.cardsHolder[6].Left - 10,
                            this.cardsHolder[6].Top - 10);

                        if (cardsInGame == 7)
                        {
                            this.check = false;
                        }
                    }
                }

                var fourthBot = this.dataBase.Players.FirstOrDefault(bot => bot.Name == "FourthBot");
                if (fourthBot.Chips > 0)
                {
                    //this.foldedPlayers--;

                    if (cardsInGame >= 8 && cardsInGame < 10) // elica: cards of the fourth bot with index 8 and 9
                    {
                        if (this.cardsHolder[8].Tag != null)
                        {
                            this.cardsHolder[9].Tag = this.availableCardsInGame[9];
                        }

                        this.cardsHolder[8].Tag = this.availableCardsInGame[8];

                        if (!this.check)
                        {
                            this.horizontal = 1115;
                            this.vertical = 65;
                        }

                        this.check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = this.backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.controls.Add(fourthBot.Panel);
                        fourthBot.Panel.Location = new Point(this.cardsHolder[8].Left - 10,
                            this.cardsHolder[8].Top - 10);

                        if (cardsInGame == 9)
                        {
                            this.check = false;
                        }
                    }
                }

                var fifthBot = this.dataBase.Players.FirstOrDefault(bot => bot.Name == "FifthBot");
                if (fifthBot.Chips > 0)
                {
                    //this.foldedPlayers--;

                    if (cardsInGame >= 10 && cardsInGame < 12) // elica: cards of the fifth bot with index 10 and 11
                    {
                        if (this.cardsHolder[10].Tag != null)
                        {
                            this.cardsHolder[11].Tag = this.availableCardsInGame[11];
                        }

                        this.cardsHolder[10].Tag = this.availableCardsInGame[10];

                        if (!this.check)
                        {
                            this.horizontal = 1160;
                            this.vertical = 420;
                        }

                        this.check = true;
                        this.cardsHolder[cardsInGame].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.cardsHolder[cardsInGame].Image = this.backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += this.cardsHolder[cardsInGame].Width;
                        this.cardsHolder[cardsInGame].Visible = true;
                        this.controls.Add(fifthBot.Panel);
                        fifthBot.Panel.Location = new Point(this.cardsHolder[10].Left - 10,
                            this.cardsHolder[10].Top - 10);

                        if (cardsInGame == 11)
                        {
                            this.check = false;
                        }
                    }
                }

                // TODO if statement to switch
                if (cardsInGame >= 12) // elica: cards on the table with index 12, 13, 14, 15, 16
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

                    if (!this.check)
                    {
                        this.horizontal = 410;
                        this.vertical = 265;
                    }

                    this.check = true;

                    if (this.cardsHolder[cardsInGame] != null)
                    {
                        this.cardsHolder[cardsInGame].Anchor = AnchorStyles.None;
                        this.cardsHolder[cardsInGame].Image = this.backImage;
                        //cardsHolder[cardsInGame].Image = cardsImageDeck[cardsInGame];
                        this.cardsHolder[cardsInGame].Location = new Point(this.horizontal, this.vertical);
                        this.horizontal += 110;
                    }
                }
                #endregion

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
                        this.maximizeBox = true;
                        this.minimizeBox = true;
                    }
                    this.timer.Start();
                }
            } //elica: end of the second loop
        }
    }
}
