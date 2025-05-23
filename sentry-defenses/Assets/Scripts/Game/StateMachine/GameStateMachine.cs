using Game;
using UnityEngine;
using Utility.StateMachine;

public class GameStateMachine : StateMachine<GameStates>
{
    public Transform MouseTransform;

    [Header("Menus")] 
    public PickUpgradeMenu PickUpgradeMenu;
    public ApplyUpgradeMenu ApplyUpgradeMenu;
    public GameOverMenu GameOverMenu;

    public UpgradeType PickedUpgrade = UpgradeType.None;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(GameStates.Prepare, new GameStatePrepare(this));
        _states.Add(GameStates.Fight, new GameStateFighting(this));
        _states.Add(GameStates.PickUpgrade, new GameStatePickUpgrade(this));
        _states.Add(GameStates.UpgradeSentry, new GameStateApplyUpgrade(this));
        _states.Add(GameStates.PlaceSentry, new GameStatePlaceSentry(this));
        _states.Add(GameStates.GameOver, new GameStateGameOver(this));
        
        _currentState = _states[GameStates.Prepare];
    }
}
