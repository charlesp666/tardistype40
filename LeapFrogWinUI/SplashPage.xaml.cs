/***************************************************************************************************
 * Page Class: MainPage
 * 
 * Main or "Splash" Page definition for launching of game.
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */
using Windows.UI.ViewManagement; //For View Management

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Animation;

using System.Reflection;
using Windows.UI.WebUI; //To load text file instructions


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        public SplashPage()
        {
            this.InitializeComponent();

            //var view = ApplicationView.GetForCurrentView();
            //if (view.IsFullScreenMode)
            //{
            //    view.ExitFullScreenMode();
            //}

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
            //Window.Current.Activate();
        }

        ///*******************************************************************************************
        // * Method: btnLaunchGame_Click
        // * 
        // * Navigates to the GameTableau (Playing area) when the "Launch Game" button is clicked.
        // */
        private void btnLaunchGame_Click(object sender, RoutedEventArgs e)
        {
            GameTableau myGameTableau = new GameTableau();
            Frame.Navigate(typeof(GameTableau), null, new EntranceNavigationTransitionInfo());
        }
    }
}
