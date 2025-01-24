using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

using System;
using System.Threading.Tasks;

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
