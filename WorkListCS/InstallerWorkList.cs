using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace WorkListCS
{
    [RunInstaller(true)]
    public partial class InstallerWorkList : System.Configuration.Install.Installer
    {
        public InstallerWorkList()
        {
            InitializeComponent();
            /* 服务未注册前，System.Configuration.ConfigurationManager.AppSettings读取无效。
            //serviceInstaller1.ServiceName = "ChinaHN.XHService." + System.Configuration.ConfigurationManager.AppSettings["Service_ID"];
            //serviceInstaller1.DisplayName = System.Configuration.ConfigurationManager.AppSettings["Service_DisplayName"];
            //serviceInstaller1.Description = System.Configuration.ConfigurationManager.AppSettings["Service_Description"]; 
            */

            /* 指定该服务的启动模式：自动，手动，禁用
            serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.;
            */
            using (SettingHelper setting = new SettingHelper())
            {
                ////系统用于标志此服务名称(唯一性)
                //serviceInstaller1.ServiceName = setting.ServiceName;
                ////向用户标志服务的显示名称(可以重复)
                //serviceInstaller1.DisplayName = setting.DisplayName;
                ////服务的说明(描述)
                //serviceInstaller1.Description = setting.Description;
            }
        }
    }
}
