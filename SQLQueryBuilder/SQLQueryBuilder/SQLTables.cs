/***************************************************************************************************
 * SQLTables.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/***************************************************************************************************
 * Namespace Definition
 */
namespace SQLQueryBuilder
{
    /***********************************************************************************************
     * Class: JoinDefinition
     **********************************************************************************************/
    public class JoinDefinition
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private String leftsideColumnName;                     //Name of Join Column from This Table
        private String leftsideModifier;                 //Function to Modify Column from this Table
        private String joinRelation;                  //Operand or other relating the Joined Columns
        private String rightsideColumnName;                     //Flag indicating Column is nullable
        private String rightsideModifier;              //Function to Modify Column from Joined Table

        /*******************************************************************************************
         * Constructor: JoinDefintion (Default)
         */
        public JoinDefinition(XmlNode aJoinInXML)
        {
            leftsideColumnName = aJoinInXML["leftsideColumnName"].InnerText;
            leftsideModifier = aJoinInXML["leftsideModifier"].InnerText;
            joinRelation = aJoinInXML["relation"].InnerText;
            rightsideColumnName = aJoinInXML["rightsideColumnName"].InnerText;
            rightsideModifier = aJoinInXML["rightsideModifier"].InnerText;
        }

        /*******************************************************************************************
         * Method: 
         * Returns the Join Relation between the Columns being joined
         * @param none
         */
        public String getJoinRelation()
        {
            return (this.joinRelation);
        }

        /*******************************************************************************************
         * Method: getLeftsideColumnName
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getLeftsideColumnName()
        {
            return (this.leftsideColumnName);
        }

        /*******************************************************************************************
         * Method: getLeftsideModifier
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getLeftsideModifier()
        {
            return (this.leftsideModifier);
        }

        /*******************************************************************************************
         * Method: getRightsideColumnName
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getRightsideColumnName()
        {
            return (this.rightsideColumnName);
        }

        /*******************************************************************************************
         * Method: getRightsideModifier
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getRightsideModifier()
        {
            return (this.rightsideModifier);
        }
    }

    /***********************************************************************************************
     * Class: SQLTables
     **********************************************************************************************/
    public class SQLTables
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private List<SQLTable> mySQLTables = new List<SQLTable>();      //List of Defined SQL Tables

        private Boolean xmlFileLoaded;                  //Flag indicating successful XML File Import

        /*******************************************************************************************
         * Constructor: TableColumn (Default)
         * Load the Specified XML File (FQN passed as parameter) and build the List of SQL Tables
         * from the data in that file.
         */
        public SQLTables(String anXMLFilePath)
        {
            XmlDocument myXMLRoot;                  //Object to Contain the XML document when loaded
            myXMLRoot = new XmlDocument();      //Create the XML document Object for further work...

            xmlFileLoaded = loadXMLFile(myXMLRoot, anXMLFilePath);     //Load the Specified XML file

            buildSQLTable(myXMLRoot);               //Build the SQL Tables List from Loaded XML file
        }

        /*******************************************************************************************
         *  buildSQLTable
         *  Builds a SQL Table list from newly opened XML Document.
         * ****************************************************************************************/
        private void buildSQLTable(XmlDocument myXMLDoc)
        {
            XmlElement sqlTablesList;                  //XML Element to Store the List of SQL Tables
            XmlElement sqlTables;                       //Element to Store the XML SQL Table Objects
            XmlElement currentSQLTable;                  //Element to Store the XML SQL Table Object
            SQLTable mySQLTable;                        //Storage for the Converted SQL Table Object

            sqlTablesList = myXMLDoc.DocumentElement;

            sqlTables = (XmlElement)sqlTablesList.ChildNodes[0];

            int maxTables = sqlTables.GetElementsByTagName("SQLTable").Count;

            for (int i = 0; i < maxTables; i++)
            {
                currentSQLTable = (XmlElement)sqlTables.ChildNodes[i];
                mySQLTable = new SQLTable(currentSQLTable);

                mySQLTables.Add(mySQLTable);
            }
        }

