using Game;
using UnityEngine;
using Utility.StateMachine;

public class GameStateMachine : StateMachine<GameStates>
{
    public Transform MouseTransform;

    [Header("Menus")] 
    public StartMenu StartMenu;
    public PickUpgradeMenu PickUpgradeMenu;
    public ApplyUpgradeMenu ApplyUpgradeMenu;
    public GameOverMenu GameOverMenu;

    public int PickedUpgrade = -1;
    
    protected override void Awake()
    {
        // SentrySdk.ConfigureScope(scope => scope.Transaction = SentrySdk.StartTransaction("initialize", "state.machine"));
        base.Awake();
    }
    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(GameStates.StartMenu, new GameStateStart(this));
        _states.Add(GameStates.Fight, new GameStateFighting(this));
        _states.Add(GameStates.PickUpgrade, new GameStatePickUpgrade(this));
        _states.Add(GameStates.UpgradeSentry, new GameStateApplyUpgrade(this));
        _states.Add(GameStates.PlaceSentry, new GameStatePlaceSentry(this));
        _states.Add(GameStates.GameOver, new GameStateGameOver(this));
        
        _currentState = _states[GameStates.StartMenu];
    }
}
