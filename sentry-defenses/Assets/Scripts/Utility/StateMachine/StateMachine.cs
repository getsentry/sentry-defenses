using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class StateMachine<TStateType> : MonoBehaviour where TStateType : Enum
    {
        [field: SerializeField] public bool PrintStateTransitions { get; private set; } = false;
        [field: SerializeField] public bool PrintDebugMessages { get; private set; } = false;
        [field: SerializeField] public bool ShowDebugGizmos{ get; private set; } = false;
        
        protected State<TStateType> _currentState;
        protected Dictionary<TStateType, State<TStateType>> _states;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            Initialize();

            _currentState.OnEnter();
        }

        protected virtual void Update()
        {
            _currentState?.Tick();
        }

        protected virtual void FixedUpdate()
        {
            _currentState?.FixedTick();
        }

        protected virtual void Initialize()
        {
            _states = new Dictionary<TStateType, State<TStateType>>();
        }

        public virtual void TransitionTo(TStateType state)
        {
            _currentState?.OnExit();

            if (_states.ContainsKey(state))
                _currentState = _states[state];

            _currentState?.OnEnter();
        }

        public bool IsStateCurrentState(TStateType state)
        {
            if (_states.ContainsKey(state))
                if (_currentState == _states[state])
                    return true;

            return false;
        }
    }
}