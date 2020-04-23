using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace CSWorkListService
{
    /// <summary>
    /// 【功能说明：日志记录】
    /// </summary>
    class Log
    {
        private static string pathName = "Log";
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="isErr">是否是错误日志</param>
        public static void Loger(string content, bool isErr = false)
        {
            if (!System.IO.Directory.Exists(ServiceStartupPath + "\\" + pathName))
            {
                System.IO.Directory.CreateDirectory(ServiceStartupPath + "\\" + pathName);
            }
            if (!System.IO.Directory.Exists(ServiceStartupPath + "\\" + pathName + "\\" + DateTime.Now.ToString("yyyy-MM-dd")))
            {
                System.IO.Directory.CreateDirectory(ServiceStartupPath + "\\" + pathName + "\\" + DateTime.Now.ToString("yyyy-MM-dd"));
                DeleteLog();
            }

            System.IO.File.AppendAllText(ServiceStartupPath + "\\" + pathName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\DebugLog.log"
                , "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==>" + (isErr ? "Err" : "DEBUG") + ":" + content
                , Encoding.GetEncoding("GB2312")
                );

            if (isErr)
            {
                System.IO.File.AppendAllText(ServiceStartupPath + "\\" + pathName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\ErrLog.log"
                    , "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==>Err:" + content
                    , Encoding.GetEncoding("GB2312")
                    );
            }
        }

        /// <summary>
        /// 本地错误日志信息格式信息:
        /// 0：说明信息；1：入参值；2：返回值；3：出参值；4：是否成功(1:成功 -1:失败)；
        /// </summary>
        public static string MessageFormat = "【{0}】" + Environment.NewLine
                       + "入参值：{1}" + Environment.NewLine
                        + "返回值：{2}" + Environment.NewLine
                        + "出参值：{3}" + Environment.NewLine
                        + "是否成功(1:成功 -1:失败)：{4}" + Environment.NewLine;

        /// <summary>
        /// 服务地址
        /// </summary>
        private static string serviceStartupPath = string.Empty;

        /// <summary>
        /// 服务地址
        /// </summary>
        public static string ServiceStartupPath
        {
            get
            {
                if (string.IsNullOrEmpty(serviceStartupPath))
                {
                    serviceStartupPath = GetWindowsServiceInstallPath(Model.Const.ServiceConsts.ServiceName);
                }
                return serviceStartupPath;
            }
        }

        /// <summary>
        /// 获取服务安装路径
        /// </summary>
        /// <param name="ServiceName">服务名称</param>
        /// <returns></returns>
        public static string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            string path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
            //替换掉双引号   
            path = path.Replace("\"", string.Empty);

            FileInfo fi = new FileInfo(path);
            return fi.Directory.ToString();

        }

        /// <summary>
        /// 清空日志(保留15天)
        /// </summary>
        /// <param name="saveDays">保留天数 默认15天</param>
        private static void DeleteLog(int saveDays = 15)
        {
            string path = ServiceStartupPath + "\\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                DirectoryInfo[] childs = folder.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    DateTime dt = child.CreationTime;
                    if (dt < DateTime.Today.AddDays(-saveDays))
                    {
                        child.Delete(true);
                    }
                }
            }
        }
    }
}
