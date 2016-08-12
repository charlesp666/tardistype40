/***************************************************************************************************
 * SQLQueryBuilder.cs
 * 
 * Class Definition for the SQL Tables Class. This stores a list of SQL Tables read from an XML
 * file and all the attributes of a SQL Table that might be used to build a SQL Query.
 * 
 * Created:            09-October-2015
 * Created By;         cjpilgirm
 *  
 * Last Maintained:    01-January-2016
 * Last Maintained By: cjpilgrim
 * 
 * @Copyright (c) 2015, 2016 Charles J. Pilgrim
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
using System.Xml;

/***************************************************************************************************
 * Namespace Definition
 */
namespace SQLQueryBuilder
{
    public partial class windowMain : Form
    {
        //Assign Constants for Opening the XML File
        private String xmlDefaultExt = "xml";
        private String xmlFilePath = @"..\Data\CaseDetailsColumnList.xml";
        private String xmlFileMask = "XML Files (*.xml;*.xsl;*.xslt;*.xsd;*.dtd)|*.xml;*.xsl;*.xslt;*.xsd;*.dtd";

        //Assign Constants for Saving SQl Query Text
        private String sqlDefaultExt = ".sql";           //Default Extension to use when saving file
        private String sqlFilePath = @"..\Data\";             //Default path to use when saving file
        private String sqlBaseFileName = "SQLBuilderQuery-";      //Base Filename for SQL Query File
        private int sqlFileCounter = 0;           //File Counter to use as Suffix for SQL Query File

        private String sqlFileName;                           //Full SQL Filename to store SQL Query

        //Create local Objects and Variables to build SQL Queries from Loaded Object
        private SQLTables mySQLTables;                                           //My SQL Table List
        private SQLQuery mySQLQuery;                                           //My SQL Query object

        /*******************************************************************************************
         *  windowMain
         *  Method to Initialize the Main Window
         ******************************************************************************************/
        public windowMain()
        {
            InitializeComponent();

            updateStatusMessage("Initializing...");
            updateStatusMessage("Open XML File to Load SQL Table Definitions...");
        }

        /*******************************************************************************************
        *  buildQueryObject
        *  Builds the Query Object from which SQL Queries are built
        * ****************************************************************************************/
        private void buildQueryObject()
        {
            mySQLQuery= new SQLQuery(mySQLTables);
            mySQLQuery.setSQLEngine(mySQLQuery.sqlEngineOracle);
        }

        /*******************************************************************************************
        *  buildColumnCheckList
        *  Build the Check List of Columns for Currently Selected table
        * ****************************************************************************************/
        private void buildColumnCheckList(SQLQuery.QueryTable myQueryTable)
        {
            chklistQueryColumns.Items.Clear();

            //Add the columnns for the currently selected table
            for (int currentColumn = 0; currentColumn < myQueryTable.columnCount(); currentColumn++)
            {
                chklistQueryColumns.Items.Add(myQueryTable.getQueryColumn(currentColumn).getColumnName());
            }

            chklistQueryColumns.CheckOnClick = true;
            chklistQueryColumns.ScrollAlwaysVisible = true;
            chklistQueryColumns.ThreeDCheckBoxes = true;
        }

        /*******************************************************************************************
        *  buildSQLFileName
        *  Builds the SQL File Name
        * ****************************************************************************************/
        private void buildSQLFileName()
        {
            sqlFileName = sqlBaseFileName + (sqlFileCounter++).ToString() + sqlDefaultExt;
        }

        /*******************************************************************************************
        *  buildTableCheckList
        *  Build the Check List of Tables
        * ****************************************************************************************/
        private void buildTableCheckList()
        {
            for (int currentTable = 0; currentTable < mySQLQuery.getNumberOfTables() ; currentTable++)
            {
                SQLQuery.QueryTable currentSQLTable = mySQLQuery.getQueryTable(currentTable);
                String fqTableName = currentSQLTable.getDatabaseName()
                                   + "."
                                   + currentSQLTable.getTableName();
                chklistQueryTables.Items.Add(fqTableName);
            }

            chklistQueryTables.CheckOnClick = true;
            chklistQueryTables.ScrollAlwaysVisible = true;
            chklistQueryTables.ThreeDCheckBoxes = true;
        }

        /*******************************************************************************************
         * updateStatusMessage
         * Updates the Displayed Status Message; calling procedure without parameter clears the
         * message box.
         ******************************************************************************************/
        private void updateStatusMessage(String theMessage = "")
        {
            txtStatus.Text = theMessage;
        }

        /*******************************************************************************************
         * updateSQLQuery
         * Calls the Objects and methods to update the Query in the Query text box.
         ******************************************************************************************/
        private void updateSQLQuery()
        {
            txtSQLQuery.Text = mySQLQuery.buildSQLQuery();           //Update the SQL Query text-box
            grpSQLQuery.Text = sqlFileName;       //Change the title of the box around the SQL Query
        }

