using System;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class State<TStateType> where TStateType : Enum
    {
        protected bool IsActive;
        private readonly StateMachine<TStateType> _stateMachine;

        protected State(StateMachine<TStateType> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
            IsActive = true;

            if (_stateMachine.PrintStateTransitions)
            {
                Debug.Log($"{GetType().Name}", _stateMachine);
            }
        }

        public virtual void OnExit()
        {
            IsActive = false;
        }

        public virtual void Tick()
        {
        }

        public virtual void FixedTick()
        {
        }

        public virtual void StateTransition(TStateType targetState)
        {
            _stateMachine.TransitionTo(targetState);
        }
    }
}