using UnityEngine;

namespace Bugs
{
    public class BugStateMove : BugState
    {
        private readonly Vector3 _targetPosition = Vector3.zero;
        private readonly BugStateMachine _stateMachine;
    
        public BugStateMove(BugStateMachine stateMachine) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.OnHit += () =>
            {
                if (IsActive)
                {
                    StateTransition(BugStates.Hit);    
                }
            };
        }

        public override void Tick()
        {
            base.Tick();
            
            var direction = (_targetPosition - _stateMachine.transform.position);
            var distance = direction.magnitude;
            direction.Normalize();

            if (distance <= 0.5f)
            {
                StateTransition(BugStates.Attack);
            }
        
            _stateMachine.Rigidbody.velocity = direction * _stateMachine.MovementSpeed;
        }
    }
}
