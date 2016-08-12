/***************************************************************************************************
 * Class: GameAbout.cs
 * 
 * "Help/About" form for game of "LeapFrog." 
 * 
 * Author:              Charles J Pilgrim
 * Created:             22-January-2016
 * 
 * Last Maintained By:  Charles J Pilgrim
 * Last Maintained:     26-January-2016
 * 
 * @Copyright (c) 2013-2016 Charles J. Pilgrim
 * All Rights Reserved.
 */

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Windows.Forms;

/***************************************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    public partial class frmGameAbout : Form
    {
        /*******************************************************************************************
        * Class Variables and Constants
        */
        private int objectMargin = 5;       //Space to use to compute Margins between window objects

        /*******************************************************************************************
         * Constructor: frmGameAbout (Default)
         * 
         * Creates and Displays the form that displays general information about the game
         */
        public frmGameAbout(GameInformation localGameInfo)
        {
            InitializeComponent();

            this.Text = "About " + localGameInfo.getNameOfGame(); //Add Name of Game to Window title
            lblNameOfGame.Text = localGameInfo.getNameOfGame();       //Put Name of Game into Dialog
            lblCopyright.Text = localGameInfo.getCopyrightNotice(); //Put Copyright Notice in Dialog
            lblRights.Text = localGameInfo.getRightsNotice();                    //Add Rights Notice

            this.Width = findWindowWidth();
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         EVENT HANDLERS                              ***********
         *******************************************************************************************
         ******************************************************************************************/
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                  Other Functions and Procedures                     ***********
         *******************************************************************************************
         ******************************************************************************************/

        /*******************************************************************************************
        * Function: findWindowWidth
        * Finds the width of the Window to dynamical adjust window width to ensure all objects are
        * displayed.
        */
        private int findWindowWidth()
        {
            int windowWidth = picFroggy.Location.X;
            windowWidth += lblNameOfGame.Location.X;

            int maxWidth = lblNameOfGame.Width;
            if(lblCopyright.Width > maxWidth)
            {
                maxWidth = lblCopyright.Width;
            }
            if(lblRights.Width > maxWidth)
            {
                maxWidth = lblRights.Width;
            }

            maxWidth += (3 * objectMargin);
            windowWidth += maxWidth;

            return (windowWidth);
        }
    }
}
