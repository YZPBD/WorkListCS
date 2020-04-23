using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;
using System.Configuration.Install;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;

namespace WorkListCS
{
    /// <summary>
    /// 配置服务
    /// </summary>
    class ConfigService
    {

        #region Private Members

        private static Model.ServerList ConfigList = new Model.ServerList();

        private static string confPath = StartupPath + "\\conf.xml";

        private static string serviceSettingPath = "\\ServiceSetting.xml";

        private static string serviceConfigPath = "\\CSWorkListService.exe.config";

        #endregion

        #region Public Members
        
        public static string Err = string.Empty;

        #endregion

        #region Private Methods

        /// <summary>
        /// 程序地址
        /// </summary>
        public static string StartupPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        public static Model.ServerList GetConfigList()
        {
            if (!System.IO.File.Exists(confPath))
            {
                //System.IO.Directory.CreateDirectory(StartupPath + "\\" + pathName);
                XmlSerialize(confPath, ConfigList);
            }
            else
            {
                string path = confPath;
                ConfigList = XmlConvertModel<Model.ServerList>(path);
            }

            return ConfigList;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        public static Model.ServiceSetting GetServiceSetting(Model.ServerConfig serverConfig)
        {
            Model.ServiceSetting serviceConfig = new Model.ServiceSetting();
            string path = StartupPath + "\\" + serverConfig.FileFloderName + serviceSettingPath;
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode("Settings/ServiceName");
                serviceConfig.ServiceName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/DisplayName");
                serviceConfig.DisplayName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/Description");
                serviceConfig.Description = xn.InnerText;
                doc = null;
            }
            return serviceConfig;
        }

        /// <summary>
        /// 获取服务配置
        /// </summary>
        /// <returns></returns>
        public static Model.ServiceConfig GetServiceConfig(Model.ServerConfig serverConfig)
        {
            if (serverConfig.ServerName == null)
            {
                return null;
            }
            Model.ServiceConfig serviceConfig = new Model.ServiceConfig();
            string path = StartupPath + "\\" + serverConfig.FileFloderName + serviceConfigPath.Replace(".config", "");
            var appConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(path);
            System.Reflection.PropertyInfo[] propertyInfo = typeof(Model.ServiceConfig).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (appConfig.AppSettings.Settings.AllKeys.Contains(pinfo.Name))
                {
                    if (pinfo.GetValue(serviceConfig, null) is bool)
                    {
                        bool value = false;
                        if ((appConfig.AppSettings.Settings[pinfo.Name].Value == "1"))
                        {
                            value = true;
                        }
                        pinfo.SetValue(serviceConfig, value, null);
                    }
                    else
                    {
                        pinfo.SetValue(serviceConfig, appConfig.AppSettings.Settings[pinfo.Name].Value, null);
                    }
                }
            }

            return serviceConfig;
        }

