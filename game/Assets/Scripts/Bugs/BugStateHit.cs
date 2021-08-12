using UnityEngine;

namespace Bugs
{
    public class BugStateHit : BugState
    {
        private float _runtime;
        private readonly BugStateMachine _stateMachine;
    
        public BugStateHit(BugStateMachine stateMachine) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            // _stateMachine.OnHit += () =>
            // {
            //     if (IsActive)
            //     {
            //         StateTransition(BugStates.Hit);    
            //     }
            // };
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _runtime = 0.0f;

            _stateMachine.Rigidbody.velocity = Vector2.zero;

            _stateMachine.HitPoints -= _stateMachine.DamageTaken;
            _stateMachine.DamageTaken = 0;

            _stateMachine.Visuals.Hit(_stateMachine.HitPoints, _stateMachine.HitPointsTotal);
        }

        public override void Tick()
        {
            base.Tick();

            _runtime += Time.deltaTime;
            if (_runtime >= _stateMachine.HitReactionDuration)
            {
                if (_stateMachine.HitPoints > 0)
                    StateTransition(BugStates.Move);
                else
                    StateTransition(BugStates.Despawn);
                return;
            }
        
            var pushBackFactor =_stateMachine.HitReactionCurve.Evaluate(_runtime / _stateMachine.HitReactionDuration);
            _stateMachine.transform.position += _stateMachine.PushBackDirection * (pushBackFactor * Time.deltaTime * _stateMachine.HitPushBackForce);
        }
    }
}
