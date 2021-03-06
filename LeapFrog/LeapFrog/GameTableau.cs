﻿/***************************************************************************************************
 * Class: GameTableau
 * 
 * Defines the Playing Area for the Card Game, and respon. and responds to player's moves.
 * 
 * Author:              Charles J Pilgrim
 * Created:             24-September-2013
 * 
 * @Copyright (c) 2013-2017 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/***************************************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    /***********************************************************************************************
     * Class: Game Tableau
     **********************************************************************************************/
    public partial class GameTableau : Form
    {
        /*******************************************************************************************
         * Class Variables and Constants
         */

        // Define variables/constants for play area (main window)
        private static int numberPlayRows = Cards.Card.possibleSuits.Length;        //Play Area Rows
        private static int numberPlayColumns = Cards.Card.possibleRanks.Length;  //Play Area Columns

        //Define Parameters for the various Icons used in game

        private Image playSpaceIcon = LeapFrog.Properties.Resources.Playable;  //Playable Space icon
        private Image noPlayIcon = LeapFrog.Properties.Resources.NotPlayable;    //Non-Playable icon

        private static String playSpace = "";             //String or character to use on play spots

        private bool playKingPosition = false;          //Flag Indicating the Position is for a King

        private static bool isGameSet = false;                  // Flag indicates play area is ready
        public bool flgGameOver = true;                               //Flag identifies game is over

        private Player myPlayer;                                 //Storage for Current Player Object
        private GameInformation myGameInfo;              //Storage for Local Game Information Object

        // Define parameters for Scoring Games (Determining Player's Winnings)
        private int incrementSequence = 2;             //Points to add for cards in correct sequence
        private int incrementPosition = 5;             //Points to add for cards in correct position
        private int incrementCompleteSuit = 10;                  //Points to add for a complete suit

        private int gameBuyIn = 100;                  //Deduction to player's score to buy into game
        private int moveCount = 0;                        //Counter for Number of Moves Made in game

        //Declare and Initialize Game Playing Deck
        private Cards.Deck gameDeck = new Cards.Deck();

        private PlayPosition tempStorage;      //Storage for PlayPosition Object-Needed to Move King

        private UndoBuffer myUndoBuffer = new UndoBuffer();                 //Create the Undo Buffer
        private static int displayDelayMS = 150;      //Action display delay so user can see changes

        //Below Parameters used to reflect Game time and store Accumulated play time
        private DateTime gameStartTime;                                            //Game Start Time
        private DateTime gameEndTime;                                      //Game "Now" and end time

        /*******************************************************************************************
         * Constructor: GameTableau (Default)
         * 
         * Sets up the Game Tableau and Prepares for game play.
          */
        public GameTableau(Player playerID, GameInformation myGameInformation)
        {
            InitializeComponent();

            myPlayer = playerID;                           //Set Player Object to Class Global Value
            myGameInfo = myGameInformation;             //Set Game Information to Class Global Value

            //Customize the Game Tableau Form/Window
            this.Icon = myGameInfo.getWindowIcon();               //Set the Icon for the Main window

            this.Text = this.Text + ":" + playerID.getPlayerName();   //Add Player Name to Title Bar

            this.BackColor = myGameInfo.getBackgroundColor();         //Set Tableau Background Color
            this.ForeColor = myGameInfo.getForegroundColor();         //Set Tableau Foreground Color

            //Build the Game Timer and Timer Display
            gameTime.Tick += new EventHandler(gameTimerTick);        //Create Handler for Game Timer
            gameTime.Interval = (1000) * 1;                    //Set Timer Tick interval to 1 second
            lblGameTimer.Text = String.Empty;                             //Clear the Timer Text box

            //Build Game Play Form or Window and Prepare to Play
            buildInitialGameBoard();                            //Build and Configure the Game Board
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
        private void dataGridGameBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            playSpaceClicked(dataGridGameBoard.CurrentCell.ColumnIndex, dataGridGameBoard.CurrentCell.RowIndex);
        }

        /*******************************************************************************************
         * Event Handler: GameTableau_FormClosed
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void GameTableau_FormClosed(object sender, FormClosedEventArgs e)
        {
            exitGame();
        }

        /*******************************************************************************************
        * Event Handler: gameTimerTick
        * Displays the elapsed time for the current game.
        */

        private void gameTimerTick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = computeTimePlayed();                  //Compute Current Game Time

            String timeDisplay;                        //String to Store the Elapsed Time to Display
            timeDisplay = elapsedTime.ToString();                     //Convert Elapsed Time to Text

            //Display elapsed time after removing milliseconds
            lblGameTimer.Text = timeDisplay.Substring(0, (timeDisplay.IndexOf('.')));
        }
        #endregion

        /*******************************************************************************************
         *******************************************************************************************
         ***********                          MENU OPTIONS                               ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
         * Menu: Help/About
         * Displays the Help/About dialog
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGameAbout HelpAbout = new frmGameAbout(myGameInfo);
            HelpAbout.Show();
        }

        /*******************************************************************************************
         * Menu: Game/Exit
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitGame();
        }

        /*******************************************************************************************
         * Menu: Game/New Game
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void gameHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGameHelp HelpInstructions = new frmGameHelp(myGameInfo);
            HelpInstructions.Show();
        }

        /*******************************************************************************************
         * Menu: Game/New Game
         * Handles the Closing of the Game Tableau Windows Form.
         */
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setUpNewGame(gameDeck);
        }

        /*******************************************************************************************
         * Menu: Game/Player Statistics
         * Displays the Current Player Statistics Message Box.
         */
        private void playerStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPlayer.displayPlayerStats();
        }

        /*******************************************************************************************
         * Menu: Game/Undo
         * Displays the Current Player Statistics Message Box.
         */
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoMove();
        }

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
            //Configure the Game Playing Grid
            dataGridGameBoard.BackgroundColor = myGameInfo.getBackgroundColor();//Set Background Color
            dataGridGameBoard.ForeColor = myGameInfo.getForegroundColor();    //Set Foreground Color

            dataGridGameBoard.GridColor = myGameInfo.getBackgroundColor();//Set Grid Background Color

            dataGridGameBoard.AllowUserToAddRows = false;            //Disallow New Rows to be Added
            dataGridGameBoard.AllowUserToDeleteRows = false;           //Disallow Rows to be Deleted
            dataGridGameBoard.ReadOnly = true;                               //Set Grid to Read-Only

            //Build the Game Board and Insert Default Image into Grid Cells
            Image defaultImage = gameDeck.getCardBack();                         //Get Default Image

            for (int aCol = 0; aCol < numberPlayColumns; aCol++)         //Add Image Columns to Grid
            {
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.Image = defaultImage;

                dataGridGameBoard.Columns.Add(imageCol);
            }

            for (int aRow = 0; aRow < numberPlayRows; aRow++)                //Add Game Rows to Grid
            {
                dataGridGameBoard.Rows.Add();
            }

            //Set Current Cell to Upper Leftmost to Remove Extra Row that Appears
            dataGridGameBoard.CurrentCell = dataGridGameBoard.Rows[0].Cells[0];
        }

        /*******************************************************************************************
         * Method: clearBoard
         * Clears the Playing board (tableau) for a new game.
         * 
         * aDeck - Deck Object containing cards to be dealt.
         */
        private void clearBoard(Cards.Deck aDeck)
        {
            for (int aRow = 0; aRow < numberPlayRows; aRow++)
            {
                for (int aCol = 0; aCol < numberPlayColumns; aCol++)
                {
                    //Assign the Various Fields in the Cell
                    dataGridGameBoard[aCol, aRow].ToolTipText = String.Empty;
                    dataGridGameBoard[aCol, aRow].Tag = String.Empty;
                    dataGridGameBoard[aCol, aRow].Value = aDeck.getCardBack();
                }
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
        private void dealCards(Cards.Deck aDeck)
        {
            Cards.Card aCard = new Cards.Card();                         //Storage for Current Card

            for (int aRow = 0; aRow < numberPlayRows; aRow++)
            {
                for (int aCol = 0; aCol < numberPlayColumns; aCol++)
                {
                    //Compute Card Element from Deck Array to Deal
                    int arrayElement = (aRow * Cards.Card.possibleRanks.Length) + aCol;

                    aCard = aDeck.getCard(arrayElement);       //Select the card from the card array

                    //Assign the Various Fields in the Cell
                    dataGridGameBoard[aCol, aRow].ToolTipText = aCard.getRank() + aCard.getSuit();
                    dataGridGameBoard[aCol, aRow].Tag = aCard.getRank() + aCard.getSuit();
                    dataGridGameBoard[aCol, aRow].Value = aCard.getCardFace();

                    delay(displayDelayMS);                    //Pause Deal for user to see cards dealt
                }
            }
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
                System.Windows.Forms.Application.DoEvents();
                thisInstant = System.DateTime.Now;
            }
        }

        /*******************************************************************************************
         * Method: displayMessage
         * Displays the informational Message passed as parameter.
         */
        private void displayMessage(String theMessage)
        {
            MessageBox.Show(theMessage, "Leapfrog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*******************************************************************************************
         * Method: displayWarning
         * Displays the Warning message passed as parameter.
         */
        private void displayWarning(String theMessage)
        {
            MessageBox.Show(theMessage, "Leapfrog", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /*******************************************************************************************
         * Method: exitGame
         * Closes Out all processing and Exits or Closes the Application.
         */
        private void exitGame()
        {
            Application.Exit();
        }

        /*******************************************************************************************
         * Method: endGame
         * Process finishes all the tasks that are necessary when a game has ended; i.e.
         * accumulate the score for the game, set the public "Game Over" flag, etc.
         */
        private void endGame()
        {
            gameTime.Stop();                                                   //Stop the Game Timer

            displayMessage("Game Over!");
            scoreGame();               //Compute Score for Current Game and Update Player Statistics

            flgGameOver = true;                                               //Set "Game Over" flag
            isGameSet = false;                                               //Set the game set flag
            playKingPosition = false;                             //Ensure Moving King Flag is Unset

            clearBoard(gameDeck);                                                //Clear the tableau
            lblGameTimer.Text = String.Empty;                             //Clear the Timer Text box
            moveCount = -1;                                                  //Reset the move counter
            txtMoveCount.Text = moveCount.ToString();                //Clear the Move count Text box
        }

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
        private bool isGameOver()
        {
            int countPlayPositions = 0;
            for (int aRow = 0; aRow < dataGridGameBoard.Rows.Count; aRow++)
            {
                for (int aCol = 0; aCol < dataGridGameBoard.Columns.Count; aCol++)
                {
                    if (isPlayable(aCol, aRow))                              //If Cell is Playable...
                    {
                        countPlayPositions++;                  //Increment Playable position counter
                    }
                }
            }

            return (countPlayPositions == 0);           //Return "True" if no positions are playable
        }

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
        private bool isPlayable(int colPosition, int rowPosition)
        {
            bool retVal = true;                                          // Set default return value

            if (dataGridGameBoard[colPosition, rowPosition].Tag.ToString().Equals(playSpace))
            {
                if (colPosition > 0)                                  //If Not the Leftmost Column...
                {
                    //Check if the Cell to Left is "Playable"
                    String cardToLeft = dataGridGameBoard[colPosition - 1, rowPosition].Tag.ToString();
                    if (cardToLeft.Equals(playSpace))                 //If the cell is "PlaySpace"...
                    {
                        retVal = false;                               //Set Return to "Not-Playable"
                    }
                    else if (cardToLeft.Substring(0, 1).Equals("2"))         //Or if it is a Deuce...
                    {
                        retVal = false;                               //Set Return to "Not-Playable"
                    }
                    else                                              //Otherwise, it is playable...
                    {
                        retVal = true;                                    //Set Return to "Playable"
                    }
                }
                else                                            //Else, It is the Leftmost Column...
                {
                    retVal = true;                                        //Set Return to "Playable"
                }

                //Set the Icon for the Cell
                if (retVal)                                            //If the Cell is Playable...
                    dataGridGameBoard[colPosition, rowPosition].Value = playSpaceIcon;
                else
                    dataGridGameBoard[colPosition, rowPosition].Value = noPlayIcon;
            }
            else                                                          //The Cell is Not Playable
            {
                retVal = false;                                       //Set Return to "Not-Playable"
            }

            return retVal;
        }

        /*******************************************************************************************
         * Method: isSuitComplete
         * Walks through a row on the Tableau to determine if the suit for that row
         * is complete. For a suit to be complete, a King should be found in the leftmost
         * column, and the rank of the same suit should decrease down to the deuce in the
         * second column from the end. Strictly speaking, the rightmost column should be
         * a "play space" character, but for the purposes of scoring, this is unimportant.
         */
        private bool isSuitComplete(int currentRow)
        {
            int currentCol = 0;

            while (currentCol < Cards.Card.possibleRanks.Length - 2)        //If not at end of row...
            {
                PlayPosition thisCard = new PlayPosition(dataGridGameBoard, currentCol, currentRow);
                String currentSuit = "";                  //Store the Current Suit from first Column

                if (thisCard.getCard().Equals(playSpace))               //If Card is a Play Space...
                    return false;

                if (currentCol == 0)                                         //If leftmost column...
                    currentSuit = thisCard.getSuit(thisCard.getCard());       //Get the Current Suit

                if (!(thisCard.getSuit(thisCard.getCard()).Equals(currentSuit)))//If not Correct Suit
                    return false;

                if (!isCorrectPosition(thisCard))                //If Card not in Correct Position...
                    return false;

                currentCol++;                                                   //Move to Next Column
            }

            return true;
        }

        /*******************************************************************************************
         * Method: moveCard
         * Moves the Card from the Source Position to the Destination Position, then checks if the
         * move ended the game.
         */
        private void moveCard(PlayPosition sourcePosition, PlayPosition destinationPosition)
        {
            if (sourcePosition != destinationPosition)   //If the Source and Destination not Equal...
                swapPlayCards(sourcePosition, destinationPosition);         //Move the Selected Card

            playKingPosition = false;                                         //Ensure Flag is Unset

            if (isGameOver())                           //Check if game still has playable positions
            {
                endGame();                   //Close out the current game, and set appropriate flags
            }
        }

        /*******************************************************************************************
         * playSpaceClicked
         * Actions to perform when mouse is Clicked. Process determines the Grid
         * Component that was clicked then initiates a card move.
         */
        public void playSpaceClicked(int playCol, int playRow)
        {
            if (isGameSet)                                         //Playing Area is set for play...
            {
                if (isPlayable(playCol, playRow) || playKingPosition)    //Playable or King Space...
                {
                    PlayPosition destinationPosition = new PlayPosition(dataGridGameBoard, playCol, playRow);
                    PlayPosition sourcePosition = destinationPosition;         //Set a "Dummy" Value

                    if (isKingPosition(playCol))               //If Clicked Cell is Leftmost Cell...
                    {
                        tempStorage = destinationPosition;           //Store the Current Destination

                        displayMessage("Select King to Move to this Position...");  //Prompt User...
                        playKingPosition = true;                         //Set King being Moved Flag
                    }
                    else if (!playKingPosition)                            //If not Moving a King...
                    {
                        sourcePosition = destinationPosition.findPlayCard(dataGridGameBoard);
                    }
                    else if (playKingPosition)                           //If King is being Moved...
                    {
                        if (!isKing(destinationPosition.getCard()))  //If a King was not selected...
                        {
                            displayWarning("King was not selected; cancelling move!");
                            sourcePosition = destinationPosition;
                        }
                        else
                        {
                            sourcePosition = destinationPosition;//Set Source to Current Destination
                            destinationPosition = tempStorage;        //Restore Original Destination
                        }

                        playKingPosition = false;                  //Unset the King being Moved Flag
                    }

                    //Destination and Source should be set, move the selected card
                    if (!playKingPosition)
                    {
                        moveCard(sourcePosition, destinationPosition);     //Move the Designated Card

                        moveCount++;                                         //Increment Move Counter
                        txtMoveCount.Text = moveCount.ToString();            //Display Count of Moves
                    }
                }
            }
        }

        /*******************************************************************************************
         * Method: removeAces
         * Removes the Aces from the playing area in order to initialize the play spots.
         */
        public void removeAces()
        {
            for (int aRow = 0; aRow < numberPlayRows; aRow++)
            {
                for (int aCol = 0; aCol < numberPlayColumns; aCol++)
                {
                    if (dataGridGameBoard[aCol, aRow].Tag.ToString().Substring(0, 1).Equals(Cards.Card.possibleRanks[0]))
                    {
                        dataGridGameBoard[aCol, aRow].Value = playSpaceIcon;
                        dataGridGameBoard[aCol, aRow].Tag = playSpace;

                        delay(displayDelayMS);                    //Pause Deal for user to see cards dealt
                    }
                }
            }

            isGameSet = true;                                     //Set the game is set flag to true
            isGameOver();                                      // Initialize the icons for game play
            flgGameOver = false;                                 //Set the "Game Over" flag to false
        }

        /*******************************************************************************************
         * Method: scoreGame
         * Adds up the score of the current game; scoring as follows:
         * 
         * 2 points for each card in correct sequence (by rank and suit)
         * 5 points for each card in correct position (column by rank)
         * 10 points for completion of a suit (King through 2 of same suit on same row)
         */
        private void scoreGame()
        {
            int scoreThisGame = 0;                                          //Score for Current Game

            // Sum score for cards that are in correct sequence and correct position
            for (int aRow = 0; aRow < Cards.Card.possibleSuits.Length; aRow++)
            {
                int countSequence = 0;                     //Counter for Number of Cards in Sequence
                bool correctPosition = false;                //Ensure Correct Position Flag is Unset

                for (int aCol = 0; aCol < Cards.Card.possibleRanks.Length - 1; aCol++)
                {
                    PlayPosition aPosition = new PlayPosition(dataGridGameBoard, aCol, aRow);

                    String thisCard = dataGridGameBoard[aCol, aRow].Tag.ToString();//This Card Value
                    String nextCard = dataGridGameBoard[aCol + 1, aRow].Tag.ToString();  //Next Card

                    if (thisCard.Equals(playSpace))              //If No Card in Current Position...
                    {
                        correctPosition = false;             //Ensure Correct Position Flag is Unset
                        countSequence = 0; //Ensure Sequence Counter
                    }
                    else               //Card is in Current position, begin checking for sequence...
                    {
                        if (!(nextCard.Equals(playSpace)))        //If Next Card is not Play Space...
                        {
                            correctPosition = isCorrectPosition(aPosition);
                            if (aPosition.getSuit(thisCard) == aPosition.getSuit(nextCard)) //Same Suit?
                            {
                                if (nextCard.Equals(gameDeck.getNextCardDescending(thisCard)))
                                {
                                    countSequence++;                    //Increment Sequence Counter
                                    scoreThisGame += incrementSequence;     //Add Score for sequence
                                    if ((countSequence > 0) && (correctPosition))
                                        scoreThisGame += incrementPosition; //Add Score for Position
                                }
                                else
                                {
                                    if ((countSequence > 0) && (correctPosition))
                                        scoreThisGame += incrementPosition; //Add Score for Position

                                    countSequence = 0;           //Reset Number of Cards in Sequence
                                    correctPosition = false; //Ensure Correct Position Flag is Unset
                                }
                            }
                            else              //Card of Different Suit, end sequence accumulation...
                            {
                                if ((countSequence > 0) && (correctPosition))
                                    scoreThisGame += incrementPosition;     //Add Score for Position

                                countSequence = 0;               //Reset Number of Cards in Sequence
                                correctPosition = false;     //Ensure Correct Position Flag is Unset
                            }
                        }
                        else                   //Otherwise, next Card is a "Play Space" (no Card)...
                        {
                            if ((countSequence > 0) && (correctPosition))
                                scoreThisGame += incrementPosition;         //Add Score for Position

                            countSequence = 0;                   //Reset Number of Cards in Sequence
                            correctPosition = false;         //Ensure Correct Position Flag is Unset
                        }
                    }
                }
            }

            //Sum Score for completed Suits - Use recursive call to function isSuitComplete
            int suitsCompleted = 0;     //Count number of suits that were completed; if 4, game won!

            for (int aRow = 0; aRow < Cards.Card.possibleSuits.Length; aRow++)
            {
                if (isSuitComplete(aRow))                  //If the row contains a completed suit...
                {
                    scoreThisGame += incrementCompleteSuit;    //Increment score for a complete suit
                    suitsCompleted++;                            //Increment suits completed counter
                }
            }

            if (suitsCompleted == 4)                           //If All four suits are completed...
                scoreThisGame += (suitsCompleted * gameBuyIn); //Add Game winning bonus!!

            //Adjust current game score for buy-in and record to Player's stats
            scoreThisGame -= gameBuyIn;                             // Deduct the Game Buy-In Amount

            //Update Player Statistics then Display Results
            myPlayer.finishGameForPlayer(scoreThisGame, moveCount, computeTimePlayed()); 
            myPlayer.displayPlayerStats(scoreThisGame, moveCount, computeTimePlayed());
        }

        /*******************************************************************************************
         * Method: setUpNewGame
         * Prepares the playing board, shuffles the deck of cards and initializes the tableau for
         * playing the game
         */
        private void setUpNewGame(Cards.Deck aDeck)
        {
            aDeck.shuffleDeck();                                         //Shuffle the Deck of Cards
            aDeck.cutDeck();                                                          //Cut the Deck

            dealCards(aDeck);                                        //Deal the Cards to the Tableau
            removeAces();                                    //Remove Aces to Initialize Play Spaces

            moveCount = 0;                              //Initialize the Move Counter for a New Game
            txtMoveCount.Text = moveCount.ToString();                       //Display Count of Moves

            gameStartTime = System.DateTime.Now;                 //Set the Starting Time for Game...
            gameTime.Start();                                                  //And start the clock
        }

        /*******************************************************************************************
         * Method: swapPlayCards
         * Copies the Card Value and Card Face from the Source Play Position to the Destination
         * Play Position, then sets the Card Face and Value of the Source to "Playable".
         */
        private void swapPlayCards(PlayPosition sourceCard, PlayPosition destinationCard)
        {
            //Copy Source Card to Destination
            dataGridGameBoard[destinationCard.getColumn(), destinationCard.getRow()].Tag =
                dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Tag;
            dataGridGameBoard[destinationCard.getColumn(), destinationCard.getRow()].Value =
                dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Value;

            //Set Source to "Playable"
            dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Tag = playSpace;
            dataGridGameBoard[sourceCard.getColumn(), sourceCard.getRow()].Value = playSpaceIcon;

            myUndoBuffer.push(sourceCard, destinationCard);             //Push Move onto Undo Buffer
        }

        private void undoMove()
        {
            UndoBuffer.UndoItem myItem;                                //Local Storage for Undo Item
            myItem = myUndoBuffer.pop();                                 //Get the last Move Made...
            swapPlayCards(myItem.getToPosition(), myItem.getFromPosition());           //And Undo it
        }

    }
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

        /*******************************************************************************************
         * Constructor: PlayPosition (Default)
         * Default constructor for a PlayPostion.
         */
        public PlayPosition(DataGridView aGrid, int aColumn, int aRow)
        {
            cardValue = aGrid[aColumn, aRow].Tag.ToString();        //Store Rank and Suit of the Card
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
         * Function: findPlayCard
         * Returns the Play Position object for the Card currently being played (that is, the card
         * to move to the currently selected play position).
         */
        public PlayPosition findPlayCard(DataGridView aGrid)
        {
            PlayPosition playCard = new PlayPosition();                 //Create Return Value Object

            playCard.cardValue = identifyPlayCard(aGrid);               //Get the Card to Search for

            //Search the Grid for the Desired Card
            for (int aRow = 0; aRow < aGrid.RowCount; aRow++)
                for (int aCol = 0; aCol < aGrid.ColumnCount; aCol++)
                {
                    if (aGrid[aCol, aRow].Tag.ToString().Equals(playCard.cardValue))
                    {
                        playCard.positionColumn = aCol;                       //Set the Column Value
                        playCard.positionRow = aRow;                             //Set the Row Value

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
        private String identifyPlayCard(DataGridView aGrid)
        {
            //Get the Rank and Suit of Card in Cell to Left
            String searchValue = aGrid[this.getColumn() - 1, this.getRow()].Tag.ToString();

            String cardSuit = getSuit(searchValue);                            //Get the Card's Suit
            String cardRank = getRank(searchValue);                            //Get the Card's Rank

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
     * Simulates a Stack to provide for "Undo" functionality.
     **********************************************************************************************/
    #region
    public partial class UndoBuffer
    {
        /*******************************************************************************************
        * Sub-Class UndoItem
        * Defines the structure of a single Undo Item.
        */
        public partial class UndoItem
        {
            private PlayPosition fromPosition;                    //Card Position that move was from
            private PlayPosition toPosition;                        //Card Position that move was to

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
         * Class Variables and Constants
         */
         private List<UndoItem> myUndoItems = new List<UndoItem>();

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

            if(bufferItem >= 0)
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
        public void push(PlayPosition aFromPosition, PlayPosition aToPosition)
        {
            UndoItem newItem = new UndoItem(aFromPosition, aToPosition);
            myUndoItems.Add(newItem);
        }
    }

    #endregion
}