        /*******************************************************************************************
         * Method: findIndex
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public int findIndex(String fqTableName)
        {
            int tableIndex = -1;
            String[] aTableName = fqTableName.Split('.');

            for (int currentTable = 0; currentTable < this.getNumberOfTables(); currentTable++)
            {
                if((this.getTable(currentTable).getDatabaseName() == aTableName[0])
                    && (this.getTable(currentTable).getTableName() == aTableName[1])
                  )
                {
                    tableIndex = currentTable;
                }
            }

            return (tableIndex);
        }

        /*******************************************************************************************
         * Method: getLeftsideColumnName
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public int getNumberOfTables()
        {
            return (mySQLTables.Count);
        }

        /*******************************************************************************************
         * Method: getLeftsideModifier
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public SQLTable getTable(int aTableNumber)
        {
            return (this.mySQLTables[aTableNumber]);
        }

        /*******************************************************************************************
         *  loadXMLFile
         *  Attempts to load XML Document passed as the first parameter with the File specified in
         *  the second parameter. Returns "True" if successful; "False" if not.
         * ****************************************************************************************/
        private Boolean loadXMLFile(XmlDocument myXMLDoc, String aFilePath)
        {
            Boolean isLoaded;
            isLoaded = false;

            myXMLDoc.Load(aFilePath);

            if (myXMLDoc != null)
            {
                isLoaded = true;
            }

            return isLoaded;
        }

