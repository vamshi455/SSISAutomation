using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using SSISPackageAutomation;

namespace GUI
{
    public partial class AutoGenerate : Form
    {
        string Source_connectionstring = "";
        string Destination_connectionstring = "";

        public AutoGenerate()
        {
            InitializeComponent();
            cmb_SrcServer.SelectedIndex = 0;
            //txtSrcServer.Text = "d-db1n2.shr.ord1.corp.rackspace.net";
            //txtSrcDB.Text = "jira_staging_ebi";
            //txtSrcPort.Text = "5432";
            //txtSrcUserID.Text = "vams3203";
            //txtSrcPwd.Text = "";

            cmb_destServer.SelectedIndex = 3;
            txtDestServer.Text = "ebi-etl-dev-01";
            txtDestDB.Text = "JIRA_ODS";
            chkDestWindowsAuth.Text = "Windows Authentication";
            chkDestWindowsAuth.Checked = true;

            Source_connectionstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", txtSrcServer.Text, txtSrcPort.Text, txtSrcUserID.Text, txtSrcPwd.Text, txtSrcDB.Text); //source
            if (chkDestWindowsAuth.Checked)
            {
                Destination_connectionstring = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", txtDestServer.Text, txtDestDB.Text); //source
            }
            else
            {
                Destination_connectionstring = String.Format("Data Source={0};Initial Catalog={1};User Id={2}; password= {3}", txtDestServer.Text, txtDestDB.Text, txtDestUserID.Text, txtDestPwd.Text); //source
            }
            populateComboDropdown();

        }
        List<string> EntityListGlobal = new List<string>();

