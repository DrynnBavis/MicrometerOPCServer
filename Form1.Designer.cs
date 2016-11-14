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
            this.SlikServer1 = new NDI.SLIKDA.Interop.SLIKServer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pressTimer = new System.Windows.Forms.Timer(this.components);
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.measView = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Measurement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastRecorded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.inchmm = new System.Windows.Forms.Button();
            this.logDataBtn = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SlikServer1)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.ConnectedClients);
            this.Panel1.Controls.Add(this.ExitButton);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.SlikServer1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 332);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(307, 33);
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
            this.ExitButton.Location = new System.Drawing.Point(224, 6);
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
            // SlikServer1
            // 
            this.SlikServer1.Enabled = true;
            this.SlikServer1.Location = new System.Drawing.Point(140, -159);
            this.SlikServer1.Name = "SlikServer1";
            this.SlikServer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SlikServer1.OcxState")));
            this.SlikServer1.Size = new System.Drawing.Size(32, 32);
            this.SlikServer1.TabIndex = 11;
            this.SlikServer1.OnClientConnect += new NDI.SLIKDA.Interop.SLIKServer.OnClientConnectEventHandler(this.SlikServer1_OnClientConnect);
            this.SlikServer1.OnClientDisconnect += new NDI.SLIKDA.Interop.SLIKServer.OnClientDisconnectEventHandler(this.SlikServer1_OnClientDisconnect);
            this.SlikServer1.OnRead += new NDI.SLIKDA.Interop.SLIKServer.OnReadEventHandler(this.SlikServer1_OnRead);
            this.SlikServer1.OnWrite += new NDI.SLIKDA.Interop.SLIKServer.OnWriteEventHandler(this.SlikServer1_OnWrite);
            // 
            // pressTimer
            // 
            this.pressTimer.Interval = 50;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.measView);
            this.GroupBox3.Controls.Add(this.inchmm);
            this.GroupBox3.Location = new System.Drawing.Point(12, 12);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(284, 257);
            this.GroupBox3.TabIndex = 12;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Measurement Data";
            // 
            // measView
            // 
            this.measView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Measurement,
            this.LastRecorded});
            this.measView.Location = new System.Drawing.Point(7, 39);
            this.measView.Name = "measView";
            this.measView.Size = new System.Drawing.Size(271, 212);
            this.measView.TabIndex = 0;
            this.measView.UseCompatibleStateImageBehavior = false;
            this.measView.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 41;
            // 
            // Measurement
            // 
            this.Measurement.Text = "Measurement";
            this.Measurement.Width = 77;
            // 
            // LastRecorded
            // 
            this.LastRecorded.Text = "Last Recorded Value";
            this.LastRecorded.Width = 149;
            // 
            // inchmm
            // 
            this.inchmm.Location = new System.Drawing.Point(212, 10);
            this.inchmm.Name = "inchmm";
            this.inchmm.Size = new System.Drawing.Size(65, 23);
            this.inchmm.TabIndex = 1;
            this.inchmm.Text = "inch/mm";
            this.inchmm.UseVisualStyleBackColor = true;
            this.inchmm.Click += new System.EventHandler(this.inchmm_Click);
            // 
            // logDataBtn
            // 
            this.logDataBtn.Location = new System.Drawing.Point(18, 276);
            this.logDataBtn.Name = "logDataBtn";
            this.logDataBtn.Size = new System.Drawing.Size(271, 45);
            this.logDataBtn.TabIndex = 13;
            this.logDataBtn.Text = "Simulate Micrometer Click";
            this.logDataBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 365);
            this.Controls.Add(this.logDataBtn);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.Panel1);
            this.Icon = global::OPCServer_Simulator.Properties.Resources.icon;
            this.Name = "Form1";
            this.Text = "Gyptech Micrometer OPC Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SlikServer1)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button ExitButton;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Timer timer1;
        private NDI.SLIKDA.Interop.SLIKServer SlikServer1;
        private System.Windows.Forms.Label ConnectedClients;
        private System.Windows.Forms.Timer pressTimer;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.Button inchmm;
        private System.Windows.Forms.ListView measView;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Measurement;
        private System.Windows.Forms.ColumnHeader LastRecorded;
        private System.Windows.Forms.Button logDataBtn;
    }
}

