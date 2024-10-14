using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LeapFrogWinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public AppWindow myWindow = null;

        //TitleBar Parameters
        private string myTitle = "LeapFrog";

        private Color ttlBackgroundColor = Colors.DarkSlateGray;
        private Color ttlForeGroundColor = Colors.White;

        private Color ttlButtonBackgroundColor = Colors.DarkBlue;
        private Color ttlButtonForegroundColor = Colors.White;

        public MainWindow()
        {
            this.InitializeComponent();

            myWindow = this.AppWindow;
            myWindow.Title = myTitle;

            setTitleBar();
        }

        /*******************************************************************************************
        /* Method: setTitleBar()
        /* 
        /* Sets the Parameters of the Application Title Bar.
        /*/
        private bool setTitleBar()
        {
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                AppWindowTitleBar myTitleBar = myWindow.TitleBar;     //TitleBar object to work with

                myTitleBar.BackgroundColor = ttlBackgroundColor; 
                myTitleBar.ForegroundColor = ttlForeGroundColor;

                myTitleBar.ButtonBackgroundColor = ttlButtonBackgroundColor;
                myTitleBar.ButtonForegroundColor = ttlButtonForegroundColor;

                return true;
            }

            return false;
        }

    }
}
