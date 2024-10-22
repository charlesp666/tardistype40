using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
using System.ComponentModel;
using Windows.Media.Playback;
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

        public Page2()
        {
            this.InitializeComponent();

            myCurrentActivity = new CurrentActivity();
            this.DataContext = myCurrentActivity;

            myCurrentActivity.CurrentActivityText = "Reached Page 2..." ;
        }

        // Page2.xaml.cs

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SplashPage));
        }

        private void UpdateTextButton_Click(object sender, RoutedEventArgs e)
        {
            //ViewModel.Text = "New text!";
            myCurrentActivity.CurrentActivityText = UpdatedText.Text;
        }
    }
}
