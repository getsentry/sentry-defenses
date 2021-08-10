using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Utility.StateMachine;

public class GameStatePlacing : GameState
{
    private readonly PlayerInput _input;
    private readonly GameData _data;
    private UpgradeManager _upgradeManager;
    private readonly EventManager _eventManager;
    private readonly SentryPlacingMenu _sentryPlacingMenu;

    private readonly Transform _mouseTransform;

    private GameObject _tower;
    
    public GameStatePlacing(GameStateMachine stateMachine) : base(stateMachine)
    {
        _input = PlayerInput.Instance;
        _data = GameData.Instance;
        _upgradeManager = UpgradeManager.Instance;
        _eventManager = EventManager.Instance;
        _eventManager.Upgrading += OnUpgrade;
        _sentryPlacingMenu = stateMachine.SentryPlacingMenu;

        _mouseTransform = stateMachine.MouseTransform;
    }

    private void OnUpgrade()
    {    
        if (!IsActive)
        {
            return;
        }
        
        StateTransition(GameStates.Upgrading);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _sentryPlacingMenu.Show();
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseDown() && !Helpers.IsMouseOverUI())
        {
            _tower = GameObject.Instantiate(_data.SentryPrefab, _mouseTransform);
        }
        
        // Checking for tower because the up from the button click gets read here too
        if (_tower != null && _input.GetMouseUp())
        {
            _tower.transform.parent = null;
            _tower = null;
            
            _upgradeManager.BuySentryBuildCost();
            _eventManager.Upgrade();
            
            StateTransition(GameStates.Upgrading);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _sentryPlacingMenu.Hide();
    }
}
