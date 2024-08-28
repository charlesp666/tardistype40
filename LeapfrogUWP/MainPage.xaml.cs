/***************************************************************************************************
 * Page Class: MainPage
 * 
 * Main or "Splash" Page definition for launching of game.
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Windows.UI.ViewManagement; //For View Management
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation; //For Special Effects in Navigation

namespace LeapfrogUWP
{
    public sealed partial class MainPage : Page
    {
        ///*******************************************************************************************
        // * Constructor: LeapFrog (Default)
        // * 
        // * Creates and Displays the Splash Screen when Game is first started
        // */
        public MainPage()
        {
            //Initialize Component and ensure not in fullscreen mode.
            this.InitializeComponent();

            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
            }

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
        }

        ///*******************************************************************************************
        // * Method: btnLaunchGame_Click
        // * 
        // * Navigates to the GameTableau (Playing area) when the "Launch Game" button is clicked.
        // */
        private void btnLaunchGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameTableau), null, new EntranceNavigationTransitionInfo());
        }
    }
}
