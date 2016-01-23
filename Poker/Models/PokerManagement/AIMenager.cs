using Poker.Core;

namespace Poker.Models.PokerManagement
{
    using System;
    using Poker.Models.Player;
    public static class AIMenager
    {
        public static void AI(Player player)
        {
            if (!player.FoldTurn)
            {
                if (player.Current == -1)
                {
                    HighCard(player);
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
                    ThreeOfAKind(player);
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
                DataBase.Instace.Table.CardsHolder[player.Cards[0]].Visible = false;
                DataBase.Instace.Table.CardsHolder[player.Cards[1]].Visible = false;

            }
        }

        private static void HighCard(Player player)
        {
            HP(player, 20, 25);
        }

        private static void PairTable(Player player)
        {
            HP(player, 16, 25);
        }

        private static void PairHand(Player player)
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

        private static void TwoPair(Player player)
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

        private static void ThreeOfAKind(Player player)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Power <= 390 && player.Power >= 330)
            {
                Smooth(player, tRaise);
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

        private static void Straight(Player player)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Power <= 480 && player.Power >= 410)
            {
                Smooth(player, sRaise);
            }

            if (player.Power <= 409 && player.Power >= 407)//10  8
            {
                Smooth(player, sRaise);
            }

            if (player.Power < 407 && player.Power >= 404)
            {
                Smooth(player, sRaise);
            }
        }

        private static void Flush(Player player)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(player, fRaise);
        }

        private static void FullHouse(Player player)
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

        private static void FourOfAKind(Player player)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Power <= 752 && player.Power >= 704)
            {
                Smooth(player, fkRaise);
            }
        }

        private static void StraightFlush(Player player)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Power <= 913 && player.Power >= 804)
            {
                Smooth(player, sfRaise);
            }
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
        private static void HP(Player player, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (Launcher.Poker.CallValue <= 0)
            {
                player.PlayerCheck();
            }

            if (Launcher.Poker.CallValue > 0)
            {
                if (rnd == 1)
                {
                    if (Launcher.Poker.CallValue <= RoundN(player.Chips, n))
                    {
                        player.PlayerCall();;
                    }
                    else
                    {
                        player.PlayerFold();
                    }
                }

                if (rnd == 2)
                {
                    if (Launcher.Poker.CallValue <= RoundN(player.Chips, n1))
                    {
                        player.PlayerCall();;
                    }
                    else
                    {
                        player.PlayerFold();
                    }
                }
            }

            if (rnd == 3)
            {
                if (Launcher.Poker.Raise == 0)
                {
                    Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;
                    player.PlayerRaised();
                }
                else
                {
                    if (Launcher.Poker.Raise <= RoundN(player.Chips, n))
                    {
                        Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;
                        player.PlayerRaised();
                    }
                    else
                    {
                        player.PlayerFold();
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
        private static void PH(Player player, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (Launcher.Poker.Rounds < 2)
            {
                if (Launcher.Poker.CallValue <= 0)
                {
                    player.PlayerCheck();
                }

                if (Launcher.Poker.CallValue > 0)
                {
                    if (Launcher.Poker.CallValue >= RoundN(player.Chips, n1))
                    {
                        player.PlayerFold();
                    }

                    if (Launcher.Poker.Raise > RoundN(player.Chips, n))
                    {
                        player.PlayerFold();
                    }

                    if (!player.FoldTurn)
                    {
                        if (Launcher.Poker.CallValue >= RoundN(player.Chips, n) && Launcher.Poker.CallValue <= RoundN(player.Chips, n1))
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= RoundN(player.Chips, n) && Launcher.Poker.Raise >= (RoundN(player.Chips, n)) / 2)
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= (RoundN(player.Chips, n)) / 2)
                        {
                            if (Launcher.Poker.Raise > 0)
                            {
                                Launcher.Poker.Raise = RoundN(player.Chips, n);
                                player.PlayerRaised();
                            }
                            else
                            {
                                Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;
                                player.PlayerRaised();
                            }
                        }

                    }
                }
            }
            
            if (Launcher.Poker.Rounds >= 2)
            {
                if (Launcher.Poker.CallValue > 0)
                {
                    if (Launcher.Poker.CallValue >= RoundN(player.Chips, n1 - rnd))
                    {
                        player.PlayerFold();
                    }

                    if (Launcher.Poker.Raise > RoundN(player.Chips, n - rnd))
                    {
                        player.PlayerFold();
                    }

                    if (!player.FoldTurn)
                    {
                        if (Launcher.Poker.CallValue >= RoundN(player.Chips, n - rnd) && Launcher.Poker.CallValue <= RoundN(player.Chips, n1 - rnd))
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= RoundN(player.Chips, n - rnd) && Launcher.Poker.Raise >= (RoundN(player.Chips, n - rnd)) / 2)
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= (RoundN(player.Chips, n - rnd)) / 2)
                        {
                            if (Launcher.Poker.Raise > 0)
                            {
                                Launcher.Poker.Raise = RoundN(player.Chips, n - rnd);
                                player.PlayerRaised();
                            }
                            else
                            {
                                Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;
                                player.PlayerRaised();
                            }
                        }
                    }
                }

                if (Launcher.Poker.CallValue <= 0)
                {
                    Launcher.Poker.Raise = RoundN(player.Chips, r - rnd);
                    player.PlayerRaised();
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldTurn = true;
            }
        }

       private static void Smooth(Player player, int n)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (Launcher.Poker.CallValue <= 0)
            {
                player.PlayerCheck();
            }
            else
            {
                if (Launcher.Poker.CallValue >= RoundN(player.Chips, n))
                {
                    
                    if (player.Chips > Launcher.Poker.CallValue)
                    {
                        player.PlayerCall();
                    }
                    else if (player.Chips <= Launcher.Poker.CallValue)
                    {
                        Launcher.Poker.Raising = false;
                        player.IsPlayerTurn = false;
                        player.Chips = 0;
                        player.Status.Text = "Call " + player.Chips;
                        Launcher.Poker.TextBoxPot.Text = (int.Parse(Launcher.Poker.TextBoxPot.Text) + player.Chips).ToString();
                        
                    }
                }
                else
                {
                    if (Launcher.Poker.Raise > 0)
                    {
                        
                        if (player.Chips >= Launcher.Poker.Raise * 2)
                        {
                            
                            player.PlayerRaised();
                        }
                        else
                        {
                            player.PlayerCall();
                        }
                    }
                    else
                    {
                        Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;
                        player.PlayerRaised();
                    }
                }
            }

            if (player.Chips <= 0)
            {
                player.IsPlayerTurn = true;
            }
        }

    }
}