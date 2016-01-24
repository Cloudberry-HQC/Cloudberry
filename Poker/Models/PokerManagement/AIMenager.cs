namespace Poker.Models.PokerManagement
{
    public class AiMenager
    {
        //void AI(Player player)
        //{
        //    if (!player.FoldTurn)
        //    {
        //        if (player.Current == -1)
        //        {
        //            HighCard(player);
        //        }

        //        if (player.Current == 0)
        //        {
        //            PairTable(player);
        //        }

        //        if (player.Current == 1)
        //        {
        //            PairHand(player);
        //        }

        //        if (player.Current == 2)
        //        {
        //            TwoPair(player);
        //        }

        //        if (player.Current == 3)
        //        {
        //            ThreeOfAKind(player);
        //        }

        //        if (player.Current == 4)
        //        {
        //            Straight(player);
        //        }

        //        if (player.Current == 5 || player.Current == 5.5)
        //        {
        //            Flush(player);
        //        }

        //        if (player.Current == 6)
        //        {
        //            FullHouse(player);
        //        }

        //        if (player.Current == 7)
        //        {
        //            FourOfAKind(player);
        //        }

        //        if (player.Current == 8 || player.Current == 9)
        //        {
        //            StraightFlush(player);
        //        }
        //    }

        //    if (player.FoldTurn)
        //    {
        //        Database.Instace.Table.CardsHolder[player.Cards[0]].Visible = false;
        //        Database.Instace.Table.CardsHolder[player.Cards[1]].Visible = false;
                
        //    }
        //}

        //private void HighCard(Player player)
        //{
        //    HP(player, 20, 25);
        //}

        //private void PairTable(Player player)
        //{
        //    HP(player, 16, 25);
        //}

        //private void PairHand(Player player)
        //{
        //    Random rPair = new Random();
        //    int rCall = rPair.Next(10, 16);
        //    int rRaise = rPair.Next(10, 13);
        //    if (player.Power <= 199 && player.Power >= 140)
        //    {
        //        PH(player, rCall, 6, rRaise);
        //    }

        //    if (player.Power <= 139 && player.Power >= 128)
        //    {
        //        PH(player, rCall, 7, rRaise);
        //    }

        //    if (player.Power < 128 && player.Power >= 101)
        //    {
        //        PH(player, rCall, 9, rRaise);
        //    }
        //}

        //private void TwoPair(Player player)
        //{
        //    Random rPair = new Random();
        //    int rCall = rPair.Next(6, 11);
        //    int rRaise = rPair.Next(6, 11);
        //    if (player.Power <= 290 && player.Power >= 246)
        //    {
        //        PH(player, rCall, 3, rRaise);
        //    }

        //    if (player.Power <= 244 && player.Power >= 234)
        //    {
        //        PH(player, rCall, 4, rRaise);
        //    }

        //    if (player.Power < 234 && player.Power >= 201)
        //    {
        //        PH(player, rCall, 4, rRaise);
        //    }
        //}

        //private void ThreeOfAKind(Player player)
        //{
        //    Random tk = new Random();
        //    int tCall = tk.Next(3, 7);
        //    int tRaise = tk.Next(4, 8);
        //    if (player.Power <= 390 && player.Power >= 330)
        //    {
        //        Smooth(player, tRaise);
        //    }

        //    if (player.Power <= 327 && player.Power >= 321)//10  8
        //    {
        //        Smooth(player, tRaise);
        //    }

        //    if (player.Power < 321 && player.Power >= 303)//7 2
        //    {
        //        Smooth(player, tRaise);
        //    }
        //}

        //private void Straight(Player player)
        //{
        //    Random str = new Random();
        //    int sCall = str.Next(3, 6);
        //    int sRaise = str.Next(3, 8);
        //    if (player.Power <= 480 && player.Power >= 410)
        //    {
        //        Smooth(player, sRaise);
        //    }

        //    if (player.Power <= 409 && player.Power >= 407)//10  8
        //    {
        //        Smooth(player, sRaise);
        //    }

        //    if (player.Power < 407 && player.Power >= 404)
        //    {
        //        Smooth(player, sRaise);
        //    }
        //}

        //private void Flush(Player player)
        //{
        //    Random fsh = new Random();
        //    int fCall = fsh.Next(2, 6);
        //    int fRaise = fsh.Next(3, 7);
        //    Smooth(player, fRaise);
        //}

        //private void FullHouse(Player player)
        //{
        //    Random flh = new Random();
        //    int fhCall = flh.Next(1, 5);
        //    int fhRaise = flh.Next(2, 6);
        //    if (player.Power <= 626 && player.Power >= 620)
        //    {
        //        Smooth(player, fhRaise);
        //    }

        //    if (player.Power < 620 && player.Power >= 602)
        //    {
        //        Smooth(player, fhRaise);
        //    }
        //}

        //private void FourOfAKind(Player player)
        //{
        //    Random fk = new Random();
        //    int fkCall = fk.Next(1, 4);
        //    int fkRaise = fk.Next(2, 5);
        //    if (player.Power <= 752 && player.Power >= 704)
        //    {
        //        Smooth(player, fkRaise);
        //    }
        //}

        //private void StraightFlush(Player player)
        //{
        //    Random sf = new Random();
        //    int sfCall = sf.Next(1, 3);
        //    int sfRaise = sf.Next(1, 3);
        //    if (player.Power <= 913 && player.Power >= 804)
        //    {
        //        Smooth(player, sfRaise);
        //    }
        //}

        //private static double RoundN(int sChips, int n)
        //{
        //    double a = Math.Round((sChips / n) / 100d, 0) * 100;
        //    return a;
        //}

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
        //private void HP(Player player, int n, int n1)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 4);
        //    if (this.call <= 0)
        //    {
        //        Check(player);
        //    }

        //    if (this.call > 0)
        //    {
        //        if (rnd == 1)
        //        {
        //            if (this.call <= RoundN(player.Chips, n))
        //            {
        //                Call(player);
        //            }
        //            else
        //            {
        //                Fold(player);
        //            }
        //        }

        //        if (rnd == 2)
        //        {
        //            if (this.call <= RoundN(player.Chips, n1))
        //            {
        //                Call(player);
        //            }
        //            else
        //            {
        //                Fold(player);
        //            }
        //        }
        //    }

        //    if (rnd == 3)
        //    {
        //        if (this.raise == 0)
        //        {
        //            this.raise = this.call * 2;
        //            Raised(player);
        //        }
        //        else
        //        {
        //            if (this.raise <= RoundN(player.Chips, n))
        //            {
        //                this.raise = this.call * 2;
        //                Raised(player);
        //            }
        //            else
        //            {
        //                Fold(player);
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
        //private void PH(Player player, int n, int n1, int r)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 3);
        //    if (this.rounds < 2)
        //    {
        //        if (this.call <= 0)
        //        {
        //            Check(player);
        //        }

        //        if (this.call > 0)
        //        {
        //            if (this.call >= RoundN(player.Chips, n1))
        //            {
        //                Fold(player);
        //            }

        //            if (this.raise > RoundN(player.Chips, n))
        //            {
        //                Fold(player);
        //            }

        //            if (!player.FoldTurn)
        //            {
        //                if (this.call >= RoundN(player.Chips, n) && this.call <= RoundN(player.Chips, n1))
        //                {
        //                    Call(player);
        //                }

        //                if (this.raise <= RoundN(player.Chips, n) && this.raise >= (RoundN(player.Chips, n)) / 2)
        //                {
        //                    Call(player);
        //                }

        //                if (this.raise <= (RoundN(player.Chips, n)) / 2)
        //                {
        //                    if (this.raise > 0)
        //                    {
        //                        this.raise = RoundN(player.Chips, n);
        //                        Raised(player);
        //                    }
        //                    else
        //                    {
        //                        this.raise = this.call * 2;
        //                        Raised(player);
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
        //                Fold(player);
        //            }

        //            if (this.raise > RoundN(player.Chips, n - rnd))
        //            {
        //                Fold(player);
        //            }

        //            if (!player.FoldTurn)
        //            {
        //                if (this.call >= RoundN(player.Chips, n - rnd) && this.call <= RoundN(player.Chips, n1 - rnd))
        //                {
        //                    Call(player);
        //                }

        //                if (this.raise <= RoundN(player.Chips, n - rnd) && this.raise >= (RoundN(player.Chips, n - rnd)) / 2)
        //                {
        //                    Call(player);
        //                }

        //                if (this.raise <= (RoundN(player.Chips, n - rnd)) / 2)
        //                {
        //                    if (this.raise > 0)
        //                    {
        //                        this.raise = RoundN(player.Chips, n - rnd);
        //                        Raised(player);
        //                    }
        //                    else
        //                    {
        //                        this.raise = this.call * 2;
        //                        Raised(player);
        //                    }
        //                }
        //            }
        //        }

        //        if (this.call <= 0)
        //        {
        //            this.raise = RoundN(player.Chips, r - rnd);
        //            Raised(player);
        //        }
        //    }

        //    if (player.Chips <= 0)
        //    {
        //        player.FoldTurn = true;
        //    }
        //}

        //void Smooth(Player player, int n)
        //{
        //    Random rand = new Random();
        //    int rnd = rand.Next(1, 3);
        //    if (this.call <= 0)
        //    {
        //        Check(player);
        //    }
        //    else
        //    {
        //        if (this.call >= RoundN(player.Chips, n))
        //        {
        //            if (player.Chips > this.call)
        //            {
        //                Call(player);
        //            }
        //            else if (player.Chips <= this.call)
        //            {
        //                this.raising = false;
        //                player.Turn = false;
        //                player.Chips = 0;
        //                player.Status.Text = "Call " + player.Chips;
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
        //                    Raised(player);
        //                }
        //                else
        //                {
        //                    Call(player);
        //                }
        //            }
        //            else
        //            {
        //                this.raise = this.call * 2;
        //                Raised(player);
        //            }
        //        }
        //    }

        //    if (player.Chips <= 0)
        //    {
        //        player.IsPlayerTurn = true;
        //    }
        //}

    }
}