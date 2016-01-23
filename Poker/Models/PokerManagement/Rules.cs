using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Core;


namespace Poker.Models.PokerManagement
{
    using Poker.Models.Player;

    public class Rules
    {
        private List<Type> win;
        private Type sorted;
        private double type;
        private int winners;
        private static Rules instance = null;
        private List<string> checkWinners = new List<string>();


        private Rules()
        {

            this.win = new List<Type>();
        }

        public static Rules Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Rules();
                }
                return instance;
            }
        }

        public List<string> CheckWinners
        {
            get { return this.checkWinners; }

            set { this.checkWinners = value; }
        }

        public int Winners
        {
            get { return this.winners; }
            set { this.winners = value; }
        }

        public double Type
        {
            get { return this.type; }
            private set { this.type = value; }
        }

        public List<Type> Win
        {
            get { return this.win; }
            private set { this.win = value; }
        }

        public Type Sorted
        {
            get { return this.sorted; }
            set { this.sorted = value; }
        }

        public void ApplyTo(Player player)
        {
            if (player.Cards[1] == 0 && player.Cards[2] == 1)        //elica: Empty if statement
            {
            }

            if (!player.FoldTurn || player.Cards[1] == 0 && player.Cards[2] == 1 && player.Status.Text.Contains("Fold") == false)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] cardsOnTable = new int[5];      // cards on the table
                int[] Straight = new int[7];
                Straight[0] = DataBase.Instace.Table.AvailableCardsInGame[player.Cards[0]];
                Straight[1] = DataBase.Instace.Table.AvailableCardsInGame[player.Cards[1]];
                cardsOnTable[0] = Straight[2] = DataBase.Instace.Table.AvailableCardsInGame[12];
                cardsOnTable[1] = Straight[3] = DataBase.Instace.Table.AvailableCardsInGame[13];
                cardsOnTable[2] = Straight[4] = DataBase.Instace.Table.AvailableCardsInGame[14];
                cardsOnTable[3] = Straight[5] = DataBase.Instace.Table.AvailableCardsInGame[15];
                cardsOnTable[4] = Straight[6] = DataBase.Instace.Table.AvailableCardsInGame[16];
                var clubs = Straight.Where(o => o % 4 == 0).ToArray();         //  clubs
                var diamonds = Straight.Where(o => o % 4 == 1).ToArray();     //  diamonds
                var hearts = Straight.Where(o => o % 4 == 2).ToArray();       //  hearts
                var spades = Straight.Where(o => o % 4 == 3).ToArray();        //  spades
                var st1 = clubs.Select(o => o / 4).Distinct().ToArray();
                var st2 = diamonds.Select(o => o / 4).Distinct().ToArray();
                var st3 = hearts.Select(o => o / 4).Distinct().ToArray();
                var st4 = spades.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);
                #endregion

                for (int card = 0; card < 16; card++)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] == int.Parse(DataBase.Instace.Table.CardsHolder[player.Cards[0]].Tag.ToString()) &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] == int.Parse(DataBase.Instace.Table.CardsHolder[player.Cards[1]].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(card, player);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(card, player);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(card, player);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(player, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(player, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(card, player, ref vf, cardsOnTable);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(player, ref done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(player, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(player, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(card, player);
                        #endregion
                    }
                }
            }
        }

        private void rStraightFlush(Player player, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (player.Current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        player.Current = 8;
                        player.Power = (st1.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        player.Current = 9;
                        player.Power = (st1.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        player.Current = 8;
                        player.Power = (st2.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        player.Current = 9;
                        player.Power = (st2.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        player.Current = 8;
                        player.Power = (st3.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        player.Current = 9;
                        player.Power = (st3.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        player.Current = 8;
                        player.Power = (st4.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        player.Current = 9;
                        player.Power = (st4.Max()) / 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFourOfAKind(Player player, int[] Straight)
        {
            if (player.Current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        player.Current = 7;
                        player.Power = (Straight[j] / 4) * 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 7 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        player.Current = 7;
                        player.Power = 13 * 4 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 7 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFullHouse(Player player, ref bool done, int[] Straight)
        {
            if (player.Current >= -1)
            {
                this.type = player.Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Current = 6;
                                player.Power = 13 * 2 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 6 });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                player.Current = 6;
                                player.Power = fh.Max() / 4 * 2 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 6 });
                                this.sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                player.Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (player.Current != 6)
                {
                    player.Power = this.type;
                }
            }
        }

        private void rFlush(int card, Player player, ref bool vf, int[] Straight1)
        {
            if (player.Current >= -1)
            {
                //addition cardsOnDesk = Streight1, f1=clubs, f2=diamonds, f3=hearts, f4=spades
                var clubs = Straight1.Where(o => o % 4 == 0).ToArray();    //clubs
                var diamonds = Straight1.Where(o => o % 4 == 1).ToArray();     //diamonds
                var hearts = Straight1.Where(o => o % 4 == 2).ToArray();      //hearts
                var spades = Straight1.Where(o => o % 4 == 3).ToArray();      //spades
                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == clubs[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > clubs.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > clubs.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < clubs.Max() / 4 &&
                            DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < clubs.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = clubs.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (clubs.Length == 4)//different cards in hand
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == clubs[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > clubs.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = clubs.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == clubs[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > clubs.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = clubs.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (clubs.Length == 5)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == clubs[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > clubs.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == clubs[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > clubs.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < clubs.Min() / 4 &&
                       DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < clubs.Min())
                    {
                        player.Current = 5;
                        player.Power = clubs.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == diamonds[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > diamonds.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > diamonds.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < diamonds.Max() / 4 &&
                            DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < diamonds.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = diamonds.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (diamonds.Length == 4)//different cards in hand
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == diamonds[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > diamonds.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = diamonds.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == diamonds[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > diamonds.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = diamonds.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (diamonds.Length == 5)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == diamonds[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > diamonds.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == diamonds[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > diamonds.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < diamonds.Min() / 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < diamonds.Min())
                    {
                        player.Current = 5;
                        player.Power = diamonds.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                       DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == hearts[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > hearts.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > hearts.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < hearts.Max() / 4 &&
                            DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < hearts.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = hearts.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (hearts.Length == 4)//different cards in hand
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == hearts[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > hearts.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = hearts.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card] % 4 &&
                       DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == hearts[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > hearts.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = hearts.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (hearts.Length == 5)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == hearts[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > hearts.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == hearts[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > hearts.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < hearts.Min() / 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < hearts.Min())
                    {
                        player.Current = 5;
                        player.Power = hearts.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == spades[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > spades.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > spades.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < spades.Max() / 4 &&
                            DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < spades.Max() / 4)
                        {
                            player.Current = 5;
                            player.Power = spades.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (spades.Length == 4)//different cards in hand
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == spades[0] % 4)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > spades.Max() / 4)

                            player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else
                    {
                        player.Current = 5;
                        player.Power = spades.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 != DataBase.Instace.Table.AvailableCardsInGame[card] % 4 &&
                    DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == spades[0] % 4)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > spades.Max() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else
                    {
                        player.Current = 5;
                        player.Power = spades.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }


                if (spades.Length == 5)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == spades[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > spades.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == spades[0] % 4 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 > spades.Min() / 4)
                    {
                        player.Current = 5;
                        player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 < spades.Min() / 4 &&
                       DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 < spades.Min())
                    {
                        player.Current = 5;
                        player.Power = spades.Max() + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (clubs.Length > 0)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (diamonds.Length > 0)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (hearts.Length > 0)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (spades.Length > 0)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card] % 4 == spades[0] % 4 && vf && spades.Length > 0)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0 &&
                        DataBase.Instace.Table.AvailableCardsInGame[card + 1] % 4 == spades[0] % 4 && vf)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rStraight(Player player, int[] Straight)
        {
            if (player.Current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            player.Current = 4;
                            player.Power = op.Max() + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 4 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            player.Current = 4;
                            player.Power = op[j + 4] + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 4 });
                            this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        player.Current = 4;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type() { Power = player.Power, Current = 4 });
                        this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rThreeOfAKind(Player player, int[] Straight)
        {
            if (player.Current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            player.Current = 3;
                            player.Power = 13 * 3 + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 3 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            player.Current = 3;
                            player.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 3 });
                            this.Sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(int card, Player player)
        {
            if (player.Current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if ((DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 &&
                                    DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc - k] / 4) ||
                                    (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 &&
                                    DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc - k] / 4))
                                {
                                    if (!msgbox)
                                    {
                                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0)
                                        {
                                            player.Current = 2;
                                            player.Power = 13 * 4 + (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0)
                                        {
                                            player.Current = 2;
                                            player.Power = 13 * 4 + (DataBase.Instace.Table.AvailableCardsInGame[card] / 4) * 2 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 != 0 && DataBase.Instace.Table.AvailableCardsInGame[card] / 4 != 0)
                                        {
                                            player.Current = 2;
                                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[card] / 4) * 2 + (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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

        private void rPairTwoPair(int card, Player player)
        {
            if (player.Current >= -1)
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
                            if (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc - k] / 4)
                            {
                                if (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 != DataBase.Instace.Table.AvailableCardsInGame[card] / 4 &&
                                    DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 != DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 && player.Current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0)
                                        {
                                            player.Current = 2;
                                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[card] / 4) * 2 + 13 * 4 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0)
                                        {
                                            player.Current = 2;
                                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 2 + 13 * 4 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 != 0)
                                        {
                                            player.Current = 2;
                                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4) * 2 + (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 2 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 != 0)
                                        {
                                            player.Current = 2;
                                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4) * 2 + (DataBase.Instace.Table.AvailableCardsInGame[card] / 4) * 2 + player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                    }
                                    msgbox = true;
                                }

                                if (player.Current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4)
                                        {
                                            if (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 == 0)
                                            {
                                                player.Current = 0;
                                                player.Power = 13 + DataBase.Instace.Table.AvailableCardsInGame[card] / 4 + player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                player.Current = 0;
                                                player.Power = DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 + DataBase.Instace.Table.AvailableCardsInGame[card] / 4 + player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 == 0)
                                            {
                                                player.Current = 0;
                                                player.Power = 13 + DataBase.Instace.Table.AvailableCardsInGame[card + 1] + player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                player.Current = 0;
                                                player.Power = DataBase.Instace.Table.AvailableCardsInGame[tc] / 4 + DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 + player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
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

        private void rPairFromHand(int card, Player player)
        {
            if (player.Current >= -1)
            {
                bool msgbox = false;
                if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0)
                        {
                            player.Current = 1;
                            player.Power = 13 * 4 + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            player.Current = 1;
                            player.Power = (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 4 + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0)
                            {
                                player.Current = 1;
                                player.Power = 13 * 4 + DataBase.Instace.Table.AvailableCardsInGame[card] / 4 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                player.Current = 1;
                                player.Power = (DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4) * 4 + DataBase.Instace.Table.AvailableCardsInGame[card] / 4 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }
                        msgbox = true;
                    }

                    if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == DataBase.Instace.Table.AvailableCardsInGame[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0)
                            {
                                player.Current = 1;
                                player.Power = 13 * 4 + DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                player.Current = 1;
                                player.Power = (DataBase.Instace.Table.AvailableCardsInGame[tc] / 4) * 4 + DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 + player.Current * 100;
                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(int card, Player player)
        {
            if (player.Current == -1)
            {
                if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 > DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4)
                {
                    player.Current = -1;
                    player.Power = DataBase.Instace.Table.AvailableCardsInGame[card] / 4;
                    this.Win.Add(new Type() { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    player.Current = -1;
                    player.Power = DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4;
                    this.Win.Add(new Type() { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (DataBase.Instace.Table.AvailableCardsInGame[card] / 4 == 0 || DataBase.Instace.Table.AvailableCardsInGame[card + 1] / 4 == 0)
                {
                    player.Current = -1;
                    player.Power = 13;
                    this.Win.Add(new Type() { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        public void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";

            foreach (var player in DataBase.Instace.Players)
            {
                if (!player.Status.Text.Contains("Fold"))
                {
                    fixedLast = player.Name;
                    this.ApplyTo(player);
                    Winner(player, fixedLast);
                }
            }
        }

        private void Winner(Player player, string lastly)
        {
            if (lastly == " ")
            {
                lastly = DataBase.Instace.Players[5].Name;
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (DataBase.Instace.Table.CardsHolder[j].Visible)
                {
                    DataBase.Instace.Table.CardsHolder[j].Image = DataBase.Instace.Table.CardsImageDeck[j];
                }

            }

            if (player.Current == this.sorted.Current)
            {
                if (player.Power == this.sorted.Power)
                {
                    this.Winners++;
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
                if (this.Winners > 1)
                {
                    for (int i = 0; i < DataBase.Instace.Players.Length; i++)
                    {
                        if (this.CheckWinners.Contains(DataBase.Instace.Players[i].Name))
                        {
                            DataBase.Instace.Players[i].Chips += int.Parse(Launcher.Poker.TextBoxPot.Text) / this.Winners;
                            Launcher.Poker.ChipsTextBoxes[i].Text = DataBase.Instace.Players[i].Chips.ToString();

                        }
                    }

                    //await Finish(1);
                }

                if (this.Winners == 1)
                {
                    foreach (var item in DataBase.Instace.Players)
                    {
                        if (this.CheckWinners.Contains(item.Name))
                        {
                            item.Chips += int.Parse(Launcher.Poker.TextBoxPot.Text);

                        }
                    }
                }
            }
        }
    }
}