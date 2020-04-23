using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WorkListCS
{
    public partial class frmWorkListCS : Form
    {
        public frmWorkListCS()
        {
            InitializeComponent();
            InitForm();
        }

        #region Private Members

        Model.ServerList serverList = new Model.ServerList();

        Model.ServerConfig currentServer = new Model.ServerConfig();

        #endregion

        #region Public Members

        #endregion

        #region Private Methods

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            this.ServiceID.Visible = false;
            this.GetSetting();
        }

        /// <summary>
        /// 刷新窗体
        /// </summary>
        private void RefreshForm()
        {
            this.GetSetting();
            Thread childThread = new Thread(DelayRefreshForm);
            childThread.Start();
        }

        /// <summary>
        /// 延时刷新窗体
        /// </summary>
        private void DelayRefreshForm()
        {
            Thread.Sleep(2500);
            this.GetSetting();
        }

        /// <summary>
        /// 获取服务列表
        /// </summary>
        private void GetSetting()
        {
            serverList = ConfigService.GetConfigList();
            if (serverList != null)
            {
                if (serverList.ServerConfigs != null)
                {
                    int drc = serverList.ServerConfigs.Count();
                    this.dgvServices.RowCount = drc;
                    this.dgvServices.ColumnCount = 3;
                    if (drc > 0)
                    {
                        this.dgvServices.ColumnHeadersHeight = 35;
                        for (int i = 0; i < drc; i++)
                        {
                            this.dgvServices.Rows[i].Height = 30;
                            this.dgvServices.Rows[i].Cells[0].Value = serverList.ServerConfigs[i].ServerID;
                            this.dgvServices.Rows[i].Cells[1].Value = serverList.ServerConfigs[i].ServerName;
                            string state = string.Empty;
                            ConfigService.GetServiceState(serverList.ServerConfigs[i].ServerName, ref state);
                            this.dgvServices.Rows[i].Cells[2].Value = state;
                            this.dgvServices.Rows[i].Tag = serverList.ServerConfigs[i];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前选中服务
        /// </summary>
        private Model.ServerConfig GetNowService()
        {
            if (serverList.ServerConfigs.Count() > 0)
            {
                if (this.dgvServices.CurrentRow != null)
                {
                    //foreach (Model.ServerConfig sc in serverList.ServerConfigs)
                    //{
                    //    if (sc.ServerName == (this.dgvServices.CurrentRow.Cells[0].Value as string))
                    //    {
                    //        currentServer = sc;
                    //        break;
                    //    }
                    //}
                    currentServer = this.dgvServices.CurrentRow.Tag as Model.ServerConfig;
                }
                else
                {
                    currentServer = serverList.ServerConfigs[0];
                }
            }
            else
            {
                currentServer = new Model.ServerConfig();
            }
            return currentServer;
        }

        /// <summary>
        /// 编辑当前服务
        /// </summary>
        private void EditCurrentService()
        {
            this.GetNowService();
            if (currentServer == null || string.IsNullOrEmpty(currentServer.ServerID))
            {
                MessageBox.Show("请先选择服务！");
                return;
            }
            //是否存在服务
            bool isExisted = ConfigService.IsServiceExisted(currentServer.ServerName);
            //存在服务先停止
            if (isExisted)
            {
                ConfigService.StopService(currentServer.ServerName);
            }
            frmEditWorkList frmEditWork = new frmEditWorkList(currentServer);
            frmEditWork.ShowDialog();
            if (frmEditWork.DialogResult == DialogResult.OK)
            {
                Model.ServerConfig newServerConfig = frmEditWork.EditServerConfig;
                //服务名或路径有变化则卸载重装
                if (currentServer.ServerName != newServerConfig.ServerName
                    || currentServer.FileFloderName != newServerConfig.FileFloderName
                    || !ConfigService.IsServiceExisted(newServerConfig.ServerName))
                {
                    //非新建且旧服务名存在则先卸载
                    if (!string.IsNullOrEmpty(currentServer.ServerName) && isExisted)
                    {
                        ConfigService.UninstallService(currentServer.FileFloderName);
                    }
                    currentServer = newServerConfig;
                    ConfigService.InstallService(currentServer.FileFloderName);
                    isExisted = true;
                }
                else
                {
                    currentServer = newServerConfig;
                }
            }
            if (isExisted)
            {
                Thread.Sleep(500);
                ConfigService.StartService(currentServer.ServerName);
            }
            this.RefreshForm();
        }

        /// <summary>
        /// 新增服务
        /// </summary>
        private void AddNewService()
        {
            Model.ServerConfig newServerConfig = new Model.ServerConfig();
            frmEditWorkList frmEditWork = new frmEditWorkList(newServerConfig);
            frmEditWork.ShowDialog();
            if (frmEditWork.DialogResult == DialogResult.OK)
            {
                newServerConfig = frmEditWork.EditServerConfig;
                //是否存在服务
                bool isExisted = ConfigService.IsServiceExisted(newServerConfig.ServerName);
                //非新建且旧服务名存在则先卸载
                if (isExisted)
                {
                    ConfigService.UninstallService(currentServer.FileFloderName);
                }
                currentServer = newServerConfig;
                ConfigService.InstallService(newServerConfig.FileFloderName);
                Thread.Sleep(500);
                ConfigService.StartService(newServerConfig.ServerName);
            }
            this.RefreshForm();
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        private void OpenLogFolder()
        {
            this.GetNowService();
            if (currentServer == null || string.IsNullOrEmpty(currentServer.ServerID))
            {
                return;
            }
            else
            {
                string serviceFilePath = $"{System.Environment.CurrentDirectory}\\{currentServer.FileFloderName}\\Log";
                if (System.IO.Directory.Exists(serviceFilePath))
                {
                    System.Diagnostics.Process.Start(serviceFilePath);
                }
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Events
        /// <summary>
        /// 托盘图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void niWorkListCS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 窗体改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWorkListCS_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.niWorkListCS.Visible = true;
                this.Visible = false;
            }
            else
            {
                this.niWorkListCS.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddNewService();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditCurrentService();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {

            #region 旧方法
            //frmEditWorkList frmEditWork = new frmEditWorkList(new Model.ServerConfig());
            //frmEditWork.ShowDialog();
            //InitForm();
            //this.InstallService(currentServer.FileFloderName);
            //this.StartService(currentServer.ServerName); 
            #endregion

            this.GetNowService();
            string serviceName = currentServer.ServerName;

            if (ConfigService.IsServiceExisted(serviceName))
            {
                ConfigService.UninstallService(currentServer.FileFloderName);
            }
            ConfigService.InstallService(currentServer.FileFloderName);
            Thread.Sleep(500);
            ConfigService.StartService(serviceName);
            this.RefreshForm();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            this.GetNowService();
            string serviceName = currentServer.ServerName;
            if (ConfigService.IsServiceExisted(serviceName))
            {
                ConfigService.UninstallService(currentServer.FileFloderName);
            }
            this.RefreshForm();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ConfigService.StopService(currentServer.ServerName);
            ConfigService.StartService(currentServer.ServerName);
            this.RefreshForm();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ConfigService.StopService(currentServer.ServerName);
            this.RefreshForm();
        }

        private void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            this.GetNowService();
        }
        
        private void dgvServices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.EditCurrentService();
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            this.OpenLogFolder();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion

        #region Override

        #endregion
    }
}
