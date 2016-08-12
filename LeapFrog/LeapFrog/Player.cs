/********************************************************************************
 * Class: Player
 * 
 * Defines the Properties and Methods for the Player Object. 
 * 
 * Author:              Charles J Pilgrim
 * Created:             10-October-2013
 * 
 * Last Maintained By:  Charles J Pilgrim
 * Last Maintained:     22-January-2016
 * 
 * @Copyright (c) 2013-2016 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Windows.Forms;

using Microsoft.Win32;                                               //For Interacting with Registry

/***************************************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    /***********************************************************************************************
     * Class: Player
     **********************************************************************************************/
    public class Player
    {
        private static String keyName = "SOFTWARE\\LeapFrog";         //Name of App and Registry Key
        private static RegistryKey playerBaseKey = Registry.CurrentUser; //Base Key Game Information
        private RegistryKey playerSubKey;

        private System.Security.Principal.WindowsIdentity playerID = System.Security.Principal.WindowsIdentity.GetCurrent();

        private int gamesPlayed = 0;                   //Cumulative Number of Games Played by player
        private int gameWinnings = 0;                               //Cumulative Winnings for player
        private int countMoves = 0;
        private String namePlayer = "";                         //Name or Identity of Current Player
        
        /*******************************************************************************************
         * Constructor: Player (Default)
         */
        public Player()
        {
            //Set Default Values before Attempting to Load Stats from Registry
            setPlayerName(playerID.Name);          //Set the Player Name to the Logged In User Name
            setGameWinnings(0);                                 // Initialize the Player's Winnings
            setGamesPlayed(0);                        // Initialize the Player's Games Played Count

            loadPlayerStats();                                    //Load Player Stats from Registry
        }

        /*******************************************************************************************
         * Method: addToMoves
         * Adds the Value passed as Argument to the Current Value of the Object's Game
         * Winnings.
         * @param aValue - Value (Dollars) to Add to Winnings
         */
        public void addToMoves(int aValue)
        {
            this.countMoves += aValue;
        }

        /*******************************************************************************************
         * Method: addToWinnings
         * Adds the Value passed as Argument to the Current Value of the Object's Game
         * Winnings.
         * @param aValue - Value (Dollars) to Add to Winnings
         */
        public void addToWinnings(int aValue)
        {
            this.gameWinnings += aValue;
        }

        /*******************************************************************************************
         * Method: displayPlayerStats
         * Display the Current Statistics (Name
         * @param currentScore - Score for Current Game
         */
        public void displayPlayerStats()
        {
            String messageBoxTitle = "Player Score";

            String reportScore = "For " + getPlayerName() + "\n\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\n\nTotal Moves for All Games: " + getCountMoves();

            MessageBox.Show(reportScore, messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*******************************************************************************************
         * Method: displayPlayerStats
         * Display the Current Statistics (Name
         * @param currentScore - Score for Current Game
         */
        public void displayPlayerStats(int currentScore, int currentMoves)
        {
            String messageBoxTitle = "Player Score";

            String reportScore = "For " + getPlayerName() + "\n\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for Current Game: " + currentScore.ToString();
            reportScore += "\nMoves in Current Game: " + currentMoves.ToString();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\nTotal Moves for All Games: " + getCountMoves();

            MessageBox.Show(reportScore, messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*******************************************************************************************
         * Method: finishGameForPlayer
         * Adds winnings and increments the number of games for player, then writes data to
         * Registry.
         * @param currentScore - Score for latest game.
         */
        public void finishGameForPlayer(int currentScore, int numberMoves)
        {
            loadPlayerStats();                                      //Get the Player's current stats
            
            this.incrementGamesPlayed();                 //Increment count of number of games played
            this.addToWinnings(currentScore);                       //Add Winnings to Player's Total
            this.addToMoves(numberMoves);                      //Add Moves to Move Count Accumulator
            
            writePlayerStats();                          //Write Updated Statistics Back to Registry
        }

        /*******************************************************************************************
         * Method: getCountMoves
         * Returns the total Number of Moves made
         */
        public int getCountMoves()
        {
            return (this.countMoves);
        }

        /*******************************************************************************************
         * Method: getGameWinnings
         * Returns the total Game Winnings for the Object that Invoked the method
         */
        public int getGameWinnings()
        {
            return (this.gameWinnings);
        }
        
        /*******************************************************************************************
         * Method: getGamesPlayed
         * Returns the Cumulative Number of Games Won by Object that Invoked Method.
         */
        public int getGamesPlayed()
        {
            return (this.gamesPlayed);
        }
        
        /*******************************************************************************************
         * Method: getPlayerName
         * Returns the Name of the Player of the Object that Invoked the Method.
         */
        public String getPlayerName()
        {
            return this.namePlayer;
        }
        
        /*******************************************************************************************
         * Method: incrementGamesPlayed
         * Increments the number of games Current Player has played.
         */
        private void incrementGamesPlayed()
        {
            this.gamesPlayed++;                                            //Add One to Game Counter
        }
        
        /*******************************************************************************************
         * Method: loadPlayerStats
         * Loads the Player Stats from the Registry for the Current User.
         */
        private void loadPlayerStats()
        {
            playerSubKey = playerBaseKey.OpenSubKey(keyName, true); //Attempt to Open the Sub Key...
            if (playerSubKey == null)                                       //If no Sub Key found...
            {
                playerSubKey = playerBaseKey.CreateSubKey(keyName);          //Create the Sub Key...
                writePlayerStats();                                        //And Save Default Values
            }
            else                                    //Otherwise, load the Stats from the Registry...
             {
                setPlayerName((string)playerSubKey.GetValue("PlayerName"));
                setGamesPlayed((int)playerSubKey.GetValue("GamesPlayed"));
                setGameWinnings((int)playerSubKey.GetValue("Winnings"));
                setCountMoves((int)playerSubKey.GetValue("Moves"));
             }
        }

        /*******************************************************************************************
         * Method: setCountMoves
         * Sets the Value of the Number of Moves.
         */
        public void setCountMoves(int aValue)
        {
            this.countMoves = aValue;
        }

        /*******************************************************************************************
         * Method: setGameWinnings
         * Sets the Value of the Game Winnings.
         */
        public void setGameWinnings(int aValue)
        {
            this.gameWinnings = aValue;
        }
        
        /*******************************************************************************************
         * Method: setGamesPlayed
         * Sets the Value of Games Won by Player that Invoked the Method.
         * aValue - Integer Indicating the Total Number of Games Won
         */
        public void setGamesPlayed(int aValue)
        {
            this.gamesPlayed = aValue;
        }
        
        /*******************************************************************************************
         * Method: setPlayerName
         * Sets the Name of the Player of the Object that Invoked the Method.
         */
        public void setPlayerName(String aName)
        {
            this.namePlayer = aName;
        }

        /*******************************************************************************************
         * Method: writePlayerStats
         * Writes Player Stats to the Registry entry for this player's game.
         */
        private void writePlayerStats()
        {
            playerSubKey.SetValue("PlayerName", this.namePlayer);
            playerSubKey.SetValue("GamesPlayed", this.gamesPlayed);
            playerSubKey.SetValue("Winnings", this.gameWinnings);
            playerSubKey.SetValue("Moves", this.countMoves);
        }
    }
}
