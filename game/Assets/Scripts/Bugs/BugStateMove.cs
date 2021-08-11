using Manager;
using UnityEngine;

namespace Bugs
{
    public class BugStateMove : BugState
    {
        private readonly Vector3 _targetPosition = Vector3.zero;
        private readonly BugStateMachine _stateMachine;
        private EventManager _eventManager;
    
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

            _stateMachine.OnTargetReached += () =>
            {
                if (IsActive)
                {
                    StateTransition(BugStates.Attack);
                }
            };
            
            _eventManager = EventManager.Instance;
            _eventManager.Resetting += OnReset;
        }

        private void OnReset()
        {
            if (IsActive)
            {
                if (_stateMachine != null)
                {
                    GameObject.Destroy(_stateMachine.gameObject);
                }
            }
        }

        public override void Tick()
        {
            base.Tick();
            
            var direction = (_targetPosition - _stateMachine.transform.position).normalized;
            _stateMachine.Rigidbody.velocity = direction * _stateMachine.MovementSpeed;
        }
    }
}
