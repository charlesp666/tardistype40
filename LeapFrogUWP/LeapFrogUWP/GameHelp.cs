/********************************************************************************
 * Class: frmGameHelp.cs
 * 
 * "Help/Help" form for game of "LeapFrog." 
 * 
 * Author:              Charles J Pilgrim
 * Created:             26-January-2016
 * 
 * Last Maintained By:  Charles J Pilgrim
 * Last Maintained:     26-January-2016
 * 
 * @Copyright (c) 2013-2016 Charles J. Pilgrim
 * All Rights Reserved.
 */

 /******************************************************************************
  * System Class/Library Declarations
  */
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

/******************************************************************************
 * Namespace Definition
 */
namespace LeapFrog
{
    public partial class frmGameHelp : Form
    {
        /*******************************************************************************************
        * Class Variables and Constants
        */

        /*******************************************************************************************
         * Constructor: frmGameHelp (Default)
         * 
         * Creates and Displays the form containing instructions to play the game
         */
        public frmGameHelp(GameInformation localGameInfo)
        {
            InitializeComponent();

            this.Text = "Help: " + localGameInfo.getNameOfGame();            //Update the Form Title
            txtHelpInstructions.Text = localGameInfo.getHelpText();     //Load the Game Instructions
        }

        /*******************************************************************************************
         *******************************************************************************************
         ***********                         EVENT HANDLERS                              ***********
         *******************************************************************************************
         ******************************************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
