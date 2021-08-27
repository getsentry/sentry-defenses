using UnityEngine;
using Manager;

public class GameStateUpgrading : GameState
{
    private readonly PlayerInput _input;
    private GameData _data;
    private EventManager _eventManager;
    private UpgradeMenu _upgradeMenu;
    private Camera _camera;
    
    private SentryTower _selectedSentryTower = null;
    
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
                if (_selectedSentryTower != null)
                {
                    _selectedSentryTower.Deselect();
                    _selectedSentryTower = null;
                }
            }
            else
            {
                var sentry = hit.collider.GetComponent<SentryTower>();
                if (_selectedSentryTower == null)
                {
                    _selectedSentryTower = sentry;
                }
                else if (_selectedSentryTower != sentry)
                {
                    _selectedSentryTower.Deselect();
                    _selectedSentryTower = sentry;
                }
                
                _selectedSentryTower.Select();
            }

            _upgradeMenu.UpgradeButtons.SetSelectedSentry(_selectedSentryTower);
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
