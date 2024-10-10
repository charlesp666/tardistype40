using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

using System;
using System.ComponentModel;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.ApplicationModel.Core;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.Graphics.Display;                                      //For Adjusting App Window size
using Windows.Media.Core;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;                                    //To load Help Instructions from Text File
//using Windows.UI.Popups;
using Windows.UI.ViewManagement;             //For ApplicationView Object; adjusting App Window size

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

/*******************************************************************************************
* Class to use for Notification
*/
public abstract class NotifyBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

/*******************************************************************************************
* Class for Current Activity Refreshing
*/
public class CurrentActivity : NotifyBase
{
    private string txtCurrentActivity;

    public string currentActivity
    {
        get { return this.txtCurrentActivity; }
        set
        {
            if (value != this.txtCurrentActivity)
            {
                this.txtCurrentActivity = value;
                OnPropertyChanged();
            }
        }
    }
}

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameTableau : Page
    {
        /*******************************************************************************************
        * Objects for Property Change Notification
        */
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        /*******************************************************************************************
        * Class Variables and Constants
        */
        //Create Player and GameInformation Objects
        //private Player myAvatar = new Player();                  //Storage for Current Player Object
        private GameInformation myGameInfo = new GameInformation();  //Local Game Information Object

        //Declare and Initialize Game Playing Deck(s)
        public Cards gameDeck = new Cards(true);                              //Initialize Deck of Cards

        //private static String folderPlayableIcons = "ms-appx://Assets//GameImages//";
        private static String folderGameData = "ms-appx:///Assets//Data//";

        private String helpText = null;

        // Define variables/constants for play area (main window)
        private static int numberPlayRows = Cards.Card.possibleSuits.Length;        //Play Area Rows
        private static int numberPlayColumns = Cards.Card.possibleRanks.Length;  //Play Area Columns

        private static String playSpace = "";             //String or character to use on play spots

        private bool playKingPosition = false;          //Flag Indicating the Position is for a King

        private static bool isGameSet = false;                  // Flag indicates play area is ready
        public bool flgGameOver = true;                               //Flag identifies game is over

        // Define parameters for Scoring Games (Determining Player's Winnings)
        private int incrementSequence = 2;             //Points to add for cards in correct sequence
        private int incrementPosition = 5;             //Points to add for cards in correct position
        private int incrementCompleteSuit = 10;                  //Points to add for a complete suit

        private int gameWinningBonus = 100;                      //Bonus Amount for a completed Suit
        private int moveCount = 0;                        //Counter for Number of Moves Made in game

        //private PlayPosition tempStorage;      //Storage for PlayPosition Object-Needed to Move King

        //"Public" Xaml access for Playable and Non-Playable Cards
        public Cards.Card cardPlayable
        {
            get;
            set;
        }

        public Cards.Card cardNotPlayable
        {
            get;
            set;
        }

        /*******************************************************************************************
        * Objects for INotify refresh of window
        */
        //Card Object to flag INotify when card changes
        //String to Refresh "Current Activity" message
        CurrentActivity currentActivity = new CurrentActivity();

        //private string CurrentActivity;

        //public string currentActivity
        //{
        //    get { return this.CurrentActivity; }
        //    set
        //    {
        //        if (value != this.CurrentActivity)
        //        {
        //            this.CurrentActivity = value;
        //            NotifyPropertyChanged("currentActivity");
        //        }
        //    }
        //}

        /*******************************************************************************************
        * Class Variables and Constants
        */
        //private Stack<UndoItem> myUndoItems = new Stack<UndoItem>();

        //private UndoBuffer myUndoBuffer = new UndoBuffer();                 //Create the Undo Buffer
        private static int displayDelayMS = 150;      //Action display delay so user can see changes

        //Below Parameters used to reflect Game time and store Accumulated play time
        private DateTime gameStartTime;                                            //Game Start Time
        private DateTime gameEndTime;                                      //Game "Now" and end time

        public GameTableau()
        {
            this.InitializeComponent();

            //currentActivity = new CurrentActivity();
            currentActivity.currentActivity = "Beginning...";

            //Get Text for Game Instructions
            loadHelpText();

            //Clear the Game Deck to initialize the Game Board, and prepare for new game
            cardPlayable = new Cards.Card("n", "p", gameDeck.getCardFacePlayable());
            cardNotPlayable = new Cards.Card("p", "l", gameDeck.getCardFaceNotPlayable());

            clearDeck();

            //Build the Initial Game Board and set Data Context
            buildInitialGameBoard();

            currentActivity.currentActivity = "Waiting for User Input...";

            //Junk Code to announce completion of GameTableau--Remove when tableau is working  *****
            string aMsg = "This is the end, my only friend, the end...";
            speakText(aMsg);
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         EVENT HANDLERS                              ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
         * Event Handler: GameBoard_CellClicked
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void dataGridGameBoard_CellClick(object sender, ItemClickEventArgs e)
        {
            int indexClickedCell = dataGridGameBoard.SelectedIndex;

            string speakingText = "Clickety Click on Index " + indexClickedCell.ToString();
            speakText(speakingText);

            //Cards.Card selectedCard = gameDeck.getCard(indexClickedCell);
            //SelectedCard(selectedCard);
        }

        /*******************************************************************************************
         * Menu: Help/About
         * Displays the Help/About dialog
         */
        private async void SelectedCard(Cards.Card aCard)
        {
            string theCard = aCard.cardRank + aCard.cardSuit;

            ContentDialog dlgSelectedCard = new ContentDialog
            {
                Title = "Selected Card is:",
                Content = theCard,
                CloseButtonText = "OK"
            };

            //set the XamlRoot property
            dlgSelectedCard.XamlRoot = btnHelp.XamlRoot;

            ContentDialogResult result = await dlgSelectedCard.ShowAsync();
        }

        /*******************************************************************************************
         * Event Handler: btnExit_Click
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            exitGame(sender, e);
        }

        /*******************************************************************************************
         * Event Handler: Help
         * Displays the Help/About dialog
         */
        private async void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            currentActivity.currentActivity = "Displaying How to Play...";
            ContentDialog dlgGameInstructions = new ContentDialog
            {
                Title = "How to Play Leapfrog",
                Content = helpText,
                CloseButtonText = "OK"
            };

            //set the XamlRoot property
            dlgGameInstructions.XamlRoot = btnHelp.XamlRoot;

            ContentDialogResult result = await dlgGameInstructions.ShowAsync();
            currentActivity.currentActivity = "Waiting...";
        }

        /*******************************************************************************************
         * Event Handler: New Game
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            loadDeck();                                  //Load the Game Deck from the Standard Deck

            setUpNewGame(gameDeck);                            //Shuffle and Deal Cards for new game
        }

        /*******************************************************************************************
         * Event Handler: Player Statistics
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            //myAvatar.displayPlayerStats();
        }

        /*******************************************************************************************
        * Event Handler: gameTimerTick
        * Displays the elapsed time for the current game.
        */

        //private void gameTimerTick(object sender, EventArgs e)
        //{
        //    TimeSpan elapsedTime = computeTimePlayed();                  //Compute Current Game Time

        //    String timeDisplay;                        //String to Store the Elapsed Time to Display
        //    timeDisplay = elapsedTime.ToString();                     //Convert Elapsed Time to Text

        //    //Display elapsed time after removing milliseconds
        //    lblGameTimer.Text = timeDisplay.Substring(0, (timeDisplay.IndexOf('.')));
        //}
        #endregion

        /*******************************************************************************************
         *******************************************************************************************
         ***********                          MENU OPTIONS                               ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
         * Menu: Game/Exit
         * Handles the Closing of the Game Tableau Windows Form.
         */
        //private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    exitGame();
        //}

        /*******************************************************************************************
         * Menu: Game/Player Statistics
         * Displays the Current Player Statistics Message Box.
         */
        //private void playerStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    myPlayer.displayPlayerStats();
        //}

        /*******************************************************************************************
         * Menu: Game/Undo
         * Displays the Current Player Statistics Message Box.
         */
        //private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    undoMove();
        //}
        #endregion

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         CLASS METHODS                               ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
          * Method: buildInitialGameBoard
          * Initializes the Rows and Columns of the Game Grid and Configures Display and Other
          * Options.
          */
        private void buildInitialGameBoard()
        {
            currentActivity.currentActivity = "Building Initial Game Board...";

            //Configure the Game Playing Grid
            gameTableau.Background = myGameInfo.getBackgroundColor();       //Set Tableau Background

            dataGridGameBoard.Background = myGameInfo.getBackgroundColor();   //Set Background Color
            dataGridGameBoard.AllowFocusOnInteraction = true;
            dataGridGameBoard.IsEnabled = true;

            //Set SelectedIndex to No item
            dataGridGameBoard.SelectedIndex = -1;
            dataGridGameBoard.SelectedItem = null;

            currentActivity.currentActivity = "Waiting...";
        }

        /*******************************************************************************************
         * Method: clearDeck
         * Clears the Game Deck of Cards for Initiating Display and new Game
         */
        private void clearDeck()
        {
            foreach (Cards.Card aCard in gameDeck.deckCards)
            {
                //aCard.cardFace = "";
            }
        }

        /*******************************************************************************************
         * Method: clearBoard
         * Clears the Playing board (tableau) for a new game.
         * 
         * aDeck - Deck Object containing cards to be dealt.
         */
        //private void clearBoard(Cards.Deck aDeck)
        //{
        //    for (int aRow = 0; aRow < numberPlayRows; aRow++)
        //    {
        //        for (int aCol = 0; aCol < numberPlayColumns; aCol++)
        //        {
        //            //Assign the Various Fields in the Cell
        //            dataGridGameBoard[aCol, aRow].ToolTipText = String.Empty;
        //            dataGridGameBoard[aCol, aRow].Tag = String.Empty;
        //            dataGridGameBoard[aCol, aRow].Value = aDeck.getCardBack();
        //        }
        //    }
        //}

        /*******************************************************************************************
         * Method: computeTimePlayed
         * Computes the Time Played for Last Game
         */
        private TimeSpan computeTimePlayed()
        {
            gameEndTime = System.DateTime.Now;                  //Set the Current Game Time to "Now"
            TimeSpan elapsedTime = gameEndTime - gameStartTime;          //Compute Current Game Time

            return elapsedTime;
        }

        /*******************************************************************************************
         * Method: dealCards
         * Assigns the values in the individual elements of the card array to the cells in the
         * play area (grid).
         * 
         * aDeck - Deck Object containing cards to be dealt.
         */
        private void dealCards(Cards aDeck)
        {
            currentActivity.currentActivity = "Dealing Cards...";

            int countCards = aDeck.deckCards.Count;

            for (int aCard = 0; aCard < countCards; aCard++)
            {
                aDeck.deckCards.ResetItem(aCard);
                //NotifyPropertyChanged("aDeck.deckCards[aCard]");
                delay(displayDelayMS);                    //Pause Deal for user to see cards dealt
            }
            //this.dataGridGameBoard.ItemsSource = null;
            //this.dataGridGameBoard.ItemsSource = aDeck.deckCards;

            //for (int aRow = 0; aRow < numberPlayRows; aRow++)
            //{
            //    for (int aCol = 0; aCol < numberPlayColumns; aCol++)
            //    {
            //        //Compute Card Element from Deck Array to Deal
            //        int arrayElement = aDeck.calcArrayPosition(aRow, aCol);

            //        dataGridGameBoard.SelectedIndex = arrayElement;
            //        dataGridGameBoard.SelectedItem = null;
            //        dataGridGameBoard.SelectedItem = aDeck.deckCards[arrayElement];

            //        NotifyPropertyChanged();

            //        delay(displayDelayMS);                    //Pause Deal for user to see cards dealt
            //    }
            //}

            currentActivity.currentActivity = "";
        }

        /*******************************************************************************************
         * Method: delay
         * Pause Processing for specified number of milliseconds.
         */
        private void delay(int milliSecondsToPauseFor)
        {
            System.DateTime startInstant = System.DateTime.Now;
            System.DateTime thisInstant = startInstant;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
            System.DateTime finalInstant = thisInstant.Add(duration);

            while (finalInstant >= thisInstant)
            {
                thisInstant = System.DateTime.Now;
            }
        }

        /*******************************************************************************************
         * Method: displayMessage
         * Displays the informational Message passed as parameter.
         */
        //private void displayMessage(String theMessage)
        //{
        //    MessageBox.Show(theMessage, "Leapfrog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        /*******************************************************************************************
         * Method: displayWarning
         * Displays the Warning message passed as parameter.
         */
        //private void displayWarning(String theMessage)
        //{
        //    MessageBox.Show(theMessage, "Leapfrog", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}

        /*******************************************************************************************
         * Method: exitGame
         * Closes Out all processing and Exits or Closes the Application.
         */
        private async void exitGame(object sender, RoutedEventArgs e)
        {
            await ApplicationView.GetForCurrentView().TryConsolidateAsync();
        }

        /*******************************************************************************************
         * Method: endGame
         * Process finishes all the tasks that are necessary when a game has ended; i.e.
         * accumulate the score for the game, set the public "Game Over" flag, etc.
         */
        //private void endGame()
        //{
        //    gameTime.Stop();                                                   //Stop the Game Timer

        //    displayMessage("Game Over!");
        //    scoreGame();               //Compute Score for Current Game and Update Player Statistics

        //    flgGameOver = true;                                               //Set "Game Over" flag
        //    isGameSet = false;                                               //Set the game set flag
        //    playKingPosition = false;                             //Ensure Moving King Flag is Unset

        //    clearBoard(gameDeck);                                                //Clear the tableau
        //    lblGameTimer.Text = String.Empty;                             //Clear the Timer Text box
        //    moveCount = -1;                                                 //Reset the move counter
        //    txtMoveCount.Text = moveCount.ToString();                //Clear the Move count Text box
        //}

        /*******************************************************************************************
         * Function: isCorrectPosition
         * Compares card position in row and determines if this is correctly placed. Returns "true"
         * if card is in correct position; otherwise returns false.
         */
        private bool isCorrectPosition(PlayPosition aCard)
        {
            bool placedCorrectly = false;                                 //Set default return value
            Cards.Card aDummy = new Cards.Card();         //Create Dummy Card to Access Card Methods

            if (!(aCard.getCard().Equals(playSpace)))
            {
                String thisRank = aDummy.getRank(aCard.getCard());           //Get Current Card Rank
                int correctPosition = aDummy.findRank(thisRank);    //Get Position in Possible Ranks
                correctPosition = Math.Abs(correctPosition - 12);         //Adjust for Reverse Order

                placedCorrectly = (aCard.getColumn() == correctPosition);  //Compute Correct Placing
            }

            return placedCorrectly;
        }

        /*******************************************************************************************
         * Function: isGameOver
         * Process walks through the play area checking if all play positions are 
         * playable.Returns true if no more plays can be made; false if still playable.
         */
        //private bool isGameOver()
        //{
        //    int countPlayPositions = 0;
        //    for (int aRow = 0; aRow < dataGridGameBoard.Rows.Count; aRow++)
        //    {
        //        for (int aCol = 0; aCol < dataGridGameBoard.Columns.Count; aCol++)
        //        {
        //            if (isPlayable(aCol, aRow))                              //If Cell is Playable...
        //            {
        //                countPlayPositions++;                  //Increment Playable position counter
        //            }
        //        }
        //    }

        //    return (countPlayPositions == 0);           //Return "True" if no positions are playable
        //}

        /*******************************************************************************************
         * Function: isKing
         * Function verifies the selected card is a king by checking if the Card String contains
         * the value of the highest possible Card Rank. Returns "True" if the card matches; 
         * othewise returns "False."
         */
        private bool isKing(String aCard)
        {
            return aCard.StartsWith(Cards.Card.possibleRanks[Cards.Card.possibleRanks.Length - 1]);
        }

        /*******************************************************************************************
         * Function: isKingPosition
         * Checks if the Column for the position is for Rank of King; returns "True" if it is,
         * otherwise returns false.
         */
        private bool isKingPosition(int aCol)
        {
            return (aCol == 0);
        }

        /*******************************************************************************************
         * Function: isPlayable
         * Using the row and column positions process determines if the selected position
         * is playable. Returns "true" if current position is playable; otherwise returns "false."
         */
        //private bool isPlayable(int colPosition, int rowPosition)
        //{
        //    bool retVal = true;                                          // Set default return value

        //    if (dataGridGameBoard[colPosition, rowPosition].Tag.ToString().Equals(playSpace))
        //    {
        //        if (colPosition > 0)                                  //If Not the Leftmost Column...
        //        {
        //            //Check if the Cell to Left is "Playable"
        //            String cardToLeft = dataGridGameBoard[colPosition - 1, rowPosition].Tag.ToString();
        //            if (cardToLeft.Equals(playSpace))                 //If the cell is "PlaySpace"...
        //            {
        //                retVal = false;                               //Set Return to "Not-Playable"
        //            }
        //            else if (cardToLeft.Substring(0, 1).Equals("2"))         //Or if it is a Deuce...
        //            {
        //                retVal = false;                               //Set Return to "Not-Playable"
        //            }
        //            else                                              //Otherwise, it is playable...
        //            {
        //                retVal = true;                                    //Set Return to "Playable"
        //            }
        //        }
        //        else                                            //Else, It is the Leftmost Column...
        //        {
        //            retVal = true;                                        //Set Return to "Playable"
        //        }

        //        //Set the Icon for the Cell
        //        if (retVal)                                            //If the Cell is Playable...
        //            dataGridGameBoard[colPosition, rowPosition].Value = playSpaceIcon;
        //        else
        //            dataGridGameBoard[colPosition, rowPosition].Value = noPlayIcon;
        //    }
        //    else                                                          //The Cell is Not Playable
        //    {
        //        retVal = false;                                       //Set Return to "Not-Playable"
        //    }

        //    return retVal;
        //}

        /*******************************************************************************************
         * Method: isSuitComplete
         * Walks through a row on the Tableau to determine if the suit for that row
         * is complete. For a suit to be complete, a King should be found in the leftmost
         * column, and the rank of the same suit should decrease down to the deuce in the
         * second column from the end. Strictly speaking, the rightmost column should be
         * a "play space" character, but for the purposes of scoring, this is unimportant.
         */
        //private bool isSuitComplete(int currentRow)
        //{
        //    int currentCol = 0;

        //    while (currentCol < Cards.Card.possibleRanks.Length - 2)        //If not at end of row...
        //    {
        //        PlayPosition thisCard = new PlayPosition(dataGridGameBoard, currentCol, currentRow);
        //        String currentSuit = "";                  //Store the Current Suit from first Column

        //        if (thisCard.getCard().Equals(playSpace))               //If Card is a Play Space...
        //            return false;

        //        if (currentCol == 0)                                         //If leftmost column...
        //            currentSuit = thisCard.getSuit(thisCard.getCard());       //Get the Current Suit

        //        if (!(thisCard.getSuit(thisCard.getCard()).Equals(currentSuit)))//If not Correct Suit
        //            return false;

        //        if (!isCorrectPosition(thisCard))                //If Card not in Correct Position...
        //            return false;

        //        currentCol++;                                                   //Move to Next Column
        //    }

        //    return true;
        //}

        /*******************************************************************************************
         * Method: loadDeck
         * Loads the Game Deck from Standard Deck of Cards; prepares Game Deck to play game.
         */
        private void loadDeck()
        {
            Cards standardDeck = new Cards(true);       //Create temporary, unshuffled deck of Cards
            int countCards = gameDeck.deckCards.Count;

            for (int aCard = 0; aCard < countCards; aCard++)
            {
                gameDeck.deckCards[aCard] = standardDeck.deckCards[aCard];
            }
        }

        /*******************************************************************************************
        * Method: loadHelpText
        * Loads the Intstructions on How to Play the Game from Text file in Assets folder.
        */
        private async void loadHelpText()
        {
            currentActivity.currentActivity = "Loading Help Text...";
            string fileInstructions = folderGameData + "GameInstructions.txt";
            var HelpFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(fileInstructions));

            helpText = await FileIO.ReadTextAsync(HelpFile);

            currentActivity.currentActivity = "";
        }

        /*******************************************************************************************
         * Method: moveCard
         * Moves the Card from the Source Position to the Destination Position, then checks if the
         * move ended the game.
         */
        //private void moveCard(PlayPosition sourcePosition, PlayPosition destinationPosition)
        //{
        //    if (sourcePosition != destinationPosition)   //If the Source and Destination not Equal...
        //        swapPlayCards(sourcePosition, destinationPosition);         //Move the Selected Card

        //    playKingPosition = false;                                         //Ensure Flag is Unset

        //    if (isGameOver())                           //Check if game still has playable positions
        //    {
        //        endGame();                   //Close out the current game, and set appropriate flags
        //    }
        //}

        /*******************************************************************************************
         * playSpaceClicked
         * Actions to perform when mouse is Clicked. Process determines the Grid
         * Component that was clicked then initiates a card move.
         */
        //public void playSpaceClicked(int playCol, int playRow)
        //{
        //    if (isGameSet)                                         //Playing Area is set for play...
        //    {
        //        if (isPlayable(playCol, playRow) || playKingPosition)    //Playable or King Space...
        //        {
        //            PlayPosition destinationPosition = new PlayPosition(dataGridGameBoard, playCol, playRow);
        //            PlayPosition sourcePosition = destinationPosition;         //Set a "Dummy" Value

        //            if (isKingPosition(playCol))               //If Clicked Cell is Leftmost Cell...
        //            {
        //                tempStorage = destinationPosition;           //Store the Current Destination

        //                displayMessage("Select King to Move to this Position...");  //Prompt User...
        //                playKingPosition = true;                         //Set King being Moved Flag
        //            }
        //            else if (!playKingPosition)                            //If not Moving a King...
        //            {
        //                sourcePosition = destinationPosition.findPlayCard(dataGridGameBoard);
        //            }
        //            else if (playKingPosition)                           //If King is being Moved...
        //            {
        //                if (!isKing(destinationPosition.getCard()))  //If a King was not selected...
        //                {
        //                    displayWarning("King was not selected; cancelling move!");
        //                    sourcePosition = destinationPosition;
        //                }
        //                else
        //                {
        //                    sourcePosition = destinationPosition;//Set Source to Current Destination
        //                    destinationPosition = tempStorage;        //Restore Original Destination
        //                }

        //                playKingPosition = false;                  //Unset the King being Moved Flag
        //            }

        //            //Destination and Source should be set, move the selected card
        //            if (!playKingPosition)
        //            {
        //                moveCard(sourcePosition, destinationPosition);     //Move the Designated Card

        //                moveCount++;                                         //Increment Move Counter
        //                txtMoveCount.Text = moveCount.ToString();            //Display Count of Moves
        //            }
        //        }
        //    }
        //}

        /*******************************************************************************************
         * Method: removeAces
         * Removes the Aces from the playing area in order to initialize the play spots.
         */
        public void removeAces()
        {
            currentActivity.currentActivity = "Removing Aces...";

            int arrayPosition = 0;

            for (int aRow = 0; aRow < numberPlayRows; aRow++)
            {
                for (int aCol = 0; aCol < numberPlayColumns; aCol++)
                {
                    arrayPosition = gameDeck.calcArrayPosition(aRow, aCol);
                    if (gameDeck.deckCards[arrayPosition].cardRank.ToLower() == "a")
                    {
                        int arrayElement = gameDeck.calcArrayPosition(aRow, aCol);

                        dataGridGameBoard.SelectedIndex = arrayElement;
                        dataGridGameBoard.SelectedItem = null;

                        gameDeck.deckCards[arrayPosition] = cardPlayable;
                        dataGridGameBoard.SelectedItem = gameDeck.deckCards[arrayElement];

                        //if (gameDeck.deckCards[arrayPosition - 1].cardRank.ToLower() == "2")
                        //{
                        //    gameDeck.deckCards[arrayPosition] = cardNotPlayable;
                        //}
                        //else
                        //{
                        //    gameDeck.deckCards[arrayPosition] = cardPlayable;
                        //}

                        delay(displayDelayMS);                    //Pause Deal for user to see cards dealt
                    }
                }
            }

            isGameSet = true;                                     //Set the game is set flag to true
            //isGameOver();                                      // Initialize the icons for game play
            flgGameOver = false;                                 //Set the "Game Over" flag to false

            currentActivity.currentActivity = "";
        }

        /*******************************************************************************************
         * Method: scoreGame
         * Adds up the score of the current game; scoring as follows:
         * 
         * 2 points for each card in correct sequence (by rank and suit)
         * 5 points for each card in correct position (column by rank)
         * 10 points for completion of a suit (King through 2 of same suit on same row)
         */
        //private void scoreGame()
        //{
        //    int scoreThisGame = 0;                                          //Score for Current Game

        //    // Sum score for cards that are in correct sequence and correct position
        //    for (int aRow = 0; aRow < Cards.Card.possibleSuits.Length; aRow++)
        //    {
        //        int countSequence = 0;                     //Counter for Number of Cards in Sequence
        //        bool correctPosition = false;                //Ensure Correct Position Flag is Unset

        //        for (int aCol = 0; aCol < Cards.Card.possibleRanks.Length - 1; aCol++)
        //        {
        //            PlayPosition aPosition = new PlayPosition(dataGridGameBoard, aCol, aRow);

        //            String thisCard = dataGridGameBoard[aCol, aRow].Tag.ToString();//This Card Value
        //            String nextCard = dataGridGameBoard[aCol + 1, aRow].Tag.ToString();  //Next Card

        //            if (thisCard.Equals(playSpace))              //If No Card in Current Position...
        //            {
        //                correctPosition = false;             //Ensure Correct Position Flag is Unset
        //                countSequence = 0; //Ensure Sequence Counter
        //            }
        //            else               //Card is in Current position, begin checking for sequence...
        //            {
        //                if (!(nextCard.Equals(playSpace)))        //If Next Card is not Play Space...
        //                {
        //                    correctPosition = isCorrectPosition(aPosition);
        //                    if (aPosition.getSuit(thisCard) == aPosition.getSuit(nextCard)) //Same Suit?
        //                    {
        //                        if (nextCard.Equals(gameDeck.getNextCardDescending(thisCard)))
        //                        {
        //                            countSequence++;                    //Increment Sequence Counter
        //                            scoreThisGame += incrementSequence;     //Add Score for sequence
        //                            if ((countSequence > 0) && (correctPosition))
        //                                scoreThisGame += incrementPosition; //Add Score for Position
        //                        }
        //                        else
        //                        {
        //                            if ((countSequence > 0) && (correctPosition))
        //                                scoreThisGame += incrementPosition; //Add Score for Position

        //                            countSequence = 0;           //Reset Number of Cards in Sequence
        //                            correctPosition = false; //Ensure Correct Position Flag is Unset
        //                        }
        //                    }
        //                    else              //Card of Different Suit, end sequence accumulation...
        //                    {
        //                        if ((countSequence > 0) && (correctPosition))
        //                            scoreThisGame += incrementPosition;     //Add Score for Position

        //                        countSequence = 0;               //Reset Number of Cards in Sequence
        //                        correctPosition = false;     //Ensure Correct Position Flag is Unset
        //                    }
        //                }
        //                else                   //Otherwise, next Card is a "Play Space" (no Card)...
        //                {
        //                    if ((countSequence > 0) && (correctPosition))
        //                        scoreThisGame += incrementPosition;         //Add Score for Position

        //                    countSequence = 0;                   //Reset Number of Cards in Sequence
        //                    correctPosition = false;         //Ensure Correct Position Flag is Unset
        //                }
        //            }
        //        }
        //    }

        //    //Sum Score for completed Suits - Use recursive call to function isSuitComplete
        //    int suitsCompleted = 0;     //Count number of suits that were completed; if 4, game won!

        //    for (int aRow = 0; aRow < Cards.Card.possibleSuits.Length; aRow++)
        //    {
        //        if (isSuitComplete(aRow))                  //If the row contains a completed suit...
        //        {
        //            scoreThisGame += incrementCompleteSuit;    //Increment score for a complete suit
        //            suitsCompleted++;                            //Increment suits completed counter
        //        }
        //    }

        //    if (suitsCompleted == 4)                           //If All four suits are completed...
        //        scoreThisGame += (suitsCompleted * gameWinningBonus); //Add Game winning bonus!!

        //    //Update Player Statistics then Display Results
        //    myPlayer.finishGameForPlayer(scoreThisGame, moveCount, computeTimePlayed());
        //    myPlayer.displayPlayerStats(scoreThisGame, moveCount, computeTimePlayed());
        //}

        /*******************************************************************************************
         * Method: setUpNewGame
         * Prepares the playing board, shuffles the deck of cards and initializes the tableau for
         * playing the game
         */
        private void setUpNewGame(Cards aDeck)
        {
            aDeck.shuffleDeck();                                         //Shuffle the Deck of Cards
            aDeck.cutDeck();                                                          //Cut the Deck

            dealCards(aDeck);                                        //Deal the Cards to the Tableau

            removeAces();                                    //Remove Aces to Initialize Play Spaces

            //myUndoItems.Clear();                                             //Clear the Undo Buffer

            //moveCount = 0;                              //Initialize the Move Counter for a New Game
            //txtMoveCount.Text = moveCount.ToString();                       //Display Count of Moves

            //gameStartTime = System.DateTime.Now;                 //Set the Starting Time for Game...
            //gameTime.Start();                                                  //And start the clock

            string speakingText = "New Game Setup Complete!";
            speakText(speakingText);
        }

        /*******************************************************************************************
          * Method: speakText
          * Using Windows Media speech synthesizer, speaks the text passed as parameter.
          */
        private async void speakText(string speechText)
        {
            //MediaElement mediaElement = new MediaElement();
            MediaPlayerElement mediaElement = new MediaPlayerElement(); var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = null;

            using (synth)
            {
                VoiceInformation voiceInfo =
                    (
                        from voice in SpeechSynthesizer.AllVoices
                        where voice.Gender == VoiceGender.Female
                        select voice
                    ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

                synth.Voice = voiceInfo;
                stream = await synth.SynthesizeTextToStreamAsync(speechText);
            }

            mediaElement.AutoPlay = true;
            mediaElement.Source = MediaSource.CreateFromStream(stream, stream.ContentType);
        }

        /*******************************************************************************************
        * Method: swapPlayCards
        * Copies the Card Value and Card Face from the Source Play Position to the Destination
        * Play Position, then sets the Card Face and Value of the Source to "Playable".
        */
        //private void swapPlayCards(PlayPosition sourceCard, PlayPosition destinationCard)
        //{
        //    //Copy Source Card to Destination
        //    dataGridGameBoard[destinationCard.getColumn(), destinationCard.getRow()].Tag =
        //        dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Tag;
        //    dataGridGameBoard[destinationCard.getColumn(), destinationCard.getRow()].Value =
        //        dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Value;

        //    //Set Source to "Playable"
        //    dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Tag = playSpace;
        //    dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Value = playSpaceIcon;

        //    UndoItem thisMove = new UndoItem(sourceCard, destinationCard);
        //    myUndoItems.Push(thisMove);                                 //Push Move onto Undo Buffer
        //}

        //private void undoMove()
        //{
        //    UndoItem undoMove = new UndoItem();                     //Object to store Undo Positions
        //    undoMove = myUndoItems.Pop();                             //Get the last move from stack

        //    //UndoBuffer.UndoItem myItem;                                //Local Storage for Undo Item
        //    //myItem = myUndoBuffer.pop();                                 //Get the last Move Made...
        //    swapPlayCards(undoMove.getToPosition(), undoMove.getFromPosition());      //And Undo it
        //}

        #endregion

        /***********************************************************************************************
         * Class: Play Position
         * Stores the Play Position as an object to simplify parameter passing during game play.
         **********************************************************************************************/
        #region
        public partial class PlayPosition
        {
            /*******************************************************************************************
             * Class Variables and Constants
             */
            private String cardValue;                                            //Rank and Suit of Card
            private int positionRow;                             //Row of Play Position in the Grid View
            private int positionColumn;                       //Column of Play Position in the Grid View

            const int cardsInSuit = 13;        //Number of Cards in a Suit; number of columns in Tableau

            /*******************************************************************************************
             * Constructor: PlayPosition (Default)
             * Default constructor for a PlayPostion.
             */
            public PlayPosition(GridView aGrid, int aColumn, int aRow)
            {
                cardValue = aGrid.SelectedIndex.ToString(); //[aColumn, aRow].Tag.ToString();        //Store Rank and Suit of the Card
                positionRow = aRow;                                     //Store Row of the Play Position
                positionColumn = aColumn;                            //Store Column of the Play Position
            }

            /*******************************************************************************************
             * Constructor: PlayPosition (Default)
             * 
             * Sets up the Game Tableau and Prepares for game play.
             */
            private PlayPosition()
            {
                cardValue = null;                                      //Store Rank and Suit of the Card
                positionRow = -1;                                       //Store Row of the Play Position
                positionColumn = -1;                                 //Store Column of the Play Position
            }

            /*******************************************************************************************
            * Method: computeRow
            * Computes and Returns the Tableau column Value for the Selected Card.
            */
            public int computeColumn(int indexValue)
            {
                return (int)(indexValue % cardsInSuit);
            }

            /*******************************************************************************************
              * Method: computeRow
              * Computes and Returns the Tableau row Value for the Selected Card.
              */
            public int computeRow(int indexValue)
            {
                double rowNumber = indexValue / cardsInSuit;
                return (int)Math.Truncate(rowNumber);
            }

            /*******************************************************************************************
             * Function: findPlayCard
             * Returns the Play Position object for the Card currently being played (that is, the card
             * to move to the currently selected play position).
             */
            public PlayPosition findPlayCard(GridView aGrid)
            {
                PlayPosition playCard = new PlayPosition();                 //Create Return Value Object

                playCard.cardValue = identifyPlayCard(aGrid);               //Get the Card to Search for

                //Search the Grid for the Desired Card
                foreach (var gridCard in aGrid.Items)
                {
                    if (gridCard.Equals(playCard))
                    {
                        playCard.positionColumn = computeColumn(aGrid.SelectedIndex);                       //Set the Column Value
                        playCard.positionRow = computeRow(aGrid.SelectedIndex);                             //Set the Row Value

                        break;
                    }
                }

                return playCard;
            }

            /*******************************************************************************************
             * Method: getCard
             * Return the Value for the Card (Rank and Suit) in the Play Position.
             */
            public String getCard()
            {
                return this.cardValue;
            }

            /*******************************************************************************************
             * Method: getColumn
             * Return the Value for the Column in the Play Position.
             */
            public int getColumn()
            {
                return this.positionColumn;
            }

            /*******************************************************************************************
             * Method: getRank
             * Parses the Rank from the CardValue passed.
             */
            public String getRank(String aCardValue)
            {
                int cardLength = aCardValue.Length;                         //Store Length of Card Value
                int rankLength = cardLength - 1;              //Determine Length of String for Card Rank

                return (aCardValue.Substring(0, rankLength));                        //Return Card's Rank
            }

            /*******************************************************************************************
             * Method: getSuit
             * Parses the Rank from the Card Value passed.
             */
            public String getSuit(String aCardValue)
            {
                int suitPosition = aCardValue.Length - 1;         //Determine Position in String of Suit

                return (aCardValue.Substring(suitPosition, 1));                 //Return the Card's Suit
            }

            /*******************************************************************************************
             * Method: getRow
             * Return the Value for the Row in the Play Position.
             */
            public int getRow()
            {
                return this.positionRow;
            }

            /*******************************************************************************************
             * Function: identifyPlayCard
             * Based on the array index of the selected play position, looks at the card
             * to the left, then determines the card to be played.
             */
            private String identifyPlayCard(GridView aGrid)
            {
                //Get the Rank and Suit of Card in Cell to Left
                Cards.Card searchItem = (Cards.Card)aGrid.SelectedItem; //aGrid[this.getColumn() - 1, this.getRow()].Tag.ToString();
                                                                        //String searchValue = searchItem.

                String cardSuit = searchItem.getSuit().ToString(); ;//getSuit(searchValue);                            //Get the Card's Suit
                String cardRank = searchItem.getRank().ToString();//getRank(searchValue);                            //Get the Card's Rank

                int anIndex = 2;     //Set Search Start Postion in Possible Rank (Ace and Deuce ignored)
                while (!(Cards.Card.possibleRanks[anIndex].Equals(cardRank)))
                {
                    anIndex++;
                }
                cardRank = Cards.Card.possibleRanks[anIndex - 1];            //Get the Located Card Rank

                return (cardRank + cardSuit);                                    //Return the Card Value
            }
        }

        #endregion

        /***********************************************************************************************
         * Class: Undo Buffer
         * Defines an object that stores the from and to card locations for a move.
         **********************************************************************************************/
        #region
        //public partial class UndoBuffer
        //{
        /*******************************************************************************************
        * Sub-Class UndoItem
        * Defines the structure of a single Undo Item.
        */
        public partial class UndoItem
        {
            private PlayPosition fromPosition;                    //Card Position that move was from
            private PlayPosition toPosition;                        //Card Position that move was to

            public UndoItem()
            {
            }

            public UndoItem(PlayPosition aFromPosition, PlayPosition aToPosition)
            {
                fromPosition = aFromPosition;
                toPosition = aToPosition;
            }

            public PlayPosition getFromPosition()
            {
                return (fromPosition);
            }

            public PlayPosition getToPosition()
            {
                return (toPosition);
            }
        }

        /*******************************************************************************************
         * Constructor: UndoBuffer (Default)
         * Initializes the Undo Buffer.
         */
        //public UndoBuffer()
        //{
        //     //No Initialization State required
        //}

        /*******************************************************************************************
         * Method: pop
         * "Pops" the last move made from the Stack
         */
        //public UndoItem pop()
        //{
        //    int bufferItem = myUndoItems.Count - 1;                             //Top Item in Buffer
        //    UndoItem myItem = null;                                                //Local Undo Item

        //    if(bufferItem >= 0)
        //    {
        //        myItem = myUndoItems[bufferItem];                      //Get the most recent move...
        //        myUndoItems.RemoveAt(bufferItem);              //and Remove "popped" item from stack
        //    }
        //    return (myItem);
        //}

        /*******************************************************************************************
         * Method: push
         * "Pushes" the most recent move made to the Stack
         */
        //public void push(PlayPosition aFromPosition, PlayPosition aToPosition)
        //{
        //    UndoItem newItem = new UndoItem(aFromPosition, aToPosition);
        //    myUndoItems.Add(newItem);
        //}
        //}

        #endregion
    }
}