﻿namespace Poker.Test
{
    using Enums;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Core;
    using Poker.Forms;
    using Poker.Interfaces;
    using Poker.Models.Card;
    using Poker.Models.Player;
    using Poker.Models.PokerManagement;


    [TestClass]
    public class RulesTest
    {

        private Database dataBase = Database.Instace;

        private PokerTable table = new PokerTable();


        [TestMethod]
        public void CheckForHandTest_CheckForHighCard()
        {
            IRules rules = new Rules();
            IPlayer player = new Human("player");
            player.PlayerCards[0] = new Card(2);
            player.PlayerCards[1] = new Card(51);

            this.table.CardsOnTable[0] = new Card(37);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(12);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(42);

            this.dataBase.Table = this.table;
            rules.CheckForHand(player);
            Assert.AreEqual(TypeOfTheHand.HighCard, player.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(13, player.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.HighCard, rules.Win[0].HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(13, rules.Win[0].Power, "Rules.win.power not correct");
            Assert.AreEqual(13, rules.Sorted.Power);
            Assert.AreEqual(TypeOfTheHand.HighCard, rules.Sorted.HandFactor);
        }

        [TestMethod]
        public void CheckForHandTest_CheckForPairFromHand_PairFromPlayerCards()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(50);
            player1.PlayerCards[1] = new Card(51);

            this.table.CardsOnTable[0] = new Card(37);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(12);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(42);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.Pair, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(148, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.Pair, rules.Win[0].HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(148, rules.Win[0].Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForPairFromHand_PairFromTableCards()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(50);
            player1.PlayerCards[1] = new Card(7);

            this.table.CardsOnTable[0] = new Card(11);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(51);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(42);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.Pair, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(149, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.Pair, rules.Win[0].HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(149, rules.Win[0].Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForPairTwoPair_TwoPairFromTableCards()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(50);
            player1.PlayerCards[1] = new Card(7);

            this.table.CardsOnTable[0] = new Card(28);
            this.table.CardsOnTable[1] = new Card(21);
            this.table.CardsOnTable[2] = new Card(51);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(42);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.TwoPairs, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(234, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.TwoPairs, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(234, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForPairTwoPair_PairOnTables()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(14);
            player1.PlayerCards[1] = new Card(7);

            this.table.CardsOnTable[0] = new Card(28);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(51);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(50);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.PairTable, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(15, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.PairTable, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(15, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForThreeOfAKind_ThreeOfAKindOnTable()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(48);
            player1.PlayerCards[1] = new Card(0);

            this.table.CardsOnTable[0] = new Card(25);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(46);
            this.table.CardsOnTable[3] = new Card(27);
            this.table.CardsOnTable[4] = new Card(24);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(318, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(318, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForThreeOfAKind_WithPairInHand()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(21);
            player1.PlayerCards[1] = new Card(22);

            this.table.CardsOnTable[0] = new Card(13);
            this.table.CardsOnTable[1] = new Card(32);
            this.table.CardsOnTable[2] = new Card(23);
            this.table.CardsOnTable[3] = new Card(46);
            this.table.CardsOnTable[4] = new Card(42);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(315, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(315, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForThreeOfAKind_WithPairOnTable()
        {

            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(41);
            player1.PlayerCards[1] = new Card(37);

            this.table.CardsOnTable[0] = new Card(1);
            this.table.CardsOnTable[1] = new Card(40);
            this.table.CardsOnTable[2] = new Card(43);
            this.table.CardsOnTable[3] = new Card(20);
            this.table.CardsOnTable[4] = new Card(10);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(330, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.ThreeOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(330, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForFourOfAKind_WithTwoEqualCardInPlayerHand()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(44);
            player1.PlayerCards[1] = new Card(45);

            this.table.CardsOnTable[0] = new Card(1);
            this.table.CardsOnTable[1] = new Card(40);
            this.table.CardsOnTable[2] = new Card(46);
            this.table.CardsOnTable[3] = new Card(47);
            this.table.CardsOnTable[4] = new Card(10);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.FourOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(744, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.FourOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(744, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForFourOfAKind_WithFourEqualCardsOnTable()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(8);
            player1.PlayerCards[1] = new Card(24);

            this.table.CardsOnTable[0] = new Card(28);
            this.table.CardsOnTable[1] = new Card(40);
            this.table.CardsOnTable[2] = new Card(29);
            this.table.CardsOnTable[3] = new Card(30);
            this.table.CardsOnTable[4] = new Card(31);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.FourOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(728, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.FourOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(728, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForFourOfAKind_WithFourAce()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(0);
            player1.PlayerCards[1] = new Card(24);

            this.table.CardsOnTable[0] = new Card(1);
            this.table.CardsOnTable[1] = new Card(40);
            this.table.CardsOnTable[2] = new Card(2);
            this.table.CardsOnTable[3] = new Card(30);
            this.table.CardsOnTable[4] = new Card(3);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.FourOfAKind, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(752, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.FourOfAKind, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(752, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForStraightFlushOfSpades_WithAceAndJackInPlayerHand()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(3);
            player1.PlayerCards[1] = new Card(43);

            this.table.CardsOnTable[0] = new Card(1);
            this.table.CardsOnTable[1] = new Card(39);
            this.table.CardsOnTable[2] = new Card(47);
            this.table.CardsOnTable[3] = new Card(51);
            this.table.CardsOnTable[4] = new Card(3);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.RoyalFlush, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(913, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.RoyalFlush, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(913, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForStraightFlushOfHearts_FromTwoToSix()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(18);
            player1.PlayerCards[1] = new Card(43);

            this.table.CardsOnTable[0] = new Card(6);
            this.table.CardsOnTable[1] = new Card(39);
            this.table.CardsOnTable[2] = new Card(10);
            this.table.CardsOnTable[3] = new Card(14);
            this.table.CardsOnTable[4] = new Card(22);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.StraightFlush, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(805, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.StraightFlush, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(805, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForStraightFlushOfClubsFromAceToFive()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(0);
            player1.PlayerCards[1] = new Card(16);

            this.table.CardsOnTable[0] = new Card(12);
            this.table.CardsOnTable[1] = new Card(33);
            this.table.CardsOnTable[2] = new Card(4);
            this.table.CardsOnTable[3] = new Card(8);
            this.table.CardsOnTable[4] = new Card(46);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.StraightFlush, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(804, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.StraightFlush, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(804, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForStraightFlushOfClubsFromTwoToSixWithJackOfClubs()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(40);
            player1.PlayerCards[1] = new Card(16);

            this.table.CardsOnTable[0] = new Card(12);
            this.table.CardsOnTable[1] = new Card(33);
            this.table.CardsOnTable[2] = new Card(4);
            this.table.CardsOnTable[3] = new Card(8);
            this.table.CardsOnTable[4] = new Card(20);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.StraightFlush, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(805, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.StraightFlush, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(805, rules.Sorted.Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_CheckForStraightFlushOfDiamondsFromFourToEightWithAceOfDiamonds()
        {
            IRules rules = new Rules();
            IPlayer player1 = new Human("player");
            player1.PlayerCards[0] = new Card(1);
            player1.PlayerCards[1] = new Card(17);

            this.table.CardsOnTable[0] = new Card(46);
            this.table.CardsOnTable[1] = new Card(13);
            this.table.CardsOnTable[2] = new Card(21);
            this.table.CardsOnTable[3] = new Card(29);
            this.table.CardsOnTable[4] = new Card(25);
            this.dataBase.Table = this.table;
            rules.CheckForHand(player1);

            Assert.AreEqual(TypeOfTheHand.StraightFlush, player1.HandFactor, "Player.HandFactor not correct");
            Assert.AreEqual(807, player1.Power, "Player.Power not correct");
            Assert.AreEqual(TypeOfTheHand.StraightFlush, rules.Sorted.HandFactor, "Rules.win.current not correct");
            Assert.AreEqual(807, rules.Sorted.Power, "Rules.win.power not correct");
        }


    }
}
