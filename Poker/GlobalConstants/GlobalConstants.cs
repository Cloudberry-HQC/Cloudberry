namespace Poker.GlobalConstants
{
    /// <summary>
    /// Holds all constant values in the project.
    /// </summary>
    public class GlobalConstants
    {
        //Names of the players
        public const string HumanPlayerName = "Human";
        public const string FirstBotPlayerName = "First Bot";
        public const string SecondbotPlayerName = "Second Bot";
        public const string ThirdbotPlayerName = "Third Bot";
        public const string FourthBotPlayerName = "Fourth Bot";
        public const string FifthBotPlayerName = "Fifth Bot";

        //Win messages based on player name
        public const string HumanPlayerWinMessage = HumanPlayerName + " Wins";
        public const string FirstBotPlayerWinMessage = FirstBotPlayerName + " Wins";
        public const string SecondbotPlayerWinMessage = SecondbotPlayerName + " Wins";
        public const string ThirdbotPlayerWinMessage = ThirdbotPlayerName + " Wins";
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

        public const int MinRandomNumberForPairHandCall = 10;
        public const int MaxRandomNumberForPairHandCall = 16;
        public const int MinRandomNumberForPairHandRaise = 10;
        public const int MaxRandomNumberForPairHandRaise = 13;
        public const int MinRandomNumberForTwoPair = 6;
        public const int MaxRandomNumberForTwoPair = 11;
        public const int MinRandomNumberForThreeOfKindRaise = 4;
        public const int MaxRandomNumberForThreeOfKindRaise = 8;
        public const int MinRandomNumberForStraightRaise = 3;
        public const int MaxRandomNumberForStraightRaise = 8;
        public const int MinRandomNumberForFlushRaise = 3;
        public const int MaxRandomNumberForFlushRaise = 7;
        public const int MinRandomNumberForFullHouseRaise = 2;
        public const int MaxRandomNumberForFullHouseRaise = 6;
        public const int MinRandomNumberForFourOfKindRaise = 2;
        public const int MaxRandomNumberForFourOfKindRaise = 5;
        public const int MinRandomNumberForStraightFlushRaise = 1;
        public const int MaxRandomNumberForStraightFlushRaise = 3;

        public const int FactorForCalculatingThePower = 100;
        public const int NumberOfCards = 2;
        public const int DefaultValueOfBigBlind = 500;
        public const int DefaultValueOfSmallBlind = 250;

        public const int CountOfTheAvailableCardsInGame = 17;
        public const int PlayerPanelCoordinateX = 580;
        public const int PlayerPanelCoordinateY = 480;
        public const int FirstBotPanelCoordinateX = 15;
        public const int FirstBotPanelCoordinateY = 420;
        public const int SecondBotPanelCoordinateX = 75;
        public const int SecondBotPanelCoordinateY = 65;
        public const int ThirdBotPanelCoordinateX = 590;
        public const int ThirdBotPanelCoordinateY = 25;
        public const int FourthBotPanelCoordinateX = 1115;
        public const int FourthBotPanelCoordinateY = 65;
        public const int FifthBotPanelCoordinateX = 1160;
        public const int FifthBotPanelCoordinateY = 420;
    }
}