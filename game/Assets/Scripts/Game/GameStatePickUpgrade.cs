using System;
using Game;
using Manager;
using Random = UnityEngine.Random;

public class GameStatePickUpgrade : GameState
{
    private readonly PickUpgradeMenu _pickUpgradeMenu;
    private readonly GameStateMachine _stateMachine;

    public GameStatePickUpgrade(GameStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _pickUpgradeMenu = _stateMachine.PickUpgradeMenu;
    }
    
    private void SetUpgrade(UpgradeType upgradeType)
    {
        _stateMachine.PickedUpgrade = upgradeType;
        _pickUpgradeMenu.Hide(() =>
        {
            if (upgradeType == UpgradeType.NewTower)
            {
                StateTransition(GameStates.PlaceSentry);
            }
            else
            {
                StateTransition(GameStates.UpgradeSentry);
            }    
        });
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.PickedUpgrade = UpgradeType.None;
        
        EventManager.Instance.PauseGame();

        var upgradeCount = Enum.GetNames(typeof(UpgradeType)).Length;
        var firstUpgrade = Random.Range(1, upgradeCount);
        var secondUpgrade = Random.Range(1, upgradeCount - 1);
        if (secondUpgrade >= firstUpgrade) // because we don't want the same upgrade twice
        {
            secondUpgrade++;
        }

        _pickUpgradeMenu.CreateButton((UpgradeType)firstUpgrade, SetUpgrade);
        _pickUpgradeMenu.CreateButton((UpgradeType)secondUpgrade, SetUpgrade);
        
        _pickUpgradeMenu.Show();
    }
}
