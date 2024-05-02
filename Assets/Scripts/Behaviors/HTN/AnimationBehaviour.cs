﻿using Unity.VisualScripting;
using UnityEngine;
using Enums.HTN;

namespace Behaviours.HTN
{
    public class AnimationBehaviour : AgentBehaviorBase
    {
        private Animator animator;
        private static readonly int Walking = Animator.StringToHash("Walking");

        private bool isWalking;
        private bool isMovingLeft;

        protected override void Awake()
        {
            base.Awake();

            this.animator = this.GetComponentInChildren<Animator>();
            
            // Random y offset to prevent clipping
            this.animator.transform.localPosition = new Vector3(0, UnityEngine.Random.Range(-0.1f, 0.1f), 0);
        }

        private void Update()
        {
            this.UpdateAnimation();
            this.UpdateScale();
        }

        private void UpdateAnimation()
        {
            var isWalking = this.agent.State == AgentState.MovingToTarget;

            if (this.isWalking == isWalking)
                return;

            this.isWalking = isWalking;
            
            this.animator.SetBool(Walking, isWalking);
        }

        private void UpdateScale()
        {
            if (!this.isWalking)
                return;
            
            var isMovingLeft = this.IsMovingLeft();

            if (this.isMovingLeft == isMovingLeft)
                return;

            this.isMovingLeft = isMovingLeft;
            
            this.animator.transform.localScale = new Vector3(isMovingLeft ? -1 : 1, 1, 1);
        }

        private bool IsMovingLeft()
        {
            var target = this.agent.CurrentActionData.Target.Position;
            
            return this.transform.position.x > target.x;
        }
    }
}