/********************************************************************************
 * Class: Player
 * 
 * Defines the Properties and Methods for the Player Object. 
 * 
 * Author:              Charles J Pilgrim
 * Created:             10-October-2013
 * 
 * LastMaintained:   04-March-2024
 * LastMaintainedBy: cjpilgrim
 * 
 * @Copyright (c) 2013-2017 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Linq;
using System.Resources;                                     //To Pull Images from Assembly Resources
using Windows.Storage;
using Windows.UI.Xaml.Controls;                                            //For Image Data Datatype

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
        private Windows.Storage.ApplicationDataContainer PlayerStats = Windows.Storage.ApplicationData.Current.LocalSettings;
        private System.Security.Principal.WindowsIdentity playerID = System.Security.Principal.WindowsIdentity.GetCurrent();

        private int gamesPlayed = 0;                             //Cumulative Number of Games Played
        private int gameWinnings = 0;                                          //Cumulative Winnings
        private int countMoves = 0;             //Cumulative Count of Moves Made in All Games Played
        private String namePlayer = "";                         //Name or Identity of Current Player
        private TimeSpan timePlayed = new TimeSpan(0,0,0);         //Total Time for All Games Played
        
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
         * @param aValue - Count of Moves in Last Game
         */
        public void addToMoves(int aValue)
        {
            this.countMoves += aValue;
        }

        /*******************************************************************************************
         * Method: addToWinnings
         * Adds the Value passed as Argument to the Current Value of the Object's Game
         * Winnings.
         * @param aValue - Value to Add to Winnings
         */
        public void addToWinnings(int aValue)
        {
            this.gameWinnings += aValue;
        }

        /*******************************************************************************************
          * Method: addToTime
          * Adds the Game Time to the Cumulative Game Time.
          * @param aValue - Value (Dollars) to Add to Winnings
          */
        public void addToTime(TimeSpan aValue)
        {
            this.timePlayed += aValue;
        }

        /*******************************************************************************************
          * Method: convertRegistryTimePlayed
          * Converts the Registry String value of Time Played to a TimeSpan Value
          */
        private TimeSpan convertRegistryTimePlayed()
        {
            String theRegTime = (string)PlayerStats.Values["TimePlayed"]; //Read Current Time Played

            String[] theTime = theRegTime.Split('.');                          //Remove Milliseconds
            String[] timeConvert = theTime[0].Split(':');         //Split Hours, Minutes and Seconds

            int theHours = Convert.ToInt32(timeConvert[0]);               //Convert Hours to Integer
            int theMinutes = Convert.ToInt32(timeConvert[1]);           //Convert Minutes to Integer
            int theSeconds = Convert.ToInt32(timeConvert[2]);           //Convert Seconds to Integer
     
            TimeSpan totalTime = new TimeSpan(theHours, theMinutes, theSeconds); //Build TimeSpan

            return totalTime;
        }

        /*******************************************************************************************
         * Method: displayPlayerStats
         * Display the Current Statistics
         */
        public void displayPlayerStats()
        {
            String messageBoxTitle = "Player Score";

            String reportScore = "For " + getPlayerName() + "\n\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\n\nTotal Moves for All Games: " + getCountMoves();

            String totalTimePlayed = convertRegistryTimePlayed().ToString();
            reportScore += "\n\nTotal Time for All Games: " + totalTimePlayed;

            MessageBox.Show(reportScore, messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*******************************************************************************************
         * Method: displayPlayerStats
         * Display the Current Statistics (Name
         * @param currentScore - Score for Current Game
         */
        public void displayPlayerStats(int currentScore, int currentMoves, TimeSpan gameTime)
        {
            String messageBoxTitle = "Player Score";

            String reportScore = "For " + getPlayerName() + "\n\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for Current Game: " + currentScore.ToString();
            reportScore += "\nMoves in Current Game: " + currentMoves.ToString();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\nTotal Moves for All Games: " + getCountMoves();

            String[] timeOfPlay = gameTime.ToString().Split('.');
            reportScore += "\n\nGame Time for This Game: " + timeOfPlay[0];
            String totalTimePlayed = convertRegistryTimePlayed().ToString();
            reportScore += "\nTotal Time for All Games: " + totalTimePlayed;

            MessageBox.Show(reportScore, messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*******************************************************************************************
         * Method: finishGameForPlayer
         * Adds winnings and increments the number of games for player, then writes data to
         * Registry.
         * @param currentScore - Score for latest game.
         */
        public void finishGameForPlayer(int currentScore, int numberMoves, TimeSpan gameTime)
        {
            loadPlayerStats();                                      //Get the Player's current stats
            
            this.incrementGamesPlayed();                 //Increment count of number of games played
            this.addToWinnings(currentScore);                       //Add Winnings to Player's Total
            this.addToMoves(numberMoves);                      //Add Moves to Move Count Accumulator
            this.addToTime(gameTime);                     //Add Game Time to Time Played Accumulator
            
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
         * Method: getTimePlayed
         * Returns the Amount of Time Played
         */
        public TimeSpan getTimePlayed()
        {
            return this.timePlayed;
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
            namePlayer = (string)PlayerStats.Values["PlayerName"];

            if (namePlayer == null)                                       //If no Sub Key found...
            {
                writePlayerStats();                                        //And Save Default Values
            }
            else                                    //Otherwise, load the Stats from the Registry...
             {
                setPlayerName((string)PlayerStats.Values["PlayerName"]);
                setGamesPlayed((int)PlayerStats.Values["GamesPlayed"]);
                setGameWinnings((int)PlayerStats.Values["Winnings"]);
                setCountMoves((int)PlayerStats.Values["Moves"];
 
                setTimePlayed(convertRegistryTimePlayed());
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
         * Method: setTimePlayed
         * Sets the Total Time Played.
         */
        public void setTimePlayed(TimeSpan theTime)
        {
            this.timePlayed = theTime;
        }

        /*******************************************************************************************
         * Method: writePlayerStats
         * Writes Player Stats to the Registry entry for this player's game.
         */
        private void writePlayerStats()
        {
            PlayerStats.Values["PlayerName"]  = this.namePlayer;
            PlayerStats.Values["GamesPlayed"] = this.gamesPlayed;
            PlayerStats.Values["Winnings"]    = this.gameWinnings;
            PlayerStats.Values["Moves"]       = this.countMoves;
            PlayerStats.Values["TimePlayed"]  = this.timePlayed;
        }
    }
}
