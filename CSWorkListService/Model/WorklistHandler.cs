// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dicom.Log;

namespace CSWorkListService.Model
{

    public class WorklistHandler
    {

        public static IEnumerable<DicomDataset> FilterWorklistItemsByDataset(DicomDataset request, List<DicomDataset> allDatasetItems, Logger Logger)
        {
            var exams = allDatasetItems.AsQueryable();

            if (request.TryGetSingleValue(DicomTag.PatientID, out string patientId))
            {
                if (patientId != "*")
                {
                    Logger.Info($"Exam patientId: {patientId}");
                    Log.Loger($"Exam patientId: {patientId}");
                    exams = exams.Where(x => x.GetSingleValue<string>(DicomTag.PatientID).Equals(patientId));
                }
            }

      /*      var patientName = request.GetSingleValueOrDefault(DicomTag.PatientName, string.Empty);
            if (!string.IsNullOrEmpty(patientName))
            {
                Logger.Info($"Exam patientName: {patientName}");
                exams = exams.Where(x => x.GetSingleValueOrDefault<string>(DicomTag.PatientName, string.Empty).Contains(patientName));
            }
*/
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

                if (!string.IsNullOrEmpty(scheduledProcedureStepStartDate) && scheduledProcedureStepStartDate != "*" &&
                            !string.IsNullOrEmpty(scheduledProcedureStepEndDate) && scheduledProcedureStepEndDate != "*")
                {
                    var st = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepStartDate).Get<System.DateTime>();
                    var et = new DicomDateTime(DicomTag.ScheduledProcedureStepEndDate, scheduledProcedureStepEndDate).Get<System.DateTime>();
                    var range = new DicomDateRange(st, et);
                    exams = exams.Where(x => range.Contains(System.DateTime.ParseExact(x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)));

                }
                else if (string.IsNullOrEmpty(scheduledProcedureStepStartDate))
                {
                    // if no start date is specified, we just query today's data
                    System.DateTime endDate = System.DateTime.Today.AddDays(1).AddSeconds(-1);
                    System.DateTime startDate = System.DateTime.Today;
                    DicomDateRange range = new DicomDateRange(startDate, endDate);
                    exams = exams.Where(x => range.Contains(System.DateTime.ParseExact(x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)));
                }

            }



            /*
            var scheduledProcedureStepStartDateTime = request.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDateTime, string.Empty);
            if (!string.IsNullOrEmpty(scheduledProcedureStepStartDateTime) && scheduledProcedureStepStartDateTime != "*")
            {
                DicomDateRange range = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepStartDateTime).Get<DicomDateRange>();
                exams = exams.Where(x => range.Contains(System.DateTime.Parse(x.GetSingleValueOrDefault<string>(DicomTag.ScheduledProcedureStepStartDateTime, string.Empty))));
            }
            */

            var results = exams.ToList();
            Logger.Info($"Exam Resutls number: {results.Count}");
            Log.Loger($"Exam Resutls number: {results.Count}");

