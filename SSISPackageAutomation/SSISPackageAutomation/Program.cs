using System;
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
            //pg.CreatePackage();
            //commit
            pg.GetPostGreSQLSchema();
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

                string sql = "SELECT * FROM PRODUCTS where 1 = 0";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable schema = reader.GetSchemaTable();

                //get schema of a table (example)
                var sqlselect = new StringBuilder();

                sqlselect.Append("CREATE TABLE ");
                sqlselect.Append("PRODUCTS");
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

                // Add a Connection Manager to the Package, of type, FLATFILE
                var connMgrFlatFile = package.Connections.Add("FLATFILE");

                connMgrFlatFile.ConnectionString = file;
                connMgrFlatFile.Name = "My Import File Connection";
                connMgrFlatFile.Description = "Flat File Connection";

                // Get the Column names to be used in configuring the Flat File Connection 
                // by reading the first line of the Import File which contains the Field names
                string[] columns = null;

                using (var stream = new StreamReader(file))
                {
                    var fieldNames = stream.ReadLine();
                    if (fieldNames != null) columns = fieldNames.Split(" ".ToCharArray());
                }

                // Configure Columns and their Properties for the Flat File Connection Manager
                var connMgrFlatFileInnerObj = (Wrapper.IDTSConnectionManagerFlatFile100)connMgrFlatFile.InnerObject;

                connMgrFlatFileInnerObj.RowDelimiter = "\r\n";
                connMgrFlatFileInnerObj.ColumnNamesInFirstDataRow = true;

                if (columns != null)
                {
                    foreach (var column in columns)
                    {
                        // Add a new Column to the Flat File Connection Manager
                        var flatFileColumn = connMgrFlatFileInnerObj.Columns.Add();

                        flatFileColumn.DataType = Wrapper.DataType.DT_WSTR;
                        flatFileColumn.ColumnWidth = 255;

                        flatFileColumn.ColumnDelimiter = columns.GetUpperBound(0) == Array.IndexOf(columns, column) ? "\r\n" : "\t";

                        flatFileColumn.ColumnType = "Delimited";

                        // Use the Import File Field name to name the Column
                        var columnName = flatFileColumn as Wrapper.IDTSName100;
                        if (columnName != null) columnName.Name = column;
                    }

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
                            // Add a Flat File Source Component to the Data Flow Task
                            var flatFileSourceComponent = dataFlowTask.ComponentMetaDataCollection.New();
                            flatFileSourceComponent.Name = "My Flat File Source";
                            flatFileSourceComponent.ComponentClassID = app.PipelineComponentInfos["Flat File Source"].CreationName;

                            // Get the design time instance of the Flat File Source Component
                            var flatFileSourceInstance = flatFileSourceComponent.Instantiate();
                            flatFileSourceInstance.ProvideComponentProperties();

                            flatFileSourceComponent.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.GetExtendedInterface(connMgrFlatFile);
                            flatFileSourceComponent.RuntimeConnectionCollection[0].ConnectionManagerID = connMgrFlatFile.ID;

                            // Reinitialize the metadata.
                            flatFileSourceInstance.AcquireConnections(null);
                            flatFileSourceInstance.ReinitializeMetaData();
                            flatFileSourceInstance.ReleaseConnections();

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

                            // Create table in destination sql database to hold file data
                            var sql = new StringBuilder();

                            sql.Append("CREATE TABLE ");
                            sql.Append(Path.GetFileNameWithoutExtension(file));
                            sql.Append(" (");

                            foreach (var columnName in columns)
                            {
                                if (columns.GetUpperBound(0) == Array.IndexOf(columns, columnName))
                                {
                                    sql.Append(columnName);
                                    sql.Append(" NVARCHAR(255))");
                                }
                                else
                                {
                                    sql.Append(columnName);
                                    sql.Append(" NVARCHAR(255),");
                                }
                            }
                            var connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", server, database));
                            var command = new SqlCommand(sql.ToString(), connection);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();

                            // Now set Ole Db Destination Table name
                            oleDbDestinationInstance.SetComponentProperty("OpenRowset", Path.GetFileNameWithoutExtension(file));

                            // Create a Precedence Constraint between Flat File Source and OLEDB Destination Components
                            var path = dataFlowTask.PathCollection.New();
                            path.AttachPathAndPropagateNotifications(flatFileSourceComponent.OutputCollection[0],
                                oleDbDestinationComponent.InputCollection[0]);

                            // Get the list of available columns
                            var oleDbDestinationInput = oleDbDestinationComponent.InputCollection[0];
                            var oleDbDestinationvInput = oleDbDestinationInput.GetVirtualInput();

                            var oleDbDestinationVirtualInputColumns =
                                oleDbDestinationvInput.VirtualInputColumnCollection;

                            // Reinitialize the metadata
                            oleDbDestinationInstance.AcquireConnections(null);
                            oleDbDestinationInstance.ReinitializeMetaData();
                            oleDbDestinationInstance.ReleaseConnections();

                            // Map Flat File Source Component Output Columns to Ole Db Destination Input Columns
                            foreach (IDTSVirtualInputColumn100 vColumn in oleDbDestinationVirtualInputColumns)
                            {
                                var inputColumn = oleDbDestinationInstance.SetUsageType(oleDbDestinationInput.ID,
                                    oleDbDestinationvInput, vColumn.LineageID, DTSUsageType.UT_READONLY);

                                var externalColumn =
                                    oleDbDestinationInput.ExternalMetadataColumnCollection[inputColumn.Name];

                                oleDbDestinationInstance.MapInputColumn(oleDbDestinationInput.ID, inputColumn.ID, externalColumn.ID);
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
            catch
            {

            }

        }
    }
}