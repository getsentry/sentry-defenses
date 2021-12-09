using Game;
using UnityEngine;
using Utility.StateMachine;
using Sentry;
using System;

public class GameStateMachine : StateMachine<GameStates>
{
    public Transform MouseTransform;

    [Header("Menus")] 
    public StartMenu StartMenu;
    public UpgradeMenu UpgradeMenu;
    public SentryPlacingMenu SentryPlacingMenu;
    public GameOverMenu GameOverMenu;
    private ITransaction _startTransaction;
    
    public void Awake()
    {
        _startTransaction = SentrySdk.StartTransaction("initialize", "state.machine");
        SentrySdk.ConfigureScope(scope => scope.Transaction = _startTransaction);
        // The transaction will be closed by BugSpawner.
        base.Awake();
    }
    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(GameStates.StartMenu, new GameStateStart(this));
        _states.Add(GameStates.Upgrading, new GameStateUpgrading(this));
        _states.Add(GameStates.SentryPlacing, new GameStatePlacing(this));
        _states.Add(GameStates.Fighting, new GameStateFighting(this));
        _states.Add(GameStates.GameOver, new GameStateGameOver(this));
        
        _currentState = _states[GameStates.StartMenu];
    }
}
