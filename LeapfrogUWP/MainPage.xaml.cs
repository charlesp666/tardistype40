/***************************************************************************************************
 * Page Class: MainPage
 * 
 * Main or "Splash" Page definition for launching of game.
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeapfrogUWP
{
    public sealed partial class MainPage : Page
    {
        ///*******************************************************************************************
        //* Class Variables and Constants
        //*/

        private DispatcherTimer processDelay = new DispatcherTimer();  //Timer for Progress Bar Status
        private int timeDelayInMS = 5000;                     //Time to Display Screen in milliseconds

        ///*******************************************************************************************
        // * Constructor: LeapFrog (Default)
        // * 
        // * Creates and Displays the Splash Screen when Game is first started
        // */
        public MainPage()
        {
            this.InitializeComponent();

            // Build the Game Information object
            GameInformation myGameInfo = new GameInformation();

            // Populate the Splash page components
            lblGameTitle.Text = myGameInfo.getNameOfGame();                     //Name of appliication
            txtSubTitle.Text = myGameInfo.getGameSubTitle();                       //Subtitle for Game
            lblCopyright.Text = myGameInfo.getCopyrightNotice();                    //Copyright Notice
            txtRights.Text = myGameInfo.getRightsNotice();                             //Rights Notice
            txtPublisher.Text = "Pubished by: " + myGameInfo.getPublisher();       //Name of Publisher
            txtVersion.Text = "Version: " + myGameInfo.getVersion();             //Game Version Number

            picGameImage.Source = myGameInfo.getGameImage();                      //Get the Game Image

            //Activate "Splash" Page and delay game load to give user time to read it...
            Window.Current.Activate();

            pbGameIntro.Maximum = timeDelayInMS;                      //Set Maximum progress bar value
            delay(timeDelayInMS);                 //Pause a few seconds to display the Splash Screen

            //Get or Initialize the Player Object
            Player myAvatar = new Player();

            //    //Declare and Activate the Main Game Tableau
            //    GameTableau myLeapFrog = new GameTableau(myAvatar, myGameInfo);

            //    this.Close();                                               //Close the Splash Screen...
            //    myLeapFrog.Show();                                      //And display the Tableau window
        }

        ///*******************************************************************************************
        // * Method: delay
        // * Pause Processing for specified number of milliseconds.
        // */
        private void delay(int milliSecondsToPauseFor)
        {
            Double deltaTime;
            System.DateTime startInstant = System.DateTime.Now;
            System.DateTime thisInstant = startInstant;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
            System.DateTime finalInstant = thisInstant.Add(duration);

            while (finalInstant >= thisInstant)
            {
                //System.Windows.Forms.Application.DoEvents();
                thisInstant = System.DateTime.Now;

                //Update the Progress bar on the Window
                deltaTime = (int)(thisInstant - startInstant).TotalMilliseconds;
                if (deltaTime > pbGameIntro.Maximum)
                {
                    deltaTime = pbGameIntro.Maximum;
                }
                pbGameIntro.Value = deltaTime;
            }
            pbGameIntro.Value = milliSecondsToPauseFor;            //Update Progress bar completely
        }
    }
}