        /*******************************************************************************************
         * Method: xmlFileLoaded
         * Returns the Value of the XML File loaded Flag
         * @param none
         */
        public Boolean isXMLFileLoaded()
        {
            return (this.xmlFileLoaded);
        }
    }
    
    /***********************************************************************************************
     * Class: SQLTable
     **********************************************************************************************/
    public class SQLTable
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private String databaseName;                        //Name of the Owner or Database of Table
        private String tableAbbreviation;                //Abbreviation for Table for use in Queries
        private String tableName;                                                //Name of the Table

        private List<TableColumn> tableColumns = new List<TableColumn>();//Column List for this Table
        private List<TableJoin> tableJoins = new List<TableJoin>();            //Joins for this Table

        /*******************************************************************************************
         * Constructor: SQLTable (Default)
         */
        public SQLTable(XmlElement aTableInXML)
        {
            XmlElement objectList;                    //Storage for the Column and Join List Objects
            int maxObjects = 0;             //Maximum Number of Columns Defined in this Table object

            TableColumn myColumn;                  //Working object to use when Creating Column List
            TableJoin myJoin;                        //Working object to use when creating Join List
            
            //Add the Table Name and Object Information
            databaseName = aTableInXML["databaseName"].InnerText;
            tableName = aTableInXML["tableName"].InnerText;
            tableAbbreviation = aTableInXML["tableAbbreviation"].InnerText;

            //Loop through the Columns List and Add to the SQL Table Object
            objectList = (XmlElement)aTableInXML.GetElementsByTagName("columns")[0];

            maxObjects = objectList.GetElementsByTagName("column").Count;

            for (int thisColumn = 0; thisColumn < maxObjects; thisColumn++)
            {
                myColumn = new TableColumn(objectList.GetElementsByTagName("column")[thisColumn]);
                tableColumns.Add(myColumn);
            }

            //Loop through the Joins List and Add to the SQL Table Object
            objectList = (XmlElement)aTableInXML.GetElementsByTagName("joins")[0];

            maxObjects = objectList.GetElementsByTagName("join").Count;

            for (int thisJoin = 0; thisJoin < maxObjects; thisJoin++)
            {
                myJoin = new TableJoin((XmlElement)objectList.GetElementsByTagName("join")[thisJoin]);
                tableJoins.Add(myJoin);
            }
        }

        /*******************************************************************************************
         * Method: columnCount
         * Returns the Number of Columns this Table Contains
         * @param none
         */
        public int columnCount()
        {
            return (this.tableColumns.Count);
        }

        /*******************************************************************************************
         * Method: getColumnDataType
         * Returns the Name of Specified Column's Datatype
         * @param none
         */
        public String getColumnDataType(int aColumnNumber)
        {
            return (this.tableColumns[aColumnNumber].getColumnDataType());
        }

        /*******************************************************************************************
         * Method: getColumnIsNull
         * Returns the Is Null Value of Specified Column
         * @param none
         */
        public Boolean getColumnIsNull(int aColumnNumber)
        {
            return (this.tableColumns[aColumnNumber].isNull());
        }

        /*******************************************************************************************
         * Method: getColumnName
         * Returns the Name of Specified Column
         * @param none
         */
        public String getColumnName(int aColumnNumber)
        {
            return (this.tableColumns[aColumnNumber].getColumnName());
        }

        /*******************************************************************************************
         * Method: getDatabaseName
         * Returns the Name of this object's Database
         * @param none
         */
        public String getDatabaseName()
        {
            return (this.databaseName);
        }

        /*******************************************************************************************
         * Method: getJoinRightsideDatabase
         * Returns the Table on the left side of the specified join
         * @param none
         */
        public String getJoinRightsideDatabase(int aJoinNumber)
        {
            return (this.tableJoins[aJoinNumber].getRightsideDatabase());
        }

        /*******************************************************************************************
         * Method: getJoinRightsideModifier
         * Returns the Table on the left side of the specified join
         * @param none
         */
        public String getJoinRightsideTableName(int aJoinNumber)
        {
            return (this.tableJoins[aJoinNumber].getRightsideTableName());
        }

        /*******************************************************************************************
         * Method: getTableAbbreviation
         * Returns the Abbreviation of this object's Table
         * @param none
         */
        public String getTableAbbreviation()
        {
            return (this.tableAbbreviation);
        }

        /*******************************************************************************************
         * Method: getTableColumn
         * Returns the Specified Table Column as an object
         * @param none
         */
        public TableColumn getTableColumn(int aColumnNumber)
        {
            return (this.tableColumns[aColumnNumber]);
        }

        /*******************************************************************************************
         * Method: getTableName
         * Returns the Name of this object's Table
         * @param none
         */
        public String getTableName()
        {
            return (this.tableName);
        }

        /*******************************************************************************************
         * Method: getTableJoin
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public TableJoin getTableJoin(int joinNumber)
        {
            return (this.tableJoins[joinNumber]);
        }

        /*******************************************************************************************
         * Method: joinCount
         * Returns the Number of Joins this Table has Defined
         * @param none
         */
        public int joinCount()
        {
            return (this.tableJoins.Count);
        }

        /*******************************************************************************************
         * Method: setTableAbbreviation
         * Returns the Abbreviation of this object's Table
         * @param none
         */
        public void setTableAbbreviation(String anAbbreviation)
        {
            tableAbbreviation = anAbbreviation;
        }
    }

    /***********************************************************************************************
     * Class: TableColumn
     **********************************************************************************************/
    public class TableColumn
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private String columnDataType;                                     //SQL Data Type of Column
        private String columnName;                           //Name of the Column as used in Queries
        private Boolean isNullable;                             //Flag indicating Column is nullable

        /*******************************************************************************************
         * Constructor: TableColumn (Default)
         */
        public TableColumn(XmlNode aColumnInXML)
        {
            columnName = aColumnInXML["columnName"].InnerText;
            columnDataType = aColumnInXML["columnDataType"].InnerText;
            if (aColumnInXML["isNull"].InnerText == "True")
            {
                isNullable = true;
            }
            else
            {
                isNullable = false;
            }
        }

        /*******************************************************************************************
         * Method: getColumnDataType
         * Returns the Data Type of this object's column
         * @param none
         */
        public String getColumnDataType()
        {
            return (this.columnDataType);
        }

        /*******************************************************************************************
         * Method: isNull
         * Returns the Name of this object's column
         * @param none
         */
        public Boolean isNull()
        {
            return (this.isNullable);
        }

        /*******************************************************************************************
         * Method: getColumnName
         * Returns the Name of this object's column
         * @param none
         */
        public String getColumnName()
        {
            return (this.columnName);
        }
    }

    /***********************************************************************************************
     * Class: TableJoin
     **********************************************************************************************/
    public class TableJoin
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private String rightsideDatabase;                      //Database Name of Table being Joined
        private String rightsideTableName;            //Name of the Table for the Right side of Join

        private List<JoinDefinition> myJoinList = new List<JoinDefinition>();  //List of Defined Joins

        /*******************************************************************************************
         * Constructor: TableColumn (Default)
         */
        public TableJoin(XmlElement aJoinInXML)
        {
            int maxObjects = 0;             //Maximum Number of Columns Defined in this Table object

            JoinDefinition myJoin;                    //Working object to use when creating Join List

            rightsideDatabase = aJoinInXML["rightsideDatabase"].InnerText;
            rightsideTableName = aJoinInXML["rightsideTableName"].InnerText;

            //Loop through the Joins List and Add to the SQL Table Object
            maxObjects = aJoinInXML.GetElementsByTagName("joindef").Count;

            for (int thisJoin = 0; thisJoin < maxObjects; thisJoin++)
            {
                myJoin = new JoinDefinition(aJoinInXML.GetElementsByTagName("joindef")[thisJoin]);
                myJoinList.Add(myJoin);
            }
        }

        /*******************************************************************************************
         * Method: getJoinDefinition
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public JoinDefinition getJoinDefinition(int joinDefinitionNumber)
        {
            return (this.myJoinList[joinDefinitionNumber]);
        }

        /*******************************************************************************************
         * Method: getNumberOfJoinDefinitions
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public int getNumberOfJoinDefinitions()
        {
            return (this.myJoinList.Count());
        }

        /*******************************************************************************************
         * Method: getRightsideDatabase
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getRightsideDatabase()
        {
            return (this.rightsideDatabase);
        }

        /*******************************************************************************************
         * Method: getRightsideTableName
         * Returns the Column Name for this Table in Join
         * @param none
         */
        public String getRightsideTableName()
        {
            return (this.rightsideTableName);
        }
    }
}
