/***************************************************************************************************
 * Class: LeapFrog.cs
 * 
 * Intro Screen for the solitaire game of "LeapFrog." 
 * 
 * Author:              Charles J Pilgrim
 * Created:             23-July-2013
 * 
 * @Copyright (c) 2013-2017 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Windows.Forms;

/***************************************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    public partial class LeapFrogSplashScreen : Form
    {
        /*******************************************************************************************
         * Class Variables and Constants
         */

        private Timer processDelay = new Timer();                    //Timer for Progress Bar Status
        private int timeDelayInMS = 5000;                   //Time to Display Screen in milliseconds

        /*******************************************************************************************
         * Constructor: LeapFrog (Default)
         * 
         * Creates and Displays the Splash Screen when Game is first started
         */
        public LeapFrogSplashScreen()
        {
            InitializeComponent();

            Application.UseWaitCursor = true;                          //Set the Wait Cursor Display

            //Build the Game Information object
            GameInformation myGameInfo = new GameInformation( Application.ProductName
                                                            , "A Game of Solitaire"
                                                            );

            //Populate the Form components
            lblGameTitle.Text = myGameInfo.getNameOfGame();                   //Name of appliication
            textSubtitle.Text = myGameInfo.getGameSubTitle();                    //Subtitle for Game
            lblCopyright.Text = myGameInfo.getCopyrightNotice();                  //Copyright Notice
            textRights.Text = myGameInfo.getRightsNotice();                          //Rights Notice

            picGameImage.Image = myGameInfo.getGameImage();                     //Get the Game Image

            pbGameIntro.Maximum = timeDelayInMS;                    //Set Maximum progress bar value

            this.Show();                                                          //Display the Form

            delay(timeDelayInMS);                 //Pause a few seconds to display the Splash Screen

            //Get or Initialize the Player Object
            Player myAvatar = new Player();

            //Declare and Activate the Main Game Tableau
            GameTableau myLeapFrog = new GameTableau(myAvatar, myGameInfo);

            Application.UseWaitCursor = false;                       //Unset the Wait Cursor Display
            this.Close();                                               //Close the Splash Screen...
            myLeapFrog.Show();                                      //And display the Tableau window
        }

        /*******************************************************************************************
         * Method: delay
         * Pause Processing for specified number of milliseconds.
         */
        private void delay(int milliSecondsToPauseFor)
        {
            int deltaTime;
            System.DateTime startInstant = System.DateTime.Now;
            System.DateTime thisInstant = startInstant;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
            System.DateTime finalInstant = thisInstant.Add(duration);

            while (finalInstant >= thisInstant)
            {
                System.Windows.Forms.Application.DoEvents();
                thisInstant = System.DateTime.Now;

                //Update the Progress bar on the Window
                deltaTime = (int)(thisInstant - startInstant).TotalMilliseconds;
                if(deltaTime > pbGameIntro.Maximum)
                {
                    deltaTime = pbGameIntro.Maximum;
                }
                pbGameIntro.Value = deltaTime;
            }
            pbGameIntro.Value = milliSecondsToPauseFor;            //Update Progress bar completely
        }
    }
}
