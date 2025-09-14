/***************************************************************************************************
 * UserControl: PlayerStatsDialog
 * 
 * Custom Dialog to display Users Statistics, e.g.: Total games played; total score; etc.. 
 * 
 * @Copyright (c) 2025 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
//using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    public sealed partial class PlayerStatsDialog : UserControl
    {
        /*******************************************************************************************
         * Constructor: PlayerStatsDialog (Default)
         */
        public PlayerStatsDialog(Player thePlayer)
        {
            InitializeComponent();

            /*Get and Store Total Time Played, Total Games Played and Total Score                 */
            string totalTimePlayed = thePlayer.getTimePlayed().ToString();
            int totalGamesPlayed = thePlayer.getGamesPlayed();
            int totalScore = thePlayer.getGameWinnings();

            /*Display Dates Game was first played and most recently played                        */
            txtGameFirstPlayed.Text = thePlayer.getGameFirstPlayed();
            txtGameMostRecentlyPlayed.Text = thePlayer.getGameMostRecentPlayed();

            /*Display Total Time Played, Total Games Played and Total Score                       */
            txtTimePlayed.Text = totalTimePlayed;
            txtGameCount.Text = totalGamesPlayed.ToString();
            txtTotalScore.Text = totalScore.ToString();

            /*Compute Various Statistics and Display                                              */
            string meanTimePerGame = computeMeanTimePerGame(totalTimePlayed, totalGamesPlayed);
            txtMeanTimePerGame.Text = meanTimePerGame;

            string meanScorePerGame = computeMeanScorePerGame(totalScore, totalGamesPlayed);
            txtMeanScorePerGame.Text = meanScorePerGame;
        }

        /*******************************************************************************************
         * Method: computeMeanScorePerGame
         * Computes the Mean Score per game by dividing the total score by the number of games
         * played; this value is converted to string and returned.
         */
        public string computeMeanScorePerGame(int totalScore, int countGames)
        {
            int meanScorePerGame = 0;
            if (countGames > 0)
                { meanScorePerGame = (int)Math.Round((float)totalScore / (float)countGames); }

            return (meanScorePerGame.ToString());
        }

        /*******************************************************************************************
         * Method: computeMeanTimePerGame
         * Converts the string of total time played ("hh:mm:ss") to total number of seconds then
         * divides by the count of games played to provide average time per game. This result is
         * then converted back to string ("hh:mm:ss").
         */
        public string computeMeanTimePerGame(string timePlayed, int countGames)
        {
            string[] timeComponents = timePlayed.Split(":");

            int Hours = int.Parse(timeComponents[0]);
            int Minutes = int.Parse(timeComponents[1]);
            int Seconds = int.Parse(timeComponents[2]);

            int totalSeconds = (((Hours * 60) + Minutes) * 60) + Seconds;

            int meanGameTime = 0;
            if (countGames > 0)
                { meanGameTime = (int)Math.Round((float)totalSeconds / (float)countGames); }

            Seconds = meanGameTime % 60;
            Hours = (meanGameTime - Seconds) / 3600;
            Minutes = (meanGameTime - Hours) / 60;

            string txtHours = "00" + Hours.ToString();
            txtHours = txtHours.Substring(txtHours.Length - 2);

            string txtMinutes = "00" + Minutes.ToString();
            txtMinutes = txtMinutes.Substring(txtMinutes.Length - 2);

            string txtSeconds = "00" + Seconds.ToString();
            txtSeconds = txtSeconds.Substring(txtSeconds.Length - 2);

            return (txtHours + ":" + txtMinutes + ":" + txtSeconds);
        }
    }
}
