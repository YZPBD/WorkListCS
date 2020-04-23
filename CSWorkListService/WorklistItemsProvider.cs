// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using Dicom;
using Dicom.Network;
using System.Text;
using Dicom.Log;
using CSWorkListService.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace CSWorkListService
{
    public class WorklistItemsProvider
    {
        private static string _serverIP;
        private static int _serverPort;
        private static string _serverAET;
        private static string _clientAET;
        private static int _clientPort;
        private static double _cacheDays;
        private static double _loopSeconds;
        private static bool _isRealTime;
        private static bool _isOnlyArrived;
        private static string _deviceType;
        private static Timer _itemsLoaderTimer;
        private static string _setAETitle;
        private static bool _isNotConvertPatientId;

        public static Logger Logger;

        public static List<DicomDataset> CurrentWorklistItems { get; private set; }

        public static List<DicomDataset> WorklistItems
        {
            get
            {
                if (_isRealTime)
                {
                    var newWorklistItems = GetCachedWorklist();
                    Logger.Info($"Get new work list, length: {newWorklistItems.Count}");
                    Log.Loger($"Get new work list, length: {newWorklistItems.Count}");
                    return newWorklistItems;
                }
                else
                {
                    return CurrentWorklistItems;
                }
            }
        }

        public static List<DicomDataset> QueryWorklistItems(DicomDataset request)
        {
            if (_isRealTime)
            {
                //记录查询时间
                System.Diagnostics.Stopwatch stopWatch1 = new System.Diagnostics.Stopwatch();
                stopWatch1.Start();
                DateTime dtRequestBeg = DateTime.Now;
                Logger.Info($">>QueryWorklistItems CreateWorklistQuery BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");
                Log.Loger($">>QueryWorklistItems CreateWorklistQuery BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");

                DateTime endDate = System.DateTime.Today.AddDays(1).AddSeconds(-1);
                DateTime startDate = endDate.AddDays(_cacheDays * -1);
                string patientId = string.Empty;

                if (!request.TryGetSingleValue(DicomTag.PatientID, out patientId))
                {
                    patientId = null;
                }
                if (request.Contains(DicomTag.ScheduledProcedureStepSequence))
                {
                    DicomDataset procedureStep = request.GetSequence(DicomTag.ScheduledProcedureStepSequence).First();

                    var scheduledProcedureStepStartDate = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty);
                    var scheduledProcedureStepEndDate = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepEndDate, string.Empty);
                    Logger.Info($"Exam scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                    Log.Loger($"Exam scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                    Logger.Info($"Exam scheduledProcedureStepEndDate: {scheduledProcedureStepEndDate}");
                    Log.Loger($"Exam scheduledProcedureStepEndDate: {scheduledProcedureStepEndDate}");

                    var index = scheduledProcedureStepStartDate.IndexOf("-");
                    if (index >= 0)
                    {
                        scheduledProcedureStepEndDate = scheduledProcedureStepStartDate.Substring(index + 1);
                        Logger.Info($"Exam New scheduledProcedureStepEndDate: {scheduledProcedureStepEndDate}");
                        Log.Loger($"Exam New scheduledProcedureStepEndDate: {scheduledProcedureStepEndDate}");

                        scheduledProcedureStepStartDate = scheduledProcedureStepStartDate.Substring(0, index);
                        Logger.Info($"Exam New scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                        Log.Loger($"Exam New scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                    }
                    scheduledProcedureStepStartDate = scheduledProcedureStepStartDate.Replace(":", "").Replace(" ", "");
                    scheduledProcedureStepEndDate = scheduledProcedureStepEndDate.Replace(":", "").Replace(" ", "");

                    if (!string.IsNullOrEmpty(scheduledProcedureStepStartDate) && scheduledProcedureStepStartDate != "*")
                    {
                        startDate = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepStartDate).Get<System.DateTime>();

                    }
                    if (!string.IsNullOrEmpty(scheduledProcedureStepEndDate) && scheduledProcedureStepEndDate != "*")
                    {
                        endDate = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepEndDate).Get<System.DateTime>();
                    }
                }

                DicomDateRange dr = new DicomDateRange(startDate, endDate);
                var cfind = DicomCFindRequest.CreateWorklistQuery(patientId, null, null, null, null, dr);

                //记录查询时间
                DateTime dtRequestEnd = DateTime.Now;
                Logger.Info($">>CreateWorklistQuery EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
                Log.Loger($">>CreateWorklistQuery EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
                stopWatch1.Stop();
                Logger.Info($">>CreateWorklistQuery SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
                Log.Loger($">>CreateWorklistQuery SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
                Log.Loger($"\r\n");

                var newWorklistItems = GetWorkList(cfind);
                Logger.Info($"Get new work list, length: {newWorklistItems.Count}");
                Log.Loger($"Get new work list, length: {newWorklistItems.Count}");
                return newWorklistItems;
            }
            else
            {
                if (CurrentWorklistItems != null && CurrentWorklistItems.Count == 0)
                {
                    var newWorklistItems = GetCachedWorklist();
                    Logger.Info($"Get new work list, length: {newWorklistItems.Count}");
                    Log.Loger($"Get new work list, length: {newWorklistItems.Count}");
                    CurrentWorklistItems = newWorklistItems;
                }
                return CurrentWorklistItems;
            }
        }

        public void Start(Logger logger)
        {
            var appConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (appConfig.AppSettings.Settings.AllKeys.Contains("serverIP"))
            {
                _serverIP = appConfig.AppSettings.Settings["serverIP"].Value;
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("serverPort"))
            {
                _serverPort = int.Parse(appConfig.AppSettings.Settings["serverPort"].Value);
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("serverAET"))
            {
                _serverAET = appConfig.AppSettings.Settings["serverAET"].Value;
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("clientAET"))
            {
                _clientAET = appConfig.AppSettings.Settings["clientAET"].Value;
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("clientPort"))
            {
                _clientPort = int.Parse(appConfig.AppSettings.Settings["clientPort"].Value);
            }
            else
            {
                _clientPort = 8004;
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("cacheDays"))
            {
                _cacheDays = int.Parse(appConfig.AppSettings.Settings["cacheDays"].Value);
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("deviceType"))
            {
                _deviceType = appConfig.AppSettings.Settings["deviceType"].Value;
                _deviceType = _deviceType.ToUpper();
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("loopSeconds"))
            {
                _loopSeconds = int.Parse(appConfig.AppSettings.Settings["loopSeconds"].Value);
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("isRealTime"))
            {
                _isRealTime = appConfig.AppSettings.Settings["isRealTime"].Value.ToString() == "1";
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("isOnlyArrived"))
            {
                _isOnlyArrived = appConfig.AppSettings.Settings["isOnlyArrived"].Value.ToString() == "1";
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("setAETitle"))
            {
                _setAETitle = appConfig.AppSettings.Settings["setAETitle"].Value;
            }
            if (appConfig.AppSettings.Settings.AllKeys.Contains("isNotConvertPatientId"))
            {
                _isNotConvertPatientId = appConfig.AppSettings.Settings["isNotConvertPatientId"].Value.ToString() == "1";
            }
            

           /*       _serverIP = "192.168.66.196";
                  _serverPort = 57301;
                  _serverAET = "MXVOBB1";
                  _clientAET = "MXVOBB1";
                  _cacheDays = 7;
                  _loopSeconds = 180;
          */
           Logger = logger;

            bool hasResult = true;

            _itemsLoaderTimer = new System.Threading.Timer((state) =>
            {
                if (hasResult)
                {
                    hasResult = false;
                    var newWorklistItems = GetCachedWorklist();
                    Logger.Info($"Start Get new work list, length: {newWorklistItems.Count}");
                    Log.Loger($"Start Get new work list, length: {newWorklistItems.Count}");
                    CurrentWorklistItems = newWorklistItems;
                    hasResult = true;
                }

            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(_loopSeconds));
        }

        public static bool IsChinese(string CString)
        {
            bool BoolValue = false;
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = false;
                }
                else
                {
                    return BoolValue = true;
                }
            }
            return BoolValue;
        }

        public static List<DicomDataset> GetWorkList(DicomCFindRequest cfind)
        {
            var worklistItems = new List<DicomDataset>();

            try
            {
                if (cfind != null)
                {
                    int studyNumber = 0;
                    cfind.OnResponseReceived = (DicomCFindRequest rq, DicomCFindResponse rp) =>
                    {
                        Logger.Info($"OnResponseReceived..................");
                        Log.Loger($"OnResponseReceived..................");
                        if (rp.Dataset != null)
                        {
                            try
                            {
                                bool canAddWL = true;
                                //仅到检
                                if (_isOnlyArrived)
                                {
                                    string status = rp.Dataset.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStatus, string.Empty);
                                    //if (status != "ARRIVED")
                                    //{
                                    //    continue;
                                    //}
                                    canAddWL = (status == "ARRIVED");
                                }

                                if (canAddWL)
                                {
                                    Logger.Info($"Before Convert..................");
                                    Log.Loger($"Before Convert..................");
                                    string log = LogToConsole(rp.Dataset);
                                    Logger.Info(log);
                                    Log.Loger(log);

                                    #region PatientName转换
                                    string oldName = rp.Dataset.GetValue<string>(Dicom.DicomTag.PatientName, 0);
                                    string newName = oldName;
                                    string initialsName = string.Empty;
                                    int sepIndex = oldName.IndexOf("^");
                                    string surName = oldName;
                                    string firstName = string.Empty;
                                    string lastName = string.Empty;
                                    if (sepIndex >= 0)
                                    {
                                        //surName = oldName.Substring(0, sepIndex);
                                        firstName = oldName.Split('^')[0];
                                        lastName = oldName.Split('^')[1];
                                        if (string.IsNullOrEmpty(lastName))
                                        {
                                            surName = firstName;
                                        }
                                        else
                                        {
                                            //若拼音码为首字母，则不取
                                            if (firstName.Length == lastName.Length)
                                            {
                                                surName = firstName;
                                            }
                                            else
                                            {
                                                surName = lastName;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        firstName = oldName;
                                    }

                                    //取首字母，保持AccessionNumber一致
                                    if (IsChinese(firstName))
                                    {
                                        initialsName = NPinyin.Pinyin.GetInitials(firstName, true, 1);
                                        initialsName = initialsName.ToUpper();
                                    }

                                    if (IsChinese(surName))
                                    {
                                        //string newSurName = NPinyin.Pinyin.GetPinyin(surName, Encoding.Unicode, true);
                                        string newSurName = NPinyin.Pinyin.GetPinyin(surName, true, 1);
                                        newSurName = newSurName.ToUpper();
                                        //oldName = oldName.Replace(surName, newSurName);
                                        newName = newSurName;

                                        //取首字母
                                        //initialsName = NPinyin.Pinyin.GetInitials(surName, Encoding.Unicode, true);
                                        //initialsName = initialsName.ToUpper();
                                    }
                                    else
                                    {
                                        newName = surName;
                                    }

                                    rp.Dataset.AddOrUpdate(Dicom.DicomTag.PatientName, newName);
                                    #endregion

                                    #region PtianetID修改
                                    if (!_isNotConvertPatientId)
                                    {
                                        string patientId = rp.Dataset.GetSingleValueOrDefault(DicomTag.OtherPatientIDsRETIRED, string.Empty);
                                        string newNameNoSpace = newName;
                                        if (newNameNoSpace.Substring(newNameNoSpace.Length - 1, 1) == " ")
                                        {
                                            newNameNoSpace = newNameNoSpace.Remove(newNameNoSpace.Length - 1);
                                        }
                                        string newPatientId = $"{newNameNoSpace}({patientId})";
                                        if (_deviceType == "MR")
                                        {
                                            newPatientId = _deviceType + " " + newPatientId;
                                        }
                                        rp.Dataset.AddOrUpdate(Dicom.DicomTag.PatientID, newPatientId);
                                    }
                                    #endregion

                                    #region Modality置空
                                    rp.Dataset.GetSequence(Dicom.DicomTag.ScheduledProcedureStepSequence).Items[0].AddOrUpdate(Dicom.DicomTag.Modality, _deviceType);
                                    #endregion

                                    #region SpecificCharacterSet字符集转换
                                    rp.Dataset.AddOrUpdate(Dicom.DicomTag.SpecificCharacterSet, "ISO_IR 100");
                                    #endregion

                                    #region AccessionNumber

                                    string accessionNumber = rp.Dataset.GetSingleValueOrDefault<string>(Dicom.DicomTag.AccessionNumber, "");
                                    if (string.IsNullOrEmpty(accessionNumber))
                                    {
                                        string scheduledProcedureStepStartTime = rp.Dataset.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartTime, string.Empty);
                                        if (scheduledProcedureStepStartTime.Length > 4)
                                        {
                                            scheduledProcedureStepStartTime.Remove(4);
                                        }
                                        accessionNumber = _deviceType + initialsName + scheduledProcedureStepStartTime;
                                        rp.Dataset.AddOrUpdate(Dicom.DicomTag.AccessionNumber, accessionNumber);
                                    }
                                    #endregion

                                    #region StudyInstanceUID
                                    string studyInstanceUID = rp.Dataset.GetSingleValueOrDefault<string>(Dicom.DicomTag.StudyInstanceUID, "");
                                    if (string.IsNullOrEmpty(studyInstanceUID))
                                    {
                                        //rp.Dataset.AddOrUpdate(Dicom.DicomTag.StudyInstanceUID, rp.Dataset.GetValue<string>(Dicom.DicomTag.SOPInstanceUID, 0) + "." + (studyNumber++).ToString());
                                        string sOPInstanceUID = rp.Dataset.GetSingleValueOrDefault<string>(Dicom.DicomTag.SOPInstanceUID, "");
                                        if (sOPInstanceUID.Contains("."))
                                        {
                                            sOPInstanceUID = sOPInstanceUID.Remove(sOPInstanceUID.LastIndexOf('.'));
                                        }
                                        //rp.Dataset.AddOrUpdate(Dicom.DicomTag.StudyInstanceUID, rp.Dataset.GetValue<string>(Dicom.DicomTag.SOPInstanceUID, 0) + "." + (studyNumber++).ToString());
                                        rp.Dataset.AddOrUpdate(Dicom.DicomTag.StudyInstanceUID, sOPInstanceUID);
                                    }
                                    #endregion

                                    #region Set AETitle
                                    if (!string.IsNullOrEmpty(_setAETitle))
                                    {
                                        rp.Dataset.GetSequence(Dicom.DicomTag.ScheduledProcedureStepSequence).Items[0].AddOrUpdate(Dicom.DicomTag.ScheduledStationAETitle, _setAETitle);
                                    }
                                    #endregion

                                    #region Set Scheduled Protocol Code Sequence
                                    Dicom.DicomSequence spcs = new Dicom.DicomSequence(Dicom.DicomTag.ScheduledProtocolCodeSequence);
                                    if (!rp.Dataset.GetSequence(Dicom.DicomTag.ScheduledProcedureStepSequence).Items[0].TryGetSequence(Dicom.DicomTag.ScheduledProtocolCodeSequence, out spcs))
                                    {
                                        Dicom.DicomDataset dd = new DicomDataset();
                                        dd.AddOrUpdate(Dicom.DicomTag.CodeValue, "");
                                        dd.AddOrUpdate(Dicom.DicomTag.CodingSchemeDesignator, "");
                                        dd.AddOrUpdate(Dicom.DicomTag.CodingSchemeVersion, "");
                                        dd.AddOrUpdate(Dicom.DicomTag.CodeMeaning, "");
                                        spcs = new Dicom.DicomSequence(Dicom.DicomTag.ScheduledProtocolCodeSequence, new DicomDataset[] { dd });
                                        rp.Dataset.GetSequence(Dicom.DicomTag.ScheduledProcedureStepSequence).Items[0].Add(Dicom.DicomTag.ScheduledProtocolCodeSequence, dd);
                                        //rp.Dataset.GetSequence(Dicom.DicomTag.ScheduledProtocolCodeSequence).Items.Add(dd);
                                    }

                                    #endregion

                                    Logger.Info($"After Convert..................");
                                    Log.Loger($"After Convert..................");
                                    log = LogToConsole(rp.Dataset);
                                    Logger.Info(log);
                                    Log.Loger(log);
                                    Logger.Info($"\r\n");
                                    Log.Loger($"\r\n");

                                    worklistItems.Add(rp.Dataset);
                                }

                            }
                            catch (Exception ex)
                            {
                                string log = " Convert Failed: \r\n" + ex.Message;
                                Logger.Info($"\r\n {log}");
                                Log.Loger($"\r\n {log}");
                            }
                        }
                    };

                    var client = new DicomClient();
                    client.AddRequest(cfind);
                    Logger.Info($"Sending request async..................");
                    Log.Loger($"Sending request async..................");
                    client.SendAsync(_serverIP, _serverPort, false, _clientAET, _serverAET).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                string log = " Convert Failed: \r\n" + ex.Message;
                Logger.Info($"\r\n {log}");
                Log.Loger($"\r\n {log}");
            }

            return worklistItems;
        }


        public static List<DicomDataset> GetCachedWorklist()
        {
            //记录查询时间
            System.Diagnostics.Stopwatch stopWatch1 = new System.Diagnostics.Stopwatch();
            stopWatch1.Start();
            DateTime dtRequestBeg = DateTime.Now;
            Logger.Info($">>GetCachedWorklist CreateWorklistQuery BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");
            Log.Loger($">>GetCachedWorklist CreateWorklistQuery BeginTime: {dtRequestBeg.ToString("yyyy-MM-dd HH:mm:ss")}");

            DateTime endDate = System.DateTime.Today.AddDays(1).AddSeconds(-1);
            DateTime startDate = endDate.AddDays(_cacheDays * -1);
            DicomDateRange dr = new DicomDateRange(startDate, endDate);
            var cfind = DicomCFindRequest.CreateWorklistQuery(null, null, null, null, null, dr);

            //记录查询时间
            DateTime dtRequestEnd = DateTime.Now;
            Logger.Info($">>CreateWorklistQuery EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
            Log.Loger($">>CreateWorklistQuery EndTime: {dtRequestEnd.ToString("yyyy-MM-dd HH:mm:ss")}");
            stopWatch1.Stop();
            Logger.Info($">>CreateWorklistQuery SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
            Log.Loger($">>CreateWorklistQuery SpentTime: {stopWatch1.Elapsed.TotalSeconds}");
            Log.Loger($"\r\n");

            return GetWorkList(cfind);
        }



        public static bool IsOutOfRange(DicomDataset request)
        {
            if (request.Contains(DicomTag.ScheduledProcedureStepSequence))
            {
                DicomDataset procedureStep = request.GetSequence(DicomTag.ScheduledProcedureStepSequence).First();
                var scheduledProcedureStepStartDate = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty);
                Logger.Info($"scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                Log.Loger($"scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                if (!string.IsNullOrEmpty(scheduledProcedureStepStartDate) && scheduledProcedureStepStartDate != "*")
                {
                    var index = scheduledProcedureStepStartDate.IndexOf("-");
                    if (index >= 0)
                    {
                        scheduledProcedureStepStartDate = scheduledProcedureStepStartDate.Substring(0, index);
                        Logger.Info($"New scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                        Log.Loger($"New scheduledProcedureStepStartDate: {scheduledProcedureStepStartDate}");
                    }
                    if (string.IsNullOrEmpty(scheduledProcedureStepStartDate))
                    {
                        return true;
                    }
                    DateTime endDate = System.DateTime.Today.AddDays(1).AddSeconds(-1);
                    DateTime startDate = endDate.AddDays(_cacheDays * -1);
                    //DicomDateRange range = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, startDate).Get<DicomDateRange>();
                    DicomDateRange range = new DicomDateRange(startDate, endDate);
                    DateTime queryStartDate = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepStartDate).Get<DateTime>();
                    return !range.Contains(queryStartDate);
                }
            }

            return false;
        }

        /// <summary>
        /// 转换DicomDataset为日志字符串
        /// </summary>
        /// <param name="ddt">DicomDataset</param>
        /// <param name="lev">级别</param>
        /// <returns></returns>
        public static string LogToConsole(DicomDataset ddt, int lev = 0)
        {
            string log = string.Empty;
            List<DicomItem> dicomItems = ddt.ToList();
            int ddtCount = ddt.Count();
            string sLev = " ";
            for (int i = 0; i < lev; i++)
            {
                sLev += "\t";
            }
            for (int i = 0; i < ddtCount; i++)
            {
                DicomItem dicomItem = dicomItems[i];
                if (dicomItem is Dicom.DicomSequence)
                {
                    //log = $"  Tag: \"{dicomItem.Tag.DictionaryEntry.Name}\" \"{dicomItem.Tag.DictionaryEntry.Tag.ToString()}\", VR: \"{dicomItem.ValueRepresentation.ToString()}\", VM: {ddt.GetSequence(dicomItem.Tag).Items.Count.ToString()}";
                    log = log + "\r\n" + $"{sLev}{dicomItem.Tag.DictionaryEntry.Tag.ToString().PadRight(12)} {dicomItem.ValueRepresentation.Code.ToString().PadRight(5)} ({ddt.GetSequence(dicomItem.Tag).Items.Count.ToString().PadLeft(3)}) {dicomItem.Tag.DictionaryEntry.Name}";
                    foreach (DicomDataset ddts in ddt.GetSequence(dicomItem.Tag).Items)
                    {
                        log = log + LogToConsole(ddts, lev + 1);
                    }
                }
                else
                {
                    if (ddt.GetValueCount(dicomItem.Tag) > 0)
                    {
                        //log = $"{sLev}Tag: \"{dicomItem.Tag.DictionaryEntry.Name}\" \"{dicomItem.Tag.DictionaryEntry.Tag.ToString()}\", VR: \"{dicomItem.ValueRepresentation.Code.ToString()}\", VM: {((Dicom.DicomMultiStringElement)dicomItem).Count.ToString()}, Value: \"{ddt.GetValue<string>(dicomItem.Tag, 0)}\"";

                        log = log + "\r\n" + $"{sLev}{dicomItem.Tag.DictionaryEntry.Tag.ToString().PadRight(12)} {dicomItem.ValueRepresentation.Code.ToString().PadRight(5)} {($"|{ddt.GetValue<string>(dicomItem.Tag, 0)}|").PadRight(20)} ({((Dicom.DicomMultiStringElement)dicomItem).Count.ToString().PadLeft(3)}) {dicomItem.Tag.DictionaryEntry.Name}";
                    }
                    else
                    {
                        //log = $"{sLev}Tag: \"{dicomItem.Tag.DictionaryEntry.Name}\" \"{dicomItem.Tag.DictionaryEntry.Tag.ToString()}\", VR: \"{dicomItem.ValueRepresentation.Code.ToString()}\", VM: {((Dicom.DicomMultiStringElement)dicomItem).Count.ToString()}, Value: \"\"";

                        log = log + "\r\n" + $"{sLev}{dicomItem.Tag.DictionaryEntry.Tag.ToString().PadRight(12)} {dicomItem.ValueRepresentation.Code.ToString().PadRight(5)} {($"|<null>|").PadRight(20)} ({((Dicom.DicomMultiStringElement)dicomItem).Count.ToString().PadLeft(3)}) {dicomItem.Tag.DictionaryEntry.Name}";
                    }
                }
            }

            return log;
        }

    }
}
