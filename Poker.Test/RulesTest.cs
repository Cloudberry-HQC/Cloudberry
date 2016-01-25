using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Core;
using Poker.Forms;
using Poker.Interfaces;
using Poker.Models.Card;
using Poker.Models.Player;
using Poker.Models.PokerManagement;

namespace Poker.Test
{
    

    [TestClass]
  public class RulesTest
    {
       
       private Database db = Database.Instace;
       
       PokerTable table =new PokerTable();

       
        
        [TestMethod]
        public void CheckForHandTest_RHighCard()
        {
            IRules rules=new Rules();
            IPlayer player=new Human("player");
            player.PlayerCards[0]=new Card(2);
            player.PlayerCards[1] = new Card(51);
            
            this.table.CardsOnTable[0] = new Card(37);
            this.table.CardsOnTable[1] = new Card(29);
            this.table.CardsOnTable[2] = new Card(12);
            this.table.CardsOnTable[3] = new Card(22);
            this.table.CardsOnTable[4] = new Card(42);
            
            this.db.Table = this.table;
            rules.CheckForHand(player);
            Assert.AreEqual(-1, player.Current,"Player.Current not correct");
            Assert.AreEqual(13, player.Power, "Player.Power not correct");
            Assert.AreEqual(-1, rules.Win[0].Current, "Rules.win.current not correct");
            Assert.AreEqual(13, rules.Win[0].Power, "Rules.win.power not correct");
            Assert.AreEqual(13, rules.Sorted.Power);
            Assert.AreEqual(-1, rules.Sorted.Current);
        }

        [TestMethod]
        public void CheckForHandTest_RPairFromHand_PairFromPlayerCards()
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
            this.db.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(1, player1.Current, "Player.Current not correct");
            Assert.AreEqual(148, player1.Power, "Player.Power not correct");
            Assert.AreEqual(1, rules.Win[0].Current, "Rules.win.current not correct");
            Assert.AreEqual(148, rules.Win[0].Power, "Rules.win.power not correct");
        }
        [TestMethod]
        public void CheckForHandTest_RPairFromHand_PairFromTableCards()
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
            this.db.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(1, player1.Current, "Player.Current not correct");
            Assert.AreEqual(149, player1.Power, "Player.Power not correct");
            Assert.AreEqual(1, rules.Win[0].Current, "Rules.win.current not correct");
            Assert.AreEqual(149, rules.Win[0].Power, "Rules.win.power not correct");
        }

        [TestMethod]
        public void CheckForHandTest_RPairTwoPair_TwoPairFromTableCards()
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
            this.db.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(2, player1.Current, "Player.Current not correct");
            Assert.AreEqual(234, player1.Power, "Player.Power not correct");
            Assert.AreEqual(2, rules.Sorted.Current, "Rules.win.current not correct");
            Assert.AreEqual(234, rules.Sorted.Power, "Rules.win.power not correct");
        }
        [TestMethod]
        public void CheckForHandTest_RPairTwoPair_PairOnTables()
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
            this.db.Table = this.table;
            rules.CheckForHand(player1);
            Assert.AreEqual(0, player1.Current, "Player.Current not correct");
            Assert.AreEqual(15, player1.Power, "Player.Power not correct");
            Assert.AreEqual(1, rules.Sorted.Current, "Rules.win.current not correct");
            Assert.AreEqual(15, rules.Sorted.Power, "Rules.win.power not correct");
        }

    }
}
