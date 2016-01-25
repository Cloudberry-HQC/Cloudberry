namespace Poker.Models.PokerManagement
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Enums;
    using Interfaces;
    using System.Windows.Forms;
    public class Rules:IRules
    {
        private List<Type> win;
        private Type sorted;
        private double type;
        private int winners;
        private static Rules instance;
        private List<string> checkWinners = new List<string>();

        public Rules()
        {
            this.win = new List<Type>();
        }

        //public static Rules Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Rules();
        //        }
        //        return instance;
        //    }
        //}

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
             set { this.type = value; }
        }

        public List<Type> Win
        {
            get { return this.win; }
            set { this.win = value; }
        }

        public Type Sorted
        {
            get { return this.sorted; }
            set { this.sorted = value; }
        }

        public void CheckForHand(IPlayer player)
        {
            if (!player.FoldTurn || player.Status.Text.Contains("Fold") == false)
            {
                bool hasTrips = false;
                bool hasFlush = false;

                var allSevenCards = new ICard[7];
                allSevenCards[0] = player.PlayerCards[0];
                allSevenCards[1] = player.PlayerCards[1];
                allSevenCards[2] = Database.Instace.Table.CardsOnTable[0];
                allSevenCards[3] = Database.Instace.Table.CardsOnTable[1];
                allSevenCards[4] = Database.Instace.Table.CardsOnTable[2];
                allSevenCards[5] = Database.Instace.Table.CardsOnTable[3];
                allSevenCards[6] = Database.Instace.Table.CardsOnTable[4];

                var sortedSevenCards = allSevenCards.OrderBy(card => card.Value).ToArray();

                //int[] cardsOnTable = new int[5];      // cards on the table
                //int[] Straight = new int[7];
                //Straight[0] = Database.Instace.Table.AvailableCardsInGame[player.Cards[0]];
                //Straight[1] = Database.Instace.Table.AvailableCardsInGame[player.Cards[1]];
                //cardsOnTable[0] = Straight[2] = Database.Instace.Table.AvailableCardsInGame[12];
                //cardsOnTable[1] = Straight[3] = Database.Instace.Table.AvailableCardsInGame[13];
                //cardsOnTable[2] = Straight[4] = Database.Instace.Table.AvailableCardsInGame[14];
                //cardsOnTable[3] = Straight[5] = Database.Instace.Table.AvailableCardsInGame[15];
                //cardsOnTable[4] = Straight[6] = Database.Instace.Table.AvailableCardsInGame[16];

                //var clubs = Straight.Where(o => o % 4 == 0).ToArray();         //  clubs
                //var diamonds = Straight.Where(o => o % 4 == 1).ToArray();     //  diamonds
                //var hearts = Straight.Where(o => o % 4 == 2).ToArray();       //  hearts
                //var spades = Straight.Where(o => o % 4 == 3).ToArray();        //  spades
                //var st1 = clubs.Select(o => o / 4).Distinct().ToArray();
                //var st2 = diamonds.Select(o => o / 4).Distinct().ToArray();
                //var st3 = hearts.Select(o => o / 4).Distinct().ToArray();
                //var st4 = spades.Select(o => o / 4).Distinct().ToArray();
                //Array.Sort(Straight);
                //Array.Sort(st1);
                //Array.Sort(st2);
                //Array.Sort(st3);
                //Array.Sort(st4);

                //for (int card = 0; card < 16; card++)
                //{
                //    if (Database.Instace.Table.AvailableCardsInGame[card] == int.Parse(Database.Instace.Table.CardsHolder[player.Cards[0]].Tag.ToString()) &&
                //        Database.Instace.Table.AvailableCardsInGame[card + 1] == int.Parse(Database.Instace.Table.CardsHolder[player.Cards[1]].Tag.ToString()))
                //    {

                //Pair from Hand current = 1
                this.RPairFromHand(player); //ready

                //Pair or Two Pair from Table current = 2 || 0
                this.RPairTwoPair(player); //ready

                //Two Pair current = 2
                this.RTwoPair(player); //ready
                
                //Three of a kind current = 3
                this.RThreeOfAKind(player, sortedSevenCards); //ready
                
                //Straight current = 4
                this.RStraight(player, sortedSevenCards); //ready

                //Flush current = 5 || 5.5
                this.RFlush(player, ref hasFlush); //ready
                
                //Full House current = 6
                this.RFullHouse(player, ref hasTrips, sortedSevenCards); //ready

                //Four of a Kind current = 7
                this.RFourOfAKind(player, sortedSevenCards); //ready 

                //Straight Flush current = 8 || 9
                this.RStraightFlush(player, sortedSevenCards); //ready

                //High Card current = -1
                this.RHighCard(player); //ready

                //}
                //}
            }
        }

        private void RStraightFlush(IPlayer player, ICard[] allSevenCards)
        {
            //TODO Check if orderBy work correctly
            ICard[] clubs = allSevenCards.Where(card => card.Suit == SuitOfCard.Clubs).ToArray(); //  clubs
            ICard[] diamonds = allSevenCards.Where(card => card.Suit == SuitOfCard.Diamonds).ToArray(); //  diamonds
            ICard[] hearts = allSevenCards.Where(card => card.Suit == SuitOfCard.Hearts).ToArray(); //  hearts
            ICard[] spades = allSevenCards.Where(card => card.Suit == SuitOfCard.Spades).ToArray(); //  spades

            ICard[] distinctValueOfClubs = clubs
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray(); //st1

            ICard[] distinctValueOfDiamonds = diamonds
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray(); //st2

            ICard[] distinctValueOfHearts = hearts
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray(); //st3

            ICard[] distinctValueOfSpades = spades
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray(); //st4

            if (player.Current >= -1)
            {
                if (distinctValueOfClubs.Length >= 5)
                {
                    if (distinctValueOfClubs[0].Value + 4 == distinctValueOfClubs[4].Value)
                    {
                        player.Current = 8;
                        player.Power = (int)distinctValueOfClubs.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (distinctValueOfClubs[0].Value == ValueOfCard.Ace &&
                        distinctValueOfClubs[1].Value == ValueOfCard.Ten &&
                        distinctValueOfClubs[2].Value == ValueOfCard.Jack &&
                        distinctValueOfClubs[3].Value == ValueOfCard.Queen &&
                        distinctValueOfClubs[4].Value == ValueOfCard.King)
                    {
                        player.Current = 9;
                        player.Power = (int)distinctValueOfClubs.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (distinctValueOfDiamonds.Length >= 5)
                {
                    if (distinctValueOfDiamonds[0].Value + 4 == distinctValueOfDiamonds[4].Value)
                    {
                        player.Current = 8;
                        player.Power = (int)distinctValueOfDiamonds.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (distinctValueOfDiamonds[0].Value == ValueOfCard.Ace &&
                        distinctValueOfDiamonds[1].Value == ValueOfCard.Ten &&
                        distinctValueOfDiamonds[2].Value == ValueOfCard.Jack &&
                        distinctValueOfDiamonds[3].Value == ValueOfCard.Queen &&
                        distinctValueOfDiamonds[4].Value == ValueOfCard.King)
                    {
                        player.Current = 9;
                        player.Power = (int)distinctValueOfDiamonds.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (distinctValueOfHearts.Length >= 5)
                {
                    if (distinctValueOfHearts[0].Value + 4 == distinctValueOfHearts[4].Value)
                    {
                        player.Current = 8;
                        player.Power = (int)distinctValueOfHearts.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (distinctValueOfHearts[0].Value == ValueOfCard.Ace &&
                        distinctValueOfHearts[1].Value == ValueOfCard.Ten &&
                        distinctValueOfHearts[2].Value == ValueOfCard.Jack &&
                        distinctValueOfHearts[3].Value == ValueOfCard.Queen &&
                        distinctValueOfHearts[4].Value == ValueOfCard.King)
                    {
                        player.Current = 9;
                        player.Power = (int)distinctValueOfHearts.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (distinctValueOfSpades.Length >= 5)
                {
                    if (distinctValueOfSpades[0].Value + 4 == distinctValueOfSpades[4].Value)
                    {
                        player.Current = 8;
                        player.Power = (int)distinctValueOfSpades.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 8 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (distinctValueOfSpades[0].Value == ValueOfCard.Ace &&
                        distinctValueOfSpades[1].Value == ValueOfCard.Ten &&
                        distinctValueOfSpades[2].Value == ValueOfCard.Jack &&
                        distinctValueOfSpades[3].Value == ValueOfCard.Queen &&
                        distinctValueOfSpades[4].Value == ValueOfCard.King)
                    {
                        player.Current = 9;
                        player.Power = (int)distinctValueOfSpades.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 9 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RFourOfAKind(IPlayer player, ICard[] allSevenCards)
        {
            if (player.Current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    // Ckeck for four equal cards
                    if (allSevenCards[j].Value == allSevenCards[j + 1].Value &&
                        allSevenCards[j].Value == allSevenCards[j + 2].Value &&
                        allSevenCards[j].Value == allSevenCards[j + 3].Value)
                    {
                        player.Current = 7;
                        player.Power = (int)allSevenCards[j].Value * 4 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 7 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    // Ckeck if all four cards are ace
                    if (allSevenCards[j].Value == ValueOfCard.Ace &&
                        allSevenCards[j + 1].Value == ValueOfCard.Ace &&
                        allSevenCards[j + 2].Value == ValueOfCard.Ace &&
                        allSevenCards[j + 3].Value == ValueOfCard.Ace)
                    {
                        player.Current = 7;
                        player.Power = 13 * 4 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 7 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RFullHouse(IPlayer player, ref bool hasTrips, ICard[] allSevenCards)
        {
            if (player.Current >= -1)
            {
                this.type = player.Power;
                //loops through value (rank) of cards 0-ace ... king-12
                for (int j = 0; j <= 12; j++)
                {
                    var equalCards = allSevenCards.Where(card => (int)card.Value == j).ToArray();
                    if (equalCards.Length == 3 || hasTrips)
                    {
                        if (equalCards.Length == 2)
                        {
                            if (equalCards.Max(card => card.Value) == ValueOfCard.Ace)
                            {
                                player.Current = 6;
                                player.Power = 13 * 2 + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 6 });
                                this.sorted = this.Win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                                break;
                            }

                            if (equalCards.Max(card => card.Value) != 0)
                            {
                                player.Current = 6;
                                player.Power = (int)equalCards.Max(card => card.Value) * 2 + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 6 });
                                this.sorted = this.Win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                                break;
                            }
                        }

                        if (!hasTrips)
                        {
                            if (equalCards.Max(card => card.Value) == ValueOfCard.Ace)
                            {
                                player.Power = 13;
                                hasTrips = true;
                                j = -1;
                            }
                            else
                            {
                                player.Power = (int)equalCards.Max(card => card.Value);
                                hasTrips = true;
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

        private void RFlush(IPlayer player, ref bool hasFlush)
        {
            if (player.Current >= -1)
            {
                ICard[] clubs = Database.Instace.Table.CardsOnTable
                    .Where(card => card.Suit == SuitOfCard.Clubs).ToArray();
                ICard[] diamonds = Database.Instace.Table.CardsOnTable
                    .Where(card => card.Suit == SuitOfCard.Diamonds).ToArray();
                ICard[] hearts = Database.Instace.Table.CardsOnTable
                    .Where(card => card.Suit == SuitOfCard.Hearts).ToArray();
                ICard[] spades = Database.Instace.Table.CardsOnTable
                    .Where(card => card.Suit == SuitOfCard.Spades).ToArray();

                //Check for Flush of clubs
                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    //Ckeck if suits are the same --> clubs...first and second card in hand and the one of the table
                    if (player.PlayerCards[0].Suit == player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == clubs[0].Suit)
                    {
                        //If value of the first card in hand is bigger than the bigest value on table
                        if (player.PlayerCards[0].Value > clubs.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }

                        //If value of the second card in hand is bigger than the bigest value on table
                        if (player.PlayerCards[1].Value > clubs.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        //If the values of the first and second card are smaller from the biggest value on table
                        else if (player.PlayerCards[0].Value < clubs.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < clubs.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)clubs.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (clubs.Length == 4)
                {
                    //If the suit of the first card is different from the suit of the second card in hand && 
                    //the suit of the first card is the same as the card on table
                    if (player.PlayerCards[0].Suit != player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == clubs[0].Suit)
                    {
                        //if the first card in hand is bigger than the bigest on table
                        if (player.PlayerCards[0].Value > clubs.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)clubs.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }

                    //If the suit of the second card is different from the suit of the first card in hand && 
                    //the suit of the second card is the same as the card on table
                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == clubs[0].Suit)
                    {
                        //If the second card is bigger than the bigest card on table
                        if (player.PlayerCards[1].Value > clubs.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)clubs.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                //If all 5 cards on table are of the same suit..in this case clubs
                if (clubs.Length == 5)
                {
                    //If the suit of the first card is club and its value is bigger than the smalest value on the table
                    if (player.PlayerCards[0].Suit == clubs[0].Suit &&
                        player.PlayerCards[0].Value > clubs.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    //If the suit of the second card is club and its value is bigger than the smalest value on the table
                    if (player.PlayerCards[1].Suit == clubs[0].Suit &&
                        player.PlayerCards[1].Value > clubs.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                    //If the value of the first card is smaller than the smalest value on the table &&
                    // the value of the second card is smaller than the smalest value on the table
                    //TODO In this if statement there was an error
                    else if (player.PlayerCards[0].Value < clubs.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < clubs.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)clubs.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                }

                //Check for Flush of diamonds
                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (player.PlayerCards[0].Suit == player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == diamonds[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > diamonds.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > diamonds.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else if (player.PlayerCards[0].Value < diamonds.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < diamonds.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)diamonds.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 4)
                {
                    if (player.PlayerCards[0].Suit != player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == diamonds[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > diamonds.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)diamonds.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == diamonds[0].Suit)
                    {
                        if (player.PlayerCards[1].Value > diamonds.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)diamonds.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 5)
                {
                    if (player.PlayerCards[0].Suit == diamonds[0].Suit &&
                        player.PlayerCards[0].Value > diamonds.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == diamonds[0].Suit &&
                        player.PlayerCards[1].Value > diamonds.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                    //TODO the same error in min 
                    else if (player.PlayerCards[0].Value < diamonds.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < diamonds.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)diamonds.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                }

                //Check for Flush of hearts
                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (player.PlayerCards[0].Suit == player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == hearts[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > hearts.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > hearts.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else if (player.PlayerCards[0].Value < hearts.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < hearts.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)hearts.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 4)
                {
                    if (player.PlayerCards[0].Suit != player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == hearts[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > hearts.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)hearts.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == hearts[0].Suit)
                    {
                        if (player.PlayerCards[1].Value > hearts.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)hearts.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 5)
                {
                    if (player.PlayerCards[0].Suit == hearts[0].Suit &&
                        player.PlayerCards[0].Value > hearts.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == hearts[0].Suit &&
                        player.PlayerCards[1].Value > hearts.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    //TODO the same error
                    else if (player.PlayerCards[0].Value < hearts.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < hearts.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)hearts.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                }

                //Check for Flush of spades
                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (player.PlayerCards[0].Suit == player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == spades[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > spades.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > spades.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }

                        //TODO another error Check the first and the second are smaller than the max value, not just the first
                        else if (player.PlayerCards[0].Value < spades.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < spades.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)spades.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }

                if (spades.Length == 4)
                {
                    if (player.PlayerCards[0].Suit != player.PlayerCards[1].Suit &&
                        player.PlayerCards[0].Suit == spades[0].Suit)
                    {
                        if (player.PlayerCards[0].Value > spades.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)spades.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == spades[0].Suit)
                    {
                        if (player.PlayerCards[1].Value > spades.Max(card => card.Value))
                        {
                            player.Current = 5;
                            player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                        else
                        {
                            player.Current = 5;
                            player.Power = (int)spades.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 5 });
                            this.Sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            hasFlush = true;
                        }
                    }
                }


                if (spades.Length == 5)
                {
                    if (player.PlayerCards[0].Suit == spades[0].Suit &&
                        player.PlayerCards[0].Value > spades.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[0].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == spades[0].Suit &&
                        player.PlayerCards[1].Value > spades.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)player.PlayerCards[1].Value + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }

                    //TODO the same
                    else if (player.PlayerCards[0].Value < spades.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < spades.Min(card => card.Value))
                    {
                        player.Current = 5;
                        player.Power = (int)spades.Max(card => card.Value) + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5 });
                        this.Sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        hasFlush = true;
                    }
                }
                //Check for ace of club
                if (clubs.Length > 0)
                {
                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == clubs[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == clubs[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (diamonds.Length > 0)
                {
                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == diamonds[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == diamonds[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (hearts.Length > 0)
                {
                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == hearts[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == hearts[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (spades.Length > 0)
                {
                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == spades[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == spades[0].Suit && hasFlush)
                    {
                        player.Current = 5.5;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 5.5 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RStraight(IPlayer player, ICard[] allSevenCards)
        {
            if (player.Current >= -1)
            {
                //ICard[] distinctCards = allSevenCards.Select(card => card.Value).Distinct().ToArray(); //op
                //List<ICard> distinctCards = new List<ICard>();
                //foreach (ICard card in allSevenCards)
                //{
                //    if (!distinctCards.Exists(c=>c.Value==card.Value))
                //    {
                //        distinctCards.Add(card);
                //    }
                //}
                //TODO Check if this works as expected
                ICard[] distinctCards = allSevenCards.GroupBy(card => card.Value).Select(c => c.First()).ToArray();

                for (int j = 0; j < distinctCards.Length - 4; j++)
                {
                    if (distinctCards[j].Value + 4 == distinctCards[j + 4].Value)
                    {
                        if ((int)distinctCards.Max(card => card.Value) - 4 == (int)distinctCards[j].Value)
                        {
                            player.Current = 4;
                            player.Power = (int)distinctCards.Max(card => card.Value) + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 4 });
                            this.Sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                        else
                        {
                            player.Current = 4;
                            player.Power = (int)distinctCards[j + 4].Value + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 4 });
                            this.Sorted = this.Win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                    }

                    if (distinctCards[j].Value == ValueOfCard.Ace &&
                        distinctCards[j + 1].Value == ValueOfCard.Ten &&
                        distinctCards[j + 2].Value == ValueOfCard.Jack &&
                        distinctCards[j + 3].Value == ValueOfCard.Queen &&
                        distinctCards[j + 4].Value == ValueOfCard.King)
                    {
                        player.Current = 4;
                        player.Power = 13 + player.Current * 100;
                        this.Win.Add(new Type { Power = player.Power, Current = 4 });
                        this.Sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RThreeOfAKind(IPlayer player, ICard[] allSevenCards)
        {
            if (player.Current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {

                    ICard[] equalCards = allSevenCards.Where(card => (int)card.Value == j).ToArray();
                    if (equalCards.Length == 3)
                    {

                        //TODO Check if this works as expected
                        //if the bigger card is ace
                        if (equalCards.Max(card => card.Value) == ValueOfCard.Ace)
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
                            player.Power = (int)equalCards[0].Value + (int)equalCards[1].Value +
                                           (int)equalCards[2].Value + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 3 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                }
            }
        }

        private void RTwoPair(IPlayer player)
        {
            if (player.Current >= -1)
            {
                bool msgbox = false;
                for (int tableCard = 4; tableCard >= 0; tableCard--)
                {
                    int max = tableCard - 0;

                    //Check if cards on hand have not equal values
                    if (player.PlayerCards[0].Value != player.PlayerCards[1].Value)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tableCard - k < 0)
                            {
                                max--;
                            }
                            if (tableCard - k >= 0)
                            {
                                //check for pair among all seven cards
                                if ((player.PlayerCards[0].Value == Database.Instace.Table.CardsOnTable[tableCard].Value &&
                                     player.PlayerCards[1].Value ==
                                     Database.Instace.Table.CardsOnTable[tableCard - k].Value) ||
                                    (player.PlayerCards[1].Value == Database.Instace.Table.CardsOnTable[tableCard].Value &&
                                     player.PlayerCards[0].Value ==
                                     Database.Instace.Table.CardsOnTable[tableCard - k].Value))
                                {
                                    if (!msgbox)
                                    {
                                        if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = 13 * 4 + (int)player.PlayerCards[1].Value * 2 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (player.PlayerCards[1].Value == ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = 13 * 4 + (int)player.PlayerCards[0].Value * 2 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (player.PlayerCards[1].Value != ValueOfCard.Ace &&
                                            player.PlayerCards[0].Value != ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = (int)player.PlayerCards[0].Value * 2 +
                                                           (int)player.PlayerCards[1].Value * 2 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
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

        private void RPairTwoPair(IPlayer player)
        {
            if (player.Current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tableCard = 4; tableCard >= 0; tableCard--)
                {
                    int max = tableCard - 0;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tableCard - k < 0)
                        {
                            max--;
                        }

                        if (tableCard - k >= 0)
                        {
                            //Check if some of the cards on table have an equal values (for example D and D), 
                            if (Database.Instace.Table.CardsOnTable[tableCard].Value ==
                                Database.Instace.Table.CardsOnTable[tableCard - k].Value)
                            {
                                //Check if some of the cards on table are not equal to the values in hand of bot
                                if (Database.Instace.Table.CardsOnTable[tableCard].Value != player.PlayerCards[0].Value &&
                                    Database.Instace.Table.CardsOnTable[tableCard].Value != player.PlayerCards[1].Value &&
                                    player.Current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (player.PlayerCards[1].Value == ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = (int)player.PlayerCards[0].Value * 2 + 13 * 4 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = (int)player.PlayerCards[1].Value * 2 + 13 * 4 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (player.PlayerCards[1].Value != ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value *
                                                           2 +
                                                           (int)player.PlayerCards[1].Value * 2 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type() { Power = player.Power, Current = 2 });
                                            this.Sorted = this.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (player.PlayerCards[0].Value != ValueOfCard.Ace)
                                        {
                                            player.Current = 2;
                                            player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value *
                                                           2 +
                                                           (int)player.PlayerCards[0].Value * 2 +
                                                           player.Current * 100;
                                            this.Win.Add(new Type { Power = player.Power, Current = 2 });
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
                                        // Ckeck for bigger value of the card in hand of player
                                        if (player.PlayerCards[0].Value > player.PlayerCards[1].Value)
                                        {
                                            if (Database.Instace.Table.CardsOnTable[tableCard].Value == ValueOfCard.Ace)
                                            {
                                                player.Current = 0;
                                                player.Power = 13 + (int)player.PlayerCards[0].Value +
                                                               player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                player.Current = 0;
                                                player.Power =
                                                    (int)Database.Instace.Table.CardsOnTable[tableCard].Value +
                                                    (int)player.PlayerCards[0].Value + player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (Database.Instace.Table.CardsOnTable[tableCard].Value == ValueOfCard.Ace)
                                            {
                                                player.Current = 0;
                                                player.Power = 13 + (int)player.PlayerCards[1].Value +
                                                               player.Current * 100;
                                                this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                                                this.Sorted = this.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                player.Current = 0;
                                                player.Power =
                                                    (int)Database.Instace.Table.CardsOnTable[tableCard].Value +
                                                    (int)player.PlayerCards[1].Value + player.Current * 100;
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

        private void RPairFromHand(IPlayer player)
        {
            if (player.Current >= -1)
            {
                bool msgbox = false;

                //Check for the rank from cards in hand....if value of the cards are equal (for example 5 and 5)
                if (player.PlayerCards[0].Value == player.PlayerCards[1].Value)
                {
                    if (!msgbox)
                    {
                        if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                        {
                            player.Current = 1;
                            player.Power = 13 * 4 + player.Current * 100;
                            this.Win.Add(new Type { Power = player.Power, Current = 1 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            player.Current = 1;
                            player.Power = (int)player.PlayerCards[1].Value * 4 + player.Current * 100;
                            this.Win.Add(new Type() { Power = player.Power, Current = 1 });
                            this.Sorted = this.Win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                    msgbox = true;
                }

                //Check if some cards on table are equal to the first card in hand of bot --> tc turns cards from table
                for (int tableCard = 4; tableCard >= 0; tableCard--)
                {
                    //Check if the first card is equal to some card on table
                    if (player.PlayerCards[1].Value == Database.Instace.Table.CardsOnTable[tableCard].Value)
                    {
                        if (!msgbox)
                        {
                            if (player.PlayerCards[1].Value == ValueOfCard.Ace)
                            {
                                player.Current = 1;
                                player.Power = 13 * 4 + (int)player.PlayerCards[0].Value + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                player.Current = 1;
                                player.Power = (int)player.PlayerCards[1].Value * 4 +
                                               (int)player.PlayerCards[0].Value + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                        }
                        msgbox = true;
                    }
                    //Check if some cards on table are equal to the second card in hand of bot --> tc turns cards from table
                    if (player.PlayerCards[0].Value == Database.Instace.Table.CardsOnTable[tableCard].Value)
                    {
                        if (!msgbox)
                        {
                            if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                            {
                                player.Current = 1;
                                player.Power = 13 * 4 + (int)player.PlayerCards[1].Value + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
                                    .OrderByDescending(op => op.Current)
                                    .ThenByDescending(op => op.Power)
                                    .First();
                            }
                            else
                            {
                                player.Current = 1;
                                player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value * 4 +
                                               (int)player.PlayerCards[1].Value + player.Current * 100;
                                this.Win.Add(new Type { Power = player.Power, Current = 1 });
                                this.Sorted = this.Win
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

        private void RHighCard(IPlayer player)
        {
            if (player.Current == -1)
            {
                //Check for bigger value from cards in hand

                if (player.PlayerCards[0].Value > player.PlayerCards[1].Value)
                {
                    player.Current = -1;
                    player.Power = (int)player.PlayerCards[0].Value;
                    this.Win.Add(new Type { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }
                else
                {
                    player.Current = -1;
                    player.Power = (int)player.PlayerCards[1].Value;
                    this.Win.Add(new Type { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }

                if (player.PlayerCards[0].Value == ValueOfCard.Ace || player.PlayerCards[1].Value == ValueOfCard.Ace)
                {
                    player.Current = -1;
                    player.Power = 13;
                    this.Win.Add(new Type { Power = player.Power, Current = -1 });
                    this.Sorted = this.Win
                        .OrderByDescending(op1 => op1.Current)
                        .ThenByDescending(op1 => op1.Power)
                        .First();
                }
            }
        }


        public void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";

            foreach (var player in Database.Instace.Players)
            {
                if (!player.Status.Text.Contains("Fold"))
                {
                    fixedLast = player.Name;
                    this.CheckForHand(player);
                    Winner(player, fixedLast);
                }
            }
        }

        public void Winner(IPlayer player, string lastly)
        {
            if (lastly == " ")
            {
                lastly = Database.Instace.Players[5].Name;
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (Launcher.Poker.CardsHolder[j].Visible)
                {
                    Launcher.Poker.CardsHolder[j].Image = Launcher.Poker.CardsImageDeck[j];
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
                    for (int i = 0; i < Database.Instace.Players.Length; i++)
                    {
                        if (this.CheckWinners.Contains(Database.Instace.Players[i].Name))
                        {
                            Database.Instace.Players[i].Chips += int.Parse(Launcher.Poker.TextBoxPot.Text) / this.Winners;
                            Launcher.Poker.ChipsTextBoxes[i].Text = Database.Instace.Players[i].Chips.ToString();

                        }
                    }

                    //await Finish(1);
                }

                if (this.Winners == 1)
                {
                    foreach (var item in Database.Instace.Players)
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