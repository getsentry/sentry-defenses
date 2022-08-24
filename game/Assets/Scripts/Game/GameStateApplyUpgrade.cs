using Sentry;
using UnityEngine;

namespace Game
{
    public class GameStateApplyUpgrade : GameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ApplyUpgradeMenu _applyUpgradeMenu;
        
        private readonly PlayerInput _input;
        private readonly Camera _camera;
        
        public GameStateApplyUpgrade(GameStateMachine stateMachine) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            _applyUpgradeMenu = stateMachine.ApplyUpgradeMenu;
            
            _input = PlayerInput.Instance;
            _camera = Camera.main;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _applyUpgradeMenu.Show();

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
                            SentrySdk.AddBreadcrumb("Upgraded Range", "selection", "user");
                            sentry.Upgrades.Range++;
                            break;
                        case 2:
                            SentrySdk.AddBreadcrumb("Upgraded Fire Rate", "selection", "user");
                            sentry.Upgrades.FireRate++;
                            break;
                        case 3:
                            SentrySdk.AddBreadcrumb("Hiding Damage", "selection", "user");
                            sentry.Upgrades.Damage++;
                            break;
                    }

                    sentry.PostUpgrade();
                    
                    StateTransition(GameStates.Fight);
                }
            }
        }
        
        public override void OnExit()
        {
            base.OnExit();
            _applyUpgradeMenu.Hide(null);
        }
    }
}