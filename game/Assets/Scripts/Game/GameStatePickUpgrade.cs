using UnityEngine;
using Manager;

public class GameStatePickUpgrade : GameState
{
    private readonly PickUpgradeMenu _pickUpgradeMenu;
    private readonly GameStateMachine _stateMachine;
    
    public GameStatePickUpgrade(GameStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        
        _pickUpgradeMenu = _stateMachine.PickUpgradeMenu;
        _pickUpgradeMenu.UpgradeRange.onClick.AddListener(OnUpgradeRange); 
        _pickUpgradeMenu.UpgradeFireRate.onClick.AddListener(OnUpgradeFireRate);
        _pickUpgradeMenu.UpgradeDamage.onClick.AddListener(OnUpgradeDamage);
        _pickUpgradeMenu.BuildTower.onClick.AddListener(OnBuildTower);
    }

    private void OnUpgradeRange() => SetUpgrade(1);
    private void OnUpgradeFireRate() => SetUpgrade(2);
    private void OnUpgradeDamage() => SetUpgrade(3);
    private void SetUpgrade(int upgrade)
    {
        _stateMachine.PickedUpgrade = upgrade;
        StateTransition(GameStates.UpgradeSentry);
    }
    
    private void OnBuildTower()
    {
        StateTransition(GameStates.PlaceSentry);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.PickedUpgrade = -1;
        
        EventManager.Instance.PauseGame();
        _pickUpgradeMenu.Show();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void OnExit()
    {
        base.OnExit();
        _pickUpgradeMenu.Hide(null);
    }
}
