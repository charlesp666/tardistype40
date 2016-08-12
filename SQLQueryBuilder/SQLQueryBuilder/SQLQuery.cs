/***************************************************************************************************
 * SQLQuery.cs
 * 
 * Class Definition for the SQL Query Class. This stores the data and builds the query according
 * to User Selections.
 * 
 * Created:            26-October-2015
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

namespace SQLQueryBuilder
{
    /***********************************************************************************************
     * Class: SQLQuery
     **********************************************************************************************/
    class SQLQuery
    {
        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        private List<QueryTable> myQueryTables = new List<QueryTable>();      //List of Query Tables
        private int mySQLEngine = 0;      //Flag indicating the SQL Engine (Oracle, SQL Server, ...)

        public int sqlEngineSQLServer = 0;                 //Flag Value indicating SQL Server Engine
        public int sqlEngineOracle = 1;                    //Flag Value indicating Oracle SQL Engine

        //Store the "execute" command for Oracle and SQL Server as well as Server flag Values
        private String executeOracle = ";";                         //Execute Command for Oracle SQL
        private String executeSQLServer = "go";                     //Execute Command for SQL Server

        //Build Templates to Use when Building Query
        private String templateJoinDefinition = "|           and [joinDefinition]";
        private String templateJoinTable = "|  join [joinDatabase].[joinTable] [tableAbbreviation]|       on ([joinDefinition]|          )";
        private String templateSelectColumn = "|     , [colTableAbbreviation].[selectColumn]";

        private String templateSQLQuery = "select [colTableAbbreviation].[selectColumn]|  from [joinDatabase].[joinTable]  [tableAbbreviation]|[ExecuteCommand]";

        /*******************************************************************************************
         * Constructor: SQLQuery (Default)
         * Load the Specified XML File (FQN passed as parameter) and build the List of SQL Tables
         * from the data in that file.
         */
        public SQLQuery(SQLTables listSQLTables)
        {
            addTables(listSQLTables);
        }

        /*******************************************************************************************
         * addTables
         * Adds the Table from the SQL Query List to the Query Object Table List.
         */
        public void addTables(SQLTables listSQLTables)
        {
            for (int currentTable = 0; currentTable < listSQLTables.getNumberOfTables(); currentTable++)
            {
                QueryTable aQueryTable = new QueryTable(listSQLTables.getTable(currentTable));
                myQueryTables.Add(aQueryTable);
            }
        }

        /*******************************************************************************************
         *  buildSQLTable
         *  Builds a SQL Query from the Selected Tables and Columns.
         * ****************************************************************************************/
        public String buildSQLQuery()
        {
            String myQuery = templateSQLQuery;        //Copy Query Template to Use a Working Storage

            //Replace the Execute Commmand with the appropriate text based on the SQL Engine
            String theExecuteCommand = "";
            if (this.mySQLEngine == this.sqlEngineOracle)
            {
                theExecuteCommand = this.executeOracle;
            }
            else if (this.mySQLEngine == this.sqlEngineSQLServer)
            {
                theExecuteCommand = this.executeSQLServer;
            }

            myQuery = myQuery.Replace("[ExecuteCommand]", theExecuteCommand);

            //Loop Through tables and add to Query if they've been selected
            for (int currentTable = 0; currentTable < this.getNumberOfTables(); currentTable++)
            {
                QueryTable thisTable = myQueryTables[currentTable];
                if (thisTable.isTableSelected())
                {
                    //Store the Table's Abbreviation to Simplify referencing it
                    String tableAbbreviation = thisTable.getTableAbbreviation();

                    //Loop through and add to the query the columns that have been selected
                    for (int i = 0; i < thisTable.columnCount(); i++)
                    {
                        QueryColumn myColumn = thisTable.getQueryColumn(i);
                        if (myColumn.isSelected())
                        {
                            myQuery = myQuery.Replace("[colTableAbbreviation]", tableAbbreviation);
                            myQuery = myQuery.Replace("[selectColumn]", (myColumn.getColumnName() + templateSelectColumn));
                        }
                    }

                    myQuery = myQuery.Replace("[joinDatabase]", thisTable.getDatabaseName());
                    myQuery = myQuery.Replace("[joinTable]", thisTable.getTableName());
                    myQuery = myQuery.Replace("[tableAbbreviation]", (tableAbbreviation + templateJoinTable));

                    //Add the join clauses for other than the first selected table
//                    if (currentTable > 0)
//                    {

//                    }
                }
            }

            //Clear any remaining Tags from the final Query
            myQuery = myQuery.Replace(templateSelectColumn, "");
            myQuery = myQuery.Replace(templateJoinTable, "");
            myQuery = myQuery.Replace(templateJoinDefinition, "");

            myQuery = myQuery.Replace("|", Environment.NewLine);

            return myQuery;
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
                if ((this.getQueryTable(currentTable).getDatabaseName() == aTableName[0])
                    && (this.getQueryTable(currentTable).getTableName() == aTableName[1])
                  )
                {
                    tableIndex = currentTable;
                }
            }

            return (tableIndex);
        }

        /*******************************************************************************************
         * Constructor: QueryTable (Default)
         * Load the Specified XML File (FQN passed as parameter) and build the List of SQL Tables
         * from the data in that file.
         */
        public QueryTable getQueryTable(int aTableNumber)
        {
            return this.myQueryTables[aTableNumber];
        }

        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        public int getNumberOfTables()
        {
            return (myQueryTables.Count);
        }

        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        public int getSQLEngine()
        {
            return (this.mySQLEngine);
        }

        /*******************************************************************************************
         * Attributes or Properties of SQLTable
         */
        public void setSQLEngine(int aSQLEngineFlag)
        {
            this.mySQLEngine = aSQLEngineFlag;
        }

        /***********************************************************************************************
         * Class: QueryColumn
         **********************************************************************************************/
        public class QueryColumn
        {
            /*******************************************************************************************
             * Attributes or Properties of SQLTable
             */
            private Boolean columnSelected;                      //Boolean indicating Column is Selected

            private String columnDataType;                                     //SQL Data Type of Column
            private String columnName;                           //Name of the Column as used in Queries
            private Boolean isNullable;                             //Flag indicating Column is nullable

            /*******************************************************************************************
             * Constructor: TableColumn (Default)
             */
            public QueryColumn(TableColumn aColumn)
            {
                this.columnSelected = false;

                this.columnDataType = aColumn.getColumnDataType();
                this.columnName = aColumn.getColumnName();
                this.isNullable = aColumn.isNull();
            }

            /*******************************************************************************************
             * Method: getColumnName
             * Returns the Name of this objects Column
             * @param none
             */
            public String getColumnName()
            {
                return (this.columnName);
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
             * Method: isSelected
             * Returns the Name of this object's column
             * @param none
             */
            public Boolean isSelected()
            {
                return (this.columnSelected);
            }

            /*******************************************************************************************
             * Method: setSelected
             * Returns the Name of this object's column
             * @param none
             */
            public void setSelected(Boolean flgSelected)
            {
                this.columnSelected = flgSelected;
            }
        }

        /***********************************************************************************************
         * Class: QueryTable
         **********************************************************************************************/
        public class QueryTable
        {
            /*******************************************************************************************
             * Attributes or Properties of SQLTable
             */
            private Boolean isSelected;              //Flag indicating Table has been Selected for Query

            private String databaseName;                        //Name of the Owner or Database of Table
            private String tableAbbreviation;                //Abbreviation for Table for use in Queries
            private String tableName;                                                //Name of the Table
            
            private List<QueryColumn> queryColumns = new List<QueryColumn>();//Column List for this Table
            private List<QueryJoin> queryJoins = new List<QueryJoin>();            //Joins for this Table

            /*******************************************************************************************
             * Constructor: QueryTable (Default)
             * Load the Specified XML File (FQN passed as parameter) and build the List of SQL Tables
             * from the data in that file.
             */
            public QueryTable(SQLTable aSQLTable)
            {
                int maxColumns = 0 ;
                int maxJoins = 0;

                this.isSelected = false;

                this.databaseName = aSQLTable.getDatabaseName();
                this.tableAbbreviation = aSQLTable.getTableAbbreviation();
                this.tableName = aSQLTable.getTableName();
                
                //Loop through the Columns List and Add to the SQL Table Object
                maxColumns = aSQLTable.columnCount();

                for (int thisColumn = 0; thisColumn < maxColumns; thisColumn++)
                {
                    QueryColumn myColumn = new QueryColumn(aSQLTable.getTableColumn(thisColumn));

                    queryColumns.Add(myColumn);
                }

                //Loop through the Joins List and Add to the SQL Table Object
                maxJoins = aSQLTable.joinCount();

                for (int thisJoin = 0; maxJoins < maxJoins; thisJoin++)
                {
                    QueryJoin myJoin = new QueryJoin(aSQLTable.getTableJoin(thisJoin));

                    queryJoins.Add(myJoin);
                }
            }

            /*******************************************************************************************
             * Method: columnCount
             * Returns the Number of Columns this Table Contains
             * @param none
             */
            public int columnCount()
            {
                return (this.queryColumns.Count);
            }

            /*******************************************************************************************
             * Method: findColumnIndex
             * Given the name of column returns the 
             * @param none
             */
            public int findColumnIndex(String aColumnName)
            {
                int columnNumber = -1;

                for (int columnCount = 0; columnCount < this.columnCount(); columnCount++)
                {
                    if (this.getQueryColumn(columnCount).getColumnName() == aColumnName)
                    {
                        columnNumber = columnCount;
                        break;
                    }
                }
                return (columnNumber);
            }

            /*******************************************************************************************
             * getTableAbbreviation
             * Returns the Value of the Table Abbreviation
             */
            public String getTableAbbreviation()
            {
                return (this.tableAbbreviation);
            }

            /*******************************************************************************************
             * getDatabaseName
             * Returns the Value of the Database Name
             */
            public String getDatabaseName()
            {
                return(this.databaseName);
            }

            /*******************************************************************************************
             * Method: getQueryColumn
             * Returns the Specified Query Column as an object
             * @param none
             */
            public QueryColumn getQueryColumn(int aColumnNum)
            {
                return (this.queryColumns[aColumnNum]);
            }

            /*******************************************************************************************
             * getTableName
             * Returns the Value of the Table Name
             */
            public String getTableName()
            {
                return(this.tableName);
            }

            /*******************************************************************************************
             * isTableSelected
             * Returns the Value of the isSelected Flag
             */
            public Boolean isTableSelected()
            {
                return(this.isSelected);
            }

            /*******************************************************************************************
             * setTableSelected
             * Sets the Value of the isSelected Flag
             */
            public void setTableSelected(Boolean tableSelectionStatus)
            {
                this.isSelected = tableSelectionStatus;
            }
        }

        /***********************************************************************************************
         * Class: JoinDefinition
         **********************************************************************************************/
        public class QueryJoinDefinition
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
            public QueryJoinDefinition( String aLeftsideColumnName
                                      , String aLeftsideModifier
                                      , String aJoinRelation
                                      , String aRightsideColumnName
                                      , String aRightsideModifier
                                      )
            {
                leftsideColumnName = aLeftsideColumnName;
                leftsideModifier = aLeftsideModifier;
                joinRelation = aJoinRelation;
                rightsideColumnName = aRightsideColumnName;
                rightsideModifier = aRightsideModifier;
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
         * Class: QueryJoin
         **********************************************************************************************/
        public class QueryJoin
        {
            /*******************************************************************************************
             * Attributes or Properties of SQLTable
             */
            private String rightsideDatabase;                      //Database Name of Table being Joined
            private String rightsideTableName;            //Name of the Table for the Right side of Join

            private List<QueryJoinDefinition> myJoinList = new List<QueryJoinDefinition>();  //List of Defined Joins

            /*******************************************************************************************
             * Constructor: TableColumn (Default)
             */
            public QueryJoin(TableJoin aJoin)
            {
                //Loop through the Joins List and Add to the SQL Table Object
                this.rightsideDatabase = aJoin.getRightsideDatabase();
                this.rightsideTableName = aJoin.getRightsideTableName();

                int maxObjects = aJoin.getNumberOfJoinDefinitions();

                for (int thisJoin = 0; thisJoin < maxObjects; thisJoin++)
                {
                    JoinDefinition sourceJoin = aJoin.getJoinDefinition(thisJoin);
                     QueryJoinDefinition myJoin = new QueryJoinDefinition( sourceJoin.getLeftsideColumnName()
                                                                         , sourceJoin.getLeftsideModifier()
                                                                         , sourceJoin.getJoinRelation()
                                                                         , sourceJoin.getRightsideColumnName()
                                                                         , sourceJoin.getRightsideModifier()
                                                                         );

                    myJoinList.Add(myJoin);
                }
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
}
