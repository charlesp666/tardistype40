/********************************************************************************
 * Class: Player
 * 
 * Defines the Properties and Methods for the Player Object. 
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using Windows.Storage;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Windows.UI.Popups;

namespace LeapFrogWinUI
{
    internal class Player
    {
        private int gamesPlayed = 0;                             //Cumulative Number of Games Played
        private int gameWinnings = 0;                                          //Cumulative Winnings
        private int countMoves = 0;             //Cumulative Count of Moves Made in All Games Played

        //private String namePlayer = "jdoe@somewhere.net";            //Set Current Player to Default
        private TimeSpan timePlayed = new TimeSpan(0, 0, 0);         //Total Time for All Games Played

        //Parameters to track User Statistics
        private ApplicationDataContainer PlayerStats = ApplicationData.Current.LocalSettings;

        /*******************************************************************************************
         * Constructor: Player (Default)
         */
        public Player()
        {
            loadPlayerStats();                            //Load Player Stats from Application Data
        }

        /*******************************************************************************************
         * Method: addToMoves
         * Adds the Value passed as Argument to the Current Value of the Object's Game
         * Winnings.
         * @param aValue - Count of Moves in Last Game
         */
        public void addToMoves(int aValue)
        {
            this.countMoves = getCountMoves();
            this.countMoves += aValue;

            PlayerStats.Values["Moves"] = this.countMoves;
        }

        /*******************************************************************************************
         * Method: addToWinnings
         * Adds the Value passed as Argument to the Current Value of the Object's Game
         * Winnings.
         * @param aValue - Value to Add to Winnings
         */
        public void addToWinnings(int aValue)
        {
            var currentWinnings = getGameWinnings();
            currentWinnings += aValue;
            setGameWinnings(currentWinnings);
        }

        /*******************************************************************************************
          * Method: addToTime
          * Adds the Game Time to the Cumulative Game Time.
          * @param aValue - Value (Dollars) to Add to Winnings
          */
        public void addToTime(TimeSpan aValue)
        {
            this.timePlayed = getTimePlayed();
            this.timePlayed += aValue;
            setTimePlayed(this.timePlayed);
        }

        /*******************************************************************************************
          * Method: convertRegistryTimePlayed
          * Converts the Registry String value of Time Played to a TimeSpan Value
          */
        private TimeSpan convertRegistryTimePlayed()
        {
            //String theRegTime = localStats["TimePlayed"].ToString();      //Read Current Time Played
            String theRegTime = PlayerStats.Values["TimePlayed"].ToString();//Read Current Time Played

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
        public async void displayPlayerStats()
        {
            String messageBoxTitle = "Player Statistics:";

            String reportScore = "\n\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\n\nTotal Moves for All Games: " + getCountMoves();

            String totalTimePlayed = convertRegistryTimePlayed().ToString();
            reportScore += "\n\nTotal Time for All Games: " + totalTimePlayed;

            messageBoxTitle += reportScore;

            MessageDialog PlayerStats = new MessageDialog(messageBoxTitle);

            PlayerStats.Commands.Add(new UICommand("Close"));
            PlayerStats.DefaultCommandIndex = 0;

            await PlayerStats.ShowAsync();
        }

        /*******************************************************************************************
         * Method: displayPlayerStats
         * Display the Current Statistics (Name
         * @param currentScore - Score for Current Game
         */
        public async void displayPlayerStats(int currentScore, int currentMoves, TimeSpan gameTime)
        {
            String messageBoxTitle = "Player Score";

            String reportScore = "\nGames Played : " + getGamesPlayed();
            reportScore += "\n\nScore for Current Game: " + currentScore.ToString();
            reportScore += "\nMoves in Current Game: " + currentMoves.ToString();
            reportScore += "\n\nScore for All Games: " + getGameWinnings();
            reportScore += "\nTotal Moves for All Games: " + getCountMoves();

            String[] timeOfPlay = gameTime.ToString().Split('.');
            reportScore += "\n\nGame Time for This Game: " + timeOfPlay[0];
            String totalTimePlayed = convertRegistryTimePlayed().ToString();
            reportScore += "\nTotal Time for All Games: " + totalTimePlayed;

            messageBoxTitle += reportScore;

            MessageDialog PlayerStats = new MessageDialog(messageBoxTitle);

            PlayerStats.Commands.Add(new UICommand("Close"));
            PlayerStats.DefaultCommandIndex = 0;

            await PlayerStats.ShowAsync();
        }

        /*******************************************************************************************
         * Method: finishGameForPlayer
         * Adds winnings and increments the number of games for player, then writes data to
         * Registry.
         * @param currentScore - Score for latest game.
         */
        public void finishGameForPlayer(int currentScore, int numberMoves, TimeSpan gameTime)
        {
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
            return ((int)PlayerStats.Values["Moves"]);
        }

        /*******************************************************************************************
         * Method: getGameWinnings
         * Returns the total Game Winnings for the Object that Invoked the method
         */
        public int getGameWinnings()
        {
            return ((int)PlayerStats.Values["Winnings"]);
        }

        /*******************************************************************************************
         * Method: getGamesPlayed
         * Returns the Cumulative Number of Games Won by Object that Invoked Method.
         */
        public int getGamesPlayed()
        {
            return ((int)PlayerStats.Values["GamesPlayed"]);
        }

        /*******************************************************************************************
         * Method: getPlayerName
         * Returns the Name of the Player of the Object that Invoked the Method.
         */
        public String getPlayerName()
        {
            return ((string)PlayerStats.Values["PlayerName"]);
        }

        /*******************************************************************************************
         * Method: getTimePlayed
         * Returns the Amount of Time Played
         */
        public TimeSpan getTimePlayed()
        {
            return (TimeSpan)PlayerStats.Values["TimePlayed"];
        }

        /*******************************************************************************************
         * Method: incrementGamesPlayed
         * Increments the number of games Current Player has played.
         */
        private void incrementGamesPlayed()
        {
            this.gamesPlayed = getGamesPlayed();
            this.gamesPlayed++;

            PlayerStats.Values["GamesPlayed"] = this.gamesPlayed;
        }

        /*******************************************************************************************
         * Method: loadPlayerStats
         * Loads the Player Stats from the Registry for the Current User.
         */
        private void loadPlayerStats()
        {

            if (PlayerStats is null)
            {
                setGamesPlayed(0);
                setGameWinnings(0);
                setCountMoves(0);
                setTimePlayed(TimeSpan.Zero);

                writePlayerStats();
            }

            setGamesPlayed((int)PlayerStats.Values["GamesPlayed"]);
            setGameWinnings((int)PlayerStats.Values["Winnings"]);
            setCountMoves((int)PlayerStats.Values["Moves"]);

            setTimePlayed(convertRegistryTimePlayed());
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
            PlayerStats.Values["GamesPlayed"] = this.gamesPlayed;
            PlayerStats.Values["Winnings"] = this.gameWinnings;
            PlayerStats.Values["Moves"] = this.countMoves;
            PlayerStats.Values["TimePlayed"] = this.timePlayed;
        }
    }
}
