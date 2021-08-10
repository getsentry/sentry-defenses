using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Utility.StateMachine;

public class GameStateUpgrading : GameState
{
    private GameData _data;
    private EventManager _eventManager;
    private UpgradeMenu _upgradeMenu;
    
    public GameStateUpgrading(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;
        _eventManager.SentryPlacing += OnSentryPlacing;
        _eventManager.Fighting += OnFighting;

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
    }

    public override void OnExit()
    {
        base.OnExit();
        _upgradeMenu.Hide(null);
    }
}
