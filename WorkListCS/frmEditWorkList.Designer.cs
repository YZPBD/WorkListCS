namespace WorkListCS
{
    partial class frmEditWorkList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditWorkList));
            this.splProperties = new System.Windows.Forms.SplitContainer();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lbDisplayName = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lbServerName = new System.Windows.Forms.Label();
            this.groupProperties = new System.Windows.Forms.GroupBox();
            this.chkIsConvertPatientId = new System.Windows.Forms.CheckBox();
            this.chkIsOnlyArrived = new System.Windows.Forms.CheckBox();
            this.chkIsRealTime = new System.Windows.Forms.CheckBox();
            this.txtSetAETitle = new System.Windows.Forms.TextBox();
            this.lbSetAETitle = new System.Windows.Forms.Label();
            this.lbServerIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.lbServerPort = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.lbServerAET = new System.Windows.Forms.Label();
            this.txtLoopSeconds = new System.Windows.Forms.TextBox();
            this.txtServerAET = new System.Windows.Forms.TextBox();
            this.lbLoopSeconds = new System.Windows.Forms.Label();
            this.lbClientAET = new System.Windows.Forms.Label();
            this.txtDeviceType = new System.Windows.Forms.TextBox();
            this.txtClientAET = new System.Windows.Forms.TextBox();
            this.lbDeviceType = new System.Windows.Forms.Label();
            this.lbClientPort = new System.Windows.Forms.Label();
            this.txtCacheDays = new System.Windows.Forms.TextBox();
            this.txtClientPort = new System.Windows.Forms.TextBox();
            this.lbCacheDays = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.lbErrMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splProperties)).BeginInit();
            this.splProperties.Panel1.SuspendLayout();
            this.splProperties.Panel2.SuspendLayout();
            this.splProperties.SuspendLayout();
            this.groupProperties.SuspendLayout();
            this.panelEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // splProperties
            // 
            this.splProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splProperties.Location = new System.Drawing.Point(0, 0);
            this.splProperties.Name = "splProperties";
            // 
            // splProperties.Panel1
            // 
            this.splProperties.Panel1.BackColor = System.Drawing.Color.White;
            this.splProperties.Panel1.Controls.Add(this.txtDescription);
            this.splProperties.Panel1.Controls.Add(this.lbDescription);
            this.splProperties.Panel1.Controls.Add(this.txtDisplayName);
            this.splProperties.Panel1.Controls.Add(this.lbDisplayName);
            this.splProperties.Panel1.Controls.Add(this.txtServerName);
            this.splProperties.Panel1.Controls.Add(this.lbServerName);
            // 
            // splProperties.Panel2
            // 
            this.splProperties.Panel2.Controls.Add(this.groupProperties);
            this.splProperties.Size = new System.Drawing.Size(684, 389);
            this.splProperties.SplitterDistance = 334;
            this.splProperties.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(19, 144);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(298, 229);
            this.txtDescription.TabIndex = 5;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDescription.Location = new System.Drawing.Point(16, 124);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(79, 17);
            this.lbDescription.TabIndex = 4;
            this.lbDescription.Text = "Description";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDisplayName.Location = new System.Drawing.Point(19, 88);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(298, 23);
            this.txtDisplayName.TabIndex = 3;
            // 
            // lbDisplayName
            // 
            this.lbDisplayName.AutoSize = true;
            this.lbDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDisplayName.Location = new System.Drawing.Point(16, 68);
            this.lbDisplayName.Name = "lbDisplayName";
            this.lbDisplayName.Size = new System.Drawing.Size(91, 17);
            this.lbDisplayName.TabIndex = 2;
            this.lbDisplayName.Text = "DisplayName";
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerName.Location = new System.Drawing.Point(19, 32);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(298, 23);
            this.txtServerName.TabIndex = 1;
            // 
            // lbServerName
            // 
            this.lbServerName.AutoSize = true;
            this.lbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServerName.Location = new System.Drawing.Point(16, 12);
            this.lbServerName.Name = "lbServerName";
            this.lbServerName.Size = new System.Drawing.Size(87, 17);
            this.lbServerName.TabIndex = 0;
            this.lbServerName.Text = "ServerName";
            // 
            // groupProperties
            // 
            this.groupProperties.BackColor = System.Drawing.Color.White;
            this.groupProperties.Controls.Add(this.chkIsConvertPatientId);
            this.groupProperties.Controls.Add(this.chkIsOnlyArrived);
            this.groupProperties.Controls.Add(this.chkIsRealTime);
            this.groupProperties.Controls.Add(this.txtSetAETitle);
            this.groupProperties.Controls.Add(this.lbSetAETitle);
            this.groupProperties.Controls.Add(this.lbServerIP);
            this.groupProperties.Controls.Add(this.txtServerIP);
            this.groupProperties.Controls.Add(this.lbServerPort);
            this.groupProperties.Controls.Add(this.txtServerPort);
            this.groupProperties.Controls.Add(this.lbServerAET);
            this.groupProperties.Controls.Add(this.txtLoopSeconds);
            this.groupProperties.Controls.Add(this.txtServerAET);
            this.groupProperties.Controls.Add(this.lbLoopSeconds);
            this.groupProperties.Controls.Add(this.lbClientAET);
            this.groupProperties.Controls.Add(this.txtDeviceType);
            this.groupProperties.Controls.Add(this.txtClientAET);
            this.groupProperties.Controls.Add(this.lbDeviceType);
            this.groupProperties.Controls.Add(this.lbClientPort);
            this.groupProperties.Controls.Add(this.txtCacheDays);
            this.groupProperties.Controls.Add(this.txtClientPort);
            this.groupProperties.Controls.Add(this.lbCacheDays);
            this.groupProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupProperties.Location = new System.Drawing.Point(0, 0);
            this.groupProperties.Name = "groupProperties";
            this.groupProperties.Size = new System.Drawing.Size(346, 389);
            this.groupProperties.TabIndex = 24;
            this.groupProperties.TabStop = false;
            this.groupProperties.Text = "Properties";
            // 
            // chkIsConvertPatientId
            // 
            this.chkIsConvertPatientId.AutoSize = true;
            this.chkIsConvertPatientId.Location = new System.Drawing.Point(124, 316);
            this.chkIsConvertPatientId.Name = "chkIsConvertPatientId";
            this.chkIsConvertPatientId.Size = new System.Drawing.Size(141, 21);
            this.chkIsConvertPatientId.TabIndex = 30;
            this.chkIsConvertPatientId.Text = "IsConvertPatientId";
            this.chkIsConvertPatientId.UseVisualStyleBackColor = true;
            // 
            // chkIsOnlyArrived
            // 
            this.chkIsOnlyArrived.AutoSize = true;
            this.chkIsOnlyArrived.Location = new System.Drawing.Point(17, 352);
            this.chkIsOnlyArrived.Name = "chkIsOnlyArrived";
            this.chkIsOnlyArrived.Size = new System.Drawing.Size(111, 21);
            this.chkIsOnlyArrived.TabIndex = 29;
            this.chkIsOnlyArrived.Text = "IsOnlyArrived";
            this.chkIsOnlyArrived.UseVisualStyleBackColor = true;
            // 
            // chkIsRealTime
            // 
            this.chkIsRealTime.AutoSize = true;
            this.chkIsRealTime.Location = new System.Drawing.Point(17, 316);
            this.chkIsRealTime.Name = "chkIsRealTime";
            this.chkIsRealTime.Size = new System.Drawing.Size(97, 21);
            this.chkIsRealTime.TabIndex = 28;
            this.chkIsRealTime.Text = "IsRealTime";
            this.chkIsRealTime.UseVisualStyleBackColor = true;
            // 
            // txtSetAETitle
            // 
            this.txtSetAETitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSetAETitle.Location = new System.Drawing.Point(124, 281);
            this.txtSetAETitle.Name = "txtSetAETitle";
            this.txtSetAETitle.Size = new System.Drawing.Size(207, 23);
            this.txtSetAETitle.TabIndex = 25;
            // 
            // lbSetAETitle
            // 
            this.lbSetAETitle.AutoSize = true;
            this.lbSetAETitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSetAETitle.Location = new System.Drawing.Point(35, 284);
            this.lbSetAETitle.Name = "lbSetAETitle";
            this.lbSetAETitle.Size = new System.Drawing.Size(74, 17);
            this.lbSetAETitle.TabIndex = 24;
            this.lbSetAETitle.Text = "SetAETitle";
            // 
            // lbServerIP
            // 
            this.lbServerIP.AutoSize = true;
            this.lbServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServerIP.Location = new System.Drawing.Point(47, 31);
            this.lbServerIP.Name = "lbServerIP";
            this.lbServerIP.Size = new System.Drawing.Size(62, 17);
            this.lbServerIP.TabIndex = 2;
            this.lbServerIP.Text = "ServerIP";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerIP.Location = new System.Drawing.Point(124, 28);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(207, 23);
            this.txtServerIP.TabIndex = 3;
            // 
            // lbServerPort
            // 
            this.lbServerPort.AutoSize = true;
            this.lbServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServerPort.Location = new System.Drawing.Point(33, 63);
            this.lbServerPort.Name = "lbServerPort";
            this.lbServerPort.Size = new System.Drawing.Size(76, 17);
            this.lbServerPort.TabIndex = 4;
            this.lbServerPort.Text = "ServerPort";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerPort.Location = new System.Drawing.Point(124, 60);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(207, 23);
            this.txtServerPort.TabIndex = 5;
            // 
            // lbServerAET
            // 
            this.lbServerAET.AutoSize = true;
            this.lbServerAET.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServerAET.Location = new System.Drawing.Point(32, 95);
            this.lbServerAET.Name = "lbServerAET";
            this.lbServerAET.Size = new System.Drawing.Size(77, 17);
            this.lbServerAET.TabIndex = 6;
            this.lbServerAET.Text = "ServerAET";
            // 
            // txtLoopSeconds
            // 
            this.txtLoopSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLoopSeconds.Location = new System.Drawing.Point(124, 252);
            this.txtLoopSeconds.Name = "txtLoopSeconds";
            this.txtLoopSeconds.Size = new System.Drawing.Size(207, 23);
            this.txtLoopSeconds.TabIndex = 17;
            // 
            // txtServerAET
            // 
            this.txtServerAET.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServerAET.Location = new System.Drawing.Point(124, 92);
            this.txtServerAET.Name = "txtServerAET";
            this.txtServerAET.Size = new System.Drawing.Size(207, 23);
            this.txtServerAET.TabIndex = 7;
            // 
            // lbLoopSeconds
            // 
            this.lbLoopSeconds.AutoSize = true;
            this.lbLoopSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLoopSeconds.Location = new System.Drawing.Point(14, 255);
            this.lbLoopSeconds.Name = "lbLoopSeconds";
            this.lbLoopSeconds.Size = new System.Drawing.Size(95, 17);
            this.lbLoopSeconds.TabIndex = 16;
            this.lbLoopSeconds.Text = "LoopSeconds";
            // 
            // lbClientAET
            // 
            this.lbClientAET.AutoSize = true;
            this.lbClientAET.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbClientAET.Location = new System.Drawing.Point(39, 127);
            this.lbClientAET.Name = "lbClientAET";
            this.lbClientAET.Size = new System.Drawing.Size(70, 17);
            this.lbClientAET.TabIndex = 8;
            this.lbClientAET.Text = "ClientAET";
            // 
            // txtDeviceType
            // 
            this.txtDeviceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeviceType.Location = new System.Drawing.Point(124, 220);
            this.txtDeviceType.Name = "txtDeviceType";
            this.txtDeviceType.Size = new System.Drawing.Size(207, 23);
            this.txtDeviceType.TabIndex = 15;
            // 
            // txtClientAET
            // 
            this.txtClientAET.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClientAET.Location = new System.Drawing.Point(124, 124);
            this.txtClientAET.Name = "txtClientAET";
            this.txtClientAET.Size = new System.Drawing.Size(207, 23);
            this.txtClientAET.TabIndex = 9;
            // 
            // lbDeviceType
            // 
            this.lbDeviceType.AutoSize = true;
            this.lbDeviceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDeviceType.Location = new System.Drawing.Point(26, 223);
            this.lbDeviceType.Name = "lbDeviceType";
            this.lbDeviceType.Size = new System.Drawing.Size(83, 17);
            this.lbDeviceType.TabIndex = 14;
            this.lbDeviceType.Text = "DeviceType";
            // 
            // lbClientPort
            // 
            this.lbClientPort.AutoSize = true;
            this.lbClientPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbClientPort.Location = new System.Drawing.Point(40, 159);
            this.lbClientPort.Name = "lbClientPort";
            this.lbClientPort.Size = new System.Drawing.Size(69, 17);
            this.lbClientPort.TabIndex = 10;
            this.lbClientPort.Text = "ClientPort";
            // 
            // txtCacheDays
            // 
            this.txtCacheDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCacheDays.Location = new System.Drawing.Point(124, 188);
            this.txtCacheDays.Name = "txtCacheDays";
            this.txtCacheDays.Size = new System.Drawing.Size(207, 23);
            this.txtCacheDays.TabIndex = 13;
            // 
            // txtClientPort
            // 
            this.txtClientPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClientPort.Location = new System.Drawing.Point(124, 156);
            this.txtClientPort.Name = "txtClientPort";
            this.txtClientPort.Size = new System.Drawing.Size(207, 23);
            this.txtClientPort.TabIndex = 11;
            // 
            // lbCacheDays
            // 
            this.lbCacheDays.AutoSize = true;
            this.lbCacheDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCacheDays.Location = new System.Drawing.Point(29, 191);
            this.lbCacheDays.Name = "lbCacheDays";
            this.lbCacheDays.Size = new System.Drawing.Size(80, 17);
            this.lbCacheDays.TabIndex = 12;
            this.lbCacheDays.Text = "CacheDays";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnClose.Location = new System.Drawing.Point(590, 397);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 33);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSave.Location = new System.Drawing.Point(491, 397);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 33);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.splProperties);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEdit.Location = new System.Drawing.Point(0, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(684, 389);
            this.panelEdit.TabIndex = 13;
            // 
            // lbErrMessage
            // 
            this.lbErrMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbErrMessage.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbErrMessage.ForeColor = System.Drawing.Color.Red;
            this.lbErrMessage.Location = new System.Drawing.Point(12, 403);
            this.lbErrMessage.Name = "lbErrMessage";
            this.lbErrMessage.Size = new System.Drawing.Size(473, 20);
            this.lbErrMessage.TabIndex = 14;
            this.lbErrMessage.Text = "label1";
            this.lbErrMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbErrMessage.Visible = false;
            // 
            // frmEditWorkList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(684, 443);
            this.Controls.Add(this.lbErrMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditWorkList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditWorkList";
            this.splProperties.Panel1.ResumeLayout(false);
            this.splProperties.Panel1.PerformLayout();
            this.splProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splProperties)).EndInit();
            this.splProperties.ResumeLayout(false);
            this.groupProperties.ResumeLayout(false);
            this.groupProperties.PerformLayout();
            this.panelEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splProperties;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lbDisplayName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lbServerName;
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.Label lbServerIP;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label lbServerPort;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label lbServerAET;
        private System.Windows.Forms.TextBox txtLoopSeconds;
        private System.Windows.Forms.TextBox txtServerAET;
        private System.Windows.Forms.Label lbLoopSeconds;
        private System.Windows.Forms.Label lbClientAET;
        private System.Windows.Forms.TextBox txtDeviceType;
        private System.Windows.Forms.TextBox txtClientAET;
        private System.Windows.Forms.Label lbDeviceType;
        private System.Windows.Forms.Label lbClientPort;
        private System.Windows.Forms.TextBox txtCacheDays;
        private System.Windows.Forms.TextBox txtClientPort;
        private System.Windows.Forms.Label lbCacheDays;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.TextBox txtSetAETitle;
        private System.Windows.Forms.Label lbSetAETitle;
        private System.Windows.Forms.CheckBox chkIsConvertPatientId;
        private System.Windows.Forms.CheckBox chkIsOnlyArrived;
        private System.Windows.Forms.CheckBox chkIsRealTime;
        private System.Windows.Forms.Label lbErrMessage;
    }
}