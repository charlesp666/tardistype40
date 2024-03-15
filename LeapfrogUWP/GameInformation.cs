/***************************************************************************************************
 * Class: GameInformation
 * 
 * Class stores general information about the game such as the Name of the Game,
 * Copyright Data and Rights notices, etc. 
 * 
 * Author:           Charles J Pilgrim
 * Created:          11-March-2024
 * 
 * LastMaintained:   12-March-2024
 * LastMaintainedBy: cjpilgrim
 * 
* @Copyright (c) 2024 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Drawing;
using System.IO;
using System.Resources;                                     //To Pull Images from Assembly Resources
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;                                            //For Image Data Datatype
using Windows.UI.Xaml.Media.Imaging;                                      //For BitmapImage DataType

/***********************************
 * ****************************************************************
 * Namespace Definition
 */
namespace LeapFrogUWP
{
    /***********************************************************************************************
     * Class: Game Information
     **********************************************************************************************/
    public class GameInformation
    {
        /*******************************************************************************************
         * Class Variables and Constants
         */
        private ResourceLoader appResources = ResourceLoader.GetForCurrentView();

        private String helpText;                                      //Text for the display of Help
        private String nameOfGame;                         //Storage for the Name of the Game Object
        private String subTitleOfGame;                                      //Sub-Title for the Game
        private String gamePublisher;                                        //Publisher of the Game
        private String gameVersion;                                           //Version for the Game

        private BitmapImage gameImage = new BitmapImage(new Uri("./GameImages/LeapFrog.jpg", UriKind.Absolute)); //"Froggy" image
        //private BitmapIcon mainWindowIcon = "./GameImages/CardsIcon.png";          //Icon to Use for Main Window

        private Color colorBackground;             //Color to assign to the Background of game board
        private Color colorForeground;                //Color to assignt to Foreground of game board

        //Constants
        private String copyrightNotice = "Copyright (c) 2024 Charles J. Pilgrim";
        private String rightsNotice = "All Rights Reserved";

        /*******************************************************************************************
         * Constructor: GameInformation (Default)
         * 
         * Pulls Game Information from the Manifest and stores in "Local" variables
         */
        //public GameInformation(String aNameOfGame, String aSubTitle = null)
        public GameInformation()
        {
            nameOfGame = appResources.GetString("DisplayName");     //Set value for the name of game
            subTitleOfGame = appResources.GetString("Description");     //Set value for the Subtitle

            //Pull the Major, Minor and Build Values from the Manifest...
            var versionMajor = appResources.GetString("Major");        //Set value for Major Version
            var versionMinor = appResources.GetString("Minor");        //Set value for Minor Version
            var versionBuild = appResources.GetString("Build");        //Set value for Build Version

            gameVersion = versionMajor.ToString() + "." + versionMinor.ToString() + "." + versionBuild.ToString();

            gamePublisher = appResources.GetString("Publisher");           //Set value for Publisher

            //nameOfGame = aNameOfGame;                       //Set the value for the name of the game
            //subTitleOfGame = aSubTitle;                        //Set the value for the Game Subtitle

            //helpText = LeapFrog.Properties.Resources.GameInstructions;     //Store Help Instructions
            //gameImage = LeapFrog.Properties.Resources.LeapFrog;         //Get the Image for the game
            //try
            //{
            //using (var sr = new StreamReader("..\\Data\\GameInstructions.txt"))
            //{
            //helpText = sr.ReadToEndAsync();
            helpText = (new StreamReader("..\\Data\\GameInstructions.txt")).ReadToEnd();
                //}
            //}
            //catch (FileNotFoundException ex)
            //{
            //    ResultBlock.Text = ex.Message;
            //}

            colorBackground = Color.Blue;              //Set the value for the Game board background
            colorForeground = Color.White;             //Set the value for the Game board foreground

            //Get the Window Icon Image and Convert to Icon
            //BitmapImage tempImage = LeapFrog.Properties.Resources.CardsIcon;
            //mainWindowIcon.(new Uri("./GameImages/CardsIcon.png");
            //mainWindowIcon =Icon.FromHandle(tempImage.GetHicon());
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
        public Color getBackgroundColor()
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
        public Color getForegroundColor()
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
          * Method: getCopyrightNotice
          * Returns the Name of the Game.
          */
        public String getNameOfGame()
        {
            return (nameOfGame);
        }

        /*******************************************************************************************
          * Method: getRightsNotice
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
          * Method: getRightsNotice
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
        //public Icon getWindowIcon()
        //{
        //    return (mainWindowIcon);
        //}

        #endregion

    }
}
