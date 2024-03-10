﻿/********************************************************************************
 * Class: Player
 * 
 * Defines the Properties and Methods for the Player Object. 
 * 
 * Author:           Charles J Pilgrim
 * Created:          04-March-2024
 * 
 * LastMaintained:   05-March-2024
 * LastMaintainedBy: cjpilgrim
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Popups;

/***********************************************************************************************
 * Class: Player
 **********************************************************************************************/
public class Player
{

    private int gamesPlayed = 0;                             //Cumulative Number of Games Played
    private int gameWinnings = 0;                                          //Cumulative Winnings
    private int countMoves = 0;             //Cumulative Count of Moves Made in All Games Played
    private String namePlayer = "jdoe@somewhere.net";       //Default Identity of Current Player
    private TimeSpan timePlayed = new TimeSpan(0,0,0);         //Total Time for All Games Played

    private String localSettingsName = "PlayerStatistics";
    private Windows.Storage.ApplicationDataContainer PlayerStats = Windows.Storage.ApplicationData.Current.LocalSettings;
    private Windows.Storage.ApplicationDataCompositeValue localStats = new Windows.Storage.ApplicationDataCompositeValue();

    /*******************************************************************************************
     * Constructor: Player (Default)
     */
    public Player()
    {
        //initializePlayerName();

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
        localStats["Moves"] = this.countMoves;
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
        String theRegTime = localStats["TimePlayed"].ToString();      //Read Current Time Played

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
        String messageBoxTitle = "Player Statistics for " + getPlayerName() + ":";

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

        String reportScore = "For " + getPlayerName() + "\n\nGames Played : " + getGamesPlayed();
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
        return ((int)localStats["Moves"]);
    }

    /*******************************************************************************************
     * Method: getGameWinnings
     * Returns the total Game Winnings for the Object that Invoked the method
     */
    public int getGameWinnings()
    {
        return ((int)localStats["Winnings"]);
    }
    
    /*******************************************************************************************
     * Method: getGamesPlayed
     * Returns the Cumulative Number of Games Won by Object that Invoked Method.
     */
    public int getGamesPlayed()
    {
        return ((int)localStats["GamesPlayed"]);
    }
    
    /*******************************************************************************************
     * Method: getPlayerName
     * Returns the Name of the Player of the Object that Invoked the Method.
     */
    public String getPlayerName()
    {
        return ((string)localStats["PlayerName"]);
    }

    /*******************************************************************************************
     * Method: getTimePlayed
     * Returns the Amount of Time Played
     */
    public TimeSpan getTimePlayed()
    {
        return (TimeSpan)localStats["TimePlayed"];
    }

    /*******************************************************************************************
     * Method: incrementGamesPlayed
     * Increments the number of games Current Player has played.
     */
    private void incrementGamesPlayed()
    {
        this.gamesPlayed = getGamesPlayed();
        this.gamesPlayed++;
        localStats["GamesPlayed"] = this.gamesPlayed;
    }

    /*******************************************************************************************
     * Method: initializePlayerName
     * Initializes the Player Name to tbe Current User.
     */
    private async void initializePlayerName()
    {
        var domainUser = "";

        IReadOnlyList<User> users = await User.FindAllAsync();
       // var currentUser = users[0];

        // Get the AccountName for the Current User grabbed above
        var accountName = await users[0].GetPropertyAsync(KnownUserProperties.AccountName);
        domainUser = (string)accountName;

        this.namePlayer = domainUser;
    }

    /*******************************************************************************************
     * Method: loadPlayerStats
     * Loads the Player Stats from the Registry for the Current User.
     */
    private void loadPlayerStats()
    {
        localStats = (Windows.Storage.ApplicationDataCompositeValue) PlayerStats.Values[localSettingsName];
        var testPlayerName = (string)localStats["PlayerName"];    //PlayerName to Test for Entry

        //Check if Current User has Application Data; if not, Create Entry
        if ((localStats == null) || (testPlayerName == null) || (testPlayerName == "")) 
        {
            writePlayerStats();                       //And Save Default Application Data Values
        }
        else                            //Otherwise, load the Stats from the Application Data...
         {
            setPlayerName((string)localStats["PlayerName"]);
            setGamesPlayed((int)localStats["GamesPlayed"]);
            setGameWinnings((int)localStats["Winnings"]);
            setCountMoves((int)localStats["Moves"]);

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
        //Windows.Storage.ApplicationDataCompositeValue localStats = new Windows.Storage.ApplicationDataCompositeValue();

        localStats["PlayerName"]  = this.namePlayer;
        localStats["GamesPlayed"] = this.gamesPlayed;
        localStats["Winnings"]    = this.gameWinnings;
        localStats["Moves"]       = this.countMoves;
        localStats["TimePlayed"]  = this.timePlayed;

        PlayerStats.Values[localSettingsName] = localStats;
    }
}
