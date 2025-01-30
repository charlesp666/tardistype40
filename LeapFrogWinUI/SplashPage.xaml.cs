/***************************************************************************************************
* Page Class: SplashPage
* 
* Entry or "Splash" Page definition for launching of game.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        private static int linkDelayMS = 150;         //Action display delay so user can see changes

        public SplashPage()
        {
            this.InitializeComponent();
            this.Loaded += SplashPage_Loaded;

            // Build the Game Information object
            GameInformation myGameInfo = new GameInformation();

            // Populate the Splash page components
            lblGameTitle.Text = myGameInfo.getNameOfGame();                   //Name of appliication
            txtSubTitle.Text = myGameInfo.getGameSubTitle();                     //Subtitle for Game
            lblCopyright.Text = myGameInfo.getCopyrightNotice();                  //Copyright Notice
            txtRights.Text = myGameInfo.getRightsNotice();                           //Rights Notice
            txtPublisher.Text = "Pubished by: " + myGameInfo.getPublisher();     //Name of Publisher
            txtVersion.Text = "Version: " + myGameInfo.getVersion();           //Game Version Number

            picGameImage.Source = myGameInfo.getGameImage();                    //Get the Game Image
        }

        /*******************************************************************************************
        /* Method: SplashPage_Loaded
        /* 
        /* Handles the SplashPage Loaded Event.
        /*/
        private void SplashPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Load MainPage asynchronously
            LoadMainPageAsync();
        }

        /*******************************************************************************************
        /* Method: LoadMainPageAsync
        /* 
        /* Displays the SplashPage for awhile then Navigates to GameTableau.
        /*/
        private async Task LoadMainPageAsync()
        {
            await UpdateProgressBarValue();

            //UnhideMainWindow();

            Frame.Navigate(typeof(GameTableau), null, new EntranceNavigationTransitionInfo());
        }

        /*******************************************************************************************
        /* Method: UpdateProgressBarValue
        /* 
        /* Updates the ProgressBar while the SplashPage is displayed.
        /*/
        private async Task UpdateProgressBarValue()
        {
            for (int i = 0; i < 100; i++)
            {
                linkToGameTableau.Value = i;

                await Task.Delay(linkDelayMS);
            }
        }
    }
}
