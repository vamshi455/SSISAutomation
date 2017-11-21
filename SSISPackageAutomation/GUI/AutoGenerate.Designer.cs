using System.Windows.Forms;

namespace GUI
{
    partial class AutoGenerate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoGenerate));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_GenerateSchema = new System.Windows.Forms.Button();
            this.gdvw_Output = new System.Windows.Forms.DataGridView();
            this.btn_generate_package = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblOutput = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grppackage = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lstboxEntities = new System.Windows.Forms.ListView();
            this.Entities = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.grpDest = new System.Windows.Forms.GroupBox();
            this.cmb_destServer = new System.Windows.Forms.ComboBox();
            this.chkDestWindowsAuth = new System.Windows.Forms.CheckBox();
            this.btnDestConnection = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txtDestPwd = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtDestUserID = new System.Windows.Forms.TextBox();
            this.txtDestPort = new System.Windows.Forms.TextBox();
            this.txtDestDB = new System.Windows.Forms.TextBox();
            this.txtDestServer = new System.Windows.Forms.TextBox();
            this.lblDestPwd = new System.Windows.Forms.Label();
            this.lblDestUserID = new System.Windows.Forms.Label();
            this.lblDestPort = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_SrcServer = new System.Windows.Forms.ComboBox();
            this.btnSourceconnection = new System.Windows.Forms.Button();
            this.txtSrcPwd = new System.Windows.Forms.TextBox();
            this.txtSrcUserID = new System.Windows.Forms.TextBox();
            this.txtSrcPort = new System.Windows.Forms.TextBox();
            this.txtSrcDB = new System.Windows.Forms.TextBox();
            this.txtSrcServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gdvw_Output)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grppackage.SuspendLayout();
            this.grpDest.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btn_GenerateSchema
            // 
            this.btn_GenerateSchema.Font = new System.Drawing.Font("Verdana", 8F);
            this.btn_GenerateSchema.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_GenerateSchema.Location = new System.Drawing.Point(410, 187);
            this.btn_GenerateSchema.Margin = new System.Windows.Forms.Padding(0);
            this.btn_GenerateSchema.Name = "btn_GenerateSchema";
            this.btn_GenerateSchema.Size = new System.Drawing.Size(166, 24);
            this.btn_GenerateSchema.TabIndex = 14;
            this.btn_GenerateSchema.Text = "Generate Schema";
            this.btn_GenerateSchema.UseVisualStyleBackColor = true;
            this.btn_GenerateSchema.Click += new System.EventHandler(this.btnGenerateSchema_Click);
            // 
            // gdvw_Output
            // 
            this.gdvw_Output.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gdvw_Output.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gdvw_Output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gdvw_Output.DefaultCellStyle = dataGridViewCellStyle2;
            this.gdvw_Output.Location = new System.Drawing.Point(7, 405);
            this.gdvw_Output.Margin = new System.Windows.Forms.Padding(2);
            this.gdvw_Output.Name = "gdvw_Output";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gdvw_Output.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gdvw_Output.RowTemplate.Height = 24;
            this.gdvw_Output.Size = new System.Drawing.Size(994, 200);
            this.gdvw_Output.TabIndex = 16;
            this.gdvw_Output.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvw_Output_CellContentClick);
            // 
            // btn_generate_package
            // 
            this.btn_generate_package.Font = new System.Drawing.Font("Verdana", 8F);
            this.btn_generate_package.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_generate_package.Location = new System.Drawing.Point(420, 330);
            this.btn_generate_package.Margin = new System.Windows.Forms.Padding(1);
            this.btn_generate_package.Name = "btn_generate_package";
            this.btn_generate_package.Size = new System.Drawing.Size(166, 24);
            this.btn_generate_package.TabIndex = 15;
            this.btn_generate_package.Text = "Generate Package";
            this.btn_generate_package.UseVisualStyleBackColor = true;
            this.btn_generate_package.Click += new System.EventHandler(this.btnGeneratePackage_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.lblOutput);
            this.panel4.Location = new System.Drawing.Point(7, 358);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(994, 42);
            this.panel4.TabIndex = 29;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(914, 26);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 13);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "ExportToExcel";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblOutput.Location = new System.Drawing.Point(19, 26);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(0, 13);
            this.lblOutput.TabIndex = 18;
            this.lblOutput.Click += new System.EventHandler(this.label7_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.grppackage);
            this.panel2.Controls.Add(this.grpDest);
            this.panel2.Controls.Add(this.grpSource);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.gdvw_Output);
            this.panel2.Controls.Add(this.btn_GenerateSchema);
            this.panel2.Controls.Add(this.btn_generate_package);
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 618);
            this.panel2.TabIndex = 3;
            // 
            // grppackage
            // 
            this.grppackage.Controls.Add(this.label19);
            this.grppackage.Controls.Add(this.label18);
            this.grppackage.Controls.Add(this.button1);
            this.grppackage.Controls.Add(this.lstboxEntities);
            this.grppackage.Controls.Add(this.checkedListBox1);
            this.grppackage.Controls.Add(this.label9);
            this.grppackage.Controls.Add(this.label8);
            this.grppackage.Controls.Add(this.button2);
            this.grppackage.Controls.Add(this.txtFolderPath);
            this.grppackage.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grppackage.Location = new System.Drawing.Point(3, 214);
            this.grppackage.Name = "grppackage";
            this.grppackage.Size = new System.Drawing.Size(994, 115);
            this.grppackage.TabIndex = 32;
            this.grppackage.TabStop = false;
            this.grppackage.Text = "Package";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.DarkRed;
            this.label19.Location = new System.Drawing.Point(76, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 41;
            this.label19.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.DarkRed;
            this.label18.Location = new System.Drawing.Point(77, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 40;
            this.label18.Text = "*";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(918, 95);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 19);
            this.button1.TabIndex = 39;
            this.button1.Text = "clear all";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lstboxEntities
            // 
            this.lstboxEntities.AutoArrange = false;
            this.lstboxEntities.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lstboxEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Entities});
            this.lstboxEntities.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lstboxEntities.GridLines = true;
            this.lstboxEntities.LabelEdit = true;
            this.lstboxEntities.LabelWrap = false;
            this.lstboxEntities.Location = new System.Drawing.Point(380, 14);
            this.lstboxEntities.Margin = new System.Windows.Forms.Padding(2);
            this.lstboxEntities.Name = "lstboxEntities";
            this.lstboxEntities.Size = new System.Drawing.Size(606, 80);
            this.lstboxEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstboxEntities.TabIndex = 38;
            this.lstboxEntities.UseCompatibleStateImageBehavior = false;
            // 
            // Entities
            // 
            this.Entities.Width = 150;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ForeColor = System.Drawing.Color.DimGray;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(92, 16);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(248, 60);
            this.checkedListBox1.TabIndex = 37;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(5, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Select Entity:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(7, 84);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Folder:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.Location = new System.Drawing.Point(340, 83);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 18);
            this.button2.TabIndex = 34;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFolderPath.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.txtFolderPath.Location = new System.Drawing.Point(92, 84);
            this.txtFolderPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFolderPath.Multiline = true;
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(247, 16);
            this.txtFolderPath.TabIndex = 33;
            // 
            // grpDest
            // 
            this.grpDest.Controls.Add(this.cmb_destServer);
            this.grpDest.Controls.Add(this.chkDestWindowsAuth);
            this.grpDest.Controls.Add(this.btnDestConnection);
            this.grpDest.Controls.Add(this.label21);
            this.grpDest.Controls.Add(this.txtDestPwd);
            this.grpDest.Controls.Add(this.label22);
            this.grpDest.Controls.Add(this.label23);
            this.grpDest.Controls.Add(this.txtDestUserID);
            this.grpDest.Controls.Add(this.txtDestPort);
            this.grpDest.Controls.Add(this.txtDestDB);
            this.grpDest.Controls.Add(this.txtDestServer);
            this.grpDest.Controls.Add(this.lblDestPwd);
            this.grpDest.Controls.Add(this.lblDestUserID);
            this.grpDest.Controls.Add(this.lblDestPort);
            this.grpDest.Controls.Add(this.label10);
            this.grpDest.Controls.Add(this.label11);
            this.grpDest.Controls.Add(this.label12);
            this.grpDest.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDest.Location = new System.Drawing.Point(502, 15);
            this.grpDest.Name = "grpDest";
            this.grpDest.Size = new System.Drawing.Size(495, 169);
            this.grpDest.TabIndex = 31;
            this.grpDest.TabStop = false;
            this.grpDest.Text = "Destination";
            // 
            // cmb_destServer
            // 
            this.cmb_destServer.BackColor = System.Drawing.Color.DarkGray;
            this.cmb_destServer.DisplayMember = "PostGreSQL";
            this.cmb_destServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_destServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmb_destServer.FormattingEnabled = true;
            this.cmb_destServer.Items.AddRange(new object[] {
            "PostGreSQL",
            "MySQL",
            "Oracle",
            "SQL Server"});
            this.cmb_destServer.Location = new System.Drawing.Point(131, 18);
            this.cmb_destServer.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_destServer.Name = "cmb_destServer";
            this.cmb_destServer.Size = new System.Drawing.Size(319, 21);
            this.cmb_destServer.TabIndex = 29;
            // 
            // chkDestWindowsAuth
            // 
            this.chkDestWindowsAuth.AutoSize = true;
            this.chkDestWindowsAuth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDestWindowsAuth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDestWindowsAuth.ForeColor = System.Drawing.Color.Black;
            this.chkDestWindowsAuth.Location = new System.Drawing.Point(289, 88);
            this.chkDestWindowsAuth.Margin = new System.Windows.Forms.Padding(2);
            this.chkDestWindowsAuth.Name = "chkDestWindowsAuth";
            this.chkDestWindowsAuth.Size = new System.Drawing.Size(161, 17);
            this.chkDestWindowsAuth.TabIndex = 36;
            this.chkDestWindowsAuth.Text = "Windows Authentication";
            this.chkDestWindowsAuth.UseVisualStyleBackColor = true;
            this.chkDestWindowsAuth.CheckedChanged += new System.EventHandler(this.chkDestWindowsAuth_CheckedChanged_1);
            // 
            // btnDestConnection
            // 
            this.btnDestConnection.Font = new System.Drawing.Font("Verdana", 8F);
            this.btnDestConnection.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDestConnection.Location = new System.Drawing.Point(276, 131);
            this.btnDestConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnDestConnection.Name = "btnDestConnection";
            this.btnDestConnection.Size = new System.Drawing.Size(81, 20);
            this.btnDestConnection.TabIndex = 35;
            this.btnDestConnection.Text = "Test";
            this.btnDestConnection.UseVisualStyleBackColor = true;
            this.btnDestConnection.Click += new System.EventHandler(this.btnDestConnection_Click_1);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 7F);
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(115, 71);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(12, 12);
            this.label21.TabIndex = 39;
            this.label21.Text = "*";
            // 
            // txtDestPwd
            // 
            this.txtDestPwd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDestPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestPwd.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestPwd.ForeColor = System.Drawing.Color.DimGray;
            this.txtDestPwd.Location = new System.Drawing.Point(131, 133);
            this.txtDestPwd.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestPwd.Multiline = true;
            this.txtDestPwd.Name = "txtDestPwd";
            this.txtDestPwd.PasswordChar = '*';
            this.txtDestPwd.Size = new System.Drawing.Size(141, 16);
            this.txtDestPwd.TabIndex = 34;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 7F);
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(115, 46);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(12, 12);
            this.label22.TabIndex = 38;
            this.label22.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(115, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(12, 12);
            this.label23.TabIndex = 37;
            this.label23.Text = "*";
            // 
            // txtDestUserID
            // 
            this.txtDestUserID.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDestUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestUserID.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestUserID.ForeColor = System.Drawing.Color.DimGray;
            this.txtDestUserID.Location = new System.Drawing.Point(131, 113);
            this.txtDestUserID.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestUserID.Multiline = true;
            this.txtDestUserID.Name = "txtDestUserID";
            this.txtDestUserID.Size = new System.Drawing.Size(141, 16);
            this.txtDestUserID.TabIndex = 33;
            // 
            // txtDestPort
            // 
            this.txtDestPort.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDestPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestPort.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestPort.ForeColor = System.Drawing.Color.DimGray;
            this.txtDestPort.Location = new System.Drawing.Point(131, 88);
            this.txtDestPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestPort.Multiline = true;
            this.txtDestPort.Name = "txtDestPort";
            this.txtDestPort.Size = new System.Drawing.Size(53, 16);
            this.txtDestPort.TabIndex = 32;
            // 
            // txtDestDB
            // 
            this.txtDestDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDestDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestDB.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestDB.ForeColor = System.Drawing.Color.DimGray;
            this.txtDestDB.Location = new System.Drawing.Point(131, 66);
            this.txtDestDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestDB.Multiline = true;
            this.txtDestDB.Name = "txtDestDB";
            this.txtDestDB.Size = new System.Drawing.Size(256, 16);
            this.txtDestDB.TabIndex = 31;
            // 
            // txtDestServer
            // 
            this.txtDestServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDestServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestServer.ForeColor = System.Drawing.Color.DimGray;
            this.txtDestServer.Location = new System.Drawing.Point(131, 43);
            this.txtDestServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestServer.Multiline = true;
            this.txtDestServer.Name = "txtDestServer";
            this.txtDestServer.Size = new System.Drawing.Size(319, 16);
            this.txtDestServer.TabIndex = 30;
            // 
            // lblDestPwd
            // 
            this.lblDestPwd.AutoSize = true;
            this.lblDestPwd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestPwd.ForeColor = System.Drawing.Color.Black;
            this.lblDestPwd.Location = new System.Drawing.Point(34, 133);
            this.lblDestPwd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestPwd.Name = "lblDestPwd";
            this.lblDestPwd.Size = new System.Drawing.Size(66, 13);
            this.lblDestPwd.TabIndex = 24;
            this.lblDestPwd.Text = "Password:";
            // 
            // lblDestUserID
            // 
            this.lblDestUserID.AutoSize = true;
            this.lblDestUserID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestUserID.ForeColor = System.Drawing.Color.Black;
            this.lblDestUserID.Location = new System.Drawing.Point(34, 114);
            this.lblDestUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestUserID.Name = "lblDestUserID";
            this.lblDestUserID.Size = new System.Drawing.Size(70, 13);
            this.lblDestUserID.TabIndex = 25;
            this.lblDestUserID.Text = "Username:";
            // 
            // lblDestPort
            // 
            this.lblDestPort.AutoSize = true;
            this.lblDestPort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestPort.ForeColor = System.Drawing.Color.Black;
            this.lblDestPort.Location = new System.Drawing.Point(34, 91);
            this.lblDestPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestPort.Name = "lblDestPort";
            this.lblDestPort.Size = new System.Drawing.Size(35, 13);
            this.lblDestPort.TabIndex = 26;
            this.lblDestPort.Text = "Port:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(34, 67);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Database:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(34, 43);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Sever:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(34, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Destination:";
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.label17);
            this.grpSource.Controls.Add(this.label16);
            this.grpSource.Controls.Add(this.label15);
            this.grpSource.Controls.Add(this.label14);
            this.grpSource.Controls.Add(this.label13);
            this.grpSource.Controls.Add(this.label7);
            this.grpSource.Controls.Add(this.cmb_SrcServer);
            this.grpSource.Controls.Add(this.btnSourceconnection);
            this.grpSource.Controls.Add(this.txtSrcPwd);
            this.grpSource.Controls.Add(this.txtSrcUserID);
            this.grpSource.Controls.Add(this.txtSrcPort);
            this.grpSource.Controls.Add(this.txtSrcDB);
            this.grpSource.Controls.Add(this.txtSrcServer);
            this.grpSource.Controls.Add(this.label6);
            this.grpSource.Controls.Add(this.label5);
            this.grpSource.Controls.Add(this.label4);
            this.grpSource.Controls.Add(this.label3);
            this.grpSource.Controls.Add(this.label2);
            this.grpSource.Controls.Add(this.label1);
            this.grpSource.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSource.Location = new System.Drawing.Point(11, 15);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(485, 169);
            this.grpSource.TabIndex = 30;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Source";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkRed;
            this.label17.Location = new System.Drawing.Point(89, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 38;
            this.label17.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkRed;
            this.label16.Location = new System.Drawing.Point(89, 108);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 37;
            this.label16.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.DarkRed;
            this.label15.Location = new System.Drawing.Point(89, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DarkRed;
            this.label14.Location = new System.Drawing.Point(89, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkRed;
            this.label13.Location = new System.Drawing.Point(89, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkRed;
            this.label7.Location = new System.Drawing.Point(89, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "*";
            // 
            // cmb_SrcServer
            // 
            this.cmb_SrcServer.BackColor = System.Drawing.Color.DarkGray;
            this.cmb_SrcServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_SrcServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmb_SrcServer.FormattingEnabled = true;
            this.cmb_SrcServer.ItemHeight = 13;
            this.cmb_SrcServer.Items.AddRange(new object[] {
            "PostGreSQL",
            "MySQL",
            "Oracle",
            "SQL Server"});
            this.cmb_SrcServer.Location = new System.Drawing.Point(104, 14);
            this.cmb_SrcServer.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_SrcServer.Name = "cmb_SrcServer";
            this.cmb_SrcServer.Size = new System.Drawing.Size(314, 21);
            this.cmb_SrcServer.TabIndex = 21;
            // 
            // btnSourceconnection
            // 
            this.btnSourceconnection.AutoEllipsis = true;
            this.btnSourceconnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSourceconnection.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSourceconnection.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSourceconnection.FlatAppearance.BorderSize = 2;
            this.btnSourceconnection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSourceconnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSourceconnection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSourceconnection.Font = new System.Drawing.Font("Verdana", 8F);
            this.btnSourceconnection.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnSourceconnection.Location = new System.Drawing.Point(243, 136);
            this.btnSourceconnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnSourceconnection.Name = "btnSourceconnection";
            this.btnSourceconnection.Size = new System.Drawing.Size(81, 20);
            this.btnSourceconnection.TabIndex = 32;
            this.btnSourceconnection.Text = "Test";
            this.btnSourceconnection.UseVisualStyleBackColor = true;
            this.btnSourceconnection.Click += new System.EventHandler(this.btnSourceconnection_Click);
            // 
            // txtSrcPwd
            // 
            this.txtSrcPwd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSrcPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcPwd.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcPwd.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcPwd.Location = new System.Drawing.Point(104, 138);
            this.txtSrcPwd.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrcPwd.Multiline = true;
            this.txtSrcPwd.Name = "txtSrcPwd";
            this.txtSrcPwd.PasswordChar = '*';
            this.txtSrcPwd.Size = new System.Drawing.Size(135, 16);
            this.txtSrcPwd.TabIndex = 30;
            // 
            // txtSrcUserID
            // 
            this.txtSrcUserID.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSrcUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcUserID.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcUserID.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcUserID.Location = new System.Drawing.Point(104, 106);
            this.txtSrcUserID.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrcUserID.Multiline = true;
            this.txtSrcUserID.Name = "txtSrcUserID";
            this.txtSrcUserID.Size = new System.Drawing.Size(135, 16);
            this.txtSrcUserID.TabIndex = 29;
            // 
            // txtSrcPort
            // 
            this.txtSrcPort.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSrcPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcPort.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcPort.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcPort.Location = new System.Drawing.Point(104, 83);
            this.txtSrcPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrcPort.Multiline = true;
            this.txtSrcPort.Name = "txtSrcPort";
            this.txtSrcPort.Size = new System.Drawing.Size(58, 16);
            this.txtSrcPort.TabIndex = 27;
            // 
            // txtSrcDB
            // 
            this.txtSrcDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSrcDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcDB.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcDB.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcDB.Location = new System.Drawing.Point(104, 60);
            this.txtSrcDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrcDB.Multiline = true;
            this.txtSrcDB.Name = "txtSrcDB";
            this.txtSrcDB.Size = new System.Drawing.Size(202, 16);
            this.txtSrcDB.TabIndex = 25;
            // 
            // txtSrcServer
            // 
            this.txtSrcServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSrcServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcServer.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcServer.Location = new System.Drawing.Point(104, 37);
            this.txtSrcServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrcServer.Multiline = true;
            this.txtSrcServer.Name = "txtSrcServer";
            this.txtSrcServer.Size = new System.Drawing.Size(314, 16);
            this.txtSrcServer.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(14, 139);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(14, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(14, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Database:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(14, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Host:";
            // 
            // AutoGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1016, 625);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.DarkRed;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AutoGenerate";
            this.Text = "Database Schema & SSIS Package Generator Enterprise 1.0.1.45 (32-bit) (TRIAL)";
            ((System.ComponentModel.ISupportInitialize)(this.gdvw_Output)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grppackage.ResumeLayout(false);
            this.grppackage.PerformLayout();
            this.grpDest.ResumeLayout(false);
            this.grpDest.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private Button btn_GenerateSchema;
        private DataGridView gdvw_Output;
        private Button btn_generate_package;
        private Panel panel4;
        private LinkLabel linkLabel1;
        private Label lblOutput;
        private Panel panel2;
        private GroupBox grpSource;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label7;
        private ComboBox cmb_SrcServer;
        private Button btnSourceconnection;
        private TextBox txtSrcPwd;
        private TextBox txtSrcUserID;
        private TextBox txtSrcPort;
        private TextBox txtSrcDB;
        private TextBox txtSrcServer;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox grpDest;
        private ComboBox cmb_destServer;
        private CheckBox chkDestWindowsAuth;
        private Button btnDestConnection;
        private Label label21;
        private TextBox txtDestPwd;
        private Label label22;
        private Label label23;
        private TextBox txtDestUserID;
        private TextBox txtDestPort;
        private TextBox txtDestDB;
        private TextBox txtDestServer;
        private Label lblDestPwd;
        private Label lblDestUserID;
        private Label lblDestPort;
        private Label label10;
        private Label label11;
        private Label label12;
        private GroupBox grppackage;
        private Label label19;
        private Label label18;
        private Button button1;
        private ListView lstboxEntities;
        private ColumnHeader Entities;
        private CheckedListBox checkedListBox1;
        private Label label9;
        private Label label8;
        private Button button2;
        private TextBox txtFolderPath;
    }
}

