using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
//using Windows.Media.Playback;
using Windows.Storage;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiPlayApp
{
    public class CurrentActivity : INotifyPropertyChanged
    {
        private string currentActivity;
        public string CurrentActivityText
        {
            get => currentActivity;
            set
            {
                if (currentActivity != value)
                {
                    currentActivity = value;
                    OnPropertyChanged(nameof(CurrentActivityText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public CurrentActivity myCurrentActivity { get; set; }

        private static String folderGameData = "ms-appx:///Assets//Data//";
        private string fileInstructions = folderGameData + "GameInstructions.txt";

        private String helpText = "The quick brown fox jumped over the lazy god."; //null;
        private Player myAvatar = new Player();

        public Page2()
        {
            this.InitializeComponent();

            //loadHelpText();

            myCurrentActivity = new CurrentActivity();
            this.DataContext = myCurrentActivity;

            myCurrentActivity.CurrentActivityText = "Reached Page 2..." ;

            //Player myAvatar = new Player();

            //myAvatar.displayPlayerStats();
        }

        /*******************************************************************************************
         * Event Handler: Help
         * Displays the Help/About dialog
         */
        private async void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            var gameInstructions = new DisplayInstructions(helpText);
            gameInstructions.XamlRoot = this.XamlRoot;

            await gameInstructions.ShowAsync();
        }

        /*******************************************************************************************
         * Event Handler: Player Stats
         * Displays Player Statistics dialog
         */
        private async void btnPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            var playerStats = new DisplayPlayerStats(myAvatar);
            playerStats.XamlRoot = this.XamlRoot;

            await playerStats.ShowAsync();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SplashPage));
        }

        private void UpdateTextButton_Click(object sender, RoutedEventArgs e)
        {
            myCurrentActivity.CurrentActivityText = UpdatedText.Text;
        }

        /*******************************************************************************************
        * Method: loadHelpText
        * Loads the Intstructions on How to Play the Game from Text file in Assets folder.
        */
        private async Task loadHelpText()
        {
            var HelpFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(fileInstructions));

            helpText = await FileIO.ReadTextAsync(HelpFile);
        }
    }
}
