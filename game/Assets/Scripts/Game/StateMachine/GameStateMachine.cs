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

    class DisposableSpan : IDisposable
    {
        private ISpan _span;
        
        public DisposableSpan(ISpan span) => _span = span;

        public void Dispose() => _span?.Finish(SpanStatus.Ok);
    }

    private ITransaction _startTransaction;
    
    public void Awake()
    {
        _startTransaction = SentrySdk.StartTransaction("initialize", "State Machine");
        SentrySdk.ConfigureScope(scope => scope.Transaction = _startTransaction);
        // The transaction will be closed by BugSpawner.
        base.Awake();
    }
    protected override void Initialize()
    {
        base.Initialize();
        
        using (new DisposableSpan(_startTransaction.StartChild("StartMenu", "state")))
        {
            _states.Add(GameStates.StartMenu, new GameStateStart(this));
        }
        using (new DisposableSpan(_startTransaction.StartChild("Upgrading", "state")))
        {
            _states.Add(GameStates.Upgrading, new GameStateUpgrading(this));
        }
        using (new DisposableSpan(_startTransaction.StartChild("SentryPlacing", "state")))
        {
            _states.Add(GameStates.SentryPlacing, new GameStatePlacing(this));
        }
        using (new DisposableSpan(_startTransaction.StartChild("Fighting", "state")))
        {
            _states.Add(GameStates.Fighting, new GameStateFighting(this));
        }
        using (new DisposableSpan(_startTransaction.StartChild("GameOver", "state")))
        {
            _states.Add(GameStates.GameOver, new GameStateGameOver(this));
        }
        
        _currentState = _states[GameStates.StartMenu];
    }
}