        /*******************************************************************************************
         *******************************************************************************************
         ************************* Form Objects Control Procedures  ********************************
         *******************************************************************************************
         ******************************************************************************************/

        private void Main_Load(object sender, EventArgs e)
        {
            btnCopyQuery.Enabled = false;
            btnNewQuery.Enabled = false;                 //Need to Load XML File before New Query...
        }

        /*******************************************************************************************
        *  btnCopyQuery_Click
        *  Copy the Newly Built Query.
        * ****************************************************************************************/
        private void btnCopyQuery_Click(object sender, EventArgs e)
        {
            txtSQLQuery.SelectAll();                      //Select the current query in text-box...
            txtSQLQuery.Refresh();                    //Refresh the display to confirm selection...
            txtSQLQuery.Copy();                                        //And copy the selected text
        }

        /*******************************************************************************************
        *  btnNewQuery_Click
        *  Sets Up a new Query.
        * ****************************************************************************************/
        private void btnNewQuery_Click(object sender, EventArgs e)
        {
            buildQueryObject();                         //Create the Query Object for Current Query
            buildTableCheckList();               //Build the Table Check List from the Query Object

            //Enable Objects that are dependent on Query Object
            btnCopyQuery.Enabled = true;

            buildSQLFileName();                                                 //Build SQL Filename
            updateSQLQuery();                      //Update the SQL Query text in the Query Text-Box

            updateStatusMessage("Select Table(s) and Columns to Include in Query...");
        }

        /*******************************************************************************************
        *  chklistQueryColumns_SelectedIndexChanged
        *  Set the flag of the selected column to indicate it is part of the query
        * ****************************************************************************************/
        private void chklistQueryColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tableNumber = -1;
            int columnNumber = -1;

            tableNumber = mySQLQuery.findIndex(chklistQueryTables.SelectedItem.ToString());
            SQLQuery.QueryTable myQueryTable = mySQLQuery.getQueryTable(tableNumber);

            columnNumber = myQueryTable.findColumnIndex(chklistQueryColumns.SelectedItem.ToString());
            if (chklistQueryColumns.GetItemCheckState(columnNumber) == CheckState.Checked)
            {
                myQueryTable.getQueryColumn(columnNumber).setSelected(true);
            }
            else
            {
                myQueryTable.getQueryColumn(columnNumber).setSelected(false);
            }

            updateSQLQuery();                      //Update the SQL Query text in the Query Text-Box
        }

        /*******************************************************************************************
        *  chklistSQLTables_SelectedIndexChanged
        *  set the flag for the Selected table to indicate it is part of the query.
        * ****************************************************************************************/
        private void chklistSQLTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tableNumber = -1;
            tableNumber = mySQLQuery.findIndex(chklistQueryTables.SelectedItem.ToString());

            if (chklistQueryTables.GetItemChecked(chklistQueryTables.SelectedIndex))
            {
                if (chklistQueryTables.GetItemCheckState(tableNumber) == CheckState.Checked)
                {
                    mySQLQuery.getQueryTable(tableNumber).setTableSelected(true);
                    buildColumnCheckList(mySQLQuery.getQueryTable(tableNumber));
                }
                else
                {
                    mySQLQuery.getQueryTable(tableNumber).setTableSelected(false);
                }
            }

            updateSQLQuery();                      //Update the SQL Query text in the Query Text-Box
        }

        /*******************************************************************************************
        *  exitToolStripMenuItem_Click
        *  Responds to "File/Exit" menu click event.
        * ****************************************************************************************/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*******************************************************************************************
        *  openXMLFileToolStripMenuItem_Click
        *  Responds to "File/Open XML File" menu click event.
        * ****************************************************************************************/
        private void openXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateStatusMessage("Opening File Dialog...");

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"..";

            openFileDialog1.Title = "Browse XML Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = xmlDefaultExt;
            openFileDialog1.Filter = xmlFileMask;

            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = false;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                xmlFilePath = openFileDialog1.FileName;

                //Create SQL Tables Object from Specified File
                mySQLTables = new SQLTables(xmlFilePath);

                if (mySQLTables.isXMLFileLoaded())
                {
                    updateStatusMessage("XML File Loaded; Click 'New Query' to Begin Building Query...");
                    txtNumberOfTablesLoaded.Text = mySQLTables.getNumberOfTables().ToString();

                    btnNewQuery.Enabled = true;                  //XML File loaded, enable New Query
                }
                else
                {
                    MessageBox.Show("XML File not Loaded!", "XML Error", MessageBoxButtons.OK);
                }
            } 

        }

        /*******************************************************************************************
        *  showTablesToolStripMenuItem_Click
        *  Show Form that Displays the SQL Table Objects.
        * ****************************************************************************************/
        private void showTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSQLTables showSQLTableStructures = new ShowSQLTables(mySQLTables);

            showSQLTableStructures.Show();
        }
    }
}
