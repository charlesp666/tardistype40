using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

using System;
//using System.Diagnostics;

//using System.ComponentModel;
//using System.Diagnostics;
using System.Collections.Generic;

//using System.Collections.Immutable;
//using System.IO;
using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

//using Windows.ApplicationModel.Core;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Windows.Graphics;
//using Windows.Graphics.Display;                                      //For Adjusting App Window size
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;                                    //To load Help Instructions from Text File
using Windows.UI;
using Windows.UI.Composition;
//using Windows.UI.Popups;
//using Windows.UI.ViewManagement;             //For ApplicationView Object; adjusting App Window size
//using Windows.UI.Xaml;

using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameTableau : Page
    {
        /*******************************************************************************************
        * Class Variables and Constants
        */
        private AppWindow myWindow = null;

        //Create Player and GameInformation Objects
        //private Player myAvatar = new Player();                  //Storage for Current Player Object
        private GameInformation myGameInfo = new GameInformation();  //Local Game Information Object

        //Declare and Initialize Game Playing Deck(s)
        public Cards gameDeck = new Cards(true);                          //Initialize Deck of Cards

        //"Public" Xaml access for Playable and Non-Playable Cards
        public Cards.Card cardPlayable;
        public Cards.Card cardNotPlayable;

        //private static String folderPlayableIcons = "ms-appx://Assets//GameImages//";
        private static String folderGameData = "ms-appx:///Assets//Data//";

        private String helpText = null;

        //Media Player object to play various sounds during play; sound files
        private MediaPlayer myMediaPlayer = new MediaPlayer();

        private Uri soundNotPlayable = new Uri("ms-appx:///Assets//Sounds/NotPlayablePosition.wav");

        private int delayClearDeck = 125;                //Task Delay when Clearing the Playing area
        private int delayDealCards = 125;                            //Task Delay when Dealing Cards
        private int delayRemoveAces = 125;                           //Task Delay when removing aces

        private static int displayDelayMS = 5000;      //Action display delay so user can see changes

        private string fileInstructions = folderGameData + "GameInstructions.txt";

        // Define variables/constants for play area (main window)
        private static int numberOfSuits = Cards.Card.possibleSuits.Length;         //Play Area Rows
        private static int numberOfRanks = Cards.Card.possibleRanks.Length;      //Play Area Columns

        private bool flgGameOver = true;                              //Flag identifies game is over
        private bool isGameSet = false;                          //Flag indicates play area is ready
        private bool isKingMoving = false;                   //Flag indicating a King is being moved

        private int moveCount = 0;                        //Counter for Number of Moves Made in game

        // Define parameters for Scoring Games (Determining Player's Winnings)
        private int incrementSequence = 2;             //Points to add for cards in correct sequence
        private int incrementPosition = 5;             //Points to add for cards in correct position
        private int incrementCompleteSuit = 10;                  //Points to add for a complete suit

        private int gameWinningBonus = 100;      //Bonus Amount for a All Cards Correctly Positioned

        private UndoBuffer myUndoBuffer = new UndoBuffer();                 //Create the Undo Buffer

        //Below Parameters used to reflect Game time and store Accumulated play time
        private DateTime gameStartTime;                                            //Game Start Time
        private DateTime gameEndTime;                                      //Game "Now" and end time

        /*******************************************************************************************
        * GameTableau Constructor
        * Initializes and constructs the Game Tableau and initial game board.
        */
        public GameTableau()
        {
            this.InitializeComponent();

            myWindow = getMyAppWindow();
            myWindow.TitleBar.ExtendsContentIntoTitleBar = false;

            ResizeAppWindow(myWindow);              //Resize the AppWindow to Match GameTableau size
            CenterAppWindow(myWindow);                         //Center the AppWindow on the Display

            //Clear the Game Deck to initialize the Game Board, and prepare for new game
            cardPlayable = new Cards.Card("p", "l", gameDeck.getCardFacePlayable());
            cardNotPlayable = new Cards.Card("n", "p", gameDeck.getCardFaceNotPlayable());

            initialLoad();

            //Junk Code to announce completion of GameTableau--Remove when tableau is working  *****
            //string aMsg = "This is the end, my only friend, the end...";
            //speakText(aMsg);
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         EVENT HANDLERS                              ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
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
            ContentDialog dlgGameInstructions = new ContentDialog
            {
                Title = "How to Play Leapfrog",
                Content = helpText,
                CloseButtonText = "OK"
            };

            //set the XamlRoot property
            dlgGameInstructions.XamlRoot = btnHelp.XamlRoot;

            ContentDialogResult result = await dlgGameInstructions.ShowAsync();
        }

        /*******************************************************************************************
         * Event Handler: New Game
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            setUpNewGame();                                    //Shuffle and Deal Cards for new game
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
         * Event Handler: dataGridGameBoard_SelectionChanged
         * Initiates moving a card when a destination is selected.
         */
        private void dataGridGameBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If the Game is set for play and the Selected Item is not null...
            if (isGameSet && (dataGridGameBoard.SelectedItem != null))
            {
                var gridViewItem = dataGridGameBoard.ContainerFromItem(dataGridGameBoard.SelectedItem) as GridViewItem;
                if (gridViewItem != null)
                {
                    int indexClickedCell = dataGridGameBoard.SelectedIndex;

                    playSpaceClicked(indexClickedCell);
                }
            }
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
         ***********                   Initial Setup of Tableau                          ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
         * Method: CenterAppWindow
         * Centers the AppWindow on the Display.
         */
        private void CenterAppWindow(AppWindow myAppWindow)
        {
            DisplayArea displayArea = DisplayArea.GetFromWindowId(myAppWindow.Id, DisplayAreaFallback.Primary);

            RectInt32 displayAreaRect = displayArea.WorkArea;
            int centerX = (displayAreaRect.Width - myAppWindow.Size.Width) / 2;
            int centerY = (displayAreaRect.Height - myAppWindow.Size.Height) / 2;

            myAppWindow.Move(new PointInt32(centerX, centerY));
        }

        /*******************************************************************************************
        /* Method: getMyAppWindow
        /* 
        /* Retrieves the AppWindow Object for Current Page.
        /*/
        private AppWindow getMyAppWindow()
        {
            var myWindow = (Application.Current as App)?.m_window as MainWindow;
            var hwnd = WindowNative.GetWindowHandle(myWindow);
            var myWindowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(myWindowId);

            return appWindow;
        }

        /*******************************************************************************************
        /* Method: ResizeAppWindow
        /* 
        /* Resizes the AppWindow to the size of the page.
        /*/
        private void ResizeAppWindow(AppWindow appWindow)
        {
            int pageWidth = (int)this.Width;
            int pageHeight = (int)this.Height;
            SizeInt32 newSize = new SizeInt32(pageWidth, pageHeight);
            appWindow.Resize(newSize);
        }

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
        private async Task buildInitialGameBoard()
        {
            enableSelectionChanged(false);             //Disable the GridView SelectionChanged Event

            string aMsg = "Initiating Layout Parameters...";
            await updateCurrentActivityText(aMsg);

            await removeAces();                              //Remove Aces to Initialize Play Spaces

            //Configure the Game Playing Grid
            gameTableau.Background = myGameInfo.getBackgroundColor();       //Set Tableau Background

            dataGridGameBoard.Background = myGameInfo.getBackgroundColor();   //Set Background Color
            dataGridGameBoard.AllowFocusOnInteraction = true;
            dataGridGameBoard.IsEnabled = true;

            //Build a "Dummy" Layout
            //Cards tempDeck = new Cards(true);           //Create a working deck to shuffle, cut, etc.

            //await clearDeck();                                            //Clear the Current Layout
            //await dealCards(tempDeck);                               //Deal the Cards to the Tableau
        }

        /*******************************************************************************************
         * Method: clearDeck
         * Clears the Game Deck of Cards for Initiating Display and new Game
         */
        private async Task clearDeck()
        {

            Cards.Card blankCard = new Cards.Card("", "", gameDeck.deckCards[0].cardBack);

            for (int i = 0; i < gameDeck.deckCards.Count(); i++)
            {
                gameDeck.deckCards[i] = blankCard;

                await Task.Delay(delayClearDeck);                   //Wait for the Display to Update
                //await Task.Run(() => Thread.Sleep(delayClearDeck));
            }
        }

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
        private async Task dealCards(Cards aDeck)
        {
            int countCards = gameDeck.deckCards.Count;

            for (int aCard = 0; aCard < countCards; aCard++)
            {
                gameDeck.deckCards[aCard] = aDeck.deckCards[aCard];
                await Task.Delay(delayDealCards);
            }
        }

        /*******************************************************************************************
        * Method: enableSelectionChanged
        * Programatically enables and disables the SelectionChanged Event.
        */
        private void enableSelectionChanged(bool enableEvent = true)
        {
            if (enableEvent)
            {
                dataGridGameBoard.SelectionChanged += dataGridGameBoard_SelectionChanged;
            }
            else
            {
                dataGridGameBoard.SelectionChanged -= dataGridGameBoard_SelectionChanged;
            }
        }

        /*******************************************************************************************
         * Method: delay
         * Pause Processing for specified number of milliseconds.
         */
        //private void delay(int milliSecondsToPauseFor)
        //{
        //    System.DateTime startInstant = System.DateTime.Now;
        //    System.DateTime thisInstant = startInstant;
        //    System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
        //    System.DateTime finalInstant = thisInstant.Add(duration);

        //    while (finalInstant >= thisInstant)
        //    {
        //        thisInstant = System.DateTime.Now;
        //    }
        //}

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
        private void exitGame(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /*******************************************************************************************
         * Method: endGame
         * Process finishes all the tasks that are necessary when a game has ended; i.e.
         * accumulate the score for the game, set the public "Game Over" flag, etc.
         */
        private void endGame()
        {
            //gameTime.Stop();                                                   //Stop the Game Timer

            string aMsg = "Game Over!";
            speakText(aMsg);

            //displayMessage("Game Over!");
            //scoreGame();               //Compute Score for Current Game and Update Player Statistics

            flgGameOver = true;                                               //Set "Game Over" flag
            isGameSet = false;                                               //Set the game set flag
            isKingMoving = false;                                   //Ensure Moving King Flag is Unset

            //clearBoard(gameDeck);                                                //Clear the tableau
            //lblGameTimer.Text = String.Empty;                             //Clear the Timer Text box
            //moveCount = -1;                                                 //Reset the move counter
            //txtMoveCount.Text = moveCount.ToString();                //Clear the Move count Text box
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Check for null
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // Check the child’s type
                if (child is T childType)
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = childType;
                        break;
                    }
                }
                else
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
            }

            return foundChild;
        }

        /*******************************************************************************************
         * Method: highlightKingForMoving
         * Changes Border Color and Thickness on King that can be moved.
         */
        private async Task highlightKingForMoving(int gridIndex, bool highlightKing = false)
        {
            Color normalBorderBrush = Colors.White;
            Color highlightedBorderBrush = Colors.Black;

            int normalBorderWidth = 1;
            int highlightedBorderWidth = 5;

            Color newBorderBrush = normalBorderBrush;
            int newBorderWidth = normalBorderWidth;

            if (highlightKing)
            {
                newBorderBrush = highlightedBorderBrush;
                newBorderWidth = highlightedBorderWidth;
            }

            dataGridGameBoard.SelectedIndex = gridIndex;
            var myItem = dataGridGameBoard.SelectedItem;

            //var myContainer = dataGridGameBoard.ContainerFromItem(myItem) as GridViewItem;

            //if (myContainer != null)
            //{
            //    var border = FindChild<Border>(myContainer, "ItemBorder");
            //    if (border != null)
            //    { // Change properties directly
                    //border.BorderBrush = new SolidColorBrush(newBorderBrush);
                    //border.BorderThickness = new Thickness(newBorderWidth);

                //    await Task.Run(() => Thread.Sleep(50));
                //}
            //}
        }

        /*******************************************************************************************
         * Function: initialLoad
         * Setups up the playing area when the game first loads.
         */
        private async Task initialLoad()
        {
            //Get Text for Game Instructions
            await updateCurrentActivityText("Loading Help Text...");

            await loadHelpText();

            //Build the Initial Game Board and set Data Context
            await updateCurrentActivityText("Preparing Initial Game Board...");

            await buildInitialGameBoard();

            //tbCurrentActivity.Text = "Waiting for User Input...";
            await updateCurrentActivityText("Waiting for User Input...");
        }

        /*******************************************************************************************
         * Function: isGameOver
         * Process walks through the play area checking if all play positions are 
         * playable.Returns true if no more plays can be made; false if still playable.
         */
        private bool isGameOver()
        {
            int countPlayPositions = 0;

            for (int i = 0; i < gameDeck.deckCards.Count; i++)
            {
                Cards.Card currentCard = gameDeck.deckCards[i];
                //Check if the Current Card is a "play position" (Is not a playing card)...
                if ( (currentCard.cardRank.ToLower() == "n")
                  || (currentCard.cardRank.ToLower() == "p")
                  )
                {
                    if (isPlayable(i))                    //If Current Index is actually playable...
                    {
                        gameDeck.deckCards[i] = cardPlayable;        //Set CardFace to "Playable"...
                        countPlayPositions++;              //And increment Playable position counter
                    }
                    else
                    {
                        gameDeck.deckCards[i] = cardNotPlayable;  //Set CardFace to "NotPlayable"...
                    }
                }
            }

            flgGameOver = (countPlayPositions == 0);
            return flgGameOver;                         //Return "True" if no positions are playable
        }

        /*******************************************************************************************
         * Function: isKing
         * Function verifies the selected card is a king by checking if the Card Rank is "k." 
         * Returns "True" if the card matches; othewise returns "False."
         */
        private bool isKing(Cards.Card aCard)
        {
            return (aCard.cardRank.ToLower() == "k");
        }

        /*******************************************************************************************
         * Function: isKing
         * Function verifies the selected card is a king by checking if the Card String contains
         * the value of the highest possible Card Rank. Returns "True" if the card matches; 
         * othewise returns "False."
         */
        private bool isKing(string aCard)
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
            return ((aCol % 13) == 0);
        }

        /*******************************************************************************************
         * Function: isPlayable
         * Using the Index of the card at the "selectedPosition," determines if the selected 
         * position is playable. Returns "true" if current position is playable; otherwise returns
         * "false."
         */
        private bool isPlayable(int selectedPostion)
        {
            bool retVal = true;                                          // Set default return value

            if (!isKingPosition(selectedPostion))                   //If King Position is playable...
            {
                Cards.Card cardToLeft = gameDeck.deckCards[selectedPostion - 1];

                if ((cardToLeft.cardRank.ToLower() == "2")
                  || (cardToLeft.cardRank.ToLower() == "n")
                  || (cardToLeft.cardRank.ToLower() == "p")
                  )
                {
                    retVal = false;
                }
            }

            return retVal;
        }

        /*******************************************************************************************
         * Function: isPlayableShuffle
         * Checks if the shuffled deck has at least one playable position; returns "true" if the
         * shuffle can be played; "false" if all "Ace" positions are not playable.
         */
        private bool isPlayableShuffle(Cards aDeck)
        {
            int countPlayPositions = 0;

            for (int i = 0; i < aDeck.deckCards.Count; i++)
            {
                Cards.Card currentCard = aDeck.deckCards[i];
                //Check if the Current Card is a "play position" (Is not a playing card)...
                if ((currentCard.cardRank.ToLower() == "a") && (isPlayable(i)))
                {
                        countPlayPositions++;              //And increment Playable position counter
                }
            }

            return !(countPlayPositions == 0);   //Return "True" if at least one position is playable
        }

        /*******************************************************************************************
        * Method: loadHelpText
        * Loads the Intstructions on How to Play the Game from Text file in Assets folder.
        */
        private async Task loadHelpText()
        {
            var HelpFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(fileInstructions));

            helpText = await FileIO.ReadTextAsync(HelpFile);
        }

        /*******************************************************************************************
         * Method: moveCard
         * Moves the Card from the Source Position to the Destination Position, then checks if the
         * move ended the game.
         */
        private void moveCard(int sourceIndex, int destinationIndex)
        {
            if (sourceIndex != destinationIndex)       //If the Source and Destination not Equal...
            {
                swapPlayCards(sourceIndex, destinationIndex);              //Move the Selected Card
                moveCount++;                                             //Increment the Move Count

                isKingMoving = false;                                         //Ensure Flag is Unset
            }
        }

        /*******************************************************************************************
         * Method: playSound
         * Play the Sound in file passed as parameter.
         */
        private async Task playSound(Uri soundFile, int delayTask = 1000)
        {
            myMediaPlayer.Source = MediaSource.CreateFromUri(soundFile);
            myMediaPlayer.Play();

            await Task.Delay(delayTask);
        }

        /*******************************************************************************************
         * playSpaceClicked
         * Actions to perform when mouse is Clicked. Process determines the Grid
         * Component that was clicked then initiates a card move.
         */
        public async Task playSpaceClicked(int destinationIndex)
        {
            if ((gameDeck.deckCards[destinationIndex].cardRank.ToLower() != "p") && !isKingMoving)
            {
                await playSound(soundNotPlayable);
            }
            else
            {
                if (isPlayable(destinationIndex))                            //Playable or King Space...
                {
                    if (!isKingPosition(destinationIndex))
                    {
                        Cards.Card sourceCard = gameDeck.deckCards[destinationIndex-1];
                        Cards.Card cardToMove = gameDeck.findNextCardDescending(sourceCard);
               
                        int sourceIndex = gameDeck.findCardIndex(cardToMove);
               
                        moveCard(sourceIndex, destinationIndex);

                        moveCount++;                                         //Increment Move Counter
                        //    txtMoveCount.Text = moveCount.ToString();            //Display Count of Moves
                    }
                    else
                    {
                        isKingMoving = true;                             //Set King being Moved Flag
               
                        int sourceIndex = selectKingToMove();             //Get the King to be Moved
                        //if (!isKing(destinationPosition.getCard()))  //If a King was not selected...
                        //{
                        //    displayWarning("King was not selected; cancelling move!");
                        //    sourcePosition = destinationPosition;
                        //}
                        //else
                        //{
                        //    moveCount++;                                         //Increment Move Counter
                        //    txtMoveCount.Text = moveCount.ToString();            //Display Count of Moves
                        //}
                    }

                    isKingMoving = false;                                   //Reset King Moving Flag 

                    if (isGameOver())                   //Check if game still has playable positions
                    {
                        endGame();           //Close out the current game, and set appropriate flags
                    }
                }
            }
        }

        /*******************************************************************************************
         * Method: removeAces
         * Removes the Aces from the playing area in order to initialize the play spots, and
         * assigns the "Playable" and "Not-Playable" icons/cards as appropriate.
         */
        public async Task removeAces()
        {
            int arrayPosition = 0;

            for (int aRow = 0; aRow < numberOfSuits; aRow++)                       //For each Row...
            {
                for (int aCol = 0; aCol < numberOfRanks; aCol++)                //And each Column...
                {
                    arrayPosition = gameDeck.calcArrayPosition(aRow, aCol);   //Get Index of Card...
                    if (gameDeck.deckCards[arrayPosition].cardRank.ToLower() == "a")  //If an Ace...
                    {
                        dataGridGameBoard.SelectedIndex = arrayPosition;    //Set Current Postion...
                        dataGridGameBoard.SelectedItem = null;           //Clear Current Contents...

                        if(isPlayable(arrayPosition))                   //If the Card is Playable...
                        {
                            gameDeck.deckCards[arrayPosition] = cardPlayable;    //Assign "Playable"
                            await Task.Delay(delayRemoveAces);
                        }
                        else                                    //Otherwise, Card is not Playable...
                        {
                            gameDeck.deckCards[arrayPosition] = cardNotPlayable; //Assign "Not Playable"
                            await Task.Delay(delayRemoveAces);
                        }
                    }
                }
            }
        }

        /*******************************************************************************************
         * Method: selectKingToMove
         * Locates and "Highlighs" the Kings in the Playing tableau; then waits for one to be
         * selected.
         */
        private int selectKingToMove()
        {
            int countKings = 0;                                  //Initialize Counter of Kings found
            int maxKingCount = Cards.Card.possibleSuits.Length;                 //Max Count of Kings
            int cardIndex = 0;                               //Counter to walk through Deck of Cards

            int kingSourceIndex = -1;               //Initialize King Source Index to "not Selected"

            enableSelectionChanged(false);                   //Disable the Selection Change Event...

            string aMsg = "Marking Kings for Moving...";
            updateCurrentActivityText(aMsg);

            while (countKings < maxKingCount)               //While Not all Kinds have been found...
            {
                if (isKing(gameDeck.deckCards[cardIndex]))                    //If Card is a King...
                {
                    countKings++;                                    //Increment the King Counter...
                    if(!isKingPosition(cardIndex))      //Check if King is not in a King Position...
                    {
                        //dataGridGameBoard.SelectedIndex = cardIndex;
                        //var selectedItem = dataGridGameBoard.SelectedItem;
                        //var myItemContainer = (GridViewItem)dataGridGameBoard.ContainerFromItem(selectedItem);

                        highlightKingForMoving(cardIndex, true);
                    }
                }

                cardIndex++;                                 //Increment the Card Index to next card
            }

            enableSelectionChanged(true);                   //Reenable the Selection Change Event...
            isKingMoving = true;
            waitForKingSelection();

            return kingSourceIndex;
        }
        
        /*******************************************************************************************
         * Method: setUpNewGame
         * Prepares the playing board, shuffles the deck of cards and initializes the tableau for
         * playing the game
         */
        private async Task setUpNewGame()
        {
            await updateCurrentActivityText("Setting Up for a New Game...");

            Cards tempDeck = new Cards();              //Create a working deck to shuffle, cut, etc.
            flgGameOver = false;                                       //Set Game Over Flag to false

            await updateCurrentActivityText("Clearing the Playing Area...");
            await clearDeck();                                           //Clear the Current Layout

            //Shuffle the Deck of Cards Until Shuffled Deck has at least one playable position
            await updateCurrentActivityText("Shuffling and Cutting Cards...");
            do
            {
                await tempDeck.shuffleDeck();                            //Shuffle the Deck of Cards
                await tempDeck.cutDeck();                                             //Cut the Deck
            }
            while(!isPlayableShuffle(tempDeck));//Does Shuffled Deck has at least one playable space?

            await updateCurrentActivityText("Dealing Cards...");
            await dealCards(tempDeck);                               //Deal the Cards to the Tableau

            await updateCurrentActivityText("Removing Aces...");
            await removeAces();                              //Remove Aces to Initialize Play Spaces

            isGameSet = true;                                     //Set the game is set flag to true

            enableSelectionChanged(true);               //Enable the GridView SelectionChanged Event

            //myUndoItems.Clear();                                             //Clear the Undo Buffer

            moveCount = 0;                              //Initialize the Move Counter for a New Game
            //txtMoveCount.Text = moveCount.ToString();                       //Display Count of Moves

            //gameStartTime = System.DateTime.Now;                 //Set the Starting Time for Game...
            //gameTime.Start();                                                  //And start the clock

            updateCurrentActivityText("Click on Play Space to Move Card...");
        }

        /*******************************************************************************************
          * Method: speakText
          * Using Windows Media speech synthesizer, speaks the text passed as parameter.
          */
        private async Task speakText(string speechText)
        {
            string voiceLanguage = "en";

            MediaPlayerElement mediaElement = new MediaPlayerElement();
            var mediaPlayer = new MediaPlayer();

            var synth = new SpeechSynthesizer();
            // Set the voice
            var voices = SpeechSynthesizer.AllVoices;
            var selectedVoice = voices.First(voice => voice.Gender == VoiceGender.Female && voice.Language.Contains(voiceLanguage));
            synth.Voice = selectedVoice;

            var audioStream = await synth.SynthesizeTextToStreamAsync(speechText);

            mediaPlayer.Source = MediaSource.CreateFromStream(audioStream, audioStream.ContentType);
            mediaPlayer.Play();
        }

        /*******************************************************************************************
        * Method: swapPlayCards
        * Copies the Card Value and Card Face from the Source Play Position to the Destination
        * Play Position, then sets the Card Face and Value of the Source to "Playable" if the
        * source position is playable or "NotPlayable" otherwise.
        */
        private void swapPlayCards(int sourceIndex, int destinationIndex)
        {
            //Copy Source Card to Destination
            gameDeck.deckCards[destinationIndex] = gameDeck.deckCards[sourceIndex];

            if(isPlayable(sourceIndex))
            {
                gameDeck.deckCards[sourceIndex] = cardPlayable;
            }
            else
            {
                gameDeck.deckCards[sourceIndex] = cardNotPlayable;
            }

            //UndoItem thisMove = new UndoItem(sourceCard, destinationCard);
            //myUndoItems.Push(thisMove);                                 //Push Move onto Undo Buffer
        }

        //private void undoMove()
        //{
        //    UndoItem undoMove = new UndoItem();                     //Object to store Undo Positions
        //    undoMove = myUndoItems.Pop();                             //Get the last move from stack

        //    //UndoBuffer.UndoItem myItem;                                //Local Storage for Undo Item
        //    //myItem = myUndoBuffer.pop();                                 //Get the last Move Made...
        //    swapPlayCards(undoMove.getToPosition(), undoMove.getFromPosition());      //And Undo it
        //}

        /*******************************************************************************************
        /* Method: updateCurrentActivityText
        /* 
        /* Updates the Text in the "Current Activity" TextBlock.
        /*/
        private async Task updateCurrentActivityText(string msgCurrentActivity, int delayTask = 1000)
        {
            tbCurrentActivity.Text = msgCurrentActivity;

            await Task.Run(() => Thread.Sleep(delayTask));
        }

        /*******************************************************************************************
        * Method: waitForKingSelection
        * Waits for a King to be selected, then passes the index back to calling process.
        */
        private async void waitForKingSelection()
        {
            updateCurrentActivityText("Select King to Move...");

            //Wait for a selection to be made
            while (dataGridGameBoard.SelectedItem == null)
            {
                var selectedItem = dataGridGameBoard.SelectedItem as Cards.Card;

            }
        }
        #endregion

        /***********************************************************************************************
         * Score Game
         * Stores the procedures used to score the game.
         **********************************************************************************************/
        #region
        /*******************************************************************************************
         * Function: isCorrectPosition
         * Compares card position in row and determines if this is correctly placed. Returns "true"
         * if card is in correct position; otherwise returns false.
         */
        //private bool isCorrectPosition(PlayPosition aCard)
        //{
        //    bool placedCorrectly = false;                                 //Set default return value
        //    Cards.Card aDummy = new Cards.Card();         //Create Dummy Card to Access Card Methods

        //    if (!(aCard.getCard().Equals(playSpace)))
        //    {
        //        String thisRank = aDummy.getRank(aCard.getCard());           //Get Current Card Rank
        //        int correctPosition = aDummy.findRank(thisRank);    //Get Position in Possible Ranks
        //        correctPosition = Math.Abs(correctPosition - 12);         //Adjust for Reverse Order

        //        placedCorrectly = (aCard.getColumn() == correctPosition);  //Compute Correct Placing
        //    }

        //    return placedCorrectly;
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

        #endregion

        /***********************************************************************************************
         * Class: Play Position
         * Stores the Play Position as an object to simplify parameter passing during game play.
         **********************************************************************************************/
        #region
        //public partial class PlayPosition
        //{
        //    /*******************************************************************************************
        //     * Class Variables and Constants
        //     */
        //    private String cardValue;                                            //Rank and Suit of Card
        //    private int positionRow;                             //Row of Play Position in the Grid View
        //    private int positionColumn;                       //Column of Play Position in the Grid View

        //    const int cardsInSuit = 13;        //Number of Cards in a Suit; number of columns in Tableau

        //    /*******************************************************************************************
        //     * Constructor: PlayPosition (Default)
        //     * Default constructor for a PlayPostion.
        //     */
        //    public PlayPosition(GridView aGrid, int aColumn, int aRow)
        //    {
        //        cardValue = aGrid.SelectedIndex.ToString(); //[aColumn, aRow].Tag.ToString();        //Store Rank and Suit of the Card
        //        positionRow = aRow;                                     //Store Row of the Play Position
        //        positionColumn = aColumn;                            //Store Column of the Play Position
        //    }

        //    /*******************************************************************************************
        //     * Constructor: PlayPosition (Default)
        //     * 
        //     * Creates an "empty" play position.
        //     */
        //    private PlayPosition()
        //    {
        //        cardValue = null;                                      //Store Rank and Suit of the Card
        //        positionRow = -1;                                       //Store Row of the Play Position
        //        positionColumn = -1;                                 //Store Column of the Play Position
        //    }

        //    /*******************************************************************************************
        //    * Method: computeRow
        //    * Computes and Returns the Tableau column Value for the Selected Card.
        //    */
        //    public int computeColumn(int indexValue)
        //    {
        //        return (int)(indexValue % cardsInSuit);
        //    }

        //    /*******************************************************************************************
        //      * Method: computeRow
        //      * Computes and Returns the Tableau row Value for the Selected Card.
        //      */
        //    public int computeRow(int indexValue)
        //    {
        //        double rowNumber = indexValue / cardsInSuit;
        //        return (int)Math.Truncate(rowNumber);
        //    }

        //    /*******************************************************************************************
        //     * Function: findPlayCard
        //     * Returns the Play Position object for the Card currently being played (that is, the card
        //     * to move to the currently selected play position).
        //     */
        //    public PlayPosition findPlayCard(GridView aGrid)
        //    {
        //        PlayPosition playCard = new PlayPosition();                 //Create Return Value Object

        //        playCard.cardValue = identifyPlayCard(aGrid);               //Get the Card to Search for

        //        //Search the Grid for the Desired Card
        //        foreach (var gridCard in aGrid.Items)
        //        {
        //            if (gridCard.Equals(playCard))
        //            {
        //                playCard.positionColumn = computeColumn(aGrid.SelectedIndex);                       //Set the Column Value
        //                playCard.positionRow = computeRow(aGrid.SelectedIndex);                             //Set the Row Value

        //                break;
        //            }
        //        }

        //        return playCard;
        //    }

        //    /*******************************************************************************************
        //     * Method: getCard
        //     * Return the Value for the Card (Rank and Suit) in the Play Position.
        //     */
        //    public String getCard()
        //    {
        //        return this.cardValue;
        //    }

        //    /*******************************************************************************************
        //     * Method: getColumn
        //     * Return the Value for the Column in the Play Position.
        //     */
        //    public int getColumn()
        //    {
        //        return this.positionColumn;
        //    }

        //    /*******************************************************************************************
        //     * Method: getRank
        //     * Parses the Rank from the CardValue passed.
        //     */
        //    public String getRank(String aCardValue)
        //    {
        //        int cardLength = aCardValue.Length;                         //Store Length of Card Value
        //        int rankLength = cardLength - 1;              //Determine Length of String for Card Rank

        //        return (aCardValue.Substring(0, rankLength));                        //Return Card's Rank
        //    }

        //    /*******************************************************************************************
        //     * Method: getSuit
        //     * Parses the Rank from the Card Value passed.
        //     */
        //    public String getSuit(String aCardValue)
        //    {
        //        int suitPosition = aCardValue.Length - 1;         //Determine Position in String of Suit

        //        return (aCardValue.Substring(suitPosition, 1));                 //Return the Card's Suit
        //    }

        //    /*******************************************************************************************
        //     * Method: getRow
        //     * Return the Value for the Row in the Play Position.
        //     */
        //    public int getRow()
        //    {
        //        return this.positionRow;
        //    }

        //    /*******************************************************************************************
        //     * Function: identifyPlayCard
        //     * Based on the array index of the selected play position, looks at the card
        //     * to the left, then determines the card to be played.
        //     */
        //    private String identifyPlayCard(GridView aGrid)
        //    {
        //        //Get the Rank and Suit of Card in Cell to Left
        //        Cards.Card searchItem = (Cards.Card)aGrid.SelectedItem; //aGrid[this.getColumn() - 1, this.getRow()].Tag.ToString();
        //                                                                //String searchValue = searchItem.

        //        String cardSuit = searchItem.getSuit().ToString(); ;//getSuit(searchValue);                            //Get the Card's Suit
        //        String cardRank = searchItem.getRank().ToString();//getRank(searchValue);                            //Get the Card's Rank

        //        int anIndex = 2;     //Set Search Start Postion in Possible Rank (Ace and Deuce ignored)
        //        while (!(Cards.Card.possibleRanks[anIndex].Equals(cardRank)))
        //        {
        //            anIndex++;
        //        }
        //        cardRank = Cards.Card.possibleRanks[anIndex - 1];            //Get the Located Card Rank

        //        return (cardRank + cardSuit);                                    //Return the Card Value
        //    }
        //}

        #endregion

        /***********************************************************************************************
         * Class: Undo Buffer
         * Defines an object that stores the from and to card locations for a move.
         **********************************************************************************************/
        #region
        public partial class UndoBuffer
        {
            List<UndoItem> myUndoItems;

            /*******************************************************************************************
            * Sub-Class UndoItem
            * Defines the structure of a single Undo Item.
            */
            public partial class UndoItem
            {
                private int fromPosition;                    //Card Position that move was from
                private int toPosition;                        //Card Position that move was to
    
                public UndoItem(int aFromPosition, int aToPosition)
                {
                    fromPosition = aFromPosition;
                    toPosition = aToPosition;
                }
    
                public int getFromPosition()
                {
                    return (fromPosition);
                }
    
                public int getToPosition()
                {
                    return (toPosition);
                }
            }

        /*******************************************************************************************
         * Constructor: UndoBuffer (Default)
         * Initializes the Undo Buffer.
         */
        public UndoBuffer()
        {
            //No Initialization State required
        }

        /*******************************************************************************************
         * Method: pop
         * "Pops" the last move made from the Stack
         */
        public UndoItem pop()
        {
            int bufferItem = myUndoItems.Count - 1;                             //Top Item in Buffer
            UndoItem myItem = null;                                                //Local Undo Item

            if (bufferItem >= 0)
            {
                myItem = myUndoItems[bufferItem];                      //Get the most recent move...
                myUndoItems.RemoveAt(bufferItem);              //and Remove "popped" item from stack
            }
            return (myItem);
        }

            /*******************************************************************************************
             * Method: push
             * "Pushes" the most recent move made to the Stack
             */
            public void push(int aFromPosition, int aToPosition)
            {
                UndoItem newItem = new UndoItem(aFromPosition, aToPosition);
                myUndoItems.Add(newItem);
            }
        }
        #endregion
    }
}
