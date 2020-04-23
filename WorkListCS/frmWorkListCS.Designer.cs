namespace WorkListCS
{
    partial class frmWorkListCS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkListCS));
            this.panelWorkList = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.ServiceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.niWorkListCS = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelWorkList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.SuspendLayout();
            // 
            // panelWorkList
            // 
            this.panelWorkList.BackColor = System.Drawing.Color.White;
            this.panelWorkList.Controls.Add(this.btnRefresh);
            this.panelWorkList.Controls.Add(this.btnOpenLog);
            this.panelWorkList.Controls.Add(this.btnEdit);
            this.panelWorkList.Controls.Add(this.btnAdd);
            this.panelWorkList.Controls.Add(this.dgvServices);
            this.panelWorkList.Controls.Add(this.btnUninstall);
            this.panelWorkList.Controls.Add(this.btnStart);
            this.panelWorkList.Controls.Add(this.btnInstall);
            this.panelWorkList.Controls.Add(this.btnStop);
            resources.ApplyResources(this.panelWorkList, "panelWorkList");
            this.panelWorkList.Name = "panelWorkList";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnEdit.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnAdd.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvServices
            // 
            this.dgvServices.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServiceID,
            this.ServiceName,
            this.State});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServices.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dgvServices, "dgvServices");
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.RowTemplate.Height = 30;
            this.dgvServices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServices_CellDoubleClick);
            this.dgvServices.SelectionChanged += new System.EventHandler(this.dgvServices_SelectionChanged);
            // 
            // ServiceID
            // 
            resources.ApplyResources(this.ServiceID, "ServiceID");
            this.ServiceID.Name = "ServiceID";
            this.ServiceID.ReadOnly = true;
            // 
            // ServiceName
            // 
            resources.ApplyResources(this.ServiceName, "ServiceName");
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.ReadOnly = true;
            // 
            // State
            // 
            resources.ApplyResources(this.State, "State");
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // btnUninstall
            // 
            this.btnUninstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnUninstall.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnUninstall.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnUninstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnUninstall.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnUninstall, "btnUninstall");
            this.btnUninstall.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.UseVisualStyleBackColor = false;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInstall.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnInstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnInstall.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnInstall, "btnInstall");
            this.btnInstall.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // niWorkListCS
            // 
            resources.ApplyResources(this.niWorkListCS, "niWorkListCS");
            this.niWorkListCS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niWorkListCS_MouseDoubleClick);
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnOpenLog.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnOpenLog.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnOpenLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnOpenLog.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnOpenLog, "btnOpenLog");
            this.btnOpenLog.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.UseVisualStyleBackColor = false;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnRefresh.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(128)))), ((int)(((byte)(196)))));
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(119)))), ((int)(((byte)(175)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmWorkListCS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelWorkList);
            this.Name = "frmWorkListCS";
            this.Resize += new System.EventHandler(this.frmWorkListCS_Resize);
            this.panelWorkList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWorkList;
        private System.Windows.Forms.NotifyIcon niWorkListCS;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Button btnRefresh;
    }
}

