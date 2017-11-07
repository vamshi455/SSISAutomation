﻿using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Npgsql;
using System.Data;

namespace SSISPackageAutomation
{
    class Program
    {
        static void Main(string[] args)
        {

            Program pg = new Program();
           // pg.GetPostGreSQLSchema();
            pg.CreatePackage();
            //commit
            
        }

        public void GetPostGreSQLSchema()
        {
            try
            {
                //get connect to postgresql
                //String connstring = String.Format("Data Source=localhost;User ID=vams3203; Password= 123456; Initial Catalog=NEWDB;Persist Security Info=True;");
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};","localhost", "5432", "vams3203", "123456", "NEWDB");

                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();

                //string sqldrop = ""

                string sql = "SELECT * FROM PRODUCTS where 1 = 0"; //variable 
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable schema = reader.GetSchemaTable();

                //get schema of a table (example)
                var sqlselect = new StringBuilder();

                sqlselect.Append("CREATE TABLE ");
                sqlselect.Append("PRODUCTS");  //variable
                sqlselect.Append(" (");

                int i = 0;

                foreach (DataRow row in schema.Rows)
                {
                    i = i + 1;
                    if (i != schema.Rows.Count)
                    {
                        sqlselect.Append(row["COLUMNNAME"].ToString()+"  ");
                        sqlselect.Append(GetSQLDataType(row["DATATYPE"].ToString() )+ ","); //GetSQLDataType() pass the system datatype to a function and get sql datatype
                    }
                    else
                    {
                        sqlselect.Append(row["COLUMNNAME"].ToString()+"  ");
                        sqlselect.Append(GetSQLDataType(row["DATATYPE"].ToString()) + ")"); //pass the system datatype to a function and get sql datatype
                    }

                    //string ColumnName = row["COLUMNNAME"].ToString();
                    //string DataType = row["DATATYPE"].ToString();
                    //string Lenghth = row["NumericPrecision"].ToString();
                }

                // Create table in destination sql database to hold file data

                //sqlselect.Replace("", "");
                //sqlselect.Replace("System.Int32", "INT");
                //afdsf


                var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "LDAP"));
                var command = new SqlCommand(sql.ToString(), connection);
                // Create table in destination sql database to hold file data
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //build sql schema equivalent to postgresql schema


