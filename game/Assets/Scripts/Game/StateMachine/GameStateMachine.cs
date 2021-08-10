using Game;
using UnityEngine;
using Utility.StateMachine;

public class GameStateMachine : StateMachine<GameStates>
{
    public Transform MouseTransform;

    [Header("Menus")] 
    public StartMenu StartMenu;
    public UpgradeMenu UpgradeMenu;
    public SentryPlacingMenu SentryPlacingMenu;
    public GameOverMenu GameOverMenu;
    
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