        /// <summary>
        /// 保存新服务
        /// </summary>
        /// <param name="serverConfig"></param>
        /// <param name="serviceConfig"></param>
        /// <param name="serviceSetting"></param>
        public static bool SaveNewService(Model.ServerConfig serverConfig, Model.ServiceConfig serviceConfig, Model.ServiceSetting serviceSetting)
        {
            Err = string.Empty;
            try
            {
                CopyService(serverConfig);
                SaveSetting(serverConfig, serviceSetting);
                SaveConfig(serverConfig, serviceConfig);

                SaveConfigList(serverConfig);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存配置列表
        /// </summary>
        /// <param name="serverConfig"></param>
        public static void SaveConfigList(Model.ServerConfig serverConfig)
        {
            GetConfigList();
            //已存在
            int indexNo = ConfigList.ServerConfigs.FindIndex(o => o.ServerID == serverConfig.ServerID);
            if (indexNo >= 0)
            {
                ConfigList.ServerConfigs[indexNo] = serverConfig;
            }
            //未存在
            else
            {
                ConfigList.ServerConfigs.Add(serverConfig);
            }

            XmlSerialize(confPath, ConfigList);
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="serverConfig"></param>
        /// <param name="serviceConfig"></param>
        public static void SaveConfig(Model.ServerConfig serverConfig, Model.ServiceConfig serviceConfig)
        {
            string xmlFileName = StartupPath + "\\" + serverConfig.FileFloderName + serviceConfigPath;
            XmlDocument xmlDoc = new XmlDocument();
            //创建xml的根节点
            XmlElement nodeRoot = xmlDoc.CreateElement("configuration");
            //将根节点加入到xml文件中（AppendChild）
            xmlDoc.AppendChild(nodeRoot);

            //创建startup节点
            XmlElement nodeStartUp = xmlDoc.CreateElement("startup");
            //创建startup节点
            XmlElement nodesupportedRuntime = xmlDoc.CreateElement("supportedRuntime");
            nodesupportedRuntime.SetAttribute("version", "v4.0");
            nodesupportedRuntime.SetAttribute("sku", ".NETFramework,Version=v4.5.2");
            //添加新建的节点
            nodeStartUp.AppendChild(nodesupportedRuntime);
            //添加到根节点
            nodeRoot.AppendChild(nodeStartUp);//添加到根节点

            //创建appSettings节点
            XmlElement nodeAppSettingsp = xmlDoc.CreateElement("appSettings");
            //创建add节点
            System.Reflection.PropertyInfo[] propertyInfo = typeof(Model.ServiceConfig).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                XmlElement nodeAdd = xmlDoc.CreateElement("add");
                nodeAdd.SetAttribute("key", pinfo.Name);
                string value = string.Empty;
                if (pinfo.GetValue(serviceConfig, null) != null)
                {
                    if (pinfo.GetValue(serviceConfig, null) is bool)
                    {
                        if (pinfo.GetValue(serviceConfig, null).ToString().ToUpper() == "TRUE" || pinfo.GetValue(serviceConfig, null).ToString().ToUpper() == "YES")
                        {
                            value = "1";
                        }
                        else
                        {
                            value = "0";
                        }
                    }
                    else
                    {
                        value = pinfo.GetValue(serviceConfig, null) as string;
                    }
                }
                nodeAdd.SetAttribute("value", value);
                //添加新建的节点
                nodeAppSettingsp.AppendChild(nodeAdd);
            }
            //添加到根节点
            nodeRoot.AppendChild(nodeAppSettingsp);//添加到根节点

            //保存xml
            xmlDoc.Save(xmlFileName);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="serverConfig"></param>
        /// <param name="serviceSetting"></param>
        public static void SaveSetting(Model.ServerConfig serverConfig, Model.ServiceSetting serviceSetting)
        {
            string xmlFileName = StartupPath + "\\" + serverConfig.FileFloderName + serviceSettingPath;
            XmlSerialize(xmlFileName, serviceSetting);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="serverConfig"></param>
        /// <param name="serviceConfig"></param>
        public static void CopyService(Model.ServerConfig serverConfig)
        {
            string basePath = StartupPath + "\\ServiceBase";
            string newPath = StartupPath + "\\" + serverConfig.FileFloderName;
            CopyDirectory(basePath, newPath);
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="SaveDirPath"></param>
        public static void CopyDirectory(string sourceDirPath, string SaveDirPath)
        {
            try
            {
                //如果指定的存储路径不存在，则创建该存储路径
                if (!Directory.Exists(SaveDirPath))
                {
                    //创建
                    Directory.CreateDirectory(SaveDirPath);
                }
                //获取源路径文件的名称
                string[] files = Directory.GetFiles(sourceDirPath);
                //遍历子文件夹的所有文件
                foreach (string file in files)
                {
                    string pFilePath = SaveDirPath + "\\" + Path.GetFileName(file);
                    if (File.Exists(pFilePath))
                    {
                        continue;
                    }
                    File.Copy(file, pFilePath, true);
                }
                string[] dirs = Directory.GetDirectories(sourceDirPath);
                //递归，遍历文件夹
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, SaveDirPath + "\\" + Path.GetFileName(dir));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 序列化XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlpath"></param>
        /// <param name="obj"></param>
        public static void XmlSerialize<T>(string xmlpath, T obj)
        {
            StreamWriter writer = new StreamWriter(xmlpath);
            Type t = obj.GetType();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        /// <summary>
        /// 读取XML到实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T XmlConvertModel<T>(string xmlStr) where T : class, new()
        {
            T t = new T();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlStr);
            foreach (XmlNode xnls in xmlDoc.ChildNodes)
            {
                if (xnls.Name.ToUpper() == typeof(T).Name.ToUpper())
                {
                    foreach (XmlNode xnl in xnls.ChildNodes)
                    {
                        System.Reflection.PropertyInfo[] propertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                        {
                            if (xnl.Name.ToUpper() == pinfo.Name.ToUpper())
                            {
                                if (xnl.ChildNodes.Count > 0)
                                {
                                    List<Model.ServerConfig> serverConfigs = new List<Model.ServerConfig>();
                                    int i = 0;
                                    foreach (XmlNode xn in xnl.ChildNodes)
                                    {
                                        Model.ServerConfig serverConfig = new Model.ServerConfig();
                                        foreach (XmlNode x in xn.ChildNodes)
                                        {
                                            System.Reflection.PropertyInfo[] scPropertyInfo = typeof(Model.ServerConfig).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                                            foreach (System.Reflection.PropertyInfo p in scPropertyInfo)
                                            {
                                                if (x.Name.ToUpper() == p.Name.ToUpper())
                                                {
                                                    p.SetValue(serverConfig, x.InnerText, null);
                                                    break;
                                                }
                                            }
                                        }
                                        serverConfigs.Add(serverConfig);
                                    }
                                    pinfo.SetValue(t, serverConfigs, null);
                                }
                                else
                                {
                                    pinfo.SetValue(t, xnl.InnerText, null);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 读取XML到实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T XmlConvertModels<T>(string xmlStr) where T : class, new()
        {
            T t = new T();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlStr);
            foreach (XmlNode xnls in xmlDoc.ChildNodes)
            {
                if (xnls.Name.ToUpper() == typeof(T).Name.ToUpper())
                {
                    foreach (XmlNode xnl in xnls.ChildNodes)
                    {
                        System.Reflection.PropertyInfo[] propertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                        {
                            if (xnl.Name.ToUpper() == pinfo.Name.ToUpper())
                            {
                                pinfo.SetValue(t, xnl.InnerText, null);
                                break;
                            }
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="servicePath">服务路径</param>
        public static bool InstallService(string servicePath)
        {
            try
            {
                #region 旧方法
                string CurrentDirectory = System.Environment.CurrentDirectory;
                System.Environment.CurrentDirectory = CurrentDirectory + "\\" + servicePath;
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "Install.bat";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                System.Environment.CurrentDirectory = CurrentDirectory;

                //this.SetFormServiceState();
                #endregion

                //using (AssemblyInstaller installer = new AssemblyInstaller())
                //{
                //    installer.UseNewContext = true;
                //    //string serviceFilePath = $"{Application.StartupPath}\\{servicePath}\\CSWorkListService.exe";
                //    string serviceFilePath = $"{System.Environment.CurrentDirectory}\\{servicePath}\\CSWorkListService.exe";
                //    installer.Path = serviceFilePath;
                //    IDictionary savedState = new Hashtable();
                //    installer.Install(savedState);
                //    installer.Commit(savedState);
                //}
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="servicePath">服务路径</param>
        public static bool UninstallService(string servicePath)
        {
            try
            {
                #region 旧方法
                string CurrentDirectory = System.Environment.CurrentDirectory;

                System.Environment.CurrentDirectory = CurrentDirectory + "\\" + servicePath;
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "Uninstall.bat";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                System.Environment.CurrentDirectory = CurrentDirectory;

                //this.SetFormServiceState();
                #endregion

                //using (AssemblyInstaller installer = new AssemblyInstaller())
                //{
                //    installer.UseNewContext = true;
                //    //string serviceFilePath = $"{Application.StartupPath}\\{servicePath}\\CSWorkListService.exe";
                //    string serviceFilePath = $"{System.Environment.CurrentDirectory}\\{servicePath}\\CSWorkListService.exe";
                //    installer.Path = serviceFilePath;
                //    installer.Uninstall(null);
                //}
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        public static void StartService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }
            serviceController.Start();
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        public static void StopService(string serviceName)
        {
            //是否存在
            bool isExistService = IsServiceExisted(serviceName);
            if (isExistService)
            {
                ServiceController serviceController = new ServiceController(serviceName);
                if (serviceController.CanStop)
                {
                    serviceController.Stop();
                }
            }
        }

        /// <summary>
        /// 暂停或继续服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        public static void PauseAndContinueService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
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
        }

        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="status">状态名称</param>
        /// <returns></returns>
        public static ServiceControllerStatus GetServiceState(string serviceName, ref string status)
        {
            ServiceControllerStatus serviceControllerStatus = ServiceControllerStatus.Stopped;
            //是否存在
            bool isExistService = IsServiceExisted(serviceName);
            //状态名称
            status = "未安装";

            if (isExistService)
            {
                ServiceController serviceController = new ServiceController(serviceName);
                serviceControllerStatus = serviceController.Status;
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

            return serviceControllerStatus;
        }

        //判断服务是否存在
        public static bool IsServiceExisted(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                return false;
            }
            //获取windows服务列表
            ServiceController[] services = ServiceController.GetServices();
            //循环查找该名称的服务
            //foreach (ServiceController sc in services)
            //{
            //    if (sc.ServiceName.ToLower() == serviceName.ToLower())
            //    {
            //        return true;
            //    }
            //}
            //return false;
            bool isExist = services.ToList().Exists(o => o.ServiceName.ToLower() == serviceName.ToLower());
            return isExist;
        }

        #region 判断ip地址有效性

        /// <summary>  
        /// 验证IPv4地址  
        /// [第一位和最后一位数字不能是0或255；允许用0补位]  
        /// </summary>  
        /// <param name="input">待验证的字符串</param>  
        /// <returns>是否匹配</returns>  
        public static bool IsIPv4(string input)
        {
            string pattern = @"^(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);//指定不区分大小写的匹配 ;
            return regex.IsMatch(input);
            //string[] IPs = input.Split('.');
            //if (IPs.Length != 4)
            //    return false;
            //int n = -1;
            //for (int i = 0; i < IPs.Length; i++)
            //{
            //    if (i == 0 || i == 3)
            //    {
            //        if (int.TryParse(IPs[i], out n) && n > 0 && n < 255)
            //            continue;
            //        else
            //            return false;
            //    }
            //    else
            //    {
            //        if (int.TryParse(IPs[i], out n) && n >= 0 && n <= 255)
            //            continue;
            //        else
            //            return false;
            //    }
            //}
            //return true;
        }
        #endregion

        #region 指定类型的端口是否已经被使用了
        /// <summary>
        /// 指定类型的端口是否已经被使用了
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="type">端口类型</param>
        /// <returns></returns>
        public static bool portInUse(int port, string type)
        {
            bool flag = false;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipendpoints = null;
            if (type.ToUpper() == "TCP")
            {
                ipendpoints = properties.GetActiveTcpListeners();
            }
            else
            {
                ipendpoints = properties.GetActiveUdpListeners();
            }
            foreach (IPEndPoint ipendpoint in ipendpoints)
            {
                if (ipendpoint.Port == port)
                {
                    flag = true;
                    break;
                }
            }
            ipendpoints = null;
            properties = null;
            return flag;
        }
        #endregion

        #endregion
    }
}
