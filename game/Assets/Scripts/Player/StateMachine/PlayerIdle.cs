using Data;
using Manager;

public class PlayerIdle : PlayerState
{
    private readonly PlayerData _playerData;
    private readonly PlayerInput _input;
    
    public PlayerIdle(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _playerData = stateMachine.PlayerData;
        _input = PlayerInput.Instance;
        
        EventManager.Instance.OnPlacing += OnPlacing;
    }

    private void OnPlacing(SentryData sentryData)
    {
        if(!IsActive) return;

        _playerData.SentryData = sentryData;
        StateTransition(PlayerStates.Placing);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
    }

    public override void Tick()
    {
        base.Tick();
    }
}
