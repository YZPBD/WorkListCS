using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WorkListCS
{
    public partial class frmEditWorkList : Form
    {
        public frmEditWorkList()
        {
            InitializeComponent();
        }

        public frmEditWorkList(Model.ServerConfig serverConfig)
        {
            InitializeComponent();
            EditServerConfig = serverConfig;
            InitForm();
        }

        #region Private Members


        #endregion

        #region Public Members

        public Model.ServerConfig EditServerConfig = new Model.ServerConfig();

        public Model.ServiceSetting EditServiceSetting = new Model.ServiceSetting();

        public Model.ServiceConfig EditServiceConfig = new Model.ServiceConfig();

        public bool IsSaved = false;

        public string Err = string.Empty;

        #endregion

        #region Private Methods

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            //获取设置
            this.lbErrMessage.Visible = false;
            EditServiceSetting = ConfigService.GetServiceSetting(EditServerConfig);
            this.txtServerName.Text = EditServiceSetting.ServiceName;
            this.txtDisplayName.Text = EditServiceSetting.DisplayName;
            this.txtDescription.Text = EditServiceSetting.Description;

            //设置参数
            IsSaved = false;
            EditServiceConfig = ConfigService.GetServiceConfig(EditServerConfig);
            if (EditServiceConfig == null)
            {
                return;
            }
            this.txtServerIP.Text = EditServiceConfig.serverIP;
            this.txtServerPort.Text = EditServiceConfig.serverPort;
            this.txtServerAET.Text = EditServiceConfig.serverAET;
            this.txtClientAET.Text = EditServiceConfig.clientAET;
            this.txtClientPort.Text = EditServiceConfig.clientPort;
            this.txtCacheDays.Text = EditServiceConfig.cacheDays;
            this.txtDeviceType.Text = EditServiceConfig.deviceType;
            this.txtLoopSeconds.Text = EditServiceConfig.loopSeconds;
            this.chkIsRealTime.Checked = EditServiceConfig.isRealTime;
            this.chkIsOnlyArrived.Checked = EditServiceConfig.isOnlyArrived;
            this.txtSetAETitle.Text = EditServiceConfig.setAETitle;
            this.chkIsConvertPatientId.Checked = !EditServiceConfig.isNotConvertPatientId;
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private bool SaveConfig()
        {
            if (string.IsNullOrEmpty(EditServerConfig.ServerID))
            {
                EditServerConfig.ServerID = Guid.NewGuid().ToString();
            }            
            EditServerConfig.ServerName = this.txtServerName.Text;
            if (string.IsNullOrEmpty(EditServerConfig.FileFloderName))
            {
                EditServerConfig.FileFloderName = EditServerConfig.ServerID;
            }

            EditServiceSetting = new Model.ServiceSetting();
            EditServiceSetting.ServiceName = this.txtServerName.Text;
            EditServiceSetting.DisplayName = this.txtDisplayName.Text;
            EditServiceSetting.Description = this.txtDescription.Text;

            EditServiceConfig = new Model.ServiceConfig();
            EditServiceConfig.serverIP = this.txtServerIP.Text;
            EditServiceConfig.serverPort = this.txtServerPort.Text;
            EditServiceConfig.serverAET = this.txtServerAET.Text;
            EditServiceConfig.clientAET = this.txtClientAET.Text;
            EditServiceConfig.clientPort = this.txtClientPort.Text;
            EditServiceConfig.cacheDays = this.txtCacheDays.Text;
            EditServiceConfig.deviceType = this.txtDeviceType.Text;
            EditServiceConfig.loopSeconds = this.txtLoopSeconds.Text;
            EditServiceConfig.isRealTime = this.chkIsRealTime.Checked;
            EditServiceConfig.isOnlyArrived = this.chkIsOnlyArrived.Checked;
            EditServiceConfig.setAETitle = this.txtSetAETitle.Text;
            EditServiceConfig.isNotConvertPatientId = !this.chkIsConvertPatientId.Checked;

            bool isSaved = ConfigService.SaveNewService(EditServerConfig, EditServiceConfig, EditServiceSetting);
            if (!isSaved)
            {
                Err = ConfigService.Err;
            }

            return isSaved;
        }

        private bool CheckValidity()
        {
            bool validity = true;
            this.lbErrMessage.Visible = false;

            //检查服务名
            if (string.IsNullOrEmpty(this.txtServerName.Text))
            {
                Err = "Service name can not NULL, please fill in again!";
                this.lbErrMessage.Text = Err;
                this.lbErrMessage.Visible = true;
                this.txtServerName.Focus();
                return false;
            }
            //是否有修改
            if (EditServiceSetting.ServiceName != this.txtServerName.Text)
            {
                //先判断当前配置是否冲突
                validity = !ConfigService.GetConfigList().ServerConfigs.Exists(o => o.ServerName.ToLower() == this.txtServerName.Text.ToLower());
                //检查是否存在相同服务
                if (validity)
                {
                    validity = !ConfigService.IsServiceExisted(this.txtServerName.Text);
                }
            }
            if (!validity)
            {
                Err = "Service name is not available, please fill in again!";
                this.lbErrMessage.Text = Err;
                this.lbErrMessage.Visible = true;
                this.txtServerName.Focus();
                return false;
            }

            //判断服务IP
            if (!ConfigService.IsIPv4(this.txtServerIP.Text))
            {
                Err = "Server IP is not available, please fill in again!";
                this.lbErrMessage.Text = Err;
                this.lbErrMessage.Visible = true;
                this.txtServerIP.Focus();
                return false;
            }

            //判断客户端端口
            int port = 0;
            validity = int.TryParse(this.txtClientPort.Text, out port);
            if (validity)
            {
                validity = !ConfigService.portInUse(port, "TCP");
            }
            if (!validity)
            {
                Err = "Server Port is not available, please fill in again!";
                this.lbErrMessage.Text = Err;
                this.lbErrMessage.Visible = true;
                this.txtClientPort.Focus();
                return false;
            }

            return validity;
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Events
        /// <summary>
        /// 保存按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //检查有效性
            this.IsSaved = this.CheckValidity();
            if (!this.IsSaved)
            {
                return;
            }
            //保存数据
            this.IsSaved = this.SaveConfig();
            if (this.IsSaved)
            {
                //安装并运行服务
                try
                {
                    ConfigService.InstallService(EditServerConfig.FileFloderName);
                    //ConfigService.StartService(EditServerConfig.ServerName);
                }
                catch (Exception ex)
                {
                    this.IsSaved = false;
                    this.Err = ex.Message;
                }
            }

            if (this.IsSaved)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Save failed！\n\r" + this.Err);
            }
        }

        /// <summary>
        /// 退出按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        #endregion

        #region Override

        #endregion
    }
}
