/***************************************************************************************************
 * SQLQueryBuilder.cs
 * 
 * Class Definition for the SQL Tables Class. This stores a list of SQL Tables read from an XML
 * file and all the attributes of a SQL Table that might be used to build a SQL Query.
 * 
 * Created:            09-October-2015
 * Created By;         cjpilgirm
 *  
 * Last Maintained:    25-October-2015
 * Last Maintained By: cjpilgrim
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

        //Create local Objects and Variables to build SQL Queries from Loaded Object
        private SQLTables mySQLTables;                                           //My SQL Table List

        /*******************************************************************************************
         *  windowMain
         *  Method to Initialize the Main Window
         ******************************************************************************************/
        public windowMain()
        {
            InitializeComponent();

            txtStatus.Text = "Initializing...";

            txtXMLFilePath.Text = xmlFilePath;

            txtStatus.Text = "Ready...";
        }


        /*******************************************************************************************
         *******************************************************************************************
         ************************* Form Objects Control Procedures  ********************************
         *******************************************************************************************
         ******************************************************************************************/

        private void Main_Load(object sender, EventArgs e)
        {

            //            sqlTablesList = new XmlDocument();
            //            sqlTablesList.Load(xmlFilePath);

            //            sqlTables = sqlTablesList.DocumentElement;

            //            showDetails(sqlTablesList);
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
            txtStatus.Text = "Opening File Dialog...";

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
                txtStatus.Text = "Found XML File: " + txtXMLFilePath.Text + "...";
                txtXMLFilePath.Text = openFileDialog1.FileName;

                xmlFilePath = txtXMLFilePath.Text;

                //Create SQL Tables Object from Specified File
                mySQLTables = new SQLTables(xmlFilePath);

                if (mySQLTables.isXMLFileLoaded())
                {
                    txtStatus.Text = "Successfully Loaded XML File: " + xmlFilePath + "...";
                    txtNumberOfTablesLoaded.Text = mySQLTables.getNumberOfTables().ToString();
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