                //build the table in SQL Server destination database
            }
            catch (Exception ex)
            {

            }
        }

        public string GetSQLDataType(string postgresqltype)
        {
            string sqltype = null;
            switch (postgresqltype)
            {
                case "System.Int32":
                    sqltype = "INT";
                    break;
                case "System.String":
                    sqltype = "NVARCHAR(255)";
                    break;
                default:
                    sqltype = "NVARCHAR(255)";
                    break;
            }
            return sqltype;
        }

        public void CreatePackage()
        {
            try
            {
                Console.Title = "File Loader";
                Console.ForegroundColor = ConsoleColor.Yellow;

                //// Read input parameters
                var file = "C:\\vamshi\\GIT\\SSIS PackageAutomation\\SSISPackageAutomation\\FILELOCATION\\SAMPLEINPUT.txt";
                var server = "ebi-etl-dev-01";
                var database = "LDAP";

                Console.WriteLine("Building Package...");

                // Create a new SSIS Package
                var package = new Package();
                SSISConnection conn = new SSISConnection();
                package.ProtectionLevel = DTSProtectionLevel.DontSaveSensitive;

                //conn.CreateODBCConnection(package);
                ConnectionManager ConMgr;
                ConMgr = package.Connections.Add("ODBC");
                ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                ConMgr.Name = "SSIS Connection Manager for ODBC to connect POSTGRESQL";
                ConMgr.Description = "OLE DB connection to the PostGreSQL Database";
                

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
                connMgrOleDb.Name = "My OLE DB Connection";
                connMgrOleDb.Description = "OLE DB connection";

                // Add a Data Flow Task to the Package
                var e = package.Executables.Add("STOCK:PipelineTask");
                var mainPipe = e as TaskHost;

                if (mainPipe != null)
                {
                    mainPipe.Name = "MyDataFlowTask";
                    var dataFlowTask = mainPipe.InnerObject as MainPipe;
                    var app = new Application();

                    if (dataFlowTask != null)
                    {
                        // Add a Flat ODBC Source Component to the Data Flow Task
                        var ODBCSourceComponent = dataFlowTask.ComponentMetaDataCollection.New();
                        ODBCSourceComponent.Name = "My PostgreSQL Component";
                        ODBCSourceComponent.ComponentClassID = app.PipelineComponentInfos["ODBC Source"].CreationName;// "A77F5655-A006-443A-9B7E-90B6BD55CB84";//"DTSAdapter.ODBCSource";//app.PipelineComponentInfos["ODBC"].CreationName;
                        var ODBCSourceInstance = ODBCSourceComponent.Instantiate();
                        ODBCSourceInstance.ProvideComponentProperties();

                        ODBCSourceComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(ConMgr);
                        ODBCSourceComponent.RuntimeConnectionCollection[0].ConnectionManagerID = ConMgr.ID;
                        ODBCSourceInstance.SetComponentProperty("AccessMode", 2);
                        ODBCSourceInstance.SetComponentProperty("SqlCommand", "SELECT * FROM PRODUCTS");
                        //ODBCSourceInstance.

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

                        //
                        // Configure the transform
                        //

                        IDTSVirtualInput100 dataConvertVirtualInput = dataConvertComponent.InputCollection[0].GetVirtualInput();
                        IDTSOutput100 dataConvertOutput = dataConvertComponent.OutputCollection[0];
                        IDTSOutputColumnCollection100 dataConvertOutputColumns = dataConvertOutput.OutputColumnCollection;

                        //loop through ODBCSourceComponent.OutputCollection[0] FIND THE COLUMNS WHICH NEEDS CONVERSION
                        foreach (IDTSVirtualInputColumn100 vColumn in dataConvertVirtualInput.VirtualInputColumnCollection)
                        {

                        }

                        //only one column datatype is considered
                        int sourceColumnLineageId = dataConvertVirtualInput.VirtualInputColumnCollection["name"].LineageID;
                        dataConvertWrapper.SetUsageType(dataConvertComponent.InputCollection[0].ID, dataConvertVirtualInput, sourceColumnLineageId, DTSUsageType.UT_READONLY);
                        IDTSOutputColumn100 newOutputColumn = dataConvertWrapper.InsertOutputColumnAt(dataConvertOutput.ID, 0, "name_nvarchar", string.Empty);
                        newOutputColumn.SetDataTypeProperties(Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR, 255, 0, 0, 0);
                        newOutputColumn.MappedColumnID = 0;
                        dataConvertWrapper.SetOutputColumnProperty(dataConvertOutput.ID, newOutputColumn.ID, "SourceInputColumnLineageID", sourceColumnLineageId);

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
                        oleDbDestinationInstance.SetComponentProperty("OpenRowset", "Products");

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

                        //configure the destination
                        IDTSInput100 destInput = oleDbDestinationComponent.InputCollection[0];
                        IDTSVirtualInput100 destVirInput = destInput.GetVirtualInput();
                        IDTSInputColumnCollection100 destInputCols = destInput.InputColumnCollection;
                        IDTSExternalMetadataColumnCollection100 destExtCols = destInput.ExternalMetadataColumnCollection;
                        IDTSOutputColumnCollection100 sourceColumns = dataConvertComponent.OutputCollection[0].OutputColumnCollection;
                        IDTSOutputColumnCollection100 excsourceColumns = ODBCSourceComponent.OutputCollection[0].OutputColumnCollection;

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
                        foreach (IDTSOutputColumn100 outputCol in excsourceColumns)
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

                        Console.WriteLine("Executing Package...");
                        package.Execute();

                        var dtsx = new StringBuilder();
                        dtsx.Append(Path.GetDirectoryName(file)).Append("\\").Append(Path.GetFileNameWithoutExtension(file)).Append(".dtsx");

                        Console.WriteLine("Saving Package...");
                        app.SaveToXml(dtsx.ToString(), package, null);
                    }
                }
                package.Dispose();
                Console.WriteLine("Done");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

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

    
}