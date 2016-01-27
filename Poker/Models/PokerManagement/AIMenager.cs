namespace Poker.Models.PokerManagement
{
    using System;
    using Interfaces;

    /// <summary>
    /// This class is responsible for decision-making by bots.
    /// </summary>
    public class AiMenager
    {
        private const int MinRandomNumberForPairHandCall = 10;
        private const int MaxRandomNumberForPairHandCall = 16;
        private const int MinRandomNumberForPairHandRaise = 10;
        private const int MaxRandomNumberForPairHandRaise = 13;
        private const int MinRandomNumberForTwoPair = 6;
        private const int MaxRandomNumberForTwoPair = 11;
        private const int MinRandomNumberForThreeOfKindRaise = 4;
        private const int MaxRandomNumberForThreeOfKindRaise = 8;
        private const int MinRandomNumberForStraightRaise = 3;
        private const int MaxRandomNumberForStraightRaise = 8;
        private const int MinRandomNumberForFlushRaise = 3;
        private const int MaxRandomNumberForFlushRaise = 7;
        private const int MinRandomNumberForFullHouseRaise = 2;
        private const int MaxRandomNumberForFullHouseRaise = 6;
        private const int MinRandomNumberForFourOfKindRaise = 2;
        private const int MaxRandomNumberForFourOfKindRaise = 5;
        private const int MinRandomNumberForStraightFlushRaise = 1;
        private const int MaxRandomNumberForStraightFlushRaise = 3;
        //private static Random random = new Random();

        /// <summary>
        /// Checks the hands of the bots. According to the strength of the hand, the bot makes a decision how to play.
        /// </summary>
        /// <param name="player">Current player</param>
        public static void CheckHand(IPlayer player)
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
                Launcher.Poker.CardsHolder[player.PlayerCards[0].NumberInGame].Visible = false;
                Launcher.Poker.CardsHolder[player.PlayerCards[1].NumberInGame].Visible = false;

            }
        }

        //If the hand is High Card
        private static void HighCard(IPlayer player)
        {
            HP(player, 20, 25);
        }

        //If the hand is Pair on table
        private static void PairTable(IPlayer player)
        {
            HP(player, 16, 25);
        }

        //If the hand is Pair on hand
        private static void PairHand(IPlayer player)
        {
            Random rPair = new Random();
            int randomNumberForPairHandCall = rPair.Next(MinRandomNumberForPairHandCall, MaxRandomNumberForPairHandCall);
            int randomNumberForPairHandRaise = rPair.Next(MinRandomNumberForPairHandRaise, MaxRandomNumberForPairHandRaise);
            if (player.Power <= 199 && player.Power >= 140)
            {
                PH(player, randomNumberForPairHandCall, 6, randomNumberForPairHandRaise);
            }

            if (player.Power <= 139 && player.Power >= 128)
            {
                PH(player, randomNumberForPairHandCall, 7, randomNumberForPairHandRaise);
            }

            if (player.Power < 128 && player.Power >= 101)
            {
                PH(player, randomNumberForPairHandCall, 9, randomNumberForPairHandRaise);
            }
        }

        //If the hand is Two Pairs
        private static void TwoPair(IPlayer player)
        {
            Random rPair = new Random();
            int randomNumberForTwoPairCall = rPair.Next(MinRandomNumberForTwoPair, MaxRandomNumberForTwoPair);
            int randomNumberForTwoPairRaise = rPair.Next(MinRandomNumberForTwoPair, MaxRandomNumberForTwoPair);
            if (player.Power <= 290 && player.Power >= 246)
            {
                PH(player, randomNumberForTwoPairCall, 3, randomNumberForTwoPairRaise);
            }

            if (player.Power <= 244 && player.Power >= 234)
            {
                PH(player, randomNumberForTwoPairCall, 4, randomNumberForTwoPairRaise);
            }

            if (player.Power < 234 && player.Power >= 201)
            {
                PH(player, randomNumberForTwoPairCall, 4, randomNumberForTwoPairRaise);
            }
        }

        //If the hand is Three of a kind
        private static void ThreeOfAKind(IPlayer player)
        {
            Random tk = new Random();
            //int threeOfKindCall = random.Next(3, 7);
            int randomNumberForThreeOfKindRaise = tk.Next(MinRandomNumberForThreeOfKindRaise, MaxRandomNumberForThreeOfKindRaise);
            if (player.Power <= 390 && player.Power >= 330)
            {
                Smooth(player, randomNumberForThreeOfKindRaise);
            }

            if (player.Power <= 327 && player.Power >= 321)//10  8
            {
                Smooth(player, randomNumberForThreeOfKindRaise);
            }

            if (player.Power < 321 && player.Power >= 303)//7 2
            {
                Smooth(player, randomNumberForThreeOfKindRaise);
            }
        }

        //If the hand is Straight
        private static void Straight(IPlayer player)
        {
            Random str = new Random();
            //int straightCall = random.Next(3, 6);
            int randomNumberForStraightRaise = str.Next(MinRandomNumberForStraightRaise, MaxRandomNumberForStraightRaise);
            if (player.Power <= 480 && player.Power >= 410)
            {
                Smooth(player, randomNumberForStraightRaise);
            }

            if (player.Power <= 409 && player.Power >= 407)//10  8
            {
                Smooth(player, randomNumberForStraightRaise);
            }

            if (player.Power < 407 && player.Power >= 404)
            {
                Smooth(player, randomNumberForStraightRaise);
            }
        }

        //If the hand is Flush
        private static void Flush(IPlayer player)
        {
            Random fsh = new Random();
            //int flushCall = random.Next(2, 6);
            int randomNumberForFlushRaise = fsh.Next(MinRandomNumberForFlushRaise, MaxRandomNumberForFlushRaise);
            Smooth(player, randomNumberForFlushRaise);
        }

        //If the hand is Full House
        private static void FullHouse(IPlayer player)
        {
            Random flh = new Random();
            //int fullHouseCall = random.Next(1, 5);
            int randomNumberForFullHouseRaise = flh.Next(MinRandomNumberForFullHouseRaise, MaxRandomNumberForFullHouseRaise);
            if (player.Power <= 626 && player.Power >= 620)
            {
                Smooth(player, randomNumberForFullHouseRaise);
            }

            if (player.Power < 620 && player.Power >= 602)
            {
                Smooth(player, randomNumberForFullHouseRaise);
            }
        }

        //If the hand is Four of a kind
        private static void FourOfAKind(IPlayer player)
        {
            Random fk = new Random();
            //int fourOfKindCall = random.Next(1, 4);
            int randomNumberForFourOfKindRaise = fk.Next(MinRandomNumberForFourOfKindRaise, MaxRandomNumberForFourOfKindRaise);
            if (player.Power <= 752 && player.Power >= 704)
            {
                Smooth(player, randomNumberForFourOfKindRaise);
            }
        }

        //If the hand is Straight Flush
        private static void StraightFlush(IPlayer player)
        {
            Random sf = new Random();
            //int straightFlushCall = random.Next(1, 3);
            int randomNumberForStraightFlushRaise = sf.Next(MinRandomNumberForStraightFlushRaise,
                                                                MaxRandomNumberForStraightFlushRaise);
            if (player.Power <= 913 && player.Power >= 804)
            {
                Smooth(player, randomNumberForStraightFlushRaise);
            }
        }

        //Formula for determine the bot choice 
        private static double BotChoiceFormula(int currentChips, int n)
        {
            double value = Math.Round((currentChips / n) / 100d, 0) * 100;
            return value;
        }

        //A choice generator for bots. This generator is used when bot has only high card or table pair hand         
        private static void HP(IPlayer player, int n, int n1)
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
                    if (Launcher.Poker.CallValue <= BotChoiceFormula(player.Chips, n))
                    {
                        player.PlayerCall();
                    }
                    else
                    {
                        player.PlayerFold();
                    }
                }

                if (rnd == 2)
                {
                    if (Launcher.Poker.CallValue <= BotChoiceFormula(player.Chips, n1))
                    {
                        player.PlayerCall(); 
                    }
                    else
                    {
                        player.PlayerFold();
                    }
                }
            }

            if (rnd == 3)
            {
                //TODO elica: Added check for callvalue=0 
                if (Launcher.Poker.CallValue <= 0)
                {
                    player.PlayerCheck();
                }
                else
                {
                    if (Launcher.Poker.Raise == 0)
                    {
                        Launcher.Poker.Raise = Launcher.Poker.CallValue * 2;   
                        player.PlayerRaised();
                    }
                    else
                    {
                        if (Launcher.Poker.Raise <= BotChoiceFormula(player.Chips, n))
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

            }

            if (player.Chips <= 0)
            {
                player.FoldTurn = true;
            }
        }

        //Choice maker for bots if they have a hand which is a pair or two pairs
        private static void PH(IPlayer player, int n, int n1, int r)
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
                    if (Launcher.Poker.CallValue >= BotChoiceFormula(player.Chips, n1))
                    {
                        player.PlayerFold();
                    }

                    if (Launcher.Poker.Raise > BotChoiceFormula(player.Chips, n))
                    {
                        player.PlayerFold();
                    }

                    if (!player.FoldTurn)
                    {
                        if (Launcher.Poker.CallValue >= BotChoiceFormula(player.Chips, n) &&
                            Launcher.Poker.CallValue <= BotChoiceFormula(player.Chips, n1))
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= BotChoiceFormula(player.Chips, n) &&
                            Launcher.Poker.Raise >= (BotChoiceFormula(player.Chips, n)) / 2)
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= (BotChoiceFormula(player.Chips, n)) / 2)
                        {
                            if (Launcher.Poker.Raise > 0)
                            {
                                Launcher.Poker.Raise = BotChoiceFormula(player.Chips, n);
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
                    if (Launcher.Poker.CallValue >= BotChoiceFormula(player.Chips, n1 - rnd))
                    {
                        player.PlayerFold();
                    }

                    if (Launcher.Poker.Raise > BotChoiceFormula(player.Chips, n - rnd))
                    {
                        player.PlayerFold();
                    }

                    if (!player.FoldTurn)
                    {
                        if (Launcher.Poker.CallValue >= BotChoiceFormula(player.Chips, n - rnd) &&
                            Launcher.Poker.CallValue <= BotChoiceFormula(player.Chips, n1 - rnd))
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= BotChoiceFormula(player.Chips, n - rnd) &&
                            Launcher.Poker.Raise >= (BotChoiceFormula(player.Chips, n - rnd)) / 2)
                        {
                            player.PlayerCall();
                        }

                        if (Launcher.Poker.Raise <= (BotChoiceFormula(player.Chips, n - rnd)) / 2)
                        {
                            if (Launcher.Poker.Raise > 0)
                            {
                                Launcher.Poker.Raise = BotChoiceFormula(player.Chips, n - rnd);
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
                    Launcher.Poker.Raise = BotChoiceFormula(player.Chips, r - rnd);
                    player.PlayerRaised();
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldTurn = true;
            }
        }

        //Choice maker for bots with a hand three of a kind or higher
        private static void Smooth(IPlayer player, int n)
        {
            //Random rand = new Random();
            //int rnd = rand.Next(1, 3);
            if (Launcher.Poker.CallValue <= 0)
            {
                player.PlayerCheck();
            }
            else
            {
                if (Launcher.Poker.CallValue >= BotChoiceFormula(player.Chips, n))
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