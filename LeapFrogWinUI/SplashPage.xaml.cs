/***************************************************************************************************
* Page Class: SplashPage
* 
* Entry or "Splash" Page definition for launching of game.
* 
* @Copyright (c) 2024 Charles J. Pilgrim
* All Rights Reserved.
*/

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Windows.Graphics;

using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        private static int linkDelayMS = 100;      //Action display delay so user can see changes

        public SplashPage()
        {
            this.InitializeComponent();
            ResizeAppWindow();

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

            //UpdateProgressBarValue();
        }

        /*******************************************************************************************
        /* Method: btnLaunchGame_Click
        /* 
        /* Navigates to the GameTableau (Playing area) when the "Launch Game" button is clicked.
        /*/
        private void btnLaunchGame_Click(object sender, RoutedEventArgs e)
        {
            GameTableau myGameTableau = new GameTableau();
            Frame.Navigate(typeof(GameTableau), null, new EntranceNavigationTransitionInfo());
        }

        /*******************************************************************************************
         * Method: delay
         * Pause Processing for specified number of milliseconds.
         */
        //private void delay(int milliSecondsToPauseFor)
        //{
        //    System.DateTime startInstant = System.DateTime.Now;
        //    System.DateTime thisInstant = startInstant;
        //    System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, milliSecondsToPauseFor);
        //    System.DateTime finalInstant = thisInstant.Add(duration);

        //    while (finalInstant >= thisInstant)
        //    {
        //        thisInstant = System.DateTime.Now;
        //    }
        //}

        /*******************************************************************************************
        /* Method: ResizeAppWindow
        /* 
        /* Resizes the AppWindow to the size of the page.
        /*/
        private void ResizeAppWindow()
        {
            var myWindow = (Application.Current as App)?.m_window as MainWindow; 
            var hwnd = WindowNative.GetWindowHandle(myWindow);
            var myWindowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(myWindowId);

            int pageWidth = (int)this.Width;
            int pageHeight = (int)this.Height;
            SizeInt32 newSize = new SizeInt32(pageWidth, pageHeight);
            appWindow.Resize(newSize);
        }

        //private void UpdateProgressBarValue()
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        linkToGameTableau.Value = i;

        //        delay(linkDelayMS);
        //    }
        //}
    }
}
