using UnityEngine;
using System.IO;
using UnityEditor;

namespace HTN.PlanningData.CSV
{
    public static class CSVManager
    {
        private static string _reportDirectoryName = "HTN_CSVReport";
        private static string _reportSeparator = ";";
        private static string _mainReportFileName = "htn_moreApples_mainreport";
        private static string _plansReportFileName = "htn_moreApples_plansreport";

        private static string[] _mainReportHeaders = new string[5] {
            "AgentID",
            "TotalPlans",
            "PlansSucceeded",
            "Replans",
            "TimeToMainGoal"
        };

        private static string[] _plansReportHeaders = new string[3] {
            "AgentID",
            "PlanLength",
            "PlanTime"
        };

#region Interactions
        public static void AppendToMainReport(string AgentID, string TotalPlans, string PlansSucceeded, string Replans, string TimeToMainGoal, int AgentCount)
        {
            Debug.Log($"CSV Append Main - ID: {AgentID}, TP: {TotalPlans}, S: {PlansSucceeded}, R: {Replans}, T: {TimeToMainGoal}");
            AppendToReport(GetMainReportFilePath(AgentCount), new string[5] {
                AgentID,
                TotalPlans,
                PlansSucceeded,
                Replans,
                TimeToMainGoal
            }, 
            _mainReportHeaders);
        }

        public static void AppendToPlansReport(string AgentID, string PlanLength, string PlanTime, int AgentCount)
        {
            Debug.Log($"CSV Append Plan - ID: {AgentID}, L: {PlanLength}, T: {PlanTime}");
            AppendToReport(GetPlansReportFilePath(AgentCount), new string[3] {
                AgentID,
                PlanLength,
                PlanTime
            },
            _plansReportHeaders);
        }

        private static void AppendToReport(string file, string[] strings, string[] headers)
        {
            VerifyDirectory();
            VerifyFile(file, headers);
            using (StreamWriter sw = File.AppendText(file))
            {
                string line = GetStringLineForCSV(strings);
                sw.WriteLine(line);
            }
        }

        private static void CreateReport(string file, string[] headers)
        {
            VerifyDirectory();
            using (StreamWriter sw = File.CreateText(file))
            {
                string headersString = GetStringLineForCSV(headers);
                sw.WriteLine(headersString);
            }
        }
#endregion


#region Operations
        private static string GetStringLineForCSV(string[] strings)
        {
            string finalString = "";
            foreach (string str in strings)
            {
                if (finalString != "")
                {
                    finalString += _reportSeparator;
                }

                finalString += str;
            }

            return finalString;
        }
        static void VerifyDirectory()
        {
            string dir = GetDirectoryPath();
            if (Directory.Exists(dir))
                return;
            
            Directory.CreateDirectory(dir);
        }
        static void VerifyFile(string file, string[] headers)
        {
            if (DoesFileExist(file))
                return;

            CreateReport(file, headers);
        }
#endregion


#region Queries
        static bool DoesFileExist(string file)
        {
            return File.Exists(file);
        }
        static string GetDirectoryPath()
        {
            return Application.dataPath + "/" + _reportDirectoryName;
        }
        static string GetMainReportFilePath(int AgentCount)
        {
            return GetDirectoryPath() + "/" + _mainReportFileName + "_" + AgentCount.ToString() + ".csv";
        }
        static string GetPlansReportFilePath(int AgentCount)
        {
            return GetDirectoryPath() + "/" + _plansReportFileName + "_" + AgentCount.ToString() + ".csv";
        }
#endregion
    }
}