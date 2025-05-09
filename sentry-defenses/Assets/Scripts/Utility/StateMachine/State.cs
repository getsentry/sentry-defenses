﻿using System;
using System.Collections.Generic;
using Sentry;
using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class State<TStateType> where TStateType : Enum
    {
        protected bool IsActive;
        private readonly StateMachine<TStateType> _stateMachine;

        private static string PreviousState = null;
        private static ITransactionTracer PreviousTransaction = null;

        protected State(StateMachine<TStateType> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
            IsActive = true;

            if (_stateMachine.PrintStateTransitions)
            {
                PreviousTransaction?.Finish(SpanStatus.Ok);
               
                var currentState = GetType().Name;

                if (PreviousState != null &&
                    PreviousState != currentState)
                {
                    SentrySdk.AddBreadcrumb($"{PreviousState} -> {currentState}","navigation", "navigation");
                    PreviousTransaction = SentrySdk.StartTransaction(currentState.Replace("GameState", "state.").ToLower(), "state.machine");
                }

                PreviousState =  currentState;
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
