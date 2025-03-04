/***************************************************************************************************
* Page Class: GameTableau
* 
* Main Page for playing game.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Navigation;

using System;
//using System.Diagnostics;
//using System.ComponentModel;
//using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
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
//using Windows.UI.Composition;
//using Windows.UI.Popups;
//using Windows.UI.ViewManagement;             //For ApplicationView Object; adjusting App Window size
//using Windows.UI.Xaml;

using WinRT.Interop;
//using static System.Net.Mime.MediaTypeNames;

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
        private Player myAvatar = new Player();                  //Storage for Current Player Object
        private GameInformation myGameInfo = new GameInformation();  //Local Game Information Object

        //Declare and Initialize Game Playing Deck(s)
        public Cards gameDeck = new Cards(true);                          //Initialize Deck of Cards

        //"Public" Xaml access for Playable and Non-Playable Cards
        public Cards.Card cardPlayable;
        public Cards.Card cardNotPlayable;

        //private static String folderPlayableIcons = "ms-appx://Assets//GameImages//";
        private static String folderGameData = "ms-appx:///Assets//Data//";
        private static String folderGameImages = "ms-appx:///Assets//GameImages//";

        private String helpText = null;

        //Media Player object to play various sounds during play; sound files
        private MediaPlayer myMediaPlayer = new MediaPlayer();

        private Uri soundNotPlayable = new Uri("ms-appx:///Assets//Sounds/NotPlayablePosition.wav");

        private int delayClearDeck = 125;                //Task Delay when Clearing the Playing area
        private int delayDealCards = 125;                            //Task Delay when Dealing Cards
        private int delayRemoveAces = 125;                           //Task Delay when removing aces

        //private static int displayDelayMS = 5000;      //Action display delay so user can see changes

        private string fileInstructions = folderGameData + "GameInstructions.txt";
        private string imgSmilingFrog = folderGameImages + "SmilingFrogFace.ico";

        // Define variables/constants for play area (main window)
        private static int numberOfSuits = Cards.Card.possibleSuits.Length;         //Play Area Rows
        private static int numberOfRanks = Cards.Card.possibleRanks.Length;      //Play Area Columns

        private bool flgGameOver = true;                              //Flag identifies game is over
        private bool isGameSet = false;                          //Flag indicates play area is ready
        private bool isKingMoving = false;                   //Flag indicating a King is being moved

        private int indexKingDestination = -1;         //Storage for the index of a Kings desination
        private List<int> playableKingPositions = new List<int>();

        private SolidColorBrush normalBorderBrush = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush highlightBorderBrush = new SolidColorBrush(Colors.Green);

        Thickness newBorderWidth = new Thickness();

        private int normalBorderWidth = 1;
        private int highlightBorderWidth = 5;

        private int moveCount = 0;                        //Counter for Number of Moves Made in game

        private UndoBuffer myUndoBuffer = new UndoBuffer();                 //Create the Undo Buffer

        //Below Parameters used to reflect Game time and store Accumulated play time
        private DispatcherTimer myGameTimer;
        private Stopwatch myGameStopwatch;

        private TimeSpan totalTimePlayed = TimeSpan.Zero;

        //Current Activity Messages
        private static string errKingNotSelected = "King not Selected; Cancelling Move...";

        private static string msgClearPlayArea = "Clearing the Playing Area...";
        private static string msgDealing = "Dealing Cards...";
        private static string msgGameOver = "Game Over!";
        private static string msgInitialLayout = "Initiating Layout Parameters...";
        private static string msgLoadingHelp = "Loading Help Text...";
        private static string msgMarkingKings = "Marking Kings for Moving...";
        private static string msgPrepareInitial = "Preparing Initial Game Board...";
        private static string msgRemoveAces = "Removing Aces...";
        private static string msgSelectKing = "Select King to Move...";
        private static string msgSelectPlay = "Click on Play Space to Move Card...";
        private static string msgSettingNewGame = "Setting Up for a New Game...";
        private static string msgShuffling = "Shuffling and Cutting Cards...";
        private static string msgWaiting = "Waiting for User Input...";

        private static string dialogTitle = "LeapFrog";

        /*******************************************************************************************
        * GameTableau Constructor
        * Initializes and constructs the Game Tableau and initial game board.
        */
        public GameTableau()
        {
            this.InitializeComponent();

            myWindow = getMyAppWindow();
            myWindow.SetIcon(imgSmilingFrog);
            myWindow.TitleBar.ExtendsContentIntoTitleBar = false;

            ResizeAppWindow(myWindow);              //Resize the AppWindow to Match GameTableau size
            CenterAppWindow(myWindow);                         //Center the AppWindow on the Display

            InitializeTimer();

            //Clear the Game Deck to initialize the Game Board, and prepare for new game
            cardPlayable = new Cards.Card("p", "l", gameDeck.getCardFacePlayable());
            cardNotPlayable = new Cards.Card("n", "p", gameDeck.getCardFaceNotPlayable());

            initialLoad();
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         EVENT HANDLERS                              ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
         * Event Handler: btnExit_Click
         * Handles the Closing of the Game Application.
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
            var gameInstructions = new DisplayInstructions(helpText);
            gameInstructions.XamlRoot = this.XamlRoot;

            await gameInstructions.ShowAsync();
        }

        /*******************************************************************************************
         * Event Handler: New Game
         * Calls procedure to setup new game.
         */
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            setUpNewGame();                                    //Shuffle and Deal Cards for new game
        }

        /*******************************************************************************************
         * Event Handler: Player Statistics
         * Calls procedure to display player statistics.
         */
        private async void btnStats_Click(object sender, RoutedEventArgs e)
        {
            await displayPlayerStats(myAvatar);
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

                    //if (!isKingMoving)
                    //{
                        playSpaceClicked(indexClickedCell);
                    //}
                    //else
                    //{
                    //    moveKing(indexClickedCell);
                    //}
                }
            }
        }

        /*******************************************************************************************
         * Event Handler: dataGridGameBoardKing_SelectionChanged
         * Initiates moving a King when a destination is selected.
         */
        private void dataGridGameBoardKing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If the Game is set for play and the Selected Item is not null...
            if (isGameSet && isKingMoving)
            {
                var myGridView = sender as GridView;
                if(myGridView != null) //if(dataGridGameBoard.SelectedItem != null)
                {
                    var gridViewItem = myGridView.SelectedIndex; // dataGridGameBoard.ContainerFromItem(dataGridGameBoard.SelectedItem) as GridViewItem;
                    //if (gridViewItem != null)
                    //{
                        int indexClickedCell = dataGridGameBoard.SelectedIndex;

                        if (isPlayableKing(indexClickedCell))
                        {
                            moveKing(indexClickedCell);
                        }
                        else
                        {
                            playSound(soundNotPlayable);
                        }
                    //}
                }
            }
        }
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
            var myWindow = (Microsoft.UI.Xaml.Application.Current as App)?.m_window as MainWindow;
            var hwnd = WindowNative.GetWindowHandle(myWindow);
            var myWindowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(myWindowId);

            return appWindow;
        }

        /*******************************************************************************************
         * Event Handler: GameTimer
         * Displays the Time Played
         */
        private void MyGameTimer_Tick(object sender, object e)
        {
            myTimerDisplay.Text = myGameStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
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

            await updateCurrentActivityText(msgInitialLayout);

            //await removeAces();                              //Remove Aces to Initialize Play Spaces

            //Configure the Game Playing Grid
            gameTableau.Background = myGameInfo.getBackgroundColor();       //Set Tableau Background

            dataGridGameBoard.Background = myGameInfo.getBackgroundColor();   //Set Background Color
            dataGridGameBoard.AllowFocusOnInteraction = true;
            dataGridGameBoard.IsEnabled = true;
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
            }
        }

        /*******************************************************************************************
         * Method: deactivateKingMove
         * Disables the flags and whatnot that are used to activate and perform a move of a King to
         * a King Position (Leftmost column).
         */
        private async void deactivateKingMove()
        {
            highlightKingForMoving(playableKingPositions, false);

            isKingMoving = false;
            indexKingDestination = -1;
            playableKingPositions.Clear();

            enableSelectionKingChanged(false);             //Turn off the SelectionKingChanged Event
            enableSelectionChanged(true);                  //Turn on the Play SelectionChanged Event

            await updateCurrentActivityText(msgSelectPlay);
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
         * Method: displayMessage
         * Displays the informational Message passed as parameter.
         */
        private async Task displayMessage(String theMessage)
        {
            ContentDialog myMessage = new ContentDialog();
            myMessage.XamlRoot = this.XamlRoot;

            myMessage.Title = dialogTitle;
            myMessage.Content = theMessage;
            myMessage.PrimaryButtonText = "OK";

            var messageResponse = await myMessage.ShowAsync();
        }

        /*******************************************************************************************
         * Method: displayMessage
         * Displays the informational Message passed as parameter.
         */
        private async Task displayPlayerStats(Player myAvatar)
        {
            var playerStats = new DisplayPlayerStats(myAvatar);
            playerStats.XamlRoot = this.XamlRoot;

            await playerStats.ShowAsync();
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
        * Method: enableSelectionKingChanged
        * Programatically enables and disables the SelectionKingChanged Event.
        */
        private void enableSelectionKingChanged(bool enableEvent = true)
        {
            if (enableEvent)
            {
                dataGridGameBoard.SelectionChanged += dataGridGameBoardKing_SelectionChanged;
            }
            else
            {
                dataGridGameBoard.SelectionChanged -= dataGridGameBoardKing_SelectionChanged;
            }
        }

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
        private async void endGame()
        {
            StopTimer();                                                      //Stop the Game Timer

            await updateCurrentActivityText(msgGameOver);

            int currentScore = scoreGame();                        //Compute Score for Current Game
            //Update Player Statistics then Display Results
            myAvatar.finishGameForPlayer(currentScore, moveCount, totalTimePlayed);
            flgGameOver = true;                                               //Set "Game Over" flag
            isGameSet = false;                                               //Set the game set flag

            await speakText(msgGameOver);
            await displayMessage(msgGameOver);                          //Display "Game Over" Dialog

            await displayPlayerStats(myAvatar);
        }

        /*******************************************************************************************
         * Method: findPlayableKings
         * Locates and stores the gridview indexes of the Kings that are playable; that is, have not
         * already been moved to the "King Position."
         */
        private async Task findPlayableKings()
        {
            int countKings = 0;                                  //Initialize Counter of Kings found
            int cardIndex = 0;                               //Counter to walk through Deck of Cards
            int maxKingCount = Cards.Card.possibleSuits.Length;                 //Max Count of Kings

            while ((countKings < maxKingCount) && (cardIndex < gameDeck.deckCards.Count))               //While Not all Kings have been found...
            {
                if (isKing(gameDeck.deckCards[cardIndex]))                    //If Card is a King...
                {
                    countKings++;                                    //Increment the King Counter...
                    if (!isKingPosition(cardIndex))      //Check if King is not in a King Position...
                    {
                        playableKingPositions.Add(cardIndex);
                    }
                }

                cardIndex++;                                 //Increment the Card Index to next card
            }
        }

        /*******************************************************************************************
         * Method: highlightKingForMoving
         * Changes Border Color and Thickness on King that can be moved.
         */
        private async Task highlightKingForMoving(List<int> KingPositions, bool highlightKing = false)
        {
            enableSelectionChanged(false);        //Disable Selection Event while highlighting Kings

            SolidColorBrush newBorderBrush = normalBorderBrush;
            newBorderWidth = new Thickness(normalBorderWidth);

            if (highlightKing)
            {
                newBorderBrush = highlightBorderBrush;
                newBorderWidth = new Thickness(highlightBorderWidth);
            }

            foreach (int kingPosition in KingPositions)
            {
                dataGridGameBoard.SelectedIndex = kingPosition;
                var myItem = dataGridGameBoard.SelectedItem;
                var anItem = dataGridGameBoard.ContainerFromItem(myItem) as GridViewItem;

                anItem.BorderBrush = newBorderBrush;
                anItem.BorderThickness = newBorderWidth;
            }

            //var myStackPanel = anItem.ContentTemplateRoot as StackPanel;
            //myStoryBoard = myStackPanel.Resources["ZoomInMoveableKing"] as Storyboard;

            //myStoryBoard.Begin();
            enableSelectionChanged(true);        //Reenable Selection Event after highlighting Kings
        }

        /*******************************************************************************************
         * Function: initialLoad
         * Setups up the playing area when the game first loads.
         */
        private async Task initialLoad()
        {
            //Ensure parameters and various knick-knacks are initialized for new game
            ResetTimer();                                                 //Ensure Timer has stopped
            await updateMoveCountText(0);                            //Clear the move count text box
            playableKingPositions.Clear();               //Ensure the Playable Kings List is cleared

            //Get Text for Game Instructions
            await updateCurrentActivityText(msgLoadingHelp);
            await loadHelpText();

            //Build the Initial Game Board and set Data Context
            await updateCurrentActivityText(msgPrepareInitial);

            await buildInitialGameBoard();

            await updateCurrentActivityText(msgWaiting);
        }

        /*******************************************************************************************
         * InitializeTimer()
         * Sets Up the Game Timer
         */
        private void InitializeTimer()
        {
            myGameStopwatch = new Stopwatch();

            myGameTimer = new DispatcherTimer();
            myGameTimer.Interval = TimeSpan.FromSeconds(1);
            myGameTimer.Tick += MyGameTimer_Tick;
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
         * Function: isPlayableKing
         * Compares the Index of the "selected King" to the indexes in the list of playable Kings.
         * Returns "true" if a match is found; otherwise returns "false"
         */
        private bool isPlayableKing(int kingPosition)
        {
            bool isPlayable = false;
            foreach (int playableKing in playableKingPositions)
            {
                if (playableKing == kingPosition)
                {
                    isPlayable = true;
                    break;
                }
            }

            return isPlayable;
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
            }
        }

        /*******************************************************************************************
         * Method: moveKing
         * Moves the King from the Source Position to the Destination Position, then checks if the
         * move ended the game.
         */
        private async Task moveKing(int sourceIndex)
        {
            if (sourceIndex != indexKingDestination)    //If the Source and Destination not Equal...
            {
                enableSelectionKingChanged(false);

                //if (!isKing(gameDeck.deckCards[sourceIndex]))        //If a King was not selected...
                //{
                //    await playSound(soundNotPlayable);
                //    await updateCurrentActivityText(errKingNotSelected);
                //}
                //else
                //{
                    swapPlayCards(sourceIndex, indexKingDestination);       //Move the Selected King

                    deactivateKingMove();  //reset King Moving Parameters; i.e. turn off King Moving
                //}
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
                if (isPlayable(destinationIndex))                        //Playable or King Space...
                {
                    if (!isKingPosition(destinationIndex))
                    {
                        Cards.Card sourceCard = gameDeck.deckCards[destinationIndex-1];
                        Cards.Card cardToMove = gameDeck.findNextCardDescending(sourceCard);
               
                        int sourceIndex = gameDeck.findCardIndex(cardToMove);
               
                        moveCard(sourceIndex, destinationIndex);
                    }
                    else
                    {
                        isKingMoving = true;                             //Set King being Moved Flag
                        indexKingDestination = destinationIndex;

                        setupKingMove();                                  //Get the King to be Moved

                        dataGridGameBoard.SelectedIndex = -1; //Deselect Grid Item...
                    }

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
         * ResetTimer()
         * Resets Game Timer to "Zero."
         */
        private void ResetTimer()
        {
            myGameStopwatch.Reset();
            myTimerDisplay.Text = myGameStopwatch.Elapsed.ToString(@"hh\:mm\:ss");// "00:00:00";
        }

        /*******************************************************************************************
         * Method: setupKingMove
         * Locates and "Highlighs" the Kings in the Playing tableau; then waits for one to be
         * selected.
         */
        private async void setupKingMove()
        {
            string currentActivityMessage = msgSelectKing;
            enableSelectionChanged(false);                   //Disable the Selection Change Event...

            await findPlayableKings();    //Find positions of kings not currently in "King Position"

            await updateCurrentActivityText(msgMarkingKings);
            if(playableKingPositions.Count > 1)               //If more than one King is playable...
            {
                await highlightKingForMoving(playableKingPositions, true);
            }
            else                           //otherwise, just move the last King not in King Position
            {
                await moveKing(playableKingPositions[0]);
                currentActivityMessage = msgSelectPlay;
            }

            enableSelectionKingChanged(true);            //Enable the King Selection Change Event...

            //Wait for a selection to be made
            await updateCurrentActivityText(currentActivityMessage);
        }
        
        /*******************************************************************************************
         * Method: setUpNewGame
         * Prepares the playing board, shuffles the deck of cards and initializes the tableau for
         * playing the game
         */
        private async Task setUpNewGame()
        {
            //If a game is currently running, score the game before setting up new game
            if(isGameSet)                                     //If a Game is currently in process...
            {
               endGame();                          //Score the Current Layout and Reset for New Game
            }

            ResetTimer();                                                 //Ensure Timer has stopped
            updateMoveCountText(0);                                  //Clear the move count text box
            playableKingPositions.Clear();               //Ensure the Playable Kings List is cleared

            await updateCurrentActivityText(msgSettingNewGame);
            
            Cards tempDeck = new Cards();              //Create a working deck to shuffle, cut, etc.
            flgGameOver = false;                                       //Set Game Over Flag to false

            await updateCurrentActivityText(msgClearPlayArea);
            await clearDeck();                                           //Clear the Current Layout

            //Shuffle the Deck of Cards Until Shuffled Deck has at least one playable position
            await updateCurrentActivityText(msgShuffling);
            do
            {
                await tempDeck.shuffleDeck();                            //Shuffle the Deck of Cards
                await tempDeck.cutDeck();                                             //Cut the Deck
            }
            while(!isPlayableShuffle(tempDeck));   //Shuffled Deck have at least one playable space?

            await updateCurrentActivityText(msgDealing);
            await dealCards(tempDeck);                               //Deal the Cards to the Tableau

            await updateCurrentActivityText(msgRemoveAces);
            await removeAces();                              //Remove Aces to Initialize Play Spaces

            isGameSet = true;                                     //Set the game is set flag to true

            enableSelectionChanged(true);               //Enable the GridView SelectionChanged Event

            //myUndoItems.Clear();                                             //Clear the Undo Buffer

            moveCount = 0;                              //Initialize the Move Counter for a New Game
            updateMoveCountText(moveCount);                             //Update Move count Text box

            StartTimer();                                                     //Start the Game Timer

            updateCurrentActivityText(msgSelectPlay);
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
         * StartTimer()
         * Starts the Game Timer
         */
        private void StartTimer()
        {
            ResetTimer();
            myGameStopwatch.Start();
            myGameTimer.Start();
        }

        /*******************************************************************************************
         * StopTimer()
         * Stops the Game Timer
         */
        private void StopTimer()
        {
            myGameTimer.Stop();
            myGameStopwatch.Stop();

            totalTimePlayed = myGameStopwatch.Elapsed;
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

            moveCount++;                                //Initialize the Move Counter for a New Game
            updateMoveCountText(moveCount);                             //Update Move count Text box

            //UndoItem thisMove = new UndoItem(sourceCard, destinationCard);
            //myUndoItems.Push(thisMove);                                 //Push Move onto Undo Buffer

            isGameOver();                            //Check if there are no more playable positions
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

            await Task.Delay(delayTask);
        }

        /*******************************************************************************************
        /* Method: updateCurrentActivityText
        /* 
        /* Updates the Text in the "Current Activity" TextBlock.
        /*/
        private async Task updateMoveCountText(int currentMoveCount, int delayTask = 1000)
        {
            string msgMoveCount = "";

            if (currentMoveCount > -1)
            {
                msgMoveCount = "Moves: " + currentMoveCount.ToString();
            }

            tbMoveCount.Text = msgMoveCount;

            await Task.Run(() => Thread.Sleep(delayTask));
        }
        #endregion

        /*******************************************************************************************
         * Score Game
         * Stores the procedures used to score the game.
         ******************************************************************************************/
        #region
        // Define parameters for Scoring Games (Determining Player's Winnings)
        private int pointsForSequence = 1;             //Points to add for cards in correct sequence
        private int pointsForPosition = 2;             //Points to add for cards in correct position
        private int pointsForCompleteSuit = 10;                  //Points to add for a complete suit

        private int gameWinningBonus = 100;      //Bonus Amount for a All Cards Correctly Positioned

        /*******************************************************************************************
         * Function: isCorrectPosition
         * Compares card position in row and determines if this is correctly placed. Returns "true"
         * if card is in correct position; otherwise returns false.
         */
        private bool isCorrectPosition(int cardPosition)
        {
            Cards.Card thisCard = gameDeck.deckCards[cardPosition];          //Get Card being tested
            bool placedCorrectly = false;                                 //Set default return value

            if (!(thisCard.Equals(cardNotPlayable)))
            {
                String thisRank = thisCard.getRank();                        //Get Current Card Rank
                int correctPosition = thisCard.findRank(thisRank);  //Get Position in Possible Ranks
                correctPosition = Math.Abs(correctPosition - 12);         //Adjust for Reverse Order

                int thisPosition = (cardPosition % 13);           //Get Current Card Column Position

                placedCorrectly = (thisPosition == correctPosition);       //Compute Correct Placing
            }

            return placedCorrectly;
        }

        /*******************************************************************************************
         * Method: scoreGame
         * Adds up the score of the current game; scoring as follows:
         * 
         * 2 points for each card in correct sequence (by rank and suit)
         * 5 points for each card in correct position (column by rank)
         * 10 points for completion of a suit (King through 2 of same suit on same row)
         */
        private int scoreGame()
        {
            int thisGameScore = 0;              //Local variable to accumulate score of current game
            int completedSuits = 0;                          //Count of the Suits that are completed
            int countSequence = 0;                   //Count the number of cards in correct sequence

            // Sum score for cards that are in correct sequence and correct position
            for (int aSuit = 0; aSuit < Cards.Card.possibleSuits.Length; aSuit++)
            {
                countSequence = 0;                     //Ensure Sequence Count is reset for each row
                int currentRank = 0;       //Set initial column or Rank position for the current row
                bool correctPosition = false;          //Initialize "Correct Position" flag to "Not"

                while(currentRank < 12)
                {
                    //Compute the Play Position of the Current Card being checked
                    int playPosition = gameDeck.calcArrayPosition(aSuit, currentRank);

                    Cards.Card thisCard = gameDeck.deckCards[playPosition];              //This Card
                    if (!thisCard.cardsMatch(cardNotPlayable))
                    {
                        Cards.Card nextCard = gameDeck.deckCards[playPosition + 1];          //Next Card

                        Cards.Card nextCardInSequence = gameDeck.findNextCardDescending(thisCard);

                        if (nextCard.cardsMatch(nextCardInSequence))        //If next card is correct...
                        {
                            countSequence++;                            //Increment the Sequence Counter
                            if (isCorrectPosition(playPosition)) //If Current Card is in correct position...
                            {
                                correctPosition = true;       //Set the "Correct Position" flag to "Yes"
                            }
                        }
                        else               //Card is in Current position, begin checking for sequence...
                        {
                            countSequence++;                      //Adjust Sequence Count for first card

                            if (countSequence == 12)                         //If the suit is complete...
                            {
                                completedSuits++;                //Increment the Completed Suits counter
                                thisGameScore += pointsForCompleteSuit; //Add Completed Suits points to score
                            }

                            if (countSequence > 2)       //If at least 3 cards are in correct sequence...
                            {
                                thisGameScore += (countSequence * pointsForSequence); //Add points to score

                                if (correctPosition)                //If cards are in correct position...
                                {
                                    thisGameScore += (countSequence * pointsForPosition); //Add points to score
                                }
                            }

                            countSequence = 0;                                  //Reset sequence Counter
                            correctPosition = false;                     //And the Correct Position Flag
                        }
                    }

                    currentRank++;                                                //Go the next card
                }
            }

            if(completedSuits == 4)                                  //If all Suits are completed...
            {
                thisGameScore += gameWinningBonus;                          //Add Winning Game Bonus
            }

            return thisGameScore;
        }

        #endregion

        /*******************************************************************************************
         * Class: Play Position
         * Stores the Play Position as an object to simplify parameter passing during game play.
         ******************************************************************************************/
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
