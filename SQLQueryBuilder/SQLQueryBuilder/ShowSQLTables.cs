/***************************************************************************************************
 * ShowSQLTables.cs
 * 
 * Subform to Display the Loaded Tables.
 * 
 * Created:            23-October-2015
 * Created By;         cjpilgirm
 *  
 * Last Maintained:    30-October-2015
 * Last Maintained By: cjpilgrim
 * 
 * @Copyright (c) 2015 Charles J. Pilgrim
 * All Rights Reserved.
 * ************************************************************************************************/

/***************************************************************************************************
 * System Class/Library Declarations
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/***************************************************************************************************
 * Namespace Definition
 */
namespace SQLQueryBuilder
{
    public partial class ShowSQLTables : Form
    {
        private SQLTables myShowSQLTables;                        //My Local  Copy of SQL Table List
        private SQLTable mySQLTable;                            //My Local SQL Table Working Storage

        private int currentColumn = 0;                                   //Currently Selected Column
        private int currentJoin = 0;                                       //Currently Selected Join
        private int currentTable = 0;                                     //Currently Selected Table
        private int currentJoinDefinition = 0;  //Number of Join Definitions for Selected Join Table

        private int maxColumns = 0;                             //Number of Columns in current Table
        private int maxJoins = 0;                         //Number of Defined Joins in Current Table
        private int maxJoinDefinitions = 0;                             //Number of Join Definitions
        private int maxTables = 0;                           //Number of Tables Loaded from XML File

        public ShowSQLTables(SQLTables aSQLTablesObject)
        {
            InitializeComponent();
            myShowSQLTables = aSQLTablesObject;

            maxTables = myShowSQLTables.getNumberOfTables();       //Get the Number of Tables Loaded

            currentTable = 0;                                   //Initialize the Current Table Value
            currentColumn = 0;                                 //Initialize the Current column Value

            mySQLTable = myShowSQLTables.getTable(currentTable);     //Get a SQL Table from the List

            spinTableNumber.Minimum = 1;                                   //Set Minimum Table Count
            spinTableNumber.Maximum = maxTables;                           //Set Maximum Table Count

            spinColumnNumber.Minimum = 1;           //Initialize the Column Spinner Minimum Value...
            maxColumns = mySQLTable.columnCount();                         //Store Number of Columns
            spinColumnNumber.Maximum = maxColumns;               //And Set the Maximum spinner Value

            spinJoinNumber.Minimum = 1;               //Initialize the Join Spinner Minimum Value...
            maxJoins = mySQLTable.joinCount();                           //Store the Number of Joins
            spinJoinNumber.Maximum = maxJoins;                   //And set the Maximum Spinner Value

            showDetails(mySQLTable);                           //Show the Details of Table(s) Loaded
        }

        /*******************************************************************************************
         *  showColumn
         *  Fills the various entries in the Main Window for the Selected Column
         ******************************************************************************************/
        private void showColumn(SQLTable myTable, int aColumnNum)
        {
            txtNumberOfColumns.Text = maxColumns.ToString();

            txtColumnName.Text = myTable.getColumnName(aColumnNum);
            txtColumnDataType.Text = myTable.getColumnDataType(aColumnNum);
            txtColumnIsNullable.Text = (myTable.getColumnIsNull(aColumnNum)).ToString();
        }

        /*******************************************************************************************
        *  showDetails
        *  Fills the various entries in the Main Window.
        * *****************************************************************************************/
        private void showDetails(SQLTable myTable)
        {
            showTable(myTable);                          //Show Table Attributes for Specified Table
            showColumn(myTable, currentColumn);                     //show Current Column Attributes
            showJoinTable(myTable, currentJoin);                      //Show current Join Attributes
        }

        /*******************************************************************************************
         *  showJoinDefinition
         *  Fills the various entries in the Main Window for the Selected Column
         ******************************************************************************************/
        private void showJoinDefinition(TableJoin myTableJoin, int joinDefinitionNum)
        {
            JoinDefinition myJoinDefinition = myTableJoin.getJoinDefinition(joinDefinitionNum);
            txtNumberOfDefinitions.Text = maxJoinDefinitions.ToString();

            txtLeftsideColumnName.Text = myJoinDefinition.getLeftsideColumnName();
            txtLeftsideModifier.Text = myJoinDefinition.getLeftsideModifier();
            txtJoinRelation.Text = myJoinDefinition.getJoinRelation();
            txtRightsideColumnName.Text = myJoinDefinition.getRightsideColumnName();
            txtRightsideModifier.Text = myJoinDefinition.getRightsideModifier();
        }
        /*******************************************************************************************
         *  showJoin
         *  Fills the various entries in the Main Window for the Selected Column
         ******************************************************************************************/
        private void showJoinTable(SQLTable myTable, int aJoinNum)
        {
            TableJoin currentJoinTable = myTable.getTableJoin(aJoinNum);
            txtNumberOfJoins.Text = maxJoins.ToString();

            txtRightsideDatabase.Text = currentJoinTable.getRightsideDatabase();
            txtRightsideTableName.Text = currentJoinTable.getRightsideTableName();

            showJoinDefinition(currentJoinTable, currentJoinDefinition);
        }