        private void populateComboDropdown()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Destination_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT ENTITY FROM (SELECT 'ALL' ENTITY  UNION ALL SELECT SUBSTRING(TABLE_NAME,0,5) ENTITY FROM LOG_SCHEMA_TABLE GROUP BY SUBSTRING(TABLE_NAME,0,5)) A ORDER BY ENTITY ASC", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                sda.Fill(ds);
                                checkedListBox1.DataSource = ds.Tables[0];
                                checkedListBox1.DisplayMember = "ENTITY";
                                checkedListBox1.ClearSelected();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        static int PackageCount = 0;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerateSchema_Click(object sender, EventArgs e)
        {
            try
            {
                //PostGreSQL
                //MySQL
                //Oracle
                //SQL Server


                //validate connection string:
                Boolean valid = validateInput();
                if (valid)
                {

                    if (cmb_SrcServer.SelectedItem.ToString() == "PostGreSQL")
                    {
                        GetPostGreSQLSchema(txtSrcServer.Text, txtSrcPort.Text, txtSrcUserID.Text, txtSrcPwd.Text, txtSrcDB.Text);
                        gdvw_Output.AutoResizeColumns();
                    }
                    else if (cmb_SrcServer.SelectedItem.ToString() == "MySQL")
                    {
                        GetMySQLSchema(txtSrcServer.Text, txtSrcPort.Text, txtSrcUserID.Text, txtSrcPwd.Text, txtSrcDB.Text);
                    }

                    var connection = new SqlConnection(Destination_connectionstring);
                    var command = new SqlCommand("SELECT  TABLE_NAME, SCHEMA_STATUS, SCHEMA_ISSUES, SUPPORTS_INCREMENTAL, SCHEMA_RAW  FROM  LOG_SCHEMA_TABLE", connection);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lblOutput.Text = "";
                    gdvw_Output.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Refresh();
                lblOutput.Text = "Cannot initiate schema generation" + ex.Message + "\t" + ex.GetType();
            }
        }

        private Boolean validateInput()
        {
            if (txtSrcServer.Text == "" || txtSrcDB.Text == "" || txtSrcPort.Text == "" || txtSrcUserID.Text == "")
            {
                MessageBox.Show("Please enter all the fields");
                return false;
            }
            else
            {
                if (cmb_SrcServer.SelectedItem == "PostGreSQL")
                {
                    try
                    {
                        string connstring = Source_connectionstring; //source
                        NpgsqlConnection conn = new NpgsqlConnection(connstring);
                        conn.Open();
                        conn.Close();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Source Connection Failed");
                    }
                }
                return false;
            }
        }

        private void btnGeneratePackage_Click(object sender, EventArgs e)
        {
            if (lstboxEntities.Items.Count == 0 )
            {
                MessageBox.Show("Select Entity");
            }
            else if (txtFolderPath.Text == "")
            {
                MessageBox.Show("Select Folderpath");
            }
            else
            {
                try
                {
                    string Entities = "";
                    int Count = 0;
                    foreach (string item in EntityListGlobal.Distinct())
                    {
                        Count = Count + 1;
                    }

                    int i = 0;
                    if (lstboxEntities.Items.Count > 0)
                    {
                        if (lstboxEntities.Items.ContainsKey("ALL"))
                        {
                            string connectionstring = Destination_connectionstring;
                            //DataRow dr;
                            DataTable dt;
                            using (SqlConnection con = new SqlConnection(connectionstring))
                            {
                                using (SqlCommand cmd = new SqlCommand("SELECT SUBSTRING(TABLE_NAME,0,5) ENTITY FROM LOG_SCHEMA_TABLE GROUP BY SUBSTRING(TABLE_NAME,0,5) ORDER BY  SUBSTRING(TABLE_NAME,0,5) ASC", con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                                    {
                                        using (DataSet ds = new DataSet())
                                        {
                                            sda.Fill(ds);
                                            checkedListBox1.DataSource = ds.Tables[0];
                                            checkedListBox1.DisplayMember = "ENTITY";
                                            checkedListBox1.ClearSelected();
                                            con.Close();
                                            if (ds.Tables.Count > 0)
                                            {
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    foreach(DataRow dr in ds.Tables[0].Rows)
                                                    {
                                                        GeneratePackage_PostgreSQL("", txtFolderPath.Text);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                            
                        }
                        else
                        {
                            foreach (string item in EntityListGlobal.Distinct())
                            {
                                i = i + 1;
                                if (i != Count)
                                {
                                    Entities += "" + item.ToString() + ",";
                                }
                                else
                                {
                                    Entities += item.ToString();
                                }
                            }
                            GeneratePackage_PostgreSQL(Entities, txtFolderPath.Text);
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cannot initiate schema generation" + ex.Message + "\t" + ex.GetType());
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtDestDB_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblOutputText_Click(object sender, EventArgs e)
        {

        }

        public void GetPostGreSQLSchema(string server, string port, string userID, string password, string database)
        {
            try
            {
                //get connect to postgresql
                //String connstring = String.Format("Server ={0}; Port ={1}; User Id = {2}; Password ={3}; Database ={4};","localhost","5432", "vams3203", "123456", "NEWDB");
                // string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "d-db1n2.shr.ord1.corp.rackspace.net", "5432", "vams3203", "", "jira_staging_ebi"); //source
                this.lblOutput.Visible = true;
                this.lblOutput.Text = "Preparing to generate schema...";
                this.lblOutput.ForeColor = System.Drawing.Color.Green;
                string connstring = Source_connectionstring; //source

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

                        var connection = new SqlConnection(Destination_connectionstring);
                        var command = new SqlCommand(sqlselect.ToString(), connection);
                        // Create table in destination sql database to hold file data
                        connection.Open();
                        command.ExecuteNonQuery();

                        Log_TableEntry(TableNm, sqlselect.ToString(), "Success");
                        Log_TableColumnEntry(lstColumnDetails);
                        //Console.WriteLine("Success, Created Table: " + TableNm);
                        this.lblOutput.Refresh();
                        this.lblOutput.Text = "Creating Table: " + TableNm;
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Log_TableEntry(TableNm, sqlselect.ToString(), "Failure");
                        this.lblOutput.Refresh();
                        this.lblOutput.Text = "Creating Table: " + TableNm;
                        //Console.WriteLine("Failed, Creating Table: " + TableNm);
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblOutput.Refresh();
                this.lblOutput.Text = "Failed, Creating Table: " + ex.ToString();
            }
        }

        public void GetMySQLSchema(string server, string port, string userID, string password, string database)
        {
            try
            {
                //get connect to postgresql
                //String connstring = String.Format("Server ={0}; Port ={1}; User Id = {2}; Password ={3}; Database ={4};","localhost","5432", "vams3203", "123456", "NEWDB");
                // string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "d-db1n2.shr.ord1.corp.rackspace.net", "5432", "vams3203", "", "jira_staging_ebi"); //source
                this.lblOutput.Visible = true;
                this.lblOutput.Text = "Preparing to generate schema...";
                this.lblOutput.ForeColor = System.Drawing.Color.Green;
                string connstring = Source_connectionstring; //source

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

                        var connection = new SqlConnection(Destination_connectionstring);
                        var command = new SqlCommand(sqlselect.ToString(), connection);
                        // Create table in destination sql database to hold file data
                        connection.Open();
                        command.ExecuteNonQuery();

                        Log_TableEntry(TableNm, sqlselect.ToString(), "Success");
                        Log_TableColumnEntry(lstColumnDetails);
                        //Console.WriteLine("Success, Created Table: " + TableNm);
                        this.lblOutput.Refresh();
                        this.lblOutput.Text = "Creating Table: " + TableNm;
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Log_TableEntry(TableNm, sqlselect.ToString(), "Failure");
                        this.lblOutput.Refresh();
                        this.lblOutput.Text = "Creating Table: " + TableNm;
                        //Console.WriteLine("Failed, Creating Table: " + TableNm);
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblOutput.Refresh();
                this.lblOutput.Text = "Failed, Creating Table: " + ex.ToString();
            }
        }

        public void Log_TableEntry(string TableName, string Schema, string Status)
        {
            try
            {
                var connection = new SqlConnection(Destination_connectionstring);
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
                var connection = new SqlConnection(Destination_connectionstring);
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void BindColumnSchema()
        {
            try
            {
                //show the log shcema in datagrid  
                var connection = new SqlConnection(Destination_connectionstring);
                var command = new SqlCommand("SELECT  TABLE_NAME, COLUMN_NAME, SOURCE_COLUMN_DATATYPE, DESTINATION_COLUMN_DATATYPE, COLUMN_PRECISION AS SOURCE_PRECISION, COLUMN_SCALE AS SOURCE_SCALE, COLUMN_ISNULL FROM  LOG_SCHEMA_COLUMN", connection);
                // Create table in destination sql database to hold file data
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblOutput.Text = "";
                gdvw_Output.DataSource = dt;
                gdvw_Output.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                lblOutput.Refresh();
                lblOutput.Text = "Cannot get the mappings:" + ex.Message + "\t" + ex.GetType();
            }
        }

        private void ExportToExcel()
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {

                worksheet = workbook.ActiveSheet;
                worksheet.Name = "ExportedFromDatGrid";
                int cellRowIndex = 1;
                int cellColumnIndex = 1;
                //Loop through each row and read value from each column. 
                for (int i = 0; i < gdvw_Output.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < gdvw_Output.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = gdvw_Output.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = gdvw_Output.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private List<String> GetTableNames(string entity)
        {
            List<string> names = entity.Split(',').ToList<string>();
            names.Reverse();
            var connection = new SqlConnection(Destination_connectionstring);
            connection.Open();
            List<String> TableNames = new List<String>();
            string query = "";
            foreach (var item in names)
            {
                query = "SELECT DISTINCT TABLE_NAME FROM LOG_SCHEMA_TABLE WHERE TABLE_NAME like '" + item + "%'";
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
            }
            connection.Close();
            return TableNames;
        }

        private void CreateLogTables()
        {
            try
            {
                var connection = new SqlConnection(Destination_connectionstring);
                connection.Open();
                string sqlTablequery = " IF NOT EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'LOG_SCHEMA_TABLE' AND XTYPE = 'U')"
                                     + " CREATE TABLE LOG_SCHEMA_TABLE"
                                     + " ("
                                     + " TABLE_ID INT IDENTITY(1, 1),"
                                     + " TABLE_NAME VARCHAR(100),"
                                     + " SCHEMA_RAW NVARCHAR(MAX),"
                                     + " SCHEMA_STATUS VARCHAR(100) NULL,"
                                     + " SCHEMA_ISSUES NVARCHAR(1000) NULL,"
                                     + " PACKAGE_CREATED VARCHAR(3) NULL,"
                                     + " PACKAGE_ISSUES NVARCHAR(1000) NULL,"
                                     + " SUPPORTS_INCREMENTAL VARCHAR(3) NULL,"
                                     + " CREATION_DATE DATETIME NULL, "
                                     + " UPDATED_DATE DATETIME NULL"
                                     + " )";

                 string sqlColumnquery = " IF NOT EXISTS(SELECT * FROM SYSOBJECTS WHERE NAME = 'LOG_SCHEMA_COLUMN' AND XTYPE = 'U' )"
                                     + " CREATE TABLE LOG_SCHEMA_COLUMN "
                                     + " ( "
                                     + " TABLE_COLUMN_ID INT IDENTITY(1, 1), "
                                     + " TABLE_NAME VARCHAR(100),"
                                     + " COLUMN_NAME VARCHAR(100),"
                                     + " SOURCE_COLUMN_DATATYPE VARCHAR(100),"
                                     + " DESTINATION_COLUMN_DATATYPE VARCHAR(100),"
                                     + " COLUMN_PRECISION INT NULL,"
                                     + " COLUMN_SCALE INT NULL,"
                                     + " COLUMN_ISNULL VARCHAR(10),"
                                     + " COLUMN_1 VARCHAR(100) NULL,"
                                     + " COLUMN_2 VARCHAR(100) NULL,"
                                     + " COLUMN_3 VARCHAR(100) NULL"
                                     + " )";
                SqlCommand cmd = new SqlCommand(sqlTablequery);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                
                SqlCommand cmdColumn = new SqlCommand(sqlColumnquery);
                cmdColumn.CommandType = CommandType.Text;
                cmdColumn.Connection = connection;
                cmdColumn.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {

            }
        }

        public void GeneratePackage_PostgreSQL(string Entity, string filepath)
        {
            try
            {
                this.lblOutput.Visible = true;
                this.lblOutput.Text = "SSIS Automation: " + Entity + " is in Progress";
                this.lblOutput.ForeColor = System.Drawing.Color.Green;

                CreateLogTables();

                //// Read input parameters
                var server = "ebi-etl-dev-01";
                var database = "JIRA_ODS";

                this.lblOutput.Visible = true;
                this.lblOutput.Text = "Building Package...";
                this.lblOutput.ForeColor = System.Drawing.Color.Green;

                // Create a new SSIS Package
                var package = new Package();

                SSISConnection conn = new SSISConnection();
                package.ProtectionLevel = DTSProtectionLevel.DontSaveSensitive;

                //conn.CreateODBCConnection(package);
                ConnectionManager ConMgr;
                ConMgr = package.Connections.Add("ODBC");
                //ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=" + txtSrcServer.Text + ";uid=" + txtSrcUserID + ";database=" + txtSrcDB.Text + ";port=" + txtSrcPort.Text + ";sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                ConMgr.Name = "ODBC_SRC_" + txtSrcDB.Text;
                ConMgr.Description = "ODBC Connection for PostGreSQL Database";

                //if (cmb_SrcServer.Text == "PostGreSQL")
                //{
                //    ConMgr = package.Connections.Add("ODBC");
                //    //ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                //    ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=" + txtSrcServer.Text + ";uid=" + txtSrcUserID + ";database=" + txtSrcDB.Text + ";port=" + txtSrcPort.Text + ";sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                //    ConMgr.Name = "ODBC_SRC_" + txtSrcDB.Text;
                //    ConMgr.Description = "ODBC Connection for PostGreSQL Database";
                //}

                //if (cmb_SrcServer.Text == "MySQL")
                //{
                //    ConMgr = package.Connections.Add("ADO");
                //    //ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
                //    ConMgr.ConnectionString = "server=" + txtSrcServer.Text + ";user id=" + txtSrcUserID.Text + ";database=" + txtSrcDB.Text;
                //    ConMgr.Name = "ADO_SRC_" + txtSrcDB.Text;
                //    ConMgr.Description = "ADO Connection for MYSQL Database";
                //}

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

                List<String> TableNames = new List<String>();
                TableNames = GetTableNames(Entity);

                //ADD EXECUTE SQL TASK - FOR TRUNCATING DATA BEFORE LOADING
                Executable eFileTask1 = package.Executables.Add("STOCK:SQLTask");
                TaskHost SQLTask = (TaskHost)(eFileTask1);
                SQLTask.Properties["Name"].SetValue(SQLTask, "Truncate Existing Data in Destination");
                SQLTask.Properties["Description"].SetValue(SQLTask, "Erases existing data in the destination tables before loading");
                SQLTask.Properties["Connection"].SetValue(SQLTask, connMgrOleDb.Name);
                string Truncatescript = GetTruncateScript(TableNames);
                SQLTask.Properties["SqlStatementSource"].SetValue(SQLTask, Truncatescript);

                // Add a Data Flow Task to the Package
                var app = new Microsoft.SqlServer.Dts.Runtime.Application();
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
                                var connection_column = new SqlConnection(Destination_connectionstring);
                                connection_column.Open();
                                List<String> NvarcharColumns = new List<String>();
                                string Query = "SELECT COLUMN_NAME FROM LOG_SCHEMA_COLUMN WHERE TABLE_NAME LIKE '" + TableNm + "' AND DESTINATION_COLUMN_DATATYPE LIKE 'NVARCHAR%' AND DESTINATION_COLUMN_DATATYPE <> 'NVARCHAR(MAX)'";
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
                                this.lblOutput.Visible = true;
                                this.lblOutput.Text = "Hey der...Exception Occured while creating DFT: " + ex.Message + "\t" + ex.GetType() + "Press any key to close....";
                                this.lblOutput.ForeColor = System.Drawing.Color.Red;
                                //Console.ReadKey();
                            }
                        }
                    }

                    var dtsx = new StringBuilder();
                    string PackageName = Entity + "_Tables_Load";
                    dtsx.Append(filepath).Append("\\").Append(PackageName).Append(".dtsx");
                    //Console.WriteLine("Saving Package...");
                    this.lblOutput.Visible = true;
                    this.lblOutput.Text = "Saving Package...";
                    this.lblOutput.ForeColor = System.Drawing.Color.Green;
                    
                    app.SaveToXml(dtsx.ToString(), package, null);

                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine("Done, Packages are successfully created in the following path:   " + filepath);

                    this.lblOutput.Visible = true;
                    this.lblOutput.Text = "Packages are successfully created in the following path: " + filepath;
                    this.lblOutput.ForeColor = System.Drawing.Color.Green;
                   
                    //Brief completion statistics - Number of packages generated:
                    PackageCount = PackageCount + 1;
                    package.Dispose();
                }
                else
                {
                    this.lblOutput.Visible = true;
                    this.lblOutput.Text = "Internal Error: Cannot generate package due to entities not returning tables:   " + filepath;
                    this.lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                this.lblOutput.Visible = true;
                this.lblOutput.Text = "Hey der...Exception Occured while saving the package: " + ex.Message + "\t" + ex.GetType() + "Press any key to close....";
                this.lblOutput.ForeColor = System.Drawing.Color.Red;
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }
       
        private void txtSrcServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BindColumnSchema();
            ExportToExcel();
        }

        private String GetEntityTables(String Entity)
        {
            string TablesCommanSeperated = "";
            string connectionstring = Destination_connectionstring;
            SqlConnection sqlcon = new SqlConnection(connectionstring);
            string sqlcmd = "";
            if (Entity == "ALL")
            {
                 sqlcmd = "SELECT DISTINCT TABLE_NAME FROM LOG_SCHEMA_TABLE";
            }
            else
            {
                sqlcmd = "SELECT DISTINCT TABLE_NAME FROM LOG_SCHEMA_TABLE WHERE TABLE_NAME LIKE '" + Entity + "%'";
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd,sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TablesCommanSeperated = TablesCommanSeperated + ds.Tables[0].Rows[i]["TABLE_NAME"].ToString();
                    TablesCommanSeperated += (i < ds.Tables[0].Rows.Count) ? "," : string.Empty;
                }
                return TablesCommanSeperated;
            }
            return TablesCommanSeperated;
        }

        private void lstboxEntities_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            e.ItemHeight = 30;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstboxEntities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnlPackage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gdvw_Output_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string[] lines3 = new string[100];

            lstboxEntities.Clear();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                var row = (item as DataRowView).Row;
                String Entity = row.ItemArray[0].ToString();
                ListViewItem lstVwItem = new ListViewItem();
                lstVwItem = lstboxEntities.FindItemWithText(Entity);
                if (lstVwItem == null)
                {
                    //lstboxEntities.Items.Add();
                    lstboxEntities.Items.Add(new ListViewItem(new[] { GetEntityTables(Entity) }));
                    EntityListGlobal.Add(Entity);
                }
            }
        }

        private void btnSourceconnection_Click(object sender, EventArgs e)
        {
            try
            {
                string connstring = Source_connectionstring; //source
                // string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "d-db1n2.shr.ord1.corp.rackspace.net", "5432", "vams3203", "", "jira_staging_ebi"); //source
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                MessageBox.Show("connection successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("connection failed..");
            }
        }

        private void chkDestWindowsAuth_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkDestWindowsAuth.Checked)
            {
                txtDestPort.Visible = false;
                txtDestUserID.Visible = false;
                txtDestPwd.Visible = false;
                lblDestPort.Visible = false;
                lblDestUserID.Visible = false;
                lblDestPwd.Visible = false;

            }
            else
            {
                txtDestPort.Visible = true;
                txtDestUserID.Visible = true;
                txtDestPwd.Visible = true;
                lblDestPort.Visible = true;
                lblDestUserID.Visible = true;
                lblDestPwd.Visible = true;
            }
        }

        private void btnDestConnection_Click_1(object sender, EventArgs e)
        {
            try
            {
                var connstring = Destination_connectionstring;
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();
                MessageBox.Show("connection successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("connection failed..");
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            lstboxEntities.Items.Clear();
        }
    }
}
