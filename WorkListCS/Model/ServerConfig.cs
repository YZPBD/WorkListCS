using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorkListCS.Model
{
    /// <summary>
    /// 服务设置实体
    /// </summary>
    [XmlRootAttribute("ServerConfig", IsNullable = false)]
    public class ServerConfig
    {
        /// <summary>
        /// 服务编码
        /// </summary>
        [XmlElement("ServerID")]
        public string ServerID
        {
            get;
            set;
        }
        /// <summary>
        /// 服务名
        /// </summary>
        [XmlElement("ServerName")]
        public string ServerName
        {
            get;
            set;
        }
        /// <summary>
        /// 服务文件夹
        /// </summary>
        [XmlElement("FileFloderName")]
        public string FileFloderName
        {
            get;
            set;
        }
    }
}
