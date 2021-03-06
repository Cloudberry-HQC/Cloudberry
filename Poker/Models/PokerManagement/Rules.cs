﻿namespace Poker.Models.PokerManagement
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Enums;
    using Interfaces;
    using System.Windows.Forms;
    using GlobalConstants;

    /// <summary>
    /// Class that holds the rules of the game for identifying the type of the current hand and determines the winner.
    /// </summary>
    public class Rules : IRules
    {
        public Rules()
        {
            this.Win = new List<Hand>();
        }

        public List<string> CheckWinners { get; set; } = new List<string>();

        public int Winners { get; set; }

        public double Type { get; set; }

        public List<Hand> Win { get; set; }

        public Hand Sorted { get; set; }

        /// <summary>
        /// Checks for the hand type of the current player.
        /// </summary>
        /// <param name="player">Current player.</param>
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

                //Pair from Hand current = 1
                this.CheckForPairFromHand(player);

                //Pair or Two Pair from Table current = 2 || 0
                this.CheckForPairTwoPair(player);

                //Two Pair current = 2
                this.CheckForTwoPair(player);

                //Three of a kind current = 3
                this.CheckForThreeOfAKind(player, sortedSevenCards);

                //Straight current = 4
                this.CheckForStraight(player, sortedSevenCards);

                //Flush current = 5 || 5.5
                this.CheckForFlush(player, ref hasFlush);

                //Full House current = 6
                this.CheckForFullHouse(player, ref hasTrips, sortedSevenCards);

                //Four of a Kind current = 7
                this.CheckForFourOfAKind(player, sortedSevenCards);

                //Straight Flush current = 8 || 9
                this.CheckForStraightFlush(player, sortedSevenCards);

                //High Card current = -1
                this.CheckForHighCard(player);

            }
        }

        /// <summary>
        /// Rearrenges the winners.
        /// </summary>
        public void FixWinners()
        {
            this.Win.Clear();
            this.Sorted.HandFactor = 0;
            this.Sorted.Power = 0;

            foreach (var player in Database.Instace.Players)
            {
                if (!player.HasPlayerFolded)
                {
                    this.CheckForHand(player);
                    this.ShowHands(player);
                }
            }

            foreach (var player in Database.Instace.Players)
            {
                if (!player.HasPlayerFolded)
                {
                    this.Winner(player);
                }
            }

            this.TakePot();
        }

        /// <summary>
        /// Method that shows the best hand of each remaining player
        /// </summary>
        /// <param name="player">Current player</param>
        public void ShowHands(IPlayer player)
        {
            if (player.HandFactor == TypeOfTheHand.HighCard)
            {
                MessageBox.Show(player.Name + " High Card ");
            }

            if (player.HandFactor == TypeOfTheHand.Pair || player.HandFactor == TypeOfTheHand.PairTable)
            {
                MessageBox.Show(player.Name + " Pair ");
            }

            if (player.HandFactor == TypeOfTheHand.TwoPairs)
            {
                MessageBox.Show(player.Name + " Two Pair ");
            }

            if (player.HandFactor == TypeOfTheHand.ThreeOfAKind)
            {
                MessageBox.Show(player.Name + " Three of a Kind ");
            }

            if (player.HandFactor == TypeOfTheHand.Straight)
            {
                MessageBox.Show(player.Name + " Straight ");
            }

            if (player.HandFactor == TypeOfTheHand.Flush)
            {
                MessageBox.Show(player.Name + " Flush ");
            }

            if (player.HandFactor == TypeOfTheHand.FullHouse)
            {
                MessageBox.Show(player.Name + " Full House ");
            }

            if (player.HandFactor == TypeOfTheHand.FourOfAKind)
            {
                MessageBox.Show(player.Name + " Four of a Kind ");
            }

            if (player.HandFactor == TypeOfTheHand.StraightFlush)
            {
                MessageBox.Show(player.Name + " Straight Flush ");
            }

            if (player.HandFactor == TypeOfTheHand.RoyalFlush)
            {
                MessageBox.Show(player.Name + " Royal Flush ! ");
            }
        }

        /// <summary>
        /// Method that distribute the pot between the winners
        /// </summary>
        public void TakePot()
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

        /// <summary>
        /// Determines the winner.
        /// </summary>
        /// <param name="player">Current player.</param>
        public void Winner(IPlayer player)
        {
            for (int j = 0; j <= 16; j++)
            {
                if (Launcher.Poker.CardsHolder[j].Visible)
                {
                    Launcher.Poker.CardsHolder[j].Image = Launcher.Poker.CardsImageDeck[j];
                }

            }

            if (player.HandFactor == this.Sorted.HandFactor)
            {
                if (player.Power == this.Sorted.Power)
                {
                    this.Winners++;
                    this.CheckWinners.Add(player.Name);

                }
            }
        }

        private void CheckForStraightFlush(IPlayer player, ICard[] allSevenCards)
        {
            ICard[] clubs = allSevenCards.Where(card => card.Suit == SuitOfCard.Clubs).ToArray();
            ICard[] diamonds = allSevenCards.Where(card => card.Suit == SuitOfCard.Diamonds).ToArray();
            ICard[] hearts = allSevenCards.Where(card => card.Suit == SuitOfCard.Hearts).ToArray();
            ICard[] spades = allSevenCards.Where(card => card.Suit == SuitOfCard.Spades).ToArray();

            ICard[] distinctValueOfClubs = clubs
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray();

            ICard[] distinctValueOfDiamonds = diamonds
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray();

            ICard[] distinctValueOfHearts = hearts
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray();

            ICard[] distinctValueOfSpades = spades
                .GroupBy(c => c.Value)
                .Select(c => c.First())
                .OrderBy(c => c.Value)
                .ToArray();

            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {

                if (distinctValueOfClubs.Length >= 5)
                {
                    //implement a new logic to satisfy the test with straight flush and another card from the same suit but greater value

                    for (int i = distinctValueOfClubs.Length - 1; i - 4 >= 0; i--)
                    {
                        if (distinctValueOfClubs[i].Value - 4 == distinctValueOfClubs[i - 4].Value)
                        {
                            player.HandFactor = distinctValueOfClubs[i].Value == ValueOfCard.Ace ? TypeOfTheHand.RoyalFlush : TypeOfTheHand.StraightFlush;

                            player.Power = (int)distinctValueOfClubs[i].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            break;
                        }
                    }

                    if (distinctValueOfClubs[0].Value == ValueOfCard.Two &&
                          distinctValueOfClubs[1].Value == ValueOfCard.Three &&
                          distinctValueOfClubs[2].Value == ValueOfCard.Four &&
                          distinctValueOfClubs[3].Value == ValueOfCard.Five &&
                          distinctValueOfClubs[4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.StraightFlush;
                        player.Power = (int)ValueOfCard.Five + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }

                if (distinctValueOfDiamonds.Length >= 5)
                {
                    //implement a new logic to satisfy the test with straight flush and another card from the same suit but greater value

                    for (int i = distinctValueOfDiamonds.Length - 1; i - 4 >= 0; i--)
                    {
                        if (distinctValueOfDiamonds[i].Value - 4 == distinctValueOfDiamonds[i - 4].Value)
                        {
                            player.HandFactor = distinctValueOfDiamonds[i].Value == ValueOfCard.Ace ? TypeOfTheHand.RoyalFlush : TypeOfTheHand.StraightFlush;

                            player.Power = (int)distinctValueOfDiamonds[i].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            break;
                        }
                    }

                    if (distinctValueOfDiamonds[0].Value == ValueOfCard.Two &&
                         distinctValueOfDiamonds[1].Value == ValueOfCard.Three &&
                         distinctValueOfDiamonds[2].Value == ValueOfCard.Four &&
                         distinctValueOfDiamonds[3].Value == ValueOfCard.Five &&
                         distinctValueOfDiamonds[4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.StraightFlush;
                        player.Power = (int)ValueOfCard.Five + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }

                if (distinctValueOfHearts.Length >= 5)
                {
                    //implement a new logic to satisfy the test with straight flush and another card from the same suit but greater value

                    for (int i = distinctValueOfHearts.Length - 1; i - 4 >= 0; i--)
                    {
                        if (distinctValueOfHearts[i].Value - 4 == distinctValueOfHearts[i - 4].Value)
                        {
                            player.HandFactor = distinctValueOfHearts[i].Value == ValueOfCard.Ace ? TypeOfTheHand.RoyalFlush : TypeOfTheHand.StraightFlush;

                            player.Power = (int)distinctValueOfHearts[i].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);

                            break;
                        }
                    }

                    if (distinctValueOfHearts[0].Value == ValueOfCard.Two &&
                         distinctValueOfHearts[1].Value == ValueOfCard.Three &&
                         distinctValueOfHearts[2].Value == ValueOfCard.Four &&
                         distinctValueOfHearts[3].Value == ValueOfCard.Five &&
                         distinctValueOfHearts[4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.StraightFlush;
                        player.Power = (int)ValueOfCard.Five + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }
                }

                if (distinctValueOfSpades.Length >= 5)
                {
                    //implement a new logic to satisfy the test with straight flush and another card from the same suit but greater value

                    for (int i = distinctValueOfSpades.Length - 1; i - 4 >= 0; i--)
                    {
                        if (distinctValueOfSpades[i].Value - 4 == distinctValueOfSpades[i - 4].Value)
                        {
                            player.HandFactor = distinctValueOfSpades[i].Value == ValueOfCard.Ace ? TypeOfTheHand.RoyalFlush : TypeOfTheHand.StraightFlush;

                            player.Power = (int)distinctValueOfSpades[i].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            break;
                        }
                    }

                    if (distinctValueOfSpades[0].Value == ValueOfCard.Two &&
                         distinctValueOfSpades[1].Value == ValueOfCard.Three &&
                         distinctValueOfSpades[2].Value == ValueOfCard.Four &&
                         distinctValueOfSpades[3].Value == ValueOfCard.Five &&
                         distinctValueOfSpades[4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.StraightFlush;
                        player.Power = (int)ValueOfCard.Five + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }
            }
        }

        private void AddHand(IPlayer player)
        {
            this.Win.Add(new Hand { Power = player.Power, HandFactor = player.HandFactor });
            this.Sorted = this.Win
                .OrderByDescending(op1 => op1.HandFactor)
                .ThenByDescending(op1 => op1.Power)
                .First();
        }

        private void CheckForFourOfAKind(IPlayer player, ICard[] allSevenCards)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                for (int j = 0; j <= 3; j++)
                {
                    // Ckeck for four equal cards
                    if (allSevenCards[j].Value == allSevenCards[j + 1].Value &&
                        allSevenCards[j].Value == allSevenCards[j + 2].Value &&
                        allSevenCards[j].Value == allSevenCards[j + 3].Value)
                    {
                        player.HandFactor = TypeOfTheHand.FourOfAKind;
                        player.Power = (int)allSevenCards[j].Value * 4 + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }
                }
            }
        }

        private void CheckForFullHouse(IPlayer player, ref bool hasTrips, ICard[] allSevenCards)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                this.Type = player.Power;
                //loops through value (rank) of cards 0-ace ... king-12
                //TODO Test this : possible error in the loop new value of the ace 13
                for (int j = 1; j <= 13; j++)
                {
                    var equalCards = allSevenCards.Where(card => (int)card.Value == j).ToArray();
                    if (equalCards.Length == 3 || hasTrips)
                    {
                        if (equalCards.Length == 2)
                        {
                            player.HandFactor = TypeOfTheHand.FullHouse;
                            player.Power = (int)equalCards.Max(card => card.Value) * 2 + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            break;
                        }

                        if (!hasTrips)
                        {
                            if (equalCards.Max(card => card.Value) == ValueOfCard.Ace)
                            {
                                player.Power = (int)ValueOfCard.Ace;
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

                if (player.HandFactor != TypeOfTheHand.FullHouse)
                {
                    player.Power = this.Type;
                }
            }
        }

        private void CheckForFlush(IPlayer player, ref bool hasFlush)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                        //If value of the second card in hand is bigger than the bigest value on table
                        if (player.PlayerCards[1].Value > clubs.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        //If the values of the first and second card are smaller from the biggest value on table
                        else if (player.PlayerCards[0].Value < clubs.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < clubs.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)clubs.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)clubs.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)clubs.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    //If the suit of the second card is club and its value is bigger than the smalest value on the table
                    if (player.PlayerCards[1].Suit == clubs[0].Suit &&
                        player.PlayerCards[1].Value > clubs.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }
                    //If the value of the first card is smaller than the smalest value on the table &&
                    // the value of the second card is smaller than the smalest value on the table
                    else if (player.PlayerCards[0].Value < clubs.Min(card => card.Value) &&
                              player.PlayerCards[1].Value < clubs.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)clubs.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > diamonds.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else if (player.PlayerCards[0].Value < diamonds.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < diamonds.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)diamonds.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)diamonds.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == diamonds[0].Suit)
                    {

                        if (player.PlayerCards[1].Value > diamonds.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)diamonds.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 5)
                {

                    if (player.PlayerCards[0].Suit == diamonds[0].Suit &&
                        player.PlayerCards[0].Value > diamonds.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == diamonds[0].Suit &&
                        player.PlayerCards[1].Value > diamonds.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }
                    //TODO the same error in min 
                    else if (player.PlayerCards[0].Value < diamonds.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < diamonds.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)diamonds.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > hearts.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else if (player.PlayerCards[0].Value < hearts.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < hearts.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)hearts.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)hearts.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == hearts[0].Suit)
                    {

                        if (player.PlayerCards[1].Value > hearts.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)hearts.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                    }
                }

                if (hearts.Length == 5)
                {

                    if (player.PlayerCards[0].Suit == hearts[0].Suit &&
                        player.PlayerCards[0].Value > hearts.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == hearts[0].Suit &&
                        player.PlayerCards[1].Value > hearts.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    else if (player.PlayerCards[0].Value < hearts.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < hearts.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)hearts.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                        if (player.PlayerCards[1].Value > spades.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                        else if (player.PlayerCards[0].Value < spades.Max(card => card.Value) &&
                                 player.PlayerCards[1].Value < spades.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)spades.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
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
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)spades.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                    }

                    if (player.PlayerCards[1].Suit != player.PlayerCards[0].Suit &&
                        player.PlayerCards[1].Suit == spades[0].Suit)
                    {

                        if (player.PlayerCards[1].Value > spades.Max(card => card.Value))
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }
                        else
                        {
                            player.HandFactor = TypeOfTheHand.Flush;
                            player.Power = (int)spades.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                            hasFlush = true;
                        }

                    }
                }


                if (spades.Length == 5)
                {

                    if (player.PlayerCards[0].Suit == spades[0].Suit &&
                        player.PlayerCards[0].Value > spades.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    if (player.PlayerCards[1].Suit == spades[0].Suit &&
                        player.PlayerCards[1].Value > spades.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                    else if (player.PlayerCards[0].Value < spades.Min(card => card.Value) &&
                             player.PlayerCards[1].Value < spades.Min(card => card.Value))
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)spades.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        hasFlush = true;
                    }

                }
                //Check for ace of club
                if (clubs.Length > 0)
                {

                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == clubs[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == clubs[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }
                }

                if (diamonds.Length > 0)
                {

                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == diamonds[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == diamonds[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }

                if (hearts.Length > 0)
                {

                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == hearts[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == hearts[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }

                if (spades.Length > 0)
                {

                    if (player.PlayerCards[0].Value == ValueOfCard.Ace &&
                        player.PlayerCards[0].Suit == spades[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                    if (player.PlayerCards[1].Value == ValueOfCard.Ace &&
                        player.PlayerCards[1].Suit == spades[0].Suit && hasFlush)
                    {
                        player.HandFactor = TypeOfTheHand.Flush;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }

                }
            }
        }

        private void CheckForStraight(IPlayer player, ICard[] allSevenCards)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {

                ICard[] distinctCards = allSevenCards.GroupBy(card => card.Value).Select(c => c.First()).ToArray();

                for (int j = 0; j < distinctCards.Length - 4; j++)
                {
                    if (distinctCards[j].Value + 4 == distinctCards[j + 4].Value)
                    {
                        player.HandFactor = TypeOfTheHand.Straight;
                        player.Power = (int)distinctCards.Max(card => card.Value) + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);

                    }

                    if (distinctCards[j].Value == ValueOfCard.Ten &&
                        distinctCards[j + 1].Value == ValueOfCard.Jack &&
                        distinctCards[j + 2].Value == ValueOfCard.Queen &&
                        distinctCards[j + 3].Value == ValueOfCard.King &&
                        distinctCards[j + 4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.Straight;
                        player.Power = (int)ValueOfCard.Ace + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }
                    
                    if (distinctCards[j].Value == ValueOfCard.Two &&
                        distinctCards[j + 1].Value == ValueOfCard.Three &&
                        distinctCards[j + 2].Value == ValueOfCard.Four &&
                        distinctCards[j + 3].Value == ValueOfCard.Five &&
                        distinctCards[j + 4].Value == ValueOfCard.Ace)
                    {
                        player.HandFactor = TypeOfTheHand.Straight;
                        player.Power = (int)ValueOfCard.Five + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                    }
                }
            }
        }

        private void CheckForThreeOfAKind(IPlayer player, ICard[] allSevenCards)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                for (int j = 1; j <= 13; j++)
                {
                    ICard[] equalCards = allSevenCards.Where(card => (int)card.Value == j).ToArray();
                    if (equalCards.Length == 3)
                    {
                        player.HandFactor = TypeOfTheHand.ThreeOfAKind;
                        player.Power = (int)equalCards[0].Value + (int)equalCards[1].Value +
                                       (int)equalCards[2].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);
                        
                    }
                }
            }
        }

        private void CheckForTwoPair(IPlayer player)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                bool firstPairFound = false;
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
                                     player.PlayerCards[1].Value == Database.Instace.Table.CardsOnTable[tableCard - k].Value) ||
                                    (player.PlayerCards[1].Value == Database.Instace.Table.CardsOnTable[tableCard].Value &&
                                     player.PlayerCards[0].Value == Database.Instace.Table.CardsOnTable[tableCard - k].Value))
                                {
                                    if (!firstPairFound)
                                    {
                                        if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)ValueOfCard.Ace * 4 + (int)player.PlayerCards[1].Value * 2 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                        if (player.PlayerCards[1].Value == ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)ValueOfCard.Ace * 4 + (int)player.PlayerCards[0].Value * 2 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                        if (player.PlayerCards[1].Value != ValueOfCard.Ace &&
                                            player.PlayerCards[0].Value != ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)player.PlayerCards[0].Value * 2 +
                                                           (int)player.PlayerCards[1].Value * 2 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                    }
                                    firstPairFound = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckForPairTwoPair(IPlayer player)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                bool firstPairFound = false;
                bool secondPairFound = false;
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
                                    player.HandFactor == TypeOfTheHand.Pair)
                                {
                                    if (!firstPairFound)
                                    {
                                        if (player.PlayerCards[1].Value == ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)player.PlayerCards[0].Value * 2 + (int)ValueOfCard.Ace * 4 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                        if (player.PlayerCards[0].Value == ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)player.PlayerCards[1].Value * 2 + (int)ValueOfCard.Ace * 4 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.Win.Add(new Hand() { Power = player.Power, HandFactor = TypeOfTheHand.TwoPairs });
                                            this.AddHand(player);
                                        }

                                        if (player.PlayerCards[1].Value != ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value *
                                                           2 +
                                                           (int)player.PlayerCards[1].Value * 2 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                        if (player.PlayerCards[0].Value != ValueOfCard.Ace)
                                        {
                                            player.HandFactor = TypeOfTheHand.TwoPairs;
                                            player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value *
                                                           2 +
                                                           (int)player.PlayerCards[0].Value * 2 +
                                                           (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                    }
                                    firstPairFound = true;
                                }

                                if (player.HandFactor == TypeOfTheHand.HighCard)
                                {
                                    if (!secondPairFound)
                                    {
                                        player.HandFactor = 0;
                                        // Ckeck for bigger value of the card in hand of player
                                        if (player.PlayerCards[0].Value > player.PlayerCards[1].Value)
                                        {
                                            player.HandFactor = 0;
                                            player.Power =
                                                (int)Database.Instace.Table.CardsOnTable[tableCard].Value +
                                                (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                        else
                                        {
                                            player.HandFactor = 0;
                                            player.Power =
                                                (int)Database.Instace.Table.CardsOnTable[tableCard].Value +
                                                (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                                            this.AddHand(player);
                                        }

                                    }
                                    secondPairFound = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckForPairFromHand(IPlayer player)
        {
            if (player.HandFactor >= TypeOfTheHand.HighCard)
            {
                bool firstPairFound = false;

                //Check for the rank from cards in hand....if value of the cards are equal (for example 5 and 5)
                if (player.PlayerCards[0].Value == player.PlayerCards[1].Value)
                {
                    if (!firstPairFound)
                    {
                        player.HandFactor = TypeOfTheHand.Pair;
                        player.Power = (int)player.PlayerCards[1].Value * 4 + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                        this.AddHand(player);

                    }
                    firstPairFound = true;
                }

                //Check if some cards on table are equal to the first card in hand of bot --> tc turns cards from table
                for (int tableCard = 4; tableCard >= 0; tableCard--)
                {
                    //Check if the first card is equal to some card on table
                    if (player.PlayerCards[1].Value == Database.Instace.Table.CardsOnTable[tableCard].Value)
                    {
                        if (!firstPairFound)
                        {

                            player.HandFactor = TypeOfTheHand.Pair;
                            player.Power = (int)player.PlayerCards[1].Value * 4 +
                                           (int)player.PlayerCards[0].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                        }

                        firstPairFound = true;
                    }

                    //Check if some cards on table are equal to the second card in hand of bot 
                    if (player.PlayerCards[0].Value == Database.Instace.Table.CardsOnTable[tableCard].Value)
                    {
                        if (!firstPairFound)
                        {
                            player.HandFactor = TypeOfTheHand.Pair;
                            player.Power = (int)Database.Instace.Table.CardsOnTable[tableCard].Value * 4 +
                                           (int)player.PlayerCards[1].Value + (int)player.HandFactor * GlobalConstants.FactorForCalculatingThePower;
                            this.AddHand(player);
                        }
                        firstPairFound = true;
                    }
                }
            }

        }

        private void CheckForHighCard(IPlayer player)
        {
            if (player.HandFactor == TypeOfTheHand.HighCard)
            {

                //Check for bigger value from cards in hand
                if (player.PlayerCards[0].Value > player.PlayerCards[1].Value)
                {
                    player.Power = (int)player.PlayerCards[0].Value;
                }
                else
                {
                    player.Power = (int)player.PlayerCards[1].Value;
                }

                this.AddHand(player);
            }
        }
    }
}