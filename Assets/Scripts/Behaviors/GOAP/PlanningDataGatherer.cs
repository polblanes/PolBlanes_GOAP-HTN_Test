using System;
using System.Collections.Generic;
using FluidHTN;
using GameUtils;
using GOAP.PlanningData.CSV;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using Behaviours.GOAP;

namespace GOAP.PlanningData
{
    [System.Serializable]
    class AIPlanData
    {
        public AIPlanData(string agentID, int planLength, float planTime)
        {
            _agentID = agentID;
            _planLength = planLength;
            _planTime = planTime;
        }

        string _agentID;

        [SerializeField]
        int _planLength;

        [SerializeField]
        float _planTime;

        public void LogData(int AgentCount)
        {
            CSVManager.AppendToPlansReport(
                _agentID,
                _planLength.ToString(),
                _planTime.ToString("F6"),
                AgentCount
            );
        }
    }

    [System.Serializable]
    class AIAgentPlanningData
    {
        public AIAgentPlanningData(AIAgent agent, int agentID)
        {
            _agent = agent;
            _agentID = agentID.ToString();
            _plans = new List<AIPlanData>();
            _replans = 0;
            _plansSucceeded = 0;
            _timeToMainGoalsSucceeded = -1.0f;

            _agent.OnMainGoalsAchieved += OnMainGoalsSucceeded;
            _agent.OnNewPlan += OnNewPlan;
            _agent.OnReplan += OnReplan;
            _agent.OnPlanSucceeded += OnPlanSucceeded;
        }

        AIAgent _agent;

        [SerializeField]
        string _agentID;

        [SerializeField]
        List<AIPlanData> _plans;

        [SerializeField]
        int _replans;

        [SerializeField]
        int _plansSucceeded;

        [SerializeField]
        float _timeToMainGoalsSucceeded;

        void OnNewPlan(int planLength, float planTime)
        {
            _plans.Add(new AIPlanData(_agentID, planLength, planTime));
        }
        void OnReplan()
        {
            _replans += 1;
        }
        void OnPlanSucceeded()
        {
            _plansSucceeded += 1;
        }
        void OnMainGoalsSucceeded(float time)
        {
            _timeToMainGoalsSucceeded = time;
        }

        public void LogData(int AgentCount)
        {
            CSVManager.AppendToMainReport(
                _agentID, 
                _plans.Count.ToString(),
                _plansSucceeded.ToString(),
                _replans.ToString(),
                _timeToMainGoalsSucceeded.ToString("F6"),
                AgentCount
                );
            
            foreach (AIPlanData plan in _plans)
            {
                plan.LogData(AgentCount);
            }
        }

        public bool IsAgent(AIAgent Agent)
        {
            return _agent == Agent;
        }
    }

    class PlanningDataGatherer : MonoBehaviour
    {
        [SerializeField]
        private bool EnableLogs = false;

        public bool bEnableLogs {get; private set;}

        [SerializeField]
        private float timePassedInMinutes = 0.0f;

        [SerializeField]
        private float testDurationInMinutes;

        [SerializeField]
        public bool bTestLogged = false;

        [SerializeField]
        int AgentsCompletedMainGoal = 0;

        [SerializeField]
        List<AIAgentPlanningData> _data = new List<AIAgentPlanningData>();

        int AgentCount = 0;

        private void Awake()
        {
            bEnableLogs = EnableLogs;
        }

        private void Start()
        {
            if (!bEnableLogs)
                return;

            SurvivorSpawnBehavior spawner = FindObjectOfType<SurvivorSpawnBehavior>();
            if (spawner != null)
            {
                AgentCount = spawner.agentAmount;
            }
        }

        private void Update()
        {
            if (!bEnableLogs)
                return;

            timePassedInMinutes = Time.realtimeSinceStartup / 60.0f;

            if (!bTestLogged && Time.realtimeSinceStartup >= testDurationInMinutes * 60.0f)
            {
                Debug.Log("Test over");
                LogData();
                Debug.Log("CSV Log finished");
                bTestLogged = true;
            }
        }

        public void AddAgent(AIAgent agent)
        {
            if (!bEnableLogs)
                return;

            foreach (AIAgentPlanningData data in _data)
            {
                if (data.IsAgent(agent))
                    return;
            }

            _data.Add(new AIAgentPlanningData(agent, _data.Count + 1));
            agent.OnMainGoalsAchieved += OnMainGoalsSucceeded;
        }

        public void LogData()
        {
            if (!bEnableLogs)
                return;

            foreach (AIAgentPlanningData data in _data)
            {
                data.LogData(AgentCount);
            }
        }

        void OnMainGoalsSucceeded(float time)
        {
            AgentsCompletedMainGoal++;

            if (bTestLogged)
                return;

            if (AgentsCompletedMainGoal < AgentCount)
                return;

            LogData();
        }
    }

    [CustomEditor(typeof(PlanningDataGatherer))]
    public class PlanningDataGathererEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PlanningDataGatherer planningDataGatherer = target as PlanningDataGatherer;
            if (!planningDataGatherer.bEnableLogs)
            {
                DrawDefaultInspector();
                return;
            }
            
            if (GUILayout.Button("Log CSV"))
            {
                Debug.Log("Logging CSV manually");
                planningDataGatherer.LogData();
                planningDataGatherer.bTestLogged = true;
            }
            DrawDefaultInspector();
        }
    }
}