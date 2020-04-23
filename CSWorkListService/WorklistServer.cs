using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Dicom.Network;
using CSWorkListService.Model;

namespace CSWorkListService
{
    class WorklistServer
    {

        private static IDicomServer _server;
        private static Timer _itemsLoaderTimer;
        public static WorklistItemsProvider CreateItemsSourceService => new WorklistItemsProvider();


        protected WorklistServer()
        {
        }

        public static string AETitle { get; set; }


        public static List<WorklistItem> CurrentWorklistItems { get; private set; }

        public static void Start(int port, string aet)
        {
            AETitle = aet;
            _server = DicomServer.Create<WorklistService>(port);
            CreateItemsSourceService.Start(_server.Logger);
        }


        public static void Stop()
        {
            _server.Dispose();
        }


    }
}
