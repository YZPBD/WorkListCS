using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom;
using Dicom.Network;
using Dicom.Log;
using CSWorkListService.Model;

namespace CSWorkListService
{
    public class WorklistService : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCFindProvider, IDicomNServiceProvider
    {

        private static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[]
           {
                DicomTransferSyntax.ExplicitVRLittleEndian,
                DicomTransferSyntax.ExplicitVRBigEndian,
                DicomTransferSyntax.ImplicitVRLittleEndian
           };

        private IMppsSource _mppsSource;
        private IMppsSource MppsSource
        {
            get
            {
                if (_mppsSource == null) _mppsSource = new MppsHandler(Logger);
                return _mppsSource;
            }
        }




        public WorklistService(INetworkStream stream, Encoding fallbackEncoding, Logger log) : base(stream, fallbackEncoding, log)
        {

        }


        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            Logger.Info($"Received verification request from AE {Association.CallingAE} with IP: {Association.RemoteHost}");
            Log.Loger($"Received verification request from AE {Association.CallingAE} with IP: {Association.RemoteHost}");
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }




        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            // you should validate the level of the request. I leave it here since there is a bug in version 3.0.2
            // from version 4 on this should be done
            //if (request.Level != DicomQueryRetrieveLevel.Worklist)
            //{
            //    yield return new DicomCFindResponse(request, DicomStatus.QueryRetrieveUnableToProcess);
            //}
            //else
            //{
            /*        foreach (DicomDataset result in WorklistHandler.FilterWorklistItems(request.Dataset, WorklistServer.CurrentWorklistItems))
                    {
                        yield return new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = result };
                    }
                    yield return new DicomCFindResponse(request, DicomStatus.Success);
             */
            //}

            // set the connection parameters
            // production envrionment
            /*        var serverIP = "192.168.66.21";
                    var serverPort = 57300;
                    var serverAET = "MXVOBB";
                    var clientAET = "MXVOBB";
        */


            #region Old Code What J8 Thing？
            //bool outOfRange = WorklistItemsProvider.IsOutOfRange(request.Dataset);

            //Logger.Info($"OutofRange: {outOfRange}");
            //Log.Loger($"OutofRange: {outOfRange}");

            //if (outOfRange)
            //{
            //    // if date range is out of cached date range, we just forward the request to query
            //    var worklistItems = WorklistItemsProvider.GetWorkList(request);
            //    foreach (DicomDataset result in worklistItems)
            //    {
            //        yield return new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = result };
            //    }
            //    yield return new DicomCFindResponse(request, DicomStatus.Success);
            //}
            //else
            //{
            //    // if date range is in the cached date range, we filtered the result from the cached data set
            //    foreach (DicomDataset result in WorklistHandler.FilterWorklistItemsByDataset(request.Dataset, WorklistItemsProvider.CurrentWorklistItems, Logger))
            //    {
            //        yield return new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = result };
            //    }
            //    yield return new DicomCFindResponse(request, DicomStatus.Success);
            //}
            #endregion

            //记录查询时间
            System.Diagnostics.Stopwatch stopWatch1 = new System.Diagnostics.Stopwatch();
            stopWatch1.Start();
            DateTime dtRequestBeg = DateTime.Now;
            Logger.Info($"\r\n\r\nOnCFindRequest BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");
            Log.Loger($"\r\n\r\nOnCFindRequest BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");

            bool outOfRange = WorklistItemsProvider.IsOutOfRange(request.Dataset);

            Logger.Info($"OutofRange: {outOfRange}");
            Log.Loger($"OutofRange: {outOfRange}");

