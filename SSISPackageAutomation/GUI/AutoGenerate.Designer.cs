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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoGenerate));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlPackage = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lstboxEntities = new System.Windows.Forms.ListView();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_generate_package = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblOutput = new System.Windows.Forms.Label();
            this.gdvw_Output = new System.Windows.Forms.DataGridView();
            this.btn_GenerateSchema = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmb_destServer = new System.Windows.Forms.ComboBox();
            this.chkDestWindowsAuth = new System.Windows.Forms.CheckBox();
            this.btnDestConnection = new System.Windows.Forms.Button();
            this.txtDestPwd = new System.Windows.Forms.TextBox();
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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2.SuspendLayout();
            this.pnlPackage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvw_Output)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlPackage);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.lblOutput);
            this.panel2.Controls.Add(this.gdvw_Output);
            this.panel2.Controls.Add(this.btn_GenerateSchema);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(9, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(942, 598);
            this.panel2.TabIndex = 3;
            // 
            // pnlPackage
            // 
            this.pnlPackage.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlPackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPackage.Controls.Add(this.button1);
            this.pnlPackage.Controls.Add(this.lstboxEntities);
            this.pnlPackage.Controls.Add(this.checkedListBox1);
            this.pnlPackage.Controls.Add(this.label9);
            this.pnlPackage.Controls.Add(this.label8);
            this.pnlPackage.Controls.Add(this.button2);
            this.pnlPackage.Controls.Add(this.btn_generate_package);
            this.pnlPackage.Controls.Add(this.txtFolderPath);
            this.pnlPackage.Font = new System.Drawing.Font("Verdana", 8F);
            this.pnlPackage.Location = new System.Drawing.Point(11, 210);
            this.pnlPackage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlPackage.Name = "pnlPackage";
            this.pnlPackage.Size = new System.Drawing.Size(920, 139);
            this.pnlPackage.TabIndex = 28;
            this.pnlPackage.Tag = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(848, 94);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 19);
            this.button1.TabIndex = 30;
            this.button1.Text = "clear all";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstboxEntities
            // 
            this.lstboxEntities.GridLines = true;
            this.lstboxEntities.Location = new System.Drawing.Point(365, 11);
            this.lstboxEntities.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstboxEntities.Name = "lstboxEntities";
            this.lstboxEntities.Size = new System.Drawing.Size(549, 80);
            this.lstboxEntities.TabIndex = 29;
            this.lstboxEntities.UseCompatibleStateImageBehavior = false;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(92, 11);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(248, 64);
            this.checkedListBox1.TabIndex = 27;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label9.Location = new System.Drawing.Point(3, 39);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Select Entity:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label8.Location = new System.Drawing.Point(7, 88);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Folder:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button2.Location = new System.Drawing.Point(336, 88);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 16);
            this.button2.TabIndex = 22;
            this.button2.Text = "...";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btn_generate_package
            // 
            this.btn_generate_package.Font = new System.Drawing.Font("Verdana", 8F);
            this.btn_generate_package.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_generate_package.Location = new System.Drawing.Point(365, 107);
            this.btn_generate_package.Margin = new System.Windows.Forms.Padding(1);
            this.btn_generate_package.Name = "btn_generate_package";
            this.btn_generate_package.Size = new System.Drawing.Size(166, 24);
            this.btn_generate_package.TabIndex = 15;
            this.btn_generate_package.Text = "Generate Package";
            this.btn_generate_package.UseVisualStyleBackColor = true;
            this.btn_generate_package.Click += new System.EventHandler(this.btnGeneratePackage_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFolderPath.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolderPath.Location = new System.Drawing.Point(92, 88);
            this.txtFolderPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFolderPath.Multiline = true;
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(247, 16);
            this.txtFolderPath.TabIndex = 21;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(785, 368);
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
            this.lblOutput.Location = new System.Drawing.Point(11, 368);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(0, 13);
            this.lblOutput.TabIndex = 18;
            this.lblOutput.Click += new System.EventHandler(this.label7_Click);
            // 
            // gdvw_Output
            // 
            this.gdvw_Output.BackgroundColor = System.Drawing.Color.Silver;
            this.gdvw_Output.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gdvw_Output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvw_Output.Location = new System.Drawing.Point(14, 392);
            this.gdvw_Output.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gdvw_Output.Name = "gdvw_Output";
            this.gdvw_Output.RowTemplate.Height = 24;
            this.gdvw_Output.Size = new System.Drawing.Size(916, 205);
            this.gdvw_Output.TabIndex = 16;
            // 
            // btn_GenerateSchema
            // 
            this.btn_GenerateSchema.Font = new System.Drawing.Font("Verdana", 8F);
            this.btn_GenerateSchema.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_GenerateSchema.Location = new System.Drawing.Point(378, 172);
            this.btn_GenerateSchema.Margin = new System.Windows.Forms.Padding(0);
            this.btn_GenerateSchema.Name = "btn_GenerateSchema";
            this.btn_GenerateSchema.Size = new System.Drawing.Size(166, 28);
            this.btn_GenerateSchema.TabIndex = 14;
            this.btn_GenerateSchema.Text = "Generate Schema";
            this.btn_GenerateSchema.UseVisualStyleBackColor = true;
            this.btn_GenerateSchema.Click += new System.EventHandler(this.btnGenerateSchema_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.cmb_destServer);
            this.panel3.Controls.Add(this.chkDestWindowsAuth);
            this.panel3.Controls.Add(this.btnDestConnection);
            this.panel3.Controls.Add(this.txtDestPwd);
            this.panel3.Controls.Add(this.txtDestUserID);
            this.panel3.Controls.Add(this.txtDestPort);
            this.panel3.Controls.Add(this.txtDestDB);
            this.panel3.Controls.Add(this.txtDestServer);
            this.panel3.Controls.Add(this.lblDestPwd);
            this.panel3.Controls.Add(this.lblDestUserID);
            this.panel3.Controls.Add(this.lblDestPort);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(465, 15);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(466, 155);
            this.panel3.TabIndex = 13;
            // 
            // cmb_destServer
            // 
            this.cmb_destServer.DisplayMember = "PostGreSQL";
            this.cmb_destServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_destServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmb_destServer.FormattingEnabled = true;
            this.cmb_destServer.Items.AddRange(new object[] {
            "PostGreSQL",
            "MySQL",
            "Oracle",
            "SQL Server"});
            this.cmb_destServer.Location = new System.Drawing.Point(94, 6);
            this.cmb_destServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmb_destServer.Name = "cmb_destServer";
            this.cmb_destServer.Size = new System.Drawing.Size(188, 21);
            this.cmb_destServer.TabIndex = 14;
            // 
            // chkDestWindowsAuth
            // 
            this.chkDestWindowsAuth.AutoSize = true;
            this.chkDestWindowsAuth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDestWindowsAuth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDestWindowsAuth.ForeColor = System.Drawing.SystemColors.InfoText;
            this.chkDestWindowsAuth.Location = new System.Drawing.Point(214, 76);
            this.chkDestWindowsAuth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkDestWindowsAuth.Name = "chkDestWindowsAuth";
            this.chkDestWindowsAuth.Size = new System.Drawing.Size(161, 17);
            this.chkDestWindowsAuth.TabIndex = 13;
            this.chkDestWindowsAuth.Text = "Windows Authentication";
            this.chkDestWindowsAuth.UseVisualStyleBackColor = true;
            this.chkDestWindowsAuth.CheckedChanged += new System.EventHandler(this.chkDestWindowsAuth_CheckedChanged);
            // 
            // btnDestConnection
            // 
            this.btnDestConnection.Font = new System.Drawing.Font("Verdana", 8F);
            this.btnDestConnection.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDestConnection.Location = new System.Drawing.Point(332, 117);
            this.btnDestConnection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDestConnection.Name = "btnDestConnection";
            this.btnDestConnection.Size = new System.Drawing.Size(81, 20);
            this.btnDestConnection.TabIndex = 12;
            this.btnDestConnection.Text = "Test";
            this.btnDestConnection.UseVisualStyleBackColor = true;
            this.btnDestConnection.Click += new System.EventHandler(this.btnDestConnection_Click);
            // 
            // txtDestPwd
            // 
            this.txtDestPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestPwd.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestPwd.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtDestPwd.Location = new System.Drawing.Point(94, 121);
            this.txtDestPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDestPwd.Multiline = true;
            this.txtDestPwd.Name = "txtDestPwd";
            this.txtDestPwd.PasswordChar = '*';
            this.txtDestPwd.Size = new System.Drawing.Size(141, 16);
            this.txtDestPwd.TabIndex = 7;
            // 
            // txtDestUserID
            // 
            this.txtDestUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestUserID.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestUserID.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtDestUserID.Location = new System.Drawing.Point(94, 101);
            this.txtDestUserID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDestUserID.Multiline = true;
            this.txtDestUserID.Name = "txtDestUserID";
            this.txtDestUserID.Size = new System.Drawing.Size(141, 16);
            this.txtDestUserID.TabIndex = 8;
            // 
            // txtDestPort
            // 
            this.txtDestPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestPort.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestPort.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtDestPort.Location = new System.Drawing.Point(94, 76);
            this.txtDestPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDestPort.Multiline = true;
            this.txtDestPort.Name = "txtDestPort";
            this.txtDestPort.Size = new System.Drawing.Size(53, 16);
            this.txtDestPort.TabIndex = 9;
            // 
            // txtDestDB
            // 
            this.txtDestDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestDB.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestDB.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtDestDB.Location = new System.Drawing.Point(94, 54);
            this.txtDestDB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDestDB.Multiline = true;
            this.txtDestDB.Name = "txtDestDB";
            this.txtDestDB.Size = new System.Drawing.Size(256, 16);
            this.txtDestDB.TabIndex = 10;
            this.txtDestDB.TextChanged += new System.EventHandler(this.txtDestDB_TextChanged);
            // 
            // txtDestServer
            // 
            this.txtDestServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtDestServer.Location = new System.Drawing.Point(94, 31);
            this.txtDestServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDestServer.Multiline = true;
            this.txtDestServer.Name = "txtDestServer";
            this.txtDestServer.Size = new System.Drawing.Size(319, 16);
            this.txtDestServer.TabIndex = 11;
            // 
            // lblDestPwd
            // 
            this.lblDestPwd.AutoSize = true;
            this.lblDestPwd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestPwd.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblDestPwd.Location = new System.Drawing.Point(8, 121);
            this.lblDestPwd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestPwd.Name = "lblDestPwd";
            this.lblDestPwd.Size = new System.Drawing.Size(66, 13);
            this.lblDestPwd.TabIndex = 2;
            this.lblDestPwd.Text = "Password:";
            // 
            // lblDestUserID
            // 
            this.lblDestUserID.AutoSize = true;
            this.lblDestUserID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestUserID.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblDestUserID.Location = new System.Drawing.Point(8, 102);
            this.lblDestUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestUserID.Name = "lblDestUserID";
            this.lblDestUserID.Size = new System.Drawing.Size(70, 13);
            this.lblDestUserID.TabIndex = 3;
            this.lblDestUserID.Text = "Username:";
            // 
            // lblDestPort
            // 
            this.lblDestPort.AutoSize = true;
            this.lblDestPort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestPort.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblDestPort.Location = new System.Drawing.Point(8, 80);
            this.lblDestPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDestPort.Name = "lblDestPort";
            this.lblDestPort.Size = new System.Drawing.Size(35, 13);
            this.lblDestPort.TabIndex = 4;
            this.lblDestPort.Text = "Port:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label10.Location = new System.Drawing.Point(8, 55);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Database:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label11.Location = new System.Drawing.Point(8, 31);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Sever:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label12.Location = new System.Drawing.Point(8, 10);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Destination:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmb_SrcServer);
            this.panel1.Controls.Add(this.btnSourceconnection);
            this.panel1.Controls.Add(this.txtSrcPwd);
            this.panel1.Controls.Add(this.txtSrcUserID);
            this.panel1.Controls.Add(this.txtSrcPort);
            this.panel1.Controls.Add(this.txtSrcDB);
            this.panel1.Controls.Add(this.txtSrcServer);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Location = new System.Drawing.Point(11, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 155);
            this.panel1.TabIndex = 3;
            // 
            // cmb_SrcServer
            // 
            this.cmb_SrcServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_SrcServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cmb_SrcServer.FormattingEnabled = true;
            this.cmb_SrcServer.ItemHeight = 13;
            this.cmb_SrcServer.Items.AddRange(new object[] {
            "PostGreSQL",
            "MySQL",
            "Oracle",
            "SQL Server"});
            this.cmb_SrcServer.Location = new System.Drawing.Point(85, 6);
            this.cmb_SrcServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmb_SrcServer.Name = "cmb_SrcServer";
            this.cmb_SrcServer.Size = new System.Drawing.Size(203, 21);
            this.cmb_SrcServer.TabIndex = 13;
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
            this.btnSourceconnection.Location = new System.Drawing.Point(317, 117);
            this.btnSourceconnection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSourceconnection.Name = "btnSourceconnection";
            this.btnSourceconnection.Size = new System.Drawing.Size(81, 20);
            this.btnSourceconnection.TabIndex = 12;
            this.btnSourceconnection.Text = "Test";
            this.btnSourceconnection.UseVisualStyleBackColor = true;
            this.btnSourceconnection.Click += new System.EventHandler(this.btnSrcTestConnection_Click);
            // 
            // txtSrcPwd
            // 
            this.txtSrcPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcPwd.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcPwd.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSrcPwd.Location = new System.Drawing.Point(85, 119);
            this.txtSrcPwd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSrcPwd.Multiline = true;
            this.txtSrcPwd.Name = "txtSrcPwd";
            this.txtSrcPwd.PasswordChar = '*';
            this.txtSrcPwd.Size = new System.Drawing.Size(135, 16);
            this.txtSrcPwd.TabIndex = 7;
            // 
            // txtSrcUserID
            // 
            this.txtSrcUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcUserID.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcUserID.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSrcUserID.Location = new System.Drawing.Point(85, 98);
            this.txtSrcUserID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSrcUserID.Multiline = true;
            this.txtSrcUserID.Name = "txtSrcUserID";
            this.txtSrcUserID.Size = new System.Drawing.Size(135, 16);
            this.txtSrcUserID.TabIndex = 8;
            // 
            // txtSrcPort
            // 
            this.txtSrcPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcPort.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcPort.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSrcPort.Location = new System.Drawing.Point(85, 75);
            this.txtSrcPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSrcPort.Multiline = true;
            this.txtSrcPort.Name = "txtSrcPort";
            this.txtSrcPort.Size = new System.Drawing.Size(58, 16);
            this.txtSrcPort.TabIndex = 9;
            // 
            // txtSrcDB
            // 
            this.txtSrcDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcDB.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcDB.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSrcDB.Location = new System.Drawing.Point(85, 52);
            this.txtSrcDB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSrcDB.Multiline = true;
            this.txtSrcDB.Name = "txtSrcDB";
            this.txtSrcDB.Size = new System.Drawing.Size(202, 16);
            this.txtSrcDB.TabIndex = 10;
            // 
            // txtSrcServer
            // 
            this.txtSrcServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrcServer.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcServer.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSrcServer.Location = new System.Drawing.Point(85, 29);
            this.txtSrcServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSrcServer.Multiline = true;
            this.txtSrcServer.Name = "txtSrcServer";
            this.txtSrcServer.Size = new System.Drawing.Size(314, 16);
            this.txtSrcServer.TabIndex = 11;
            this.txtSrcServer.TextChanged += new System.EventHandler(this.txtSrcServer_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(3, 121);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label5.Location = new System.Drawing.Point(3, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label4.Location = new System.Drawing.Point(3, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGray;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // AutoGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(960, 618);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AutoGenerate";
            this.Text = "Database Schema & SSIS Package Generator Enterprise 1.0.1.45 (32-bit) (TRIAL)";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlPackage.ResumeLayout(false);
            this.pnlPackage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvw_Output)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_generate_package;
        private System.Windows.Forms.Button btn_GenerateSchema;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDestConnection;
        private System.Windows.Forms.TextBox txtDestPwd;
        private System.Windows.Forms.TextBox txtDestUserID;
        private System.Windows.Forms.TextBox txtDestPort;
        private System.Windows.Forms.TextBox txtDestDB;
        private System.Windows.Forms.TextBox txtDestServer;
        private System.Windows.Forms.Label lblDestPwd;
        private System.Windows.Forms.Label lblDestUserID;
        private System.Windows.Forms.Label lblDestPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkDestWindowsAuth;
        private System.Windows.Forms.DataGridView gdvw_Output;
        private System.Windows.Forms.ComboBox cmb_destServer;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_SrcServer;
        private System.Windows.Forms.Button btnSourceconnection;
        private System.Windows.Forms.TextBox txtSrcPwd;
        private System.Windows.Forms.TextBox txtSrcUserID;
        private System.Windows.Forms.TextBox txtSrcPort;
        private System.Windows.Forms.TextBox txtSrcDB;
        private System.Windows.Forms.TextBox txtSrcServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label9;
        private CheckedListBox checkedListBox1;
        private Panel pnlPackage;
        private ListView lstboxEntities;
        private Button button1;
    }
}

