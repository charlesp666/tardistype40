using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;

//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;


//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiPlayApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaPlayer myMediaPlayer = new MediaPlayer();
        private Uri soundShuffling = new Uri("ms-appx:///Assets//Sounds/ShufflingCards.mp3");

        private string dialogTitle = "Playing Around...";

        private static String keyName = "SOFTWARE\\Microsoft\\IdentityStore\\LogonCache";         //Name of App and Registry Key
        private static RegistryKey playerBaseKey = Registry.LocalMachine; //Base Key Game Information
        //private static RegistryKey playerBaseKey = Registry.CurrentUser; //Base Key Game Information
        private RegistryKey playerSubKey;
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = this;

            displayMessage("Initialization Complete.");
        }

        // MainPage.xaml.cs

        /*******************************************************************************************
         * Method: displayMessage
         * Displays the informational Message passed as parameter.
         */
        private async Task displayMessage(String theMessage)
        {
            ContentDialog myMessage = new ContentDialog();

            myMessage.Title = dialogTitle;
            myMessage.Content = theMessage;
            myMessage.PrimaryButtonText = "OK";

            myMessage.XamlRoot = this.XamlRoot;

            await myMessage.ShowAsync();
        }

        private void DisplayContentDialog_Click(object sender, RoutedEventArgs e)
        {
            displayMessage("Message from Button Click...");
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }

        private void LoadPlayer_Click(object sender, RoutedEventArgs e)
        {
            loadPlayerStats();
        }

        /*******************************************************************************************
        * Method: loadPlayerStats
        * Loads the Player Stats from the Registry for the Current User.
        */
        private void loadPlayerStats()
        {
            //string myName = ReadRegistryValue(string keyPath, string valueName);

            playerSubKey = playerBaseKey.OpenSubKey(keyName, true); //Attempt to Open the Sub Key...
            if (playerSubKey == null)                                       //If no Sub Key found...
            {
                playerSubKey = playerBaseKey.CreateSubKey(keyName);          //Create the Sub Key...
                //writePlayerStats();                                        //And Save Default Values
            }
            //else                                    //Otherwise, load the Stats from the Registry...
            //{
            //    setGamesPlayed((int)playerSubKey.GetValue("GamesPlayed"));
            //    setGameWinnings((int)playerSubKey.GetValue("Winnings"));
            //    setCountMoves((int)playerSubKey.GetValue("Moves"));

            //    setTimePlayed(convertRegistryTimePlayed());
            //}
        }

        //public static string ReadRegistryValue(string keyPath, string valueName)
        //{
        //    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
        //    {
        //        return key?.GetValue(valueName)?.ToString() ?? "Value not found";
        //    }
        //}
        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            var currentValue = myButton.Content;

            if ((string)currentValue == "Clicked")
            {
                myButton.Content = "Click Me";
            }
            else
            {
                myButton.Content = "Clicked";
            }
        }

        private void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            playSound(soundShuffling);
        }

        private void playSound(Uri soundFile)
        {
            myMediaPlayer.Source = MediaSource.CreateFromUri(soundFile);
            myMediaPlayer.Play();
        }
    }
}
