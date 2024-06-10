using UnityEngine;
using UnityEditor;

namespace HTN.PlanningData.CSV
{
    public static class CSVTools
    {
        [MenuItem("CSVTools/Test/HTN/Add To Main Report")]
        static void DEV_AppendToMainReport()
        {
            Debug.Log("CSV Test Main Report started");
            CSVManager.AppendToMainReport("1", "1", "1", "1", "1", 0);
        }

        [MenuItem("CSVTools/Test/HTN/Add To Plans Report")]
        static void DEV_AppendToPlansReport()
        {
            Debug.Log("CSV Test Plans Report started");
            CSVManager.AppendToPlansReport("1", "1", "1", 0);
        }
    }
}