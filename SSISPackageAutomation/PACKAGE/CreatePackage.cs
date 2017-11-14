using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Npgsql;
using System.Data;
using System.Collections.Generic;

namespace SSISPackageAutomation
{
    class CreatePackage
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine("Please pass Entity and File Location: ");
                    System.Console.WriteLine("Entity Like:  AO_ , AO_A , AO_E, AO_3 , AO_5, AO_8 , AO_2 , AO_0");
                    System.Console.WriteLine("File Location like: D:\\FolderName (in which SSIS Packages will be stored)");
                    return 1;
                }

                if (args[0] == "")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine("Please pass Entity and File Location: ");
                    System.Console.WriteLine("Entity Like:  AO_ , AO_A , AO_E, AO_3 , AO_5, AO_8 , AO_2 , AO_0");
                    System.Console.WriteLine("File Location Like: D:\\FolderName (in which SSIS Packages will be stored)");
                    return 1;
                }

                if (args[1] == "")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine("Please pass Entity and File Location: ");
                    System.Console.WriteLine("Entity Like:  AO_ , AO_A , AO_E, AO_3 , AO_5, AO_8 , AO_2 , AO_0");
                    System.Console.WriteLine("File Location Like: D:\\FolderName (in which SSIS Packages will be stored)");
                    return 1;
                }

                CreatePackage pg = new CreatePackage();
                //pg.GetPostGreSQLSchema();  //one - 
                pg.GeneratePackage(args[0], args[1]);  //variable

                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            //commit
        }

        public void GeneratePackage(string entity , string filepath)
        {
            try
            {
                Console.Title = "SSIS Automation";
                Console.ForegroundColor = ConsoleColor.Yellow;

                //// Read input parameters
                var server = "ebi-etl-dev-01";
                var database = "JIRA_ODS";

                Console.WriteLine("Building Package...");

                // Create a new SSIS Package
                var package = new Package();
                
                SSISConnection conn = new SSISConnection();
                package.ProtectionLevel = DTSProtectionLevel.DontSaveSensitive;

                //conn.CreateODBCConnection(package);
                ConnectionManager ConMgr;
                ConMgr = package.Connections.Add("ODBC");
                //ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=d-db1n2.shr.ord1.corp.rackspace.net;uid=vams3203;database=jira_staging_ebi;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                ConMgr.Name = "JIRA_ODS POSTGRESQL_SOURCE";
                ConMgr.Description = "ODBC Connection for PostGreSQL Database";

                // Add a Connection Manager to the Package, of type, OLEDB 
                var connMgrOleDb = package.Connections.Add("OLEDB");
                var connectionString = new StringBuilder();
                connectionString.Append("Provider=SQLOLEDB.1;");
                connectionString.Append("Integrated Security=SSPI;Initial Catalog=");
                connectionString.Append(database);
                connectionString.Append(";Data Source=");
                connectionString.Append(server);
                connectionString.Append(";");

                connMgrOleDb.ConnectionString = connectionString.ToString();
                connMgrOleDb.Name = "JIRA_ODS SQLSERVER_DESTINATION";
                connMgrOleDb.Description = "OLE DB connection for SQLSERVER Database";

                //get list of table to be loaded in one DFT
                //get list of tables in one package from SQL
                var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "JIRA_ODS"));
                connection.Open();
                List<String> TableNames = new List<String>();
                string query = "SELECT DISTINCT TABLE_NAME FROM LOG_SCHEMA_TABLE WHERE TABLE_NAME LIKE '" + entity + "%'";
                using (SqlCommand PostGrsCmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader PostRead = PostGrsCmd.ExecuteReader())
                    {
                        while (PostRead.Read())
                        {
                            TableNames.Add(PostRead.GetString(0));
                        }
                    }
                }
                connection.Close();

                //ADD EXECUTE SQL TASK - FOR TRUNCATING DATA BEFORE LOADING
                Executable eFileTask1 = package.Executables.Add("STOCK:SQLTask");
                TaskHost SQLTask = (TaskHost)(eFileTask1);
                SQLTask.Properties["Name"].SetValue(SQLTask, "Truncate Existing Data in Destination");
                SQLTask.Properties["Description"].SetValue(SQLTask, "Erases existing data in the destination tables before loading");
                SQLTask.Properties["Connection"].SetValue(SQLTask, connMgrOleDb.Name);
                string Truncatescript = GetTruncateScript(TableNames);
                SQLTask.Properties["SqlStatementSource"].SetValue(SQLTask, Truncatescript);

                // Add a Data Flow Task to the Package
                var app = new Application();
                if (TableNames.Count > 0)
                {
                    foreach (string TableNm in TableNames)
                    {
                        var e = package.Executables.Add("STOCK:PipelineTask");
                        var mainPipe = e as TaskHost;
                        var dataFlowTask = mainPipe.InnerObject as MainPipe;
                        mainPipe.Name = TableNm;

                        if (dataFlowTask != null)
                        {
                            try
                            {
                                // Add a Flat ODBC Source Component to the Data Flow Task
                                var ODBCSourceComponent = dataFlowTask.ComponentMetaDataCollection.New();
                                ODBCSourceComponent.Name = "My PostgreSQL_" + TableNm;
                                ODBCSourceComponent.ComponentClassID = app.PipelineComponentInfos["ODBC Source"].CreationName; // "A77F5655-A006-443A-9B7E-90B6BD55CB84";//"DTSAdapter.ODBCSource";//app.PipelineComponentInfos["ODBC"].CreationName;
                                var ODBCSourceInstance = ODBCSourceComponent.Instantiate();
                                ODBCSourceInstance.ProvideComponentProperties();
                                ODBCSourceComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(ConMgr);
                                ODBCSourceComponent.RuntimeConnectionCollection[0].ConnectionManagerID = ConMgr.ID;
                                ODBCSourceInstance.SetComponentProperty("AccessMode", 2);
                                string cmd = "SELECT * FROM public.\"" + TableNm + "\"";
                                ODBCSourceInstance.SetComponentProperty("SqlCommand", cmd);

                                // Reinitialize the metadata.
                                ODBCSourceInstance.AcquireConnections(null);
                                ODBCSourceInstance.ReinitializeMetaData();
                                ODBCSourceInstance.ReleaseConnections();

                                //  Add transform data conversion
                                IDTSComponentMetaData100 dataConvertComponent = dataFlowTask.ComponentMetaDataCollection.New();
                                dataConvertComponent.ComponentClassID = "DTSTransform.DataConvert";
                                dataConvertComponent.Name = "Data Convert";
                                dataConvertComponent.Description = "Data Conversion Component";
                                CManagedComponentWrapper dataConvertWrapper = dataConvertComponent.Instantiate();
                                dataConvertWrapper.ProvideComponentProperties();

                                // Connect the source and the transform
                                dataFlowTask.PathCollection.New().AttachPathAndPropagateNotifications(ODBCSourceComponent.OutputCollection[0], dataConvertComponent.InputCollection[0]);

                                // Configure the transform
                                IDTSVirtualInput100 dataConvertVirtualInput = dataConvertComponent.InputCollection[0].GetVirtualInput();
                                IDTSOutput100 dataConvertOutput = dataConvertComponent.OutputCollection[0];
                                IDTSOutputColumnCollection100 dataConvertOutputColumns = dataConvertOutput.OutputColumnCollection;

                                //only one column datatype is converted to different datatype
                                //GET DATA COLUMNS OF TYPE NVARCHAR FROM SQL SERVER BY TABLE NAME
                                //<list> column = GetColumnNVarcharFromTable(TableNm);
                                var connection_column = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "JIRA_ODS"));
                                connection_column.Open();
                                List<String> NvarcharColumns = new List<String>();
                                string Query = "SELECT COLUMN_NAME FROM LOG_SCHEMA_COLUMN WHERE TABLE_NAME LIKE '" + TableNm + "' AND DESTINATION_COLUMN_DATATYPE LIKE 'NVARCHAR%' ";
                                using (SqlCommand PostGrsCmd = new SqlCommand(Query, connection_column))
                                {
                                    using (SqlDataReader PostRead = PostGrsCmd.ExecuteReader())
                                    {
                                        while (PostRead.Read())
                                        {
                                            NvarcharColumns.Add(PostRead.GetString(0));
                                        }
                                    }
                                }
                                connection_column.Close();

                                foreach (string Column in NvarcharColumns)
                                {
                                    int sourceColumnLineageId = dataConvertVirtualInput.VirtualInputColumnCollection[Column].LineageID;
                                    dataConvertWrapper.SetUsageType(dataConvertComponent.InputCollection[0].ID, dataConvertVirtualInput, sourceColumnLineageId, DTSUsageType.UT_READONLY);
                                    IDTSOutputColumn100 newOutputColumn = dataConvertWrapper.InsertOutputColumnAt(dataConvertOutput.ID, 0, Column, string.Empty);
                                    newOutputColumn.SetDataTypeProperties(Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR, 255, 0, 0, 0);
                                    newOutputColumn.MappedColumnID = 0;
                                    dataConvertWrapper.SetOutputColumnProperty(dataConvertOutput.ID, newOutputColumn.ID, "SourceInputColumnLineageID", sourceColumnLineageId);
                                }

                                // Add an OLE DB Destination
                                IDTSComponentMetaData100 oleDbDestinationComponent = dataFlowTask.ComponentMetaDataCollection.New();
                                oleDbDestinationComponent.Name = "MyOLEDBDestination";
                                oleDbDestinationComponent.ComponentClassID = app.PipelineComponentInfos["OLE DB Destination"].CreationName;

                                // Get the design time instance of the Ole Db Destination component
                                IDTSDesigntimeComponent100 oleDbDestinationInstance = oleDbDestinationComponent.Instantiate();
                                oleDbDestinationInstance.ProvideComponentProperties();

                                // Set Ole Db Destination Connection
                                oleDbDestinationComponent.RuntimeConnectionCollection[0].ConnectionManagerID = connMgrOleDb.ID;
                                oleDbDestinationComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(connMgrOleDb);

                                // Set destination load type
                                oleDbDestinationInstance.SetComponentProperty("AccessMode", 3);
                                oleDbDestinationInstance.SetComponentProperty("OpenRowset", TableNm);

                                // Get the list of available columns
                                var oleDbDestinationInput = oleDbDestinationComponent.InputCollection[0];
                                var oleDbDestinationvInput = oleDbDestinationInput.GetVirtualInput();
                                var oleDbDestinationVirtualInputColumns = oleDbDestinationvInput.VirtualInputColumnCollection;

                                // Reinitialize the metadata
                                oleDbDestinationInstance.AcquireConnections(null);
                                oleDbDestinationInstance.ReinitializeMetaData();
                                oleDbDestinationInstance.ReleaseConnections();

                                // Create a Precedence Constraint between Data conversion component and OLEDB Destination Components
                                var path = dataFlowTask.PathCollection.New();
                                path.AttachPathAndPropagateNotifications(dataConvertComponent.OutputCollection[0], oleDbDestinationComponent.InputCollection[0]);

                                // Configure the destination
                                IDTSInput100 destInput = oleDbDestinationComponent.InputCollection[0];
                                IDTSVirtualInput100 destVirInput = destInput.GetVirtualInput();
                                IDTSInputColumnCollection100 destInputCols = destInput.InputColumnCollection;
                                IDTSExternalMetadataColumnCollection100 destExtCols = destInput.ExternalMetadataColumnCollection;
                                IDTSOutputColumnCollection100 sourceColumns = dataConvertComponent.OutputCollection[0].OutputColumnCollection;
                                IDTSOutputColumnCollection100 oledbSource = ODBCSourceComponent.OutputCollection[0].OutputColumnCollection;

                                // The OLEDB destination requires you to hook up the external data conversion columns
                                foreach (IDTSOutputColumn100 outputCol in sourceColumns)
                                {
                                    // Get the external column id
                                    IDTSExternalMetadataColumn100 extCol = (IDTSExternalMetadataColumn100)destExtCols[outputCol.Name];
                                    if (extCol != null)
                                    {
                                        // Create an input column from an output col of previous component.
                                        destVirInput.SetUsageType(outputCol.ID, DTSUsageType.UT_READONLY);
                                        IDTSInputColumn100 inputCol = destInputCols.GetInputColumnByLineageID(outputCol.ID);
                                        if (inputCol != null)
                                        {
                                            // map the input column with an external metadata column
                                            oleDbDestinationInstance.MapInputColumn(destInput.ID, inputCol.ID, extCol.ID);
                                        }
                                    }
                                }

                                // The OLEDB destination requires you to hook up the external Excel source columns
                                foreach (IDTSOutputColumn100 outputCol in oledbSource)
                                {
                                    // Get the external column id
                                    IDTSExternalMetadataColumn100 extCol = (IDTSExternalMetadataColumn100)destExtCols[outputCol.Name];
                                    if (extCol != null)
                                    {
                                        // Create an input column from an output col of previous component.
                                        destVirInput.SetUsageType(outputCol.ID, DTSUsageType.UT_READONLY);
                                        IDTSInputColumn100 inputCol = destInputCols.GetInputColumnByLineageID(outputCol.ID);
                                        if (inputCol != null)
                                        {
                                            // map the input column with an external metadata column
                                            oleDbDestinationInstance.MapInputColumn(destInput.ID, inputCol.ID, extCol.ID);
                                        }
                                    }
                                }

                                PrecedenceConstraint pcFileTasks = package.PrecedenceConstraints.Add((Executable)SQLTask, (Executable)mainPipe);
                                pcFileTasks.Value = DTSExecResult.Completion;
                            }

                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Hey der...Exception Occured while creating DFT: " + ex.Message + "\t" + ex.GetType() + "Press any key to close....");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.ReadKey();
                            }

                        }
                    }

                    var dtsx = new StringBuilder();
                    string PackageName = entity + "_Tables_Load_History";
                    dtsx.Append(filepath).Append("\\").Append(PackageName).Append(".dtsx");
                    Console.WriteLine("Saving Package...");
                    app.SaveToXml(dtsx.ToString(), package, null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Done; Packages are successfully created in the following path:   " + filepath);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadKey();
                    package.Dispose();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad Selection, check you don't have comma, pass arguments with out comma seperation:   " + filepath);
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hey der...Exception Occured while saving the package: " + ex.Message + "\t" + ex.GetType() + "Press any key to close....");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.ReadKey();
            }
        }
        
        public string GetTruncateScript(List<String> List)
        {
            string TruncateScript = "";
            foreach (var item in List)
            {
                TruncateScript = TruncateScript + "Truncate table " + item + ";";
            }
            return TruncateScript;
        }
    }

    //USE JIRA_ODS

    //CREATE TABLE LOG_SCHEMA_TABLE
    //(
    //TABLE_ID INT IDENTITY(1,1),
    //TABLE_NAME VARCHAR(100),
    //SCHEMA_RAW NVARCHAR(MAX),
    //SCHEMA_STATUS VARCHAR(100) NULL,
    //SCHEMA_ISSUES NVARCHAR(1000) NULL,
    //PACKAGE_CREATED VARCHAR(3) NULL,
    //PACKAGE_ISSUES NVARCHAR(1000) NULL,
    //CREATION_DATE DATETIME NULL,
    //UPDATED_DATE DATETIME NULL
    //)

    //CREATE TABLE LOG_SCHEMA_COLUMN
    //(
    //TABLE_COLUMN_ID INT IDENTITY(1,1),
    //TABLE_ID INT,
    //COLUMN_NAME VARCHAR(100),
    //SOURCE_COLUMN_DATATYPE VARCHAR(100),
    //DESTINATION_COLUMN_DATATYPE VARCHAR(100),
    //COLUMN_PRECISION INT NULL,
    //COLUMN_SCALE INT NULL,
    //COLUMN_ISNULL VARCHAR(3),
    //COLUMN_1 VARCHAR(100) NULL,
    //COLUMN_2 VARCHAR(100) NULL,
    //COLUMN_3 VARCHAR(100) NULL
    //)	

}