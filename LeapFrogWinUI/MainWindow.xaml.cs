using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;

using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;

//using Windows.Foundation;
//using Windows.Foundation.Collections;
using Windows.Graphics;
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
        public AppWindow myWindow = null;                      //Create a Window Object to work with

        private static String folderGameImages = "ms-appx://Assets//GameImages//";
        private string fileGameIcon = folderGameImages + "Cards.ico";

        //TitleBar Parameters
        private string myTitle = "LeapFrog";                      //Title for the Application/Window

        private Color ttlBackgroundColor = Colors.DarkSlateGray;
        private Color ttlForeGroundColor = Colors.White;

        private Color ttlButtonBackgroundColor = Colors.DarkBlue;
        private Color ttlButtonForegroundColor = Colors.White;

        public MainWindow()
        {
            this.InitializeComponent();

            //Set Parameters on the Window Object
            myWindow = this.AppWindow;

            myWindow.Title = myTitle;
            myWindow.SetIcon(fileGameIcon);

            SizeInt32 myClientSize = myWindow.ClientSize;
            SizeInt32 myCurrentSize = myWindow.Size;

            //myWindow.MoveAndResize(new RectInt32(500, 800, 100, 100));

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

                myTitleBar.ButtonHoverBackgroundColor = Colors.LightBlue;
                myTitleBar.ButtonHoverForegroundColor = Colors.WhiteSmoke;

                myTitleBar.ButtonPressedBackgroundColor = Colors.AliceBlue;
                myTitleBar.ButtonPressedForegroundColor = Colors.WhiteSmoke;

                return true;
            }

            return false;
        }

    }
}