            //  Parsing result 
            foreach (var result in results)
            {
                string log = WorklistItemsProvider.LogToConsole(result);
                Logger.Info(log);
                Log.Loger(log); 
                // Send Reponse Back
                yield return result;
            }

        }

        public static IEnumerable<DicomDataset> NewFilterWLIteamsByDataset(DicomDataset request, List<DicomDataset> allDatasetItems, Logger Logger)
        {
            var exams = allDatasetItems.AsQueryable();

            if (request.TryGetSingleValue(DicomTag.PatientID, out string patientId))
            {
                if (patientId != "*" && !string.IsNullOrEmpty(patientId))
                {
                    Logger.Info($"Exam patientId: {patientId}");
                    Log.Loger($"Exam patientId: {patientId}");
                    exams = exams.Where(x => x.GetSingleValue<string>(DicomTag.PatientID).Equals(patientId));
                }
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
                    var st = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepStartDate).Get<System.DateTime>();
                    exams = exams.Where(x => st <= System.DateTime.ParseExact(
                        x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty)
                        + x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartTime, string.Empty)
                        , "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));

                }
                if (!string.IsNullOrEmpty(scheduledProcedureStepEndDate) && scheduledProcedureStepEndDate != "*")
                {
                    var et = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, scheduledProcedureStepEndDate).Get<System.DateTime>();
                    exams = exams.Where(x => et >= System.DateTime.ParseExact(
                        x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDate, string.Empty)
                        + x.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartTime, string.Empty)
                        , "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));
                }

            }
            //仅到检
            var appConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (appConfig.AppSettings.Settings.AllKeys.Contains("isOnlyArrived"))
            {
                bool _isOnlyArrived = appConfig.AppSettings.Settings["isOnlyArrived"].Value.ToString() == "1";
                if (_isOnlyArrived)
                {
                    exams = exams.Where(o => o.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStatus, string.Empty) == "ARRIVED");
                }
            }
            //exams = exams.Where(o => o.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStatus, string.Empty) == "ARRIVED");

            var results = exams.ToList();
            Logger.Info($"Exam Resutls number: {results.Count}");
            Log.Loger($"Exam Resutls number: {results.Count}");

            //  Parsing result 
            foreach (var result in results)
            {
                string log = WorklistItemsProvider.LogToConsole(result);
                Logger.Info(log);
                Log.Loger(log);
                // Send Reponse Back
                yield return result;
            }

        }

        public static IEnumerable<DicomDataset> NewFilterWLIteamsByDataset(List<DicomDataset> allDatasetItems, Logger Logger)
        {
            var exams = allDatasetItems.AsQueryable();
            var results = exams.ToList();
            Logger.Info($"Exam Resutls number: {results.Count}");
            Log.Loger($"Exam Resutls number: {results.Count}");

            //  Parsing result 
            foreach (var result in results)
            {
                string log = WorklistItemsProvider.LogToConsole(result);
                Logger.Info(log);
                Log.Loger(log);
                // Send Reponse Back
                yield return result;
            }

        }

        public static IEnumerable<DicomDataset> FilterWorklistItems(DicomDataset request, List<WorklistItem> allWorklistItems)
        {
            var exams = allWorklistItems.AsQueryable();

            if ( request.TryGetSingleValue(DicomTag.PatientID, out string patientId))
            {
                exams = exams.Where(x => x.PatientID.Equals(patientId));
            }

            var patientName = request.GetSingleValueOrDefault(DicomTag.PatientName, string.Empty);
            if (!string.IsNullOrEmpty(patientName))
            {
                exams = AddNameCondition(exams, patientName);
            }

            DicomDataset procedureStep = null;
            if (request.Contains(DicomTag.ScheduledProcedureStepSequence))
            {
                procedureStep = request.GetSequence(DicomTag.ScheduledProcedureStepSequence).First();

                // Required Matching keys
                var scheduledStationAET = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledStationAETitle, string.Empty);
                if (!string.IsNullOrEmpty(scheduledStationAET))
                {
                    exams = exams.Where(x => x.ScheduledAET == scheduledStationAET);
                }

                var performingPhysician = procedureStep.GetSingleValueOrDefault(DicomTag.PerformingPhysicianName, string.Empty);
                if (!string.IsNullOrEmpty(performingPhysician))
                {
                    exams = exams.Where(x => x.PerformingPhysician == performingPhysician);
                }

                var modality = procedureStep.GetSingleValueOrDefault(DicomTag.Modality, string.Empty);
                if (!string.IsNullOrEmpty(modality))
                {
                    exams = exams.Where(x => x.Modality == modality);
                }

                // if only date is specified, then using standard matching
                // but if both are specified, then MWL defines a combined match
                var scheduledProcedureStepStartDateTime = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepStartDateTime, string.Empty);
                if (!string.IsNullOrEmpty(scheduledProcedureStepStartDateTime))
                {
                    exams = AddDateCondition(exams, scheduledProcedureStepStartDateTime);
                }

                // Optional (but commonly used) matching keys.
                var procedureStepLocation = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepLocation, string.Empty);
                if (!string.IsNullOrEmpty(procedureStepLocation))
                {
                    exams = exams.Where(x => x.ExamRoom.Equals(procedureStepLocation));
                }

                var procedureDescription = procedureStep.GetSingleValueOrDefault(DicomTag.ScheduledProcedureStepDescription, string.Empty);
                if (!string.IsNullOrEmpty(procedureDescription))
                {
                    exams = exams.Where(x => x.ExamDescription.Equals(procedureDescription));
                }
            }

            var results = exams.ToList();

            //  Parsing result 
            foreach (var result in results)
            {
                var resultingSPS = new DicomDataset();
                var resultDataset = new DicomDataset();
                var resultingSPSSequence = new DicomSequence(DicomTag.ScheduledProcedureStepSequence, resultingSPS);

                if (procedureStep != null)
                {
                    resultDataset.Add(resultingSPSSequence);
                }

                // add results to "main" dataset
                AddIfExistsInRequest(resultDataset, request, DicomTag.AccessionNumber, result.AccessionNumber);    // T2
                AddIfExistsInRequest(resultDataset, request, DicomTag.InstitutionName, result.HospitalName);
                AddIfExistsInRequest(resultDataset, request, DicomTag.ReferringPhysicianName, result.ReferringPhysician); // T2

                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientName, result.Surname + "^" + result.Forename + "^^" + result.Title); //T1
                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientID, result.PatientID); // T1
                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientBirthDate, result.DateOfBirth); // T2
                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientSex, result.Sex); //T2

                AddIfExistsInRequest(resultDataset, request, DicomTag.StudyInstanceUID, result.StudyUID); // T1

                AddIfExistsInRequest(resultDataset, request, DicomTag.RequestingPhysician, result.ReferringPhysician); //T2
                AddIfExistsInRequest(resultDataset, request, DicomTag.RequestedProcedureDescription, result.ExamDescription); //T1C

                AddIfExistsInRequest(resultDataset, request, DicomTag.RequestedProcedureID, result.ProcedureID); // T1

                // Scheduled Procedure Step sequence T1
                // add results to procedure step dataset
                // Return if requested
                if (procedureStep != null)
                {
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledStationAETitle, result.ScheduledAET); // T1
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledProcedureStepStartDate, result.ExamDateAndTime); //T1
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledProcedureStepStartTime, result.ExamDateAndTime); //T1
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.Modality, result.Modality); // T1

                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledPerformingPhysicianName, result.PerformingPhysician); //T2
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledProcedureStepDescription, result.ExamDescription); // T1C
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledProcedureStepID, result.ProcedureStepID); // T1
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledStationName, result.ExamRoom); //T2
                    AddIfExistsInRequest(resultingSPS, procedureStep, DicomTag.ScheduledProcedureStepLocation, result.ExamRoom); //T2
                }

                // Put blanks in for unsupported fields which are type 2 (i.e. must have a value even if NULL)
                // In a real server, you may wish to support some or all of these, but they are not commonly supported
                AddIfExistsInRequest(resultDataset, request, DicomTag.ReferencedStudySequence, new DicomDataset());         // Ref//d Study Sequence
                AddIfExistsInRequest(resultDataset, request, DicomTag.Priority, "");                                  // Priority
                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientTransportArrangements, "");              // Transport Arrangements
                AddIfExistsInRequest(resultDataset, request, DicomTag.AdmissionID, "");                               // Admission ID
                AddIfExistsInRequest(resultDataset, request, DicomTag.CurrentPatientLocation, "");                    // Patient Location
                AddIfExistsInRequest(resultDataset, request, DicomTag.ReferencedPatientSequence, new DicomDataset());       // Ref//d Patient Sequence
                AddIfExistsInRequest(resultDataset, request, DicomTag.PatientWeight, "");                             // Weight
                AddIfExistsInRequest(resultDataset, request, DicomTag.ConfidentialityConstraintOnPatientDataDescription, ""); // Confidentiality Constraint

                // Send Reponse Back
                yield return resultDataset;
            }
        }


        //Splits patient name into 2 separte strings surname and forename and send then to the addstringcondition subroutine.
        internal static IQueryable<WorklistItem> AddNameCondition(IQueryable<WorklistItem> exams, string dicomName)
        {
            if (string.IsNullOrEmpty(dicomName) || dicomName == "*")
                return exams;

            DicomPersonName personName = new DicomPersonName(DicomTag.PatientName, dicomName);
            if (dicomName.Contains("*"))
            {
                var firstNameRegex = new Regex("^" + Regex.Escape(personName.First).Replace("\\*", ".*") + "$");
                var lastNameRegex = new Regex("^" + Regex.Escape(personName.Last).Replace("\\*", ".*") + "$");
                exams = exams.Where(x => firstNameRegex.IsMatch(x.Forename) || lastNameRegex.IsMatch(x.Surname));
            }
            else
                exams = exams.Where(x => (x.Forename.Equals(personName.First) && x.Surname.Equals(personName.Last)));

            return exams;
        }



        internal static IQueryable<WorklistItem> AddDateCondition(IQueryable<WorklistItem> exams, string dateCondition)
        {
            if (!string.IsNullOrEmpty(dateCondition) && dateCondition != "*")
            {
                var range = new DicomDateTime(DicomTag.ScheduledProcedureStepStartDate, dateCondition).Get<DicomDateRange>();
                exams = exams.Where(x => range.Contains(x.ExamDateAndTime));
            }
            return exams;
        }


        internal static void AddIfExistsInRequest<T>(DicomDataset result, DicomDataset request, DicomTag tag, T value)
        {
            // Only send items which have been requested
            if (request.Contains(tag))
            {
                if (value == null) value = default(T);
                result.AddOrUpdate(tag, value);
            }
        }


    }
}
