namespace OPCServer_Simulator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ConnectedClients = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pressTimer = new System.Windows.Forms.Timer(this.components);
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.clrMemBtn = new System.Windows.Forms.Button();
            this.measView = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Measurement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numItemsLbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chngNumItemsBtn = new System.Windows.Forms.Button();
            this.numMeasBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.portActiveStatusLbl = new System.Windows.Forms.Label();
            this.portStatusLbl = new System.Windows.Forms.Label();
            this.closePortBtn = new System.Windows.Forms.Button();
            this.openPortBtn = new System.Windows.Forms.Button();
            this.updatePortsBtn = new System.Windows.Forms.Button();
            this.comPortCombo = new System.Windows.Forms.ComboBox();
            this.selPortLbl = new System.Windows.Forms.Label();
            this.slikServer1 = new NDI.SLIKDA.Interop.SLIKServer();
            this.Panel1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slikServer1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.ConnectedClients);
            this.Panel1.Controls.Add(this.ExitButton);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 429);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(310, 33);
            this.Panel1.TabIndex = 10;
            // 
            // ConnectedClients
            // 
            this.ConnectedClients.AutoSize = true;
            this.ConnectedClients.Location = new System.Drawing.Point(152, 11);
            this.ConnectedClients.Name = "ConnectedClients";
            this.ConnectedClients.Size = new System.Drawing.Size(13, 13);
            this.ConnectedClients.TabIndex = 12;
            this.ConnectedClients.Text = "0";
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.Location = new System.Drawing.Point(227, 6);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(148, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Number of Connected Clients:";
            // 
            // pressTimer
            // 
            this.pressTimer.Interval = 50;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.clrMemBtn);
            this.GroupBox3.Controls.Add(this.measView);
            this.GroupBox3.Location = new System.Drawing.Point(12, 12);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(284, 246);
            this.GroupBox3.TabIndex = 12;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Measurement Data";
            // 
            // clrMemBtn
            // 
            this.clrMemBtn.Location = new System.Drawing.Point(166, 22);
            this.clrMemBtn.Name = "clrMemBtn";
            this.clrMemBtn.Size = new System.Drawing.Size(87, 23);
            this.clrMemBtn.TabIndex = 19;
            this.clrMemBtn.Text = "Clear Memory";
            this.clrMemBtn.UseVisualStyleBackColor = true;
            this.clrMemBtn.Click += new System.EventHandler(this.clrMemBtn_Click);
            // 
            // measView
            // 
            this.measView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Measurement});
            this.measView.Location = new System.Drawing.Point(7, 20);
            this.measView.Name = "measView";
            this.measView.Size = new System.Drawing.Size(271, 218);
            this.measView.TabIndex = 0;
            this.measView.UseCompatibleStateImageBehavior = false;
            this.measView.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 48;
            // 
            // Measurement
            // 
            this.Measurement.Text = "Measurement";
            this.Measurement.Width = 112;
            // 
            // numItemsLbl
            // 
            this.numItemsLbl.AutoSize = true;
            this.numItemsLbl.Location = new System.Drawing.Point(6, 22);
            this.numItemsLbl.Name = "numItemsLbl";
            this.numItemsLbl.Size = new System.Drawing.Size(131, 13);
            this.numItemsLbl.TabIndex = 15;
            this.numItemsLbl.Text = "Number of Measurements:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chngNumItemsBtn);
            this.groupBox1.Controls.Add(this.numMeasBox);
            this.groupBox1.Controls.Add(this.numItemsLbl);
            this.groupBox1.Location = new System.Drawing.Point(12, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 48);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Specifications";
            // 
            // chngNumItemsBtn
            // 
            this.chngNumItemsBtn.Location = new System.Drawing.Point(189, 19);
            this.chngNumItemsBtn.Name = "chngNumItemsBtn";
            this.chngNumItemsBtn.Size = new System.Drawing.Size(84, 20);
            this.chngNumItemsBtn.TabIndex = 17;
            this.chngNumItemsBtn.Text = "Confirm";
            this.chngNumItemsBtn.UseVisualStyleBackColor = true;
            this.chngNumItemsBtn.Click += new System.EventHandler(this.chngNumItemsBtn_Click);
            // 
            // numMeasBox
            // 
            this.numMeasBox.Location = new System.Drawing.Point(143, 19);
            this.numMeasBox.MaxLength = 2;
            this.numMeasBox.Name = "numMeasBox";
            this.numMeasBox.Size = new System.Drawing.Size(39, 20);
            this.numMeasBox.TabIndex = 16;
            this.numMeasBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOnly_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.portActiveStatusLbl);
            this.groupBox2.Controls.Add(this.portStatusLbl);
            this.groupBox2.Controls.Add(this.closePortBtn);
            this.groupBox2.Controls.Add(this.openPortBtn);
            this.groupBox2.Controls.Add(this.updatePortsBtn);
            this.groupBox2.Controls.Add(this.comPortCombo);
            this.groupBox2.Controls.Add(this.selPortLbl);
            this.groupBox2.Location = new System.Drawing.Point(12, 318);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 98);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Micrometer Settings";
            // 
            // portActiveStatusLbl
            // 
            this.portActiveStatusLbl.AutoSize = true;
            this.portActiveStatusLbl.Location = new System.Drawing.Point(71, 79);
            this.portActiveStatusLbl.Name = "portActiveStatusLbl";
            this.portActiveStatusLbl.Size = new System.Drawing.Size(0, 13);
            this.portActiveStatusLbl.TabIndex = 21;
            // 
            // portStatusLbl
            // 
            this.portStatusLbl.AutoSize = true;
            this.portStatusLbl.Location = new System.Drawing.Point(7, 79);
            this.portStatusLbl.Name = "portStatusLbl";
            this.portStatusLbl.Size = new System.Drawing.Size(62, 13);
            this.portStatusLbl.TabIndex = 20;
            this.portStatusLbl.Text = "Port Status:";
            // 
            // closePortBtn
            // 
            this.closePortBtn.Location = new System.Drawing.Point(143, 46);
            this.closePortBtn.Name = "closePortBtn";
            this.closePortBtn.Size = new System.Drawing.Size(130, 23);
            this.closePortBtn.TabIndex = 19;
            this.closePortBtn.Text = "Close";
            this.closePortBtn.UseVisualStyleBackColor = true;
            this.closePortBtn.Click += new System.EventHandler(this.closePortBtn_Click);
            // 
            // openPortBtn
            // 
            this.openPortBtn.Location = new System.Drawing.Point(9, 46);
            this.openPortBtn.Name = "openPortBtn";
            this.openPortBtn.Size = new System.Drawing.Size(130, 23);
            this.openPortBtn.TabIndex = 18;
            this.openPortBtn.Text = "Open";
            this.openPortBtn.UseVisualStyleBackColor = true;
            this.openPortBtn.Click += new System.EventHandler(this.openPortBtn_Click);
            // 
            // updatePortsBtn
            // 
            this.updatePortsBtn.Location = new System.Drawing.Point(203, 17);
            this.updatePortsBtn.Name = "updatePortsBtn";
            this.updatePortsBtn.Size = new System.Drawing.Size(70, 23);
            this.updatePortsBtn.TabIndex = 17;
            this.updatePortsBtn.Text = "Update";
            this.updatePortsBtn.UseVisualStyleBackColor = true;
            this.updatePortsBtn.Click += new System.EventHandler(this.updatePortsBtn_Click);
            // 
            // comPortCombo
            // 
            this.comPortCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortCombo.FormattingEnabled = true;
            this.comPortCombo.Location = new System.Drawing.Point(74, 19);
            this.comPortCombo.Name = "comPortCombo";
            this.comPortCombo.Size = new System.Drawing.Size(121, 21);
            this.comPortCombo.TabIndex = 16;
            this.comPortCombo.Click += new System.EventHandler(this.comPortCombo_MouseClick);
            // 
            // selPortLbl
            // 
            this.selPortLbl.AutoSize = true;
            this.selPortLbl.Location = new System.Drawing.Point(6, 22);
            this.selPortLbl.Name = "selPortLbl";
            this.selPortLbl.Size = new System.Drawing.Size(62, 13);
            this.selPortLbl.TabIndex = 15;
            this.selPortLbl.Text = "Select Port:";
            // 
            // slikServer1
            // 
            this.slikServer1.AccessibleName = "";
            this.slikServer1.Enabled = true;
            this.slikServer1.Location = new System.Drawing.Point(247, 14);
            this.slikServer1.Name = "slikServer1";
            this.slikServer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("slikServer1.OcxState")));
            this.slikServer1.Size = new System.Drawing.Size(32, 32);
            this.slikServer1.TabIndex = 18;
            this.slikServer1.OnClientConnect += new NDI.SLIKDA.Interop.SLIKServer.OnClientConnectEventHandler(this.SlikServer1_OnClientConnect);
            this.slikServer1.OnClientDisconnect += new NDI.SLIKDA.Interop.SLIKServer.OnClientDisconnectEventHandler(this.SlikServer1_OnClientDisconnect);
            this.slikServer1.OnWrite += new NDI.SLIKDA.Interop.SLIKServer.OnWriteEventHandler(this.SlikServer1_OnWrite);
            this.slikServer1.OnWriteVQT += new NDI.SLIKDA.Interop.SLIKServer.OnWriteVQTEventHandler(this.slikServer1_OnWriteVQT);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 462);
            this.Controls.Add(this.slikServer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.Panel1);
            this.Icon = global::OPCServer_Simulator.Properties.Resources.icon;
            this.Name = "Form1";
            this.Text = "Gyptech Micrometer OPC Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slikServer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button ExitButton;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label ConnectedClients;
        private System.Windows.Forms.Timer pressTimer;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.ListView measView;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Measurement;
        private System.Windows.Forms.Label numItemsLbl;
        internal System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox numMeasBox;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comPortCombo;
        private System.Windows.Forms.Label selPortLbl;
        private System.Windows.Forms.Button chngNumItemsBtn;
        private System.Windows.Forms.Button updatePortsBtn;
        private System.Windows.Forms.Label portActiveStatusLbl;
        private System.Windows.Forms.Label portStatusLbl;
        private System.Windows.Forms.Button closePortBtn;
        private System.Windows.Forms.Button openPortBtn;
        private System.Windows.Forms.Button clrMemBtn;
        public NDI.SLIKDA.Interop.SLIKServer slikServer1;
    }
}

