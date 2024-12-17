using DG.Tweening;
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
            _stateMachine.OnHit += OnHit;
            
            _eventManager = EventManager.Instance;
            _eventManager.OnReset += OnReset;
        }

        private void OnHit(float damage)
        {
            if (!IsActive)
            {
                return;
            }

            _stateMachine.ActiveSlowdownDuration = _stateMachine.MaxSlowdownDuration;

            _stateMachine.HitPoints -= damage;
            if (_stateMachine.HitPoints > 0)
            {
                _stateMachine.Visuals.Hit(_stateMachine.HitPoints, _stateMachine.HitPointsTotal);
            }
            else
            {
                StateTransition(BugStates.Despawn);
            }
        }

        private void OnReset()
        {
            if (IsActive)
            {
                if (_stateMachine != null)
                {
                    _stateMachine.Visuals.Kill();
                    GameObject.Destroy(_stateMachine.gameObject);
                }
            }
        }

        public override void Tick()
        {
            base.Tick();

            if (_stateMachine.IsPaused)
            {
                return;
            }

            var movementSpeed = _stateMachine.MovementSpeed;
            if (_stateMachine.ActiveSlowdownDuration > 0)
            {
                movementSpeed *= _stateMachine.SlowdownMultiplier;
            }
            
            var direction = (_targetPosition - _stateMachine.transform.position).normalized;
            _stateMachine.Transform.position += direction * movementSpeed * Time.deltaTime;
            
            if (Vector3.Distance(_stateMachine.Transform.position, _targetPosition) <= 0.1f)
            {
                StateTransition(BugStates.Attack);
            }
        }
    }
}
