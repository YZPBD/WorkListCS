using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorkListCS.Model
{
    /// <summary>
    /// 服务设置列表
    /// </summary>
    [XmlRootAttribute("ServerList", IsNullable = false)]
    public class ServerList
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        [XmlArrayAttribute("ServerConfigs")]
        public List<ServerConfig> ServerConfigs
        {
            get;
            set;
        }
    }
}
