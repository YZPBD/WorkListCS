using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Dicom.Log;

namespace CSWorkListService
{
    public partial class CSWorkListService : ServiceBase
    {
        public CSWorkListService()
        {
            InitializeComponent();
            InitService();
        }

        SettingHelper setting;

        /// <summary>
        /// 初始化服务
        /// </summary>
        private void InitService()
        {
            setting = new SettingHelper();
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "ServiceName:" + Model.Const.ServiceConsts.ServiceName);
            }
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Start.");
                }
                // Initialize log manager.
                LogManager.SetImplementation(ConsoleLogManager.Instance);

                //var port = args != null && args.Length > 0 && int.TryParse(args[0], out int tmp) ? tmp : 8004;

                //Console.WriteLine($"Starting QR SCP server with AET: QRSCP on port {port}");

                //WorklistServer.Start(port, "MXVOBB");

                var appConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string clientPort = appConfig.AppSettings.Settings["clientPort"].Value;
                string clientAET = appConfig.AppSettings.Settings["clientAET"].Value;
                int cp = 0;
                var port = int.TryParse(clientPort, out cp) ? cp : 8004;

                //Console.WriteLine($"Starting QR SCP server with AET: CCIP on port {port}");

                WorklistServer.Start(port, clientAET);

                //Console.WriteLine("Press any key to stop the service");

                Console.Read();

                //Console.WriteLine("Stopping QR service");

                //WorklistServer.Stop();
            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("ERROR: ") + ex.Message);
                }
            }
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        protected override void OnStop()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
            }

            //Console.WriteLine("Stopping QR service");

            WorklistServer.Stop();
        }
    }
}
