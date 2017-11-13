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
    class Program
    {
        static void Main(string[] args)
        {
            //Application application = new Application();
            //PipelineComponentInfos componentInfos = application.PipelineComponentInfos;
            //foreach (PipelineComponentInfo componentInfo in componentInfos)
            //{
            //    Console.WriteLine("Name: " + componentInfo.Name + "  CreationName: " + componentInfo.CreationName);
            //}
            Program pg = new Program();
            pg.GetPostGreSQLSchema();  //one - 
            pg.CreatePackage("AO_6");  //variable
            //commit
        }

        public void Createexample()
        {
            // Create a package and add a Data Flow task.  
            {
                Microsoft.SqlServer.Dts.Runtime.Package package = new Microsoft.SqlServer.Dts.Runtime.Package();
                Executable e = package.Executables.Add("STOCK:PipelineTask");
                Microsoft.SqlServer.Dts.Runtime.TaskHost thMainPipe = (Microsoft.SqlServer.Dts.Runtime.TaskHost)e;
                MainPipe dataFlowTask = (MainPipe)thMainPipe.InnerObject;

                // Add an OLE DB connection manager to the package.  
                ConnectionManager conMgr = package.Connections.Add("OLEDB");
                conMgr.ConnectionString = "Provider=SQLOLEDB.1;" + "Data Source=ebi-etl-dev-01;Initial Catalog=LDAP;" + "Integrated Security=SSPI;";
                conMgr.Name = "SSIS Connection Manager for OLE DB";
                conMgr.Description = "OLE DB connection to the AdventureWorks database.";

                // Create and configure an OLE DB source component.    
                IDTSComponentMetaData100 source = dataFlowTask.ComponentMetaDataCollection.New();
                source.ComponentClassID = "DTSAdapter.OleDbSource";
                // Create the design-time instance of the source.  
                CManagedComponentWrapper srcDesignTime = source.Instantiate();
                // The ProvideComponentProperties method creates a default output.  
                srcDesignTime.ProvideComponentProperties();
                // Assign the connection manager.  
                source.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(conMgr);
                // Set the custom properties of the source.  
                srcDesignTime.SetComponentProperty("AccessMode", 2);
                srcDesignTime.SetComponentProperty("SqlCommand", "Select * from Products");

                // Connect to the data source,  
                //  and then update the metadata for the source.  
                srcDesignTime.AcquireConnections(null);
                srcDesignTime.ReinitializeMetaData();
                srcDesignTime.ReleaseConnections();

                // Create and configure an OLE DB destination.  
                IDTSComponentMetaData100 destination = dataFlowTask.ComponentMetaDataCollection.New();
                destination.ComponentClassID = "DTSAdapter.OleDbDestination";
                // Create the design-time instance of the destination.  
                CManagedComponentWrapper destDesignTime = destination.Instantiate();
                // The ProvideComponentProperties method creates a default input.  
                destDesignTime.ProvideComponentProperties();

                // Create the path from source to destination.  
                IDTSPath100 path = dataFlowTask.PathCollection.New();
                path.AttachPathAndPropagateNotifications(source.OutputCollection[0], destination.InputCollection[0]);

                // Get the destination's default input and virtual input.  
                IDTSInput100 input = destination.InputCollection[0];
                IDTSVirtualInput100 vInput = input.GetVirtualInput();

                //only one column datatype is converted to different datatype
                

                
                // Iterate through the virtual input column collection.  
                foreach (IDTSVirtualInputColumn100 vColumn in vInput.VirtualInputColumnCollection)
                {
                    // Call the SetUsageType method of the destination  
                    //  to add each available virtual input column as an input column.  
                    destDesignTime.SetUsageType(input.ID, vInput, vColumn.LineageID, DTSUsageType.UT_READONLY);
                }

                // Verify that the columns have been added to the input.  
                foreach (IDTSInputColumn100 inputColumn in destination.InputCollection[0].InputColumnCollection)
                {
                    Console.WriteLine(inputColumn.Name);
                }
                Console.Read();
            }
        }

        public void GetPostGreSQLSchema()
        {
            try
            {
                //get connect to postgresql
                //String connstring = String.Format("Server ={0}; Port ={1}; User Id = {2}; Password ={3}; Database ={4};","localhost","5432", "vams3203", "123456", "NEWDB");
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "d-db1n2.shr.ord1.corp.rackspace.net", "5432", "vams3203", "", "jira_staging_ebi"); //source
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                //string sqldrop = ""
                List<String> TableNames = new List<String>();
                string query = "select tablename from pg_catalog.pg_tables where schemaname = 'public'";
                using (NpgsqlCommand PostGrsCmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader PostRead = PostGrsCmd.ExecuteReader())
                    {
                        while (PostRead.Read())
                        {
                            TableNames.Add(PostRead.GetString(0));
                        }
                    }
                }
                conn.Close();
                //create schema for each table and execute in sqlserver
                foreach (string TableNm in TableNames)
                    {
                    var sqlselect = new StringBuilder();
                    try
                        {
                            string sql = "SELECT * FROM public.\"" + TableNm + "\" where 1 = 0"; //variable 
                            //SELECT* FROM public."AO_3A3ECC_REMOTE_IDCF" WHERE 1 = 0;
                            conn.Open();
                            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            DataTable schema = reader.GetSchemaTable();
                            conn.Close();

                            //get schema of a table (example)
                            sqlselect.Append("CREATE TABLE ");
                            sqlselect.Append(TableNm);  //variable
                            sqlselect.Append(" (");
                            List<ColumnDetails> lstColumnDetails = new List<ColumnDetails>();

                            int i = 0;
                            foreach (DataRow row in schema.Rows)
                            {
                                i = i + 1;
                                string SQLDatatype;
                                if (i != schema.Rows.Count)
                                {
                                    sqlselect.Append("["+ row["COLUMNNAME"].ToString() + "]" + "  ");
                                    SQLDatatype = GetSQLDataType(row["DATATYPE"].ToString(), row["COLUMNSIZE"].ToString(), row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["PROVIDERTYPE"].ToString());
                                    sqlselect.Append(SQLDatatype  + ","); //GetSQLDataType() pass the system datatype to a function and get sql datatype
                                    lstColumnDetails.Add(new ColumnDetails(TableNm, row["COLUMNNAME"].ToString(), row["DATATYPE"].ToString(), SQLDatatype,  row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["AllowDBNull"].ToString()));
                                }
                                else
                                {
                                    sqlselect.Append("[" + row["COLUMNNAME"].ToString() + "]" + "  ");
                                    SQLDatatype = GetSQLDataType(row["DATATYPE"].ToString(), row["COLUMNSIZE"].ToString(), row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["PROVIDERTYPE"].ToString());
                                    sqlselect.Append(GetSQLDataType(row["DATATYPE"].ToString(), row["COLUMNSIZE"].ToString(), row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["PROVIDERTYPE"].ToString()) + ")"); //pass the system datatype to a function and get sql datatype
                                    lstColumnDetails.Add(new ColumnDetails(TableNm, row["COLUMNNAME"].ToString(), row["DATATYPE"].ToString(), SQLDatatype, row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["AllowDBNull"].ToString()));
                                 }
                            }

                            var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "JIRA_ODS"));
                            var command = new SqlCommand(sqlselect.ToString(), connection);
                            // Create table in destination sql database to hold file data
                            connection.Open();
                            command.ExecuteNonQuery();
                            Log_TableEntry(TableNm, sqlselect.ToString(), "Success" );
                            Log_TableColumnEntry(lstColumnDetails);
                            Console.WriteLine("Success, Created Table: " + TableNm);
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Log_TableEntry(TableNm, sqlselect.ToString(), "Failure");
                            Console.WriteLine("Failed, Creating Table: " + TableNm);
                        }
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        public class ColumnDetails
        {
            public ColumnDetails(string TABLE_NAME, string COLUMN_NAME, string SOURCE_COLUMN_DATATYPE, string DESTINATION_COLUMN_DATATYPE, string COLUMN_PRECISION, string COLUMN_SCALE, string COLUMN_ISNULL)
            {
                this.TABLE_NAME = TABLE_NAME;
                this.COLUMN_NAME = COLUMN_NAME;
                this.SOURCE_COLUMN_DATATYPE = SOURCE_COLUMN_DATATYPE;
                this.DESTINATION_COLUMN_DATATYPE = DESTINATION_COLUMN_DATATYPE;
                this.COLUMN_PRECISION = COLUMN_PRECISION;
                this.COLUMN_SCALE = COLUMN_SCALE;
                this.COLUMN_ISNULL = COLUMN_ISNULL;
            }
            public string TABLE_NAME { set; get; }
            public string COLUMN_NAME { set; get; }
            public string SOURCE_COLUMN_DATATYPE { set; get; }
            public string DESTINATION_COLUMN_DATATYPE { set; get; }
            public string COLUMN_PRECISION { set; get; }
            public string COLUMN_SCALE { set; get; }
            public string COLUMN_ISNULL { set; get; }
        }

        public string GetSQLDataType(string postgresqltype, string length, string precision, string scale, string providertype)
        {
            string sqltype = null;
            switch (postgresqltype)
            {
                case "System.Int32":
                    sqltype = "INT";
                    break;
                case "System.Int64":
                    sqltype = "BIGINT";
                    break;
                case "System.String":
                    if (Int32.Parse(length) > 0 && providertype == "varchar")
                    {
                        sqltype = "NVARCHAR(" + length + ")";  //PASS LENGTH
                    }
                    else if (providertype == "text")
                    {
                        sqltype = "NVARCHAR(MAX)";
                    }
                    else
                    {
                        sqltype = "NVARCHAR(255)";   //DEFAULT
                    }
                    break;
                case "System.Boolean[]":
                    sqltype = "BIT";
                    break;
                case "System.DateTime":
                    sqltype = "DATETIME";
                    break;
                case "System.Decimal":
                    if (precision != "0")
                    {
                        sqltype = "DECIMAL(" + precision + "," + scale + ")";
                    }       
                    else
                    {
                        sqltype = "DECIMAL(18,0)"; //DEFAULT
                    }
                        break;
                default:
                    sqltype = "NVARCHAR(255)";   //DEFAULT
                    break;
                    //TEXT , NTEXT and IMAGE data types of SQL Server 2000 will be deprecated in future version of SQL Server, 
                    //SQL Server 2005 provides backward compatibility to data types but it is recommended to use new data types 
                    //which are VARCHAR(MAX) , NVARCHAR(MAX) and VARBINARY(MAX) . ... nvarchar(max) is what you want to be 
            }
            return sqltype;
        }

        public void CreatePackage(string entity)
        {
            try
            {
                Console.Title = "File Loader";
                Console.ForegroundColor = ConsoleColor.Yellow;

                //// Read input parameters
                var file = "C:\\vamshi\\GIT\\SSIS PackageAutomation\\SSISPackageAutomation\\FILELOCATION\\SAMPLEINPUT.txt";
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
                string query = "SELECT TABLE_NAME FROM LOG_SCHEMA_TABLE WHERE TABLE_NAME LIKE '" + entity + "%'";
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

                // Add a Data Flow Task to the Package
                var app = new Application();

                foreach (string TableNm in TableNames)
                {
                    var e = package.Executables.Add("STOCK:PipelineTask");
                    var mainPipe = e as TaskHost;
                    var dataFlowTask = mainPipe.InnerObject as MainPipe;
                    mainPipe.Name = TableNm;
                    if (dataFlowTask != null)
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

                            // Add transform (DFT)
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

                            ////only one column datatype is converted to different datatype
                            //int sourceColumnLineageId = dataConvertVirtualInput.VirtualInputColumnCollection["REMOTE_OBJECT_TYPE"].LineageID;
                            //dataConvertWrapper.SetUsageType(dataConvertComponent.InputCollection[0].ID, dataConvertVirtualInput, sourceColumnLineageId, DTSUsageType.UT_READONLY);
                            //IDTSOutputColumn100 newOutputColumn = dataConvertWrapper.InsertOutputColumnAt(dataConvertOutput.ID, 0, "CNV_REMOTE_OBJECT_TYPE", string.Empty);
                            //newOutputColumn.SetDataTypeProperties(Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR, 255, 0, 0, 0);
                            //newOutputColumn.MappedColumnID = 0;
                            //dataConvertWrapper.SetOutputColumnProperty(dataConvertOutput.ID, newOutputColumn.ID, "SourceInputColumnLineageID", sourceColumnLineageId);

                            // Add an OLE DB Destination Component to the Data Flow
                            var oleDbDestinationComponent = dataFlowTask.ComponentMetaDataCollection.New();
                            oleDbDestinationComponent.Name = "MyOLEDBDestination";
                            oleDbDestinationComponent.ComponentClassID = app.PipelineComponentInfos["OLE DB Destination"].CreationName;

                            // Get the design time instance of the Ole Db Destination component
                            var oleDbDestinationInstance = oleDbDestinationComponent.Instantiate();
                            oleDbDestinationInstance.ProvideComponentProperties();

                            // Set Ole Db Destination Connection
                            oleDbDestinationComponent.RuntimeConnectionCollection[0].ConnectionManagerID = connMgrOleDb.ID;
                            oleDbDestinationComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(connMgrOleDb);

                            // Set destination load type
                            oleDbDestinationInstance.SetComponentProperty("AccessMode", 3);
                            // Now set Ole Db Destination Table name
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
                        }
                }
                var dtsx = new StringBuilder();
                string PackageName = entity + "Tables";
                dtsx.Append(Path.GetDirectoryName(file)).Append("\\").Append(PackageName).Append(".dtsx");
                Console.WriteLine("Saving Package...");
                app.SaveToXml(dtsx.ToString(), package, null);
                Console.WriteLine("Done");
                Console.ReadLine();
                package.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public void Log_TableEntry(string TableName, string Schema, string Status)
        {
            try
            {
                var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "JIRA_ODS"));
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into LOG_SCHEMA_TABLE (TABLE_NAME, SCHEMA_RAW, SCHEMA_STATUS, SCHEMA_ISSUES, CREATION_DATE) values ('" + TableName + "', '" + Schema + "', '" + Status + "', 'No', '" + DateTime.Now + "')", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while inserting into table LOG_SHCEMA_TABLE" + e.Message + "\t" + e.GetType());
            }
          //  Console.ReadKey();
        }

        public void Log_TableColumnEntry(List<ColumnDetails> list)
        {
            try
            {
                var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "JIRA_ODS"));
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO LOG_SCHEMA_COLUMN (TABLE_NAME, COLUMN_NAME, SOURCE_COLUMN_DATATYPE, DESTINATION_COLUMN_DATATYPE, COLUMN_PRECISION, COLUMN_SCALE, COLUMN_ISNULL) VALUES (@param1, @param2, @param3,@param4, @param5, @param6, @param7)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@param1", DbType.String);
                cmd.Parameters.AddWithValue("@param2", DbType.String);
                cmd.Parameters.AddWithValue("@param3", DbType.String);
                cmd.Parameters.AddWithValue("@param4", DbType.String);
                cmd.Parameters.AddWithValue("@param5", DbType.Int16);
                cmd.Parameters.AddWithValue("@param6", DbType.Int16);
                cmd.Parameters.AddWithValue("@param7", DbType.String);
                foreach (var item in list)
                {
                    cmd.Parameters[0].Value = item.TABLE_NAME;
                    cmd.Parameters[1].Value = item.COLUMN_NAME;
                    cmd.Parameters[2].Value = item.SOURCE_COLUMN_DATATYPE;
                    cmd.Parameters[3].Value = item.DESTINATION_COLUMN_DATATYPE;
                    cmd.Parameters[4].Value = item.COLUMN_PRECISION;
                    cmd.Parameters[5].Value = item.COLUMN_SCALE;
                    cmd.Parameters[6].Value = item.COLUMN_ISNULL;
                    cmd.ExecuteNonQuery();
                }
                
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while inserting into table LOG_SCHEMA_COLUMN" + e.Message + "\t" + e.GetType());
            }
        }

        public IDTSComponentMetaData100 AddComponentToDataFlow(MainPipe mp, string Component)
        {
            if (mp != null)
            {
                IDTSComponentMetaData100 md = mp.ComponentMetaDataCollection.New();
                md.ComponentClassID = Component;
                CManagedComponentWrapper wrp = md.Instantiate();
                wrp.ProvideComponentProperties();

                return md;
            }
            throw new Exception("DataFlow task does not exist.");
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