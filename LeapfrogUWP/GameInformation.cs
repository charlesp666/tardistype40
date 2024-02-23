/***************************************************************************************************
 * Class: GameInformation
 * 
 * Class stores general information about the game such as the Name of the Game,
 * Copyright Data and Rights notices, etc. 
 * 
 * Author:              Charles J Pilgrim
 * Created:             25-January-2016
 * 
 * @Copyright (c) 2013-2017 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Drawing;

/***************************************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    /***********************************************************************************************
     * Class: Game Information
     **********************************************************************************************/
    public class GameInformation
    {
        /*******************************************************************************************
         * Class Variables and Constants
         */
        private String helpText;                                      //Text for the display of Help
        private String nameOfGame;                         //Storage for the Name of the Game Object
        private String subTitleOfGame;                                      //Sub-Title for the Game

        private Bitmap gameImage;             //"Froggy" graphic image representing the general game
        private Icon mainWindowIcon;                                   //Icon to Use for Main Window

        private Color colorBackground;             //Color to assign to the Background of game board
        private Color colorForeground;                //Color to assignt to Foreground of game board

        //Constants
        private String copyrightNotice = "Copyright (c) 2024 Charles J. Pilgrim";
        private String rightsNotice = "All Rights Reserved";

        /*******************************************************************************************
         * Constructor: GameInformation (Default)
         * 
         * Creates and Displays the Splash Screen when Game is first started
         */
        public GameInformation(String aNameOfGame, String aSubTitle = null)
        {   
            nameOfGame = aNameOfGame;                       //Set the value for the name of the game
            subTitleOfGame = aSubTitle;                        //Set the value for the Game Subtitle

            helpText = LeapFrog.Properties.Resources.GameInstructions;     //Store Help Instructions
            gameImage = LeapFrog.Properties.Resources.LeapFrog;         //Get the Image for the game

            colorBackground = Color.Blue;              //Set the value for the Game board background
            colorForeground = Color.White;             //Set the value for the Game board foreground

            //Get the Window Icon Image and Convert to Icon
            Bitmap tempImage = LeapFrog.Properties.Resources.CardsIcon;
            mainWindowIcon =Icon.FromHandle(tempImage.GetHicon());
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         CLASS METHODS                               ***********
         *******************************************************************************************
         ******************************************************************************************/
        #region
        /*******************************************************************************************
          * Method: getBackgroundColor
          * Returns the Copyright Notice Text.
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
        public Bitmap getGameImage()
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
        public String getRightsNotice()
        {
            return (rightsNotice);
        }

        /*******************************************************************************************
          * Method: getWindowIcon
          * Returns the Icon for the Main Window.
          */
        public Icon getWindowIcon()
        {
            return (mainWindowIcon);
        }

        #endregion

    }
}
