/***************************************************************************************************
 * Class: GameInformation
 * 
 * Class stores general information about the game such as the Name of the Game,
 * Copyright Data and Rights notices, etc. 
 * 
 * @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.IO;
//using System.Resources;                                     //To Pull Images from Assembly Resources
using Windows.ApplicationModel;
using Windows.UI;
//using Windows.UI.Xaml.Controls;                                            //For Image Data Datatype
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;                                      //For BitmapImage DataType

namespace LeapfrogUWP
{
    /***********************************************************************************************
     * Class: Game Information
     **********************************************************************************************/
    public class GameInformation
    {
        /*******************************************************************************************
         * Class Variables and Constants
         */
        private static String folderGameData = ".//Assets//Data//";
        private static String folderGameImages = "ms-appx:///Assets//GameImages//";

        private String helpText;                                      //Text for the display of Help
        private String nameOfGame;                         //Storage for the Name of the Game Object
        private String subTitleOfGame;                                      //Sub-Title for the Game
        private String gamePublisher;                                        //Publisher of the Game
        private String gameVersion;                                           //Version for the Game

        //private BitmapImage bmpNotPlayable = new BitmapImage(new System.Uri(folderGameImages + "NotPlayable.jpg"));

        private BitmapImage gameImage = new BitmapImage(new System.Uri(folderGameImages + "LeapFrog.jpg")); //"Froggy" image
        private BitmapImage mainWindowIcon = new BitmapImage(new System.Uri(folderGameImages + "CardsIcon.png")); //Cards Icon;

        private SolidColorBrush colorBackground;   //Color to assign to the Background of game board
        private SolidColorBrush colorForeground;      //Color to assignt to Foreground of game board

        //Constants
        private String copyrightNotice = "Copyright (c) 2024 Charles J. Pilgrim";
        private String rightsNotice = "All Rights Reserved";

        /*******************************************************************************************
         * Constructor: GameInformation (Default)
         * 
         * Pulls Game Information from the Manifest and stores in "Local" variables
         */
        public GameInformation()
        {
            //Get the Package Objects required to collect Game Information
            Package currentPackage = Package.Current;                          //Get Current Package
            PackageId currentPackageId = currentPackage.Id;                 //Package ID Information
            PackageVersion currentPackageVersion = currentPackageId.Version;

            nameOfGame = currentPackage.DisplayName;                              //Name of the Game
            //subTitleOfGame = currentPackage.Description;                 //Short Description of Game
            subTitleOfGame = "A Game of Solitaire";                    //Short Description of Game
            gamePublisher = currentPackage.PublisherDisplayName;           //Set Name of Publisher

            //Compose the full Version number from the Package Version properties.
            String versionMajor = currentPackageVersion.Major.ToString();         //Get Major Number
            String versionMinor = currentPackageVersion.Minor.ToString();         //Get Minor Number
            String versionBuild = currentPackageVersion.Build.ToString();         //Get Build Number
            String versionRevision = currentPackageVersion.Revision.ToString();//Get Revision Number

            gameVersion = versionMajor + "." + versionMinor + "." + versionBuild + "." + versionRevision;

            helpText = (new StreamReader(folderGameData + "GameInstructions.txt")).ReadToEnd();

            colorBackground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)); //Game board background
            colorForeground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));   //Game board foreground
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         CLASS METHODS                               ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
          * Method: getBackgroundColor
          * Returns Background color.
          */
        public SolidColorBrush getBackgroundColor()
        {
            return (colorBackground);
        }

        /*******************************************************************************************
          * Method: getCopyrightNotice
          * Returns the Copyright Notice Text.
          */
        public String getCopyrightNotice()
        {
            return (copyrightNotice);
        }

        /*******************************************************************************************
          * Method: getForegroundColor
          * Returns the Copyright Notice Text.
          */
        public SolidColorBrush getForegroundColor()
        {
            return (colorForeground);
        }

        /*******************************************************************************************
          * Method: getGameImage
          * Returns the Game Sub-title.
          */
        public BitmapImage getGameImage()
        {
            return (gameImage);
        }

        /*******************************************************************************************
          * Method: getGameSubTitle
          * Returns the Game Sub-title.
          */
        public String getGameSubTitle()
        {
            return (subTitleOfGame);
        }

        /*******************************************************************************************
          * Method: getHelpText
          * Returns the Help Text for the game.
          */
        public String getHelpText()
        {
            return (helpText);
        }

        /*******************************************************************************************
          * Method: getNameOfGame
          * Returns the Name of the Game.
          */
        public String getNameOfGame()
        {
            return (nameOfGame);
        }

        /*******************************************************************************************
          * Method: getPublisher
          * Returns the Rights Notice.
          */
        public String getPublisher()
        {
            return (gamePublisher);
        }

        /*******************************************************************************************
          * Method: getRightsNotice
          * Returns the Rights Notice.
          */
        public String getRightsNotice()
        {
            return (rightsNotice);
        }

        /*******************************************************************************************
          * Method: getVersion
          * Returns the Rights Notice.
          */
        public String getVersion()
        {
            return (gameVersion);
        }

        /*******************************************************************************************
          * Method: getWindowIcon
          * Returns the Icon for the Main Window.
          */
        public BitmapImage getWindowIcon()
        {
            return (mainWindowIcon);
        }
        #endregion
    }
}