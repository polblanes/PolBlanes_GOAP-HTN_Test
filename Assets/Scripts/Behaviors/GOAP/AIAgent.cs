using System;
using System.Collections.Generic;
using Classes.Items.GOAP;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using GOAP.PlanningData;
using Unity.VisualScripting;
using UnityEngine;

namespace Behaviours.GOAP
{
    public class AIAgent : AgentBehaviour
    {
        public Action<float> OnMainGoalsAchieved;
        float TimeStart;
        bool bMainGoalsAchieved = false;


        public Action<int, float> OnNewPlan;
        public Action OnReplan;
        public Action OnPlanSucceeded;
        float PlanningStartTime;


        ComplexInventoryBehaviour _inventory;

        protected override void Awake()
        {
            _inventory = GetComponent<ComplexInventoryBehaviour>();
            base.Awake();

            PlanningDataGatherer planningDataGatherer = FindObjectOfType<PlanningDataGatherer>();
            if (planningDataGatherer != null)
            {
                planningDataGatherer.AddAgent(this);
            }
        }

        protected override void Start()
        {
            base.Start();

            TimeStart = Time.realtimeSinceStartup;
        }

        void Update()
        {
            if (!bMainGoalsAchieved && _inventory != null && _inventory.Has<Axe>() && _inventory.Has<Pickaxe>())
            {
                bMainGoalsAchieved = true;

                Debug.Log("Main goal completed");

                OnMainGoalsAchieved?.Invoke(Time.realtimeSinceStartup - TimeStart);
            }
        }

        public override void OnGoapRunnerNewPlan(List<IActionBase> plan) 
        {
            float time = Time.realtimeSinceStartup - PlanningStartTime;
            OnNewPlan?.Invoke(plan.Count, time);
        }

        public override void OnGoapRunnerReplan() 
        {
            OnReplan?.Invoke();
        }

        public override void OnGoapRunnerGoalComplete()
        {
            OnPlanSucceeded?.Invoke();
        }

        public override void OnGoapRunnerPlanningStarted()
        {
            PlanningStartTime = Time.realtimeSinceStartup;
        }
    }
}