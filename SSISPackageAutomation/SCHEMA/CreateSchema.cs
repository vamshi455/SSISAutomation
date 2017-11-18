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
    public class CreateSchema
    {
        static void Main(string[] args)
        {
            try
            {
                CreateSchema pg = new CreateSchema();
                //pg.GetPostGreSQLSchema();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot initiate schema generation" + ex.Message + "\t" + ex.GetType());
            }
            //commit
        }

        public int GetPostGreSQLSchema(string server, string port, string userID, string password, string database)
        {
            try
            {
                //get connect to postgresql
                //String connstring = String.Format("Server ={0}; Port ={1}; User Id = {2}; Password ={3}; Database ={4};","localhost","5432", "vams3203", "123456", "NEWDB");
                // string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "d-db1n2.shr.ord1.corp.rackspace.net", "5432", "vams3203", "", "jira_staging_ebi"); //source

                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", server, port, userID, password, database); //source
               
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
                                sqlselect.Append("[" + row["COLUMNNAME"].ToString() + "]" + "  ");
                                SQLDatatype = GetSQLDataType(row["DATATYPE"].ToString(), row["COLUMNSIZE"].ToString(), row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["PROVIDERTYPE"].ToString());
                                sqlselect.Append(SQLDatatype + ","); //GetSQLDataType() pass the system datatype to a function and get sql datatype
                                lstColumnDetails.Add(new ColumnDetails(TableNm, row["COLUMNNAME"].ToString(), row["DATATYPE"].ToString(), SQLDatatype, row["NUMERICPRECISION"].ToString(), row["NUMERICSCALE"].ToString(), row["AllowDBNull"].ToString()));
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
                        
                        Log_TableEntry(TableNm, sqlselect.ToString(), "Success");
                        Log_TableColumnEntry(lstColumnDetails);
                        Console.WriteLine("Success, Created Table: " + TableNm);
                        connection.Close();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        Log_TableEntry(TableNm, sqlselect.ToString(), "Failure");
                        return 0;
                        //Console.WriteLine("Failed, Creating Table: " + TableNm);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hey der...Exception Occured while inserting into table LOG_SHCEMA_TABLE: " + e.Message + "\t" + e.GetType() + "Press any key to close....");
                Console.ReadKey();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception Occre while inserting into table LOG_SCHEMA_COLUMN" + e.Message + "\t" + e.GetType());
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
    }
}
