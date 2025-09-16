/***************************************************************************************************
* Page Class: SplashPage
* 
* Entry or "Splash" Page definition for launching of game.
* 
* @Copyright (c) 2025 Charles J. Pilgrim
* All Rights Reserved.
*/

//using Microsoft.UI;
//using Microsoft.UI.Windowing;
//using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Media.Animation;

//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Threading.Tasks;

//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.Graphics;
//using Windows.UI.WindowManagement;

//using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiPlayApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        private static int linkDelayMS = 50;      //Action display delay so user can see changes

        public SplashPage()
        {
            this.InitializeComponent();
            this.Loaded += SplashPage_Loaded;

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
        }

        /*******************************************************************************************
        /* Method: SplashPage_Loaded
        /* 
        /* Handles the SplashPage Loaded Event.
        /*/
        private async void SplashPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Simulate a long loading process on app startup
            //await Task.Delay(2000); // 2 seconds delay

            // Load MainPage asynchronously
            await LoadMainPageAsync();
        }

        /*******************************************************************************************
        /* Method: LoadMainPageAsync
        /* 
        /* Displays the SplashPage for awhile then Navigates to GameTableau.
        /*/
        private async Task LoadMainPageAsync()
        {
            // Simulate loading data or performing other asynchronous tasks
            //await Task.Run(() => {UpdateProgressBarValue();});
            await UpdateProgressBarValue();

            Frame.Navigate(typeof(MainPage), null, new EntranceNavigationTransitionInfo());

            //Frame.Navigate(typeof(GameTableau), null, new EntranceNavigationTransitionInfo());
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
