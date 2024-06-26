using System;
using System.Collections.Generic;
using FluidHTN;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

using HTN.Domain;
using HTN.Sensors;
using Interfaces.HTN;
using Behaviours.HTN;
using Enums.HTN;
using Unity.VisualScripting;
using Classes.Items.HTN;
using Classes.Sources.HTN;
using Interfaces;
using HTN.PlanningData;

namespace HTN
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField][Tooltip("The domain definition for this agent")]
        private AIDomainDefinition _domainDefinition;

        [SerializeField] [Tooltip("The sensing capabilities of our agent")]
        private AISenses _senses;

        [SerializeField] [Tooltip("A Transform representing the head of the agent")]
        private Transform _head;

        private Planner<AIAgentContext> _planner;
        private Domain<AIAgentContext> _domain;
        private AIAgentContext _context;
        private SensorySystem _sensory;
        private AgentMoveBehaviour _movement;

        private ComplexInventoryBehaviour _inventory;

        private HungerBehaviour _hunger;

        private ItemFactory _itemFactory;
        private ItemCollection _itemCollection;
        private InstanceHandler _instanceHandler;
        private GlobalItemManager _itemManager;

        public ITask CurrentTask { 
            get 
        {
            if (_planner == null)
                return null;

            return _planner.CurrentTask;
        }}

        public ITarget CurrentTarget { get => _context.CurrentTarget; }
        public IAgentEvents Events { get; } = new AgentEvents();
        public AgentState State { get; private set; } = AgentState.NoAction;
        public AgentMoveState MoveState { get; set; } = AgentMoveState.Idle;
        public IActionData CurrentActionData { get; private set; }

        public ComplexInventoryBehaviour Inventory { get => _inventory; }
        public HungerBehaviour Hunger { get => _hunger; }

        public ItemFactory itemFactory { get => _itemFactory; }
        public ItemCollection itemCollection { get => _itemCollection; }

        public InstanceHandler instanceHandler { get => _instanceHandler; }
        
        public GlobalItemManager itemManager { get => _itemManager; } 

        public bool IsMovementEnabled { get => _movement.bShouldMove; }

        public Action<float> OnMainGoalsAchieved;
        float TimeStart;
        bool bMainGoalsAchieved = false;


        public Action<int, float> OnNewPlan;
        public Action OnReplan;
        public Action OnPlanSucceeded;

        bool bHasPlannedBefore = false;
        float PlanningStartTime;

        private void Awake()
        {
            if (_domainDefinition == null)
            {
                Debug.LogError($"Missing domain definition in {name}!");
                gameObject.SetActive(false);
                return;
            }

            _planner = new Planner<AIAgentContext>();
            _context = new AIAgentContext(this, _senses, _head, GetComponent<Animator>(), GetComponent<NavMeshAgent>());
            _sensory = new SensorySystem(this);
            _movement = this.GetComponent<AgentMoveBehaviour>();
            _inventory = this.GetComponent<ComplexInventoryBehaviour>();
            _hunger = this.GetComponent<HungerBehaviour>();

            _itemCollection = FindObjectOfType<ItemCollection>();
            _itemFactory = FindObjectOfType<ItemFactory>();
            _instanceHandler = FindObjectOfType<InstanceHandler>();
            _itemManager = FindObjectOfType<GlobalItemManager>();

            _planner.OnNewPlan += OnPlannerNewPlan;
            _planner.OnReplacePlan += OnPlannerReplacedPlan;

            PlanningDataGatherer planningDataGatherer = FindObjectOfType<PlanningDataGatherer>();
            if (planningDataGatherer != null)
            {
                planningDataGatherer.AddAgent(this);
            }

            _itemFactory.OnNewItem += OnNewItem;
            _instanceHandler.OnItemDestroyed += OnItemDestroyed;

            _inventory.OnItemAdded += OnItemObtained;
            _inventory.OnItemRemoved += OnItemLost;

            _itemManager.OnItemClaimed += OnItemClaimed;
            _itemManager.OnItemPickedUp += OnItemPickedUp;
            _itemManager.OnItemDropped += OnItemDropped;

            _domain = _domainDefinition.Create();
        }

        private void Start()
        {
            int iTreeSourceInWorld = FindObjectsOfType<TreeSource>().Length;
            int iIronMineSourceInWorld = FindObjectsOfType<IronMineSource>().Length;
            int iAnvilSourceInWorld = FindObjectsOfType<AnvilSource>().Length;

            byte TreeSourceInWorld = (byte)iTreeSourceInWorld;
            byte IronMineSourceInWorld = (byte)iIronMineSourceInWorld;
            byte AnvilSourceInWorld = (byte)iAnvilSourceInWorld;

            foreach (var item in itemCollection.items)
            {
                OnNewItem(item);
            }
            
            _context.SetState(AIWorldState.TreeIsInWorld, TreeSourceInWorld, EffectType.Permanent);
            _context.SetState(AIWorldState.MineIsInWorld, IronMineSourceInWorld, EffectType.Permanent);
            _context.SetState(AIWorldState.AnvilIsInWorld, AnvilSourceInWorld, EffectType.Permanent);

            TimeStart = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            if (_planner == null || _domain == null || _context == null || _sensory == null)
                return;

            _context.Time = Time.time;
            _context.DeltaTime = Time.deltaTime;

            if (_context.CanSense)
            {
                _sensory.Tick(_context);
            }

            if (_hunger.hunger > 60f)
            {
                _context.SetState(AIWorldState.IsHungry, true, EffectType.Permanent);
            }
            else if (_hunger.hunger < 20.0f)
            {
                _context.SetState(AIWorldState.IsHungry, false, EffectType.Permanent);
            }

            PlanningStartTime = Time.realtimeSinceStartup;
            _planner.Tick(_domain, _context);

            LogDecomposition();

            if (!bMainGoalsAchieved && _context.HasState(AIWorldState.HasAxe, true) && _context.HasState(AIWorldState.HasPickaxe, true))
            {
                OnMainGoalsAchieved?.Invoke(Time.realtimeSinceStartup - TimeStart);
                bMainGoalsAchieved = true;
            }
        }

        private void OnDrawGizmos()
        {
            if (_context == null)
                return;

            _senses?.DrawGizmos(_context);
            _sensory?.DrawGizmos(_context);

    #if UNITY_EDITOR
            var task = _planner.GetCurrentTask();
            if (task != null)
            {
                Handles.Label(_context.Head.transform.position + Vector3.up, task.Name);
            }
    #endif
        }

        public void StartMoving()
        {
            _movement.bShouldMove = true;
        }

        public void StopMoving()
        {
            _movement.bShouldMove = false;
        }

        public void LogDecomposition()
        {
            if (!_context.LogDecomposition)
                return;
            
            while (_context.DecompositionLog?.Count > 0)
            {
                var entry = _context.DecompositionLog.Dequeue();
                var depth = FluidHTN.Debug.Debug.DepthToString(entry.Depth);
                //Debug.Log($"{this.GetInstanceID()} - {depth}{entry.Name}: {entry.Description}");
            }          
        }

        void OnNewItem(ItemBase item)
        {
            _context.IncrementState(AIWorldKeys.IsInWorld(item.GetType()), 1, EffectType.Permanent);
        }

        void OnItemObtained(IHoldable item)
        {
            _context.IncrementState(AIWorldKeys.Has(item.GetType()), 1, EffectType.Permanent);
        }
        void OnItemLost(IHoldable item)
        {
            _context.DecrementState(AIWorldKeys.Has(item.GetType()), 1, EffectType.Permanent);
        }
        void OnItemDestroyed(IHoldable item)
        {
            _context.DecrementState(AIWorldKeys.IsInWorld(item.GetType()), 1, EffectType.Permanent);

            if (item as ItemBase == null)
                return;
        }

        void OnItemClaimed(IHoldable item)
        {
            _context.DecrementState(AIWorldKeys.IsInWorld(item.GetType()), 1, EffectType.Permanent);
        }

        void OnItemPickedUp(IHoldable item)
        {
            
        }

        void OnItemDropped(IHoldable item)
        {
            _context.IncrementState(AIWorldKeys.IsInWorld(item.GetType()), 1, EffectType.Permanent);
        }



        void OnPlannerNewPlan(Queue<ITask> tasks)
        {
            if (bHasPlannedBefore)
            {
                OnPlanSucceeded?.Invoke();
            }

            bHasPlannedBefore = true;
            NewPlan(tasks);
        }

        void OnPlannerReplacedPlan(Queue<ITask> oldPlan, ITask currentTask, Queue<ITask> newPlan)
        {
            OnReplan?.Invoke();
            NewPlan(newPlan);
        }

        void NewPlan(Queue<ITask> tasks)
        {
            OnNewPlan?.Invoke(tasks.Count, Time.realtimeSinceStartup - PlanningStartTime);
        }
    }
}