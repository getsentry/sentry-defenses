using UnityEngine;
using Utility.StateMachine;

namespace Game
{
    public class GameStateUpgradeSentry : GameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly PlayerInput _input;
        private readonly Camera _camera;
        
        public GameStateUpgradeSentry(GameStateMachine stateMachine) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            
            _input = PlayerInput.Instance;
            _camera = Camera.main;
        }

        public override void Tick()
        {
            base.Tick();
                    
            if (_input.GetMouseDown() && !Helpers.IsMouseOverUI())
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit)
                {
                    var sentry = hit.collider.GetComponent<SentryTower>();
                    if (sentry == null)
                    {
                        return;
                    }

                    switch (_stateMachine.PickedUpgrade)
                    {
                        case 1:
                            sentry.Upgrades.Range++;
                            break;
                        case 2:
                            sentry.Upgrades.FireRate++;
                            break;
                        case 3:
                            sentry.Upgrades.Damage++;
                            break;
                    }

                    sentry.PostUpgrade();
                    
                    StateTransition(GameStates.Fight);
                }
            }
        }
    }
}