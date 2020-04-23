using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorkListCS.Model
{
    /// <summary>
    /// 服务配置config实体
    /// </summary>
    public class ServiceConfig
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        public string serverIP
        {
            get;
            set;
        }
        /// <summary>
        /// 服务端端口
        /// </summary>
        public string serverPort
        {
            get;
            set;
        }
        /// <summary>
        /// 服务端AE Title
        /// </summary>
        public string serverAET
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端AE Title
        /// </summary>
        public string clientAET
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端端口
        /// </summary>
        public string clientPort
        {
            get;
            set;
        }
        /// <summary>
        /// 缓存期限
        /// </summary>
        public string cacheDays
        {
            get;
            set;
        }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string deviceType
        {
            get;
            set;
        }
        /// <summary>
        /// 循环时间
        /// </summary>
        public string loopSeconds
        {
            get;
            set;
        }
        /// <summary>
        /// 是否实时获取
        /// </summary>
        public bool isRealTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否仅返回到检状态
        /// </summary>
        public bool isOnlyArrived
        {
            get;
            set;
        }
        /// <summary>
        /// 设置AETitle
        /// </summary>
        public string setAETitle
        {
            get;
            set;
        }
        /// <summary>
        /// 是否不转换患者姓名和编码
        /// </summary>
        public bool isNotConvertPatientId
        {
            get;
            set;
        }
    }
}
