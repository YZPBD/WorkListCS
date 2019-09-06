using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;

namespace WorkListCOE
{
    public partial class frmWorkListCOE : Form
    {
        public frmWorkListCOE()
        {
            InitializeComponent();
            InitForm();
        }

        #region Private Members

        #endregion

        #region Public Members

        #endregion

        #region Private Methods

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            this.SetFormServiceState();
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        private void InstallService()
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;

            this.SetFormServiceState();
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        private void UninstallService()
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;

            System.Environment.CurrentDirectory = CurrentDirectory + "\\Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            System.Environment.CurrentDirectory = CurrentDirectory;

            this.SetFormServiceState();
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        private void StartService()
        {
            ServiceController serviceController = new ServiceController("COEWorkListService");
            serviceController.Start();

            this.SetFormServiceState();
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        private void StopService()
        {
            ServiceController serviceController = new ServiceController("COEWorkListService");
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }

            this.SetFormServiceState();
        }

        /// <summary>
        /// 暂停或继续服务
        /// </summary>
        private void PauseAndContinueService()
        {
            ServiceController serviceController = new ServiceController("COEWorkListService");
            if (serviceController.CanPauseAndContinue)
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Pause();
                }
                else if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    serviceController.Continue();
                }
            }

            this.SetFormServiceState();
        }

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <returns></returns>
        private string GetServiceState()
        {
            //服务名称
            string serviceName = "COEWorkListService";
            //是否存在
            bool isExistService = false;
            //状态名称
            string status = "未安装";
            //获取windows服务列表
            ServiceController[] serviceList = ServiceController.GetServices();

            //循环查找该名称的服务
            for (int i = 0; i < serviceList.Length; i++)
            {
                if (serviceList[i].DisplayName.ToString() == serviceName)
                {
                    isExistService = true;
                    break;
                }
            }

            if (isExistService)
            {
                ServiceController serviceController = new ServiceController(serviceName);
                try
                {
                    switch (serviceController.Status)
                    {
                        case ServiceControllerStatus.Stopped:
                            {
                                status = "已停止";
                                break;
                            }
                        case ServiceControllerStatus.StartPending:
                            {
                                status = "开始挂起";
                                break;
                            }
                        case ServiceControllerStatus.StopPending:
                            {
                                status = "结束挂起";
                                break;
                            }
                        case ServiceControllerStatus.Running:
                            {
                                status = "正在运行";
                                break;
                            }
                        case ServiceControllerStatus.ContinuePending:
                            {
                                status = "继续挂起";
                                break;
                            }
                        case ServiceControllerStatus.PausePending:
                            {
                                status = "暂停挂起";
                                break;
                            }
                        case ServiceControllerStatus.Paused:
                            {
                                status = "暂停";
                                break;
                            }
                    }
                }
                catch
                { }
            }

            return status;
        }

        /// <summary>
        /// 设置界面服务名称
        /// </summary>
        private void SetFormServiceState()
        {
            this.lbState.Text = this.GetServiceState();
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
        private void niWorkListCOE_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 窗体改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWorkListCOE_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.niWorkListCOE.Visible = true;
                this.Visible = false;
            }
            else
            {
                this.niWorkListCOE.Visible = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.StartService();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.StopService();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            this.InstallService();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            this.UninstallService();
        }

        #endregion

        #region Override

        #endregion
    }
}
