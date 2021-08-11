using UnityEngine;
using Manager;

public class GameStateUpgrading : GameState
{
    private readonly PlayerInput _input;
    private GameData _data;
    private EventManager _eventManager;
    private UpgradeMenu _upgradeMenu;
    private Camera _camera;
    
    private Sentry _selectedSentry = null;
    
    public GameStateUpgrading(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _input = PlayerInput.Instance;
        _eventManager = EventManager.Instance;
        _eventManager.SentryPlacing += OnSentryPlacing;
        _eventManager.Fighting += OnFighting;
        _camera = Camera.main;

        _upgradeMenu = stateMachine.UpgradeMenu;
    }

    private void OnSentryPlacing()
    {
        if (!IsActive)
        {
            return;
        }
        
        StateTransition(GameStates.SentryPlacing);
    }
    
    private void OnFighting()
    {
        if (!IsActive)
        {
            return;
        }
        
        StateTransition(GameStates.Fighting);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _upgradeMenu.Show();
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseDown() && !Helpers.IsMouseOverUI())
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (!hit)
            {
                if (_selectedSentry != null)
                {
                    _selectedSentry.Deselect();
                    _selectedSentry = null;
                }
            }
            else
            {
                var sentry = hit.collider.GetComponent<Sentry>();
                if (_selectedSentry == null)
                {
                    _selectedSentry = sentry;
                }
                else if (_selectedSentry != sentry)
                {
                    _selectedSentry.Deselect();
                    _selectedSentry = sentry;
                }
                
                _selectedSentry.Select();
            }

            _upgradeMenu.UpgradeButtons.SetSelectedSentry(_selectedSentry);
            _eventManager.UpdateCosts();
            _eventManager.UpdateCoins();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _upgradeMenu.Hide(null);
    }
}
