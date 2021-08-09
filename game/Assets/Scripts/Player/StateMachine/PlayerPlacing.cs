using UnityEngine;

public class PlayerPlacing : PlayerState
{
    private readonly PlayerInput _input;
    
    private readonly PlayerData _playerData;
    private readonly Transform _mouseTransform;

    private GameObject tower;
    
    public PlayerPlacing(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _input = PlayerInput.Instance;
        
        _playerData = stateMachine.PlayerData;
        _mouseTransform = stateMachine.MouseTransform;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        var towerData = _playerData.SentryData;
        _playerData.SentryData = null;

        
        tower = GameObject.Instantiate(towerData.Prefab, _mouseTransform);
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseDown())
        {
            tower.GetComponent<Sentry>().Activate();
            tower.transform.parent = null;
            tower = null;
            
            StateTransition(PlayerStates.Idle);
        }
    }
}
