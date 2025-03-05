/***************************************************************************************************
 * Class: GameInformation
 * 
 * Class stores general information about the game such as the Name of the Game,
 * Copyright Data and Rights notices, etc. 
 * 
 * Author:              Charles J Pilgrim
 * Created:             25-January-2016
 * 
 * LastMaintainedBy:    Charles J Pilgrim
 * LastMaintained:      05-March-2025
 * 
 * Copyright (c) 2025 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Drawing;
using System.Reflection;

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
        private Assembly myAssembly = Assembly.GetExecutingAssembly();         //Get Assembly Object

        /*******************************************************************************************
         * Class Variables and Constants
         */
        private AssemblyName assemblyName;                                        //Name of Assembly

        private String companyName;                             //Name of Company Releasing the Game
        private String copyrightNotice;                             //Copyright Notice from Assemply
        private String gameVersion;                                         //Version Number of Game
        private String helpText;                                      //Text for the display of Help
        private String nameOfGame;                         //Storage for the Name of the Game Object
        private String subTitleOfGame;                                      //Sub-Title for the Game

        private Bitmap gameImage;             //"Froggy" graphic image representing the general game
        private Icon mainWindowIcon;                                   //Icon to Use for Main Window

        private Color colorBackground;             //Color to assign to the Background of game board
        private Color colorForeground;                //Color to assignt to Foreground of game board

        //Constants
        private const String copyrightOwner = "Charles J. Pilgrim";
        private const String rightsNotice = "All Rights Reserved";

        /*******************************************************************************************
         * Constructor: GameInformatio2025n (Default)
         * 
         * Creates and Displays the Splash Screen when Game is first started
         */
        public GameInformation()
        {
            assemblyName = myAssembly.GetName();                       //Get the AssemblyName Object

            nameOfGame = assemblyName.Name;                     //Set Value for the name of the game
            subTitleOfGame = extractSubTitle(); ;              //Set the value for the Game Subtitle
            gameVersion = buildFullVersionNumber();                    //Get the Full Version Number

            companyName = extractCompanyName();                 //Get the Company Name from Assembly

            copyrightNotice = extractCopyrightNotice() + " " + copyrightOwner;//Get Copyright notice

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
          * Method: buildFullVersionNumber
          * Builds and Returns the Full Version number ("Major.Minor.Build).
          */
        public String buildFullVersionNumber()
        {
            String thisVersion = "";
            Version myVersion = assemblyName.Version;

            string major = myVersion.Major.ToString();
            string minor = myVersion.Minor.ToString();
            string build = myVersion.Build.ToString();
            //string revision = myVersion.Revision.ToString();

            thisVersion = major + "." + minor + "." + build; // + "." + revision;

            return (thisVersion);
        }

        /*******************************************************************************************
          * Method: extractCompanyName
          * Extracts the Company Name from the Assembly Object
          */
        public String extractCompanyName()
        {
            var companyAttribute = myAssembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            return (companyAttribute?.Company ?? "No Company Information");
        }

        /*******************************************************************************************
          * Method: extractCopyrightNotice
          * Extracts the Copyright Notice from the Assembly Object
          */
        public String extractCopyrightNotice()
        {
            var copyrightAttribute = myAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>();

            return (copyrightAttribute?.Copyright ?? "No Copyright Notice");
        }

        /*******************************************************************************************
          * Method: extractSubTitle
          * Extracts the Subtitle (Assembly Description) from the Assembly Object.
          */
        public String extractSubTitle()
        {
            var descriptionAttribute = myAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>();

            return (descriptionAttribute?.Description ?? "No Description");
        }

        /*******************************************************************************************
          * Method: getBackgroundColor
          * Returns the Background Color for Tablean/Windows.
          */
        public Color getBackgroundColor()
        {
            return (colorBackground);
        }

        /*******************************************************************************************
          * Method: getCopyrightNotice
          * Returns the Copyright Notice Text.
          */
        public String getCompanyName()
        {
            return (companyName);
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
          * Method: getVersion
          * Returns the Version Number.
          */
        public String getVersion()
        {
            return (gameVersion);
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
