using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Utility.StateMachine;

public class GameStateUpgrading : GameState
{
    private GameData _data;
    private EventManager _eventManager;
    public GameStateUpgrading(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;

        _eventManager.SentryUpgraded += OnUpgraded;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        _eventManager.Updating();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public void OnUpgraded()
    {
        StateTransition(GameStates.Fighting);
    }
}