            //var worklistItems = WorklistHandler.NewFilterWLIteamsByDataset(request.Dataset, WorklistItemsProvider.CurrentWorklistItems, Logger);
            //var worklistItems = WorklistHandler.NewFilterWLIteamsByDataset(request.Dataset, WorklistItemsProvider.WorklistItems, Logger);
            var worklistItems = WorklistHandler.NewFilterWLIteamsByDataset(WorklistItemsProvider.QueryWorklistItems(request.Dataset), Logger);
            //worklistItems = WorklistItemsProvider.GetWorkList(request);
            foreach (DicomDataset result in worklistItems)
            {
                yield return new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = result };
            }
            yield return new DicomCFindResponse(request, DicomStatus.Success);

            //记录查询时间
            DateTime dtRequestEnd = DateTime.Now;
            Logger.Info($"OnCFindRequest EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
            Log.Loger($"OnCFindRequest EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
            stopWatch1.Stop();
            Logger.Info($"OnCFindRequest SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
            Log.Loger($"OnCFindRequest SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
            Log.Loger($"---------------------------------------------------------------------------------\r\n\r\n\r\n");
            stopWatch1.Reset();
        }


        public void OnConnectionClosed(Exception exception)
        {
            Clean();
        }


        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            //log the abort reason
            Logger.Error($"Received abort from {source}, reason is {reason}");
            Log.Loger($"Received abort from {source}, reason is {reason}", true);
        }


        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            Clean();
            return SendAssociationReleaseResponseAsync();
        }


        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            Logger.Info($"Received association request from AE: {association.CallingAE} with IP: {association.RemoteHost} ");
            Log.Loger($"Received association request from AE: {association.CallingAE} with IP: {association.RemoteHost} ");

            /*   if (WorklistServer.AETitle != association.CalledAE)
                {
                    Logger.Error($"Association with {association.CallingAE} rejected since called aet {association.CalledAE} is unknown");
                    return SendAssociationRejectAsync(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser, DicomRejectReason.CalledAENotRecognized);
                }
             */
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification
                    || pc.AbstractSyntax == DicomUID.ModalityWorklistInformationModelFIND
                    || pc.AbstractSyntax == DicomUID.ModalityPerformedProcedureStepSOPClass
                    || pc.AbstractSyntax == DicomUID.ModalityPerformedProcedureStepNotificationSOPClass
                    || pc.AbstractSyntax == DicomUID.ModalityPerformedProcedureStepNotificationSOPClass)
                {
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                }
                else
                {
                    Logger.Warn($"Requested abstract syntax {pc.AbstractSyntax} from {association.CallingAE} not supported");
                    Log.Loger($"Requested abstract syntax {pc.AbstractSyntax} from {association.CallingAE} not supported");
                    pc.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported);
                }
            }

            Logger.Info($"Accepted association request from {association.CallingAE}");
            Log.Loger($"Accepted association request from {association.CallingAE}");
            return SendAssociationAcceptAsync(association);
        }


        public void Clean()
        {
            // cleanup, like cancel outstanding move- or get-jobs
        }


        public DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request)
        {
            if (request.SOPClassUID != DicomUID.ModalityPerformedProcedureStepSOPClass)
            {
                return new DicomNCreateResponse(request, DicomStatus.SOPClassNotSupported);
            }
            // on N-Create the UID is stored in AffectedSopInstanceUID, in N-Set the UID is stored in RequestedSopInstanceUID
            var affectedSopInstanceUID = request.Command.GetSingleValue<string>(DicomTag.AffectedSOPInstanceUID);
            Logger.Log(LogLevel.Info, $"reeiving N-Create with SOPUID {affectedSopInstanceUID}");
            Log.Loger($"reeiving N-Create with SOPUID {affectedSopInstanceUID}");
            // get the procedureStepIds from the request
            var procedureStepId = request.Dataset
                .GetSequence(DicomTag.ScheduledStepAttributesSequence)
                .First()
                .GetSingleValue<string>(DicomTag.ScheduledProcedureStepID);
            var ok = MppsSource.SetInProgress(affectedSopInstanceUID, procedureStepId);

            return new DicomNCreateResponse(request, ok ? DicomStatus.Success : DicomStatus.ProcessingFailure);
        }


        public DicomNSetResponse OnNSetRequest(DicomNSetRequest request)
        {
            if (request.SOPClassUID != DicomUID.ModalityPerformedProcedureStepSOPClass)
            {
                return new DicomNSetResponse(request, DicomStatus.SOPClassNotSupported);
            }
            // on N-Create the UID is stored in AffectedSopInstanceUID, in N-Set the UID is stored in RequestedSopInstanceUID
            var requestedSopInstanceUID = request.Command.GetSingleValue<string>(DicomTag.RequestedSOPInstanceUID);
            Logger.Log(LogLevel.Info, $"receiving N-Set with SOPUID {requestedSopInstanceUID}");
            Log.Loger($"receiving N-Set with SOPUID {requestedSopInstanceUID}");

            var status = request.Dataset.GetSingleValue<string>(DicomTag.PerformedProcedureStepStatus);
            if (status == "COMPLETED")
            {
                // most vendors send some informations with the mpps-completed message. 
                // this information should be stored into the datbase
                var doseDescription = request.Dataset.GetSingleValueOrDefault(DicomTag.CommentsOnRadiationDose, string.Empty);
                var listOfInstanceUIDs = new List<string>();
                foreach (var seriesDataset in request.Dataset.GetSequence(DicomTag.PerformedSeriesSequence))
                {
                    // you can read here some information about the series that the modalidy created
                    //seriesDataset.Get(DicomTag.SeriesDescription, string.Empty);
                    //seriesDataset.Get(DicomTag.PerformingPhysicianName, string.Empty);
                    //seriesDataset.Get(DicomTag.ProtocolName, string.Empty);
                    foreach (var instanceDataset in seriesDataset.GetSequence(DicomTag.ReferencedImageSequence))
                    {
                        // here you can read the SOPClassUID and SOPInstanceUID
                        var instanceUID = instanceDataset.GetSingleValueOrDefault(DicomTag.ReferencedSOPInstanceUID, string.Empty);
                        if (!string.IsNullOrEmpty(instanceUID)) listOfInstanceUIDs.Add(instanceUID);
                    }
                }
                var ok = MppsSource.SetCompleted(requestedSopInstanceUID, doseDescription, listOfInstanceUIDs);

                return new DicomNSetResponse(request, ok ? DicomStatus.Success : DicomStatus.ProcessingFailure);
            }
            else if (status == "DISCONTINUED")
            {
                // some vendors send a reason code or description with the mpps-discontinued message
                // var reason = request.Dataset.Get(DicomTag.PerformedProcedureStepDiscontinuationReasonCodeSequence);
                var ok = MppsSource.SetDiscontinued(requestedSopInstanceUID, string.Empty);

                return new DicomNSetResponse(request, ok ? DicomStatus.Success : DicomStatus.ProcessingFailure);
            }
            else
            {
                return new DicomNSetResponse(request, DicomStatus.InvalidAttributeValue);
            }
        }


        #region not supported methods but that are required because of the interface

        public DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request)
        {
            Logger.Log(LogLevel.Info, "receiving N-Delete, not supported");
            Log.Loger("receiving N-Delete, not supported");
            return new DicomNDeleteResponse(request, DicomStatus.UnrecognizedOperation);
        }

        public DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request)
        {
            Logger.Log(LogLevel.Info, "receiving N-Event, not supported");
            Log.Loger("receiving N-Event, not supported");
            return new DicomNEventReportResponse(request, DicomStatus.UnrecognizedOperation);
        }

        public DicomNGetResponse OnNGetRequest(DicomNGetRequest request)
        {
            Logger.Log(LogLevel.Info, "receiving N-Get, not supported");
            Log.Loger("receiving N-Get, not supported");
            return new DicomNGetResponse(request, DicomStatus.UnrecognizedOperation);
        }

        public DicomNActionResponse OnNActionRequest(DicomNActionRequest request)
        {
            Logger.Log(LogLevel.Info, "receiving N-Action, not supported");
            Log.Loger("receiving N-Action, not supported");
            return new DicomNActionResponse(request, DicomStatus.UnrecognizedOperation);
        }

        #endregion

    }
}