        /*******************************************************************************************
        *  showTable
        *  Fills the various entries in the Main Window for the Table Properties.
        * ****************************************************************************************/
        private void showTable(SQLTable myTable)
        {
            txtNumberOfTables.Text = maxTables.ToString();

            txtDatabaseName.Text = myTable.getDatabaseName();
            txtTableName.Text = myTable.getTableName();
            txtTableAbbreviation.Text = myTable.getTableAbbreviation();
        }

        /*******************************************************************************************
         *******************************************************************************************
         ************************* Form Objects Control Procedures  ********************************
         *******************************************************************************************
         ******************************************************************************************/

        /*******************************************************************************************
        *  btnOK_Click
        *  Closes (Hides) the Current Form.
        * ****************************************************************************************/
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /*******************************************************************************************
        *  spinColumnNumber_ValueChanged
        *  Updates the Displayed Table Information when Spinner Value Changes.
        * *****************************************************************************************/
        private void spinColumnNumber_ValueChanged(object sender, EventArgs e)
        {
            currentColumn = (int)spinColumnNumber.Value - 1;    //Reset the Current column Number...
            mySQLTable = myShowSQLTables.getTable(currentTable);           //Get the Specified Table
            showColumn(mySQLTable, currentColumn);                   //And Update the Column Display
        }

        /*******************************************************************************************
        *  spinJoinDefinitions_ValueChanged
        *  Updates the Displayed Table Information when Spinner Value Changes.
        * ****************************************************************************************/
        private void spinJoinDefinitions_ValueChanged(object sender, EventArgs e)
        {
            currentJoinDefinition = (int)spinJoinDefinitions.Value - 1;
            showJoinDefinition(mySQLTable.getTableJoin(currentJoin), currentJoinDefinition);
        }

        /*******************************************************************************************
        *  spinJoinNumber_ValueChanged
        *  Updates the Displayed Table Information when Spinner Value Changes.
        * ****************************************************************************************/
        private void spinJoinNumber_ValueChanged(object sender, EventArgs e)
        {
            currentJoin = (int)spinJoinNumber.Value - 1;          //Reset the Current Join Number...
            showJoinTable(mySQLTable, currentJoin);                    //And Update the Join Display

            //Update the Join Definitions Entries
            currentJoinDefinition = 0;
            spinJoinDefinitions.Minimum = currentJoinDefinition + 1;
            spinJoinDefinitions.Value = spinJoinDefinitions.Minimum;
            maxJoinDefinitions = mySQLTable.getTableJoin(currentJoin).getNumberOfJoinDefinitions();
            spinJoinDefinitions.Maximum = maxJoinDefinitions;

            showJoinDefinition(mySQLTable.getTableJoin(currentJoin), currentJoinDefinition);
        }

        /*******************************************************************************************
        *  spinTableNumber_ValueChanged
        *  Updates the Displayed Table Information when Spinner Value Changes.
        * ****************************************************************************************/
        private void spinTableNumber_ValueChanged(object sender, EventArgs e)
        {
            currentTable = (int)spinTableNumber.Value - 1;          //Reset the Current Table Number
            currentColumn = 0;                         //Initialize Column counter for Current Table
            currentJoin = 0;                         //Initialize the Join counter for Current Table

            mySQLTable = myShowSQLTables.getTable(currentTable);           //Get the Specified Table

            //Reset the Column Number Spinner Values
            spinColumnNumber.Minimum = currentColumn + 1;   //Set the Column Number Minimum Value...
            spinColumnNumber.Value = spinColumnNumber.Minimum;       //Initialize the Column spinner
            maxColumns = mySQLTable.columnCount();                  //Store the Number of Columns...
            spinColumnNumber.Maximum = maxColumns;               //And set the Maximum Spinner Value

            //Reset the Join Number Spinner Values
            spinJoinNumber.Minimum = currentJoin + 1;         //Set the Join Number minimum value...
            spinJoinNumber.Value = spinJoinNumber.Minimum;             //Initialize the Join Spinner
            maxJoins = mySQLTable.joinCount();                        //Store the Number of Joins...
            spinJoinNumber.Maximum = maxJoins;                 //And set the Maximum Number of Joins

            //Reset the Join Definition Spinner Values
            spinJoinDefinitions.Minimum = currentJoinDefinition + 1;
            spinJoinDefinitions.Value = spinJoinDefinitions.Minimum;
            maxJoinDefinitions = mySQLTable.getTableJoin(currentJoin).getNumberOfJoinDefinitions();
            spinJoinDefinitions.Maximum = maxJoinDefinitions;

            showTable(mySQLTable);                                     //Update the Table display...
            showColumn(mySQLTable, currentColumn);                          //And the Column Display
            showJoinTable(mySQLTable, currentJoin);                    //And Update the Join Display
        }
    }
}
