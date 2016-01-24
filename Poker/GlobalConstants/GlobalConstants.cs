namespace Poker.GlobalConstants
{
    /// <summary>
    /// Holds all constant values in the project
    /// </summary>
    public class GlobalConstants
    {
        //Names of the players
        public const string HumanPlayerName = "Human";
        public const string FirstBotPlayerName = "First Bot";
        public const string SecondBotPlayerName = "Second Bot";
        public const string ThirdBotPlayerName = "Third Bot";
        public const string FourthBotPlayerName = "Fourth Bot";
        public const string FifthBotPlayerName = "Fifth Bot";

        //Win messages based on player name
        public const string HumanPlayerWinMessage = HumanPlayerName + " Wins";
        public const string FirstBotPlayerWinMessage = FirstBotPlayerName + " Wins";
        public const string SecondBotPlayerWinMessage = SecondBotPlayerName + " Wins";
        public const string ThirdBotPlayerWinMessage = ThirdBotPlayerName + " Wins";
        public const string FourthBotPlayerWinMessage = FourthBotPlayerName + " Wins";
        public const string FifthBotPlayerWinMessage = FifthBotPlayerName + " Wins";

        //File paths and extensions
        public const string PlayingCardsPath = "..\\..\\Resources\\Assets\\Cards\\";
        public const string PlayingCardsDirectoryPath = "..\\..\\Resources\\Assets\\Cards";
        public const string PlayingCardsWithPngExtension = "*.png";
        public const string PlayingCardsExtension = ".png";
        public const string PlayingCardsBackPath = "..\\..\\Resources\\Assets\\Back\\Back.png";

        //UI Messages
        public const string PlayAgainMessage = "Would You Like To Play Again ?";
        public const string WinMessage = "You Won , Congratulations ! ";
        public const string YourTurnMessage = "Your turn";

        //Button messages
        public const string AllInMessage = "All in";
        public const string FoldMessage = "Fold";
        public const string RaiseMessage = "Raise";
        public const string CallMessage = "Call";
        public const string CheckMessage = "Check";

        //Gameplay error messages
        public const string RaiseErrorMessage = "You must raise atleast twice as the current raise !";
        public const string NumberOnlyFieldErrorMessage = "This is a number only field";

        public const string SmallBlindRoundNumberErrorMessage = "The Small Blind can be only round number !";
        public const string SmallBlindMaxValueMessage = "The maximum of the Small Blind is 100 000 $";
        public const string SmallBlindMinValueMessage = "The minimum of the Small Blind is 250 $";

        public const string BigBlindRoundNumberErrorMessage = "The Big Blind can be only round number !";
        public const string BigBlindMinValueMessage = "The minimum of the Big Blind is 500 $";
        public const string BigBlindMaxValueMessage = "The maximum of the Big Blind is 200 000";

        public const string SavedChangesMessage = "The changes have been saved ! They will become available the next hand you play. ";
    }
}