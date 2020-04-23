using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorkListCS.Model
{
    /// <summary>
    /// 服务实体
    /// </summary>
    [XmlRootAttribute("Settings", IsNullable = false)]
    public class ServiceSetting
    {
        /// <summary>
        /// 服务名
        /// </summary>
        [XmlElement("ServiceName")]
        public string ServiceName
        {
            get;
            set;
        }
        /// <summary>
        /// 服务显示名
        /// </summary>
        [XmlElement("DisplayName")]
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>
        /// 服务描述
        /// </summary>
        [XmlElement("Description")]
        public string Description
        {
            get;
            set;
        }
    }
}
